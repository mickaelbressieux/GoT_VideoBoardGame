using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    //Ensure that this game object persists between scenes
    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }
    public void LoadScene(string sceneName)
    {
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


}
