using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Data_Storage : MonoBehaviour
{
    public static Data_Storage Instance;

    public string Saved_Game;
    public string Saved_Chat;
    public string Map_Name;
    public string Map_Size;
    public int Game_ID;
    public int Game_Points;

    void Awake()
    {
        if (Instance == null)
        {
            DontDestroyOnLoad(gameObject);
            Instance = this;
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
        }
    }


    public void ClearAll()
    {
        Saved_Game = "";
        Saved_Chat = "";
        //Map_Name = "Images/Maps/" + Map_Size + "/SN-01b-Blank-16x24";
        Game_ID = -1;
    }
}
