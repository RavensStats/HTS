using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine.Networking;

public class Team_Builder_Dynamic_Event_Handler : MonoBehaviour
{
    List<string> sets = new List<string>();
    List<string> sets_AS = new List<string>();

    public Dropdown set_Dropdown, unit_Dropdown, team_Dropdown, team_Name_Dropdown, set_Dropdown_Advanced_Search, Standard_Powers_Dropdown_Advanced_Search, player_Dropdown, Slot_Dropdown, SearchedUnitsDropdown;

    public Button add_Unit, remove_Unit, clear_Team;

    public Text pointsTotalText;
    public Text Spd1, Spd2, Spd3, Spd4, Spd5, Spd6, Spd7, Spd8, Spd9, Spd10, Spd11, Spd12;
    public Text Atk1, Atk2, Atk3, Atk4, Atk5, Atk6, Atk7, Atk8, Atk9, Atk10, Atk11, Atk12;
    public Text Def1, Def2, Def3, Def4, Def5, Def6, Def7, Def8, Def9, Def10, Def11, Def12;
    public Text Dmg1, Dmg2, Dmg3, Dmg4, Dmg5, Dmg6, Dmg7, Dmg8, Dmg9, Dmg10, Dmg11, Dmg12;
    public Text Slot1, Slot2, Slot3, Slot4, Slot5, Slot6, Slot7, Slot8, Slot9, Slot10, Slot11, Slot12;
    public Text RangeValue;
    public Text UnitPreviewPoints, CurrentKeyword, UnitName, PowerDescription;

    public Image SpdI1, SpdI2, SpdI3, SpdI4, SpdI5, SpdI6, SpdI7, SpdI8, SpdI9, SpdI10, SpdI11, SpdI12;
    public Image AtkI1, AtkI2, AtkI3, AtkI4, AtkI5, AtkI6, AtkI7, AtkI8, AtkI9, AtkI10, AtkI11, AtkI12;
    public Image DefI1, DefI2, DefI3, DefI4, DefI5, DefI6, DefI7, DefI8, DefI9, DefI10, DefI11, DefI12;
    public Image DmgI1, DmgI2, DmgI3, DmgI4, DmgI5, DmgI6, DmgI7, DmgI8, DmgI9, DmgI10, DmgI11, DmgI12;
    public Image speedIcon, attackIcon, defenseIcon, damageIcon, rangeTargets;
    public Image Sculpt;
    public Image TeamAbilityImage;

    public GameObject traitImg1, traitImg2, traitImg3, traitImg4, traitImg5, traitImg6;
    public GameObject ITElevated, ITHindering, ITBlocking, ITBlockingDestroy, ITCharacters, ITAdjacentCharacters;
    public GameObject IMElevated, IMHindering, IMWater, IMBlocking, IMBlockingOutdoor, IMBlockingDestroy, IMCharacters, IMAdjacentCharacters;
    public GameObject CurrentlyPreviewedUnit;

    public string speedType, attackType, defenseType, damageType;

    //Dynamic Unit Section
    public string selectedSet, SelectedUnit, SelectedCollectorsNumber;
    public string Selected_Dynamic_Collectors_Number, Selected_Dynamic_Set_Name;
    public string Set_Name_d, Collectors_Number_d, Unit_Name_d, Set_Abbrev_d;
    public int Dial_Length_d, Last_Click_d, Points_d, Range_Value_d, Range_Targets_d;
    public string Experience_d, Rarity_d, Base_Type_d, Speed_Symbol_d, Attack_Symbol_d, Defense_Symbol_d, Damage_Symbol_d;
    public bool IM_Elevated_d, IM_Hindering_d, IM_Water_d, IM_Blocking_d, IM_Outdoor_Blocking_d, IM_Blocking_Destroy_d, IM_Characters_d, IM_Adjacent_Characters_d;
    public bool IT_Elevated_d, IT_Hindering_d, IT_Blocking_d, IT_Blocking_Destroy_d, IT_Characters_d, IT_Adjacent_Characters_d;
    public string Keywords_d, Team_Ability_d, Trait1_d, Trait2_d, Trait3_d, Trait4_d, Trait5_d, Trait6_d;
    
    int SelectedOfficialUnitIndex, SelectedCustomUnitIndex;
    string SelectedOfficialCollectorsNumber, SelectedCustomCollectorsNumber;

    public List<int> DialInfoDynamic_Slot = new List<int>();

    public List<int> DialInfoDynamic_SpeedValue = new List<int>();
    public List<int> DialInfoDynamic_AttackValue = new List<int>();
    public List<int> DialInfoDynamic_DefenseValue = new List<int>();
    public List<int> DialInfoDynamic_DamageValue = new List<int>();

    public List<int> DialInfoDynamic_SpeedPowerInt = new List<int>();
    public List<int> DialInfoDynamic_AttackPowerInt = new List<int>();
    public List<int> DialInfoDynamic_DefensePowerInt = new List<int>();
    public List<int> DialInfoDynamic_DamagePowerInt = new List<int>();

    public List<string> DialInfoDynamic_SpeedPowerStr = new List<string>();
    public List<string> DialInfoDynamic_AttackPowerStr = new List<string>();
    public List<string> DialInfoDynamic_DefensePowerStr = new List<string>();
    public List<string> DialInfoDynamic_DamagePowerStr = new List<string>();

    public List<string> DialInfoDynamic_SpeedPowerDescription = new List<string>();
    public List<string> DialInfoDynamic_AttackPowerDescription = new List<string>();
    public List<string> DialInfoDynamic_DefensePowerDescription = new List<string>();
    public List<string> DialInfoDynamic_DamagePowerDescription = new List<string>();

    List<int> AllPowersList = new List<int>();
    List<string> AllPowerNamesList = new List<string>();
    List<string> AllPowerDescriptionsList = new List<string>();

    List<int> StandardPowersList = new List<int>();
    List<string> StandardPowerNamesList = new List<string>();
    List<string> StandardPowerDescriptionsList = new List<string>();

    List<string> DropdownSlotList = new List<string>();
    
    string ChosenStandardPower, ChosenStandardPowerName;
    string SelectedPlayer;
    int SelectedPlayerID;

    public List<string> DropdownUsersList = new List<string>();

    public InputField UnitNameInput_AS, PointsInput_AS, KeywordInput_AS;
    //

    public string teamAbility, teamAbilityDescription;
    public string SAVE_FOLDER;

    public int pointsTotal;
    public int rangeTargetsNum;
    public int SelectedUnitIndex, selectedTeamUnitIndex, keywordIndex, selectedTeamLoadIndex;

    //Improved Targeting
    public bool IT_elevated;
    public bool IT_hindering;

    public bool IT_blocking;
    public bool IT_blocking_destroy;

    public bool IT_characters;
    public bool IT_adjacent_characters;

    //Improved Movement
    public bool IM_elevated;
    public bool IM_hindering;
    public bool IM_water;
    public bool IM_blocking;
    public bool IM_outdoor_blocking;
    public bool IM_blocking_destroy;
    public bool IM_characters;
    public bool IM_adjacent_characters;

    public List<GameObject> units = new List<GameObject>();
    public List<string> team = new List<string>();
    public List<string> team_Unit_Name = new List<string>();
    public List<string> team_Set_Name = new List<string>();
    public List<string> team_Collectors_Number_Name = new List<string>();
    public List<GameObject> tempUnitList = new List<GameObject>();
    public List<GameObject> team_Unit_GameObject = new List<GameObject>();
    
    public List<string> selectedUnits = new List<string>();
    public List<string> selectedTeam = new List<string>();
    public List<string> UnitKeywords = new List<string>();

    List<string> valuesList = new List<string>();
    List<Color> powersList = new List<Color>();
    List<Color> textColorList = new List<Color>();
    int currentDialIndex = 0;

    List<string> speedPowersList = new List<string>();
    List<string> attackPowersList = new List<string>();
    List<string> defensePowersList = new List<string>();
    List<string> damagePowersList = new List<string>();
    List<string> loadedTeamsList = new List<string>();
    List<string> loadedUnitsList = new List<string>();

    public InputField TeamName;

    public List<string> SetDropDownDataList = new List<string>();
    public List<string> SetCollectorsNumberList = new List<string>();
    public List<string> SetUnitNameList = new List<string>();

    public Dropdown UnitsNameDropdown;

    public List<string> AdvancedSearchDropDownDataList = new List<string>();
    public List<string> AdvancedSearchCollectorsNumberList = new List<string>();
    public List<string> AdvancedSearchUnitNameList = new List<string>();
    public List<string> AdvancedSearchSetNameList = new List<string>();
    
    List<GameObject> PreviewUnitList = new List<GameObject>();

    public Toggle YourTeamsOnly;
    public Toggle LessThanOrEqualTo, GreaterThanOrEqualTo, EqualTo;

    List<string> TeamIDList = new List<string>();
    List<string> TeamNameList = new List<string>();
    List<string> TeamPointsList = new List<string>();

    bool IsEditingTeam = false;
    public Text EditingTeam;

    //Image
    public SpriteRenderer DynamicUnitSculpt;



    // Start is called before the first frame update
    IEnumerator Start()
    {
        yield return StartCoroutine(GetAllPowersList());

        CallLoadTeamNames();

        pointsTotal = 0;
        pointsTotalText.text = pointsTotal + " pts";
        CallGetSetNames();

        
    }

    private void Update()
    {
        if (IsEditingTeam)
        {
            EditingTeam.gameObject.SetActive(true);
        }
        else
        {
            EditingTeam.gameObject.SetActive(false);
        }
    }

    public void SearchForUnits()
    {
        //Unit name
        //select* from Units where Unit_Name Like '%Batma%';

        //Set name
        //select* from Units where Set_Name = 'The Mighty Thor';

        //Power and Slot special powers
        //select Set_Name, Collectors_Number from Dial
        //where
        //(
        //    Speed_Power In(
        //        select Power_ID from Powers

        //        where Power_Description Like '%Super Senses%'

        //        or Power_Name Like '%Super Senses%'
        //    )
        //or Attack_Power In(
        //    select Power_ID from Powers
        //    where Power_Description Like '%Super Senses%'
        //    or Power_Name Like '%Super Senses%'
        //)
        //or Defense_Power In(
        //    select Power_ID from Powers
        //    where Power_Description Like '%Super Senses%'
        //    or Power_Name Like '%Super Senses%'
        //)
        //or Damage_Power In(
        //    select Power_ID from Powers
        //    where Power_Description Like '%Super Senses%'
        //    or Power_Name Like '%Super Senses%'
        //)
        //)
        //AND Slot = 1
        //union
        //select Set_Name, Collectors_Number from Units
        //WHERE Trait1 Like '%Super Senses%'
        //OR Trait2 Like '%Super Senses%'
        //OR Trait3 Like '%Super Senses%'
        //OR Trait4 Like '%Super Senses%'
        //OR Trait5 Like '%Super Senses%'
        //OR Trait6 Like '%Super Senses%'

        //Keywords
        //select* from Units where Keywords Like '%Sold%';

        //User
        //select* from Units where User_ID = 11

        string SQL = "";
        string UNITNAME = UnitNameInput_AS.text;
        string SETNAME = set_Dropdown_Advanced_Search.options[set_Dropdown_Advanced_Search.value].text;
        string KEYWORDS = KeywordInput_AS.text;
        string POWERS = ChosenStandardPowerName;
        string SLOT = Slot_Dropdown.options[Slot_Dropdown.value].text;
        string POINTS = PointsInput_AS.text;
        string PLAYER = SelectedPlayer;

        // Build the non-power filter conditions (applied to both branches when POWERS is set)
        var extraConds = new System.Collections.Generic.List<string>();
        if (UNITNAME.Length > 0)
            extraConds.Add("Units.Unit_Name Like '%" + UNITNAME + "%'");
        if (SETNAME != "Sets")
            extraConds.Add("Units.Set_Name = '" + SETNAME + "'");
        if (KEYWORDS.Length > 0)
            extraConds.Add("Units.Keywords Like '%" + KEYWORDS + "%'");
        if (POINTS.Length > 0)
        {
            string op = LessThanOrEqualTo.isOn ? "<=" : GreaterThanOrEqualTo.isOn ? ">=" : "=";
            extraConds.Add("Units.Point_Value_1 " + op + " " + POINTS);
        }
        if (PLAYER != null && PLAYER.Length > 0)
            extraConds.Add("Units.User_ID = '" + PLAYER + "'");

        string extraWhere = extraConds.Count > 0 ? string.Join(" AND ", extraConds.ToArray()) : "";

        string SELECT = "SELECT DISTINCT Units.Set_Name, Units.Set_Number, Units.Unit_Name, Units.Point_Value_1 FROM Units ";

        if (POWERS != "Powers")
        {
            // Build power WHERE across all slots (or a specific slot)
            string powerWhere;
            if (SLOT != "Any" && int.TryParse(SLOT, out int slotNum))
            {
                powerWhere = "(Dial.Speed_Power_" + slotNum + " = '" + POWERS + "' OR Dial.Attack_Power_" + slotNum + " = '" + POWERS + "' OR Dial.Defense_Power_" + slotNum + " = '" + POWERS + "' OR Dial.Damage_Power_" + slotNum + " = '" + POWERS + "')";
            }
            else
            {
                var conds = new System.Text.StringBuilder();
                for (int s = 1; s <= 30; s++)
                {
                    if (s > 1) conds.Append(" OR ");
                    conds.Append("Dial.Speed_Power_" + s + " = '" + POWERS + "'");
                    conds.Append(" OR Dial.Attack_Power_" + s + " = '" + POWERS + "'");
                    conds.Append(" OR Dial.Defense_Power_" + s + " = '" + POWERS + "'");
                    conds.Append(" OR Dial.Damage_Power_" + s + " = '" + POWERS + "'");
                }
                powerWhere = "(" + conds.ToString() + ")";
            }

            // Branch 1: dial power match
            SQL += SELECT;
            SQL += "JOIN Dial ON Units.Set_Name = Dial.Set_Name AND Units.Set_Number = Dial.Set_Number ";
            SQL += "WHERE " + powerWhere;
            if (extraWhere.Length > 0) SQL += " AND " + extraWhere;

            // Branch 2: trait text match
            string traitWhere = "(Units.Trait_1_Ability Like '%" + POWERS + "%' OR Units.Trait_2_Ability Like '%" + POWERS + "%' OR Units.Trait_3_Ability Like '%" + POWERS + "%' OR Units.Trait_4_Ability Like '%" + POWERS + "%' OR Units.Trait_5_Ability Like '%" + POWERS + "%' OR Units.Trait_6_Ability Like '%" + POWERS + "%')";
            SQL += " UNION " + SELECT;
            SQL += "WHERE " + traitWhere;
            if (extraWhere.Length > 0) SQL += " AND " + extraWhere;
        }
        else
        {
            SQL += SELECT;
            if (extraWhere.Length > 0)
                SQL += "WHERE " + extraWhere;
        }

        SQL += ";";

        Debug.Log(SQL);

//        select Units.Set_Name, Units.Collectors_Number from Units
//        where(Set_Name, Collectors_Number) In(
//        select Units.Set_Name, Units.Collectors_Number from Units
//join
//                Dial
//                on Units.Set_Name = Dial.Set_Name and Units.Collectors_Number = Dial.Collectors_Number
//                where
//                (
//                    Speed_Power In(
//                        select Power_ID from Powers

//                        where Power_Description Like '%Super Senses%'

//                        or Power_Name Like '%Super Senses%'
//                    )
//                or Attack_Power In(
//                    select Power_ID from Powers
//                    where Power_Description Like '%Super Senses%'
//                    or Power_Name Like '%Super Senses%'
//                )
//                or Defense_Power In(
//                    select Power_ID from Powers
//                    where Power_Description Like '%Super Senses%'
//                    or Power_Name Like '%Super Senses%'
//                )
//                or Damage_Power In(
//                    select Power_ID from Powers
//                    where Power_Description Like '%Super Senses%'
//                    or Power_Name Like '%Super Senses%'
//                )
//                )
//                AND Slot = 1
//                union
//                select Set_Name, Collectors_Number from Units
//                WHERE(Trait1 Like '%Super Senses%'
//                OR Trait2 Like '%Super Senses%'
//                OR Trait3 Like '%Super Senses%'
//                OR Trait4 Like '%Super Senses%'
//                OR Trait5 Like '%Super Senses%'
//                OR Trait6 Like '%Super Senses%')
//            )
//            and Units.Set_Name = 'The Mighty Thor'


        CallAdvancedSearch(SQL);

    }

