using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour {

    public float autoLoadNextLevelAfter;

    void Awake()
    {
        //DontDestroyOnLoad(this.gameObject);
    }

    void Start()
    {
        if (autoLoadNextLevelAfter <= 0)
        {
            Debug.Log("Load auto load disabled, use a positive number in seconds.");
        }
        else
        {
            Invoke("LoadNextScene", autoLoadNextLevelAfter);
        }
    }

    public void LoadScene(string sceneName)
    {
        Debug.Log("Level load requested for: " + name);
        SceneManager.LoadScene(sceneName);
    }
    public void QuitRequest()
    {
        Debug.Log("Just Quit!");
        Application.Quit();
    }

    public void LoadNextScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
