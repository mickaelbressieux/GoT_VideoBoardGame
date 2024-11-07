using System.Collections;
using System.Collections.Generic;
using Microsoft.Unity.VisualStudio.Editor;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class HUDController : MonoBehaviour
{
    public GameObject unitsPanel; // Reference to the UnitsPanel GameObject
    public List<UnitStats> units = new List<UnitStats>(); // List of all units in the scene
    public Sprite sprite; // Sprite to be used for the unit insignia
    public UnitStats unit1; // Test unit to be added to the list
    // Start is called before the first frame update
    void Start()
    {
        unitsPanel = GameObject.Find("UnitsPanel");


        // ----- test with a list of units ------
        List<UnitStats> unitsToAdd = new List<UnitStats>();
        unit1 = new UnitStats();
        //create a unit2 of UnitType Cavalry
        UnitStats unit2 = new UnitStats();
        unit2.unitType = UnitStats.UnitType.Cavalry;

        UnitStats unit3 = new UnitStats();

        unitsToAdd.Add(unit1);
        unitsToAdd.Add(unit2);
        unitsToAdd.Add(unit3);
        addUnits(unitsToAdd);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void addUnits(List<UnitStats> unitsToAdd)
    {
        int count = 0;
        foreach (UnitStats unit in unitsToAdd)
        {
            count++;
            units.Add(unit);
            SpawnInsigna(count, unit);
        }
    }

    private void SpawnInsigna(int count, UnitStats unit)
    {
        //spawn the unit insigna in the UnitsPanel (add image to canvas)
        GameObject unitInsigna = new GameObject();
        unitInsigna.transform.SetParent(unitsPanel.transform);
        //rename the unit insigna
        unitInsigna.name = unit.unitType.ToString() + count;

        //create UI image for the unit insigna
        unitInsigna.AddComponent<UnityEngine.UI.Image>();
        //set the size of the image
        unitInsigna.GetComponent<RectTransform>().sizeDelta = new Vector2(100, 100);
        //set anchor to top left
        unitInsigna.GetComponent<RectTransform>().anchorMin = new Vector2(0, 1);
        unitInsigna.GetComponent<RectTransform>().anchorMax = new Vector2(0, 1);
        //set the position of the image to 220, -100 - (100*(count-1))
        unitInsigna.GetComponent<RectTransform>().anchoredPosition = new Vector2(220, -100 - (100 * (count - 1)));

        unitInsigna.AddComponent<UnityEngine.UI.Button>();
        unitInsigna.GetComponent<UnityEngine.UI.Button>().onClick.AddListener(() => AddUnitFromPanelToBattlefield());

        print("Unit added: " + unit.unitType.ToString());

        string imageName = unit.unitType.ToString();
        // load the sprite from the resources folder
        sprite = Resources.Load<Sprite>("Images/" + imageName);



        // check that the sprite is not null
        if (sprite == null)
        {
            Debug.LogError("Sprite is null. Check the path and file name: " + "Images/" + imageName);
        }
        else
        {
            unitInsigna.GetComponent<UnityEngine.UI.Image>().sprite = sprite;
        }
    }

    private void AddUnitFromPanelToBattlefield()
    {
        //When the unit insigna is clicked, add the unit to the battlefield where the mouse is clicked next
        Debug.Log("Unit beinmg added to battlefield... Waiting for click");

        //get the unit that was clicked
        GameObject unitInsigna = UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject;
        string unitName = unitInsigna.name;

        Debug.Log("Unit selected: " + unitName);

        // send the selected unit to the UnitControl in player and set isPositioningUnit to true
        GameObject player = GameObject.Find("Player");

        
        player.GetComponent<UnitControl>().selectedUnit = unitName;
        player.GetComponent<UnitControl>().isPositioningUnit = true;



    }

    public void RemoveUnitFromPanel(string unitName)
    {
        //remove the unit from the panel when it is placed on the battlefield
        
        //Debug.Log("Removing unit: " + unitName);
        
        Transform unitInsignaTransform = unitsPanel.transform.Find(unitName);
        if (unitInsignaTransform != null)
        {
            GameObject unitInsigna = unitInsignaTransform.gameObject;
            Destroy(unitInsigna);
        }
        else
        {
            Debug.LogError("Unit insigna with name " + unitName + " not found within unitsPanel.");
        }
    }
}
        
