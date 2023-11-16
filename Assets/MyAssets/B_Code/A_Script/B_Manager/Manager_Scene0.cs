using UnityEngine;
using UnityEngine.SceneManagement;

public class Manager_Scene0 : MonoBehaviour
{
    void Start()
    {
        SceneManager.sceneLoaded += OnSceneLoad;
    }

    public void OnSceneLoad(Scene scene, LoadSceneMode mode)
    {
        Debug.Log("Hello");

        SceneManager.sceneLoaded -= OnSceneLoad;
    }
}