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
            Debug.Log("Mouse button clicked");
            // Create a ray from the camera to the mouse cursor
            Ray ray = playerCamera.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            // Check if the ray hits any object
            if (Physics.Raycast(ray, out hit))
            {
                SelectUnit(hit);
            }
            else
            {
                // Deselect all units if the player clicks on an empty space
                Debug.Log("Empty space clicked");
                selectedUnits.ForEach(unit => unit.DeselectUnit());
            }
        }

        if (isPositioningUnit)
        {
            if (Input.GetMouseButtonDown(0)) // Check if the player has clicked the left mouse button
            {
                Ray ray = playerCamera.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;

                Debug.Log("Mouse button clicked while positioning unit");

                if (Physics.Raycast(ray, out hit))
                {
                    if (hit.collider.CompareTag("Battleground")) // Check if the ray hits the terrain
                    {
                        //Instantiate the selected unit at the position of the mouse click
                        

                        Debug.Log("Instantiating unit: " + selectedUnit + " at position: " + hit.point);

                        GameObject unit = Instantiate(Resources.Load(selectedUnit), hit.point, Quaternion.identity) as GameObject;
                        
                    }
                }
            }
        }
    }

    private void SelectUnit(RaycastHit hit)
    {
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
        }
    }
}

