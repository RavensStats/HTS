using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Load_Game_Event_Handler : MonoBehaviour
{
    public List<string> CurrentGames = new List<string>();
    public List<string> OpenGames = new List<string>();
    public List<string> CompletedGames = new List<string>();

    string SelectedGameToEnd, SelectedGameToJoin, SelectedGameToLoad, SelectedGameToLoadCompleted;

    public Dropdown CurrentGames_Dropdown, OpenGames_Dropdown, EndGames_Dropdown, CompletedGames_Dropdown;

    public bool LoadMenuHasLoaded;

    public int frame = 0;

    public Toggle YourTurnOnlyToggle;

    // Active_Username is set on every fresh login; fall back to Stored_Username
    // for sessions that pre-date the Active_Username key.
    string CurrentUsername
    {
        get
        {
            string a = PlayerPrefs.GetString("Active_Username");
            return !string.IsNullOrEmpty(a) ? a : PlayerPrefs.GetString("Stored_Username");
        }
    }


    // Start is called before the first frame update
    void Start()
    {
        CurrentGames.Clear();
        OpenGames.Clear();
        CompletedGames.Clear();

        CurrentGames_Dropdown.ClearOptions();
        OpenGames_Dropdown.ClearOptions();
        EndGames_Dropdown.ClearOptions();
        CompletedGames_Dropdown.ClearOptions();

        CallGetCurrentGames();
        CallGetOpenGames();
        CallGetCompletedGames();

        LoadMenuHasLoaded = true;
    }

    public void Update()
    {
        if (frame <= 10)
        {
            frame++;
        }
    }

    public void CallGetCurrentGames()
    {
        StartCoroutine(GetCurrentGames());
    }

    IEnumerator GetCurrentGames()
    {
        CurrentGames.Clear();
        CurrentGames_Dropdown.ClearOptions();
        EndGames_Dropdown.ClearOptions();

        WWWForm form = new WWWForm();
        form.AddField("Players", CurrentUsername);

        string IsYourTurn = "No";

        if (YourTurnOnlyToggle.isOn == true)
        {
            IsYourTurn = "Yes";
        }

        form.AddField("Your_Turn", IsYourTurn);

        using (UnityWebRequest www = UnityWebRequest.Post("https://www.stark44.com/Heroclix/getCurrentGames.php",form))
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
                string DBGames = www.downloadHandler.text;

                while (DBGames.Contains(";") && DBGames.Length > 1)
                {
                   
                    CurrentGames.Add(DBGames.Substring(0, DBGames.IndexOf(";")));
                    DBGames = DBGames.Substring(DBGames.IndexOf(";") + 1, DBGames.Length - DBGames.IndexOf(";") - 1);
                }

                CurrentGames_Dropdown.AddOptions(CurrentGames);
                EndGames_Dropdown.AddOptions(CurrentGames);

                if (CurrentGames.Count > 0)
                {
                    LoadGame_Dropdown_IndexChanged(0);
                    EndGame_Dropdown_IndexChanged(0);
                }



            }
        }

    }

    public void CallGetOpenGames()
    {
        StartCoroutine(GetOpenGames());
    }

    IEnumerator GetOpenGames()
    {
        OpenGames.Clear();
        OpenGames_Dropdown.ClearOptions();

        string username = CurrentUsername;
        Debug.Log("[GetOpenGames] CurrentUsername='" + username + "'");

        WWWForm form = new WWWForm();
        form.AddField("Players", username);

        using (UnityWebRequest www = UnityWebRequest.Post("https://www.stark44.com/Heroclix/getOpenGames.php", form))
        {
            www.certificateHandler = new BypassCertificate();
            yield return www.SendWebRequest();

            if (www.result != UnityWebRequest.Result.Success)
            {
                Debug.LogError("[GetOpenGames] Request failed: " + www.error);
            }
            else
            {
                string DBGames = www.downloadHandler.text;
                Debug.Log("[GetOpenGames] Server response: '" + DBGames + "'");

                while (DBGames.Contains(";") && DBGames.Length > 1)
                {
                    OpenGames.Add(DBGames.Substring(0, DBGames.IndexOf(";")));
                    DBGames = DBGames.Substring(DBGames.IndexOf(";") + 1, DBGames.Length - DBGames.IndexOf(";") - 1);
                }

                OpenGames_Dropdown.AddOptions(OpenGames);
                Debug.Log("[GetOpenGames] Populated " + OpenGames.Count + " open game(s).");

                if (OpenGames.Count > 0)
                {
                    JoinGame_Dropdown_IndexChanged(0);
                }
            }
        }

    }


    public void CallGetCompletedGames()
    {
        StartCoroutine(GetCompletedGames());
    }

    IEnumerator GetCompletedGames()
    {
        WWWForm form = new WWWForm();
        form.AddField("Players", CurrentUsername);

        using (UnityWebRequest www = UnityWebRequest.Post("https://www.stark44.com/Heroclix/getCompletedGames.php", form))
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
                string DBGames = www.downloadHandler.text;

                while (DBGames.Contains(";") && DBGames.Length > 1)
                {
                    CompletedGames.Add(DBGames.Substring(0, DBGames.IndexOf(";")));
                    DBGames = DBGames.Substring(DBGames.IndexOf(";") + 1, DBGames.Length - DBGames.IndexOf(";") - 1);
                }

                CompletedGames_Dropdown.AddOptions(CompletedGames);

                if (CompletedGames.Count > 0)
                {
                    LoadCompletedGame_Dropdown_IndexChanged(0);
                }

            }
        }

    }

    public void LoadGame_Dropdown_IndexChanged(int index)
    {
        SelectedGameToLoad = CurrentGames[index];
        SelectedGameToLoad = SelectedGameToLoad.Substring(0, SelectedGameToLoad.IndexOf("\t"));
    }

    public void CallLoadGame()
    {
        StartCoroutine(LoadGameSequence());
    }

    IEnumerator LoadGameSequence()
    {
        yield return StartCoroutine(LoadChat());
        yield return StartCoroutine(GetMapName());
        yield return StartCoroutine(LoadGame());
    }


    IEnumerator LoadGame()
    {
        WWWForm form = new WWWForm();
        form.AddField("Game_ID", int.Parse(SelectedGameToLoad));

        using (UnityWebRequest www = UnityWebRequest.Post("https://www.stark44.com/Heroclix/LoadGame.php", form))
        {
            www.certificateHandler = new BypassCertificate();
            yield return www.SendWebRequest();

            if (www.result != UnityWebRequest.Result.Success)
            {
                Debug.Log(www.error);
            }
            else
            {
                string LoadGameText = www.downloadHandler.text;
                Debug.Log("[LoadGameHandler] Raw response length=" + LoadGameText.Length +
                          " | preview: " + (LoadGameText.Length > 150 ? LoadGameText.Substring(0, 150) + "..." : LoadGameText));

                var NextTab = LoadGameText.IndexOf(",");
                string PointsPerTeam = LoadGameText.Substring(0, NextTab);
                LoadGameText = LoadGameText.Substring(NextTab + 1, LoadGameText.Length - NextTab - 1);
                NextTab = LoadGameText.IndexOf(";");

                LoadGameText = LoadGameText.Substring(0, NextTab);
                Debug.Log("[LoadGameHandler] Parsed Saved_Game length=" + LoadGameText.Length +
                          " | preview: " + (LoadGameText.Length > 150 ? LoadGameText.Substring(0, 150) + "..." : LoadGameText));
                if (LoadGameText.Length > 1)
                { 
                    Data_Storage.Instance.Saved_Game = LoadGameText;
                }

                Data_Storage.Instance.Game_ID = int.Parse(SelectedGameToLoad);
                Data_Storage.Instance.Game_Points = int.Parse(PointsPerTeam);

                SceneManager.LoadScene("Default_Map");
            }
        }
        Debug.Log("Game Loaded.");
        yield return null;
    }

    IEnumerator LoadChat()
    {
        WWWForm form = new WWWForm();
        form.AddField("Game_ID", int.Parse(SelectedGameToLoad));

        using (UnityWebRequest www = UnityWebRequest.Post("https://www.stark44.com/Heroclix/LoadChat.php", form))
        {
            www.certificateHandler = new BypassCertificate();
            yield return www.SendWebRequest();

            if (www.result != UnityWebRequest.Result.Success)
            {
                Debug.Log(www.error);
            }
            else
            {
                string LoadGameText = www.downloadHandler.text;

                if (LoadGameText == "")
                {
                    string temp = "Game " + Data_Storage.Instance.Game_ID + " Created.";
                    LoadGameText = "{ \"ChatLog\":[\"" + temp + "\"] }";
                }

                Data_Storage.Instance.Saved_Chat = LoadGameText;
            }
        }
        Debug.Log("Chat Loaded.");
        yield return null;
    }

    IEnumerator GetMapName()
    {
        WWWForm form = new WWWForm();
        form.AddField("Game_ID", int.Parse(SelectedGameToLoad));

        using (UnityWebRequest www = UnityWebRequest.Post("https://www.stark44.com/Heroclix/GetMapName.php", form))
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
                string MapNameText = www.downloadHandler.text;

                Data_Storage.Instance.Map_Name = MapNameText.Substring(0, MapNameText.IndexOf("\t"));
                Data_Storage.Instance.Map_Size = MapNameText.Substring(MapNameText.IndexOf("\t") + 1, MapNameText.Length - MapNameText.IndexOf("\t") - 1);
            }

        }
        Debug.Log("Map Name Loaded.");
        yield return null;
    }

    public void JoinGame_Dropdown_IndexChanged(int index)
    {
        SelectedGameToJoin = OpenGames[index];
        SelectedGameToJoin = SelectedGameToJoin.Substring(0, SelectedGameToJoin.IndexOf("\t"));
    }

    public void CallJoinGame()
    {
        StartCoroutine(JoinGame());
    }

    IEnumerator JoinGame()
    {
        WWWForm form = new WWWForm();
        form.AddField("Username", CurrentUsername);
        form.AddField("Game_ID", int.Parse(SelectedGameToJoin));

        using (UnityWebRequest www = UnityWebRequest.Post("https://www.stark44.com/Heroclix/JoinGame.php", form))
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
                string JoinGameText = www.downloadHandler.text;

                if(JoinGameText == "0")
                {
                    Debug.Log("Joined Game Successfully.");
                }
                else
                {
                    Debug.Log("Error: " + JoinGameText);
                }

                Start();

            }
        }

    }

    public void EndGame_Dropdown_IndexChanged(int index)
    {
        SelectedGameToEnd = CurrentGames[index];
        SelectedGameToEnd = SelectedGameToEnd.Substring(0, SelectedGameToEnd.IndexOf("\t"));
    }

    public void CallEndGame()
    {
        StartCoroutine(EndGame());
    }

    IEnumerator EndGame()
    {
        WWWForm form = new WWWForm();
        form.AddField("GameID", int.Parse(SelectedGameToEnd));

        using (UnityWebRequest www = UnityWebRequest.Post("https://www.stark44.com/Heroclix/endGame.php", form))
        {
            www.certificateHandler = new BypassCertificate();
            yield return www.SendWebRequest();

            if (www.result != UnityWebRequest.Result.Success)
            {
                Debug.Log(www.error);
            }
            else
            {
                string DBGames = www.downloadHandler.text;

                if (DBGames == "0")
                {
                    Start();
                }

            }
        }

    }

    public void LoadCompletedGame_Dropdown_IndexChanged(int index)
    {
        SelectedGameToLoadCompleted = CompletedGames[index];
        SelectedGameToLoadCompleted = SelectedGameToLoadCompleted.Substring(0, SelectedGameToLoadCompleted.IndexOf("\t"));
    }

    public void CallLoadCompletedGame()
    {
        StartCoroutine(LoadCompletedGame());
    }

    IEnumerator LoadCompletedGame()
    {
        WWWForm form = new WWWForm();
        form.AddField("Game_ID", int.Parse(SelectedGameToLoadCompleted));

        using (UnityWebRequest www = UnityWebRequest.Post("https://www.stark44.com/Heroclix/LoadGame.php", form))
        {
            www.certificateHandler = new BypassCertificate();
            yield return www.SendWebRequest();

            if (www.result != UnityWebRequest.Result.Success)
            {
                Debug.Log(www.error);
            }
            else
            {
                //byte[] results = www.downLoadCompletedHandler.data;
                string LoadCompletedGameText = www.downloadHandler.text;

                if (LoadCompletedGameText == "0")
                {
                    Debug.Log("Loaded Game Successfully.");
                }

                Data_Storage.Instance.Saved_Game = LoadCompletedGameText;
                Data_Storage.Instance.Game_ID = int.Parse(SelectedGameToLoadCompleted);

                Debug.Log(Data_Storage.Instance.Saved_Game);
                SceneManager.LoadScene("Default_Map");
            }

        }


    }

    public void callStart()
    {
        if (LoadMenuHasLoaded)
        {
            Start();
        }

    }

    

    

}
