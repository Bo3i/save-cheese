using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TerrainSlicer : MonoBehaviour
{
    [SerializeField] private int slicingTime = 10;

    [SerializeField] private Tilemap groundTilemap;
    [SerializeField] private Tilemap resourcesTilemap;
    [SerializeField] private Tilemap fuellTilemap;
    [SerializeField] private Tilemap wallsTilemap;

    private Tilemap[] tilemaps;

    private int lastSliced;
    private int lastSlicedPos;

    void Start()
    {
        tilemaps = new Tilemap[4];
        tilemaps[0] = groundTilemap;
        tilemaps[1] = resourcesTilemap;
        tilemaps[2] = fuellTilemap;
        tilemaps[3] = wallsTilemap;
        lastSliced = 0;
        lastSlicedPos = -30;
    }

    void Update()
    {
        Slice();
    }

    private void Slice()
    {
        if (CanSlice())
        {
            Debug.Log("Slicing");

            lastSliced = (int)Time.time;
            for (int i = 0; i < tilemaps.Length; i++)
            {
                for (int j = 1; j > -10; j--)
                {
                    tilemaps[i].SetTile(new Vector3Int(lastSlicedPos+1, j, 0), null);
                }
            }
            lastSlicedPos ++;
        }
    }

    

    private bool CanSlice()
    {
        bool ret = false;
        if ((int)Time.time - lastSliced > slicingTime)
        {
            ret = true;
        }
        return ret;
    }
}
