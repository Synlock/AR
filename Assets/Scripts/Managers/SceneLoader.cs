using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    SceneLoader Instance;
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(Instance);
        }
        else Destroy(gameObject);

    }

    public static void LoadMainMenu() => SceneManager.LoadScene("Main Menu");

    public static void LoadFoundationTask1() => SceneManager.LoadScene("Foundation 1");

    public static void LoadFoundationTask2() => SceneManager.LoadScene("Foundation 2");

    public static void LoadARTask1() => SceneManager.LoadScene("EasyAR 1");

    public static void LoadARTask2() => SceneManager.LoadScene("EasyAR 2");
}
