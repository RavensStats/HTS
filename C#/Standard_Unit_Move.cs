using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Standard_Unit_Move : MonoBehaviour
{

    List<Tile_Script> selectableTiles = new List<Tile_Script>();
    GameObject[] tiles;

    Stack<Tile_Script> path = new Stack<Tile_Script>();
    Tile_Script currentTile;

    public int move = 40;
    public float jumpHeight = 1;
    public float moveSpeed = 2;
    public bool moving = false; 

    Vector3 velocity = new Vector3();
    Vector3 heading = new Vector3();

    float halfHeight = 0;

    protected void init()
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

        if(Physics.Raycast(target.transform.position, -Vector3.up, out hit, 1))
        {
            tile = hit.collider.GetComponent<Tile_Script>();
        }

        return tile;
    }

    public void ComputedAdjacencyList()
    {
        foreach(GameObject tile in tiles)
        {
            Tile_Script t = tile.GetComponent<Tile_Script>();
            t.FindNeighbors(jumpHeight);
        }
    }

    public void FindSelectableTiles()
    {
        ComputedAdjacencyList();
        GetCurrentTile();

        Queue<Tile_Script> process = new Queue<Tile_Script>();

        process.Enqueue(currentTile);
        currentTile.visited = true;

        while(process.Count > 0)
        {
            Tile_Script t = process.Dequeue();

            selectableTiles.Add(t);
            t.selectable = true;

            if(t.distance < move)
            {

                foreach(Tile_Script tile in t.adjacencyList)
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
        if (!moving)
        {
            FindSelectableTiles();
            CheckMouse();
        }
        else
        {
            //to do Move();
        }
    }

    void CheckMouse()
        {
            if (Input.GetMouseButtonUp(0))
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

                RaycastHit hit;
                if(Physics.Raycast(ray,out hit))
                {
                    if (hit.collider.tag == "Tile")
                    {
                        Tile_Script t = hit.collider.GetComponent<Tile_Script>();

                        if (t.selectable)
                        {
                            //todo move target
                            t.target = true;
                            moving = false;
                        }
                    }
                }
            }
        }


    public void MoveToTile(Tile_Script tile)
        {
            path.Clear();
            tile.target = true;
            moving = true;
            Tile_Script next = tile;
            while(next != null)
            {
                path.Push(next);
                next = next.parent;
            }
        }

    public void Move()
    {
        if(path.Count > 0)
        {
            Tile_Script t = path.Peek();
            Vector3 target = t.transform.position;

            //Calculate the unit's position on top of the target tile
            target.y += halfHeight + t.GetComponent<Collider>().bounds.extents.y;

            if (Vector3.Distance(transform.position, target) >= 0.05f)
            {
                CalculateHeading(target);
                SetHorizontalVelocity();

                transform.forward = heading;
                transform.position += velocity * Time.deltaTime;
            }
            else
            {
                //Tile center reached
                transform.position = target;
                path.Pop();
            }

        }
        else
        {
            RemoveSelectableTiles();
            moving = false;
        }
    }


    protected void RemoveSelectableTiles()
    {
        if(currentTile != null)
        {
            currentTile.current = false;
            currentTile = null;
        }

        foreach(Tile_Script tile in selectableTiles)
        {
            tile.Reset();
        }

        selectableTiles.Clear();
    }

    void CalculateHeading(Vector3 target)
    {
        heading = target - transform.position;
        heading.Normalize();
    }

    void SetHorizontalVelocity()
    {
        velocity = heading * moveSpeed;
    }

}
