using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : Manager_UI
{
    public string scenceName;

    // private void Awake()
    // {
    //     DontDestroyOnLoad(this.gameObject);
    // }

    private void Start()
    {
        SceneManager.sceneLoaded += OnSceneLoad;
    }

    public void OnSceneLoad(Scene scene, LoadSceneMode mode)
    {
        StartCoroutine(WaitChangeAlpha("White", 0.5f));
        StartCoroutine(WaitChangeAlpha("Start", 2.0f));

        SceneManager.sceneLoaded -= OnSceneLoad;
    }

    public void Button_Play()
    {
        StartCoroutine(ChangeAlpha("Button"));
        StartCoroutine(WaitChangeAlpha("White", 1.5f));
        StartCoroutine(WaitLoadScene(scenceName, 3.0f));
    }

    public void Button_Exit()
    {
        // UnityEditor.EditorApplication.isPlaying = false;
        Application.Quit();
    }

    public IEnumerator WaitLoadScene(string targetName, float waitTime)
    {
        float nowTime = 0;
        while (nowTime < waitTime * UnitTime)
        {
            nowTime += Time.deltaTime;
            yield return null;
        }

        SceneManager.LoadScene(targetName);
    }
}