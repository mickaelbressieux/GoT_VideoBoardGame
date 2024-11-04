using System.Collections;
using System.Collections.Generic;
using UnityEngine;




public class HexGridLayout : MonoBehaviour
{
    public enum MapType
    {
        River,
        Land,
        Hill,
        Mountain
    }

    [Header("Grid Setting")]
    public Vector2Int gridSize;
    List<List<HexTile>> hexList = new List<List<HexTile>>();
    public MapType mapType = MapType.Land;

    [Header("Tile Setting")]
    public float hexSize = 0.5f;
    //public float innerSize = 0.9f;
    public float height = 0f;
    public bool isFlatTopped;

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
      ApplyMapType(mapType);

    }
  }

  public void LayoutGrid()
  {
    for (int y = 0; y <gridSize.y; y++)
    {
        List<HexTile> tempList = new List<HexTile>();
        for (int x = 0; x<gridSize.x;x++)
        {



            GameObject tile = new GameObject($"Hex {y},{x}", typeof(HexTile));
            tile.transform.position = GetPositionForHexFromCoordinate(new Vector2Int(x,y));
            HexTile hexTile = tile.GetComponent<HexTile>();
            tempList.Add(hexTile);
            hexTile.transform.SetParent(transform, true);
            if(Random.Range(0,5)>1)
            {
              hexTile.HexPlain();
            }
            else
            {
              hexTile.HexMountain();
            }
        }
        hexList.Add(tempList);
    }
  }

// Créer la matrice de proba d'apparition
//          | Land | Mountain|
//   Land   | 0.9  |    1    |
// Mountain | 0.5  |    1    |
    public void ApplyMapType(MapType mapType)
    {
        List<HexTile.TileType> listType = new  List<HexTile.TileType>();
        List<List<float>> probaTile = new List<List<float>>();

      if(mapType == MapType.Land)
      {
        listType = new  List<HexTile.TileType>
        {HexTile.TileType.Land, HexTile.TileType.Mountain};
        probaTile = new List<List<float>>
        {
          new List<float>() {0.9f,1.0f},
          new List<float>() {0.5f,1.0f}
        };

      }

      for (int y = 0; y <gridSize.y; y++)
      {
          for (int x = 0; x<gridSize.x;x++)
          {

            ApplyHexType(hexList[y][x], listType, probaTile);
          }
      }
     }

    public void ApplyHexType(HexTile currentTile, List<HexTile.TileType> listType, List<List<float>> probaTile)
    {
    
      int indexType = listType.IndexOf(currentTile.hexType);
      Debug.Log(indexType);
      // Chercher la position du currentTile dans la liste TileType pour savoir quelle ligne lire
      // Appliquer de façon aléatoire la valeur à un de ses voisins
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
        float size = hexSize;

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
