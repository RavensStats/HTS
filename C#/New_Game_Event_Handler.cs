using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine.Networking;

public class New_Game_Event_Handler : MonoBehaviour
{
    Sprite selectedMap;
    public Image previewedMap;

    List<Sprite> maps16x24 = new List<Sprite>();
    public List<string> mapSizes = new List<string>();
    public List<string> mapnames16x24 = new List<string>();

    public Dropdown map_Dropdown, player_Dropdown, NumberOfPlayers;
    public List<string> DropdownUsersList = new List<string>();
    public InputField NumberOfPoints;
    string Selected_Player = "", MapSize = "", MapName = "";

    int NumberOfPlayersIndex, CurrPlayers;

    private void Start()
    {
        populateMapsDropdown16x24();
        mapSizes.Add("Choose A Map");
        mapSizes.Add("16x24");
        CallGetUsers();
    }

    public void ClearScreen()
    {
        map_Dropdown.value = 0;
        NumberOfPlayers.value = 0;
        player_Dropdown.value = 0;
        NumberOfPoints.text = "";

    }

    public void CallCreateNewGame()
    {
        StartCoroutine(CreateNewGame());
    }

    public void Player_Index_Changed(int index)
    {
        Selected_Player = DropdownUsersList[index];
    }

    public void MapSize_Index_Changed(int index)
    {
        MapSize = mapSizes[index];
    }

    IEnumerator CreateNewGame()
    {
        string storedUsername = PlayerPrefs.GetString("Active_Username");
        if (string.IsNullOrEmpty(storedUsername))
            storedUsername = PlayerPrefs.GetString("Stored_Username");
        if (string.IsNullOrEmpty(storedUsername))
        {
            Debug.LogError("[CreateNewGame] Active_Username is empty — cannot create game.");
            yield break;
        }

        if (string.IsNullOrEmpty(MapName))
        {
            Debug.LogWarning("[CreateNewGame] No map selected — cannot create game.");
            yield break;
        }

        string playersField;
        if (!string.IsNullOrEmpty(Selected_Player))
        {
            CurrPlayers = 2;
            playersField = storedUsername + "," + Selected_Player;
        }
        else
        {
            CurrPlayers = 1;
            playersField = storedUsername;
        }

        Debug.Log("[CreateNewGame] Stored_Username='" + storedUsername + "' | Selected_Player='" + Selected_Player + "' | Players field='" + playersField + "'");

        WWWForm form = new WWWForm();
        form.AddField("Number_Of_Players", NumberOfPlayers.value + 1);
        form.AddField("Players", playersField);
        form.AddField("Current_Players", CurrPlayers);
        form.AddField("Status", "Active");
        form.AddField("PointsPerTeam", NumberOfPoints.text);
        form.AddField("MapSize", MapSize);
        form.AddField("MapName", MapName);

        using (UnityWebRequest www = UnityWebRequest.Post("https://www.stark44.com/Heroclix/createNewGame.php", form))
        {
            www.certificateHandler = new BypassCertificate();
            yield return www.SendWebRequest();

            if (www.result != UnityWebRequest.Result.Success)
            {
                Debug.LogError("[CreateNewGame] Request failed: " + www.error);
            }
            else
            {
                string response = www.downloadHandler.text;
                Debug.Log("[CreateNewGame] Server response: '" + response + "'");
                if (response.Trim() == "0")
                {
                    Debug.Log("[CreateNewGame] New game created successfully!");
                }
                else
                {
                    Debug.LogError("[CreateNewGame] Game creation failed: " + response);
                }
            }
        }

        ClearScreen();
    }

    public void CallGetUsers()
    {
        StartCoroutine(GetUsers());
    }

    IEnumerator GetUsers()
    {
        DropdownUsersList.Add("");

        using (UnityWebRequest www = UnityWebRequest.Get("https://www.stark44.com/Heroclix/getUsers.php"))
        {
            www.certificateHandler = new BypassCertificate();
            yield return www.SendWebRequest();

            if (www.result != UnityWebRequest.Result.Success)
            {
                Debug.Log(www.error);
            }
            else
            {
                //byte[] results = www.downloadHandler.data;
                string DBUsers = www.downloadHandler.text;

                while (DBUsers.Contains(",") && DBUsers.Length > 1)
                {
                    Debug.Log(DBUsers);
                    DropdownUsersList.Add(DBUsers.Substring(0,DBUsers.IndexOf(",")));
                    DBUsers = DBUsers.Substring(DBUsers.IndexOf(",")+1, DBUsers.Length - DBUsers.IndexOf(",")-1);
                }

                player_Dropdown.AddOptions(DropdownUsersList);

            }
        }

    }

    public void populateMapsDropdown16x24()
    {
        var tempMaps = Resources.LoadAll("Images/Maps/16x24", typeof(Sprite));

        foreach(Sprite map in tempMaps)
        {
            maps16x24.Add(map);
        }

        foreach (Sprite map in maps16x24)
        {
            mapnames16x24.Add(map.name);
        }
        map_Dropdown.AddOptions(mapnames16x24);
    }

    public void mapIndexChanged(int index)
    {
        switch (MapSize)
        {
            case "16x24":
                MapName = mapnames16x24[index];
                selectedMap = maps16x24[index];
                break;

        }
        
        previewMap();
    }

    public void NumberOfPlayersIndexChanged(int index)
    {
        NumberOfPlayersIndex = index;
    }

    public void previewMap()
    {
        previewedMap.GetComponent<Image>().sprite = selectedMap;
    }

    
}
