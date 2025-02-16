using MarksAssets.ScreenOrientationWebGL;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using ScreenOrientation = MarksAssets.ScreenOrientationWebGL.ScreenOrientationWebGL.ScreenOrientation;


public class Orientation : MonoBehaviour {
	
	[SerializeField] TextMeshProUGUI _pleaseRotate;
	[SerializeField] Image _mobile;
	[SerializeField] Canvas _canvasPortrait;
	[SerializeField] Canvas _canvasLandscape;

	public void setText(int orient) {
		ScreenOrientation orientation = (ScreenOrientation)orient;

		Debug.Log("test");

		//the 'if' is obviously unnecessary. I'm just testing if the comparisons are working as expected. It's an example after all, might as well be thorough.
		if (orientation == ScreenOrientation.Portrait || orientation == ScreenOrientation.PortraitUpsideDown)
		{
			_pleaseRotate.gameObject.SetActive(true);
			_mobile.gameObject.SetActive(true);
            _canvasPortrait.gameObject.SetActive(true);
            _canvasLandscape.gameObject.SetActive(false);
        }
		if (orientation == ScreenOrientation.LandscapeLeft || orientation == ScreenOrientation.LandscapeRight)
		{
            _pleaseRotate.gameObject.SetActive(false);
            _mobile.gameObject.SetActive(false);
            _canvasPortrait.gameObject.SetActive(false);
            _canvasLandscape.gameObject.SetActive(true);
        }
	}
}
