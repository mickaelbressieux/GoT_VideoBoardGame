using System.Collections.Generic;
using UnityEngine;


public class UnitControl : MonoBehaviour
{
    private GameObject cameraRig;
    private Camera playerCamera;
    public List<UnitStats> selectedUnits = new List<UnitStats>();
    public bool isPositioningUnit = false;
    public string selectedUnit;

    void Start()
    {
        // Access the CameraRig child object
        cameraRig = transform.Find("CameraRig").gameObject;

        if (cameraRig != null)
        {
            // Access the Camera child object of CameraRig
            playerCamera = cameraRig.transform.Find("Camera").GetComponent<Camera>();

            if (playerCamera != null)
            {
                Debug.Log("Camera found: " + playerCamera.name);
            }
            else
            {
                Debug.LogError("Camera not found under CameraRig");
            }
        }
        else
        {
            Debug.LogError("CameraRig not found under Player");
        }
    }

    void Update()
    {
        // Check if the player has clicked the left mouse button
        if (Input.GetMouseButtonDown(0))
        {
            // Create a ray from the camera to the mouse cursor
            Ray ray = playerCamera.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            // Check if the ray hits any object
            if (isPositioningUnit) // Phase 1: Fielding Units
            {
                if (Physics.Raycast(ray, out hit))
                {
                    PositionUnit(hit);
                    
                }
            }
            else if (Physics.Raycast(ray, out hit)) // Phase 2: Selecting Units for repositioning and orientation
            {
                SelectUnit(hit);
            }
            else
            {
                // Deselect all units if the player clicks on an empty space
                selectedUnits.ForEach(unit => unit.DeselectUnit());
            }
        }

        
    }

    private void PositionUnit(RaycastHit hit)
    {
        // Position the unit from the panel on the battlefield
        if (hit.collider.CompareTag("Battleground")) // Check if the ray hits the terrain
        {
            //Instantiate the selected unit at the position of the mouse click
            Debug.Log("Instantiating unit: " + selectedUnit + " at position: " + hit.point);
            string unitType = selectedUnit.Substring(0, selectedUnit.Length - 1);
            GameObject unit = Instantiate(Resources.Load(unitType), hit.point, Quaternion.identity) as GameObject;
            
            unit.name = selectedUnit;

            askRemoveUnit(selectedUnit);
            selectedUnit = null;
            isPositioningUnit = false;
        }
    }

    private void SelectUnit(RaycastHit hit)
    {
        // Manage unit selection
        // Check if the object hit by the ray is a unit
        if (hit.collider.CompareTag("Unit"))
        {
            // Get the UnitStats component of the unit
            UnitStats unitStats = hit.collider.GetComponent<UnitStats>();
            Debug.Log("Unit clicked: " + unitStats.name);
            if (unitStats != null)
            {
                // Check if the unit is already selected
                if (unitStats.isSelected)
                {
                    // Deselect the unit
                    unitStats.DeselectUnit();
                    selectedUnits.Remove(unitStats);
                }
                else
                {
                    // Deselect all other units
                    selectedUnits.ForEach(unit => unit.DeselectUnit());

                    // Select the unit
                    unitStats.SelectUnit();
                    selectedUnits.Add(unitStats);
                }
            }
        }
        else if (hit.collider.CompareTag("Battleground"))
        {
            Debug.Log("Battleground clicked");
            selectedUnits.ForEach(unit => unit.DeselectUnit());
        }
    }
    private void askRemoveUnit(string unitName)
    {
        Debug.Log("Removing unit from panel: " + unitName);
        // Use removeUnitFromPanel to remove the unit from the panel
        GameObject.Find("HUD").GetComponent<HUDController>().RemoveUnitFromPanel(unitName);
    }
}

