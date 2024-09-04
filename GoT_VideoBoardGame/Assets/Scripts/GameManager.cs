using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    // Singleton pattern to ensure only one instance of GameManager exists
    public static GameManager Instance { get; private set; }

    private void Awake()
    {
        // Ensure that the GameManager persists across scenes
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // Method to load a new scene
    public void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    // Method to switch to the Combat Simulation phase
    public void SwitchToCombatPhase()
    {
        LoadScene("CombatPhaseScene");
    }

    // Method to switch back to the Strategy Phase
    public void SwitchToStrategyPhase()
    {
        LoadScene("SampleScene");
    }
}
