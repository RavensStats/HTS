using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Create_Unit_Event_Handler : MonoBehaviour
{

    //Add New Dynamic Unit Section

    List<int> AllPowersList = new List<int>();
    List<string> AllPowerNamesList = new List<string>();
    List<string> AllPowerDescriptionsList = new List<string>();

    public InputField Add_Set_Name_Input;
    public Text Step_1_Status_Message_Text;

    public Toggle NewPowerIsStandard_Toggle;
    public Dropdown Add_Power_Type_Dropdown;
    public InputField Add_Power_Name_Input, Add_Power_Desc_Input;

    //End Add New Dynamic Unit Section

    //Step 1 Section

    public InputField Keywords_Input, Trait1_Input, Trait2_Input, Trait3_Input, Trait4_Input, Trait5_Input, Trait6_Input;
    public Toggle Official_Unit_Toggle;
    //

    //Step 2 Section

    public GameObject Step1Menu, Step2Menu, Step3Menu;

    public InputField Collectors_Number_Input, Unit_Name_Input, Dial_Length_Input, Points_Input, Range_Value_Input;
    public Dropdown Set_Dropdown, Range_Targets_Dropdown, Experience_Dropdown, Rarity_Dropdown, Base_Type_Dropdown, Speed_Symbol_Dropdown, Attack_Symbol_Dropdown, Defense_Symbol_Dropdown, Damage_Symbol_Dropdown, Team_Ability_Dropdown;
    public Toggle IM_Red, IM_Green, IM_Blue, IM_Brown, IM_Outside_Brown, IM_Destroy_Brown, IM_Bullseye, IM_Adjacent_Characters;
    public Toggle IT_Red, IT_Green, IT_Brown, IT_Destroy_Brown, IT_Bullseye, IT_Adjacent_Characters;

    public Dropdown SetNameDropdown, SpeedPowerDropdown, AttackPowerDropdown, DefensePowerDropdown, DamagePowerDropdown;

    public List<string> DropdownSetNameList = new List<string>();
    //

    //Step 3 Section
    public List<string> DropdownSpeedPowerList = new List<string>();
    public List<string> DropdownAttackPowerList = new List<string>();
    public List<string> DropdownDefensePowerList = new List<string>();
    public List<string> DropdownDamagePowerList = new List<string>();

    public List<string> DropdownSpeedPowerIndexList = new List<string>();
    public List<string> DropdownAttackPowerIndexList = new List<string>();
    public List<string> DropdownDefensePowerIndexList = new List<string>();
    public List<string> DropdownDamagePowerIndexList = new List<string>();

    public InputField SpeedVal_Input, AttackVal_Input, DefenseVal_Input, DamageVal_Input;

    public List<int> SpeedValList = new List<int>();
    public List<int> AttackValList = new List<int>();
    public List<int> DefenseValList = new List<int>();
    public List<int> DamageValList = new List<int>();

    public List<string> SpeedPowList = new List<string>();
    public List<string> AttackPowList = new List<string>();
    public List<string> DefensePowList = new List<string>();
    public List<string> DamagePowList = new List<string>();

    public Toggle ClearDialInputs;

    public Text PowerDescription;
    //

    //Preview Unit Section

    List<string> valuesList = new List<string>();
    List<Color> powersList = new List<Color>();
    List<Color> textColorList = new List<Color>();
    int currentDialIndex = 0;

    public Image SpdI1, SpdI2, SpdI3, SpdI4, SpdI5, SpdI6, SpdI7, SpdI8, SpdI9, SpdI10, SpdI11, SpdI12;
    public Image AtkI1, AtkI2, AtkI3, AtkI4, AtkI5, AtkI6, AtkI7, AtkI8, AtkI9, AtkI10, AtkI11, AtkI12;
    public Image DefI1, DefI2, DefI3, DefI4, DefI5, DefI6, DefI7, DefI8, DefI9, DefI10, DefI11, DefI12;
    public Image DmgI1, DmgI2, DmgI3, DmgI4, DmgI5, DmgI6, DmgI7, DmgI8, DmgI9, DmgI10, DmgI11, DmgI12;

    public Text Spd1, Spd2, Spd3, Spd4, Spd5, Spd6, Spd7, Spd8, Spd9, Spd10, Spd11, Spd12;
    public Text Atk1, Atk2, Atk3, Atk4, Atk5, Atk6, Atk7, Atk8, Atk9, Atk10, Atk11, Atk12;
    public Text Def1, Def2, Def3, Def4, Def5, Def6, Def7, Def8, Def9, Def10, Def11, Def12;
    public Text Dmg1, Dmg2, Dmg3, Dmg4, Dmg5, Dmg6, Dmg7, Dmg8, Dmg9, Dmg10, Dmg11, Dmg12;
    public Text Slot1, Slot2, Slot3, Slot4, Slot5, Slot6, Slot7, Slot8, Slot9, Slot10, Slot11, Slot12;

    int slotsAdded = 0;
    //

    public void callGetAllPowersList()
    {
        StartCoroutine(GetAllPowersList());
    }

    IEnumerator GetAllPowersList()
    {

        WWWForm form = new WWWForm();

        using (UnityWebRequest www2 = UnityWebRequest.Get("https://www.stark44.com/Heroclix/getAllPowers.php"))
        {
            www2.certificateHandler = new BypassCertificate();
            yield return www2.SendWebRequest();

            if (www2.result != UnityWebRequest.Result.Success)
            {
                Debug.Log(www2.error);
            }
            else
            {
                string Powers = www2.downloadHandler.text;

                while (Powers.Contains(";") && Powers.Length > 1)
                {
                    var IteratedRowData = Powers.Substring(0, Powers.IndexOf(";")).Trim();
                    Powers = Powers.Substring(Powers.IndexOf(";") + 1, Powers.Length - Powers.IndexOf(";") - 1);

                    var parts = IteratedRowData.Split('\t');
                    if (parts.Length < 3) continue;

                    if (!int.TryParse(parts[0].Trim(), out int powerId)) continue;
                    AllPowersList.Add(powerId);
                    AllPowerNamesList.Add(parts[1].Trim());
                    AllPowerDescriptionsList.Add(parts[2].Trim());
                }

            }
        }
        yield return null;
    }

    string SanitizeInputs(string inputstring)
    {
        inputstring = inputstring.Replace("'", "");
        inputstring = inputstring.Replace("\"", "");
        inputstring = inputstring.Replace(";", "");
        inputstring = inputstring.Replace(",", "");

        return inputstring;
    }

    //Check and add Set Name
    public void CallCheckSetName()
    {

        string NewSetName = SanitizeInputs(Add_Set_Name_Input.text);

        if (NewSetName.Length > 255)
        {
            Step_1_Status_Message_Text.text = "Set Name must be 255 characters or less in Step 1.";
        }
        else
        {
            Step_1_Status_Message_Text.text = "Validating Set...";

            StartCoroutine(CheckSetName(NewSetName));
        }
    }

    IEnumerator CheckSetName(string NewSetName)
    {
        WWWForm form = new WWWForm();
        form.AddField("Set_Name", NewSetName);

        using (UnityWebRequest www = UnityWebRequest.Post("https://www.stark44.com/Heroclix/CheckSet.php", form))
        {
            www.certificateHandler = new BypassCertificate();
            yield return www.SendWebRequest();

            if (www.result != UnityWebRequest.Result.Success)
            {
                Debug.Log(www.error);
            }
            else
            {
                string SetAdded = www.downloadHandler.text;

                if (SetAdded == "0")
                {
                    CallAddSetName();
                }
                else
                {
                    Step_1_Status_Message_Text.text = "Error Adding Set: " + SetAdded;
                }

            }

        }

        yield return null;
    }


    public void CallAddSetName()
    {

        string NewSetName = SanitizeInputs(Add_Set_Name_Input.text);

        Step_1_Status_Message_Text.text = "";

        StartCoroutine(AddSetName(NewSetName));
    }

    IEnumerator AddSetName(string NewSetName)
    {
        WWWForm form = new WWWForm();
        form.AddField("Set_Name", NewSetName);

        using (UnityWebRequest www = UnityWebRequest.Post("https://www.stark44.com/Heroclix/AddSet.php", form))
        {
            www.certificateHandler = new BypassCertificate();
            yield return www.SendWebRequest();


            if (www.result != UnityWebRequest.Result.Success)
            {
                Debug.Log(www.error);
            }
            else
            {
                string SetAdded = www.downloadHandler.text;

                if (SetAdded == "0")
                {
                    Step_1_Status_Message_Text.text = "Set Added Successfully";
                    Add_Set_Name_Input.text = "";
                }
                else
                {
                    Step_1_Status_Message_Text.text = "Error Adding Set: " + SetAdded;
                }

            }

        }

        yield return null;
    }

    //Check and add Power

    public void CallCheckPower()
    {
        int NewPowerIsStandard;

        string NewPowerType = Add_Power_Type_Dropdown.options[Add_Power_Type_Dropdown.value].text;
        string NewPowerName = SanitizeInputs(Add_Power_Name_Input.text);
        string NewPowerDesc = SanitizeInputs(Add_Power_Desc_Input.text);
        if (NewPowerIsStandard_Toggle.isOn == true)
        {
            NewPowerIsStandard = 1;
        }
        else
        {
            NewPowerIsStandard = 0;
        }


        Step_1_Status_Message_Text.text = "Validating Power...";

        StartCoroutine(CheckPower(NewPowerType, NewPowerName, NewPowerDesc, NewPowerIsStandard));
    }

    IEnumerator CheckPower(string NewPowerType, string NewPowerName, string NewPowerDesc, int NewPowerIsStandard)
    {
        WWWForm form = new WWWForm();
        form.AddField("Power_Type", NewPowerType);
        form.AddField("Power_Name", NewPowerName);
        form.AddField("Power_Desc", NewPowerDesc);
        form.AddField("Is_Standard", NewPowerIsStandard);

        using (UnityWebRequest www = UnityWebRequest.Post("https://www.stark44.com/Heroclix/CheckPower.php", form))
        {
            www.certificateHandler = new BypassCertificate();
            yield return www.SendWebRequest();

            if (www.result != UnityWebRequest.Result.Success)
            {
                Debug.Log(www.error);
            }
            else
            {
                string PowerAdded = www.downloadHandler.text;

                if (PowerAdded == "0")
                {
                    CallAddPower();
                }
                else
                {
                    Step_1_Status_Message_Text.text = "Error Validating Power: " + PowerAdded;
                }

            }

        }

        yield return null;
    }


    public void CallAddPower()
    {

        int NewPowerIsStandard;

        string NewPowerType = Add_Power_Type_Dropdown.options[Add_Power_Type_Dropdown.value].text;
        string NewPowerName = SanitizeInputs(Add_Power_Name_Input.text);
        string NewPowerDesc = SanitizeInputs(Add_Power_Desc_Input.text);
        if (NewPowerIsStandard_Toggle.isOn == true)
        {
            NewPowerIsStandard = 1;
        }
        else
        {
            NewPowerIsStandard = 0;
        }

        Step_1_Status_Message_Text.text = "";

        StartCoroutine(AddPower(NewPowerType, NewPowerName, NewPowerDesc, NewPowerIsStandard));


    }

    IEnumerator AddPower(string NewPowerType, string NewPowerName, string NewPowerDesc, int NewPowerIsStandard)
    {
        WWWForm form = new WWWForm();
        form.AddField("Power_Type", NewPowerType);
        form.AddField("Power_Name", NewPowerName);
        form.AddField("Power_Desc", NewPowerDesc);
        form.AddField("Is_Standard", NewPowerIsStandard);

        using (UnityWebRequest www = UnityWebRequest.Post("https://www.stark44.com/Heroclix/AddPower.php", form))
        {
            www.certificateHandler = new BypassCertificate();
            yield return www.SendWebRequest();

            if (www.result != UnityWebRequest.Result.Success)
            {
                Debug.Log(www.error);
            }
            else
            {
                string PowerAdded = www.downloadHandler.text;

                if (PowerAdded == "0")
                {
                    Step_1_Status_Message_Text.text = "Power Added Successfully";
                    Add_Power_Type_Dropdown.value = 0;
                    Add_Power_Name_Input.text = "";
                    Add_Power_Desc_Input.text = "";
                }
                else
                {
                    Step_1_Status_Message_Text.text = "Error Adding Power: " + PowerAdded;
                }

            }

        }

        yield return null;
    }

    public void CallGetSetNames()
    {
        StartCoroutine(GetSetNames());
    }

    IEnumerator GetSetNames()
    {
        DropdownSetNameList.Add("");

        using (UnityWebRequest www = UnityWebRequest.Get("https://www.stark44.com/Heroclix/getSetNames.php"))
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
                    DropdownSetNameList.Add(DBUsers.Substring(0, DBUsers.IndexOf(",")));
                    DBUsers = DBUsers.Substring(DBUsers.IndexOf(",") + 1, DBUsers.Length - DBUsers.IndexOf(",") - 1);
                }

                SetNameDropdown.AddOptions(DropdownSetNameList);

            }
        }

    }

    public void CallGetSpeedPowers()
    {
        StartCoroutine(GetSpeedPowers());
    }

    IEnumerator GetSpeedPowers()
    {
        DropdownSpeedPowerList.Clear();
        DropdownSpeedPowerIndexList.Clear();

        DropdownSpeedPowerList.Add("None");

        using (UnityWebRequest www = UnityWebRequest.Get("https://www.stark44.com/Heroclix/getSpeedPowers.php"))
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
                    DropdownSpeedPowerIndexList.Add(DBUsers.Substring(0, DBUsers.IndexOf("\t")));
                    DBUsers = DBUsers.Substring(DBUsers.IndexOf("\t") + 1, DBUsers.Length - DBUsers.IndexOf("\t") - 1);

                    DropdownSpeedPowerList.Add(DBUsers.Substring(0, DBUsers.IndexOf(",")));
                    DBUsers = DBUsers.Substring(DBUsers.IndexOf(",") + 1, DBUsers.Length - DBUsers.IndexOf(",") - 1);
                }

                SpeedPowerDropdown.AddOptions(DropdownSpeedPowerList);

            }
        }

    }

    public void CallGetAttackPowers()
    {
        StartCoroutine(GetAttackPowers());
    }

    IEnumerator GetAttackPowers()
    {
        DropdownAttackPowerList.Clear();
        DropdownAttackPowerIndexList.Clear();

        DropdownAttackPowerList.Add("None");

        using (UnityWebRequest www = UnityWebRequest.Get("https://www.stark44.com/Heroclix/getAttackPowers.php"))
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
                    DropdownAttackPowerIndexList.Add(DBUsers.Substring(0, DBUsers.IndexOf("\t")));
                    DBUsers = DBUsers.Substring(DBUsers.IndexOf("\t") + 1, DBUsers.Length - DBUsers.IndexOf("\t") - 1);

                    DropdownAttackPowerList.Add(DBUsers.Substring(0, DBUsers.IndexOf(",")));
                    DBUsers = DBUsers.Substring(DBUsers.IndexOf(",") + 1, DBUsers.Length - DBUsers.IndexOf(",") - 1);
                }

                AttackPowerDropdown.AddOptions(DropdownAttackPowerList);

            }
        }

    }

    public void CallGetDefensePowers()
    {
        StartCoroutine(GetDefensePowers());
    }

    IEnumerator GetDefensePowers()
    {
        DropdownDefensePowerList.Clear();
        DropdownDefensePowerIndexList.Clear();
        DropdownDefensePowerList.Add("None");

        using (UnityWebRequest www = UnityWebRequest.Get("https://www.stark44.com/Heroclix/getDefensePowers.php"))
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
                    DropdownDefensePowerIndexList.Add(DBUsers.Substring(0, DBUsers.IndexOf("\t")));
                    DBUsers = DBUsers.Substring(DBUsers.IndexOf("\t") + 1, DBUsers.Length - DBUsers.IndexOf("\t") - 1);

                    DropdownDefensePowerList.Add(DBUsers.Substring(0, DBUsers.IndexOf(",")));
                    DBUsers = DBUsers.Substring(DBUsers.IndexOf(",") + 1, DBUsers.Length - DBUsers.IndexOf(",") - 1);
                }

                DefensePowerDropdown.AddOptions(DropdownDefensePowerList);

            }
        }

    }

    public void CallGetDamagePowers()
    {
        StartCoroutine(GetDamagePowers());
    }

    IEnumerator GetDamagePowers()
    {
        DropdownDamagePowerList.Clear();
        DropdownDamagePowerIndexList.Clear();

        DropdownDamagePowerList.Add("None");

        using (UnityWebRequest www = UnityWebRequest.Get("https://www.stark44.com/Heroclix/getDamagePowers.php"))
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
                    DropdownDamagePowerIndexList.Add(DBUsers.Substring(0, DBUsers.IndexOf("\t")));
                    DBUsers = DBUsers.Substring(DBUsers.IndexOf("\t") + 1, DBUsers.Length - DBUsers.IndexOf("\t") - 1);

                    DropdownDamagePowerList.Add(DBUsers.Substring(0, DBUsers.IndexOf(",")));
                    DBUsers = DBUsers.Substring(DBUsers.IndexOf(",") + 1, DBUsers.Length - DBUsers.IndexOf(",") - 1);
                }

                DamagePowerDropdown.AddOptions(DropdownDamagePowerList);

            }
        }

    }

    public void CallGetSpeedPowerDescription()
    {
        if (SpeedPowerDropdown.value != 0)
        {
            StartCoroutine(GetPowerDescription("Speed", DropdownSpeedPowerIndexList[SpeedPowerDropdown.value - 1]));
        }
        else
        {
            PowerDescription.text = "";
        }
    }

    public void CallGetAttackPowerDescription()
    {
        if (AttackPowerDropdown.value != 0)
        {
            StartCoroutine(GetPowerDescription("Attack", DropdownAttackPowerIndexList[AttackPowerDropdown.value - 1]));
        }
        else
        {
            PowerDescription.text = "";
        }
    }

    public void CallGetDefensePowerDescription()
    {
        if (DefensePowerDropdown.value != 0)
        {
            StartCoroutine(GetPowerDescription("Defense", DropdownDefensePowerIndexList[DefensePowerDropdown.value - 1]));
        }
        else
        {
            PowerDescription.text = "";
        }
    }

    public void CallGetDamagePowerDescription()
    {
        if (DamagePowerDropdown.value != 0)
        {
            StartCoroutine(GetPowerDescription("Damage", DropdownDamagePowerIndexList[DamagePowerDropdown.value - 1]));
        }
        else
        {
            PowerDescription.text = "";
        }
    }

    IEnumerator GetPowerDescription(string PowerType, string Power_ID)
    {
        WWWForm form = new WWWForm();
        form.AddField("Power_Type", PowerType);
        form.AddField("Power_ID", Power_ID);

        using (UnityWebRequest www = UnityWebRequest.Post("https://www.stark44.com/Heroclix/getPowerDescription.php",form))
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

                PowerDescription.text = DBUsers;

            }
        }

    }

    public void ClearDialDropdowns()
    {
        DropdownSpeedPowerList.Clear();
        DropdownAttackPowerList.Clear();
        DropdownDefensePowerList.Clear();
        DropdownDamagePowerList.Clear();

        SpeedPowerDropdown.ClearOptions();
        AttackPowerDropdown.ClearOptions();
        DefensePowerDropdown.ClearOptions();
        DamagePowerDropdown.ClearOptions();

        Step_1_Status_Message_Text.text = " ";
        PowerDescription.text = " ";
    }

    public void ClearStepTwoDropdowns()
    {
        SetNameDropdown.ClearOptions();
        DropdownSetNameList.Clear();

        Step_1_Status_Message_Text.text = " ";
        
    }

    public void ClearDial()
    {

            SpeedValList.Clear();
            AttackValList.Clear();
            DefenseValList.Clear();
            DamageValList.Clear();

            SpeedPowList.Clear();
            AttackPowList.Clear();
            DefensePowList.Clear();
            DamagePowList.Clear();

            slotsAdded = 0;
            Preview_Dial(0);

    }

    public void AddKO()
    {
        int tempIndex = SpeedValList.Count + 1;

        if (tempIndex - 1 < int.Parse(Dial_Length_Input.text))
        {
            for (int i = SpeedValList.Count; i < tempIndex; i++)
            {
                SpeedValList.Add(-1);
                AttackValList.Add(-1);
                DefenseValList.Add(-1);
                DamageValList.Add(-1);

                SpeedPowList.Add("None");
                AttackPowList.Add("None");
                DefensePowList.Add("None");
                DamagePowList.Add("None");

                slotsAdded++;
                Preview_Dial(0);
            }
        }
    }

    public void AddKOs()
    {

        for (int i = SpeedValList.Count; i < int.Parse(Dial_Length_Input.text) ; i++){
            SpeedValList.Add(-1);
            AttackValList.Add(-1);
            DefenseValList.Add(-1);
            DamageValList.Add(-1);

            SpeedPowList.Add("None");
            AttackPowList.Add("None");
            DefensePowList.Add("None");
            DamagePowList.Add("None");

            slotsAdded++;
            Preview_Dial(0);
        }
    }

    public void AddSlot()
    {
        Step_1_Status_Message_Text.text = " ";
        int IsNumeric;

        if (int.TryParse(SpeedVal_Input.text, out IsNumeric) && int.TryParse(AttackVal_Input.text, out IsNumeric) && int.TryParse(DefenseVal_Input.text, out IsNumeric) && int.TryParse(DamageVal_Input.text, out IsNumeric))
        {
            if (SpeedVal_Input.text != "" && AttackVal_Input.text != "" && DefenseVal_Input.text != "" && DamageVal_Input.text != "")
            {
                if (SpeedValList.Count < int.Parse(Dial_Length_Input.text))
                {
                    SpeedValList.Add(int.Parse(SpeedVal_Input.text));
                    AttackValList.Add(int.Parse(AttackVal_Input.text));
                    DefenseValList.Add(int.Parse(DefenseVal_Input.text));
                    DamageValList.Add(int.Parse(DamageVal_Input.text));

                    SpeedPowList.Add(SpeedPowerDropdown.options[SpeedPowerDropdown.value].text);
                    AttackPowList.Add(AttackPowerDropdown.options[AttackPowerDropdown.value].text);
                    DefensePowList.Add(DefensePowerDropdown.options[DefensePowerDropdown.value].text);
                    DamagePowList.Add(DamagePowerDropdown.options[DamagePowerDropdown.value].text);

                    if (ClearDialInputs.isOn == true)
                    {
                        SpeedVal_Input.text = "";
                        AttackVal_Input.text = "";
                        DefenseVal_Input.text = "";
                        DamageVal_Input.text = "";

                        SpeedPowerDropdown.value = 0;
                        AttackPowerDropdown.value = 0;
                        DefensePowerDropdown.value = 0;
                        DamagePowerDropdown.value = 0;
                    }
                    slotsAdded++;
                    Preview_Dial(0);
                }
                else
                {
                    Step_1_Status_Message_Text.text = "Cannot add new slot, Dial too short.";
                }
            }
            else
            {
                Step_1_Status_Message_Text.text = "Cannot add new slot, value field is empty.";
            }
        }
        else
        {
            Step_1_Status_Message_Text.text = "Slots can only contain numeric values.";
        }
    }

    public void RemoveSlot()
    {
        Step_1_Status_Message_Text.text = " ";

        if (SpeedValList.Count > 0)
        {
            SpeedValList.RemoveAt(SpeedValList.Count - 1);
            AttackValList.RemoveAt(AttackValList.Count - 1);
            DefenseValList.RemoveAt(DefenseValList.Count - 1);
            DamageValList.RemoveAt(DamageValList.Count - 1);

            SpeedPowList.RemoveAt(SpeedPowList.Count - 1);
            AttackPowList.RemoveAt(AttackPowList.Count - 1);
            DefensePowList.RemoveAt(DefensePowList.Count - 1);
            DamagePowList.RemoveAt(DamagePowList.Count - 1);

            slotsAdded--;
            Preview_Dial(0);
        }
        else
        {
            Step_1_Status_Message_Text.text = "Dial is empty!";
        }
           
    }


    public void Previous_Click()
    {
        if (currentDialIndex > 0)
        {
            //Update_Previewed_Dial(units, selectedUnitIndex, currentDialIndex - 1);
            Preview_Dial(currentDialIndex - 1);

            Slot1.text = (currentDialIndex + 1).ToString();
            Slot2.text = (currentDialIndex + 2).ToString();
            Slot3.text = (currentDialIndex + 3).ToString();
            Slot4.text = (currentDialIndex + 4).ToString();
            Slot5.text = (currentDialIndex + 5).ToString();
            Slot6.text = (currentDialIndex + 6).ToString();
            Slot7.text = (currentDialIndex + 7).ToString();
            Slot8.text = (currentDialIndex + 8).ToString();
            Slot9.text = (currentDialIndex + 9).ToString();
            Slot10.text = (currentDialIndex + 10).ToString();
            Slot11.text = (currentDialIndex + 11).ToString();
            Slot12.text = (currentDialIndex + 12).ToString();
        }
    }

    public void Next_Click()
    {

        if (currentDialIndex + 12 <= 12 + slotsAdded && slotsAdded > 12)
        {

            //Update_Previewed_Dial(units, selectedUnitIndex, currentDialIndex + 1);
            Preview_Dial(currentDialIndex + 1);


            Slot1.text = (currentDialIndex + 1).ToString();
            Slot2.text = (currentDialIndex + 2).ToString();
            Slot3.text = (currentDialIndex + 3).ToString();
            Slot4.text = (currentDialIndex + 4).ToString();
            Slot5.text = (currentDialIndex + 5).ToString();
            Slot6.text = (currentDialIndex + 6).ToString();
            Slot7.text = (currentDialIndex + 7).ToString();
            Slot8.text = (currentDialIndex + 8).ToString();
            Slot9.text = (currentDialIndex + 9).ToString();
            Slot10.text = (currentDialIndex + 10).ToString();
            Slot11.text = (currentDialIndex + 11).ToString();
            Slot12.text = (currentDialIndex + 12).ToString();

        }

    }

    public void CallInsertDial()
    {
        StartCoroutine(InsertDial());
    }

    IEnumerator InsertDial()
    {
        yield return StartCoroutine(GetAllPowersList());

        //Fetch Powers, listize id, name, description -> unit
        int earlyExit = 0;

        for (int slot = 0; slot < SpeedPowList.Count; slot++)
        {
            earlyExit = 0;

            for (int powerCount = 0; powerCount < AllPowersList.Count; powerCount++)
            {
                if (SpeedValList[slot] != -1)
                {
                    if (SpeedPowList[slot] == AllPowerNamesList[powerCount])
                    {
                        SpeedPowList[slot] = AllPowersList[powerCount].ToString();
                        earlyExit++;
                    }

                    if (AttackPowList[slot] == AllPowerNamesList[powerCount])
                    {
                        AttackPowList[slot] = AllPowersList[powerCount].ToString();
                        earlyExit++;
                    }

                    if (DefensePowList[slot] == AllPowerNamesList[powerCount])
                    {
                        DefensePowList[slot] = AllPowersList[powerCount].ToString();
                        earlyExit++;
                    }

                    if (DamagePowList[slot] == AllPowerNamesList[powerCount])
                    {
                        DamagePowList[slot] = AllPowersList[powerCount].ToString();
                        earlyExit++;
                    }



                    if (earlyExit == 4)
                    {
                        powerCount = AllPowersList.Count;
                    }
                }
                else
                {
                    SpeedPowList[slot] = "-1";
                    AttackPowList[slot] = "-1";
                    DefensePowList[slot] = "-1";
                    DamagePowList[slot] = "-1";
                }
            }
        }

        //No Powers
        for (int slot = 0; slot < SpeedPowList.Count; slot++)
        {
            if (SpeedPowList[slot] == "None")
            {
                SpeedPowList[slot] = "0";
            }

            if (AttackPowList[slot] == "None")
            {
                AttackPowList[slot] = "0";
            }

            if (DefensePowList[slot] == "None")
            {
                DefensePowList[slot] = "0";
            }

            if (DamagePowList[slot] == "None")
            {
                DamagePowList[slot] = "0";
            }

        }

        for (int i = 0; i < SpeedValList.Count; i++)
        {

            WWWForm form = new WWWForm();

            form.AddField("Set_Name", Set_Dropdown.options[Set_Dropdown.value].text);
            form.AddField("Collectors_Number", Collectors_Number_Input.text);
            form.AddField("Slot", i + 1);

            form.AddField("Speed_Value", SpeedValList[i]);
            form.AddField("Attack_Value", AttackValList[i]);
            form.AddField("Defense_Value", DefenseValList[i]);
            form.AddField("Damage_Value", DamageValList[i]);

            form.AddField("Speed_Power", SpeedPowList[i]);
            form.AddField("Attack_Power", AttackPowList[i]);
            form.AddField("Defense_Power", DefensePowList[i]);
            form.AddField("Damage_Power", DamagePowList[i]);

            using (UnityWebRequest www = UnityWebRequest.Post("https://www.stark44.com/Heroclix/InsertDial.php", form))
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
                }
            }
        }

        Step_1_Status_Message_Text.text = "Unit Added Successfully!";

        yield return null;
    }

    public void CallAddUnit()
    {
        StartCoroutine(AddUnit());
    }

    IEnumerator AddUnit()
    {

        WWWForm form = new WWWForm();

        form.AddField("Set_Name", Set_Dropdown.options[Set_Dropdown.value].text);
        form.AddField("Collectors_Number", SanitizeInputs(Collectors_Number_Input.text));

        form.AddField("Unit_Name", SanitizeInputs(Unit_Name_Input.text));
        form.AddField("Dial_Length", int.Parse(Dial_Length_Input.text));
        form.AddField("Last_Click", 0);
        form.AddField("Points", int.Parse(Points_Input.text));
        form.AddField("Range_Value", int.Parse(Range_Value_Input.text));
        form.AddField("Range_Targets", int.Parse(Range_Targets_Dropdown.options[Range_Targets_Dropdown.value].text));
        form.AddField("Experience", Experience_Dropdown.options[Experience_Dropdown.value].text);
        form.AddField("Rarity", Rarity_Dropdown.options[Rarity_Dropdown.value].text);
        form.AddField("Base_Type", Base_Type_Dropdown.options[Base_Type_Dropdown.value].text);
        form.AddField("Speed_Symbol", Speed_Symbol_Dropdown.options[Speed_Symbol_Dropdown.value].text);
        form.AddField("Attack_Symbol", Attack_Symbol_Dropdown.options[Attack_Symbol_Dropdown.value].text);
        form.AddField("Defense_Symbol", Defense_Symbol_Dropdown.options[Defense_Symbol_Dropdown.value].text);
        form.AddField("Damage_Symbol", Damage_Symbol_Dropdown.options[Damage_Symbol_Dropdown.value].text);

        int tempToggle;

        if (IM_Red.isOn == true)
        {
            tempToggle = 1;
        }
        else
        {
            tempToggle = 0;
        }

        form.AddField("IM_Elevated", tempToggle);

        if (IM_Green.isOn == true)
        {
            tempToggle = 1;
        }
        else
        {
            tempToggle = 0;
        }

        form.AddField("IM_Hindering", tempToggle);

        if (IM_Blue.isOn == true)
        {
            tempToggle = 1;
        }
        else
        {
            tempToggle = 0;
        }

        form.AddField("IM_Water", tempToggle);

        if (IM_Brown.isOn == true)
        {
            tempToggle = 1;
        }
        else
        {
            tempToggle = 0;
        }

        form.AddField("IM_Blocking", tempToggle);

        if (IM_Outside_Brown.isOn == true)
        {
            tempToggle = 1;
        }
        else
        {
            tempToggle = 0;
        }

        form.AddField("IM_Outdoor_Blocking", tempToggle);

        if (IM_Destroy_Brown.isOn == true)
        {
            tempToggle = 1;
        }
        else
        {
            tempToggle = 0;
        }

        form.AddField("IM_Blocking_Destroy", tempToggle);

        if (IM_Bullseye.isOn == true)
        {
            tempToggle = 1;
        }
        else
        {
            tempToggle = 0;
        }
        
        form.AddField("IM_Characters", tempToggle);

        if (IM_Adjacent_Characters.isOn == true)
        {
            tempToggle = 1;
        }
        else
        {
            tempToggle = 0;
        }

        form.AddField("IM_Adjacent_Characters", tempToggle);

        if (IT_Red.isOn == true)
        {
            tempToggle = 1;
        }
        else
        {
            tempToggle = 0;
        }

        form.AddField("IT_Elevated", tempToggle);

        if (IT_Green.isOn == true)
        {
            tempToggle = 1;
        }
        else
        {
            tempToggle = 0;
        }

        form.AddField("IT_Hindering", tempToggle);

        if (IT_Brown.isOn == true)
        {
            tempToggle = 1;
        }
        else
        {
            tempToggle = 0;
        }

        form.AddField("IT_Blocking", tempToggle);

        if (IT_Destroy_Brown.isOn == true)
        {
            tempToggle = 1;
        }
        else
        {
            tempToggle = 0;
        }

        form.AddField("IT_Blocking_Destroy", tempToggle);

        if (IT_Bullseye.isOn == true)
        {
            tempToggle = 1;
        }
        else
        {
            tempToggle = 0;
        }

        form.AddField("IT_Characters", tempToggle);

        if (IT_Adjacent_Characters.isOn == true)
        {
            tempToggle = 1;
        }
        else
        {
            tempToggle = 0;
        }

        form.AddField("IT_Adjacent_Characters", tempToggle);

        if (Keywords_Input.text != "")
        {
            if (Keywords_Input.text.Substring(Keywords_Input.text.Length - 2, 1) != ",")
            {
                Keywords_Input.text = Keywords_Input.text + ",";
            }
        }

        form.AddField("Keywords", Keywords_Input.text);
        form.AddField("Team_Ability", Team_Ability_Dropdown.options[Team_Ability_Dropdown.value].text);
        form.AddField("Trait1", SanitizeInputs(Trait1_Input.text));
        form.AddField("Trait2", SanitizeInputs(Trait2_Input.text));
        form.AddField("Trait3", SanitizeInputs(Trait3_Input.text));
        form.AddField("Trait4", SanitizeInputs(Trait4_Input.text));
        form.AddField("Trait5", SanitizeInputs(Trait4_Input.text));
        form.AddField("Trait6", SanitizeInputs(Trait4_Input.text));
        form.AddField("User_ID", PlayerPrefs.GetInt("User_ID"));

        if (Official_Unit_Toggle.isOn == true)
        {
            tempToggle = 0;
        }
        else
        {
            tempToggle = 1;
        }

        form.AddField("Custom_Figure", tempToggle);

        using (UnityWebRequest www = UnityWebRequest.Post("https://www.stark44.com/Heroclix/AddUnit.php", form))
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

                //PowerDescription.text = DBUsers;

            }
        }
        

        //Step_1_Status_Message_Text.text = "Unit Added Successfully!";

        yield return null;
    }

    public void ValidateStep2()
    {
        Step_1_Status_Message_Text.text = "";
        int IsNumeric;

        //if (Keywords_Input.text.Length < 3)
        //{
        //    Step_1_Status_Message_Text.text = "Keywords needs at least 3 characters in step 1.";
        //}

        if (Keywords_Input.text == "")
        {
            Keywords_Input.text = " ";
        }

        if (SetNameDropdown.options[SetNameDropdown.value].text == "")
        {
            Step_1_Status_Message_Text.text = "Please choose a Set Name in step 2.";
        }

        if (Collectors_Number_Input.text == "")
        {
            Step_1_Status_Message_Text.text = "Please enter a Collectors Number in step 2.";
        }

        if (Collectors_Number_Input.text.Length > 10)
        {
            Step_1_Status_Message_Text.text = "Collectors Number max length is 10 characters in step 2.";
        }

        if (Unit_Name_Input.text == "")
        {
            Step_1_Status_Message_Text.text = "Please enter a Unit Name in step 2.";
        }

        if (Unit_Name_Input.text.Length > 128)
        {
            Step_1_Status_Message_Text.text = "Unit Name max length is 128 characters in step 2.";
        }

        if (Dial_Length_Input.text == "")
        {
            Step_1_Status_Message_Text.text = "Please enter a Dial Length in step 2.";
        }

        if (Dial_Length_Input.text.Length > 11)
        {
            Step_1_Status_Message_Text.text = "Dial Length max length is 11 digits in step 2.";
        }

        if (!int.TryParse(Dial_Length_Input.text, out IsNumeric))
        {
            Step_1_Status_Message_Text.text = "Please enter a numeric value for Dial Length in step 2.";
        }

        if (Points_Input.text == "")
        {
            Step_1_Status_Message_Text.text = "Please enter a Points Value in step 2.";
        }

        if (Points_Input.text.Length > 11)
        {
            Step_1_Status_Message_Text.text = "Points Value max length is 11 digits in step 2.";
        }

        if (! int.TryParse(Points_Input.text, out IsNumeric))
        {
            Step_1_Status_Message_Text.text = "Please enter a numeric value for Points Value in step 2.";
        }

        if (Range_Value_Input.text.Length > 11)
        {
            Step_1_Status_Message_Text.text = "Range Value max length is 11 digits in step 2.";
        }

        if (Range_Value_Input.text == "")
        {
            Step_1_Status_Message_Text.text = "Please enter a Range Value in step 2.";
        }

        if (!int.TryParse(Range_Value_Input.text, out IsNumeric))
        {
            Step_1_Status_Message_Text.text = "Please enter a numeric value for Range Value in step 2.";
        }

        if (Step_1_Status_Message_Text.text == "")
        {
            Step2Menu.SetActive(false);
            Step3Menu.SetActive(true);
            CallGetSpeedPowers();
            CallGetAttackPowers();
            CallGetDefensePowers();
            CallGetDamagePowers();
        }

    }

    public void CallCheckUnit()
    {
        Step_1_Status_Message_Text.text = "";

        if (SpeedValList.Count < int.Parse(Dial_Length_Input.text))
        {
            Step_1_Status_Message_Text.text = "Not all Slots have been created in step 3.";
        }

        if (SpeedValList.Count > int.Parse(Dial_Length_Input.text))
        {
            Step_1_Status_Message_Text.text = "Too many Slots have been created in step 3.";
        }

        if (Step_1_Status_Message_Text.text == "")
        {
            StartCoroutine(CheckUnit());
        }
    }

    IEnumerator CheckUnit()
    {

        WWWForm form = new WWWForm();

        form.AddField("Set_Name", Set_Dropdown.options[Set_Dropdown.value].text);
        form.AddField("Collectors_Number", SanitizeInputs(Collectors_Number_Input.text));
        
        using (UnityWebRequest www = UnityWebRequest.Post("https://www.stark44.com/Heroclix/CheckUnit.php", form))
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

                if (DBUsers == "0")
                {
                    if(int.Parse(Dial_Length_Input.text) < 12)
                    {
                        Dial_Length_Input.text = "12";
                    }
                    AddKOs();

                    CallAddUnit();
                    CallInsertDial();
                    ClearNewUnitFields();
                }
                else
                { 
                    Step_1_Status_Message_Text.text = DBUsers;
                }
            }
        }

        yield return null;
    }

    public void ClearNewUnitFields()
    {
        ClearDial();

        DropdownSpeedPowerList.Clear();
        DropdownAttackPowerList.Clear();
        DropdownDefensePowerList.Clear();
        DropdownDamagePowerList.Clear();

        SpeedPowerDropdown.ClearOptions();
        AttackPowerDropdown.ClearOptions();
        DefensePowerDropdown.ClearOptions();
        DamagePowerDropdown.ClearOptions();

        PowerDescription.text = " ";
        SetNameDropdown.ClearOptions();
        DropdownSetNameList.Clear();

        SpeedVal_Input.text = "";
        AttackVal_Input.text = "";
        DefenseVal_Input.text = "";
        DamageVal_Input.text = "";
        Keywords_Input.text = "";
        Trait1_Input.text = "";
        Trait2_Input.text = "";
        Trait3_Input.text = "";
        Trait4_Input.text = "";
        Trait5_Input.text = "";
        Trait6_Input.text = "";
        Collectors_Number_Input.text = "";
        Unit_Name_Input.text = "";
        Dial_Length_Input.text = "";
        Points_Input.text = "";
        Range_Value_Input.text = "";
        Range_Targets_Dropdown.value = 0;
        Experience_Dropdown.value = 0;
        Rarity_Dropdown.value = 0;
        Base_Type_Dropdown.value = 0;
        Speed_Symbol_Dropdown.value = 0;
        Attack_Symbol_Dropdown.value = 0;
        Defense_Symbol_Dropdown.value = 0;
        Damage_Symbol_Dropdown.value = 0;
        Team_Ability_Dropdown.value = 0;
        IM_Red.isOn = false;
        IM_Green.isOn = false;
        IM_Blue.isOn = false;
        IM_Brown.isOn = false;
        IM_Destroy_Brown.isOn = false;
        IM_Outside_Brown.isOn = false;
        IM_Bullseye.isOn = false;
        IM_Adjacent_Characters.isOn = false;
        IT_Red.isOn = false;
        IT_Green.isOn = false;
        IT_Brown.isOn = false;
        IT_Destroy_Brown.isOn = false;
        IT_Bullseye.isOn = false;
        IT_Adjacent_Characters.isOn = false;
    }

    /*DIAL PREVIEW CODE BELOW
    * 
    * 
    *
    * 
    * 
    * 
    * 
    * 
    * 
    * 
    * 
    * 
    * 
    * 
    * 
    * 
    * 
    * 
    * 
    * 
    * 
    * 
    * 
    * 
    * 
    * 
    * 
    * 
    *
    */

    public void Preview_Dial(int startingIndex)
    {

        int DialLength = SpeedValList.Count;
        valuesList.Clear();
        powersList.Clear();
        textColorList.Clear();

        //Clear Dial Preview Images
        Spd1.text = "0";
        Spd1.color = Color.black;
        SpdI1.color = new Color32(255, 255, 255, 255);

        Spd2.text = "0";
        Spd2.color = Color.black;
        SpdI2.color = new Color32(255, 255, 255, 255);

        Spd3.text = "0";
        Spd3.color = Color.black;
        SpdI3.color = new Color32(255, 255, 255, 255);

        Spd4.text = "0";
        Spd4.color = Color.black;
        SpdI4.color = new Color32(255, 255, 255, 255);

        Spd5.text = "0";
        Spd5.color = Color.black;
        SpdI5.color = new Color32(255, 255, 255, 255);

        Spd6.text = "0";
        Spd6.color = Color.black;
        SpdI6.color = new Color32(255, 255, 255, 255);

        Spd7.text = "0";
        Spd7.color = Color.black;
        SpdI7.color = new Color32(255, 255, 255, 255);

        Spd8.text = "0";
        Spd8.color = Color.black;
        SpdI8.color = new Color32(255, 255, 255, 255);

        Spd9.text = "0";
        Spd9.color = Color.black;
        SpdI9.color = new Color32(255, 255, 255, 255);

        Spd10.text = "0";
        Spd10.color = Color.black;
        SpdI10.color = new Color32(255, 255, 255, 255);

        Spd11.text = "0";
        Spd11.color = Color.black;
        SpdI11.color = new Color32(255, 255, 255, 255);

        Spd12.text = "0";
        Spd12.color = Color.black;
        SpdI12.color = new Color32(255, 255, 255, 255);

        Atk1.text = "0";
        Atk1.color = Color.black;
        AtkI1.color = new Color32(255, 255, 255, 255);

        Atk2.text = "0";
        Atk2.color = Color.black;
        AtkI2.color = new Color32(255, 255, 255, 255);

        Atk3.text = "0";
        Atk3.color = Color.black;
        AtkI3.color = new Color32(255, 255, 255, 255);

        Atk4.text = "0";
        Atk4.color = Color.black;
        AtkI4.color = new Color32(255, 255, 255, 255);

        Atk5.text = "0";
        Atk5.color = Color.black;
        AtkI5.color = new Color32(255, 255, 255, 255);

        Atk6.text = "0";
        Atk6.color = Color.black;
        AtkI6.color = new Color32(255, 255, 255, 255);

        Atk7.text = "0";
        Atk7.color = Color.black;
        AtkI7.color = new Color32(255, 255, 255, 255);

        Atk8.text = "0";
        Atk8.color = Color.black;
        AtkI8.color = new Color32(255, 255, 255, 255);

        Atk9.text = "0";
        Atk9.color = Color.black;
        AtkI9.color = new Color32(255, 255, 255, 255);

        Atk10.text = "0";
        Atk10.color = Color.black;
        AtkI10.color = new Color32(255, 255, 255, 255);

        Atk11.text = "0";
        Atk11.color = Color.black;
        AtkI11.color = new Color32(255, 255, 255, 255);

        Atk12.text = "0";
        Atk12.color = Color.black;
        AtkI12.color = new Color32(255, 255, 255, 255);

        Def1.text = "0";
        Def1.color = Color.black;
        DefI1.color = new Color32(255, 255, 255, 255);

        Def2.text = "0";
        Def2.color = Color.black;
        DefI2.color = new Color32(255, 255, 255, 255);

        Def3.text = "0";
        Def3.color = Color.black;
        DefI3.color = new Color32(255, 255, 255, 255);

        Def4.text = "0";
        Def4.color = Color.black;
        DefI4.color = new Color32(255, 255, 255, 255);

        Def5.text = "0";
        Def5.color = Color.black;
        DefI5.color = new Color32(255, 255, 255, 255);

        Def6.text = "0";
        Def6.color = Color.black;
        DefI6.color = new Color32(255, 255, 255, 255);

        Def7.text = "0";
        Def7.color = Color.black;
        DefI7.color = new Color32(255, 255, 255, 255);

        Def8.text = "0";
        Def8.color = Color.black;
        DefI8.color = new Color32(255, 255, 255, 255);

        Def9.text = "0";
        Def9.color = Color.black;
        DefI9.color = new Color32(255, 255, 255, 255);

        Def10.text = "0";
        Def10.color = Color.black;
        DefI10.color = new Color32(255, 255, 255, 255);

        Def11.text = "0";
        Def11.color = Color.black;
        DefI11.color = new Color32(255, 255, 255, 255);

        Def12.text = "0";
        Def12.color = Color.black;
        DefI12.color = new Color32(255, 255, 255, 255);

        Dmg1.text = "0";
        Dmg1.color = Color.black;
        DmgI1.color = new Color32(255, 255, 255, 255);

        Dmg2.text = "0";
        Dmg2.color = Color.black;
        DmgI2.color = new Color32(255, 255, 255, 255);

        Dmg3.text = "0";
        Dmg3.color = Color.black;
        DmgI3.color = new Color32(255, 255, 255, 255);

        Dmg4.text = "0";
        Dmg4.color = Color.black;
        DmgI4.color = new Color32(255, 255, 255, 255);

        Dmg5.text = "0";
        Dmg5.color = Color.black;
        DmgI5.color = new Color32(255, 255, 255, 255);

        Dmg6.text = "0";
        Dmg6.color = Color.black;
        DmgI6.color = new Color32(255, 255, 255, 255);

        Dmg7.text = "0";
        Dmg7.color = Color.black;
        DmgI7.color = new Color32(255, 255, 255, 255);

        Dmg8.text = "0";
        Dmg8.color = Color.black;
        DmgI8.color = new Color32(255, 255, 255, 255);

        Dmg9.text = "0";
        Dmg9.color = Color.black;
        DmgI9.color = new Color32(255, 255, 255, 255);

        Dmg10.text = "0";
        Dmg10.color = Color.black;
        DmgI10.color = new Color32(255, 255, 255, 255);

        Dmg11.text = "0";
        Dmg11.color = Color.black;
        DmgI11.color = new Color32(255, 255, 255, 255);

        Dmg12.text = "0";
        Dmg12.color = Color.black;
        DmgI12.color = new Color32(255, 255, 255, 255);
        //

        if (DialLength <= 12 || (startingIndex + 11 < DialLength && startingIndex >= 0))
        {
            currentDialIndex = startingIndex;
            int indexOffset = SpeedValList.Count -1;

            if (indexOffset > 11)
            {
                indexOffset = 11;
            } 


            for (int i = currentDialIndex; i <= currentDialIndex + indexOffset; i++)
            {

                if (SpeedValList[i] == -1)
                {
                    valuesList.Add("KO");
                }
                else
                {
                    valuesList.Add(SpeedValList[i].ToString());
                }

                switch (SpeedPowList[i])
                {
                    case "Flurry":
                        
                        powersList.Add(new Color32(255, 17, 0, 255));
                        textColorList.Add(Color.white);
                        break;

                    case "Leap/Climb":
                        
                        powersList.Add(new Color32(255, 153, 0, 255));
                        textColorList.Add(Color.black);
                        break;

                    case "Phasing/Teleport":
                        
                        powersList.Add(new Color32(255, 255, 0, 255));
                        textColorList.Add(Color.black);
                        break;

                    case "Earthbound/Neutralized":
                        
                        powersList.Add(new Color32(0, 255, 51, 255));
                        textColorList.Add(Color.black);
                        break;

                    case "Charge":
                        
                        powersList.Add(new Color32(0, 153, 0, 255));
                        textColorList.Add(Color.black);
                        break;

                    case "Mind Control":
                        
                        powersList.Add(new Color32(179, 229, 252, 255));
                        textColorList.Add(Color.black);
                        break;

                    case "Plasticity":
                        
                        powersList.Add(new Color32(0, 51, 204, 255));
                        textColorList.Add(Color.white);
                        break;

                    case "Force Blast":
                        
                        powersList.Add(new Color32(102, 0, 204, 255));
                        textColorList.Add(Color.white);
                        break;

                    case "Sidestep":
                        
                        powersList.Add(new Color32(255, 153, 255, 255));
                        textColorList.Add(Color.black);
                        break;

                    case "Hypersonic Speed":
                        
                        powersList.Add(new Color32(191, 158, 27, 255));
                        textColorList.Add(Color.black);
                        break;

                    case "Stealth":
                        
                        powersList.Add(new Color32(0, 0, 0, 255));
                        textColorList.Add(Color.white);
                        break;

                    case "Running Shot":
                        
                        powersList.Add(new Color32(176, 190, 197, 255));
                        textColorList.Add(Color.white);
                        break;

                    case "None":
                        
                        powersList.Add(new Color32(255, 255, 255, 255));
                        textColorList.Add(Color.black);
                        break;

                    case "":

                        powersList.Add(new Color32(255, 255, 255, 255));
                        textColorList.Add(Color.black);
                        break;

                    default:
                        
                        powersList.Add(new Color32(255, 0, 235, 255));
                        textColorList.Add(Color.black);
                        break;
                }
            }

            if (slotsAdded == 0)
            {
                Spd1.text = "0";
                Spd1.color = Color.black;
                SpdI1.color = Color.white;

                Spd2.text = "0";
                Spd2.color = Color.black;
                SpdI2.color = Color.white;

                Spd3.text = "0";
                Spd3.color = Color.black;
                SpdI3.color = Color.white;

                Spd4.text = "0";
                Spd4.color = Color.black;
                SpdI4.color = Color.white;

                Spd5.text = "0";
                Spd5.color = Color.black;
                SpdI5.color = Color.white;

                Spd6.text = "0";
                Spd6.color = Color.black;
                SpdI6.color = Color.white;

                Spd7.text = "0";
                Spd7.color = Color.black;
                SpdI7.color = Color.white;

                Spd8.text = "0";
                Spd8.color = Color.black;
                SpdI8.color = Color.white;

                Spd9.text = "0";
                Spd9.color = Color.black;
                SpdI9.color = Color.white;

                Spd10.text = "0";
                Spd10.color = Color.black;
                SpdI10.color = Color.white;

                Spd11.text = "0";
                Spd11.color = Color.black;
                SpdI11.color = Color.white;

                Spd12.text = "0";
                Spd12.color = Color.black;
                SpdI12.color = Color.white;
            }

            if (slotsAdded == 1)
            { 
                Spd1.text = valuesList[0];
                Spd1.color = textColorList[0];
                SpdI1.color = powersList[0];
            }

            if (slotsAdded == 2)
            {
                Spd1.text = valuesList[0];
                Spd1.color = textColorList[0];
                SpdI1.color = powersList[0];

                Spd2.text = valuesList[1];
                Spd2.color = textColorList[1];
                SpdI2.color = powersList[1];
            }

            if (slotsAdded == 3)
            {
                Spd1.text = valuesList[0];
                Spd1.color = textColorList[0];
                SpdI1.color = powersList[0];

                Spd2.text = valuesList[1];
                Spd2.color = textColorList[1];
                SpdI2.color = powersList[1];

                Spd3.text = valuesList[2];
                Spd3.color = textColorList[2];
                SpdI3.color = powersList[2];
            }

            if (slotsAdded == 4)
            {
                Spd1.text = valuesList[0];
                Spd1.color = textColorList[0];
                SpdI1.color = powersList[0];

                Spd2.text = valuesList[1];
                Spd2.color = textColorList[1];
                SpdI2.color = powersList[1];

                Spd3.text = valuesList[2];
                Spd3.color = textColorList[2];
                SpdI3.color = powersList[2];

                Spd4.text = valuesList[3];
                Spd4.color = textColorList[3];
                SpdI4.color = powersList[3];
            }

            if (slotsAdded == 5)
            {
                Spd1.text = valuesList[0];
                Spd1.color = textColorList[0];
                SpdI1.color = powersList[0];

                Spd2.text = valuesList[1];
                Spd2.color = textColorList[1];
                SpdI2.color = powersList[1];

                Spd3.text = valuesList[2];
                Spd3.color = textColorList[2];
                SpdI3.color = powersList[2];

                Spd4.text = valuesList[3];
                Spd4.color = textColorList[3];
                SpdI4.color = powersList[3];

                Spd5.text = valuesList[4];
                Spd5.color = textColorList[4];
                SpdI5.color = powersList[4];
            }

            if (slotsAdded == 6)
            {
                Spd1.text = valuesList[0];
                Spd1.color = textColorList[0];
                SpdI1.color = powersList[0];

                Spd2.text = valuesList[1];
                Spd2.color = textColorList[1];
                SpdI2.color = powersList[1];

                Spd3.text = valuesList[2];
                Spd3.color = textColorList[2];
                SpdI3.color = powersList[2];

                Spd4.text = valuesList[3];
                Spd4.color = textColorList[3];
                SpdI4.color = powersList[3];

                Spd5.text = valuesList[4];
                Spd5.color = textColorList[4];
                SpdI5.color = powersList[4];

                Spd6.text = valuesList[5];
                Spd6.color = textColorList[5];
                SpdI6.color = powersList[5];
            }

            if (slotsAdded == 7)
            {
                Spd1.text = valuesList[0];
                Spd1.color = textColorList[0];
                SpdI1.color = powersList[0];

                Spd2.text = valuesList[1];
                Spd2.color = textColorList[1];
                SpdI2.color = powersList[1];

                Spd3.text = valuesList[2];
                Spd3.color = textColorList[2];
                SpdI3.color = powersList[2];

                Spd4.text = valuesList[3];
                Spd4.color = textColorList[3];
                SpdI4.color = powersList[3];

                Spd5.text = valuesList[4];
                Spd5.color = textColorList[4];
                SpdI5.color = powersList[4];

                Spd6.text = valuesList[5];
                Spd6.color = textColorList[5];
                SpdI6.color = powersList[5];

                Spd7.text = valuesList[6];
                Spd7.color = textColorList[6];
                SpdI7.color = powersList[6];
            }

            if (slotsAdded == 8)
            {
                Spd1.text = valuesList[0];
                Spd1.color = textColorList[0];
                SpdI1.color = powersList[0];

                Spd2.text = valuesList[1];
                Spd2.color = textColorList[1];
                SpdI2.color = powersList[1];

                Spd3.text = valuesList[2];
                Spd3.color = textColorList[2];
                SpdI3.color = powersList[2];

                Spd4.text = valuesList[3];
                Spd4.color = textColorList[3];
                SpdI4.color = powersList[3];

                Spd5.text = valuesList[4];
                Spd5.color = textColorList[4];
                SpdI5.color = powersList[4];

                Spd6.text = valuesList[5];
                Spd6.color = textColorList[5];
                SpdI6.color = powersList[5];

                Spd7.text = valuesList[6];
                Spd7.color = textColorList[6];
                SpdI7.color = powersList[6];

                Spd8.text = valuesList[7];
                Spd8.color = textColorList[7];
                SpdI8.color = powersList[7];
            }

            if (slotsAdded == 9)
            {
                Spd1.text = valuesList[0];
                Spd1.color = textColorList[0];
                SpdI1.color = powersList[0];

                Spd2.text = valuesList[1];
                Spd2.color = textColorList[1];
                SpdI2.color = powersList[1];

                Spd3.text = valuesList[2];
                Spd3.color = textColorList[2];
                SpdI3.color = powersList[2];

                Spd4.text = valuesList[3];
                Spd4.color = textColorList[3];
                SpdI4.color = powersList[3];

                Spd5.text = valuesList[4];
                Spd5.color = textColorList[4];
                SpdI5.color = powersList[4];

                Spd6.text = valuesList[5];
                Spd6.color = textColorList[5];
                SpdI6.color = powersList[5];

                Spd7.text = valuesList[6];
                Spd7.color = textColorList[6];
                SpdI7.color = powersList[6];

                Spd8.text = valuesList[7];
                Spd8.color = textColorList[7];
                SpdI8.color = powersList[7];

                Spd9.text = valuesList[8];
                Spd9.color = textColorList[8];
                SpdI9.color = powersList[8];
            }

            if (slotsAdded == 10)
            {
                Spd1.text = valuesList[0];
                Spd1.color = textColorList[0];
                SpdI1.color = powersList[0];

                Spd2.text = valuesList[1];
                Spd2.color = textColorList[1];
                SpdI2.color = powersList[1];

                Spd3.text = valuesList[2];
                Spd3.color = textColorList[2];
                SpdI3.color = powersList[2];

                Spd4.text = valuesList[3];
                Spd4.color = textColorList[3];
                SpdI4.color = powersList[3];

                Spd5.text = valuesList[4];
                Spd5.color = textColorList[4];
                SpdI5.color = powersList[4];

                Spd6.text = valuesList[5];
                Spd6.color = textColorList[5];
                SpdI6.color = powersList[5];

                Spd7.text = valuesList[6];
                Spd7.color = textColorList[6];
                SpdI7.color = powersList[6];

                Spd8.text = valuesList[7];
                Spd8.color = textColorList[7];
                SpdI8.color = powersList[7];

                Spd9.text = valuesList[8];
                Spd9.color = textColorList[8];
                SpdI9.color = powersList[8];

                Spd10.text = valuesList[9];
                Spd10.color = textColorList[9];
                SpdI10.color = powersList[9];
            }

            if (slotsAdded == 11)
            {
                Spd1.text = valuesList[0];
                Spd1.color = textColorList[0];
                SpdI1.color = powersList[0];

                Spd2.text = valuesList[1];
                Spd2.color = textColorList[1];
                SpdI2.color = powersList[1];

                Spd3.text = valuesList[2];
                Spd3.color = textColorList[2];
                SpdI3.color = powersList[2];

                Spd4.text = valuesList[3];
                Spd4.color = textColorList[3];
                SpdI4.color = powersList[3];

                Spd5.text = valuesList[4];
                Spd5.color = textColorList[4];
                SpdI5.color = powersList[4];

                Spd6.text = valuesList[5];
                Spd6.color = textColorList[5];
                SpdI6.color = powersList[5];

                Spd7.text = valuesList[6];
                Spd7.color = textColorList[6];
                SpdI7.color = powersList[6];

                Spd8.text = valuesList[7];
                Spd8.color = textColorList[7];
                SpdI8.color = powersList[7];

                Spd9.text = valuesList[8];
                Spd9.color = textColorList[8];
                SpdI9.color = powersList[8];

                Spd10.text = valuesList[9];
                Spd10.color = textColorList[9];
                SpdI10.color = powersList[9];

                Spd11.text = valuesList[10];
                Spd11.color = textColorList[10];
                SpdI11.color = powersList[10];
            }

            if (slotsAdded >= 12)
            {
                Spd1.text = valuesList[0];
                Spd1.color = textColorList[0];
                SpdI1.color = powersList[0];

                Spd2.text = valuesList[1];
                Spd2.color = textColorList[1];
                SpdI2.color = powersList[1];

                Spd3.text = valuesList[2];
                Spd3.color = textColorList[2];
                SpdI3.color = powersList[2];

                Spd4.text = valuesList[3];
                Spd4.color = textColorList[3];
                SpdI4.color = powersList[3];

                Spd5.text = valuesList[4];
                Spd5.color = textColorList[4];
                SpdI5.color = powersList[4];

                Spd6.text = valuesList[5];
                Spd6.color = textColorList[5];
                SpdI6.color = powersList[5];

                Spd7.text = valuesList[6];
                Spd7.color = textColorList[6];
                SpdI7.color = powersList[6];

                Spd8.text = valuesList[7];
                Spd8.color = textColorList[7];
                SpdI8.color = powersList[7];

                Spd9.text = valuesList[8];
                Spd9.color = textColorList[8];
                SpdI9.color = powersList[8];

                Spd10.text = valuesList[9];
                Spd10.color = textColorList[9];
                SpdI10.color = powersList[9];

                Spd11.text = valuesList[10];
                Spd11.color = textColorList[10];
                SpdI11.color = powersList[10];

                Spd12.text = valuesList[11];
                Spd12.color = textColorList[11];
                SpdI12.color = powersList[11];
            }


            valuesList.Clear();
            powersList.Clear();
            textColorList.Clear();
            currentDialIndex = startingIndex;

           
            for (int i = currentDialIndex; i <= currentDialIndex + indexOffset; i++)
            {
                if (AttackValList[i] == -1)
                {
                    valuesList.Add("KO");
                }
                else
                {
                    valuesList.Add(AttackValList[i].ToString());
                }

                switch (AttackPowList[i])
                {
                    case "Blades/Claws/Fangs":

                        powersList.Add(new Color32(255, 17, 0, 255));
                        textColorList.Add(Color.white);
                        break;

                    case "Energy Explosion":

                        powersList.Add(new Color32(255, 153, 0, 255));
                        textColorList.Add(Color.black);
                        break;

                    case "Pulse Wave":

                        powersList.Add(new Color32(255, 255, 0, 255));
                        textColorList.Add(Color.black);
                        break;

                    case "Quake":

                        powersList.Add(new Color32(0, 255, 51, 255));
                        textColorList.Add(Color.black);
                        break;

                    case "Super Strength":

                        powersList.Add(new Color32(0, 153, 0, 255));
                        textColorList.Add(Color.black);
                        break;

                    case "Incapacitate":

                        powersList.Add(new Color32(179, 229, 252, 255));
                        textColorList.Add(Color.black);
                        break;

                    case "Penetrating/Psychic Blast":

                        powersList.Add(new Color32(0, 51, 204, 255));
                        textColorList.Add(Color.white);
                        break;

                    case "Smoke Cloud":

                        powersList.Add(new Color32(102, 0, 204, 255));
                        textColorList.Add(Color.white);
                        break;

                    case "Precision Strike":

                        powersList.Add(new Color32(255, 153, 255, 255));
                        textColorList.Add(Color.black);
                        break;

                    case "Poison":

                        powersList.Add(new Color32(191, 158, 27, 255));
                        textColorList.Add(Color.black);
                        break;

                    case "Steal Energy":

                        powersList.Add(new Color32(0, 0, 0, 255));
                        textColorList.Add(Color.white);
                        break;

                    case "Telekinesis":

                        powersList.Add(new Color32(176, 190, 197, 255));
                        textColorList.Add(Color.white);
                        break;

                    case "None":

                        powersList.Add(new Color32(255, 255, 255, 255));
                        textColorList.Add(Color.black);
                        break;

                    case "":

                        powersList.Add(new Color32(255, 255, 255, 255));
                        textColorList.Add(Color.black);
                        break;

                    default:

                        powersList.Add(new Color32(255, 0, 235, 255));
                        textColorList.Add(Color.black);
                        break;
                }
            }

            if (slotsAdded == 0)
            {
                Atk1.text = "0";
                Atk1.color = Color.black;
                AtkI1.color = Color.white;

                Atk2.text = "0";
                Atk2.color = Color.black;
                AtkI2.color = Color.white;

                Atk3.text = "0";
                Atk3.color = Color.black;
                AtkI3.color = Color.white;

                Atk4.text = "0";
                Atk4.color = Color.black;
                AtkI4.color = Color.white;

                Atk5.text = "0";
                Atk5.color = Color.black;
                AtkI5.color = Color.white;

                Atk6.text = "0";
                Atk6.color = Color.black;
                AtkI6.color = Color.white;

                Atk7.text = "0";
                Atk7.color = Color.black;
                AtkI7.color = Color.white;

                Atk8.text = "0";
                Atk8.color = Color.black;
                AtkI8.color = Color.white;

                Atk9.text = "0";
                Atk9.color = Color.black;
                AtkI9.color = Color.white;

                Atk10.text = "0";
                Atk10.color = Color.black;
                AtkI10.color = Color.white;

                Atk11.text = "0";
                Atk11.color = Color.black;
                AtkI11.color = Color.white;

                Atk12.text = "0";
                Atk12.color = Color.black;
                AtkI12.color = Color.white;
            }

            if (slotsAdded == 1)
            {
                Atk1.text = valuesList[0];
                Atk1.color = textColorList[0];
                AtkI1.color = powersList[0];
            }

            if (slotsAdded == 2)
            {
                Atk1.text = valuesList[0];
                Atk1.color = textColorList[0];
                AtkI1.color = powersList[0];

                Atk2.text = valuesList[1];
                Atk2.color = textColorList[1];
                AtkI2.color = powersList[1];
            }

            if (slotsAdded == 3)
            {
                Atk1.text = valuesList[0];
                Atk1.color = textColorList[0];
                AtkI1.color = powersList[0];

                Atk2.text = valuesList[1];
                Atk2.color = textColorList[1];
                AtkI2.color = powersList[1];

                Atk3.text = valuesList[2];
                Atk3.color = textColorList[2];
                AtkI3.color = powersList[2];
            }

            if (slotsAdded == 4)
            {
                Atk1.text = valuesList[0];
                Atk1.color = textColorList[0];
                AtkI1.color = powersList[0];

                Atk2.text = valuesList[1];
                Atk2.color = textColorList[1];
                AtkI2.color = powersList[1];

                Atk3.text = valuesList[2];
                Atk3.color = textColorList[2];
                AtkI3.color = powersList[2];

                Atk4.text = valuesList[3];
                Atk4.color = textColorList[3];
                AtkI4.color = powersList[3];
            }

            if (slotsAdded == 5)
            {
                Atk1.text = valuesList[0];
                Atk1.color = textColorList[0];
                AtkI1.color = powersList[0];

                Atk2.text = valuesList[1];
                Atk2.color = textColorList[1];
                AtkI2.color = powersList[1];

                Atk3.text = valuesList[2];
                Atk3.color = textColorList[2];
                AtkI3.color = powersList[2];

                Atk4.text = valuesList[3];
                Atk4.color = textColorList[3];
                AtkI4.color = powersList[3];

                Atk5.text = valuesList[4];
                Atk5.color = textColorList[4];
                AtkI5.color = powersList[4];
            }

            if (slotsAdded == 6)
            {
                Atk1.text = valuesList[0];
                Atk1.color = textColorList[0];
                AtkI1.color = powersList[0];

                Atk2.text = valuesList[1];
                Atk2.color = textColorList[1];
                AtkI2.color = powersList[1];

                Atk3.text = valuesList[2];
                Atk3.color = textColorList[2];
                AtkI3.color = powersList[2];

                Atk4.text = valuesList[3];
                Atk4.color = textColorList[3];
                AtkI4.color = powersList[3];

                Atk5.text = valuesList[4];
                Atk5.color = textColorList[4];
                AtkI5.color = powersList[4];

                Atk6.text = valuesList[5];
                Atk6.color = textColorList[5];
                AtkI6.color = powersList[5];
            }

            if (slotsAdded == 7)
            {
                Atk1.text = valuesList[0];
                Atk1.color = textColorList[0];
                AtkI1.color = powersList[0];

                Atk2.text = valuesList[1];
                Atk2.color = textColorList[1];
                AtkI2.color = powersList[1];

                Atk3.text = valuesList[2];
                Atk3.color = textColorList[2];
                AtkI3.color = powersList[2];

                Atk4.text = valuesList[3];
                Atk4.color = textColorList[3];
                AtkI4.color = powersList[3];

                Atk5.text = valuesList[4];
                Atk5.color = textColorList[4];
                AtkI5.color = powersList[4];

                Atk6.text = valuesList[5];
                Atk6.color = textColorList[5];
                AtkI6.color = powersList[5];

                Atk7.text = valuesList[6];
                Atk7.color = textColorList[6];
                AtkI7.color = powersList[6];
            }

            if (slotsAdded == 8)
            {
                Atk1.text = valuesList[0];
                Atk1.color = textColorList[0];
                AtkI1.color = powersList[0];

                Atk2.text = valuesList[1];
                Atk2.color = textColorList[1];
                AtkI2.color = powersList[1];

                Atk3.text = valuesList[2];
                Atk3.color = textColorList[2];
                AtkI3.color = powersList[2];

                Atk4.text = valuesList[3];
                Atk4.color = textColorList[3];
                AtkI4.color = powersList[3];

                Atk5.text = valuesList[4];
                Atk5.color = textColorList[4];
                AtkI5.color = powersList[4];

                Atk6.text = valuesList[5];
                Atk6.color = textColorList[5];
                AtkI6.color = powersList[5];

                Atk7.text = valuesList[6];
                Atk7.color = textColorList[6];
                AtkI7.color = powersList[6];

                Atk8.text = valuesList[7];
                Atk8.color = textColorList[7];
                AtkI8.color = powersList[7];
            }

            if (slotsAdded == 9)
            {
                Atk1.text = valuesList[0];
                Atk1.color = textColorList[0];
                AtkI1.color = powersList[0];

                Atk2.text = valuesList[1];
                Atk2.color = textColorList[1];
                AtkI2.color = powersList[1];

                Atk3.text = valuesList[2];
                Atk3.color = textColorList[2];
                AtkI3.color = powersList[2];

                Atk4.text = valuesList[3];
                Atk4.color = textColorList[3];
                AtkI4.color = powersList[3];

                Atk5.text = valuesList[4];
                Atk5.color = textColorList[4];
                AtkI5.color = powersList[4];

                Atk6.text = valuesList[5];
                Atk6.color = textColorList[5];
                AtkI6.color = powersList[5];

                Atk7.text = valuesList[6];
                Atk7.color = textColorList[6];
                AtkI7.color = powersList[6];

                Atk8.text = valuesList[7];
                Atk8.color = textColorList[7];
                AtkI8.color = powersList[7];

                Atk9.text = valuesList[8];
                Atk9.color = textColorList[8];
                AtkI9.color = powersList[8];
            }

            if (slotsAdded == 10)
            {
                Atk1.text = valuesList[0];
                Atk1.color = textColorList[0];
                AtkI1.color = powersList[0];

                Atk2.text = valuesList[1];
                Atk2.color = textColorList[1];
                AtkI2.color = powersList[1];

                Atk3.text = valuesList[2];
                Atk3.color = textColorList[2];
                AtkI3.color = powersList[2];

                Atk4.text = valuesList[3];
                Atk4.color = textColorList[3];
                AtkI4.color = powersList[3];

                Atk5.text = valuesList[4];
                Atk5.color = textColorList[4];
                AtkI5.color = powersList[4];

                Atk6.text = valuesList[5];
                Atk6.color = textColorList[5];
                AtkI6.color = powersList[5];

                Atk7.text = valuesList[6];
                Atk7.color = textColorList[6];
                AtkI7.color = powersList[6];

                Atk8.text = valuesList[7];
                Atk8.color = textColorList[7];
                AtkI8.color = powersList[7];

                Atk9.text = valuesList[8];
                Atk9.color = textColorList[8];
                AtkI9.color = powersList[8];

                Atk10.text = valuesList[9];
                Atk10.color = textColorList[9];
                AtkI10.color = powersList[9];
            }

            if (slotsAdded == 11)
            {
                Atk1.text = valuesList[0];
                Atk1.color = textColorList[0];
                AtkI1.color = powersList[0];

                Atk2.text = valuesList[1];
                Atk2.color = textColorList[1];
                AtkI2.color = powersList[1];

                Atk3.text = valuesList[2];
                Atk3.color = textColorList[2];
                AtkI3.color = powersList[2];

                Atk4.text = valuesList[3];
                Atk4.color = textColorList[3];
                AtkI4.color = powersList[3];

                Atk5.text = valuesList[4];
                Atk5.color = textColorList[4];
                AtkI5.color = powersList[4];

                Atk6.text = valuesList[5];
                Atk6.color = textColorList[5];
                AtkI6.color = powersList[5];

                Atk7.text = valuesList[6];
                Atk7.color = textColorList[6];
                AtkI7.color = powersList[6];

                Atk8.text = valuesList[7];
                Atk8.color = textColorList[7];
                AtkI8.color = powersList[7];

                Atk9.text = valuesList[8];
                Atk9.color = textColorList[8];
                AtkI9.color = powersList[8];

                Atk10.text = valuesList[9];
                Atk10.color = textColorList[9];
                AtkI10.color = powersList[9];

                Atk11.text = valuesList[10];
                Atk11.color = textColorList[10];
                AtkI11.color = powersList[10];
            }

            if (slotsAdded >= 12)
            {
                Atk1.text = valuesList[0];
                Atk1.color = textColorList[0];
                AtkI1.color = powersList[0];

                Atk2.text = valuesList[1];
                Atk2.color = textColorList[1];
                AtkI2.color = powersList[1];

                Atk3.text = valuesList[2];
                Atk3.color = textColorList[2];
                AtkI3.color = powersList[2];

                Atk4.text = valuesList[3];
                Atk4.color = textColorList[3];
                AtkI4.color = powersList[3];

                Atk5.text = valuesList[4];
                Atk5.color = textColorList[4];
                AtkI5.color = powersList[4];

                Atk6.text = valuesList[5];
                Atk6.color = textColorList[5];
                AtkI6.color = powersList[5];

                Atk7.text = valuesList[6];
                Atk7.color = textColorList[6];
                AtkI7.color = powersList[6];

                Atk8.text = valuesList[7];
                Atk8.color = textColorList[7];
                AtkI8.color = powersList[7];

                Atk9.text = valuesList[8];
                Atk9.color = textColorList[8];
                AtkI9.color = powersList[8];

                Atk10.text = valuesList[9];
                Atk10.color = textColorList[9];
                AtkI10.color = powersList[9];

                Atk11.text = valuesList[10];
                Atk11.color = textColorList[10];
                AtkI11.color = powersList[10];

                Atk12.text = valuesList[11];
                Atk12.color = textColorList[11];
                AtkI12.color = powersList[11];
            }


            valuesList.Clear();
            powersList.Clear();
            textColorList.Clear();
            currentDialIndex = startingIndex;

            for (int i = currentDialIndex; i <= currentDialIndex + indexOffset; i++)
            {
                if (DefenseValList[i] == -1)
                {
                    valuesList.Add("KO");
                }
                else
                {
                    valuesList.Add(DefenseValList[i].ToString());
                }

                switch (DefensePowList[i])
                {
                    case "Super Senses":
                        
                        powersList.Add(new Color32(255, 17, 0, 255));
                        textColorList.Add(Color.white);
                        break;

                    case "Toughness":
                        
                        powersList.Add(new Color32(255, 153, 0, 255));
                        textColorList.Add(Color.black);
                        break;

                    case "Defend":
                        
                        powersList.Add(new Color32(255, 255, 0, 255));
                        textColorList.Add(Color.black);
                        break;

                    case "Combat Reflexes":
                        
                        powersList.Add(new Color32(0, 255, 51, 255));
                        textColorList.Add(Color.black);
                        break;

                    case "Energy Shield/Deflection":
                        
                        powersList.Add(new Color32(0, 153, 0, 255));
                        textColorList.Add(Color.black);
                        break;

                    case "Barrier":
                        
                        powersList.Add(new Color32(179, 229, 252, 255));
                        textColorList.Add(Color.black);
                        break;

                    case "Mastermind":
                        
                        powersList.Add(new Color32(0, 51, 204, 255));
                        textColorList.Add(Color.white);
                        break;

                    case "Willpower":
                        
                        powersList.Add(new Color32(102, 0, 204, 255));
                        textColorList.Add(Color.white);
                        break;

                    case "Invincible":
                        
                        powersList.Add(new Color32(255, 153, 255, 255));
                        textColorList.Add(Color.black);
                        break;

                    case "Impervious":
                        
                        powersList.Add(new Color32(191, 158, 27, 255));
                        textColorList.Add(Color.black);
                        break;

                    case "Regeneration":
                        
                        powersList.Add(new Color32(0, 0, 0, 255));
                        textColorList.Add(Color.white);
                        break;

                    case "Invulnerability":
                        
                        powersList.Add(new Color32(176, 190, 197, 255));
                        textColorList.Add(Color.white);
                        break;

                    case "None":

                        powersList.Add(new Color32(255, 255, 255, 255));
                        textColorList.Add(Color.black);
                        break;

                    case "":

                        powersList.Add(new Color32(255, 255, 255, 255));
                        textColorList.Add(Color.black);
                        break;

                    default:

                        powersList.Add(new Color32(255, 0, 235, 255));
                        textColorList.Add(Color.black);
                        break;
                }
            }

            if (slotsAdded == 0)
            {
                Def1.text = "0";
                Def1.color = Color.black;
                DefI1.color = Color.white;

                Def2.text = "0";
                Def2.color = Color.black;
                DefI2.color = Color.white;

                Def3.text = "0";
                Def3.color = Color.black;
                DefI3.color = Color.white;

                Def4.text = "0";
                Def4.color = Color.black;
                DefI4.color = Color.white;

                Def5.text = "0";
                Def5.color = Color.black;
                DefI5.color = Color.white;

                Def6.text = "0";
                Def6.color = Color.black;
                DefI6.color = Color.white;

                Def7.text = "0";
                Def7.color = Color.black;
                DefI7.color = Color.white;

                Def8.text = "0";
                Def8.color = Color.black;
                DefI8.color = Color.white;

                Def9.text = "0";
                Def9.color = Color.black;
                DefI9.color = Color.white;

                Def10.text = "0";
                Def10.color = Color.black;
                DefI10.color = Color.white;

                Def11.text = "0";
                Def11.color = Color.black;
                DefI11.color = Color.white;

                Def12.text = "0";
                Def12.color = Color.black;
                DefI12.color = Color.white;
            }

            if (slotsAdded == 1)
            {
                Def1.text = valuesList[0];
                Def1.color = textColorList[0];
                DefI1.color = powersList[0];
            }

            if (slotsAdded == 2)
            {
                Def1.text = valuesList[0];
                Def1.color = textColorList[0];
                DefI1.color = powersList[0];

                Def2.text = valuesList[1];
                Def2.color = textColorList[1];
                DefI2.color = powersList[1];
            }

            if (slotsAdded == 3)
            {
                Def1.text = valuesList[0];
                Def1.color = textColorList[0];
                DefI1.color = powersList[0];

                Def2.text = valuesList[1];
                Def2.color = textColorList[1];
                DefI2.color = powersList[1];

                Def3.text = valuesList[2];
                Def3.color = textColorList[2];
                DefI3.color = powersList[2];
            }

            if (slotsAdded == 4)
            {
                Def1.text = valuesList[0];
                Def1.color = textColorList[0];
                DefI1.color = powersList[0];

                Def2.text = valuesList[1];
                Def2.color = textColorList[1];
                DefI2.color = powersList[1];

                Def3.text = valuesList[2];
                Def3.color = textColorList[2];
                DefI3.color = powersList[2];

                Def4.text = valuesList[3];
                Def4.color = textColorList[3];
                DefI4.color = powersList[3];
            }

            if (slotsAdded == 5)
            {
                Def1.text = valuesList[0];
                Def1.color = textColorList[0];
                DefI1.color = powersList[0];

                Def2.text = valuesList[1];
                Def2.color = textColorList[1];
                DefI2.color = powersList[1];

                Def3.text = valuesList[2];
                Def3.color = textColorList[2];
                DefI3.color = powersList[2];

                Def4.text = valuesList[3];
                Def4.color = textColorList[3];
                DefI4.color = powersList[3];

                Def5.text = valuesList[4];
                Def5.color = textColorList[4];
                DefI5.color = powersList[4];
            }

            if (slotsAdded == 6)
            {
                Def1.text = valuesList[0];
                Def1.color = textColorList[0];
                DefI1.color = powersList[0];

                Def2.text = valuesList[1];
                Def2.color = textColorList[1];
                DefI2.color = powersList[1];

                Def3.text = valuesList[2];
                Def3.color = textColorList[2];
                DefI3.color = powersList[2];

                Def4.text = valuesList[3];
                Def4.color = textColorList[3];
                DefI4.color = powersList[3];

                Def5.text = valuesList[4];
                Def5.color = textColorList[4];
                DefI5.color = powersList[4];

                Def6.text = valuesList[5];
                Def6.color = textColorList[5];
                DefI6.color = powersList[5];
            }

            if (slotsAdded == 7)
            {
                Def1.text = valuesList[0];
                Def1.color = textColorList[0];
                DefI1.color = powersList[0];

                Def2.text = valuesList[1];
                Def2.color = textColorList[1];
                DefI2.color = powersList[1];

                Def3.text = valuesList[2];
                Def3.color = textColorList[2];
                DefI3.color = powersList[2];

                Def4.text = valuesList[3];
                Def4.color = textColorList[3];
                DefI4.color = powersList[3];

                Def5.text = valuesList[4];
                Def5.color = textColorList[4];
                DefI5.color = powersList[4];

                Def6.text = valuesList[5];
                Def6.color = textColorList[5];
                DefI6.color = powersList[5];

                Def7.text = valuesList[6];
                Def7.color = textColorList[6];
                DefI7.color = powersList[6];
            }

            if (slotsAdded == 8)
            {
                Def1.text = valuesList[0];
                Def1.color = textColorList[0];
                DefI1.color = powersList[0];

                Def2.text = valuesList[1];
                Def2.color = textColorList[1];
                DefI2.color = powersList[1];

                Def3.text = valuesList[2];
                Def3.color = textColorList[2];
                DefI3.color = powersList[2];

                Def4.text = valuesList[3];
                Def4.color = textColorList[3];
                DefI4.color = powersList[3];

                Def5.text = valuesList[4];
                Def5.color = textColorList[4];
                DefI5.color = powersList[4];

                Def6.text = valuesList[5];
                Def6.color = textColorList[5];
                DefI6.color = powersList[5];

                Def7.text = valuesList[6];
                Def7.color = textColorList[6];
                DefI7.color = powersList[6];

                Def8.text = valuesList[7];
                Def8.color = textColorList[7];
                DefI8.color = powersList[7];
            }

            if (slotsAdded == 9)
            {
                Def1.text = valuesList[0];
                Def1.color = textColorList[0];
                DefI1.color = powersList[0];

                Def2.text = valuesList[1];
                Def2.color = textColorList[1];
                DefI2.color = powersList[1];

                Def3.text = valuesList[2];
                Def3.color = textColorList[2];
                DefI3.color = powersList[2];

                Def4.text = valuesList[3];
                Def4.color = textColorList[3];
                DefI4.color = powersList[3];

                Def5.text = valuesList[4];
                Def5.color = textColorList[4];
                DefI5.color = powersList[4];

                Def6.text = valuesList[5];
                Def6.color = textColorList[5];
                DefI6.color = powersList[5];

                Def7.text = valuesList[6];
                Def7.color = textColorList[6];
                DefI7.color = powersList[6];

                Def8.text = valuesList[7];
                Def8.color = textColorList[7];
                DefI8.color = powersList[7];

                Def9.text = valuesList[8];
                Def9.color = textColorList[8];
                DefI9.color = powersList[8];
            }

            if (slotsAdded == 10)
            {
                Def1.text = valuesList[0];
                Def1.color = textColorList[0];
                DefI1.color = powersList[0];

                Def2.text = valuesList[1];
                Def2.color = textColorList[1];
                DefI2.color = powersList[1];

                Def3.text = valuesList[2];
                Def3.color = textColorList[2];
                DefI3.color = powersList[2];

                Def4.text = valuesList[3];
                Def4.color = textColorList[3];
                DefI4.color = powersList[3];

                Def5.text = valuesList[4];
                Def5.color = textColorList[4];
                DefI5.color = powersList[4];

                Def6.text = valuesList[5];
                Def6.color = textColorList[5];
                DefI6.color = powersList[5];

                Def7.text = valuesList[6];
                Def7.color = textColorList[6];
                DefI7.color = powersList[6];

                Def8.text = valuesList[7];
                Def8.color = textColorList[7];
                DefI8.color = powersList[7];

                Def9.text = valuesList[8];
                Def9.color = textColorList[8];
                DefI9.color = powersList[8];

                Def10.text = valuesList[9];
                Def10.color = textColorList[9];
                DefI10.color = powersList[9];
            }

            if (slotsAdded == 11)
            {
                Def1.text = valuesList[0];
                Def1.color = textColorList[0];
                DefI1.color = powersList[0];

                Def2.text = valuesList[1];
                Def2.color = textColorList[1];
                DefI2.color = powersList[1];

                Def3.text = valuesList[2];
                Def3.color = textColorList[2];
                DefI3.color = powersList[2];

                Def4.text = valuesList[3];
                Def4.color = textColorList[3];
                DefI4.color = powersList[3];

                Def5.text = valuesList[4];
                Def5.color = textColorList[4];
                DefI5.color = powersList[4];

                Def6.text = valuesList[5];
                Def6.color = textColorList[5];
                DefI6.color = powersList[5];

                Def7.text = valuesList[6];
                Def7.color = textColorList[6];
                DefI7.color = powersList[6];

                Def8.text = valuesList[7];
                Def8.color = textColorList[7];
                DefI8.color = powersList[7];

                Def9.text = valuesList[8];
                Def9.color = textColorList[8];
                DefI9.color = powersList[8];

                Def10.text = valuesList[9];
                Def10.color = textColorList[9];
                DefI10.color = powersList[9];

                Def11.text = valuesList[10];
                Def11.color = textColorList[10];
                DefI11.color = powersList[10];
            }

            if (slotsAdded >= 12)
            {
                Def1.text = valuesList[0];
                Def1.color = textColorList[0];
                DefI1.color = powersList[0];

                Def2.text = valuesList[1];
                Def2.color = textColorList[1];
                DefI2.color = powersList[1];

                Def3.text = valuesList[2];
                Def3.color = textColorList[2];
                DefI3.color = powersList[2];

                Def4.text = valuesList[3];
                Def4.color = textColorList[3];
                DefI4.color = powersList[3];

                Def5.text = valuesList[4];
                Def5.color = textColorList[4];
                DefI5.color = powersList[4];

                Def6.text = valuesList[5];
                Def6.color = textColorList[5];
                DefI6.color = powersList[5];

                Def7.text = valuesList[6];
                Def7.color = textColorList[6];
                DefI7.color = powersList[6];

                Def8.text = valuesList[7];
                Def8.color = textColorList[7];
                DefI8.color = powersList[7];

                Def9.text = valuesList[8];
                Def9.color = textColorList[8];
                DefI9.color = powersList[8];

                Def10.text = valuesList[9];
                Def10.color = textColorList[9];
                DefI10.color = powersList[9];

                Def11.text = valuesList[10];
                Def11.color = textColorList[10];
                DefI11.color = powersList[10];

                Def12.text = valuesList[11];
                Def12.color = textColorList[11];
                DefI12.color = powersList[11];
            }


            valuesList.Clear();
            powersList.Clear();
            textColorList.Clear();
            currentDialIndex = startingIndex;

            for (int i = currentDialIndex; i <= currentDialIndex + indexOffset; i++)
            {
                if (DamageValList[i] == -1)
                {
                    valuesList.Add("KO");
                }
                else
                {
                    valuesList.Add(DamageValList[i].ToString());
                }

                switch (DamagePowList[i])
                {
                    case "Ranged Combat Expert":
                        
                        powersList.Add(new Color32(255, 17, 0, 255));
                        textColorList.Add(Color.white);
                        break;

                    case "Battle Fury":
                        
                        powersList.Add(new Color32(255, 153, 0, 255));
                        textColorList.Add(Color.black);
                        break;

                    case "Support":
                        
                        powersList.Add(new Color32(255, 255, 0, 255));
                        textColorList.Add(Color.black);
                        break;

                    case "Exploit Weakness":
                        
                        powersList.Add(new Color32(0, 255, 51, 255));
                        textColorList.Add(Color.black);
                        break;

                    case "Enhancement":
                        
                        powersList.Add(new Color32(0, 153, 0, 255));
                        textColorList.Add(Color.black);
                        break;

                    case "Probability Control":
                        
                        powersList.Add(new Color32(179, 229, 252, 255));
                        textColorList.Add(Color.black);
                        break;

                    case "Shape Change":
                        
                        powersList.Add(new Color32(0, 51, 204, 255));
                        textColorList.Add(Color.white);
                        break;

                    case "Close Combat Expert":
                        
                        powersList.Add(new Color32(102, 0, 204, 255));
                        textColorList.Add(Color.white);
                        break;

                    case "Empower":
                        
                        powersList.Add(new Color32(255, 153, 255, 255));
                        textColorList.Add(Color.black);
                        break;

                    case "Perplex":
                        
                        powersList.Add(new Color32(191, 158, 27, 255));
                        textColorList.Add(Color.black);
                        break;

                    case "Outwit":
                        
                        powersList.Add(new Color32(0, 0, 0, 255));
                        textColorList.Add(Color.white);
                        break;

                    case "Leadership":
                        
                        powersList.Add(new Color32(176, 190, 197, 255));
                        textColorList.Add(Color.white);
                        break;

                    case "None":

                        powersList.Add(new Color32(255, 255, 255, 255));
                        textColorList.Add(Color.black);
                        break;

                    case "":

                        powersList.Add(new Color32(255, 255, 255, 255));
                        textColorList.Add(Color.black);
                        break;

                    default:

                        powersList.Add(new Color32(255, 0, 235, 255));
                        textColorList.Add(Color.black);
                        break;
                }
            }

            if (slotsAdded == 0)
            {
                Dmg1.text = "0";
                Dmg1.color = Color.black;
                DmgI1.color = Color.white;

                Dmg2.text = "0";
                Dmg2.color = Color.black;
                DmgI2.color = Color.white;

                Dmg3.text = "0";
                Dmg3.color = Color.black;
                DmgI3.color = Color.white;

                Dmg4.text = "0";
                Dmg4.color = Color.black;
                DmgI4.color = Color.white;

                Dmg5.text = "0";
                Dmg5.color = Color.black;
                DmgI5.color = Color.white;

                Dmg6.text = "0";
                Dmg6.color = Color.black;
                DmgI6.color = Color.white;

                Dmg7.text = "0";
                Dmg7.color = Color.black;
                DmgI7.color = Color.white;

                Dmg8.text = "0";
                Dmg8.color = Color.black;
                DmgI8.color = Color.white;

                Dmg9.text = "0";
                Dmg9.color = Color.black;
                DmgI9.color = Color.white;

                Dmg10.text = "0";
                Dmg10.color = Color.black;
                DmgI10.color = Color.white;

                Dmg11.text = "0";
                Dmg11.color = Color.black;
                DmgI11.color = Color.white;

                Dmg12.text = "0";
                Dmg12.color = Color.black;
                DmgI12.color = Color.white;
            }

            if (slotsAdded == 1)
            {
                Dmg1.text = valuesList[0];
                Dmg1.color = textColorList[0];
                DmgI1.color = powersList[0];
            }

            if (slotsAdded == 2)
            {
                Dmg1.text = valuesList[0];
                Dmg1.color = textColorList[0];
                DmgI1.color = powersList[0];

                Dmg2.text = valuesList[1];
                Dmg2.color = textColorList[1];
                DmgI2.color = powersList[1];
            }

            if (slotsAdded == 3)
            {
                Dmg1.text = valuesList[0];
                Dmg1.color = textColorList[0];
                DmgI1.color = powersList[0];

                Dmg2.text = valuesList[1];
                Dmg2.color = textColorList[1];
                DmgI2.color = powersList[1];

                Dmg3.text = valuesList[2];
                Dmg3.color = textColorList[2];
                DmgI3.color = powersList[2];
            }

            if (slotsAdded == 4)
            {
                Dmg1.text = valuesList[0];
                Dmg1.color = textColorList[0];
                DmgI1.color = powersList[0];

                Dmg2.text = valuesList[1];
                Dmg2.color = textColorList[1];
                DmgI2.color = powersList[1];

                Dmg3.text = valuesList[2];
                Dmg3.color = textColorList[2];
                DmgI3.color = powersList[2];

                Dmg4.text = valuesList[3];
                Dmg4.color = textColorList[3];
                DmgI4.color = powersList[3];
            }

            if (slotsAdded == 5)
            {
                Dmg1.text = valuesList[0];
                Dmg1.color = textColorList[0];
                DmgI1.color = powersList[0];

                Dmg2.text = valuesList[1];
                Dmg2.color = textColorList[1];
                DmgI2.color = powersList[1];

                Dmg3.text = valuesList[2];
                Dmg3.color = textColorList[2];
                DmgI3.color = powersList[2];

                Dmg4.text = valuesList[3];
                Dmg4.color = textColorList[3];
                DmgI4.color = powersList[3];

                Dmg5.text = valuesList[4];
                Dmg5.color = textColorList[4];
                DmgI5.color = powersList[4];
            }

            if (slotsAdded == 6)
            {
                Dmg1.text = valuesList[0];
                Dmg1.color = textColorList[0];
                DmgI1.color = powersList[0];

                Dmg2.text = valuesList[1];
                Dmg2.color = textColorList[1];
                DmgI2.color = powersList[1];

                Dmg3.text = valuesList[2];
                Dmg3.color = textColorList[2];
                DmgI3.color = powersList[2];

                Dmg4.text = valuesList[3];
                Dmg4.color = textColorList[3];
                DmgI4.color = powersList[3];

                Dmg5.text = valuesList[4];
                Dmg5.color = textColorList[4];
                DmgI5.color = powersList[4];

                Dmg6.text = valuesList[5];
                Dmg6.color = textColorList[5];
                DmgI6.color = powersList[5];
            }

            if (slotsAdded == 7)
            {
                Dmg1.text = valuesList[0];
                Dmg1.color = textColorList[0];
                DmgI1.color = powersList[0];

                Dmg2.text = valuesList[1];
                Dmg2.color = textColorList[1];
                DmgI2.color = powersList[1];

                Dmg3.text = valuesList[2];
                Dmg3.color = textColorList[2];
                DmgI3.color = powersList[2];

                Dmg4.text = valuesList[3];
                Dmg4.color = textColorList[3];
                DmgI4.color = powersList[3];

                Dmg5.text = valuesList[4];
                Dmg5.color = textColorList[4];
                DmgI5.color = powersList[4];

                Dmg6.text = valuesList[5];
                Dmg6.color = textColorList[5];
                DmgI6.color = powersList[5];

                Dmg7.text = valuesList[6];
                Dmg7.color = textColorList[6];
                DmgI7.color = powersList[6];
            }

            if (slotsAdded == 8)
            {
                Dmg1.text = valuesList[0];
                Dmg1.color = textColorList[0];
                DmgI1.color = powersList[0];

                Dmg2.text = valuesList[1];
                Dmg2.color = textColorList[1];
                DmgI2.color = powersList[1];

                Dmg3.text = valuesList[2];
                Dmg3.color = textColorList[2];
                DmgI3.color = powersList[2];

                Dmg4.text = valuesList[3];
                Dmg4.color = textColorList[3];
                DmgI4.color = powersList[3];

                Dmg5.text = valuesList[4];
                Dmg5.color = textColorList[4];
                DmgI5.color = powersList[4];

                Dmg6.text = valuesList[5];
                Dmg6.color = textColorList[5];
                DmgI6.color = powersList[5];

                Dmg7.text = valuesList[6];
                Dmg7.color = textColorList[6];
                DmgI7.color = powersList[6];

                Dmg8.text = valuesList[7];
                Dmg8.color = textColorList[7];
                DmgI8.color = powersList[7];
            }

            if (slotsAdded == 9)
            {
                Dmg1.text = valuesList[0];
                Dmg1.color = textColorList[0];
                DmgI1.color = powersList[0];

                Dmg2.text = valuesList[1];
                Dmg2.color = textColorList[1];
                DmgI2.color = powersList[1];

                Dmg3.text = valuesList[2];
                Dmg3.color = textColorList[2];
                DmgI3.color = powersList[2];

                Dmg4.text = valuesList[3];
                Dmg4.color = textColorList[3];
                DmgI4.color = powersList[3];

                Dmg5.text = valuesList[4];
                Dmg5.color = textColorList[4];
                DmgI5.color = powersList[4];

                Dmg6.text = valuesList[5];
                Dmg6.color = textColorList[5];
                DmgI6.color = powersList[5];

                Dmg7.text = valuesList[6];
                Dmg7.color = textColorList[6];
                DmgI7.color = powersList[6];

                Dmg8.text = valuesList[7];
                Dmg8.color = textColorList[7];
                DmgI8.color = powersList[7];

                Dmg9.text = valuesList[8];
                Dmg9.color = textColorList[8];
                DmgI9.color = powersList[8];
            }

            if (slotsAdded == 10)
            {
                Dmg1.text = valuesList[0];
                Dmg1.color = textColorList[0];
                DmgI1.color = powersList[0];

                Dmg2.text = valuesList[1];
                Dmg2.color = textColorList[1];
                DmgI2.color = powersList[1];

                Dmg3.text = valuesList[2];
                Dmg3.color = textColorList[2];
                DmgI3.color = powersList[2];

                Dmg4.text = valuesList[3];
                Dmg4.color = textColorList[3];
                DmgI4.color = powersList[3];

                Dmg5.text = valuesList[4];
                Dmg5.color = textColorList[4];
                DmgI5.color = powersList[4];

                Dmg6.text = valuesList[5];
                Dmg6.color = textColorList[5];
                DmgI6.color = powersList[5];

                Dmg7.text = valuesList[6];
                Dmg7.color = textColorList[6];
                DmgI7.color = powersList[6];

                Dmg8.text = valuesList[7];
                Dmg8.color = textColorList[7];
                DmgI8.color = powersList[7];

                Dmg9.text = valuesList[8];
                Dmg9.color = textColorList[8];
                DmgI9.color = powersList[8];

                Dmg10.text = valuesList[9];
                Dmg10.color = textColorList[9];
                DmgI10.color = powersList[9];
            }

            if (slotsAdded == 11)
            {
                Dmg1.text = valuesList[0];
                Dmg1.color = textColorList[0];
                DmgI1.color = powersList[0];

                Dmg2.text = valuesList[1];
                Dmg2.color = textColorList[1];
                DmgI2.color = powersList[1];

                Dmg3.text = valuesList[2];
                Dmg3.color = textColorList[2];
                DmgI3.color = powersList[2];

                Dmg4.text = valuesList[3];
                Dmg4.color = textColorList[3];
                DmgI4.color = powersList[3];

                Dmg5.text = valuesList[4];
                Dmg5.color = textColorList[4];
                DmgI5.color = powersList[4];

                Dmg6.text = valuesList[5];
                Dmg6.color = textColorList[5];
                DmgI6.color = powersList[5];

                Dmg7.text = valuesList[6];
                Dmg7.color = textColorList[6];
                DmgI7.color = powersList[6];

                Dmg8.text = valuesList[7];
                Dmg8.color = textColorList[7];
                DmgI8.color = powersList[7];

                Dmg9.text = valuesList[8];
                Dmg9.color = textColorList[8];
                DmgI9.color = powersList[8];

                Dmg10.text = valuesList[9];
                Dmg10.color = textColorList[9];
                DmgI10.color = powersList[9];

                Dmg11.text = valuesList[10];
                Dmg11.color = textColorList[10];
                DmgI11.color = powersList[10];
            }

            if (slotsAdded >= 12)
            {
                Dmg1.text = valuesList[0];
                Dmg1.color = textColorList[0];
                DmgI1.color = powersList[0];

                Dmg2.text = valuesList[1];
                Dmg2.color = textColorList[1];
                DmgI2.color = powersList[1];

                Dmg3.text = valuesList[2];
                Dmg3.color = textColorList[2];
                DmgI3.color = powersList[2];

                Dmg4.text = valuesList[3];
                Dmg4.color = textColorList[3];
                DmgI4.color = powersList[3];

                Dmg5.text = valuesList[4];
                Dmg5.color = textColorList[4];
                DmgI5.color = powersList[4];

                Dmg6.text = valuesList[5];
                Dmg6.color = textColorList[5];
                DmgI6.color = powersList[5];

                Dmg7.text = valuesList[6];
                Dmg7.color = textColorList[6];
                DmgI7.color = powersList[6];

                Dmg8.text = valuesList[7];
                Dmg8.color = textColorList[7];
                DmgI8.color = powersList[7];

                Dmg9.text = valuesList[8];
                Dmg9.color = textColorList[8];
                DmgI9.color = powersList[8];

                Dmg10.text = valuesList[9];
                Dmg10.color = textColorList[9];
                DmgI10.color = powersList[9];

                Dmg11.text = valuesList[10];
                Dmg11.color = textColorList[10];
                DmgI11.color = powersList[10];

                Dmg12.text = valuesList[11];
                Dmg12.color = textColorList[11];
                DmgI12.color = powersList[11];
            }


            valuesList.Clear();
            powersList.Clear();
            textColorList.Clear();
            currentDialIndex = startingIndex;
        }
    }


}
