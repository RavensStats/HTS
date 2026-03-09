using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile_Script : MonoBehaviour
{
    public bool indoor = false;

    public int elevated = 0;

    int unitMask = 1 << 8;

    public bool blocking = false;
    public bool hindering = false;
    public bool water = false;
    public bool clear = true;
    public bool starting = false;

    public bool occupied = false;
    
    public bool current = false;
    public bool target = false;
    public bool selectable = false;
    public bool walkable = true;

    public List<Tile_Script> adjacencyList = new List<Tile_Script>();

    //Needed for BFS(Breadth First Search)
    public bool visited = false;
    public Tile_Script parent = null;
    public int distance = 0;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (current)
        {
            GetComponent<Renderer>().material.color = Color.magenta;
        }
        else if (target)
        {
            GetComponent<Renderer>().material.color = Color.green;
        }
        else if (selectable)
        {
            GetComponent<Renderer>().material.color = Color.red;
        }
        else
        {
            GetComponent<Renderer>().material.color = Color.white;
        }
    }


    public void Reset()
    {
        adjacencyList.Clear();
        current = false;
        target = false;
        selectable = false;

        //Needed for BFS(Breadth First Search)
        visited = false;
        parent = null;
        distance = 0;

    }

    public void FindNeighbors(float jumpHeight)
    {
        Reset();

        CheckTile(Vector3.forward, jumpHeight);
        CheckTile(-Vector3.forward, jumpHeight);
        CheckTile(Vector3.right, jumpHeight);
        CheckTile(-Vector3.right, jumpHeight);
    }

    public void CheckTile(Vector3 direction, float jumpHeight)
    {
        Vector3 halfExtents = new Vector3(0.25f, (1 + jumpHeight) / 2.0f, 0.25f);
        Collider[] colliders = Physics.OverlapBox(transform.position + direction, halfExtents);

        foreach (Collider item in colliders)
        {
            if (item.CompareTag("Tile"))
            {
                Tile_Script tile = item.GetComponent<Tile_Script>();
                if (tile != null && tile.walkable == true)
                {
                    RaycastHit hit;
                    if (!Physics.Raycast(tile.transform.position, Vector3.up, out hit, 1, unitMask))
                    {
                        adjacencyList.Add(tile);
                    }
                }
            }
        }
    }

}
