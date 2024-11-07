using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CombatPhaseGameManager : MonoBehaviour
{
    

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Combat Phase Game Manager Loaded");
        // Link the button to the GameManager function "SwitchToStrategyPhase"
        Button switchPhaseButton = GameObject.Find("SwitchPhaseButton").GetComponent<UnityEngine.UI.Button>();
        Debug.Log(switchPhaseButton);
        GameObject gameManager = GameObject.Find("GameManager");
        Debug.Log(gameManager);
        switchPhaseButton.onClick.AddListener(() => gameManager.GetComponent<GameManager>().SwitchToStrategyPhase());


        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