    public void CallAdvancedSearch(string SQL)
    {
        StartCoroutine(AdvancedSearch(SQL));
    }

    IEnumerator AdvancedSearch(string SQL)
    {
        AdvancedSearchDropDownDataList.Clear();
        AdvancedSearchCollectorsNumberList.Clear();
        AdvancedSearchUnitNameList.Clear();
        AdvancedSearchSetNameList.Clear();
        SearchedUnitsDropdown.ClearOptions();

        WWWForm form = new WWWForm();

        form.AddField("SQL", SQL);

        using (UnityWebRequest www = UnityWebRequest.Post("https://www.stark44.com/Heroclix/AdvancedSearch.php",form))
        {
            www.certificateHandler = new BypassCertificate();
            yield return www.SendWebRequest();

            if (www.result != UnityWebRequest.Result.Success)
            {
                Debug.Log(www.error);
            }
            else
            {
                string DBUnits = www.downloadHandler.text;

                while (DBUnits.Contains(";") && DBUnits.Length > 1)
                {
                    var IteratedRowData = DBUnits.Substring(0, DBUnits.IndexOf(";"));
                    var DropdownData = IteratedRowData;
                    var NextTab = IteratedRowData.IndexOf("\t");

                    var Set_Name = IteratedRowData.Substring(0, NextTab);
                    NextTab = IteratedRowData.IndexOf("\t");
                    IteratedRowData = IteratedRowData.Substring(NextTab + 1, IteratedRowData.Length - NextTab - 1);
                    NextTab = IteratedRowData.IndexOf("\t");

                    var Collectors_Num = IteratedRowData.Substring(0, NextTab);
                    NextTab = IteratedRowData.IndexOf("\t");
                    IteratedRowData = IteratedRowData.Substring(NextTab + 1, IteratedRowData.Length - NextTab - 1);
                    NextTab = IteratedRowData.IndexOf("\t");

                    var Unit_Name = IteratedRowData.Substring(0, NextTab);
                    NextTab = IteratedRowData.IndexOf("\t");
                    IteratedRowData = IteratedRowData.Substring(NextTab + 1, IteratedRowData.Length - NextTab - 1);

                    var Points = IteratedRowData.Substring(0, IteratedRowData.Length);

                    DropdownData = Unit_Name + " " + Collectors_Num + " " + Points;

                    AdvancedSearchDropDownDataList.Add(DropdownData);
                    AdvancedSearchCollectorsNumberList.Add(Collectors_Num);
                    AdvancedSearchUnitNameList.Add(Unit_Name);
                    AdvancedSearchSetNameList.Add(Set_Name);

                    DBUnits = DBUnits.Substring(DBUnits.IndexOf(";") + 1, DBUnits.Length - DBUnits.IndexOf(";") - 1);
                }




            }
        }
        SearchedUnitsDropdown.AddOptions(AdvancedSearchDropDownDataList);
        if (SearchedUnitsDropdown.options.Count > 0)
        {
            Searched_Units_Dropdown_IndexChanged(0);
        }

        yield return null;
    }

    public void Searched_Units_Dropdown_IndexChanged(int index)
    {
        selectedTeamUnitIndex = index;

        PowerDescription.text = "";

        SelectedUnit = AdvancedSearchUnitNameList[index];
        SelectedUnitIndex = index;
        SelectedCollectorsNumber = AdvancedSearchCollectorsNumberList[index];

        Selected_Dynamic_Set_Name = AdvancedSearchSetNameList[index];
        Selected_Dynamic_Collectors_Number = AdvancedSearchCollectorsNumberList[index];

        StartCoroutine(LoadSearchedUnit());
    }

    IEnumerator LoadSearchedUnit()
    {
        yield return StartCoroutine(GetDynamicUnitDial());
        yield return StartCoroutine(GetDynamicUnitStats());
        LoadDynamicUnit();
    }

    IEnumerator GetDynamicUnitSpecialPowers()
    {
        Debug.Log("[GetDynamicUnitSpecialPowers] Fetching SPs for Set=" + Selected_Dynamic_Set_Name + " CN=" + Selected_Dynamic_Collectors_Number);

        WWWForm form = new WWWForm();
        form.AddField("Set_Name", Selected_Dynamic_Set_Name);
        form.AddField("Collectors_Number", Selected_Dynamic_Collectors_Number);

        using (UnityWebRequest www = UnityWebRequest.Post("https://www.stark44.com/Heroclix/getDynamicUnitSpecialPowers.php", form))
        {
            www.certificateHandler = new BypassCertificate();
            yield return www.SendWebRequest();

            if (www.result != UnityWebRequest.Result.Success)
            {
                Debug.LogError("[GetDynamicUnitSpecialPowers] HTTP error: " + www.error);
            }
            else if (string.IsNullOrEmpty(www.downloadHandler.text))
            {
                Debug.LogWarning("[GetDynamicUnitSpecialPowers] Empty response.");
            }
            else
            {
                Debug.Log("[GetDynamicUnitSpecialPowers] Raw response: " + www.downloadHandler.text.Substring(0, System.Math.Min(300, www.downloadHandler.text.Length)));

                string data = www.downloadHandler.text;
                if (data.Contains(";"))
                    data = data.Substring(0, data.IndexOf(";"));

                string[] fields = data.Split('\t');
                Debug.Log("[GetDynamicUnitSpecialPowers] Field count after split: " + fields.Length);

                var spNames     = new System.Collections.Generic.List<string>();
                var spAbilities = new System.Collections.Generic.List<string>();
                var spTypes     = new System.Collections.Generic.List<string>();

                for (int p = 0; p + 2 < fields.Length; p += 3)
                {
                    spNames.Add(fields[p]);
                    spAbilities.Add(fields[p + 1]);
                    spTypes.Add(fields[p + 2]);
                    Debug.Log("[GetDynamicUnitSpecialPowers] SP[" + (p/3) + "] Name='" + fields[p] + "' Type='" + fields[p+2] + "'");
                }

                Debug.Log("[GetDynamicUnitSpecialPowers] Dial slot count: " + DialInfoDynamic_Slot.Count);

                for (int p = 0; p < spNames.Count; p++)
                {
                    if (string.IsNullOrEmpty(spNames[p])) continue;
                    string statType = GetStatTypeFromIcon(spTypes[p]);
                    Debug.Log("[GetDynamicUnitSpecialPowers] Applying SP '" + spNames[p] + "' iconType='" + spTypes[p] + "' -> statType='" + statType + "'");
                    int patchCount = 0;
                    for (int slot = 0; slot < DialInfoDynamic_Slot.Count; slot++)
                    {
                        switch (statType)
                        {
                            case "speed":
                                if (DialInfoDynamic_SpeedPowerStr[slot] == "Special Power")
                                { DialInfoDynamic_SpeedPowerStr[slot] = spNames[p]; DialInfoDynamic_SpeedPowerDescription[slot] = spAbilities[p]; patchCount++; }
                                break;
                            case "attack":
                                if (DialInfoDynamic_AttackPowerStr[slot] == "Special Power")
                                { DialInfoDynamic_AttackPowerStr[slot] = spNames[p]; DialInfoDynamic_AttackPowerDescription[slot] = spAbilities[p]; patchCount++; }
                                break;
                            case "defense":
                                if (DialInfoDynamic_DefensePowerStr[slot] == "Special Power")
                                { DialInfoDynamic_DefensePowerStr[slot] = spNames[p]; DialInfoDynamic_DefensePowerDescription[slot] = spAbilities[p]; patchCount++; }
                                break;
                            case "damage":
                                if (DialInfoDynamic_DamagePowerStr[slot] == "Special Power")
                                { DialInfoDynamic_DamagePowerStr[slot] = spNames[p]; DialInfoDynamic_DamagePowerDescription[slot] = spAbilities[p]; patchCount++; }
                                break;
                        }
                    }
                    Debug.Log("[GetDynamicUnitSpecialPowers] Patched " + patchCount + " slot(s) with '" + spNames[p] + "'");
                }
            }
        }
        yield return null;
    }

    string GetStatTypeFromIcon(string iconType)
    {
        switch (iconType.ToLower())
        {
            case "boot": case "wing": case "dolphin":
            case "transport_boot": case "transport_wing": case "transport_dolphin":
                return "speed";
            case "fist":
                return "attack";
            case "shield": case "indomitable":
                return "defense";
            case "starburst": case "colossal":
                return "damage";
            default:
                return "";
        }
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
        Debug.Log("Powers Finished");
        yield return null;
    }

    public void CallGetStandardPowersList()
    {
        StartCoroutine(GetStandardPowersList());
    }

    IEnumerator GetStandardPowersList()
    {
        // Hardcoded standard powers matching the dial viewer — no server call needed
        var allPowers = new List<string>
        {
            "Powers",
            // Speed
            "Charge", "Earthbound/Neutralized", "Flurry", "Force Blast",
            "Hypersonic Speed", "Leap/Climb", "Mind Control", "Phasing/Teleport",
            "Plasticity", "Running Shot", "Sidestep", "Stealth",
            // Attack
            "Blades/Claws/Fangs", "Energy Explosion", "Incapacitate",
            "Penetrating/Psychic Blast", "Poison", "Precision Strike",
            "Pulse Wave", "Quake", "Smoke Cloud", "Steal Energy",
            "Super Strength", "Telekinesis",
            // Defense
            "Barrier", "Combat Reflexes", "Defend", "Energy Shield/Deflection",
            "Impervious", "Invincible", "Invulnerability", "Mastermind",
            "Regeneration", "Super Senses", "Toughness", "Willpower",
            // Damage
            "Battle Fury", "Close Combat Expert", "Empower", "Enhancement",
            "Exploit Weakness", "Leadership", "Outwit", "Perplex",
            "Probability Control", "Ranged Combat Expert", "Shape Change", "Support"
        };

        for (int i = 0; i < allPowers.Count; i++)
        {
            StandardPowersList.Add(i - 1); // -1 for "Powers" placeholder, 0+ for real powers
            StandardPowerNamesList.Add(allPowers[i]);
            StandardPowerDescriptionsList.Add("");
        }

        Standard_Powers_Dropdown_Advanced_Search.AddOptions(StandardPowerNamesList);
        if (StandardPowersList.Count > 0)
            Standard_Powers_Dropdown_IndexChanged(0);

        Debug.Log("Powers Finished");
        yield return null;
    }

    public void Standard_Powers_Dropdown_IndexChanged(int index)
    {
        if (StandardPowersList.Count == 0 || index >= StandardPowersList.Count) return;
        ChosenStandardPower = StandardPowersList[index].ToString();
        ChosenStandardPowerName = StandardPowerNamesList[index];
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
                    DropdownUsersList.Add(DBUsers.Substring(0, DBUsers.IndexOf(",")));
                    DBUsers = DBUsers.Substring(DBUsers.IndexOf(",") + 1, DBUsers.Length - DBUsers.IndexOf(",") - 1);
                }

