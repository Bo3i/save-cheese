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

    [SerializeField] private Tilemap tilemap;
    [SerializeField] private TileBase tile;
    [SerializeField] private TileBase dirtLeftTop;
    [SerializeField] private TileBase dirtRightTop;
    [SerializeField] private TileBase dirtLeft;
    [SerializeField] private TileBase dirtRight;
    [SerializeField] private TileBase dirtBottom;
    [SerializeField] private TileBase dirtTop;
    [SerializeField] private TileBase dirtLeftBottom;
    [SerializeField] private TileBase dirtRightBottom;
    [SerializeField] private TileBase dirtMiddle;

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
        Dig();
    }

    private TileBase retDugTile(Vector3Int cell)
    {
        return tilemap.GetTile(cell);
    }

    private void UpdateNeighbours(Vector3Int tile)
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
            if (neighbourTileUp == dirtMiddle) tilemap.SetTile(neighbourCells[0], dirtBottom);
            if (neighbourTileUp == dirtLeft) tilemap.SetTile(neighbourCells[0], dirtLeftBottom);
            if (neighbourTileUp == dirtRight) tilemap.SetTile(neighbourCells[0], dirtRightBottom);
        }
        if (neighbourTileDown != null)
        {
            if (neighbourTileDown == dirtMiddle) tilemap.SetTile(neighbourCells[1], dirtTop);
            if (neighbourTileDown == dirtLeft) tilemap.SetTile(neighbourCells[1], dirtLeftTop);
            if (neighbourTileDown == dirtRight) tilemap.SetTile(neighbourCells[1], dirtRightTop);
        }
        
        if (neighbourTileLeft != null)
        {
            if (neighbourTileLeft == dirtMiddle) tilemap.SetTile(neighbourCells[2], dirtRight);
            if (neighbourTileLeft == dirtTop) tilemap.SetTile(neighbourCells[2], dirtRightTop);
            if (neighbourTileLeft == dirtBottom) tilemap.SetTile(neighbourCells[2], dirtRightBottom);
        }

        if (neighbourTileRight != null)
        {
            if (neighbourTileRight == dirtMiddle) tilemap.SetTile(neighbourCells[3], dirtLeft);
            if (neighbourTileRight == dirtTop) tilemap.SetTile(neighbourCells[3], dirtLeftTop);
            if (neighbourTileRight == dirtBottom) tilemap.SetTile(neighbourCells[3], dirtLeftBottom);
        }    
    }


    private void Dig()
    {
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
                    itemCollector.UpdateInventory(retDugTile(cell));
                    int an = UpdateAimation(dir);
                    tilemap.SetTileFlags(cell, TileFlags.None);
                    StartCoroutine(Fade(cell, an));
                }
                
            }

        }
    }

    IEnumerator Fade(Vector3Int cell, int an)
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
        tilemap.SetTile(cell, null);
        UpdateNeighbours(cell);
        digging = false;
    }

    private int UpdateAimation(Vector2 dir)
    {
        int ret = 0;
        if (digging)
        {
            if (dir.x == 1)
            {
                ret = 5;
                //anim.SetInteger("state", 5);
                Debug.Log("Right");
            }
            else if (dir.x == -1)
            {
                ret = 5;
                //anim.SetInteger("state", 5);
                Debug.Log("Left");
            }
            else if (dir.y == 1)
            {
                ret = 7;
                //anim.SetInteger("state", 7);
                Debug.Log("Up");
            }
            else if (dir.y == -1)
            {
                ret = 8;
                //anim.SetInteger("state", 8);
                Debug.Log("Down");
            }

        }
        return ret;
        
    }   
}
