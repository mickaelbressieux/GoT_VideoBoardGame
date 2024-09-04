using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    private bool justSwitched = false;


    private void Awake()
    {
        // Ensure that the GameManager persists across scenes
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);

            // Subscribe to the sceneLoaded event
            SceneManager.sceneLoaded += OnSceneLoaded;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {

        LinkBackButton();
        if (justSwitched)
        {
            justSwitched = false; // Reset the flag
            return; // Skip the first frame after switching scenes
        }

    }

    public void LoadScene(string sceneName)
    {
        justSwitched = true;
        SceneManager.LoadScene(sceneName);
    }



    // Method to switch to the Combat Simulation phase
    public void SwitchToCombatPhase()
    {
        Debug.Log("Switch to combat scene");
        LoadScene("CombatPhaseScene");
    }

    // Method to switch back to the Strategy Phase
    public void SwitchToStrategyPhase()
    {
        Debug.Log("Switch to sample scene");
        LoadScene("SampleScene");
    }

    // Method to link the back button in the Combat Phase Scene
    private void LinkBackButton()
    {
        // Try to find the button by name in the current scene
        Button backButton = GameObject.Find("SwitchPhaseButton")?.GetComponent<Button>();

        if (backButton != null)
        {
            // Clear any existing listeners and add a new one
            backButton.onClick.RemoveAllListeners();
            backButton.onClick.AddListener(() => SwitchToStrategyPhase());
            Debug.Log("Button set up");
        }
        else
        {
            Debug.Log("SwitchPhaseButton not found in the current scene.");
        }
    }
}
