using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.ResourceManagement.ResourceProviders;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class AddressableManager : MonoBehaviour
{
    [SerializeField] AssetReference _addressableSceneName;
    //[SerializeField] IList<SceneInstance> _loadScenesMode;
    [SerializeField] AsyncOperationHandle<SceneInstance> _handle;
    [SerializeField] Slider _percentDownloadUI;
    [SerializeField] TextMeshProUGUI _percentText;
    AssetReference _cacheScene;

    private void Start()
    {
        StartCoroutine(CheckSceneInCache(_addressableSceneName));
    }

    public void AddressableScene(AssetReference sceneName)
    {
        StartCoroutine(LoadAddressableScene(sceneName));
    }

    private IEnumerator LoadAddressableScene(AssetReference sceneName)
    {
        var downloadSize = Addressables.GetDownloadSizeAsync(sceneName);
        yield return downloadSize;

        Debug.Log(downloadSize);
        bool isInCache = downloadSize.Result == 0;
        _percentDownloadUI.gameObject.SetActive(!isInCache);

        if (!isInCache)
        {
            _handle = Addressables.LoadSceneAsync(sceneName, UnityEngine.SceneManagement.LoadSceneMode.Single, false);

            while (!_handle.IsDone)
            {
                if (_handle.IsValid())
                {
                    StartCoroutine(CheckSceneInCache(_cacheScene));
                    float percent = _handle.GetDownloadStatus().Percent;
                    _percentDownloadUI.value = percent;
                    _percentText.text = (percent * 100).ToString("0.00") + "%";
                    Debug.Log((percent * 100).ToString("0.00") + "%");

                    if (_handle.PercentComplete == 1f)
                    {
                        Addressables.Release(_handle);
                    }
                }
                yield return null;
            }
        }
    }

    public void onLoadScene()
    {
        AddressableScene(_addressableSceneName);
    }

    public void onDelete()
    {
#if UNITY_EDITOR
        Debug.Log("Clearing cache is not applicable in the Editor when using the Asset Database.");
#else
StartCoroutine(ClearAddressablesCache());
#endif

    }

    public IEnumerator ClearAddressablesCache()
    {
        var CachingOperation = Addressables.CleanBundleCache();
        yield return CachingOperation;
    }

    IEnumerator CheckSceneInCache(AssetReference asset)
    {
        var downloadSize = Addressables.GetDownloadSizeAsync(asset);
        yield return downloadSize;

        bool isInCache = downloadSize.Result == 0;
        _percentDownloadUI.gameObject.SetActive(!isInCache);

        if (isInCache)
        {
            Debug.Log("<color=green>Asset is in Cache!</color>");
        }
        else
        {
            Debug.Log("<color=red>Asset is NOT in Cache!</color>");
        }
    }
}
