using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HexGridLayout : MonoBehaviour
{

    [Header("Grid Setting")]
    public Vector2Int gridSize;

    [Header("Tile Setting")]
    public float outerSize = 1f;
    public float innerSize = 0.9f;
    public float height = 0.1f;
    public bool isFlatTopped;
    public Material material;
    // Start is called before the first frame update
    public void onEnable()
    {
        LayoutGrid();
    }

    // Update is called once per frame
  public void OnValidate()
  {
    if (Application.isPlaying)
    {
      LayoutGrid();
    }
  }

  public void LayoutGrid()
  {
    for (int y = 0; y <gridSize.y; y++)
    {
        for (int x = 0; x<gridSize.x;x++)
        {
            GameObject tile = new GameObject($"Hex {x},{y}", typeof(HexRenderer));
            tile.transform.position = GetPositionForHexFromCoordinate(new Vector2Int(x,y));

            HexRenderer hexRenderer = tile.GetComponent<HexRenderer>();
            hexRenderer.isFlatTopped = isFlatTopped;
            hexRenderer.outerSize = outerSize;
            hexRenderer.innerSize = innerSize;
            hexRenderer.height = height;
            hexRenderer.SetMaterial(material);
            hexRenderer.DrawMesh();

            tile.transform.SetParent(transform, true);
        }
    }
  }
    public Vector3 GetPositionForHexFromCoordinate(Vector2Int coordinate)
    {
        int column = coordinate.x;
        int row = coordinate.y;
        float width;
        float height;
        float xPosition;
        float yPosition;
        bool shouldOffset;
        float horizontalDistance;
        float verticalDistance;
        float offset;
        float size = outerSize;

        if (!isFlatTopped)
        {
          shouldOffset = (row%2) == 0;
          width = Mathf.Sqrt(3)*size;
          height = 2f *size;

          horizontalDistance = width;
          verticalDistance = height * (3f/4f);

          offset = (shouldOffset) ? width/2 : 0;

          xPosition = (column*(horizontalDistance))+offset;
          yPosition = (row * verticalDistance);
        }
        else
        {
          shouldOffset = (row%2) == 0;
          height = Mathf.Sqrt(3)*size;
          width = 2f *size;

          verticalDistance = width;
          horizontalDistance = height * (3f/4f);

          offset = (shouldOffset) ? width/2 : 0;

          xPosition = (column*(horizontalDistance));
          yPosition = (row * verticalDistance) - offset;
        }
        return new Vector3(xPosition,0,-yPosition);

    }

}
