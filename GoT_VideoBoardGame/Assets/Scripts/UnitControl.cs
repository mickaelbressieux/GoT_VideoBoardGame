using UnityEngine;

public class UnitSelection : MonoBehaviour
{
    private Renderer unitRenderer;
    private Color originalColor;
    public Color selectedColor = Color.yellow;

    private void Start()
    {
        unitRenderer = GetComponent<Renderer>();
        originalColor = unitRenderer.material.color;
    }

    private void OnMouseDown()
    {
        SelectUnit();
    }

    public void SelectUnit()
    {
        unitRenderer.material.color = selectedColor;
        // Additional logic for selection can be added here (e.g., showing UI)
    }

    public void DeselectUnit()
    {
        unitRenderer.material.color = originalColor;
    }
}