                player_Dropdown.AddOptions(DropdownUsersList);

            }
        }

    }

    public void Player_Dropdown_IndexChanged(int index)
    {
        SelectedPlayer = player_Dropdown.options[index].text;
    }

    public void CallGetUserID()
    {
        if (SelectedPlayer.Length > 0)
        {
            StartCoroutine(GetUserID());
        }
        else
        {
            SelectedPlayerID = -2;
        }
    }

    IEnumerator GetUserID()
    {
        WWWForm form = new WWWForm();
        form.AddField("Username", SelectedPlayer);

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
                string UserIDData = www.downloadHandler.text;

                SelectedPlayerID = int.Parse(UserIDData);


            }

        }

        yield return null;
    }

    public void CallGetMaxSlot()
    {
        StartCoroutine(GetMaxSlot());
    }

    IEnumerator GetMaxSlot()
    {
        DropdownSlotList.Add("Any");

        using (UnityWebRequest www = UnityWebRequest.Get("https://www.stark44.com/Heroclix/getMaxSlots.php"))
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

                Debug.Log(DBUsers);

                while (DBUsers.Contains(",") && DBUsers.Length > 1)
                {
                    DropdownSlotList.Add(DBUsers.Substring(0, DBUsers.IndexOf(",")));
                    DBUsers = DBUsers.Substring(DBUsers.IndexOf(",") + 1, DBUsers.Length - DBUsers.IndexOf(",") - 1);
                }

                Slot_Dropdown.AddOptions(DropdownSlotList);

            }
        }

        yield return null;
    }

    public void Display_Trait1()
    {
        PowerDescription.text = Trait1_d;
    }

    public void Display_Trait2()
    {
        PowerDescription.text = Trait2_d;
    }

    public void Display_Trait3()
    {
        PowerDescription.text = Trait3_d;
    }

    public void Display_Trait4()
    {
        PowerDescription.text = Trait4_d;
    }

    public void Display_Trait5()
    {
        PowerDescription.text = Trait5_d;
    }

    public void Display_Trait6()
    {
        PowerDescription.text = Trait6_d;
    }

    public void Display_Team_Ability()
    {
        if (PowerDescription.text == teamAbility)
        {
            StartCoroutine(ShowTeamAbilityDescription());
        }
        else
        {
            PowerDescription.text = teamAbility;
        }
    }

    IEnumerator ShowTeamAbilityDescription()
    {
        yield return StartCoroutine(GetTADesc());
        PowerDescription.text = teamAbilityDescription;
    }

    public void Display_Speed_Power_Text(int SelectedSlot)
    {
        if (speedPowersList.Count() > 0)
        {
            int descIndex = currentDialIndex + SelectedSlot;
            if(PowerDescription.text == speedPowersList[SelectedSlot])
            {
                PowerDescription.text = (descIndex < DialInfoDynamic_SpeedPowerDescription.Count)
                    ? DialInfoDynamic_SpeedPowerDescription[descIndex]
                    : "";
            }
            else
            {
                PowerDescription.text = speedPowersList[SelectedSlot];
            }
        }
    }

    public void Display_Attack_Power_Text(int SelectedSlot)
    {
        if (attackPowersList.Count() > 0)
        {
            int descIndex = currentDialIndex + SelectedSlot;
            if (PowerDescription.text == attackPowersList[SelectedSlot])
            {
                PowerDescription.text = (descIndex < DialInfoDynamic_AttackPowerDescription.Count)
                    ? DialInfoDynamic_AttackPowerDescription[descIndex]
                    : "";
            }
            else
            {
                PowerDescription.text = attackPowersList[SelectedSlot];
            }
        }
    }

    public void Display_Defense_Power_Text(int SelectedSlot)
    {
        if (defensePowersList.Count() > 0)
        {
            int descIndex = currentDialIndex + SelectedSlot;
            if (PowerDescription.text == defensePowersList[SelectedSlot])
            {
                PowerDescription.text = (descIndex < DialInfoDynamic_DefensePowerDescription.Count)
                    ? DialInfoDynamic_DefensePowerDescription[descIndex]
                    : "";
            }
            else
            {
                PowerDescription.text = defensePowersList[SelectedSlot];
            }
        }
    }

    public void Display_Damage_Power_Text(int SelectedSlot)
    {
        if (damagePowersList.Count() > 0)
        {
            int descIndex = currentDialIndex + SelectedSlot;
            if (PowerDescription.text == damagePowersList[SelectedSlot])
            {
                PowerDescription.text = (descIndex < DialInfoDynamic_DamagePowerDescription.Count)
                    ? DialInfoDynamic_DamagePowerDescription[descIndex]
                    : "";
            }
            else
            {
                PowerDescription.text = damagePowersList[SelectedSlot];
            }
        }
    }

    public void Team_Unit_Dropdown_IndexChanged(int index)
    {
        selectedTeamUnitIndex = index;
        //currentDialIndex = 0;
        //Preview_Unit(team, selectedTeamUnitIndex);

        PowerDescription.text = "";

        SelectedUnit = team_Unit_Name[index];
        SelectedUnitIndex = index;
        SelectedCollectorsNumber = team_Collectors_Number_Name[index];

        Selected_Dynamic_Set_Name = team_Set_Name[index];
        Selected_Dynamic_Collectors_Number = team_Collectors_Number_Name[index];

        StartCoroutine(LoadSearchedUnit());
    }

    public void Team_Name_Dropdown_IndexChanged(int index)
    {
        if (team_Name_Dropdown.options.Count == 0 || index >= team_Name_Dropdown.options.Count) return;
        var text = team_Name_Dropdown.options[index].text;
        var NextTab = text.IndexOf(" ");
        if (NextTab > 0 && int.TryParse(text.Substring(0, NextTab), out int parsedId))
            selectedTeamLoadIndex = parsedId;
    }

    public void Add_To_Team()
    {
        //var units = Resources.LoadAll("Units/" + selectedSet, typeof(GameObject));

        Selected_Unit_Dropdown_IndexChanged(unit_Dropdown.value);

        selectedTeam.Add(Collectors_Number_d + "\t" + Unit_Name_d + "\t" + Points_d.ToString() + " pts");
        team_Dropdown.ClearOptions();
        team_Dropdown.AddOptions(selectedTeam);
        team.Add(Set_Name_d + "\t" + Collectors_Number_d);
        team_Unit_Name.Add(Unit_Name_d);
        team_Set_Name.Add(Set_Name_d);
        team_Collectors_Number_Name.Add(Collectors_Number_d);
        team_Unit_GameObject.Add(PreviewUnitList[0]);
        pointsTotal += Points_d;
        pointsTotalText.text = pointsTotal + " pts";
    }

    public void Load_Add_To_Team()
    {
        //var units = Resources.LoadAll("Units/" + selectedSet, typeof(GameObject));

        selectedTeam.Add(Collectors_Number_d + "\t" + Unit_Name_d + "\t" + Points_d.ToString() + " pts");
        team_Dropdown.ClearOptions();
        team_Dropdown.AddOptions(selectedTeam);
        team.Add(Set_Name_d + "\t" + Collectors_Number_d);
        team_Unit_Name.Add(Unit_Name_d);
        team_Set_Name.Add(Set_Name_d);
        team_Collectors_Number_Name.Add(Collectors_Number_d);
        team_Unit_GameObject.Add(PreviewUnitList[0]);
        pointsTotal += Points_d;
        pointsTotalText.text = pointsTotal + " pts";
    }

    public void Advaned_Search_Add_To_Team()
    {
        if (SearchedUnitsDropdown.options.Count > 0)
        {
            Searched_Units_Dropdown_IndexChanged(SearchedUnitsDropdown.value);

            selectedTeam.Add(Collectors_Number_d + "\t" + Unit_Name_d + "\t" + Points_d.ToString() + " pts");
            team_Dropdown.ClearOptions();
            team_Dropdown.AddOptions(selectedTeam);
            team.Add(Set_Name_d + "\t" + Collectors_Number_d);
            team_Unit_Name.Add(Unit_Name_d);
            team_Set_Name.Add(Set_Name_d);
            team_Collectors_Number_Name.Add(Collectors_Number_d);
            team_Unit_GameObject.Add(PreviewUnitList[0]);
            pointsTotal += Points_d;
            pointsTotalText.text = pointsTotal + " pts";
        }
    }

    public void Remove_From_Team()
    {
        if (selectedTeam.Count > 0)
        {
            //team_Dropdown.value
            Team_Unit_Dropdown_IndexChanged(selectedTeamUnitIndex);

            selectedTeam.Remove(selectedTeam[selectedTeamUnitIndex]);
            team_Dropdown.ClearOptions();
            team_Dropdown.AddOptions(selectedTeam);
            team_Unit_Name.Remove(team_Unit_Name[selectedTeamUnitIndex]);
            team_Set_Name.Remove(team_Set_Name[selectedTeamUnitIndex]);
            team_Collectors_Number_Name.Remove(team_Collectors_Number_Name[selectedTeamUnitIndex]);
            team_Unit_GameObject.Remove(team_Unit_GameObject[selectedTeamUnitIndex]);

            pointsTotal -= Points_d;
            pointsTotalText.text = pointsTotal + " pts";

            if (selectedTeam.Count > 0)
            {
                Team_Unit_Dropdown_IndexChanged(0);
            }
        }

    }

    public void Clear_Team()
    {
        //Lame Fix for Clearing team after loading team(without any new set chosen)
        if(set_Dropdown.value == 0)
        {
            set_Dropdown.value = 1;
            Set_Name_Dropdown_IndexChanged(1);

            CallGetUnitsFromSet();

            unit_Dropdown.value = 0;
            Selected_Unit_Dropdown_IndexChanged(0);
        }

        selectedTeam.Clear();
        team_Dropdown.ClearOptions();
        pointsTotal = 0;
        pointsTotalText.text = pointsTotal + " pts";
        team.Clear();
        team_Unit_Name.Clear();
        team_Set_Name.Clear();
        team_Collectors_Number_Name.Clear();
        team_Unit_GameObject.Clear();
        TeamName.text = "";
        IsEditingTeam = false;

        CallLoadTeamNames();

        //if (selectedSet == null || selectedSet == "Sets")
        //{
        //    Selected_Unit_Dropdown_IndexChanged(0);
        //}
    }

    public void Clear_Team_Load()
    {
        //Lame Fix for Clearing team after loading team(without any new set chosen)
        if (set_Dropdown.value == 0)
        {
            set_Dropdown.value = 1;
            Set_Name_Dropdown_IndexChanged(1);

            CallGetUnitsFromSet();

            unit_Dropdown.value = 0;
            Selected_Unit_Dropdown_IndexChanged(0);
        }

        selectedTeam.Clear();
        team_Dropdown.ClearOptions();
        pointsTotal = 0;
        pointsTotalText.text = pointsTotal + " pts";
        team.Clear();
        team_Unit_Name.Clear();
        team_Set_Name.Clear();
        team_Collectors_Number_Name.Clear();
        team_Unit_GameObject.Clear();
        TeamName.text = "";
        IsEditingTeam = false;

        //CallLoadTeamNames();

        //if (selectedSet == null || selectedSet == "Sets")
        //{
        //    Selected_Unit_Dropdown_IndexChanged(0);
        //}
    }

    public void Previous_Keyword()
    {
        if (keywordIndex > 0)
        {
            keywordIndex--;
            CurrentKeyword.text = UnitKeywords[keywordIndex];
        }

    }

    public void Next_Keyword()
    {
        if (keywordIndex < UnitKeywords.Count - 1)
        {
            keywordIndex++;
            CurrentKeyword.text = UnitKeywords[keywordIndex];
        }

    }


    public void Previous_Click()
    {
        if (currentDialIndex > 0)
        {
            //Update_Previewed_Dial(units, selectedUnitIndex, currentDialIndex - 1);
            Update_Previewed_Dial(tempUnitList, 0, currentDialIndex - 1);

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

        if (currentDialIndex + 12 < CurrentlyPreviewedUnit.GetComponent<Unit_Script>().dial_length)
        {

            //Update_Previewed_Dial(units, selectedUnitIndex, currentDialIndex + 1);
            Update_Previewed_Dial(tempUnitList, 0, currentDialIndex + 1);


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

    private Dynamic_Team_Save_Script CreateDynamicSaveTeamObject()
    {
        string[] tempSetArray = new string[team_Unit_GameObject.Count()];
        string[] tempCollectorsNumberArray = new string[team_Unit_GameObject.Count()];
        int teamPoints = 0;
        string teamName = TeamName.text;
        int i = 0;

        foreach (GameObject targetGameObject in team_Unit_GameObject)
        {
            tempSetArray[i] = targetGameObject.GetComponent<Unit_Script>().set;
            tempCollectorsNumberArray[i] = targetGameObject.GetComponent<Unit_Script>().collectors_number;
            teamPoints += targetGameObject.GetComponent<Unit_Script>().points;
            i++;
        }

        Dynamic_Team_Save_Script saveTeam = new Dynamic_Team_Save_Script() { TeamSetNames = tempSetArray, TeamCollectorsNumbers = tempCollectorsNumberArray, teamName = teamName, teamPoints = teamPoints };
        //saveTeam.teamUnitNames = tempNameArray[];
        //saveTeam.teamName = teamName;
        //saveTeam.teamPoints = teamPoints;

        return saveTeam;
    }


    public void SaveDynamicTeam()
    {
        Dynamic_Team_Save_Script currentTeam = CreateDynamicSaveTeamObject();
        string json = JsonUtility.ToJson(currentTeam);

        if (TeamName.text.Length > 0  && pointsTotal > 0)
        {
            if (IsEditingTeam == false)
            {
                //Save Team To Server
                StartCoroutine(SaveTeamToServer(json, TeamName.text, pointsTotal));
            }
            else
            {
                //Save Team To Server
                StartCoroutine(UpdateTeamToServer(json, TeamName.text, pointsTotal));
            }
        }

    }

    IEnumerator SaveTeamToServer(string SaveTeamString, string TeamName, int Points)
    {
        WWWForm form = new WWWForm();
        form.AddField("User_ID", PlayerPrefs.GetInt("User_ID"));
        form.AddField("Saved_Team", SaveTeamString);
        form.AddField("Team_Name", TeamName);
        form.AddField("Points", Points);
        form.AddField("IsPrivate", 0);

        using (UnityWebRequest www = UnityWebRequest.Post("https://www.stark44.com/Heroclix/SaveDynamicTeam.php", form))
        {
            www.certificateHandler = new BypassCertificate();
            yield return www.SendWebRequest();

            if (www.result != UnityWebRequest.Result.Success)
            {
                Debug.Log(www.error);
            }
            else
            {
                if(www.downloadHandler.text == "0")
                {
                    Clear_Team();
                    Debug.Log("Created Team");
                }
                else
                {
                    Debug.Log(www.downloadHandler.text);
                }
            }
        }

        yield return null;
    }

    IEnumerator UpdateTeamToServer(string SaveTeamString, string TeamName, int Points)
    {
        WWWForm form = new WWWForm();
        form.AddField("User_ID", PlayerPrefs.GetInt("User_ID"));
        form.AddField("Saved_Team", SaveTeamString);
        form.AddField("Team_Name", TeamName);
        form.AddField("Points", Points);
        form.AddField("IsPrivate", 0);
        form.AddField("Team_ID", selectedTeamLoadIndex);

        using (UnityWebRequest www = UnityWebRequest.Post("https://www.stark44.com/Heroclix/UpdateDynamicTeam.php", form))
        {
            www.certificateHandler = new BypassCertificate();
            yield return www.SendWebRequest();

            if (www.result != UnityWebRequest.Result.Success)
            {
                Debug.Log(www.error);
            }
            else
            {
                if (www.downloadHandler.text == "0")
                {
                    Clear_Team();
                    Debug.Log("Saved Team");
                    CallLoadTeamNames();

                }
                else
                {
                    Debug.Log(www.downloadHandler.text);
                }
            }
        }

        yield return null;
    }

    public void CallLoadTeamNames()
    {
        StartCoroutine(LoadTeamNames());
    }

    IEnumerator LoadTeamNames()
    {
        loadedTeamsList.Clear();
        TeamIDList.Clear();
        TeamNameList.Clear();
        TeamPointsList.Clear();
        team_Name_Dropdown.ClearOptions();

        string WhereClauseText="";

        WWWForm form = new WWWForm();

        if (YourTeamsOnly.isOn == true)
        {
            WhereClauseText = "where User_ID = " + PlayerPrefs.GetInt("User_ID");
        }

        form.AddField("Where_Clause", WhereClauseText);

        using (UnityWebRequest www2 = UnityWebRequest.Post("https://www.stark44.com/Heroclix/getTeamNames.php",form))
        {
            www2.certificateHandler = new BypassCertificate();
            yield return www2.SendWebRequest();

            if (www2.result != UnityWebRequest.Result.Success)
            {
                Debug.Log(www2.error);
            }
            else
            {
                string Teams = www2.downloadHandler.text;
                int i = 0;

                while (Teams.Contains(";") && Teams.Length > 1)
                {
                    var IteratedRowData = Teams.Substring(0, Teams.IndexOf(";")).Trim();

                    var parts = IteratedRowData.Split('\t');
                    if (parts.Length < 3)
                    {
                        Teams = Teams.Substring(Teams.IndexOf(";") + 1, Teams.Length - Teams.IndexOf(";") - 1);
                        continue;
                    }

                    TeamIDList.Add(parts[0].Trim());
                    TeamNameList.Add(parts[1].Trim());
                    TeamPointsList.Add(parts[2].Trim());

                    loadedTeamsList.Add(TeamNameList[i] + " " + TeamPointsList[i] + " pts");

                    Teams = Teams.Substring(Teams.IndexOf(";") + 1, Teams.Length - Teams.IndexOf(";") - 1);
                    i++;
                }

            }
        }
        team_Name_Dropdown.AddOptions(loadedTeamsList);
        if (team_Name_Dropdown.options.Count > 0)
        {
            Team_Name_Dropdown_IndexChanged(0);
        }

        Debug.Log("Fetched Teams");
        yield return null;
    }


    public void CallDeleteTeam()
    {
        if (!IsEditingTeam)
        {
            int dropdownIndex = team_Name_Dropdown.value;
            if (TeamIDList.Count == 0 || dropdownIndex >= TeamIDList.Count)
            {
                Debug.Log("CallDeleteTeam: team list not loaded");
                return;
            }
            if (int.TryParse(TeamIDList[dropdownIndex], out int teamId) && teamId > 0)
            {
                selectedTeamLoadIndex = teamId;
                StartCoroutine(DeleteTeam());
            }
        }

    }

    IEnumerator DeleteTeam()
    {
        loadedTeamsList.Clear();
        team_Name_Dropdown.ClearOptions();

        WWWForm form = new WWWForm();

        form.AddField("Team_ID", selectedTeamLoadIndex);
        form.AddField("User_ID", PlayerPrefs.GetInt("User_ID"));

        using (UnityWebRequest www2 = UnityWebRequest.Post("https://www.stark44.com/Heroclix/DeleteTeam.php",form))
        {
            www2.certificateHandler = new BypassCertificate();
            yield return www2.SendWebRequest();

            if (www2.result != UnityWebRequest.Result.Success)
            {
                Debug.Log(www2.error);
            }
            else
            {
                if(www2.downloadHandler.text == "0")
                {
                    Debug.Log("Team Deleted.");
                    CallLoadTeamNames();
                }
                else
                {
                    Debug.Log(www2.downloadHandler.text);
                }

            }
        }

        yield return null;
    }

    public void CallCopyTeam()
    {
        int dropdownIndex = team_Name_Dropdown.value;
        if (TeamIDList.Count == 0 || dropdownIndex >= TeamIDList.Count)
        {
            Debug.Log("CallCopyTeam: team list not loaded");
            return;
        }
        if (int.TryParse(TeamIDList[dropdownIndex], out int teamId) && teamId > 0)
        {
            selectedTeamLoadIndex = teamId;
            StartCoroutine(CopyTeam());
        }

    }

    IEnumerator CopyTeam()
    {
        loadedTeamsList.Clear();
        team_Name_Dropdown.ClearOptions();

        WWWForm form = new WWWForm();

        form.AddField("Team_ID", selectedTeamLoadIndex);
        form.AddField("User_ID", PlayerPrefs.GetInt("User_ID"));

        using (UnityWebRequest www2 = UnityWebRequest.Post("https://www.stark44.com/Heroclix/CopyTeam.php", form))
        {
            www2.certificateHandler = new BypassCertificate();
            yield return www2.SendWebRequest();

            if (www2.result != UnityWebRequest.Result.Success)
            {
                Debug.Log(www2.error);
            }
            else
            {
                if (www2.downloadHandler.text == "0")
                {
                    Debug.Log("Team Copied.");
                    CallLoadTeamNames();
                }
                else
                {
                    Debug.Log(www2.downloadHandler.text);
                }

            }
        }

        yield return null;
    }

    public void CallLoadTeam()
    {
        if (!IsEditingTeam)
        {
            int dropdownIndex = team_Name_Dropdown.value;
            if (TeamIDList.Count == 0 || dropdownIndex >= TeamIDList.Count)
            {
                Debug.Log("CallLoadTeam: team list not loaded yet (count=" + TeamIDList.Count + ")");
                return;
            }
            if (int.TryParse(TeamIDList[dropdownIndex], out int teamId) && teamId > 0)
            {
                selectedTeamLoadIndex = teamId;
                StartCoroutine(LoadTeam());
            }
            else
            {
                Debug.Log("CallLoadTeam: could not parse team ID from '" + TeamIDList[dropdownIndex] + "'");
            }
        }

    }

    IEnumerator CheckTeam()
    {
        WWWForm form = new WWWForm();

        form.AddField("Team_ID", selectedTeamLoadIndex);
        form.AddField("User_ID", PlayerPrefs.GetInt("User_ID"));

        using (UnityWebRequest www2 = UnityWebRequest.Post("https://www.stark44.com/Heroclix/CheckTeam.php", form))
        {
            www2.certificateHandler = new BypassCertificate();
            yield return www2.SendWebRequest();

            if (www2.result != UnityWebRequest.Result.Success)
            {
                Debug.Log(www2.error);
            }
            else
            {
                if (www2.downloadHandler.text == "0")
                {
                    Debug.Log("This is your team.");

                    yield return StartCoroutine(LoadTeam());

                }
                else
                {
                    Debug.Log(www2.downloadHandler.text);
                }

            }
        }

        yield return null;
    }

    IEnumerator LoadTeam()
    {
        WWWForm form = new WWWForm();

        form.AddField("Team_ID", selectedTeamLoadIndex);
        form.AddField("User_ID", PlayerPrefs.GetInt("User_ID"));

        using (UnityWebRequest www2 = UnityWebRequest.Post("https://www.stark44.com/Heroclix/LoadTeam.php", form))
        {
            www2.certificateHandler = new BypassCertificate();
            yield return www2.SendWebRequest();

            if (www2.result != UnityWebRequest.Result.Success)
            {
                Debug.Log(www2.error);
            }
            else
            {

                    string TeamData = www2.downloadHandler.text;

                    var NextTab = TeamData.IndexOf(",");
                    if (NextTab < 0)
                    {
                        Debug.Log("LoadTeam: no team data returned for ID " + selectedTeamLoadIndex);
                        yield break;
                    }

                    Debug.Log("Team Loaded.");

                    //Second To Last
                    TeamName.text = TeamData.Substring(0, NextTab);
                    TeamData = TeamData.Substring(NextTab + 1, TeamData.Length - NextTab - 1);

                    //Last
                    string SavedTeamData = TeamData.Substring(0, TeamData.Length);

                    Dynamic_Team_Save_Script loadedTeam = new Dynamic_Team_Save_Script();

                    var JSONStartIndex = SavedTeamData.IndexOf('{');
                    var JSONEndIndex = SavedTeamData.LastIndexOf('}');
                    if (JSONStartIndex < 0 || JSONEndIndex < 0)
                    {
                        Debug.Log("LoadTeam: invalid JSON in team data");
                        yield break;
                    }
                    SavedTeamData = SavedTeamData.Substring(JSONStartIndex, JSONEndIndex - JSONStartIndex + 1);

                   loadedTeam = JsonUtility.FromJson<Dynamic_Team_Save_Script>(SavedTeamData);

                    int i = 0;

                    Clear_Team_Load();

                    TeamName.text = loadedTeam.teamName;

                    foreach (string unit in loadedTeam.TeamSetNames)
                    {
                        SelectedCollectorsNumber = loadedTeam.TeamCollectorsNumbers[i];

                        Selected_Dynamic_Set_Name = loadedTeam.TeamSetNames[i];
                        Selected_Dynamic_Collectors_Number = loadedTeam.TeamCollectorsNumbers[i];

                        yield return StartCoroutine(GetDynamicUnitDial());

                        yield return StartCoroutine(GetDynamicUnitStats());

                        LoadDynamicUnit();
                        Load_Add_To_Team();
                        IsEditingTeam = true;
                        i++;
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
        sets.Clear();
        set_Dropdown.ClearOptions();

        using (UnityWebRequest www = UnityWebRequest.Get("https://www.stark44.com/Heroclix/getDynamicUnitSetsOfficial.php"))
        {
            www.certificateHandler = new BypassCertificate();
            yield return www.SendWebRequest();

            if (www.result != UnityWebRequest.Result.Success)
            {
                Debug.Log(www.error);
            }
            else
            {
                string DBOfficialSets = www.downloadHandler.text;

                sets.Add("Sets");

                while (DBOfficialSets.Contains(",") && DBOfficialSets.Length > 1)
                {
                    sets.Add(DBOfficialSets.Substring(0, DBOfficialSets.IndexOf(",")));
                    DBOfficialSets = DBOfficialSets.Substring(DBOfficialSets.IndexOf(",") + 1, DBOfficialSets.Length - DBOfficialSets.IndexOf(",") - 1);
                }

                set_Dropdown.AddOptions(sets);
            }
        }
        Set_Name_Dropdown_IndexChanged(0);

    }

    public void CallGetSetNames_AdvancedSearch()
    {
        StartCoroutine(GetSetNames_AdvancedSearch());
    }

    IEnumerator GetSetNames_AdvancedSearch()
    {
        sets_AS.Clear();
        set_Dropdown_Advanced_Search.ClearOptions();

        using (UnityWebRequest www = UnityWebRequest.Get("https://www.stark44.com/Heroclix/getDynamicUnitSetsOfficial.php"))
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
                string DBOfficialSets = www.downloadHandler.text;

                sets_AS.Add("Sets");

                while (DBOfficialSets.Contains(",") && DBOfficialSets.Length > 1)
                {
                    sets_AS.Add(DBOfficialSets.Substring(0, DBOfficialSets.IndexOf(",")));
                    DBOfficialSets = DBOfficialSets.Substring(DBOfficialSets.IndexOf(",") + 1, DBOfficialSets.Length - DBOfficialSets.IndexOf(",") - 1);
                }

                set_Dropdown_Advanced_Search.AddOptions(sets_AS);
            }
        }
        Set_Name_Dropdown_Advanced_Search_IndexChanged(0);

    }

    public void CallGetTADesc()
    {
        StartCoroutine(GetTADesc());
    }

    IEnumerator GetTADesc()
    {
        WWWForm form = new WWWForm();

        form.AddField("Team_Ability", teamAbility);

        using (UnityWebRequest www = UnityWebRequest.Post("https://www.stark44.com/Heroclix/getTADesc.php",form))
        {
            www.certificateHandler = new BypassCertificate();
            yield return www.SendWebRequest();

            if (www.result != UnityWebRequest.Result.Success)
            {
                Debug.Log(www.error);
            }
            else
            {
                string TA = www.downloadHandler.text;
                Debug.Log(TA);
                teamAbilityDescription = TA;
            }
        }

        yield return null;
    }

    public void Set_Name_Dropdown_IndexChanged(int index)
    {
        if (sets.Count > 0)
        {
            selectedSet = sets[index];
            if (selectedSet != "Sets")
            {
                CallGetUnitsFromSet();
            }
            else
            {
                unit_Dropdown.ClearOptions();
            }
        }
    }

    public void Set_Name_Dropdown_Advanced_Search_IndexChanged(int index)
    {
        if (sets.Count > 0)
        {
            selectedSet = sets[index];
        }
    }

    public void CallGetUnitsFromSet()
    {
        StartCoroutine(GetUnitsFromSet());
    }

    IEnumerator GetUnitsFromSet()
    {
        SetDropDownDataList.Clear();
        SetCollectorsNumberList.Clear();
        SetUnitNameList.Clear();
        UnitsNameDropdown.ClearOptions();

        WWWForm form = new WWWForm();
        form.AddField("Set_Name", selectedSet);

        using (UnityWebRequest www = UnityWebRequest.Post("https://www.stark44.com/Heroclix/getDynamicUnitsFromSet.php", form))
        {
            www.certificateHandler = new BypassCertificate();
            yield return www.SendWebRequest();

            if (www.result != UnityWebRequest.Result.Success)
            {
                Debug.Log(www.error);
            }
            else
            {
                string DBUnits = www.downloadHandler.text;

                while (DBUnits.Contains(";") && DBUnits.Length > 1)
                {
                    var IteratedRowData = DBUnits.Substring(0, DBUnits.IndexOf(";"));
                    var DropdownData = IteratedRowData;
                    var NextTab = IteratedRowData.IndexOf("\t");

                    var Collectors_Num = IteratedRowData.Substring(0, NextTab);
                    NextTab = IteratedRowData.IndexOf("\t");
                    IteratedRowData = IteratedRowData.Substring(NextTab + 1, IteratedRowData.Length - NextTab - 1);
                    NextTab = IteratedRowData.IndexOf("\t");

                    var Unit_Name = IteratedRowData.Substring(0, NextTab);
                    NextTab = IteratedRowData.IndexOf("\t");
                    IteratedRowData = IteratedRowData.Substring(NextTab + 1, IteratedRowData.Length - NextTab - 1);

                    var Points = IteratedRowData.Substring(0, IteratedRowData.Length);

                    SetDropDownDataList.Add(DropdownData);
                    SetCollectorsNumberList.Add(Collectors_Num);
                    SetUnitNameList.Add(Unit_Name);


                    DBUnits = DBUnits.Substring(DBUnits.IndexOf(";") + 1, DBUnits.Length - DBUnits.IndexOf(";") - 1);
                }




            }
        }
        UnitsNameDropdown.AddOptions(SetDropDownDataList);
        Selected_Unit_Dropdown_IndexChanged(0);

        yield return null;
    }

    public void Selected_Unit_Dropdown_IndexChanged(int index)
    {
        PowerDescription.text = "";
        SelectedUnit = SetUnitNameList[index];
        SelectedUnitIndex = index;
        SelectedCollectorsNumber = SetCollectorsNumberList[index];

        Selected_Dynamic_Set_Name = selectedSet;
        Selected_Dynamic_Collectors_Number = SelectedCollectorsNumber;

        StartCoroutine(LoadSearchedUnit());
    }

    IEnumerator GetDynamicUnitStats()
    {
        WWWForm form = new WWWForm();
        form.AddField("Set_Name", Selected_Dynamic_Set_Name);
        form.AddField("Collectors_Number", Selected_Dynamic_Collectors_Number);

        using (UnityWebRequest www = UnityWebRequest.Post("https://www.stark44.com/Heroclix/getDynamicUnitStats.php", form))
        {
            www.certificateHandler = new BypassCertificate();
            yield return www.SendWebRequest();

            if (www.result != UnityWebRequest.Result.Success)
            {
                Debug.Log(www.error);
            }
            else
            {
                string DBUnits = www.downloadHandler.text;

                if (DBUnits.Length > 0)
                {
                    var IteratedRowData = DBUnits.Substring(0, DBUnits.IndexOf(";"));
                    var DropdownData = IteratedRowData;
                    var NextTab = IteratedRowData.IndexOf("\t");

                    Set_Name_d = IteratedRowData.Substring(0, NextTab);
                    NextTab = IteratedRowData.IndexOf("\t");
                    IteratedRowData = IteratedRowData.Substring(NextTab + 1, IteratedRowData.Length - NextTab - 1);
                    NextTab = IteratedRowData.IndexOf("\t");

                    Collectors_Number_d = IteratedRowData.Substring(0, NextTab);
                    NextTab = IteratedRowData.IndexOf("\t");
                    IteratedRowData = IteratedRowData.Substring(NextTab + 1, IteratedRowData.Length - NextTab - 1);
                    NextTab = IteratedRowData.IndexOf("\t");

                    Unit_Name_d = IteratedRowData.Substring(0, NextTab);
                    NextTab = IteratedRowData.IndexOf("\t");
                    IteratedRowData = IteratedRowData.Substring(NextTab + 1, IteratedRowData.Length - NextTab - 1);
                    NextTab = IteratedRowData.IndexOf("\t");

                    Dial_Length_d = int.Parse(IteratedRowData.Substring(0, NextTab));
                    NextTab = IteratedRowData.IndexOf("\t");
                    IteratedRowData = IteratedRowData.Substring(NextTab + 1, IteratedRowData.Length - NextTab - 1);
                    NextTab = IteratedRowData.IndexOf("\t");

                    Last_Click_d = int.Parse(IteratedRowData.Substring(0, NextTab));
                    NextTab = IteratedRowData.IndexOf("\t");
                    IteratedRowData = IteratedRowData.Substring(NextTab + 1, IteratedRowData.Length - NextTab - 1);
                    NextTab = IteratedRowData.IndexOf("\t");

                    Points_d = int.Parse(IteratedRowData.Substring(0, NextTab));
                    NextTab = IteratedRowData.IndexOf("\t");
                    IteratedRowData = IteratedRowData.Substring(NextTab + 1, IteratedRowData.Length - NextTab - 1);
                    NextTab = IteratedRowData.IndexOf("\t");

                    Range_Value_d = int.Parse(IteratedRowData.Substring(0, NextTab));
                    NextTab = IteratedRowData.IndexOf("\t");
                    IteratedRowData = IteratedRowData.Substring(NextTab + 1, IteratedRowData.Length - NextTab - 1);
                    NextTab = IteratedRowData.IndexOf("\t");

                    Range_Targets_d = int.Parse(IteratedRowData.Substring(0, NextTab));
                    NextTab = IteratedRowData.IndexOf("\t");
                    IteratedRowData = IteratedRowData.Substring(NextTab + 1, IteratedRowData.Length - NextTab - 1);
                    NextTab = IteratedRowData.IndexOf("\t");

                    Experience_d = IteratedRowData.Substring(0, NextTab);
                    NextTab = IteratedRowData.IndexOf("\t");
                    IteratedRowData = IteratedRowData.Substring(NextTab + 1, IteratedRowData.Length - NextTab - 1);
                    NextTab = IteratedRowData.IndexOf("\t");

                    Rarity_d = IteratedRowData.Substring(0, NextTab);
                    NextTab = IteratedRowData.IndexOf("\t");
                    IteratedRowData = IteratedRowData.Substring(NextTab + 1, IteratedRowData.Length - NextTab - 1);
                    NextTab = IteratedRowData.IndexOf("\t");

                    Base_Type_d = IteratedRowData.Substring(0, NextTab);
                    NextTab = IteratedRowData.IndexOf("\t");
                    IteratedRowData = IteratedRowData.Substring(NextTab + 1, IteratedRowData.Length - NextTab - 1);
                    NextTab = IteratedRowData.IndexOf("\t");

                    Speed_Symbol_d = IteratedRowData.Substring(0, NextTab);
                    NextTab = IteratedRowData.IndexOf("\t");
                    IteratedRowData = IteratedRowData.Substring(NextTab + 1, IteratedRowData.Length - NextTab - 1);
                    NextTab = IteratedRowData.IndexOf("\t");

                    Attack_Symbol_d = IteratedRowData.Substring(0, NextTab);
                    NextTab = IteratedRowData.IndexOf("\t");
                    IteratedRowData = IteratedRowData.Substring(NextTab + 1, IteratedRowData.Length - NextTab - 1);
                    NextTab = IteratedRowData.IndexOf("\t");

                    Defense_Symbol_d = IteratedRowData.Substring(0, NextTab);
                    NextTab = IteratedRowData.IndexOf("\t");
                    IteratedRowData = IteratedRowData.Substring(NextTab + 1, IteratedRowData.Length - NextTab - 1);
                    NextTab = IteratedRowData.IndexOf("\t");

                    Damage_Symbol_d = IteratedRowData.Substring(0, NextTab);
                    NextTab = IteratedRowData.IndexOf("\t");
                    IteratedRowData = IteratedRowData.Substring(NextTab + 1, IteratedRowData.Length - NextTab - 1);
                    NextTab = IteratedRowData.IndexOf("\t");

                    IM_Elevated_d = System.Convert.ToBoolean(int.Parse(IteratedRowData.Substring(0, NextTab)));
                    NextTab = IteratedRowData.IndexOf("\t");
                    IteratedRowData = IteratedRowData.Substring(NextTab + 1, IteratedRowData.Length - NextTab - 1);
                    NextTab = IteratedRowData.IndexOf("\t");

                    IM_Hindering_d = System.Convert.ToBoolean(int.Parse(IteratedRowData.Substring(0, NextTab)));
                    NextTab = IteratedRowData.IndexOf("\t");
                    IteratedRowData = IteratedRowData.Substring(NextTab + 1, IteratedRowData.Length - NextTab - 1);
                    NextTab = IteratedRowData.IndexOf("\t");

                    IM_Water_d = System.Convert.ToBoolean(int.Parse(IteratedRowData.Substring(0, NextTab)));
                    NextTab = IteratedRowData.IndexOf("\t");
                    IteratedRowData = IteratedRowData.Substring(NextTab + 1, IteratedRowData.Length - NextTab - 1);
                    NextTab = IteratedRowData.IndexOf("\t");

                    IM_Blocking_d = System.Convert.ToBoolean(int.Parse(IteratedRowData.Substring(0, NextTab)));
                    NextTab = IteratedRowData.IndexOf("\t");
                    IteratedRowData = IteratedRowData.Substring(NextTab + 1, IteratedRowData.Length - NextTab - 1);
                    NextTab = IteratedRowData.IndexOf("\t");

                    IM_Outdoor_Blocking_d = System.Convert.ToBoolean(int.Parse(IteratedRowData.Substring(0, NextTab)));
                    NextTab = IteratedRowData.IndexOf("\t");
                    IteratedRowData = IteratedRowData.Substring(NextTab + 1, IteratedRowData.Length - NextTab - 1);
                    NextTab = IteratedRowData.IndexOf("\t");

                    IM_Blocking_Destroy_d = System.Convert.ToBoolean(int.Parse(IteratedRowData.Substring(0, NextTab)));
                    NextTab = IteratedRowData.IndexOf("\t");
                    IteratedRowData = IteratedRowData.Substring(NextTab + 1, IteratedRowData.Length - NextTab - 1);
                    NextTab = IteratedRowData.IndexOf("\t");

                    IM_Characters_d = System.Convert.ToBoolean(int.Parse(IteratedRowData.Substring(0, NextTab)));
                    NextTab = IteratedRowData.IndexOf("\t");
                    IteratedRowData = IteratedRowData.Substring(NextTab + 1, IteratedRowData.Length - NextTab - 1);
                    NextTab = IteratedRowData.IndexOf("\t");

                    IM_Adjacent_Characters_d = System.Convert.ToBoolean(int.Parse(IteratedRowData.Substring(0, NextTab)));
                    NextTab = IteratedRowData.IndexOf("\t");
                    IteratedRowData = IteratedRowData.Substring(NextTab + 1, IteratedRowData.Length - NextTab - 1);
                    NextTab = IteratedRowData.IndexOf("\t");

                    IT_Elevated_d = System.Convert.ToBoolean(int.Parse(IteratedRowData.Substring(0, NextTab)));
                    NextTab = IteratedRowData.IndexOf("\t");
                    IteratedRowData = IteratedRowData.Substring(NextTab + 1, IteratedRowData.Length - NextTab - 1);
                    NextTab = IteratedRowData.IndexOf("\t");

                    IT_Hindering_d = System.Convert.ToBoolean(int.Parse(IteratedRowData.Substring(0, NextTab)));
                    NextTab = IteratedRowData.IndexOf("\t");
                    IteratedRowData = IteratedRowData.Substring(NextTab + 1, IteratedRowData.Length - NextTab - 1);
                    NextTab = IteratedRowData.IndexOf("\t");

                    IT_Blocking_d = System.Convert.ToBoolean(int.Parse(IteratedRowData.Substring(0, NextTab)));
                    NextTab = IteratedRowData.IndexOf("\t");
                    IteratedRowData = IteratedRowData.Substring(NextTab + 1, IteratedRowData.Length - NextTab - 1);
                    NextTab = IteratedRowData.IndexOf("\t");

                    IT_Blocking_Destroy_d = System.Convert.ToBoolean(int.Parse(IteratedRowData.Substring(0, NextTab)));
                    NextTab = IteratedRowData.IndexOf("\t");
                    IteratedRowData = IteratedRowData.Substring(NextTab + 1, IteratedRowData.Length - NextTab - 1);
                    NextTab = IteratedRowData.IndexOf("\t");

                    IT_Characters_d = System.Convert.ToBoolean(int.Parse(IteratedRowData.Substring(0, NextTab)));
                    NextTab = IteratedRowData.IndexOf("\t");
                    IteratedRowData = IteratedRowData.Substring(NextTab + 1, IteratedRowData.Length - NextTab - 1);
                    NextTab = IteratedRowData.IndexOf("\t");

                    IT_Adjacent_Characters_d = System.Convert.ToBoolean(int.Parse(IteratedRowData.Substring(0, NextTab)));
                    NextTab = IteratedRowData.IndexOf("\t");
                    IteratedRowData = IteratedRowData.Substring(NextTab + 1, IteratedRowData.Length - NextTab - 1);
                    NextTab = IteratedRowData.IndexOf("\t");

                    Keywords_d = IteratedRowData.Substring(0, NextTab);
                    NextTab = IteratedRowData.IndexOf("\t");
                    IteratedRowData = IteratedRowData.Substring(NextTab + 1, IteratedRowData.Length - NextTab - 1);
                    NextTab = IteratedRowData.IndexOf("\t");

                    Team_Ability_d = IteratedRowData.Substring(0, NextTab);
                    NextTab = IteratedRowData.IndexOf("\t");
                    IteratedRowData = IteratedRowData.Substring(NextTab + 1, IteratedRowData.Length - NextTab - 1);
                    NextTab = IteratedRowData.IndexOf("\t");

                    Trait1_d = IteratedRowData.Substring(0, NextTab);
                    NextTab = IteratedRowData.IndexOf("\t");
                    IteratedRowData = IteratedRowData.Substring(NextTab + 1, IteratedRowData.Length - NextTab - 1);
                    NextTab = IteratedRowData.IndexOf("\t");

                    Trait2_d = IteratedRowData.Substring(0, NextTab);
                    NextTab = IteratedRowData.IndexOf("\t");
                    IteratedRowData = IteratedRowData.Substring(NextTab + 1, IteratedRowData.Length - NextTab - 1);
                    NextTab = IteratedRowData.IndexOf("\t");

                    Trait3_d = IteratedRowData.Substring(0, NextTab);
                    NextTab = IteratedRowData.IndexOf("\t");
                    IteratedRowData = IteratedRowData.Substring(NextTab + 1, IteratedRowData.Length - NextTab - 1);
                    NextTab = IteratedRowData.IndexOf("\t");

                    Trait4_d = IteratedRowData.Substring(0, NextTab);
                    NextTab = IteratedRowData.IndexOf("\t");
                    IteratedRowData = IteratedRowData.Substring(NextTab + 1, IteratedRowData.Length - NextTab - 1);
                    NextTab = IteratedRowData.IndexOf("\t");

                    //Second To Last
                    Trait5_d = IteratedRowData.Substring(0, NextTab);
                    NextTab = IteratedRowData.IndexOf("\t");
                    IteratedRowData = IteratedRowData.Substring(NextTab + 1, IteratedRowData.Length - NextTab - 1);
                    NextTab = IteratedRowData.IndexOf("\t");

                    Trait6_d = IteratedRowData.Substring(0, NextTab);
                    NextTab = IteratedRowData.IndexOf("\t");
                    IteratedRowData = IteratedRowData.Substring(NextTab + 1, IteratedRowData.Length - NextTab - 1);

                    //Last
                    Set_Abbrev_d = IteratedRowData.Substring(0, IteratedRowData.Length);

                    //DBUnits = DBUnits.Substring(DBUnits.IndexOf(";") + 1, DBUnits.Length - DBUnits.IndexOf(";") - 1);
                }

            }

        }
        Debug.Log("Non-Load Stats Finished.");
        yield return null;
    }

    IEnumerator GetDynamicUnitDial()
    {
        DialInfoDynamic_Slot.Clear();
        DialInfoDynamic_SpeedValue.Clear();
        DialInfoDynamic_AttackValue.Clear();
        DialInfoDynamic_DefenseValue.Clear();
        DialInfoDynamic_DamageValue.Clear();
        DialInfoDynamic_SpeedPowerInt.Clear();
        DialInfoDynamic_AttackPowerInt.Clear();
        DialInfoDynamic_DefensePowerInt.Clear();
        DialInfoDynamic_DamagePowerInt.Clear();
        DialInfoDynamic_SpeedPowerStr.Clear();
        DialInfoDynamic_AttackPowerStr.Clear();
        DialInfoDynamic_DefensePowerStr.Clear();
        DialInfoDynamic_DamagePowerStr.Clear();
        DialInfoDynamic_SpeedPowerDescription.Clear();
        DialInfoDynamic_AttackPowerDescription.Clear();
        DialInfoDynamic_DefensePowerDescription.Clear();
        DialInfoDynamic_DamagePowerDescription.Clear();

        WWWForm form = new WWWForm();
        form.AddField("Set_Name", Selected_Dynamic_Set_Name);
        form.AddField("Collectors_Number", Selected_Dynamic_Collectors_Number);

        using (UnityWebRequest www = UnityWebRequest.Post("https://www.stark44.com/Heroclix/getDynamicUnitDial.php", form))
        {
            www.certificateHandler = new BypassCertificate();
            yield return www.SendWebRequest();

            if (www.result != UnityWebRequest.Result.Success)
            {
                Debug.Log(www.error);
            }
            else
            {
                string DBUnits = www.downloadHandler.text;

                while (DBUnits.Contains(";") && DBUnits.Length > 1)
                {
                    var IteratedRowData = DBUnits.Substring(0, DBUnits.IndexOf(";"));
                    var DropdownData = IteratedRowData;
                    var NextTab = IteratedRowData.IndexOf("\t");

                    Set_Name_d = IteratedRowData.Substring(0, NextTab);
                    NextTab = IteratedRowData.IndexOf("\t");
                    IteratedRowData = IteratedRowData.Substring(NextTab + 1, IteratedRowData.Length - NextTab - 1);
                    NextTab = IteratedRowData.IndexOf("\t");

                    Collectors_Number_d = IteratedRowData.Substring(0, NextTab);
                    NextTab = IteratedRowData.IndexOf("\t");
                    IteratedRowData = IteratedRowData.Substring(NextTab + 1, IteratedRowData.Length - NextTab - 1);
                    NextTab = IteratedRowData.IndexOf("\t");

                    DialInfoDynamic_Slot.Add(int.Parse(IteratedRowData.Substring(0, NextTab)));
                    NextTab = IteratedRowData.IndexOf("\t");
                    IteratedRowData = IteratedRowData.Substring(NextTab + 1, IteratedRowData.Length - NextTab - 1);
                    NextTab = IteratedRowData.IndexOf("\t");

                    DialInfoDynamic_SpeedValue.Add(int.Parse(IteratedRowData.Substring(0, NextTab)));
                    NextTab = IteratedRowData.IndexOf("\t");
                    IteratedRowData = IteratedRowData.Substring(NextTab + 1, IteratedRowData.Length - NextTab - 1);
                    NextTab = IteratedRowData.IndexOf("\t");

                    DialInfoDynamic_AttackValue.Add(int.Parse(IteratedRowData.Substring(0, NextTab)));
                    NextTab = IteratedRowData.IndexOf("\t");
                    IteratedRowData = IteratedRowData.Substring(NextTab + 1, IteratedRowData.Length - NextTab - 1);
                    NextTab = IteratedRowData.IndexOf("\t");

                    DialInfoDynamic_DefenseValue.Add(int.Parse(IteratedRowData.Substring(0, NextTab)));
                    NextTab = IteratedRowData.IndexOf("\t");
                    IteratedRowData = IteratedRowData.Substring(NextTab + 1, IteratedRowData.Length - NextTab - 1);
                    NextTab = IteratedRowData.IndexOf("\t");

                    DialInfoDynamic_DamageValue.Add(int.Parse(IteratedRowData.Substring(0, NextTab)));
                    NextTab = IteratedRowData.IndexOf("\t");
                    IteratedRowData = IteratedRowData.Substring(NextTab + 1, IteratedRowData.Length - NextTab - 1);
                    NextTab = IteratedRowData.IndexOf("\t");

                    // Power names stored directly as strings — parse into Str lists
                    DialInfoDynamic_SpeedPowerStr.Add(IteratedRowData.Substring(0, NextTab));
                    NextTab = IteratedRowData.IndexOf("\t");
                    IteratedRowData = IteratedRowData.Substring(NextTab + 1, IteratedRowData.Length - NextTab - 1);
                    NextTab = IteratedRowData.IndexOf("\t");

                    DialInfoDynamic_AttackPowerStr.Add(IteratedRowData.Substring(0, NextTab));
                    NextTab = IteratedRowData.IndexOf("\t");
                    IteratedRowData = IteratedRowData.Substring(NextTab + 1, IteratedRowData.Length - NextTab - 1);
                    NextTab = IteratedRowData.IndexOf("\t");

                    //Second To Last
                    DialInfoDynamic_DefensePowerStr.Add(IteratedRowData.Substring(0, NextTab));
                    NextTab = IteratedRowData.IndexOf("\t");
                    IteratedRowData = IteratedRowData.Substring(NextTab + 1, IteratedRowData.Length - NextTab - 1);

                    //Last
                    DialInfoDynamic_DamagePowerStr.Add(IteratedRowData.Substring(0, IteratedRowData.Length));


                    DBUnits = DBUnits.Substring(DBUnits.IndexOf(";") + 1, DBUnits.Length - DBUnits.IndexOf(";") - 1);
                }

            }

        }

        // Populate descriptions by looking up each power name in AllPowerNamesList
        for (int slot = 0; slot < DialInfoDynamic_Slot.Count; slot++)
        {
            DialInfoDynamic_SpeedPowerDescription.Add(LookupPowerDescription(DialInfoDynamic_SpeedPowerStr[slot]));
            DialInfoDynamic_AttackPowerDescription.Add(LookupPowerDescription(DialInfoDynamic_AttackPowerStr[slot]));
            DialInfoDynamic_DefensePowerDescription.Add(LookupPowerDescription(DialInfoDynamic_DefensePowerStr[slot]));
            DialInfoDynamic_DamagePowerDescription.Add(LookupPowerDescription(DialInfoDynamic_DamagePowerStr[slot]));
        }
        yield return StartCoroutine(GetDynamicUnitSpecialPowers());
        Debug.Log("Non-Load Dial Finished.");
        yield return null;
    }


    public void LoadDynamicUnit()
    {
        PreviewUnitList.Clear();

        var allUnitsDynamic = Resources.LoadAll("Units/Dynamic", typeof(GameObject));

        foreach (GameObject DynamicUnit in allUnitsDynamic)
        {
            GameObject newUnit1 = GameObject.Instantiate(DynamicUnit, new Vector3(-7.5f, -0.5f, -9.5f), Quaternion.identity);

            newUnit1.GetComponent<Unit_Script>().speed_values = DialInfoDynamic_SpeedValue;
            newUnit1.GetComponent<Unit_Script>().attack_values = DialInfoDynamic_AttackValue;
            newUnit1.GetComponent<Unit_Script>().defense_values = DialInfoDynamic_DefenseValue;
            newUnit1.GetComponent<Unit_Script>().damage_values = DialInfoDynamic_DamageValue;

            newUnit1.GetComponent<Unit_Script>().speed_powers = DialInfoDynamic_SpeedPowerStr;
            newUnit1.GetComponent<Unit_Script>().attack_powers = DialInfoDynamic_AttackPowerStr;
            newUnit1.GetComponent<Unit_Script>().defense_powers = DialInfoDynamic_DefensePowerStr;
            newUnit1.GetComponent<Unit_Script>().damage_powers = DialInfoDynamic_DamagePowerStr;

            newUnit1.GetComponent<Unit_Script>().speed_powers_description = DialInfoDynamic_SpeedPowerDescription;
            newUnit1.GetComponent<Unit_Script>().attack_powers_description = DialInfoDynamic_AttackPowerDescription;
            newUnit1.GetComponent<Unit_Script>().defense_powers_description = DialInfoDynamic_DefensePowerDescription;
            newUnit1.GetComponent<Unit_Script>().damage_powers_description = DialInfoDynamic_DamagePowerDescription;

            newUnit1.name = Set_Name_d + "_" + Collectors_Number_d + "_" + Unit_Name_d;

            newUnit1.GetComponent<Unit_Script>().actionTokens = 0;
            newUnit1.GetComponent<Unit_Script>().click_number = 1;
            newUnit1.GetComponent<Unit_Script>().isDynamic = 1;

            newUnit1.GetComponent<Unit_Script>().set = Set_Name_d;
            newUnit1.GetComponent<Unit_Script>().set_abbreviation = Set_Abbrev_d;
            newUnit1.GetComponent<Unit_Script>().collectors_number = Collectors_Number_d;
            newUnit1.GetComponent<Unit_Script>().unit_Name = Unit_Name_d;

            newUnit1.GetComponent<Unit_Script>().dial_length = Dial_Length_d;
            newUnit1.GetComponent<Unit_Script>().last_click = Last_Click_d;
            newUnit1.GetComponent<Unit_Script>().points = Points_d;
            newUnit1.GetComponent<Unit_Script>().range_targets = Range_Targets_d;
            newUnit1.GetComponent<Unit_Script>().range = Range_Value_d;

            newUnit1.GetComponent<Unit_Script>().experience = Experience_d;
            newUnit1.GetComponent<Unit_Script>().rarity = Rarity_d;
            newUnit1.GetComponent<Unit_Script>().base_type = Base_Type_d;
            newUnit1.GetComponent<Unit_Script>().speed_type = Speed_Symbol_d;
            newUnit1.GetComponent<Unit_Script>().attack_type = Attack_Symbol_d;
            newUnit1.GetComponent<Unit_Script>().defense_type = Defense_Symbol_d;
            newUnit1.GetComponent<Unit_Script>().damage_type = Damage_Symbol_d;

            newUnit1.GetComponent<Unit_Script>().IM_elevated = IM_Elevated_d;
            newUnit1.GetComponent<Unit_Script>().IM_hindering = IM_Hindering_d;
            newUnit1.GetComponent<Unit_Script>().IM_water = IM_Water_d;
            newUnit1.GetComponent<Unit_Script>().IM_blocking = IM_Blocking_d;
            newUnit1.GetComponent<Unit_Script>().IM_outdoor_blocking = IM_Outdoor_Blocking_d;
            newUnit1.GetComponent<Unit_Script>().IM_blocking_destroy = IM_Blocking_Destroy_d;
            newUnit1.GetComponent<Unit_Script>().IM_characters = IM_Characters_d;
            newUnit1.GetComponent<Unit_Script>().IM_adjacent_characters = IM_Adjacent_Characters_d;

            newUnit1.GetComponent<Unit_Script>().IT_elevated = IT_Elevated_d;
            newUnit1.GetComponent<Unit_Script>().IT_hindering = IT_Hindering_d;
            newUnit1.GetComponent<Unit_Script>().IT_blocking = IT_Blocking_d;
            newUnit1.GetComponent<Unit_Script>().IT_blocking_destroy = IT_Blocking_Destroy_d;
            newUnit1.GetComponent<Unit_Script>().IT_characters = IT_Characters_d;
            newUnit1.GetComponent<Unit_Script>().IT_adjacent_characters = IT_Adjacent_Characters_d;

            int keywordCounter = 0;
            List<string> tempLoadedKeywords = new List<string>();

            while (Keywords_d.Contains(",") && Keywords_d.Length > 1)
            {
                tempLoadedKeywords.Add(Keywords_d.Substring(0, Keywords_d.IndexOf(",")));
                Keywords_d = Keywords_d.Substring(Keywords_d.IndexOf(",") + 1, Keywords_d.Length - Keywords_d.IndexOf(",") - 1);
                keywordCounter++;
            }
            // Add the last (or only) keyword remaining after the final comma
            if (Keywords_d.Trim().Length > 0)
                tempLoadedKeywords.Add(Keywords_d.Trim());

            string[] tempKeyWords = new string[tempLoadedKeywords.Count];
            keywordCounter = 0;

            foreach (string keyword in tempLoadedKeywords)
            {
                tempKeyWords[keywordCounter] = tempLoadedKeywords[keywordCounter];
                keywordCounter++;
            }

            newUnit1.GetComponent<Unit_Script>().keywords = tempKeyWords;
            newUnit1.GetComponent<Unit_Script>().team_ability = Team_Ability_d;
            newUnit1.GetComponent<Unit_Script>().trait1 = Trait1_d;
            newUnit1.GetComponent<Unit_Script>().trait2 = Trait2_d;
            newUnit1.GetComponent<Unit_Script>().trait3 = Trait3_d;
            newUnit1.GetComponent<Unit_Script>().trait4 = Trait4_d;
            newUnit1.GetComponent<Unit_Script>().trait5 = Trait5_d;
            newUnit1.GetComponent<Unit_Script>().trait6 = Trait6_d;

            //            List<GameObject> PreviewUnitList = new List<GameObject>();


            PreviewUnitList.Add(newUnit1);

            Preview_Unit(PreviewUnitList, 0);
            CallLoadDynamicUnitImage();
        }
        
    }


    public void CallLoadDynamicUnitImage()
    {
        StartCoroutine(LoadDynamicUnitImage());
    }

    IEnumerator LoadDynamicUnitImage()
    {
        string url = "https://www.stark44.com/Heroclix/Unit_Images/" + CurrentlyPreviewedUnit.GetComponent<Unit_Script>().set_abbreviation + CurrentlyPreviewedUnit.GetComponent<Unit_Script>().collectors_number + ".png";
        Debug.Log(url);

        using (UnityWebRequest www2 = UnityWebRequestTexture.GetTexture(url))
        {
            www2.certificateHandler = new BypassCertificate();
            yield return www2.SendWebRequest();

            if (www2.result != UnityWebRequest.Result.Success)
            {
                Debug.Log(www2.error);
            }
            else
            {
                DownloadHandlerTexture downloadHandlerTexture = www2.downloadHandler as DownloadHandlerTexture;
                Sprite sprite = Sprite.Create(downloadHandlerTexture.texture, new Rect(0, 0, downloadHandlerTexture.texture.width, downloadHandlerTexture.texture.height), new Vector2(.5f, .5f), 100);
                DynamicUnitSculpt.sprite = sprite;
                Sculpt.sprite = sprite;
            }
        }

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

    public void Update_Previewed_Dial(List<GameObject> ListOfUnits, int listIndex, int startingIndex)
    {

        int DialLength = ListOfUnits[listIndex].GetComponent<Unit_Script>().dial_length;
        valuesList.Clear();
        powersList.Clear();
        textColorList.Clear();
        speedPowersList.Clear();
        attackPowersList.Clear();
        defensePowersList.Clear();
        damagePowersList.Clear();

        if (DialLength > startingIndex && startingIndex >= 0)
        {
            currentDialIndex = startingIndex;
            int showSlots = System.Math.Min(12, DialLength - currentDialIndex);

            for (int i = currentDialIndex; i < currentDialIndex + showSlots; i++)
            {

                if (ListOfUnits[listIndex].GetComponent<Unit_Script>().speed_values[i] == -1)
                {
                    valuesList.Add("KO");
                }
                else
                {
                    valuesList.Add(ListOfUnits[listIndex].GetComponent<Unit_Script>().speed_values[i].ToString());
                }

                switch (ListOfUnits[listIndex].GetComponent<Unit_Script>().speed_powers[i])
                {
                    case "Flurry":
                        speedPowersList.Add(ListOfUnits[listIndex].GetComponent<Unit_Script>().speed_powers[i]);
                        powersList.Add(new Color32(204, 0, 0, 255));
                        textColorList.Add(Color.white);
                        break;

                    case "Leap/Climb":
                        speedPowersList.Add(ListOfUnits[listIndex].GetComponent<Unit_Script>().speed_powers[i]);
                        powersList.Add(new Color32(255, 153, 51, 255));
                        textColorList.Add(Color.black);
                        break;

                    case "Phasing/Teleport":
                        speedPowersList.Add(ListOfUnits[listIndex].GetComponent<Unit_Script>().speed_powers[i]);
                        powersList.Add(new Color32(255, 255, 0, 255));
                        textColorList.Add(Color.black);
                        break;

                    case "Earthbound/Neutralized":
                        speedPowersList.Add(ListOfUnits[listIndex].GetComponent<Unit_Script>().speed_powers[i]);
                        powersList.Add(new Color32(0, 255, 51, 255));
                        textColorList.Add(Color.black);
                        break;

                    case "Charge":
                        speedPowersList.Add(ListOfUnits[listIndex].GetComponent<Unit_Script>().speed_powers[i]);
                        powersList.Add(new Color32(0, 153, 0, 255));
                        textColorList.Add(Color.black);
                        break;

                    case "Mind Control":
                        speedPowersList.Add(ListOfUnits[listIndex].GetComponent<Unit_Script>().speed_powers[i]);
                        powersList.Add(new Color32(179, 229, 252, 255));
                        textColorList.Add(Color.black);
                        break;

                    case "Plasticity":
                        speedPowersList.Add(ListOfUnits[listIndex].GetComponent<Unit_Script>().speed_powers[i]);
                        powersList.Add(new Color32(0, 51, 204, 255));
                        textColorList.Add(Color.white);
                        break;

                    case "Force Blast":
                        speedPowersList.Add(ListOfUnits[listIndex].GetComponent<Unit_Script>().speed_powers[i]);
                        powersList.Add(new Color32(102, 0, 204, 255));
                        textColorList.Add(Color.white);
                        break;

                    case "Sidestep":
                        speedPowersList.Add(ListOfUnits[listIndex].GetComponent<Unit_Script>().speed_powers[i]);
                        powersList.Add(new Color32(255, 153, 255, 255));
                        textColorList.Add(Color.black);
                        break;

                    case "Hypersonic Speed":
                        speedPowersList.Add(ListOfUnits[listIndex].GetComponent<Unit_Script>().speed_powers[i]);
                        powersList.Add(new Color32(191, 158, 27, 255));
                        textColorList.Add(Color.black);
                        break;

                    case "Stealth":
                        speedPowersList.Add(ListOfUnits[listIndex].GetComponent<Unit_Script>().speed_powers[i]);
                        powersList.Add(new Color32(0, 0, 0, 255));
                        textColorList.Add(Color.white);
                        break;

                    case "Running Shot":
                        speedPowersList.Add(ListOfUnits[listIndex].GetComponent<Unit_Script>().speed_powers[i]);
                        powersList.Add(new Color32(176, 190, 197, 255));
                        textColorList.Add(Color.white);
                        break;

                    case "None":

                        speedPowersList.Add(ListOfUnits[listIndex].GetComponent<Unit_Script>().speed_powers[i]);
                        powersList.Add(new Color32(255, 255, 255, 255));
                        textColorList.Add(Color.black);
                        break;

                    case "":

                        speedPowersList.Add(ListOfUnits[listIndex].GetComponent<Unit_Script>().speed_powers[i]);
                        powersList.Add(new Color32(255, 255, 255, 255));
                        textColorList.Add(Color.black);
                        break;

                    default:

                        speedPowersList.Add(ListOfUnits[listIndex].GetComponent<Unit_Script>().speed_powers[i]);
                        powersList.Add(new Color32(255, 0, 235, 255));
                        textColorList.Add(Color.black);
                        break;
                }
            }

            while (valuesList.Count < 12) { valuesList.Add("--"); }
            while (powersList.Count < 12) { powersList.Add(new Color32(220, 220, 220, 255)); }
            while (textColorList.Count < 12) { textColorList.Add(new Color32(150, 150, 150, 255)); }
            while (speedPowersList.Count < 12) { speedPowersList.Add(""); }

            Spd1.text = valuesList[0];
            Spd2.text = valuesList[1];
            Spd3.text = valuesList[2];
            Spd4.text = valuesList[3];
            Spd5.text = valuesList[4];
            Spd6.text = valuesList[5];
            Spd7.text = valuesList[6];
            Spd8.text = valuesList[7];
            Spd9.text = valuesList[8];
            Spd10.text = valuesList[9];
            Spd11.text = valuesList[10];
            Spd12.text = valuesList[11];

            Spd1.color = textColorList[0];
            Spd2.color = textColorList[1];
            Spd3.color = textColorList[2];
            Spd4.color = textColorList[3];
            Spd5.color = textColorList[4];
            Spd6.color = textColorList[5];
            Spd7.color = textColorList[6];
            Spd8.color = textColorList[7];
            Spd9.color = textColorList[8];
            Spd10.color = textColorList[9];
            Spd11.color = textColorList[10];
            Spd12.color = textColorList[11];

            SpdI1.color = powersList[0];
            SpdI2.color = powersList[1];
            SpdI3.color = powersList[2];
            SpdI4.color = powersList[3];
            SpdI5.color = powersList[4];
            SpdI6.color = powersList[5];
            SpdI7.color = powersList[6];
            SpdI8.color = powersList[7];
            SpdI9.color = powersList[8];
            SpdI10.color = powersList[9];
            SpdI11.color = powersList[10];
            SpdI12.color = powersList[11];

            valuesList.Clear();
            powersList.Clear();
            textColorList.Clear();
            currentDialIndex = startingIndex;
            showSlots = System.Math.Min(12, DialLength - currentDialIndex);

            for (int i = currentDialIndex; i < currentDialIndex + showSlots; i++)
            {
                if (ListOfUnits[listIndex].GetComponent<Unit_Script>().attack_values[i] == -1)
                {
                    valuesList.Add("KO");
                }
                else
                {
                    valuesList.Add(ListOfUnits[listIndex].GetComponent<Unit_Script>().attack_values[i].ToString());
                }

                switch (ListOfUnits[listIndex].GetComponent<Unit_Script>().attack_powers[i])
                {
                    case "Blades/Claws/Fangs":
                        attackPowersList.Add(ListOfUnits[listIndex].GetComponent<Unit_Script>().attack_powers[i]);
                        powersList.Add(new Color32(204, 0, 0, 255));
                        textColorList.Add(Color.white);
                        break;

                    case "Energy Explosion":
                        attackPowersList.Add(ListOfUnits[listIndex].GetComponent<Unit_Script>().attack_powers[i]);
                        powersList.Add(new Color32(255, 153, 51, 255));
                        textColorList.Add(Color.black);
                        break;

                    case "Pulse Wave":
                        attackPowersList.Add(ListOfUnits[listIndex].GetComponent<Unit_Script>().attack_powers[i]);
                        powersList.Add(new Color32(255, 255, 0, 255));
                        textColorList.Add(Color.black);
                        break;

                    case "Quake":
                        attackPowersList.Add(ListOfUnits[listIndex].GetComponent<Unit_Script>().attack_powers[i]);
                        powersList.Add(new Color32(0, 255, 51, 255));
                        textColorList.Add(Color.black);
                        break;

                    case "Super Strength":
                        attackPowersList.Add(ListOfUnits[listIndex].GetComponent<Unit_Script>().attack_powers[i]);
                        powersList.Add(new Color32(0, 153, 0, 255));
                        textColorList.Add(Color.black);
                        break;

                    case "Incapacitate":
                        attackPowersList.Add(ListOfUnits[listIndex].GetComponent<Unit_Script>().attack_powers[i]);
                        powersList.Add(new Color32(179, 229, 252, 255));
                        textColorList.Add(Color.black);
                        break;

                    case "Penetrating/Psychic Blast":
                        attackPowersList.Add(ListOfUnits[listIndex].GetComponent<Unit_Script>().attack_powers[i]);
                        powersList.Add(new Color32(0, 51, 204, 255));
                        textColorList.Add(Color.white);
                        break;

                    case "Smoke Cloud":
                        attackPowersList.Add(ListOfUnits[listIndex].GetComponent<Unit_Script>().attack_powers[i]);
                        powersList.Add(new Color32(102, 0, 204, 255));
                        textColorList.Add(Color.white);
                        break;

                    case "Precision Strike":
                        attackPowersList.Add(ListOfUnits[listIndex].GetComponent<Unit_Script>().attack_powers[i]);
                        powersList.Add(new Color32(255, 153, 255, 255));
                        textColorList.Add(Color.black);
                        break;

                    case "Poison":
                        attackPowersList.Add(ListOfUnits[listIndex].GetComponent<Unit_Script>().attack_powers[i]);
                        powersList.Add(new Color32(191, 158, 27, 255));
                        textColorList.Add(Color.black);
                        break;

                    case "Steal Energy":
                        attackPowersList.Add(ListOfUnits[listIndex].GetComponent<Unit_Script>().attack_powers[i]);
                        powersList.Add(new Color32(0, 0, 0, 255));
                        textColorList.Add(Color.white);
                        break;

                    case "Telekinesis":
                        attackPowersList.Add(ListOfUnits[listIndex].GetComponent<Unit_Script>().attack_powers[i]);
                        powersList.Add(new Color32(176, 190, 197, 255));
                        textColorList.Add(Color.white);
                        break;

                    case "None":

                        attackPowersList.Add(ListOfUnits[listIndex].GetComponent<Unit_Script>().attack_powers[i]);
                        powersList.Add(new Color32(255, 255, 255, 255));
                        textColorList.Add(Color.black);
                        break;

                    case "":

                        attackPowersList.Add(ListOfUnits[listIndex].GetComponent<Unit_Script>().attack_powers[i]);
                        powersList.Add(new Color32(255, 255, 255, 255));
                        textColorList.Add(Color.black);
                        break;

                    default:

                        attackPowersList.Add(ListOfUnits[listIndex].GetComponent<Unit_Script>().attack_powers[i]);
                        powersList.Add(new Color32(255, 0, 235, 255));
                        textColorList.Add(Color.black);
                        break;
                }
            }

            while (valuesList.Count < 12) { valuesList.Add("--"); }
            while (powersList.Count < 12) { powersList.Add(new Color32(220, 220, 220, 255)); }
            while (textColorList.Count < 12) { textColorList.Add(new Color32(150, 150, 150, 255)); }
            while (attackPowersList.Count < 12) { attackPowersList.Add(""); }

            Atk1.text = valuesList[0];
            Atk2.text = valuesList[1];
            Atk3.text = valuesList[2];
            Atk4.text = valuesList[3];
            Atk5.text = valuesList[4];
            Atk6.text = valuesList[5];
            Atk7.text = valuesList[6];
            Atk8.text = valuesList[7];
            Atk9.text = valuesList[8];
            Atk10.text = valuesList[9];
            Atk11.text = valuesList[10];
            Atk12.text = valuesList[11];

            Atk1.color = textColorList[0];
            Atk2.color = textColorList[1];
            Atk3.color = textColorList[2];
            Atk4.color = textColorList[3];
            Atk5.color = textColorList[4];
            Atk6.color = textColorList[5];
            Atk7.color = textColorList[6];
            Atk8.color = textColorList[7];
            Atk9.color = textColorList[8];
            Atk10.color = textColorList[9];
            Atk11.color = textColorList[10];
            Atk12.color = textColorList[11];

            AtkI1.color = powersList[0];
            AtkI2.color = powersList[1];
            AtkI3.color = powersList[2];
            AtkI4.color = powersList[3];
            AtkI5.color = powersList[4];
            AtkI6.color = powersList[5];
            AtkI7.color = powersList[6];
            AtkI8.color = powersList[7];
            AtkI9.color = powersList[8];
            AtkI10.color = powersList[9];
            AtkI11.color = powersList[10];
            AtkI12.color = powersList[11];

            valuesList.Clear();
            powersList.Clear();
            textColorList.Clear();
            currentDialIndex = startingIndex;
            showSlots = System.Math.Min(12, DialLength - currentDialIndex);

            for (int i = currentDialIndex; i < currentDialIndex + showSlots; i++)
            {
                if (ListOfUnits[listIndex].GetComponent<Unit_Script>().defense_values[i] == -1)
                {
                    valuesList.Add("KO");
                }
                else
                {
                    valuesList.Add(ListOfUnits[listIndex].GetComponent<Unit_Script>().defense_values[i].ToString());
                }

                switch (ListOfUnits[listIndex].GetComponent<Unit_Script>().defense_powers[i])
                {
                    

                    case "Super Senses":
                        defensePowersList.Add(ListOfUnits[listIndex].GetComponent<Unit_Script>().defense_powers[i]);
                        powersList.Add(new Color32(204, 0, 0, 255));
                        textColorList.Add(Color.white);
                        break;

                    case "Toughness":
                        defensePowersList.Add(ListOfUnits[listIndex].GetComponent<Unit_Script>().defense_powers[i]);
                        powersList.Add(new Color32(255, 153, 51, 255));
                        textColorList.Add(Color.black);
                        break;

                    case "Defend":
                        defensePowersList.Add(ListOfUnits[listIndex].GetComponent<Unit_Script>().defense_powers[i]);
                        powersList.Add(new Color32(255, 255, 0, 255));
                        textColorList.Add(Color.black);
                        break;

                    case "Combat Reflexes":
                        defensePowersList.Add(ListOfUnits[listIndex].GetComponent<Unit_Script>().defense_powers[i]);
                        powersList.Add(new Color32(0, 255, 51, 255));
                        textColorList.Add(Color.black);
                        break;

                    case "Energy Shield/Deflection":
                        defensePowersList.Add(ListOfUnits[listIndex].GetComponent<Unit_Script>().defense_powers[i]);
                        powersList.Add(new Color32(0, 153, 0, 255));
                        textColorList.Add(Color.black);
                        break;

                    case "Barrier":
                        defensePowersList.Add(ListOfUnits[listIndex].GetComponent<Unit_Script>().defense_powers[i]);
                        powersList.Add(new Color32(179, 229, 252, 255));
                        textColorList.Add(Color.black);
                        break;

                    case "Mastermind":
                        defensePowersList.Add(ListOfUnits[listIndex].GetComponent<Unit_Script>().defense_powers[i]);
                        powersList.Add(new Color32(0, 51, 204, 255));
                        textColorList.Add(Color.white);
                        break;

                    case "Willpower":
                        defensePowersList.Add(ListOfUnits[listIndex].GetComponent<Unit_Script>().defense_powers[i]);
                        powersList.Add(new Color32(102, 0, 204, 255));
                        textColorList.Add(Color.white);
                        break;

                    case "Invincible":
                        defensePowersList.Add(ListOfUnits[listIndex].GetComponent<Unit_Script>().defense_powers[i]);
                        powersList.Add(new Color32(255, 153, 255, 255));
                        textColorList.Add(Color.black);
                        break;

                    case "Impervious":
                        defensePowersList.Add(ListOfUnits[listIndex].GetComponent<Unit_Script>().defense_powers[i]);
                        powersList.Add(new Color32(191, 158, 27, 255));
                        textColorList.Add(Color.black);
                        break;

                    case "Regeneration":
                        defensePowersList.Add(ListOfUnits[listIndex].GetComponent<Unit_Script>().defense_powers[i]);
                        powersList.Add(new Color32(0, 0, 0, 255));
                        textColorList.Add(Color.white);
                        break;

                    case "Invulnerability":
                        defensePowersList.Add(ListOfUnits[listIndex].GetComponent<Unit_Script>().defense_powers[i]);
                        powersList.Add(new Color32(176, 190, 197, 255));
                        textColorList.Add(Color.white);
                        break;

                    case "None":

                        defensePowersList.Add(ListOfUnits[listIndex].GetComponent<Unit_Script>().defense_powers[i]);
                        powersList.Add(new Color32(255, 255, 255, 255));
                        textColorList.Add(Color.black);
                        break;

                    case "":

                        defensePowersList.Add(ListOfUnits[listIndex].GetComponent<Unit_Script>().defense_powers[i]);
                        powersList.Add(new Color32(255, 255, 255, 255));
                        textColorList.Add(Color.black);
                        break;

                    default:

                        defensePowersList.Add(ListOfUnits[listIndex].GetComponent<Unit_Script>().defense_powers[i]);
                        powersList.Add(new Color32(255, 0, 235, 255));
                        textColorList.Add(Color.black);
                        break;
                }
            }

            while (valuesList.Count < 12) { valuesList.Add("--"); }
            while (powersList.Count < 12) { powersList.Add(new Color32(220, 220, 220, 255)); }
            while (textColorList.Count < 12) { textColorList.Add(new Color32(150, 150, 150, 255)); }
            while (defensePowersList.Count < 12) { defensePowersList.Add(""); }

            Def1.text = valuesList[0];
            Def2.text = valuesList[1];
            Def3.text = valuesList[2];
            Def4.text = valuesList[3];
            Def5.text = valuesList[4];
            Def6.text = valuesList[5];
            Def7.text = valuesList[6];
            Def8.text = valuesList[7];
            Def9.text = valuesList[8];
            Def10.text = valuesList[9];
            Def11.text = valuesList[10];
            Def12.text = valuesList[11];

            Def1.color = textColorList[0];
            Def2.color = textColorList[1];
            Def3.color = textColorList[2];
            Def4.color = textColorList[3];
            Def5.color = textColorList[4];
            Def6.color = textColorList[5];
            Def7.color = textColorList[6];
            Def8.color = textColorList[7];
            Def9.color = textColorList[8];
            Def10.color = textColorList[9];
            Def11.color = textColorList[10];
            Def12.color = textColorList[11];

            DefI1.color = powersList[0];
            DefI2.color = powersList[1];
            DefI3.color = powersList[2];
            DefI4.color = powersList[3];
            DefI5.color = powersList[4];
            DefI6.color = powersList[5];
            DefI7.color = powersList[6];
            DefI8.color = powersList[7];
            DefI9.color = powersList[8];
            DefI10.color = powersList[9];
            DefI11.color = powersList[10];
            DefI12.color = powersList[11];

            valuesList.Clear();
            powersList.Clear();
            textColorList.Clear();
            currentDialIndex = startingIndex;
            showSlots = System.Math.Min(12, DialLength - currentDialIndex);

            for (int i = currentDialIndex; i < currentDialIndex + showSlots; i++)
            {
                if (ListOfUnits[listIndex].GetComponent<Unit_Script>().damage_values[i] == -1)
                {
                    valuesList.Add("KO");
                }
                else
                {
                    valuesList.Add(ListOfUnits[listIndex].GetComponent<Unit_Script>().damage_values[i].ToString());
                }

                switch (ListOfUnits[listIndex].GetComponent<Unit_Script>().damage_powers[i])
                {
                    case "Ranged Combat Expert":
                        damagePowersList.Add(ListOfUnits[listIndex].GetComponent<Unit_Script>().damage_powers[i]);
                        powersList.Add(new Color32(204, 0, 0, 255));
                        textColorList.Add(Color.white);
                        break;

                    case "Battle Fury":
                        damagePowersList.Add(ListOfUnits[listIndex].GetComponent<Unit_Script>().damage_powers[i]);
                        powersList.Add(new Color32(255, 153, 51, 255));
                        textColorList.Add(Color.black);
                        break;

                    case "Support":
                        damagePowersList.Add(ListOfUnits[listIndex].GetComponent<Unit_Script>().damage_powers[i]);
                        powersList.Add(new Color32(255, 255, 0, 255));
                        textColorList.Add(Color.black);
                        break;

                    case "Exploit Weakness":
                        damagePowersList.Add(ListOfUnits[listIndex].GetComponent<Unit_Script>().damage_powers[i]);
                        powersList.Add(new Color32(0, 255, 51, 255));
                        textColorList.Add(Color.black);
                        break;

                    case "Enhancement":
                        damagePowersList.Add(ListOfUnits[listIndex].GetComponent<Unit_Script>().damage_powers[i]);
                        powersList.Add(new Color32(0, 153, 0, 255));
                        textColorList.Add(Color.black);
                        break;

                    case "Probability Control":
                        damagePowersList.Add(ListOfUnits[listIndex].GetComponent<Unit_Script>().damage_powers[i]);
                        powersList.Add(new Color32(179, 229, 252, 255));
                        textColorList.Add(Color.black);
                        break;

                    case "Shape Change":
                        damagePowersList.Add(ListOfUnits[listIndex].GetComponent<Unit_Script>().damage_powers[i]);
                        powersList.Add(new Color32(0, 51, 204, 255));
                        textColorList.Add(Color.white);
                        break;

                    case "Close Combat Expert":
                        damagePowersList.Add(ListOfUnits[listIndex].GetComponent<Unit_Script>().damage_powers[i]);
                        powersList.Add(new Color32(102, 0, 204, 255));
                        textColorList.Add(Color.white);
                        break;

                    case "Empower":
                        damagePowersList.Add(ListOfUnits[listIndex].GetComponent<Unit_Script>().damage_powers[i]);
                        powersList.Add(new Color32(255, 153, 255, 255));
                        textColorList.Add(Color.black);
                        break;

                    case "Perplex":
                        damagePowersList.Add(ListOfUnits[listIndex].GetComponent<Unit_Script>().damage_powers[i]);
                        powersList.Add(new Color32(191, 158, 27, 255));
                        textColorList.Add(Color.black);
                        break;

                    case "Outwit":
                        damagePowersList.Add(ListOfUnits[listIndex].GetComponent<Unit_Script>().damage_powers[i]);
                        powersList.Add(new Color32(0, 0, 0, 255));
                        textColorList.Add(Color.white);
                        break;

                    case "Leadership":
                        damagePowersList.Add(ListOfUnits[listIndex].GetComponent<Unit_Script>().damage_powers[i]);
                        powersList.Add(new Color32(176, 190, 197, 255));
                        textColorList.Add(Color.white);
                        break;

                    case "None":

                        damagePowersList.Add(ListOfUnits[listIndex].GetComponent<Unit_Script>().damage_powers[i]);
                        powersList.Add(new Color32(255, 255, 255, 255));
                        textColorList.Add(Color.black);
                        break;

                    case "":

                        damagePowersList.Add(ListOfUnits[listIndex].GetComponent<Unit_Script>().damage_powers[i]);
                        powersList.Add(new Color32(255, 255, 255, 255));
                        textColorList.Add(Color.black);
                        break;

                    default:

                        damagePowersList.Add(ListOfUnits[listIndex].GetComponent<Unit_Script>().damage_powers[i]);
                        powersList.Add(new Color32(255, 0, 235, 255));
                        textColorList.Add(Color.black);
                        break;

                }
            }

            while (valuesList.Count < 12) { valuesList.Add("--"); }
            while (powersList.Count < 12) { powersList.Add(new Color32(220, 220, 220, 255)); }
            while (textColorList.Count < 12) { textColorList.Add(new Color32(150, 150, 150, 255)); }
            while (damagePowersList.Count < 12) { damagePowersList.Add(""); }

            Dmg1.text = valuesList[0];
            Dmg2.text = valuesList[1];
            Dmg3.text = valuesList[2];
            Dmg4.text = valuesList[3];
            Dmg5.text = valuesList[4];
            Dmg6.text = valuesList[5];
            Dmg7.text = valuesList[6];
            Dmg8.text = valuesList[7];
            Dmg9.text = valuesList[8];
            Dmg10.text = valuesList[9];
            Dmg11.text = valuesList[10];
            Dmg12.text = valuesList[11];

            Dmg1.color = textColorList[0];
            Dmg2.color = textColorList[1];
            Dmg3.color = textColorList[2];
            Dmg4.color = textColorList[3];
            Dmg5.color = textColorList[4];
            Dmg6.color = textColorList[5];
            Dmg7.color = textColorList[6];
            Dmg8.color = textColorList[7];
            Dmg9.color = textColorList[8];
            Dmg10.color = textColorList[9];
            Dmg11.color = textColorList[10];
            Dmg12.color = textColorList[11];

            DmgI1.color = powersList[0];
            DmgI2.color = powersList[1];
            DmgI3.color = powersList[2];
            DmgI4.color = powersList[3];
            DmgI5.color = powersList[4];
            DmgI6.color = powersList[5];
            DmgI7.color = powersList[6];
            DmgI8.color = powersList[7];
            DmgI9.color = powersList[8];
            DmgI10.color = powersList[9];
            DmgI11.color = powersList[10];
            DmgI12.color = powersList[11];

            valuesList.Clear();
            powersList.Clear();
            textColorList.Clear();
            currentDialIndex = startingIndex;
        }
    }

    public void Preview_Unit(List<GameObject> ListOfUnits, int index)
    {
        CurrentlyPreviewedUnit = ListOfUnits[index];
        tempUnitList.Clear();
        tempUnitList.Add(CurrentlyPreviewedUnit);

        Update_Previewed_Dial(ListOfUnits, index, 0);

        speedType = ListOfUnits[index].GetComponent<Unit_Script>().speed_type;
        attackType = ListOfUnits[index].GetComponent<Unit_Script>().attack_type;
        defenseType = ListOfUnits[index].GetComponent<Unit_Script>().defense_type;
        damageType = ListOfUnits[index].GetComponent<Unit_Script>().damage_type;

        switch (speedType)
        {
            case "Wing":
                speedIcon.GetComponent<Image>().sprite = Resources.Load<Sprite>("Images/Dial_Symbols/Spd_Wing");
                break;

            case "Boot":
                speedIcon.GetComponent<Image>().sprite = Resources.Load<Sprite>("Images/Dial_Symbols/Spd");
                break;

            case "Boot_Transport":
                speedIcon.GetComponent<Image>().sprite = Resources.Load<Sprite>("Images/Dial_Symbols/Spd_Transport");
                break;

            case "Wing_Transport":
                speedIcon.GetComponent<Image>().sprite = Resources.Load<Sprite>("Images/Dial_Symbols/Spd_Wing_Transport");
                break;

            case "Dolphin":
                speedIcon.GetComponent<Image>().sprite = Resources.Load<Sprite>("Images/Dial_Symbols/Spd_Dolphin");
                break;

            case "Dolphin_Transport":
                speedIcon.GetComponent<Image>().sprite = Resources.Load<Sprite>("Images/Dial_Symbols/Spd_Dolphin_Transport");
                break;

        }

        switch (attackType)
        {
            case "Fist":
                attackIcon.GetComponent<Image>().sprite = Resources.Load<Sprite>("Images/Dial_Symbols/Atk");
                break;

            case "Duo":
                attackIcon.GetComponent<Image>().sprite = Resources.Load<Sprite>("Images/Dial_Symbols/Atk_Duo");
                break;

            case "Sharpshooter":
                attackIcon.GetComponent<Image>().sprite = Resources.Load<Sprite>("Images/Dial_Symbols/Atk_Sharpshooter");
                break;

        }

        switch (defenseType)
        {
            case "Shield":
                defenseIcon.GetComponent<Image>().sprite = Resources.Load<Sprite>("Images/Dial_Symbols/Def");
                break;

            case "Indomitable":
                defenseIcon.GetComponent<Image>().sprite = Resources.Load<Sprite>("Images/Dial_Symbols/Def_Indomitable");
                break;
        }

        switch (damageType)
        {
            case "Starburst":
                damageIcon.GetComponent<Image>().sprite = Resources.Load<Sprite>("Images/Dial_Symbols/Dmg");
                break;

            case "Giant":
                damageIcon.GetComponent<Image>().sprite = Resources.Load<Sprite>("Images/Dial_Symbols/Dmg_Giant");
                break;

            case "Fist":
                damageIcon.GetComponent<Image>().sprite = Resources.Load<Sprite>("Images/Dial_Symbols/Dmg_Fist");
                break;

            case "Atom":
                damageIcon.GetComponent<Image>().sprite = Resources.Load<Sprite>("Images/Dial_Symbols/Dmg_Atom");
                break;

        }

        rangeTargetsNum = ListOfUnits[index].GetComponent<Unit_Script>().range_targets;

        switch (rangeTargetsNum)
        {
            case 1:
                rangeTargets.GetComponent<Image>().sprite = Resources.Load<Sprite>("Images/Dial_Symbols/Target_1");
                break;

            case 2:
                rangeTargets.GetComponent<Image>().sprite = Resources.Load<Sprite>("Images/Dial_Symbols/Target_2");
                break;

            case 3:
                rangeTargets.GetComponent<Image>().sprite = Resources.Load<Sprite>("Images/Dial_Symbols/Target_3");
                break;

            case 4:
                rangeTargets.GetComponent<Image>().sprite = Resources.Load<Sprite>("Images/Dial_Symbols/Target_4");
                break;

        }

        RangeValue.text = ListOfUnits[index].GetComponent<Unit_Script>().range.ToString();

        //commented for dynamic unit images
        //Sculpt.GetComponent<Image>().sprite = ListOfUnits[index].GetComponent<Unit_Script>().Sculpt_Image;

        UnitName.text = ListOfUnits[index].GetComponent<Unit_Script>().unit_Name;

        UnitPreviewPoints.text = ListOfUnits[index].GetComponent<Unit_Script>().points.ToString();

        UnitKeywords.Clear();

        for (int i = 0; i < ListOfUnits[index].GetComponent<Unit_Script>().keywords.Length; i++)
        {
            UnitKeywords.Add(ListOfUnits[index].GetComponent<Unit_Script>().keywords[i]);
        }

        if (UnitKeywords.Count() > 0)
        {
            keywordIndex = 0;
            CurrentKeyword.text = UnitKeywords[0];
        }
        else
        {
            CurrentKeyword.text = "";
        }


        teamAbility = ListOfUnits[index].GetComponent<Unit_Script>().team_ability;

        switch (teamAbility)
        {
            case "Avengers":
                TeamAbilityImage.GetComponent<Image>().sprite = Resources.Load<Sprite>("Images/Team_Abilities/Avengers");
                break;

            case "Avengers Initiative":
                TeamAbilityImage.GetComponent<Image>().sprite = Resources.Load<Sprite>("Images/Team_Abilities/Avengers Initiative");
                break;

            case "Batman Ally":
                TeamAbilityImage.GetComponent<Image>().sprite = Resources.Load<Sprite>("Images/Team_Abilities/Batman Ally");
                break;

            case "Batman Enemy":
                TeamAbilityImage.GetComponent<Image>().sprite = Resources.Load<Sprite>("Images/Team_Abilities/Batman Enemy");
                break;

            case "Brotherhood of Mutants":
                TeamAbilityImage.GetComponent<Image>().sprite = Resources.Load<Sprite>("Images/Team_Abilities/Brotherhood of Mutants");
                break;

            case "Defenders":
                TeamAbilityImage.GetComponent<Image>().sprite = Resources.Load<Sprite>("Images/Team_Abilities/Defenders");
                break;

            case "Fantastic Four":
                TeamAbilityImage.GetComponent<Image>().sprite = Resources.Load<Sprite>("Images/Team_Abilities/Fantastic Four");
                break;

            case "Green Lantern Corps":
                TeamAbilityImage.GetComponent<Image>().sprite = Resources.Load<Sprite>("Images/Team_Abilities/Green Lantern Corps");
                break;

            case "Hydra":
                TeamAbilityImage.GetComponent<Image>().sprite = Resources.Load<Sprite>("Images/Team_Abilities/Hydra");
                break;

            case "Hypertime":
                TeamAbilityImage.GetComponent<Image>().sprite = Resources.Load<Sprite>("Images/Team_Abilities/Hypertime");
                break;

            case "Injustice League":
                TeamAbilityImage.GetComponent<Image>().sprite = Resources.Load<Sprite>("Images/Team_Abilities/Injustice League");
                break;

            case "Justice League":
                TeamAbilityImage.GetComponent<Image>().sprite = Resources.Load<Sprite>("Images/Team_Abilities/Justice League");
                break;

            case "Justice Society":
                TeamAbilityImage.GetComponent<Image>().sprite = Resources.Load<Sprite>("Images/Team_Abilities/Justice Society");
                break;

            case "Kingdom Come":
                TeamAbilityImage.GetComponent<Image>().sprite = Resources.Load<Sprite>("Images/Team_Abilities/Kingdom Come");
                break;

            case "Legion of Super Heroes":
                TeamAbilityImage.GetComponent<Image>().sprite = Resources.Load<Sprite>("Images/Team_Abilities/Legion of Super Heroes");
                break;

            case "Masters of Evil":
                TeamAbilityImage.GetComponent<Image>().sprite = Resources.Load<Sprite>("Images/Team_Abilities/Masters of Evil");
                break;

            case "Minions of Doom":
                TeamAbilityImage.GetComponent<Image>().sprite = Resources.Load<Sprite>("Images/Team_Abilities/Minions of Doom");
                break;

            case "Mystics":
                TeamAbilityImage.GetComponent<Image>().sprite = Resources.Load<Sprite>("Images/Team_Abilities/Mystics");
                break;

            case "Outsiders":
                TeamAbilityImage.GetComponent<Image>().sprite = Resources.Load<Sprite>("Images/Team_Abilities/Outsiders");
                break;

            case "Police":
                TeamAbilityImage.GetComponent<Image>().sprite = Resources.Load<Sprite>("Images/Team_Abilities/Police");
                break;

            case "Power Cosmic":
                TeamAbilityImage.GetComponent<Image>().sprite = Resources.Load<Sprite>("Images/Team_Abilities/Power Cosmic");
                break;

            case "Quintessence":
                TeamAbilityImage.GetComponent<Image>().sprite = Resources.Load<Sprite>("Images/Team_Abilities/Quintessence");
                break;

            case "SHIELD":
                TeamAbilityImage.GetComponent<Image>().sprite = Resources.Load<Sprite>("Images/Team_Abilities/SHIELD");
                break;

            case "Sinister Syndicate":
                TeamAbilityImage.GetComponent<Image>().sprite = Resources.Load<Sprite>("Images/Team_Abilities/Sinister Syndicate");
                break;

            case "Skrulls":
                TeamAbilityImage.GetComponent<Image>().sprite = Resources.Load<Sprite>("Images/Team_Abilities/Skrulls");
                break;

            case "Snowfall":
                TeamAbilityImage.GetComponent<Image>().sprite = Resources.Load<Sprite>("Images/Team_Abilities/Snowfall");
                break;

            case "Spider-Man":
                TeamAbilityImage.GetComponent<Image>().sprite = Resources.Load<Sprite>("Images/Team_Abilities/Spider-Man");
                break;

            case "Suicide Squad":
                TeamAbilityImage.GetComponent<Image>().sprite = Resources.Load<Sprite>("Images/Team_Abilities/Suicide Squad");
                break;

            case "Superman Ally":
                TeamAbilityImage.GetComponent<Image>().sprite = Resources.Load<Sprite>("Images/Team_Abilities/Superman Ally");
                break;

            case "Superman Enemy":
                TeamAbilityImage.GetComponent<Image>().sprite = Resources.Load<Sprite>("Images/Team_Abilities/Superman Enemy");
                break;

            case "Team Player":
                TeamAbilityImage.GetComponent<Image>().sprite = Resources.Load<Sprite>("Images/Team_Abilities/Team Player");
                break;

            case "Titans":
                TeamAbilityImage.GetComponent<Image>().sprite = Resources.Load<Sprite>("Images/Team_Abilities/Titans");
                break;

            case "Underworld":
                TeamAbilityImage.GetComponent<Image>().sprite = Resources.Load<Sprite>("Images/Team_Abilities/Underworld");
                break;

            case "Underworld2":
                TeamAbilityImage.GetComponent<Image>().sprite = Resources.Load<Sprite>("Images/Team_Abilities/Underworld2");
                break;

            case "X-Men":
                TeamAbilityImage.GetComponent<Image>().sprite = Resources.Load<Sprite>("Images/Team_Abilities/X-Men");
                break;

            default:
                TeamAbilityImage.GetComponent<Image>().sprite = Resources.Load<Sprite>("Images/Team_Abilities/None");
                break;

        }

        ITElevated.SetActive(false);
        ITHindering.SetActive(false);
        ITBlocking.SetActive(false);
        ITBlockingDestroy.SetActive(false);
        ITCharacters.SetActive(false);
        ITAdjacentCharacters.SetActive(false);

        IMElevated.SetActive(false);
        IMHindering.SetActive(false);
        IMWater.SetActive(false);
        IMBlocking.SetActive(false);
        IMBlockingOutdoor.SetActive(false);
        IMBlockingDestroy.SetActive(false);
        IMCharacters.SetActive(false);
        IMAdjacentCharacters.SetActive(false);

        if (ListOfUnits[index].GetComponent<Unit_Script>().IT_elevated) { ITElevated.SetActive(true); }
        if (ListOfUnits[index].GetComponent<Unit_Script>().IT_hindering) { ITHindering.SetActive(true); }
        if (ListOfUnits[index].GetComponent<Unit_Script>().IT_blocking) { ITBlocking.SetActive(true); }
        if (ListOfUnits[index].GetComponent<Unit_Script>().IT_characters) { ITCharacters.SetActive(true); }
        if (ListOfUnits[index].GetComponent<Unit_Script>().IT_blocking_destroy) { ITBlockingDestroy.SetActive(true); }
        if (ListOfUnits[index].GetComponent<Unit_Script>().IT_adjacent_characters) { ITAdjacentCharacters.SetActive(true); }

        if (ListOfUnits[index].GetComponent<Unit_Script>().IM_elevated) { IMElevated.SetActive(true); }
        if (ListOfUnits[index].GetComponent<Unit_Script>().IM_hindering) { IMHindering.SetActive(true); }
        if (ListOfUnits[index].GetComponent<Unit_Script>().IM_water) { IMWater.SetActive(true); }
        if (ListOfUnits[index].GetComponent<Unit_Script>().IM_blocking) { IMBlocking.SetActive(true); }
        if (ListOfUnits[index].GetComponent<Unit_Script>().IM_outdoor_blocking) { IMBlockingOutdoor.SetActive(true); }
        if (ListOfUnits[index].GetComponent<Unit_Script>().IM_blocking_destroy) { IMBlockingDestroy.SetActive(true); }
        if (ListOfUnits[index].GetComponent<Unit_Script>().IM_characters) { IMCharacters.SetActive(true); }
        if (ListOfUnits[index].GetComponent<Unit_Script>().IM_adjacent_characters) { IMAdjacentCharacters.SetActive(true); }

        if (ListOfUnits[index].GetComponent<Unit_Script>().trait1 != "") { traitImg1.SetActive(true); } else { traitImg1.SetActive(false); }
        if (ListOfUnits[index].GetComponent<Unit_Script>().trait2 != "") { traitImg2.SetActive(true); } else { traitImg2.SetActive(false); }
        if (ListOfUnits[index].GetComponent<Unit_Script>().trait3 != "") { traitImg3.SetActive(true); } else { traitImg3.SetActive(false); }
        if (ListOfUnits[index].GetComponent<Unit_Script>().trait4 != "") { traitImg4.SetActive(true); } else { traitImg4.SetActive(false); }
        if (ListOfUnits[index].GetComponent<Unit_Script>().trait5 != "") { traitImg5.SetActive(true); } else { traitImg5.SetActive(false); }
        if (ListOfUnits[index].GetComponent<Unit_Script>().trait6 != "") { traitImg6.SetActive(true); } else { traitImg6.SetActive(false); }


    }

    private string LookupPowerDescription(string powerName)
    {
        if (string.IsNullOrEmpty(powerName)) return "";
        for (int i = 0; i < AllPowerNamesList.Count; i++)
        {
            if (AllPowerNamesList[i] == powerName)
                return AllPowerDescriptionsList[i];
        }
        return "";
    }
}
