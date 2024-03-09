using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UIElements;

public class PlayerDigging : MonoBehaviour
{
    private Animator anim;
    private ItemCollector itemCollector;
    //private Rigidbody2D rb;
    //private Transform player;

    [SerializeField] private Tilemap groundTilemap;
    [SerializeField] private Tilemap resourcesTilemap;
    [SerializeField] private Tilemap fuellTilemap;

    [SerializeField] private TileBase tile;
    [SerializeField] private TileBase cheeseLeftTop;
    [SerializeField] private TileBase cheeseRightTop;
    [SerializeField] private TileBase cheeseLeft;
    [SerializeField] private TileBase cheeseRight;
    [SerializeField] private TileBase cheeseBottom;
    [SerializeField] private TileBase cheeseTop;
    [SerializeField] private TileBase cheeseLeftBottom;
    [SerializeField] private TileBase cheeseRightBottom;
    [SerializeField] private TileBase cheeseMiddle;

    [SerializeField] private Object FuellItem;
    [SerializeField] private Object ResourceItem;

    private Tilemap tilemap;
    private bool digging;

    private enum Neighbour { up, down, left, right }
    private enum TileType
    {
        dirtLeftTop, dirtRightTop, dirtLeft, dirtRight, dirtBottom, dirtTop, dirtLeftBottom, dirtRightBottom, dirtMiddle
    }
    
    private void Start()
    {
        anim = GetComponent<Animator>();
        itemCollector = GetComponent<ItemCollector>();
        //rb = GetComponent<Rigidbody2D>();
        //player = GetComponent<Transform>();
        digging = false;
    }

    // Update is called once per frame
    private void Update()
    {
        SetTilemap();
        Dig();
    }

    private void SetTilemap()
    {
        if (groundTilemap.GetTile(groundTilemap.WorldToCell(new Vector3(transform.position.x + Input.GetAxisRaw("Horizontal"), transform.position.y + Input.GetAxisRaw("Vertical"),0))) != null)
        {
            tilemap = groundTilemap;
        }
        else if (resourcesTilemap.GetTile(resourcesTilemap.WorldToCell(new Vector3(transform.position.x + Input.GetAxisRaw("Horizontal"), transform.position.y + Input.GetAxisRaw("Vertical"),0))) != null)
        {
            tilemap = resourcesTilemap;
        }
        else if (fuellTilemap.GetTile(fuellTilemap.WorldToCell(new Vector3(transform.position.x + Input.GetAxisRaw("Horizontal"), transform.position.y + Input.GetAxisRaw("Vertical"), 0))) != null)
        {
            tilemap = fuellTilemap;
        }
        else
        {
            tilemap = null;
        }
    } 

    private TileBase retDugTile(Vector3Int cell)
    {
        return tilemap.GetTile(cell);
    }

