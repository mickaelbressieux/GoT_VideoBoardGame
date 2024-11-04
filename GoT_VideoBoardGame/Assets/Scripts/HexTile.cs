using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HexTile : MonoBehaviour
{
    public TileType hexType;
    public enum TileType
    {
        River,
        Land,
        Hill,
        Mountain
    }
    void Start()
    {       
    }

    // Update is called once per frame
    void Update()
    {    
    }

    public void HexMountain()
    {
        this.hexType = TileType.Mountain;
        string pathTile = "Low_Poly_Hexagons/Prefabs/Hexagons/Hex7";
        GameObject resourceTile = Resources.Load(pathTile) as GameObject;
        GameObject prefabTile= Instantiate(resourceTile  ,this.transform.position, Quaternion.identity) as GameObject;
        prefabTile.transform.SetParent(transform, true);
    }
        public void HexRiver()
    {
        this.hexType = TileType.River;
        string pathTile = "Low_Poly_Hexagons/Prefabs/Hexagons/Hex19";
        GameObject resourceTile = Resources.Load(pathTile) as GameObject;
        GameObject prefabTile= Instantiate(resourceTile  ,this.transform.position, Quaternion.identity) as GameObject;
        prefabTile.transform.SetParent(transform, true);
    }
        public void HexPlain()
    {
        this.hexType = TileType.Land;
        string pathTile = "Low_Poly_Hexagons/Prefabs/Hexagons/Hex10";
        GameObject resourceTile = Resources.Load(pathTile) as GameObject;
        GameObject prefabTile= Instantiate(resourceTile  ,this.transform.position, Quaternion.identity) as GameObject;
        prefabTile.transform.SetParent(transform, true);
    }
}
