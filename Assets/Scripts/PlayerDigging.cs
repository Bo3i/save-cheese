using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class PlayerDigging : MonoBehaviour
{
    [SerializeField] private Tilemap tilemap;
    [SerializeField] private TileBase tile;
    [SerializeField] private TileBase grassLeft;
    [SerializeField] private TileBase grassRight;
    [SerializeField] private TileBase dirtLeftTop;
    [SerializeField] private TileBase dirtRightTop;
    [SerializeField] private TileBase dirtLeft;
    [SerializeField] private TileBase dirtRight;
    [SerializeField] private TileBase dirtBottom;
    [SerializeField] private TileBase dirtTop;
    [SerializeField] private TileBase dirtLeftBottom;
    [SerializeField] private TileBase dirtRightBottom;
    [SerializeField] private TileBase dirtMiddle;

    private enum Neighbour { up, down, left, right }
    private enum TileType
    {
        grassLeft, grassRight, dirtLeftTop, dirtRightTop, dirtLeft, dirtRight, dirtBottom, dirtTop, dirtLeftBottom, dirtRightBottom, dirtMiddle
    }
    private Vector3Int[] neighbourCells = new Vector3Int[4];

    // Update is called once per frame
    private void Update()
    {
        Dig();

    }

    private void UpdateNeighbours(Vector3Int tile)
    {
        
    }

    private void Dig()
    {
        Vector2 dir = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        Vector3Int cell = tilemap.WorldToCell(transform.position);
        if (Input.GetKeyDown(KeyCode.E) && dir != new Vector2(0, 0))
        {
            cell.x += (int)dir.x;
            cell.y += (int)dir.y;
            tilemap.SetTile(cell, null);
        }
    }
}
