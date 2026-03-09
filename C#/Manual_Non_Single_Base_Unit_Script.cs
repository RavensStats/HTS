using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using UnityEngine.EventSystems;

[RequireComponent(typeof(MeshCollider))]
[Serializable]

public class Manual_Non_Single_Base_Unit_Script : Unit_Script
{

    //public string set = "";
    //public string collectors_number = "";
    //public string unit_Name = "";
    //public string rarity = "";
    //public string experience = "";
    //public string team_ability = "";
    //public string player_team = "";
    //public string base_type = "Standard";
    //public string traitName1 = "";
    //public string traitName2 = "";
    //public string traitName3 = "";
    //public string traitName4 = "";
    //public string traitName5 = "";
    //public string traitName6 = "";
    //public string trait1 = "";
    //public string trait2 = "";
    //public string trait3 = "";
    //public string trait4 = "";
    //public string trait5 = "";
    //public string trait6 = "";

    //public string[] keywords = { "", "" };

    //public int points = 10;
    //public int range = 6;
    //public int range_targets = 1;
    //public string speed_type = "Boot";
    //public string attack_type = "Fist";
    //public string defense_type = "Shield";
    //public string damage_type = "Starburst";

    ////Improved Targeting
    //public bool IT_elevated = false;
    //public bool IT_hindering = false;

    //public bool IT_blocking = false;
    //public bool IT_blocking_destroy = false;

    //public bool IT_characters = false;
    //public bool IT_adjacent_characters = false;

    ////Improved Movement
    //public bool IM_elevated = false;
    //public bool IM_hindering = false;
    //public bool IM_water = false;
    //public bool IM_blocking = false;
    //public bool IM_outdoor_blocking = false;
    //public bool IM_blocking_destroy = false;
    //public bool IM_characters = false;
    //public bool IM_adjacent_characters = false;

    //public bool startingForce = false;

    //public int click_number = 1;
    //public int dial_length = 26;
    //public int last_click = 25;
    //public int actionTokens = 0;
    //public int team = 1;
    //public int isDynamic = 1;

    //public List<int> speed_values = new List<int>();
    //public List<int> attack_values = new List<int>();
    //public List<int> defense_values = new List<int>();
    //public List<int> damage_values = new List<int>();

    //public List<string> speed_powers = new List<string>();
    //public List<string> attack_powers = new List<string>();
    //public List<string> defense_powers = new List<string>();
    //public List<string> damage_powers = new List<string>();

    //public List<int> speed_powersID = new List<int>();
    //public List<int> attack_powersID = new List<int>();
    //public List<int> defense_powersID = new List<int>();
    //public List<int> damage_powersID = new List<int>();

    //public List<string> speed_powers_description = new List<string>();
    //public List<string> attack_powers_description = new List<string>();
    //public List<string> defense_powers_description = new List<string>();
    //public List<string> damage_powers_description = new List<string>();

    //public Sprite Sculpt_Image;
    //public Material Team_1_Skin, Team_2_Skin;
    //public GameObject UnitMenu;

    //Rotation Code
    //Vector3 dist;
    //Vector3 startPos;
    //float posX;
    //float posZ;
    //float posY;

    ////public float clickDelta = 0.35f;
    //private bool click = false;
    //private float clickTime;
    //End Rotation Code

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
                gameObject.GetComponent<Renderer>().material.color = new Color32(50, 5, 3, 1);
                break;

            case 10:
                gameObject.GetComponent<Renderer>().material.color = Color.magenta;
                break;

        }
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
            transform.Rotate(new Vector3(0, 45, 0)); // Double click
            click = false;
        }
        else
        {
            click = true;
            clickTime = Time.time;
        }

    }


    void OnMouseDrag()
    {
        float disX = Input.mousePosition.x - posX;
        float disY = Input.mousePosition.y - posY;
        float disZ = Input.mousePosition.z - posZ;
        Vector3 lastPos = Camera.main.ScreenToWorldPoint(new Vector3(disX, disY, disZ));
        transform.position = new Vector3(lastPos.x, startPos.y, lastPos.z);
    }

}
