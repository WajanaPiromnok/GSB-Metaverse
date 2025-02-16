using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DressingScene : MonoBehaviour
{
    [SerializeField] GameObject _dressingScene;
    // Start is called before the first frame update
    void Start()
    {
        _dressingScene = GameObject.FindGameObjectWithTag("Player");
        // Create a temporary reference to the current scene.
        Scene currentScene = SceneManager.GetActiveScene();

        // Retrieve the name of this scene.
        string sceneName = currentScene.name;

        if (sceneName == "Character_Aomsin")
        {
            _dressingScene.GetComponent<Animator>().Play("Idle");
        }

        if (sceneName == "Character_Aomsin_F")
        {
            _dressingScene.GetComponent<Animator>().Play("Idle");
        }

        if (sceneName == "Character_Aomsin_M")
        {
            _dressingScene.GetComponent<Animator>().Play("Idle");
        }
    }

    // Update is called once per frame
    void Update()
    {
        _dressingScene = GameObject.FindGameObjectWithTag("Player");
        // Create a temporary reference to the current scene.
        Scene currentScene = SceneManager.GetActiveScene();

        string sceneName = currentScene.name;

        if (sceneName == "Character_Aomsin")
        {
            _dressingScene.GetComponent<Animator>().Play("Idle");
        }

        if (sceneName == "Character_Aomsin_F")
        {
            _dressingScene.GetComponent<Animator>().Play("Idle");
        }

        if (sceneName == "Character_Aomsin_M")
        {
            _dressingScene.GetComponent<Animator>().Play("Idle");
        }
    }
}