    private void UpdateNeighbours(Vector3Int tile, Tilemap tilemap)
    {
        //Debug.Log("Neighbours updated");
         
        Vector3Int[] neighbourCells = new Vector3Int[4];
        neighbourCells[0] = new Vector3Int(tile.x, tile.y + 1, tile.z);
        neighbourCells[1] = new Vector3Int(tile.x, tile.y - 1, tile.z);
        neighbourCells[2] = new Vector3Int(tile.x - 1, tile.y, tile.z);
        neighbourCells[3] = new Vector3Int(tile.x + 1, tile.y, tile.z);    
        
        

        TileBase neighbourTileUp = tilemap.GetTile(neighbourCells[0]);
        TileBase neighbourTileDown = tilemap.GetTile(neighbourCells[1]);
        TileBase neighbourTileLeft = tilemap.GetTile(neighbourCells[2]);
        TileBase neighbourTileRight = tilemap.GetTile(neighbourCells[3]);

        if (neighbourTileUp != null)
        {
            if (neighbourTileUp == cheeseMiddle) tilemap.SetTile(neighbourCells[0], cheeseBottom);
            if (neighbourTileUp == cheeseLeft) tilemap.SetTile(neighbourCells[0], cheeseLeftBottom);
            if (neighbourTileUp == cheeseRight) tilemap.SetTile(neighbourCells[0], cheeseRightBottom);
        }
        if (neighbourTileDown != null)
        {
            if (neighbourTileDown == cheeseMiddle) tilemap.SetTile(neighbourCells[1], cheeseTop);
            if (neighbourTileDown == cheeseLeft) tilemap.SetTile(neighbourCells[1], cheeseLeftTop);
            if (neighbourTileDown == cheeseRight) tilemap.SetTile(neighbourCells[1], cheeseRightTop);
        }
        
        if (neighbourTileLeft != null)
        {
            if (neighbourTileLeft == cheeseMiddle) tilemap.SetTile(neighbourCells[2], cheeseRight);
            if (neighbourTileLeft == cheeseTop) tilemap.SetTile(neighbourCells[2], cheeseRightTop);
            if (neighbourTileLeft == cheeseBottom) tilemap.SetTile(neighbourCells[2], cheeseRightBottom);
        }

        if (neighbourTileRight != null)
        {
            if (neighbourTileRight == cheeseMiddle) tilemap.SetTile(neighbourCells[3], cheeseLeft);
            if (neighbourTileRight == cheeseTop) tilemap.SetTile(neighbourCells[3], cheeseLeftTop);
            if (neighbourTileRight == cheeseBottom) tilemap.SetTile(neighbourCells[3], cheeseLeftBottom);
        }    
    }


    private void Dig()
    {
        if (tilemap == null) return;
        Vector2 dir = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        Vector3Int cell = tilemap.WorldToCell(transform.position);
        if (Input.GetKey(KeyCode.E) && dir != new Vector2(0, 0) && !digging)
        {
            
            if ((dir.x != 0 && dir.y == 0) || (dir.x == 0 && dir.y != 0) )
            {
                
                cell.x += (int)dir.x;
                cell.y += (int)dir.y;
                if (retDugTile(cell) != null)
                {
                    digging = true;
                    //itemCollector.UpdateInventory(retDugTile(cell));
                    int an = UpdateAimation(dir);
                    tilemap.SetTileFlags(cell, TileFlags.None);
                    StartCoroutine(Fade(cell, an, tilemap));
                }
                
            }

        }
    }

    IEnumerator Fade(Vector3Int cell, int an, Tilemap tilemap)
    {
        Color c = tilemap.GetColor(cell);
        for (float alpha = 1f; alpha >= 0; alpha -= 0.015f)
        {
            anim.SetInteger("state", an);
            //Debug.Log("Fading");
            c.a = alpha;
            tilemap.SetColor(cell,c);
            yield return null;
        }
        anim.SetInteger("state", 0);
        CreateItem(cell);
        tilemap.SetTile(cell, null);
        UpdateNeighbours(cell, tilemap);
        digging = false;
        
    }

    private void CreateItem(Vector3Int cell)
    {
        if (tilemap == null) return;
        if (tilemap.GetTile(cell) != null)
        {
            if (tilemap.tag == "FuellTilemap")
            {
                Instantiate(FuellItem, tilemap.GetCellCenterWorld(cell), Quaternion.identity);
            }
            else if (tilemap.tag == "ResourceTilemap")
            {
                Instantiate(ResourceItem, tilemap.GetCellCenterWorld(cell), Quaternion.identity);
            }
        }
    }

    private int UpdateAimation(Vector2 dir)
    {
        int ret = 0;
        if (digging)
        {
            if (dir.x == 1)
            {
                ret = 5;
            }
            else if (dir.x == -1)
            {
                ret = 5;
            }
            else if (dir.y == 1)
            {
                ret = 7;
            }
            else if (dir.y == -1)
            {
                ret = 8;
            }
        }
        return ret;
    }   
}
