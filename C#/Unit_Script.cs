using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using UnityEngine.EventSystems;

[Serializable]

public class Unit_Script : MonoBehaviour
{

    public string set = "";
    public string set_abbreviation = "";
    public string collectors_number = "";
    public string unit_Name = "";
    public string rarity = "";
    public string experience = "";
    public string team_ability = "";
    public string player_team = "";
    public string base_type = "Standard";
    public string traitName1 = "";
    public string traitName2 = "";
    public string traitName3 = "";
    public string traitName4 = "";
    public string traitName5 = "";
    public string traitName6 = "";
    public string trait1 = "";
    public string trait2 = "";
    public string trait3 = "";
    public string trait4 = "";
    public string trait5 = "";
    public string trait6 = "";

    public string[] keywords = { "", "" };

    public int points = 10;
    public int range = 6;
    public int range_targets = 1;
    public string speed_type = "Boot";
    public string attack_type = "Fist";
    public string defense_type = "Shield";
    public string damage_type = "Starburst";

    //Improved Targeting
    public bool IT_elevated = false;
    public bool IT_hindering = false;

    public bool IT_blocking = false;
    public bool IT_blocking_destroy = false;

    public bool IT_characters = false;
    public bool IT_adjacent_characters = false;

    //Improved Movement
    public bool IM_elevated = false;
    public bool IM_hindering = false;
    public bool IM_water = false;
    public bool IM_blocking = false;
    public bool IM_outdoor_blocking = false;
    public bool IM_blocking_destroy = false;
    public bool IM_characters = false;
    public bool IM_adjacent_characters = false;

    public bool startingForce = false;

    public int click_number = 1;
    public int dial_length = 12;
    public int last_click = 11;
    public int actionTokens = 0;
    public int team = 1;
    public int isDynamic = 1;

    public List<int> speed_values = new List<int>();
    public List<int> attack_values = new List<int>();
    public List<int> defense_values = new List<int>();
    public List<int> damage_values = new List<int>();

    public List<string> speed_powers = new List<string>();
    public List<string> attack_powers = new List<string>();
    public List<string> defense_powers = new List<string>();
    public List<string> damage_powers = new List<string>();

    public List<int> speed_powersID = new List<int>();
    public List<int> attack_powersID = new List<int>();
    public List<int> defense_powersID = new List<int>();
    public List<int> damage_powersID = new List<int>();

    public List<string> speed_powers_description = new List<string>();
    public List<string> attack_powers_description = new List<string>();
    public List<string> defense_powers_description = new List<string>();
    public List<string> damage_powers_description = new List<string>();

    public Sprite Sculpt_Image;
    public Material Team_1_Skin, Team_2_Skin;
    public GameObject UnitMenu;

    //Track Unit Stats Here? ( MVP, Number of Plays, KOs, etc.)


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

    Vector3 velocity = new Vector3();
    Vector3 heading = new Vector3();

    float halfHeight = 0;
    int tileMask = 1 << 10;

    //End Movement vars


    //Rotation Variables
    protected Vector3 dist;
    protected Vector3 startPos;
    protected float posX;
    protected float posZ;
    protected float posY;
    public float clickDelta = 0.35f;
    protected bool click = false;
    protected float clickTime;
    //End rotation variables

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

        //if (!Physics.Raycast(target.transform.position, -Vector3.up, out hit, 1))
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

        switch (team)
        {
            case 1:
                gameObject.GetComponent<Renderer>().material.color = Color.green;
                break;

            case 2:
                gameObject.GetComponent<Renderer>().material.color = Color.red;
                break;

            case 3:
                gameObject.GetComponent<Renderer>().material.color = Color.blue;
                break;

            case 4:
                gameObject.GetComponent<Renderer>().material.color = Color.yellow;
                break;

            case 5:
                gameObject.GetComponent<Renderer>().material.color = Color.cyan;
                break;

            case 6:
                gameObject.GetComponent<Renderer>().material.color = Color.black;
                break;

            case 7:
                gameObject.GetComponent<Renderer>().material.color = Color.white;
                break;

            case 8:
                gameObject.GetComponent<Renderer>().material.color = Color.grey;
                break;

            case 9:
                gameObject.GetComponent<Renderer>().material.color = new Color32(50,5,3,1);
                break;

            case 10:
                gameObject.GetComponent<Renderer>().material.color = Color.magenta;
                break;

        }

        

        if (!moving && selected)
        {
            FindSelectableTiles();
            CheckMouse();
        }
        else if (moving && selected)
        {
            Move();
            UnitMenu = GameObject.FindGameObjectWithTag("UnitMenu");
        }
        RemoveSelectableTiles();
        CheckMouse();

        if(UnitMenu != null)
        {
            UnitMenu.SetActive(false);
        }


    }

    void CheckMouse()
    {
        if (IsPointerOverUI())    // is the touch on the GUI
        {
            // GUI Action
            return;
        }
        else
        if (Input.GetMouseButtonUp(0))
        {

            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {

                if (hit.collider.tag == "Tile")
                {
                    Tile_Script t = hit.collider.GetComponent<Tile_Script>();

                    if (t.selectable)
                    {
                        MoveToTile(t);
                    }
                }

                //if (hit.collider.tag == "Unit")
                //{
                //    Unit_Script u = hit.collider.GetComponent<Unit_Script>();

                //    //u.selected = true;

                //}
                
            }
        }
    }


    public void MoveToTile(Tile_Script tile)
    {
        path.Clear();

        tile.target = true;
        moving = true;

        Tile_Script next = tile;

        while (next != null)
        {
            path.Push(next);
            next = next.parent;
        }
    }

    public void Move()
    {
        if (path.Count > 0)
        {
            Tile_Script t = path.Peek();
            Vector3 target = t.transform.position;

            //Calculate the unit's position on top of the target tile
            target.y += halfHeight + t.GetComponent<Collider>().bounds.extents.y;

            if (Vector3.Distance(transform.position, target) >= 0.2f)
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
            selected = false;
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

    void CalculateHeading(Vector3 target)
    {
        heading = target - transform.position;
        heading.Normalize();
    }

    void SetHorizontalVelocity()
    {
        velocity = heading * moveSpeed;
    }

    void OnMouseDown()
    {
        startPos = transform.position;
        dist = Camera.main.WorldToScreenPoint(transform.position);
        posX = Input.mousePosition.x - dist.x;
        posY = Input.mousePosition.y - dist.y;
        posZ = Input.mousePosition.z - dist.z;

        if (click && Time.time <= (clickTime + clickDelta))
        {
            transform.Rotate(new Vector3(0, 90, 0)); // Double click
            click = false;
        }
        else
        {
            click = true;
            clickTime = Time.time;
        }

    }

    private bool IsPointerOverUI()
    {
        if (Input.touchCount > 0)
            return EventSystem.current.IsPointerOverGameObject(Input.GetTouch(0).fingerId);
        return EventSystem.current.IsPointerOverGameObject();
    }

}
