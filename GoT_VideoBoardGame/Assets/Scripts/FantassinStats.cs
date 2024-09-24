using UnityEngine;

public class UnitStats : MonoBehaviour
{
    // Define the basic stats
    public enum UnitType { Infantry, Cavalry, Archer }

    public UnitType unitType;
    public int HP = 100; // Health points in units
    public int attack = 20;
    public int speed = 2; // Speed in squares per turn
    public int range = 1; // Attack range in squares
    public int morale = 100; // Morale in percentage

    // Defin rectangle width and length with respect to the remaining HP
    public float GetWidth()
    {
        // Return the width of the rectangle
        return (float)(HP / 100)*20 + 10;
    }

    public float GetLength()
    {
        // Return the length of the rectangle
        return 10;
    }

}
