using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

public class UpdateTiles : MonoBehaviour
{
    [SerializeField] private Tilemap groundTilemap;
    [SerializeField] private Tilemap resourcesTilemap;
    [SerializeField] private Tilemap fuellTilemap;

    [SerializeField] private TileBase tile;
    [SerializeField] private TileBase noEntranceCheese;

    [SerializeField] private TileBase cheeseLeftTop;
    [SerializeField] private TileBase cheeseRightTop;
    [SerializeField] private TileBase cheeseLeft;
    [SerializeField] private TileBase cheeseRight;
    [SerializeField] private TileBase cheeseBottom;
    [SerializeField] private TileBase cheeseTop;
    [SerializeField] private TileBase cheeseLeftBottom;
    [SerializeField] private TileBase cheeseRightBottom;
    [SerializeField] private TileBase cheeseMiddle;
    [SerializeField] private TileBase cheeseAll;
    [SerializeField] private TileBase cheeseTopLeftRight;
    [SerializeField] private TileBase cheeseLeftRight;
    [SerializeField] private TileBase cheeseBottomLeftRight;
    [SerializeField] private TileBase cheeseTopBottom;
    [SerializeField] private TileBase cheeseLeftTopBottom;
    [SerializeField] private TileBase cheeseRightTopBottom;

    [SerializeField] private TileBase fuellcheeseLeftTop;
    [SerializeField] private TileBase fuellcheeseRightTop;
    [SerializeField] private TileBase fuellcheeseLeft;
    [SerializeField] private TileBase fuellcheeseRight;
    [SerializeField] private TileBase fuellcheeseBottom;
    [SerializeField] private TileBase fuellcheeseTop;
    [SerializeField] private TileBase fuellcheeseLeftBottom;
    [SerializeField] private TileBase fuellcheeseRightBottom;
    [SerializeField] private TileBase fuellcheeseMiddle;
    [SerializeField] private TileBase fuellcheeseAll;
    [SerializeField] private TileBase fuellcheeseTopLeftRight;
    [SerializeField] private TileBase fuellcheeseLeftRight;
    [SerializeField] private TileBase fuellcheeseBottomLeftRight;
    [SerializeField] private TileBase fuellcheeseTopBottom;
    [SerializeField] private TileBase fuellcheeseLeftTopBottom;
    [SerializeField] private TileBase fuellcheeseRightTopBottom;

    [SerializeField] private TileBase resourcecheeseLeftTop;
    [SerializeField] private TileBase resourcecheeseRightTop;
    [SerializeField] private TileBase resourcecheeseLeft;
    [SerializeField] private TileBase resourcecheeseRight;
    [SerializeField] private TileBase resourcecheeseBottom;
    [SerializeField] private TileBase resourcecheeseTop;
    [SerializeField] private TileBase resourcecheeseLeftBottom;
    [SerializeField] private TileBase resourcecheeseRightBottom;
    [SerializeField] private TileBase resourcecheeseMiddle;
    [SerializeField] private TileBase resourcecheeseAll;
    [SerializeField] private TileBase resourcecheeseTopLeftRight;
    [SerializeField] private TileBase resourcecheeseLeftRight;
    [SerializeField] private TileBase resourcecheeseBottomLeftRight;
    [SerializeField] private TileBase resourcecheeseTopBottom;
    [SerializeField] private TileBase resourcecheeseLeftTopBottom;
    [SerializeField] private TileBase resourcecheeseRightTopBottom;


    private Tilemap tilemap;

    private enum Neighbour { up, down, left, right }
    private enum TileType
    {
        dirtLeftTop, dirtRightTop, dirtLeft, dirtRight, dirtBottom, dirtTop, dirtLeftBottom, dirtRightBottom, dirtMiddle
    }

