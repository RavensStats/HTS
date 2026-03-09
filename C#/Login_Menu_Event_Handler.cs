using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

public class Login_Menu_Event_Handler : MonoBehaviour
{

    public TMPro.TMP_InputField LoginUsername, LoginPassword, RegisterUsername, RegisterPassword, RegisterEmail;
    public Button LoginAttempt, RegisterAttempt;
    public GameObject LoginMenuObj, MainMenuObj;
    public Toggle RememberLogin, ChatWithEnter;
    int Fetched_User_ID;
    bool VersionValidated;
    public Text LoginMessage, VersionNumber;

    public IEnumerator Start()
    {
        if(PlayerPrefs.GetString("Logged_In") == "Yes")
        {
            LoginMenuObj.SetActive(false);
            MainMenuObj.SetActive(true);

            // Re-fetch User_ID if it wasn't saved (e.g. app was killed on Android)
            if (PlayerPrefs.GetInt("User_ID") == 0)
            {
                string storedUser = PlayerPrefs.GetString("Stored_Username");
                if (!string.IsNullOrEmpty(storedUser))
                {
                    LoginUsername.text = storedUser;
                    yield return StartCoroutine(GetUserID());
                    PlayerPrefs.SetInt("User_ID", Fetched_User_ID);
                    PlayerPrefs.SetString("Active_Username", storedUser);
                    PlayerPrefs.Save();
                }
            }
            else if (string.IsNullOrEmpty(PlayerPrefs.GetString("Active_Username")))
            {
                // Active_Username missing (old install) — restore from Stored_Username
                string storedUser = PlayerPrefs.GetString("Stored_Username");
                if (!string.IsNullOrEmpty(storedUser))
                {
                    PlayerPrefs.SetString("Active_Username", storedUser);
                    PlayerPrefs.Save();
                }
            }
        }

        if(PlayerPrefs.GetInt("Chat_With_Enter") == 1)
        {
            ChatWithEnter.isOn = true;
        }
        else
        {
            ChatWithEnter.isOn = false;
        }
    }

    public void SaveChatPrefs()
    {
        if(ChatWithEnter.isOn == true)
        {
            PlayerPrefs.SetInt("Chat_With_Enter", 1);
        }
        else
        {
            PlayerPrefs.SetInt("Chat_With_Enter", 0);
        }

        PlayerPrefs.Save();
    }

    public void SaveLoginPrefs()
    {
        if (RememberLogin.isOn == true)
        {
            PlayerPrefs.SetInt("Remember_Login", 1);
            PlayerPrefs.SetString("Stored_Username", LoginUsername.text);
            PlayerPrefs.SetString("Stored_Password", LoginPassword.text);
        }
        else
        {
            PlayerPrefs.SetInt("Remember_Login", 0);
            PlayerPrefs.SetString("Stored_Username", "");
            PlayerPrefs.SetString("Stored_Password", "");
        }

        PlayerPrefs.Save();
    }

    public void CheckForRememberLogin()
    {
        if (PlayerPrefs.GetInt("Remember_Login") == 1)
        {
            RememberLogin.isOn = true;
            LoginUsername.text = PlayerPrefs.GetString("Stored_Username");
            LoginPassword.text = PlayerPrefs.GetString("Stored_Password");
        }
    }

    public void CallRegister()
    {
        Debug.Log("[register] CallRegister() invoked. Email field null? " + (RegisterEmail == null));
        StartCoroutine(Register());
    }

    IEnumerator Register()
    {
        WWWForm form = new WWWForm();
        form.AddField("username", RegisterUsername.text);
        form.AddField("password", RegisterPassword.text);
        form.AddField("email", RegisterEmail == null ? "" : RegisterEmail.text);
        Debug.Log("[register] Sending POST. username=" + RegisterUsername.text + " email=" + (RegisterEmail == null ? "(null field)" : RegisterEmail.text));

        using (UnityWebRequest www = UnityWebRequest.Post("https://www.stark44.com/Heroclix/register.php", form))
        {
            www.certificateHandler = new BypassCertificate();
            www.timeout = 15;
            yield return www.SendWebRequest();

            Debug.Log("[register] result=" + www.result + " httpCode=" + www.responseCode + " error=" + www.error);
            string RegisterData = www.downloadHandler != null ? www.downloadHandler.text : "(no body)";
            Debug.Log("[register_test] raw response: " + RegisterData);

            if (www.result != UnityWebRequest.Result.Success)
            {
                Debug.Log("[register] Network/HTTP error: " + www.error);
            }
            else
            {
                try
                {
                    var json = JsonUtility.FromJson<RegisterResponse>(RegisterData);
                    if (json != null && json.success)
                    {
                        Debug.Log("[register] User Created Successfully!");
                    }
                    else
                    {
                        Debug.Log("[register] User creation failed: " + RegisterData);
                    }
                }
                catch (System.Exception e)
                {
                    Debug.Log("[register] JSON parse exception: " + e.Message + " | raw: " + RegisterData);
                }
            }

        }
    }

