using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Object_Script : MonoBehaviour
{
    public string objectName;

    //////////////////////////////////
    ///Movement Code Below////////////
    //////////////////////////////////

    List<Tile_Script> selectableTiles = new List<Tile_Script>();
    GameObject[] tiles;

    Stack<Tile_Script> path = new Stack<Tile_Script>();
    Tile_Script currentTile;

    public int move = 25;
    public float jumpHeight = 1;
    public float moveSpeed = 4;
    public bool moving = false;
    public bool selected = false;

    float halfHeight = 0;
    int tileMask = 1 << 10;

    public GameObject ObjectMenu;

    private void Start()
    {
        Init();
    }

    protected void Init()
    {
        tiles = GameObject.FindGameObjectsWithTag("Tile");
        halfHeight = GetComponent<Collider>().bounds.extents.y;
    }


    public void GetCurrentTile()
    {
        currentTile = GetTargetTile(gameObject);
        currentTile.current = true;
    }

    public Tile_Script GetTargetTile(GameObject target)
    {
        RaycastHit hit;
        Tile_Script tile = null;

        if (Physics.Raycast(target.transform.position, -Vector3.up, out hit, 1, tileMask))
        {
            tile = hit.collider.GetComponent<Tile_Script>();
        }

        return tile;
    }

    public void ComputeAdjacencyLists()
    {
        foreach (GameObject tile in tiles)
        {
            Tile_Script t = tile.GetComponent<Tile_Script>();
            t.FindNeighbors(jumpHeight);
        }
    }

    public void FindSelectableTiles()
    {
        ComputeAdjacencyLists();
        GetCurrentTile();

        Queue<Tile_Script> process = new Queue<Tile_Script>();

        process.Enqueue(currentTile);
        currentTile.visited = true;

        while (process.Count > 0)
        {
            Tile_Script t = process.Dequeue();

            selectableTiles.Add(t);
            t.selectable = true;

            if (t.distance < move)
            {

                foreach (Tile_Script tile in t.adjacencyList)
                {
                    if (!tile.visited)
                    {
                        tile.parent = t;
                        tile.visited = true;
                        tile.distance = 1 + t.distance;
                        process.Enqueue(tile);
                    }
                }
            }
        }
    }

    private void Update()
    {
        //RemoveSelectableTiles();
        if (!moving && selected)
        {
            FindSelectableTiles();
            CheckMouse();
        }
        RemoveSelectableTiles();
        CheckMouse();

    }

    void CheckMouse()
    {
        if (Input.GetMouseButtonUp(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                if (hit.collider.tag == "Tile" && selected)
                {
                    Tile_Script t = hit.collider.GetComponent<Tile_Script>();

                    if (t.selectable)
                    {
                        Move(t);
                    }
                    selected = false;
                    moving = false;
                    RemoveSelectableTiles();
                }

                //if (hit.collider.tag == "Object")
                //{
                //    Object_Script o = hit.collider.GetComponent<Object_Script>();

                //    o.selected = true;

                //}

            }
        }
    }


   
    public void Move(Tile_Script t)
    {
        Vector3 target = t.transform.position;

        //Calculate the unit's position on top of the target tile
//        target.y += halfHeight + t.GetComponent<Collider>().bounds.extents.y;
        target.y += .01f + t.GetComponent<Collider>().bounds.extents.y;

        //Tile center reached
        transform.position = target;

        selected = false;

        ObjectMenu = GameObject.FindGameObjectWithTag("ObjectMenu");

        if (ObjectMenu != null)
        {
            ObjectMenu.SetActive(false);
        }

    }


    protected void RemoveSelectableTiles()
    {
        if (currentTile != null)
        {
            currentTile.current = false;
            currentTile = null;
        }

        foreach (Tile_Script tile in selectableTiles)
        {
            tile.Reset();
        }

        selectableTiles.Clear();
    }

}