    public void UpdateNeighbours(Vector3Int tile)
    {
        //Debug.Log("Neighbours updated");

        Vector3Int[] neighbourCells = new Vector3Int[4];
        neighbourCells[0] = new Vector3Int(tile.x, tile.y + 1, tile.z);
        neighbourCells[1] = new Vector3Int(tile.x, tile.y - 1, tile.z);
        neighbourCells[2] = new Vector3Int(tile.x - 1, tile.y, tile.z);
        neighbourCells[3] = new Vector3Int(tile.x + 1, tile.y, tile.z);



        TileBase cheeseNeighbourTileUp = groundTilemap.GetTile(neighbourCells[0]);
        TileBase cheeseNeighbourTileDown = groundTilemap.GetTile(neighbourCells[1]);
        TileBase cheeseNeighbourTileLeft = groundTilemap.GetTile(neighbourCells[2]);
        TileBase cheeseNeighbourTileRight = groundTilemap.GetTile(neighbourCells[3]);

        TileBase fuellNeighbourTileUp = fuellTilemap.GetTile(neighbourCells[0]);
        TileBase fuellNeighbourTileDown = fuellTilemap.GetTile(neighbourCells[1]);
        TileBase fuellNeighbourTileLeft = fuellTilemap.GetTile(neighbourCells[2]);
        TileBase fuellNeighbourTileRight = fuellTilemap.GetTile(neighbourCells[3]);

        TileBase resourceNeighbourTileUp = resourcesTilemap.GetTile(neighbourCells[0]);
        TileBase resourceNeighbourTileDown = resourcesTilemap.GetTile(neighbourCells[1]);
        TileBase resourceNeighbourTileLeft = resourcesTilemap.GetTile(neighbourCells[2]);
        TileBase resourceNeighbourTileRight = resourcesTilemap.GetTile(neighbourCells[3]);

        if (cheeseNeighbourTileUp != null || fuellNeighbourTileUp != null || resourceNeighbourTileUp != null)
        {
            if (cheeseNeighbourTileUp == cheeseMiddle) groundTilemap.SetTile(neighbourCells[0], cheeseBottom);
            if (cheeseNeighbourTileUp == cheeseLeft) groundTilemap.SetTile(neighbourCells[0], cheeseLeftBottom);
            if (cheeseNeighbourTileUp == cheeseRight) groundTilemap.SetTile(neighbourCells[0], cheeseRightBottom);
            if (cheeseNeighbourTileUp == cheeseTop) groundTilemap.SetTile(neighbourCells[0], cheeseTopBottom);
            if (cheeseNeighbourTileUp == cheeseLeftTop) groundTilemap.SetTile(neighbourCells[0], cheeseLeftTopBottom);
            if (cheeseNeighbourTileUp == cheeseRightTop) groundTilemap.SetTile(neighbourCells[0], cheeseRightTopBottom);
            if (cheeseNeighbourTileUp == cheeseTopLeftRight) groundTilemap.SetTile(neighbourCells[0], cheeseAll);
            if (cheeseNeighbourTileUp == cheeseLeftRight) groundTilemap.SetTile(neighbourCells[0], cheeseBottomLeftRight);

            if (fuellNeighbourTileUp == fuellcheeseMiddle) fuellTilemap.SetTile(neighbourCells[0], fuellcheeseBottom);
            if (fuellNeighbourTileUp == fuellcheeseLeft) fuellTilemap.SetTile(neighbourCells[0], fuellcheeseLeftBottom);
            if (fuellNeighbourTileUp == fuellcheeseRight) fuellTilemap.SetTile(neighbourCells[0], fuellcheeseRightBottom);
            if (fuellNeighbourTileUp == fuellcheeseTop) fuellTilemap.SetTile(neighbourCells[0], fuellcheeseTopBottom);
            if (fuellNeighbourTileUp == fuellcheeseLeftTop) fuellTilemap.SetTile(neighbourCells[0], fuellcheeseLeftTopBottom);
            if (fuellNeighbourTileUp == fuellcheeseRightTop) fuellTilemap.SetTile(neighbourCells[0], fuellcheeseRightTopBottom);
            if (fuellNeighbourTileUp == fuellcheeseTopLeftRight) fuellTilemap.SetTile(neighbourCells[0], fuellcheeseAll);
            if (fuellNeighbourTileUp == fuellcheeseLeftRight) fuellTilemap.SetTile(neighbourCells[0], fuellcheeseBottomLeftRight);

            if (resourceNeighbourTileUp == resourcecheeseMiddle) resourcesTilemap.SetTile(neighbourCells[0], resourcecheeseBottom);
            if (resourceNeighbourTileUp == resourcecheeseLeft) resourcesTilemap.SetTile(neighbourCells[0], resourcecheeseLeftBottom);
            if (resourceNeighbourTileUp == resourcecheeseRight) resourcesTilemap.SetTile(neighbourCells[0], resourcecheeseRightBottom);
            if (resourceNeighbourTileUp == resourcecheeseTop) resourcesTilemap.SetTile(neighbourCells[0], resourcecheeseTopBottom);
            if (resourceNeighbourTileUp == resourcecheeseLeftTop) resourcesTilemap.SetTile(neighbourCells[0], resourcecheeseLeftTopBottom);
            if (resourceNeighbourTileUp == resourcecheeseRightTop) resourcesTilemap.SetTile(neighbourCells[0], resourcecheeseRightTopBottom);
            if (resourceNeighbourTileUp == resourcecheeseTopLeftRight) resourcesTilemap.SetTile(neighbourCells[0], resourcecheeseAll);
            if (resourceNeighbourTileUp == resourcecheeseLeftRight) resourcesTilemap.SetTile(neighbourCells[0], resourcecheeseBottomLeftRight);
        }
        if (cheeseNeighbourTileDown != null || fuellNeighbourTileDown != null || resourceNeighbourTileDown != null)
        {
            if (cheeseNeighbourTileDown == cheeseMiddle) groundTilemap.SetTile(neighbourCells[1], cheeseTop);
            if (cheeseNeighbourTileDown == cheeseLeft) groundTilemap.SetTile(neighbourCells[1], cheeseLeftTop);
            if (cheeseNeighbourTileDown == cheeseRight) groundTilemap.SetTile(neighbourCells[1], cheeseRightTop);
            if (cheeseNeighbourTileDown == cheeseBottom) groundTilemap.SetTile(neighbourCells[1], cheeseTopBottom);
            if (cheeseNeighbourTileDown == cheeseLeftBottom) groundTilemap.SetTile(neighbourCells[1], cheeseLeftTopBottom);
            if (cheeseNeighbourTileDown == cheeseRightBottom) groundTilemap.SetTile(neighbourCells[1], cheeseRightTopBottom);
            if (cheeseNeighbourTileDown == cheeseBottomLeftRight) groundTilemap.SetTile(neighbourCells[1], cheeseAll);
            if (cheeseNeighbourTileDown == cheeseLeftRight) groundTilemap.SetTile(neighbourCells[1], cheeseTopLeftRight);

            if (fuellNeighbourTileDown == fuellcheeseMiddle) fuellTilemap.SetTile(neighbourCells[1], fuellcheeseTop);
            if (fuellNeighbourTileDown == fuellcheeseLeft) fuellTilemap.SetTile(neighbourCells[1], fuellcheeseLeftTop);
            if (fuellNeighbourTileDown == fuellcheeseRight) fuellTilemap.SetTile(neighbourCells[1], fuellcheeseRightTop);
            if (fuellNeighbourTileDown == fuellcheeseBottom) fuellTilemap.SetTile(neighbourCells[1], fuellcheeseTopBottom);
            if (fuellNeighbourTileDown == fuellcheeseLeftBottom) fuellTilemap.SetTile(neighbourCells[1], fuellcheeseLeftTopBottom);
            if (fuellNeighbourTileDown == fuellcheeseRightBottom) fuellTilemap.SetTile(neighbourCells[1], fuellcheeseRightTopBottom);
            if (fuellNeighbourTileDown == fuellcheeseBottomLeftRight) fuellTilemap.SetTile(neighbourCells[1], fuellcheeseAll);
            if (fuellNeighbourTileDown == fuellcheeseLeftRight) fuellTilemap.SetTile(neighbourCells[1], fuellcheeseTopLeftRight);

            if (resourceNeighbourTileDown == resourcecheeseMiddle) resourcesTilemap.SetTile(neighbourCells[1], resourcecheeseTop);
            if (resourceNeighbourTileDown == resourcecheeseLeft) resourcesTilemap.SetTile(neighbourCells[1], resourcecheeseLeftTop);
            if (resourceNeighbourTileDown == resourcecheeseRight) resourcesTilemap.SetTile(neighbourCells[1], resourcecheeseRightTop);
            if (resourceNeighbourTileDown == resourcecheeseBottom) resourcesTilemap.SetTile(neighbourCells[1], resourcecheeseTopBottom);
            if (resourceNeighbourTileDown == resourcecheeseLeftBottom) resourcesTilemap.SetTile(neighbourCells[1], resourcecheeseLeftTopBottom);
            if (resourceNeighbourTileDown == resourcecheeseRightBottom) resourcesTilemap.SetTile(neighbourCells[1], resourcecheeseRightTopBottom);
            if (resourceNeighbourTileDown == resourcecheeseBottomLeftRight) resourcesTilemap.SetTile(neighbourCells[1], resourcecheeseAll);
            if (resourceNeighbourTileDown == resourcecheeseLeftRight) resourcesTilemap.SetTile(neighbourCells[1], resourcecheeseTopLeftRight);
        }

        if (cheeseNeighbourTileLeft != null || fuellNeighbourTileLeft != null || resourceNeighbourTileLeft != null)
        {
            if (cheeseNeighbourTileLeft == cheeseMiddle) groundTilemap.SetTile(neighbourCells[2], cheeseRight);
            if (cheeseNeighbourTileLeft == cheeseTop) groundTilemap.SetTile(neighbourCells[2], cheeseRightTop);
            if (cheeseNeighbourTileLeft == cheeseBottom) groundTilemap.SetTile(neighbourCells[2], cheeseRightBottom);
            if (cheeseNeighbourTileLeft == cheeseLeft) groundTilemap.SetTile(neighbourCells[2], cheeseLeftRight);
            if (cheeseNeighbourTileLeft == cheeseLeftTop) groundTilemap.SetTile(neighbourCells[2], cheeseTopLeftRight);
            if (cheeseNeighbourTileLeft == cheeseLeftBottom) groundTilemap.SetTile(neighbourCells[2], cheeseBottomLeftRight);
            if (cheeseNeighbourTileLeft == cheeseLeftTopBottom) groundTilemap.SetTile(neighbourCells[2], cheeseAll);
            if (cheeseNeighbourTileLeft == cheeseTopBottom) groundTilemap.SetTile(neighbourCells[2], cheeseRightTopBottom);

            if (fuellNeighbourTileLeft == fuellcheeseMiddle) fuellTilemap.SetTile(neighbourCells[2], fuellcheeseRight);
            if (fuellNeighbourTileLeft == fuellcheeseTop) fuellTilemap.SetTile(neighbourCells[2], fuellcheeseRightTop);
            if (fuellNeighbourTileLeft == fuellcheeseBottom) fuellTilemap.SetTile(neighbourCells[2], fuellcheeseRightBottom);
            if (fuellNeighbourTileLeft == fuellcheeseLeft) fuellTilemap.SetTile(neighbourCells[2], fuellcheeseLeftRight);
            if (fuellNeighbourTileLeft == fuellcheeseLeftTop) fuellTilemap.SetTile(neighbourCells[2], fuellcheeseTopLeftRight);
            if (fuellNeighbourTileLeft == fuellcheeseLeftBottom) fuellTilemap.SetTile(neighbourCells[2], fuellcheeseBottomLeftRight);
            if (fuellNeighbourTileLeft == fuellcheeseLeftTopBottom) fuellTilemap.SetTile(neighbourCells[2], fuellcheeseAll);
            if (fuellNeighbourTileLeft == fuellcheeseTopBottom) fuellTilemap.SetTile(neighbourCells[2], fuellcheeseRightTopBottom);

            if (resourceNeighbourTileLeft == resourcecheeseMiddle) resourcesTilemap.SetTile(neighbourCells[2], resourcecheeseRight);
            if (resourceNeighbourTileLeft == resourcecheeseTop) resourcesTilemap.SetTile(neighbourCells[2], resourcecheeseRightTop);
            if (resourceNeighbourTileLeft == resourcecheeseBottom) resourcesTilemap.SetTile(neighbourCells[2], resourcecheeseRightBottom);
            if (resourceNeighbourTileLeft == resourcecheeseLeft) resourcesTilemap.SetTile(neighbourCells[2], resourcecheeseLeftRight);
            if (resourceNeighbourTileLeft == resourcecheeseLeftTop) resourcesTilemap.SetTile(neighbourCells[2], resourcecheeseTopLeftRight);
            if (resourceNeighbourTileLeft == resourcecheeseLeftBottom) resourcesTilemap.SetTile(neighbourCells[2], resourcecheeseBottomLeftRight);
            if (resourceNeighbourTileLeft == resourcecheeseLeftTopBottom) resourcesTilemap.SetTile(neighbourCells[2], resourcecheeseAll);
            if (resourceNeighbourTileLeft == resourcecheeseTopBottom) resourcesTilemap.SetTile(neighbourCells[2], resourcecheeseRightTopBottom);
        }

        if (cheeseNeighbourTileRight != null || fuellNeighbourTileRight != null || resourceNeighbourTileRight != null)
        {
            if (cheeseNeighbourTileRight == cheeseMiddle) groundTilemap.SetTile(neighbourCells[3], cheeseLeft);
            if (cheeseNeighbourTileRight == cheeseTop) groundTilemap.SetTile(neighbourCells[3], cheeseLeftTop);
            if (cheeseNeighbourTileRight == cheeseBottom) groundTilemap.SetTile(neighbourCells[3], cheeseLeftBottom);
            if (cheeseNeighbourTileRight == cheeseRight) groundTilemap.SetTile(neighbourCells[3], cheeseLeftRight);
            if (cheeseNeighbourTileRight == cheeseRightTop) groundTilemap.SetTile(neighbourCells[3], cheeseTopLeftRight);
            if (cheeseNeighbourTileRight == cheeseRightBottom) groundTilemap.SetTile(neighbourCells[3], cheeseBottomLeftRight);
            if (cheeseNeighbourTileRight == cheeseRightTopBottom) groundTilemap.SetTile(neighbourCells[3], cheeseAll);
            if (cheeseNeighbourTileRight == cheeseTopBottom) groundTilemap.SetTile(neighbourCells[3], cheeseLeftTopBottom);

            if (fuellNeighbourTileRight == fuellcheeseMiddle) fuellTilemap.SetTile(neighbourCells[3], fuellcheeseLeft);
            if (fuellNeighbourTileRight == fuellcheeseTop) fuellTilemap.SetTile(neighbourCells[3], fuellcheeseLeftTop);
            if (fuellNeighbourTileRight == fuellcheeseBottom) fuellTilemap.SetTile(neighbourCells[3], fuellcheeseLeftBottom);
            if (fuellNeighbourTileRight == fuellcheeseRight) fuellTilemap.SetTile(neighbourCells[3], fuellcheeseLeftRight);
            if (fuellNeighbourTileRight == fuellcheeseRightTop) fuellTilemap.SetTile(neighbourCells[3], fuellcheeseTopLeftRight);
            if (fuellNeighbourTileRight == fuellcheeseRightBottom) fuellTilemap.SetTile(neighbourCells[3], fuellcheeseBottomLeftRight);
            if (fuellNeighbourTileRight == fuellcheeseRightTopBottom) fuellTilemap.SetTile(neighbourCells[3], fuellcheeseAll);
            if (fuellNeighbourTileRight == fuellcheeseTopBottom) fuellTilemap.SetTile(neighbourCells[3], fuellcheeseLeftTopBottom);

            if (resourceNeighbourTileRight == resourcecheeseMiddle) resourcesTilemap.SetTile(neighbourCells[3], resourcecheeseLeft);
            if (resourceNeighbourTileRight == resourcecheeseTop) resourcesTilemap.SetTile(neighbourCells[3], resourcecheeseLeftTop);
            if (resourceNeighbourTileRight == resourcecheeseBottom) resourcesTilemap.SetTile(neighbourCells[3], resourcecheeseLeftBottom);
            if (resourceNeighbourTileRight == resourcecheeseRight) resourcesTilemap.SetTile(neighbourCells[3], resourcecheeseLeftRight);
            if (resourceNeighbourTileRight == resourcecheeseRightTop) resourcesTilemap.SetTile(neighbourCells[3], resourcecheeseTopLeftRight);
            if (resourceNeighbourTileRight == resourcecheeseRightBottom) resourcesTilemap.SetTile(neighbourCells[3], resourcecheeseBottomLeftRight);
            if (resourceNeighbourTileRight == resourcecheeseRightTopBottom) resourcesTilemap.SetTile(neighbourCells[3], resourcecheeseAll);
            if (resourceNeighbourTileRight == resourcecheeseTopBottom) resourcesTilemap.SetTile(neighbourCells[3], resourcecheeseLeftTopBottom);
        }
    }

}