    public void CallLogin()
    {
        StartCoroutine(LoginCoroutine());
    }

    IEnumerator LoginCoroutine()
    {
        yield return StartCoroutine(Login());

        if (PlayerPrefs.GetString("Logged_In") == "Yes")
        {
            yield return StartCoroutine(GetUserID());
            PlayerPrefs.SetInt("User_ID", Fetched_User_ID);
            PlayerPrefs.SetString("Active_Username", LoginUsername.text);
            PlayerPrefs.Save();
        }
    }

    public void CallCheckVersion()
    {
        StartCoroutine(CheckVersionCoroutine());
    }

    IEnumerator CheckVersionCoroutine()
    {
        yield return StartCoroutine(CheckVersion());

        if (VersionValidated)
        {
            yield return StartCoroutine(LoginCoroutine());
        }
        else
        {
            LoginMessage.text = "App out of date.\nPlease install the current version.";
        }
    }

    IEnumerator CheckVersion()
    {
        WWWForm form = new WWWForm();
        form.AddField("App_Version", VersionNumber.text);

        using (UnityWebRequest www = UnityWebRequest.Post("https://www.stark44.com/Heroclix/CheckVersion.php", form))
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
                string LoginData = www.downloadHandler.text;

                VersionResponse vr = JsonUtility.FromJson<VersionResponse>(LoginData);
                VersionValidated = vr != null && vr.success && vr.up_to_date;


            }

        }

        yield return null;

    }

    IEnumerator Login()
    {
        WWWForm form = new WWWForm();
        form.AddField("username", LoginUsername.text);
        form.AddField("password", LoginPassword.text);

        using (UnityWebRequest www = UnityWebRequest.Post("https://www.stark44.com/Heroclix/login.php", form))
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
                string LoginData = www.downloadHandler.text;

                LoginResponse lr = JsonUtility.FromJson<LoginResponse>(LoginData);
                if (lr != null && lr.success)
                {
                    Debug.Log("User Logged in Successfully!");
                    LoginMenuObj.SetActive(false);
                    MainMenuObj.SetActive(true);
                    if (RememberLogin)
                    {
                        PlayerPrefs.SetInt("Remember_Login", 1);
                        PlayerPrefs.SetString("Stored_Username", LoginUsername.text);
                        PlayerPrefs.SetString("Stored_Password", LoginPassword.text);
                        PlayerPrefs.Save();
                    }
                    else
                    {
                        PlayerPrefs.SetString("Stored_Username", LoginUsername.text);
                        PlayerPrefs.Save();
                    }
                    SaveLoginPrefs();
                    PlayerPrefs.SetString("Logged_In", "Yes");
                    PlayerPrefs.Save();

                }
                else
                {
                    Debug.Log("User Login failed. Error # " + www.downloadHandler.text);
                }


            }

        }

        yield return null;

    }

    IEnumerator GetUserID()
    {
        WWWForm form = new WWWForm();
        form.AddField("Username", LoginUsername.text);

        using (UnityWebRequest www = UnityWebRequest.Post("https://www.stark44.com/Heroclix/getUserID.php", form))
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
                string LoginData = www.downloadHandler.text;

                if (int.TryParse(LoginData.Trim(), out int parsed_id))
                    Fetched_User_ID = parsed_id;


            }

        }

        yield return null;
    }


    public void VerifyInputs()
    {
        RegisterAttempt.interactable = (RegisterUsername.text.Length >= 3 && RegisterPassword.text.Length >= 8 && RegisterEmail.text.Length > 0);
    }

    public void Quit_Application()
    {
        PlayerPrefs.SetString("Logged_In", "No");
        PlayerPrefs.Save();
        Application.Quit();
    }

}

[System.Serializable]
public class UserIDResponse
{
    public bool success;
    public int user_id;
    public string error;
}

[System.Serializable]
public class LoginResponse
{
    public bool success;
    public string error;
}

[System.Serializable]
public class RegisterResponse
{
    public bool success;
    public string error;
}

[System.Serializable]
public class VersionResponse
{
    public bool success;
    public bool up_to_date;
}

// Bypasses SSL certificate validation — safe for development/testing.
public class BypassCertificate : UnityEngine.Networking.CertificateHandler
{
    protected override bool ValidateCertificate(byte[] certificateData) { return true; }
}
