using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public string scenceName;

    private GameObject buttons;
    private GameObject startUI;
    private GameObject endofUI;
    private GameObject maskImg;

    private const float UnitTime = 1.0f;

    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
    }

    private void Start()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    public void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        StartCoroutine(WaitChangeAlpha("White", 0.5f));
        StartCoroutine(WaitChangeAlpha("Start", 2.0f));

        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    public void GetButtonDown()
    {
        StartCoroutine(ChangeAlpha("Button"));
        StartCoroutine(WaitChangeAlpha("White", 1.5f));
        StartCoroutine(WaitLoadScene(scenceName, 3.0f));
    }

    public void Exit()
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

    public IEnumerator WaitChangeAlpha(string targetName, float waitTime)
    {
        float nowTime = 0;
        while (nowTime < waitTime * UnitTime)
        {
            nowTime += Time.deltaTime;
            yield return null;
        }

        StartCoroutine(ChangeAlpha(targetName));
    }

    public IEnumerator ChangeAlpha(string targetName)
    {
        GameObject obj = GameObject.Find(targetName);
        var component = obj.GetComponent<CanvasGroup>();

        var alpha = component.alpha;
        float value0 = alpha, value1 = 1 - alpha;

        // 响应一次
        component.blocksRaycasts = (value0 == 0) ? true : false;

        // 平滑变化
        float nowTime = 0;
        while (nowTime < UnitTime)
        {
            nowTime += Time.deltaTime;
            component.alpha = Mathf.Lerp(value0, value1, nowTime / UnitTime);
            yield return null;
        }

        component.alpha = value1;
    }
}