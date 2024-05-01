using UnityEngine;
using UnityEngine.SceneManagement; // Importa el espacio de nombres SceneManager

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    private bool isGamePaused = false;
    private int collectiblesCount = 0;
    public string currentSceneName;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        currentSceneName = scene.name;
    }

    public bool IsGamePaused()
    {
        return isGamePaused;
    }

    public void PauseGame()
    {
        isGamePaused = true;
        Time.timeScale = 0f;
    }

    public void ResumeGame()
    {
        isGamePaused = false;
        Time.timeScale = 1f;
    }

    public void CollectCollectible()
    {
        collectiblesCount++;
    }

    public int GetCollectiblesCount()
    {
        return collectiblesCount;
    }

    public string GetCurrentSceneName()
    {
        return currentSceneName;
    }

    public void ActivateTransition(GameObject _obj)
    {
        _obj.SetActive(true);
    }
}
