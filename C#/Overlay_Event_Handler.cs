using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.Networking;
using TMPro;


public class Overlay_Event_Handler : MonoBehaviour
{
    public Text Spd1, Spd2, Spd3, Spd4, Spd5, Spd6, Spd7, Spd8, Spd9, Spd10, Spd11, Spd12;
    public Text Atk1, Atk2, Atk3, Atk4, Atk5, Atk6, Atk7, Atk8, Atk9, Atk10, Atk11, Atk12;
    public Text Def1, Def2, Def3, Def4, Def5, Def6, Def7, Def8, Def9, Def10, Def11, Def12;
    public Text Dmg1, Dmg2, Dmg3, Dmg4, Dmg5, Dmg6, Dmg7, Dmg8, Dmg9, Dmg10, Dmg11, Dmg12;
    public Text Slot1, Slot2, Slot3, Slot4, Slot5, Slot6, Slot7, Slot8, Slot9, Slot10, Slot11, Slot12;
    public Text RangeValue;
    public Text UnitPreviewPoints, CurrentKeyword, UnitName, PowerDescription;
    public Text UnitMenuClickNumber, UnitMenuActionTokens;
    public Text AddNewChatText, AllChatText;
    public Text SelectedGamePoints;

    public Image SpdI1, SpdI2, SpdI3, SpdI4, SpdI5, SpdI6, SpdI7, SpdI8, SpdI9, SpdI10, SpdI11, SpdI12;
    public Image AtkI1, AtkI2, AtkI3, AtkI4, AtkI5, AtkI6, AtkI7, AtkI8, AtkI9, AtkI10, AtkI11, AtkI12;
    public Image DefI1, DefI2, DefI3, DefI4, DefI5, DefI6, DefI7, DefI8, DefI9, DefI10, DefI11, DefI12;
    public Image DmgI1, DmgI2, DmgI3, DmgI4, DmgI5, DmgI6, DmgI7, DmgI8, DmgI9, DmgI10, DmgI11, DmgI12;
    public Image SlotI1, SlotI2, SlotI3, SlotI4, SlotI5, SlotI6, SlotI7, SlotI8, SlotI9, SlotI10, SlotI11, SlotI12;
    public Image speedIcon, attackIcon, defenseIcon, damageIcon, rangeTargets;
    public Image Sculpt;
    public Image TeamAbilityImage;
    
    public GameObject traitImg1, traitImg2, traitImg3, traitImg4, traitImg5, traitImg6;
    public GameObject ITElevated, ITHindering, ITBlocking, ITBlockingDestroy, ITCharacters, ITAdjacentCharacters;
    public GameObject IMElevated, IMHindering, IMWater, IMBlocking, IMBlockingOutdoor, IMBlockingDestroy, IMCharacters, IMAdjacentCharacters;
    public GameObject CurrentlyPreviewedUnit, CurrentlySelectedObject, AttackingUnit, AttackedUnit;
    public GameObject UnitMenu, ObjectMenu;

    public string speedType, attackType, defenseType, damageType;
    public string selectedSet;
    public string teamAbility;
    public string teamAbilityDescription;
    public string targetClick;
    public string SelectedOfficialSet, SelectedCustomSet, SelectedOfficialUnit, SelectedCustomUnit;

    public int rangeTargetsNum;
    public int selectedUnitIndex, selectedTeamUnitIndex, keywordIndex, selectedTeamLoadIndex, selectedTeamDynamicUnitOfficialLoadIndex, selectedTeamDynamicUnitCustomLoadIndex;
    public Dropdown ImportPlayer;
    public Toggle SetPlayer2;

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

    public bool isAttacking;

    public List<GameObject> units = new List<GameObject>();
    public List<GameObject> team = new List<GameObject>();
    public List<GameObject> tempUnitList = new List<GameObject>();
    List<GameObject> LastUnitPreviewedList = new List<GameObject>();

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

    List<string> speedPowersDescriptionsList = new List<string>();
    List<string> attackPowersDescriptionsList = new List<string>();
    List<string> defensePowersDescriptionsList = new List<string>();
    List<string> damagePowersDescriptionsList = new List<string>();

    List<string> loadedTeamsList = new List<string>();
    List<string> loadedUnitsList = new List<string>();
    List<string> ChatLog = new List<string>();

    public string SAVE_FOLDER;

    public Dropdown team_Name_Dropdown, TeamInput;

    public Button OpenMiniStatsPanel, CloseMiniStatsPanel, OpenStatsPanel, CloseStatsPanel;

    public InputField ChatInput;

    public Material SelectedMapImage;

    public List<string> OfficialSetNameList = new List<string>();
    public List<string> CustomSetNameList = new List<string>();

    public List<string> OfficialSetDropDownDataList = new List<string>();
    public List<string> CustomSetDropDownDataList = new List<string>();

    public List<string> OfficialSetUnitNameList = new List<string>();
    public List<string> CustomSetUnitNameList = new List<string>();

    public List<string> OfficialSetCollectorsNumberList = new List<string>();
    public List<string> CustomSetCollectorsNumberList = new List<string>();

    public Dropdown OfficialSetNameDropdown, OfficialUnitsNameDropdown, CustomSetNameDropdown, CustomUnitsNameDropdown;

    //Dynamic Unit Section
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

    //Load Game Dynamic Units Section
    List<string> Set_Name_da = new List<string>();
    List<string> Collectors_Number_da = new List<string>();
    List<string> Unit_Name_da = new List<string>();

    List<int> Dial_Length_da = new List<int>();
    List<int> Last_Click_da = new List<int>();
    List<int> Points_da = new List<int>();
    List<int> Range_Value_da = new List<int>();
    List<int> Range_Targets_da = new List<int>();

    List<string> Experience_da = new List<string>();
    List<string> Rarity_da = new List<string>();
    List<string> Base_Type_da = new List<string>();
    List<string> Speed_Symbol_da = new List<string>();
    List<string> Attack_Symbol_da = new List<string>();
    List<string> Defense_Symbol_da = new List<string>();
    List<string> Damage_Symbol_da = new List<string>();

    List<bool> IM_Elevated_da = new List<bool>();
    List<bool> IM_Hindering_da = new List<bool>();
    List<bool> IM_Water_da = new List<bool>();
    List<bool> IM_Blocking_da = new List<bool>();
    List<bool> IM_Outdoor_Blocking_da = new List<bool>();
    List<bool> IM_Blocking_destroy_da = new List<bool>();
    List<bool> IM_Characters_da = new List<bool>();
    List<bool> IM_Adjacent_Characters_da = new List<bool>();

    List<bool> IT_Elevated_da = new List<bool>();
    List<bool> IT_Hindering_da = new List<bool>();
    List<bool> IT_Blocking_da = new List<bool>();
    List<bool> IT_Blocking_destroy_da = new List<bool>();
    List<bool> IT_Characters_da = new List<bool>();
    List<bool> IT_Adjacent_Characters_da = new List<bool>();

    List<string> Keywords_da = new List<string>();
    List<string> Team_Ability_da = new List<string>();
    List<string> Trait1_da = new List<string>();
    List<string> Trait2_da = new List<string>();
    List<string> Trait3_da = new List<string>();
    List<string> Trait4_da = new List<string>();
    List<string> Trait5_da = new List<string>();
    List<string> Trait6_da = new List<string>();
    List<string> Set_Abbrev_da = new List<string>();

    List<float> LoadGame_UnitList_LocationX = new List<float>();
    List<float> LoadGame_UnitList_LocationY = new List<float>();
    List<float> LoadGame_UnitList_LocationZ = new List<float>();

    List<int> LoadGame_UnitList_Actions = new List<int>();
    List<int> LoadGame_UnitList_ClickNumber = new List<int>();
    List<int> LoadGame_UnitList_TeamNumber = new List<int>();

    int AddedDynamicCount = 0;

    //Dial
    List<string> SetName_List = new List<string>();
    List<string> CollectorsNumber_List = new List<string>();
    List<List<int>> Slot_List = new List<List<int>>();

    List<List<int>> SpeedVal_List = new List<List<int>>();
    List<List<int>> AttackVal_List = new List<List<int>>();
    List<List<int>> DefenseVal_List = new List<List<int>>();
    List<List<int>> DamageVal_List = new List<List<int>>();

    List<List<string>> SpeedPow_List = new List<List<string>>();
    List<List<string>> AttackPow_List = new List<List<string>>();
    List<List<string>> DefensePow_List = new List<List<string>>();
    List<List<string>> DamagePow_List = new List<List<string>>();

    List<List<string>> SpeedPowDesc_List = new List<List<string>>();
    List<List<string>> AttackPowDesc_List = new List<List<string>>();
    List<List<string>> DefensePowDesc_List = new List<List<string>>();
    List<List<string>> DamagePowDesc_List = new List<List<string>>();
    //End Dynamic Units section

    //Dynamic Teams Section
    List<string> TeamIDList = new List<string>();
    List<string> TeamNameList = new List<string>();
    List<string> TeamPointsList = new List<string>();

    List<GameObject> PreviewUnitList = new List<GameObject>();
    List<GameObject> LoadedTeamGameObjects = new List<GameObject>();

    int xLoadOffset = 0;
    int zLoadOffset = 0;
    //End Dynamic Teams Section

    public Button PlayGameButton;

    //Image
    public SpriteRenderer DynamicUnitSculpt;

    //Turn Order
    public List<string> DropdownUsersList = new List<string>();
    public Dropdown SetupTurnOrderDropdown, SelectCurrentPlayerDropdown;

    public Text TurnOrderText, ActivePlayerText;
    public Button SaveGameButton;
    public Button Save_Turn_Order_Setup_Button, Clear_Turn_Order_Setup_Button, Add_Player_Turn_Order_Setup_Button;
    private bool activePlayerIsNull = true; // true until server confirms an active player is set

    public GameObject TurnOrderPanel;
    //End Turn Order


    public IEnumerator Start()
    {
        yield return StartCoroutine(GetAllPowersList());

        SAVE_FOLDER = Application.dataPath + "/Saved_Teams/";

        if (!Directory.Exists(SAVE_FOLDER))
        {
            Directory.CreateDirectory(SAVE_FOLDER);
        }

        CallLoadDynamicTeamNames();

        if(Application.platform == RuntimePlatform.WindowsPlayer  || Application.platform == RuntimePlatform.WindowsEditor) //|| Application.platform == RuntimePlatform.OSXPlayer
        {
            PlayGameButton.gameObject.SetActive(false);
            yield return StartCoroutine(LoadGameCoroutine());
            CallLogActivity("Game Loaded " + Application.platform);
        }
        else
        {
            Debug.Log("Android");
        }


    }


    public void Update()
    {

        if (IsPointerOverUI())    // is the touch on the GUI
        {
            // GUI Action
            return;
        }
        else
        {
            if (Input.GetMouseButtonDown(0))
            {

                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

                RaycastHit hit;
                if (Physics.Raycast(ray, out hit))
                {
                    targetClick = hit.collider.tag;
                }

                if (targetClick == "Background_Image")
                {
                    UnitMenu.SetActive(false);
                    ObjectMenu.SetActive(false);
                }

                if (targetClick == "Unit" && isAttacking)
                {
                    AttackedUnit = hit.collider.gameObject;
                    sendAttackToChat(AttackingUnit, AttackedUnit);
                    CallLogActivity(AttackingUnit + "Attacked " + AttackedUnit);
                }

                if (targetClick == "ManualUnit" && isAttacking)
                {
                    AttackedUnit = hit.collider.gameObject;
                    sendAttackToChat(AttackingUnit, AttackedUnit);
                    CallLogActivity(AttackingUnit + "Attacked " + AttackedUnit);
                }

                if (targetClick == "Unit")
                {
                    LastUnitPreviewedList.Clear();
                    PowerDescription.text = "";

                    List<GameObject> tempPreviewList = new List<GameObject>();

                    tempPreviewList.Add(hit.collider.gameObject);
                    LastUnitPreviewedList.Add(hit.collider.gameObject);

                    while (currentDialIndex > 0)
                    {
                        Previous_Click();
                    }

                    Preview_Unit(tempPreviewList, 0);
                    ObjectMenu.SetActive(false);
                    Unit_Menu_Load();
                    TeamInput.value = CurrentlyPreviewedUnit.gameObject.GetComponent<Unit_Script>().team - 1;
                    
                }

                if (targetClick == "ManualUnit")
                {
                    LastUnitPreviewedList.Clear();
                    PowerDescription.text = "";

                    List<GameObject> tempPreviewList = new List<GameObject>();

                    tempPreviewList.Add(hit.collider.gameObject);
                    LastUnitPreviewedList.Add(hit.collider.gameObject);

                    while (currentDialIndex > 0)
                    {
                        Previous_Click();
                    }

                    Preview_Unit(tempPreviewList, 0);
                    ObjectMenu.SetActive(false);
                    Unit_Menu_Load();
                    TeamInput.value = CurrentlyPreviewedUnit.gameObject.GetComponent<Manual_Non_Single_Base_Unit_Script>().team - 1;

                }

                if (targetClick == "Object")
                {
                    UnitMenu.SetActive(false);
                    Object_Menu_Load();
                    CurrentlySelectedObject = hit.collider.gameObject;
                }
            }
        }
    }

    public void AndroidStartMethod()
    {
        StartCoroutine(AndroidInitialize());
    }

    IEnumerator AndroidInitialize()
    {
        yield return StartCoroutine(GetAllPowersList());

        CallLoadDynamicTeamNames();
        yield return StartCoroutine(LoadGameCoroutine());
        CallLogActivity("Game Loaded Android");
    }

    //////////////////////////////////////////////

    public void Team_Dynamic_Unit_Official_Dropdown_IndexChanged(int index)
    {
        selectedTeamDynamicUnitOfficialLoadIndex = index + 1;

    }

    public void Team_Dynamic_Unit_Custom_Dropdown_IndexChanged(int index)
    {
        selectedTeamDynamicUnitCustomLoadIndex = index + 1;

    }

    public void LoadDynamicUnitOfficial()
    {

        var allUnitsDynamicOfficial = Resources.LoadAll("Units/Dynamic", typeof(GameObject));

        if (IsBaseType(Base_Type_d, "2x2"))
        {
            allUnitsDynamicOfficial = Resources.LoadAll("Units/Manual/2x2", typeof(GameObject));
        }
        else if (IsBaseType(Base_Type_d, "peanut") || Base_Type_d == "1x2")
        {
            allUnitsDynamicOfficial = Resources.LoadAll("Units/Manual/1x2", typeof(GameObject));
        }
        else if (IsBaseType(Base_Type_d, "2x4"))
        {
            allUnitsDynamicOfficial = Resources.LoadAll("Units/Manual/2x4", typeof(GameObject));
        }
        else if (IsBaseType(Base_Type_d, "3x6"))
        {
            allUnitsDynamicOfficial = Resources.LoadAll("Units/Manual/3x6", typeof(GameObject));
        }

        Debug.Log("[LoadDynamicUnitOfficial] Base_Type_d='" + Base_Type_d + "' | prefab count=" + allUnitsDynamicOfficial.Length + " | unit='" + Set_Name_d + " " + Collectors_Number_d + "'");

        if (allUnitsDynamicOfficial.Length == 0)
        {
            Debug.LogWarning("[LoadDynamicUnitOfficial] No prefabs found for Base_Type_d='" + Base_Type_d + "'. Check that Resources/Units/Manual/" + Base_Type_d + "/ contains a prefab.");
            return;
        }

        foreach (GameObject DynamicUnit in allUnitsDynamicOfficial)
        {
            GameObject newUnit1 = GameObject.Instantiate(DynamicUnit, new Vector3(-7.5f, -1.1f, -9.5f), Quaternion.identity);

            if (newUnit1.GetComponent<Unit_Script>() != null)
            {
                newUnit1.GetComponent<Unit_Script>().speed_values = new List<int>(DialInfoDynamic_SpeedValue);
                newUnit1.GetComponent<Unit_Script>().attack_values = new List<int>(DialInfoDynamic_AttackValue);
                newUnit1.GetComponent<Unit_Script>().defense_values = new List<int>(DialInfoDynamic_DefenseValue);
                newUnit1.GetComponent<Unit_Script>().damage_values = new List<int>(DialInfoDynamic_DamageValue);

                newUnit1.GetComponent<Unit_Script>().speed_powers = new List<string>(DialInfoDynamic_SpeedPowerStr);
                newUnit1.GetComponent<Unit_Script>().attack_powers = new List<string>(DialInfoDynamic_AttackPowerStr);
                newUnit1.GetComponent<Unit_Script>().defense_powers = new List<string>(DialInfoDynamic_DefensePowerStr);
                newUnit1.GetComponent<Unit_Script>().damage_powers = new List<string>(DialInfoDynamic_DamagePowerStr);

                newUnit1.GetComponent<Unit_Script>().speed_powers_description = new List<string>(DialInfoDynamic_SpeedPowerDescription);
                newUnit1.GetComponent<Unit_Script>().attack_powers_description = new List<string>(DialInfoDynamic_AttackPowerDescription);
                newUnit1.GetComponent<Unit_Script>().defense_powers_description = new List<string>(DialInfoDynamic_DefensePowerDescription);
                newUnit1.GetComponent<Unit_Script>().damage_powers_description = new List<string>(DialInfoDynamic_DamagePowerDescription);

                newUnit1.name = (Set_Name_d + "_" + Collectors_Number_d + "_" + Unit_Name_d).Replace(" ", "");

                newUnit1.GetComponent<Unit_Script>().actionTokens = 0;
                newUnit1.GetComponent<Unit_Script>().click_number = 1;
                newUnit1.GetComponent<Unit_Script>().team = selectedTeamDynamicUnitOfficialLoadIndex;
                newUnit1.GetComponent<Unit_Script>().isDynamic = 1;

                newUnit1.GetComponent<Unit_Script>().set = Set_Name_d;
                newUnit1.GetComponent<Unit_Script>().set_abbreviation = Set_Abbrev_d;
                newUnit1.GetComponent<Unit_Script>().collectors_number = Collectors_Number_d;
                newUnit1.GetComponent<Unit_Script>().unit_Name = Unit_Name_d;

                CurrentlyPreviewedUnit = newUnit1;
                CallLoadDynamicUnitImageSynch(newUnit1);

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

                newUnit1.GetComponentInChildren<TMP_Text>().text = Unit_Name_d;

                //newUnit1.GetComponent<Unit_Script>().Sculpt_Image = Sculpt.sprite;
                //newUnit1.GetComponentInChildren<SpriteRenderer>().sprite = Sculpt.sprite;

                CallLogActivity("Loaded Official Unit: " + newUnit1.name);
            }
            else if (newUnit1.GetComponent<Manual_Non_Single_Base_Unit_Script>() != null)
            {
                Manual_Non_Single_Base_Unit_Script mus = newUnit1.GetComponent<Manual_Non_Single_Base_Unit_Script>();
                mus.speed_values  = new List<int>(DialInfoDynamic_SpeedValue);
                mus.attack_values  = new List<int>(DialInfoDynamic_AttackValue);
                mus.defense_values = new List<int>(DialInfoDynamic_DefenseValue);
                mus.damage_values  = new List<int>(DialInfoDynamic_DamageValue);
                mus.speed_powers   = new List<string>(DialInfoDynamic_SpeedPowerStr);
                mus.attack_powers  = new List<string>(DialInfoDynamic_AttackPowerStr);
                mus.defense_powers = new List<string>(DialInfoDynamic_DefensePowerStr);
                mus.damage_powers  = new List<string>(DialInfoDynamic_DamagePowerStr);
                mus.speed_powers_description   = new List<string>(DialInfoDynamic_SpeedPowerDescription);
                mus.attack_powers_description  = new List<string>(DialInfoDynamic_AttackPowerDescription);
                mus.defense_powers_description = new List<string>(DialInfoDynamic_DefensePowerDescription);
                mus.damage_powers_description  = new List<string>(DialInfoDynamic_DamagePowerDescription);

                newUnit1.name = (Set_Name_d + "_" + Collectors_Number_d + "_" + Unit_Name_d).Replace(" ", "");
                mus.actionTokens = 0;
                mus.click_number = 1;
                mus.team = selectedTeamDynamicUnitOfficialLoadIndex;
                mus.isDynamic = 1;
                mus.set = Set_Name_d;
                mus.set_abbreviation = Set_Abbrev_d;
                mus.collectors_number = Collectors_Number_d;
                mus.unit_Name = Unit_Name_d;

                CurrentlyPreviewedUnit = newUnit1;
                CallLoadDynamicUnitImageSynch(newUnit1);

                mus.dial_length = Dial_Length_d;
                mus.last_click = Last_Click_d;
                mus.points = Points_d;
                mus.range_targets = Range_Targets_d;
                mus.range = Range_Value_d;
                mus.experience = Experience_d;
                mus.rarity = Rarity_d;
                mus.base_type = Base_Type_d;
                mus.speed_type = Speed_Symbol_d;
                mus.attack_type = Attack_Symbol_d;
                mus.defense_type = Defense_Symbol_d;
                mus.damage_type = Damage_Symbol_d;
                mus.IM_elevated = IM_Elevated_d;
                mus.IM_hindering = IM_Hindering_d;
                mus.IM_water = IM_Water_d;
                mus.IM_blocking = IM_Blocking_d;
                mus.IM_outdoor_blocking = IM_Outdoor_Blocking_d;
                mus.IM_blocking_destroy = IM_Blocking_Destroy_d;
                mus.IM_characters = IM_Characters_d;
                mus.IM_adjacent_characters = IM_Adjacent_Characters_d;
                mus.IT_elevated = IT_Elevated_d;
                mus.IT_hindering = IT_Hindering_d;
                mus.IT_blocking = IT_Blocking_d;
                mus.IT_blocking_destroy = IT_Blocking_Destroy_d;
                mus.IT_characters = IT_Characters_d;
                mus.IT_adjacent_characters = IT_Adjacent_Characters_d;

                int keywordCounter2 = 0;
                List<string> tempLoadedKeywords2 = new List<string>();
                while (Keywords_d.Contains(",") && Keywords_d.Length > 1)
                {
                    tempLoadedKeywords2.Add(Keywords_d.Substring(0, Keywords_d.IndexOf(",")));
                    Keywords_d = Keywords_d.Substring(Keywords_d.IndexOf(",") + 1, Keywords_d.Length - Keywords_d.IndexOf(",") - 1);
                    keywordCounter2++;
                }
                string[] tempKeyWords2 = new string[tempLoadedKeywords2.Count];
                for (int ki = 0; ki < tempLoadedKeywords2.Count; ki++)
                    tempKeyWords2[ki] = tempLoadedKeywords2[ki];
                mus.keywords = tempKeyWords2;
                mus.team_ability = Team_Ability_d;
                mus.trait1 = Trait1_d; mus.trait2 = Trait2_d; mus.trait3 = Trait3_d;
                mus.trait4 = Trait4_d; mus.trait5 = Trait5_d; mus.trait6 = Trait6_d;

                newUnit1.GetComponentInChildren<TMP_Text>().text = Unit_Name_d;
                CallLogActivity("Loaded Official Unit: " + newUnit1.name);
            }
        }
    }

    public void LoadDynamicUnitCustom()
    {
        var allUnitsDynamic = Resources.LoadAll("Units/Dynamic", typeof(GameObject));

        foreach (GameObject DynamicUnit2 in allUnitsDynamic)
        {
            GameObject newUnit = GameObject.Instantiate(DynamicUnit2, new Vector3(-7.5f, -1.1f, -9.5f), Quaternion.identity);

            newUnit.GetComponent<Unit_Script>().speed_values = new List<int>(DialInfoDynamic_SpeedValue);
            newUnit.GetComponent<Unit_Script>().attack_values = new List<int>(DialInfoDynamic_AttackValue);
            newUnit.GetComponent<Unit_Script>().defense_values = new List<int>(DialInfoDynamic_DefenseValue);
            newUnit.GetComponent<Unit_Script>().damage_values = new List<int>(DialInfoDynamic_DamageValue);

            newUnit.GetComponent<Unit_Script>().speed_powers = new List<string>(DialInfoDynamic_SpeedPowerStr);
            newUnit.GetComponent<Unit_Script>().attack_powers = new List<string>(DialInfoDynamic_AttackPowerStr);
            newUnit.GetComponent<Unit_Script>().defense_powers = new List<string>(DialInfoDynamic_DefensePowerStr);
            newUnit.GetComponent<Unit_Script>().damage_powers = new List<string>(DialInfoDynamic_DamagePowerStr);

            newUnit.GetComponent<Unit_Script>().speed_powers_description = new List<string>(DialInfoDynamic_SpeedPowerDescription);
            newUnit.GetComponent<Unit_Script>().attack_powers_description = new List<string>(DialInfoDynamic_AttackPowerDescription);
            newUnit.GetComponent<Unit_Script>().defense_powers_description = new List<string>(DialInfoDynamic_DefensePowerDescription);
            newUnit.GetComponent<Unit_Script>().damage_powers_description = new List<string>(DialInfoDynamic_DamagePowerDescription);

            newUnit.name = (Set_Name_d + "_" + Collectors_Number_d + "_" + Unit_Name_d).Replace(" ", "");

            newUnit.GetComponent<Unit_Script>().actionTokens = 0;
            newUnit.GetComponent<Unit_Script>().click_number = 1;
            newUnit.GetComponent<Unit_Script>().team = selectedTeamDynamicUnitCustomLoadIndex;
            newUnit.GetComponent<Unit_Script>().isDynamic = 1;

            newUnit.GetComponent<Unit_Script>().set = Set_Name_d;
            newUnit.GetComponent<Unit_Script>().set_abbreviation = Set_Abbrev_d;
            newUnit.GetComponent<Unit_Script>().collectors_number = Collectors_Number_d;
            newUnit.GetComponent<Unit_Script>().unit_Name = Unit_Name_d;

            CurrentlyPreviewedUnit = newUnit;
            CallLoadDynamicUnitImageSynch(newUnit);

            newUnit.GetComponent<Unit_Script>().dial_length = Dial_Length_d;
            newUnit.GetComponent<Unit_Script>().last_click = Last_Click_d;
            newUnit.GetComponent<Unit_Script>().points = Points_d;
            newUnit.GetComponent<Unit_Script>().range_targets = Range_Targets_d;
            newUnit.GetComponent<Unit_Script>().range = Range_Value_d;

            newUnit.GetComponent<Unit_Script>().experience = Experience_d;
            newUnit.GetComponent<Unit_Script>().rarity = Rarity_d;
            newUnit.GetComponent<Unit_Script>().base_type = Base_Type_d;
            newUnit.GetComponent<Unit_Script>().speed_type = Speed_Symbol_d;
            newUnit.GetComponent<Unit_Script>().attack_type = Attack_Symbol_d;
            newUnit.GetComponent<Unit_Script>().defense_type = Defense_Symbol_d;
            newUnit.GetComponent<Unit_Script>().damage_type = Damage_Symbol_d;

            newUnit.GetComponent<Unit_Script>().IM_elevated = IM_Elevated_d;
            newUnit.GetComponent<Unit_Script>().IM_hindering = IM_Hindering_d;
            newUnit.GetComponent<Unit_Script>().IM_water = IM_Water_d;
            newUnit.GetComponent<Unit_Script>().IM_blocking = IM_Blocking_d;
            newUnit.GetComponent<Unit_Script>().IM_outdoor_blocking = IM_Outdoor_Blocking_d;
            newUnit.GetComponent<Unit_Script>().IM_blocking_destroy = IM_Blocking_Destroy_d;
            newUnit.GetComponent<Unit_Script>().IM_characters = IM_Characters_d;
            newUnit.GetComponent<Unit_Script>().IM_adjacent_characters = IM_Adjacent_Characters_d;

            newUnit.GetComponent<Unit_Script>().IT_elevated = IT_Elevated_d;
            newUnit.GetComponent<Unit_Script>().IT_hindering = IT_Hindering_d;
            newUnit.GetComponent<Unit_Script>().IT_blocking = IT_Blocking_d;
            newUnit.GetComponent<Unit_Script>().IT_blocking_destroy = IT_Blocking_Destroy_d;
            newUnit.GetComponent<Unit_Script>().IT_characters = IT_Characters_d;
            newUnit.GetComponent<Unit_Script>().IT_adjacent_characters = IT_Adjacent_Characters_d;

            int keywordCounter = 0;
            List<string> tempLoadedKeywords = new List<string>();

            while (Keywords_d.Contains(",") && Keywords_d.Length > 1)
            {
                tempLoadedKeywords.Add(Keywords_d.Substring(0, Keywords_d.IndexOf(",")));
                Keywords_d = Keywords_d.Substring(Keywords_d.IndexOf(",") + 1, Keywords_d.Length - Keywords_d.IndexOf(",") - 1);
                keywordCounter++;
            }

            string[] tempKeyWords = new string[tempLoadedKeywords.Count];
            keywordCounter = 0;

            foreach (string keyword in tempLoadedKeywords)
            {
                tempKeyWords[keywordCounter] = tempLoadedKeywords[keywordCounter];
                keywordCounter++;
            }

            newUnit.GetComponent<Unit_Script>().keywords = tempKeyWords;
            newUnit.GetComponent<Unit_Script>().team_ability = Team_Ability_d;
            newUnit.GetComponent<Unit_Script>().trait1 = Trait1_d;
            newUnit.GetComponent<Unit_Script>().trait2 = Trait2_d;
            newUnit.GetComponent<Unit_Script>().trait3 = Trait3_d;
            newUnit.GetComponent<Unit_Script>().trait4 = Trait4_d;
            newUnit.GetComponent<Unit_Script>().trait5 = Trait5_d;
            newUnit.GetComponent<Unit_Script>().trait6 = Trait6_d;

            newUnit.GetComponentInChildren<TMP_Text>().text = Unit_Name_d;

            CallLogActivity("Loaded Custom Unit: " + newUnit.name);
        }
    }

    public void CallGetOfficialUnitSetNames()
    {
        StartCoroutine(GetOfficialUnitSetNames());
    }

    IEnumerator GetOfficialUnitSetNames()
    {
        OfficialSetNameList.Clear();
        OfficialSetNameDropdown.ClearOptions();

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

                OfficialSetNameList.Add("Official Sets");

                while (DBOfficialSets.Contains(",") && DBOfficialSets.Length > 1)
                {
                    OfficialSetNameList.Add(DBOfficialSets.Substring(0, DBOfficialSets.IndexOf(",")));
                    DBOfficialSets = DBOfficialSets.Substring(DBOfficialSets.IndexOf(",") + 1, DBOfficialSets.Length - DBOfficialSets.IndexOf(",") - 1);
                }

                OfficialSetNameDropdown.AddOptions(OfficialSetNameList);
            }
        }
        Official_Set_Name_Dropdown_IndexChanged(0);

    }

    public void CallGetCustomUnitSetNames()
    {
        StartCoroutine(GetCustomUnitSetNames());
    }

    IEnumerator GetCustomUnitSetNames()
    {
        CustomSetNameList.Clear();
        CustomSetNameDropdown.ClearOptions();

        using (UnityWebRequest www = UnityWebRequest.Get("https://www.stark44.com/Heroclix/getDynamicUnitSetsCustom.php"))
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
                string DBCustomSets = www.downloadHandler.text;

                CustomSetNameList.Add("Custom Sets");

                while (DBCustomSets.Contains(",") && DBCustomSets.Length > 1)
                {
                    CustomSetNameList.Add(DBCustomSets.Substring(0, DBCustomSets.IndexOf(",")));
                    DBCustomSets = DBCustomSets.Substring(DBCustomSets.IndexOf(",") + 1, DBCustomSets.Length - DBCustomSets.IndexOf(",") - 1);
                }

                CustomSetNameDropdown.AddOptions(CustomSetNameList);

            }
        }
        Custom_Set_Name_Dropdown_IndexChanged(0);
    }


    public void Official_Set_Name_Dropdown_IndexChanged(int index)
    {
        SelectedOfficialSet = OfficialSetNameList[index];
        if (SelectedOfficialSet != "Official Sets")
        {
            CallGetOfficialUnitsFromSet();
        }
        else
        {
            OfficialUnitsNameDropdown.ClearOptions();
        }
    }

    public void CallGetOfficialUnitsFromSet()
    {
        StartCoroutine(GetOfficialUnitsFromSet());
    }

    IEnumerator GetOfficialUnitsFromSet()
    {
        OfficialSetDropDownDataList.Clear();
        OfficialSetCollectorsNumberList.Clear();
        OfficialSetUnitNameList.Clear();
        OfficialUnitsNameDropdown.ClearOptions();

        WWWForm form = new WWWForm();
        form.AddField("Set_Name", SelectedOfficialSet);

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

                    OfficialSetDropDownDataList.Add(DropdownData);
                    OfficialSetCollectorsNumberList.Add(Collectors_Num);
                    OfficialSetUnitNameList.Add(Unit_Name);


                    DBUnits = DBUnits.Substring(DBUnits.IndexOf(";") + 1, DBUnits.Length - DBUnits.IndexOf(";") - 1);
                }




            }
        }
        OfficialUnitsNameDropdown.AddOptions(OfficialSetDropDownDataList);
        Official_Selected_Unit_Dropdown_IndexChanged(0);
    }

    public void Custom_Set_Name_Dropdown_IndexChanged(int index)
    {
        SelectedCustomSet = CustomSetNameList[index];

        if (SelectedCustomSet != "Custom Sets")
        {
            CallGetCustomUnitsFromSet();
        }
        else
        {
            CustomUnitsNameDropdown.ClearOptions();
        }
    }

    public void CallGetCustomUnitsFromSet()
    {
        StartCoroutine(GetCustomUnitsFromSet());
    }

    IEnumerator GetCustomUnitsFromSet()
    {
        CustomSetDropDownDataList.Clear();
        CustomSetCollectorsNumberList.Clear();
        CustomSetUnitNameList.Clear();
        CustomUnitsNameDropdown.ClearOptions();

        WWWForm form = new WWWForm();
        form.AddField("Set_Name", SelectedCustomSet);

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

                    CustomSetDropDownDataList.Add(DropdownData);
                    CustomSetCollectorsNumberList.Add(Collectors_Num);
                    CustomSetUnitNameList.Add(Unit_Name);


                    DBUnits = DBUnits.Substring(DBUnits.IndexOf(";") + 1, DBUnits.Length - DBUnits.IndexOf(";") - 1);
                }

                CustomUnitsNameDropdown.AddOptions(CustomSetDropDownDataList);
                Custom_Selected_Unit_Dropdown_IndexChanged(0);

            }
        }

    }

    public void Custom_Selected_Unit_Dropdown_IndexChanged(int index)
    {
        SelectedCustomUnit = CustomSetUnitNameList[index];
        SelectedCustomUnitIndex = index;
        SelectedCustomCollectorsNumber = CustomSetCollectorsNumberList[index];

        Selected_Dynamic_Set_Name = SelectedCustomSet;
        Selected_Dynamic_Collectors_Number = SelectedCustomCollectorsNumber;

        StartCoroutine(LoadSelectedUnitData());
    }

    public void Official_Selected_Unit_Dropdown_IndexChanged(int index)
    {
        SelectedOfficialUnit = OfficialSetUnitNameList[index];
        SelectedOfficialUnitIndex = index;
        SelectedOfficialCollectorsNumber = OfficialSetCollectorsNumberList[index];

        Selected_Dynamic_Set_Name = SelectedOfficialSet;
        Selected_Dynamic_Collectors_Number = SelectedOfficialCollectorsNumber;

        StartCoroutine(LoadSelectedUnitData());
    }

    IEnumerator LoadSelectedUnitData()
    {
        yield return StartCoroutine(GetDynamicUnitDial());
        yield return StartCoroutine(GetDynamicUnitStats());
    }



    public void sendSetTeam()
    {
        setTeam(TeamInput.value + 1);
    }

    public void setTeam(int setTeam)
    {
        if (setTeam <= 10 && setTeam >= 0)
        {
            CurrentlyPreviewedUnit.gameObject.GetComponent<Unit_Script>().team = setTeam;
        }

    }

    public void declareAttack()
    {
        isAttacking = true;
    }

    public void sendAttackToChat(GameObject unit_attacking, GameObject unit_attacked)
    {
        int i = 0;
        AllChatText.text = "";
        
        ChatLog.Add(unit_attacking.GetComponent<Unit_Script>().unit_Name + " attacks " + unit_attacked.GetComponent<Unit_Script>().unit_Name + " ATK: " + unit_attacking.GetComponent<Unit_Script>().attack_values[unit_attacking.GetComponent<Unit_Script>().click_number - 1] + " DEF: " + unit_attacked.GetComponent<Unit_Script>().defense_values[unit_attacked.GetComponent<Unit_Script>().click_number - 1] + "For " + unit_attacking.GetComponent<Unit_Script>().damage_values[unit_attacking.GetComponent<Unit_Script>().click_number - 1] + " DMG");
        foreach (string str in ChatLog)
        {
            AllChatText.text += ChatLog[i] + "\r\n";
            i++;
        }

        ChatInput.text = "";
        isAttacking = false;
    }

    public void sendChat(int sentbymethod)
    {
        if (PlayerPrefs.GetInt("Chat_With_Enter") == sentbymethod)
        {

            string SanitizedChat = SanitizeInputs(AddNewChatText.text);

            int i = 0;
            AllChatText.text = "";
            ChatLog.Add(SanitizedChat);
            foreach (string str in ChatLog)
            {
                AllChatText.text += ChatLog[i] + "\r\n";
                i++;
            }

            if (SanitizedChat.Length > 0)
            {
                int SubLength = 5;

                if(SanitizedChat.Length < 5)
                {
                    SubLength = SanitizedChat.Length;
                }

                if (SanitizedChat.Substring(0, SubLength) == "Roll:")
                {
                    CallLogActivity("Cheat");
                }
                else
                {
                    CallLogActivity("Chat Entered: " + SanitizedChat);
                }
            }

            ChatInput.text = "";
        }

    }

    string SanitizeInputs(string inputstring)
    {
        inputstring = inputstring.Replace("'", "");
        inputstring = inputstring.Replace("\"", "");
        inputstring = inputstring.Replace(";", "");
        inputstring = inputstring.Replace(",", "");

        return inputstring;

    }
    public void Object_Menu_Load()
    {
        ObjectMenu.SetActive(true);
    }

    public void KO_Object()
    {
        CallLogActivity("KO Object: " + CurrentlySelectedObject.name + " Coordinates: X:" + CurrentlySelectedObject.transform.position.x + " Y:" + CurrentlySelectedObject.transform.position.y + " Z:" + CurrentlySelectedObject.transform.position.z);
        Destroy(CurrentlySelectedObject);
        ObjectMenu.SetActive(false);
    }

    public void Clone_Object()
    {
        CallLogActivity("Clone Object: " + CurrentlySelectedObject.name + " Coordinates: X:" + CurrentlySelectedObject.transform.position.x + " Y:" + CurrentlySelectedObject.transform.position.y + " Z:" + CurrentlySelectedObject.transform.position.z);
        Instantiate(CurrentlySelectedObject);
        ObjectMenu.SetActive(false);
    }


    public void Add_Light_Object()
    {
        var allObjects = Resources.LoadAll("Object_Skins/", typeof(GameObject));
        foreach(GameObject allObject in allObjects)
        {
            if (allObject.name == "Light_Object")
            {
                Instantiate(allObject, new Vector3(-7.5f, -1.49f, -9.5f), Quaternion.identity);
                CallLogActivity("Add Light Object");
            }
        }

    }

    public void Add_Heavy_Object()
    {
        var allObjects = Resources.LoadAll("Object_Skins/", typeof(GameObject));
        foreach (GameObject allObject in allObjects)
        {
            if (allObject.name == "Heavy_Object")
            {
                Instantiate(allObject, new Vector3(-7.5f, -1.49f, -9.5f), Quaternion.identity);
                CallLogActivity("Add Heavy Object");
            }
        }
    }

    //Quick workaround for terrain markers
    public void Add_Water_Terrain_Object()
    {
        var allObjects = Resources.LoadAll("Object_Skins/", typeof(GameObject));
        foreach (GameObject allObject in allObjects)
        {
            if (allObject.name == "Water_Terrain_Object")
            {
                Instantiate(allObject, new Vector3(-7.5f, -1.49f, -9.5f), Quaternion.identity);
                CallLogActivity("Add Water Terrain Object");
            }
        }
    }

    public void Add_Barrier_Terrain_Object()
    {
        var allObjects = Resources.LoadAll("Object_Skins/", typeof(GameObject));
        foreach (GameObject allObject in allObjects)
        {
            if (allObject.name == "Barrier_Object")
            {
                Instantiate(allObject, new Vector3(-7.5f, -1.49f, -9.5f), Quaternion.identity);
                CallLogActivity("Add Barrier Object");
            }
        }
    }

    public void Add_Hindering_Terrain_Object()
    {
        var allObjects = Resources.LoadAll("Object_Skins/", typeof(GameObject));
        foreach (GameObject allObject in allObjects)
        {
            if (allObject.name == "Hindering_Terrain_Object")
            {
                Instantiate(allObject, new Vector3(-7.5f, -1.49f, -9.5f), Quaternion.identity);
                CallLogActivity("Add Hindering Terrain Object");
            }
        }
    }

    public void Add_Smoke_Cloud_Object()
    {
        var allObjects = Resources.LoadAll("Object_Skins/", typeof(GameObject));
        foreach (GameObject allObject in allObjects)
        {
            if (allObject.name == "Smoke_Cloud_Object")
            {
                Instantiate(allObject, new Vector3(-7.5f, -1.49f, -9.5f), Quaternion.identity);
                CallLogActivity("Add Smoke Cloud Object");
            }
        }
    }

    public void Add_Special_Object()
    {
        var allObjects = Resources.LoadAll("Object_Skins/", typeof(GameObject));
        foreach (GameObject allObject in allObjects)
        {
            if (allObject.name == "Special_Terrain_Object")
            {
                Instantiate(allObject, new Vector3(-7.5f, -1.49f, -9.5f), Quaternion.identity);
                CallLogActivity("Add Special Object");
            }
        }
    }


    public void Unit_Menu_Load()
    {
        UnitMenu.SetActive(true);
        UnitMenuClickNumber.text = "Click " + CurrentlyPreviewedUnit.GetComponent<Unit_Script>().click_number;
        UnitMenuActionTokens.text = CurrentlyPreviewedUnit.GetComponent<Unit_Script>().actionTokens + "Actions";
    }

    public void Add_Action()
    {
        CurrentlyPreviewedUnit.GetComponent<Unit_Script>().actionTokens += 1;
        Unit_Menu_Load();
        CallLogActivity("Action Added to Name: " + CurrentlyPreviewedUnit.name + " Team: " + CurrentlyPreviewedUnit.GetComponent<Unit_Script>().team + " Coordinates: X:" + CurrentlyPreviewedUnit.transform.position.x + " Y:" + CurrentlyPreviewedUnit.transform.position.y + " Z:" + CurrentlyPreviewedUnit.transform.position.z);
    }

    public void Remove_Action()
    {
        CurrentlyPreviewedUnit.GetComponent<Unit_Script>().actionTokens -= 1;
        CallLogActivity("Action Removed from Name: " + CurrentlyPreviewedUnit.name + " Team: " + CurrentlyPreviewedUnit.GetComponent<Unit_Script>().team + " Coordinates: X:" + CurrentlyPreviewedUnit.transform.position.x + " Y:" + CurrentlyPreviewedUnit.transform.position.y + " Z:" + CurrentlyPreviewedUnit.transform.position.z);
        Unit_Menu_Load();
        
    }

    public void Heal()
    {
        CurrentlyPreviewedUnit.GetComponent<Unit_Script>().click_number -= 1;
        Unit_Menu_Load();
        Preview_Unit(LastUnitPreviewedList, 0);
        CallLogActivity("Unit Healed 1 click Name: " + CurrentlyPreviewedUnit.name + " Team: " + CurrentlyPreviewedUnit.GetComponent<Unit_Script>().team + " Coordinates: X:" + CurrentlyPreviewedUnit.transform.position.x + " Y:" + CurrentlyPreviewedUnit.transform.position.y + " Z:" + CurrentlyPreviewedUnit.transform.position.z);
    }

    public void Damage()
    {
        CurrentlyPreviewedUnit.GetComponent<Unit_Script>().click_number += 1;
        Unit_Menu_Load();
        Preview_Unit(LastUnitPreviewedList, 0);
        CallLogActivity("Unit Damaged 1 click Name: " + CurrentlyPreviewedUnit.name + " Team: " + CurrentlyPreviewedUnit.GetComponent<Unit_Script>().team + " Coordinates: X:" + CurrentlyPreviewedUnit.transform.position.x + " Y:" + CurrentlyPreviewedUnit.transform.position.y + " Z:" + CurrentlyPreviewedUnit.transform.position.z);

    }

    public void KO_Unit()
    {
        Destroy(CurrentlyPreviewedUnit);
        UnitMenu.SetActive(false);
        CallLogActivity("Unit KOd Name: " + CurrentlyPreviewedUnit.name + " Team: " + CurrentlyPreviewedUnit.GetComponent<Unit_Script>().team + " Coordinates: X:" + CurrentlyPreviewedUnit.transform.position.x + " Y:" + CurrentlyPreviewedUnit.transform.position.y + " Z:" + CurrentlyPreviewedUnit.transform.position.z);

    }

    public void Exit_Game()
    {
        //Application.Quit();
        CallLogActivity("Exit To Main Menu");
        SceneManager.LoadScene("Main_Menu");
        Data_Storage.Instance.ClearAll();

        //SelectedMapImage.mainTexture = Resources.Load("Images/Maps/16x24/SN-01b-Blank-16x24") as Texture;
    }

    public void Team_Name_Dynamic_Dropdown_IndexChanged(int index)
    {
        selectedTeamLoadIndex = index;
    }


    //Instantiate Place Units
    //public void Place_Units(List<GameObject> LoadedTeam)
    //{
    //    int i = 0;
    //    int j = 0;

    //    if (ImportPlayer == 1)
    //    {
    //        foreach (GameObject unit in LoadedTeam)
    //        {
    //            if (i > 15)
    //            {
    //                j++;
    //                i = 0;
    //            }

    //            //Preview_Unit(team, i);
    //            GameObject newUnit = Instantiate(unit, new Vector3(-7.5f + i, -0.6f, -9.5f + j), Quaternion.identity);
    //            newUnit.name = unit.name;
    //            newUnit.GetComponent<Unit_Script>().team = 1;
    //            i++;
    //        }
    //    }
    //    else if (ImportPlayer == 2)
    //    {
    //        foreach (GameObject unit in LoadedTeam)
    //        {
    //            if (i > 15)
    //            {
    //                j++;
    //                i = 0;
    //            }

    //            //Preview_Unit(team, i);
    //            GameObject newUnit = Instantiate(unit, new Vector3(-7.5f + i, -0.6f, 13.5f - j), Quaternion.identity);
    //            newUnit.name = unit.name;
    //            newUnit.GetComponent<Unit_Script>().team = 2;
    //            i++;
    //        }
    //        SetPlayer2.isOn = false;
    //        ImportPlayer = 1;
    //    }


    //}



    public void CallLoadDynamicTeamNames()
    {
        StartCoroutine(LoadDynamicTeamNames());
    }

    IEnumerator LoadDynamicTeamNames()
    {
        loadedTeamsList.Clear();
        TeamIDList.Clear();
        TeamNameList.Clear();
        TeamPointsList.Clear();
        team_Name_Dropdown.ClearOptions();

        string WhereClauseText = "";

        WWWForm form = new WWWForm();

        int userId = PlayerPrefs.GetInt("User_ID");
        if (userId > 0)
        {
            WhereClauseText = "where User_ID = " + userId;
        }

        form.AddField("Where_Clause", WhereClauseText);

        using (UnityWebRequest www2 = UnityWebRequest.Post("https://www.stark44.com/Heroclix/getTeamNames.php", form))
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
                    var IteratedRowData = Teams.Substring(0, Teams.IndexOf(";"));
                    var DropdownData = IteratedRowData;
                    var NextTab = IteratedRowData.IndexOf("\t");

                    TeamIDList.Add(IteratedRowData.Substring(0, NextTab));
                    NextTab = IteratedRowData.IndexOf("\t");
                    IteratedRowData = IteratedRowData.Substring(NextTab + 1, IteratedRowData.Length - NextTab - 1);
                    NextTab = IteratedRowData.IndexOf("\t");

                    //Second To Last
                    TeamNameList.Add(IteratedRowData.Substring(0, NextTab));
                    NextTab = IteratedRowData.IndexOf("\t");
                    IteratedRowData = IteratedRowData.Substring(NextTab + 1, IteratedRowData.Length - NextTab - 1);

                    //Last
                    TeamPointsList.Add(IteratedRowData.Substring(0, IteratedRowData.Length));

                    loadedTeamsList.Add(TeamNameList[i] + " " + TeamPointsList[i]);

                    Teams = Teams.Substring(Teams.IndexOf(";") + 1, Teams.Length - Teams.IndexOf(";") - 1);
                    i++;
                }

            }
        }
        team_Name_Dropdown.AddOptions(loadedTeamsList);
        Team_Name_Dynamic_Dropdown_IndexChanged(0);

        Debug.Log("Fetched Teams");
        yield return null;
    }

    public void CallLoadTeam()
    {
        StartCoroutine(LoadDynamicTeam());
    }

    IEnumerator LoadDynamicTeam()
    {
        WWWForm form = new WWWForm();
        string chosenteamid = TeamIDList[selectedTeamLoadIndex];

        CallLogActivity("Load Team ID: " + chosenteamid);

        form.AddField("Team_ID", chosenteamid);
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
                Debug.Log("Team Loaded.");

                var NextTab = TeamData.IndexOf(",");

                //Second To Last
                TeamData = TeamData.Substring(NextTab + 1, TeamData.Length - NextTab - 1);

                //Last
                string SavedTeamData = TeamData.Substring(0, TeamData.Length);

                Dynamic_Team_Save_Script loadedTeam = new Dynamic_Team_Save_Script();

                var JSONStartIndex = SavedTeamData.IndexOf('{');
                var JSONEndIndex = SavedTeamData.LastIndexOf('}');
                SavedTeamData = SavedTeamData.Substring(JSONStartIndex, JSONEndIndex - JSONStartIndex + 1);

                loadedTeam = JsonUtility.FromJson<Dynamic_Team_Save_Script>(SavedTeamData);

                int i = 0;

                foreach (string unit in loadedTeam.TeamSetNames)
                {
                    //SelectedCollectorsNumber = loadedTeam.TeamSetNames[i];

                    Selected_Dynamic_Set_Name = loadedTeam.TeamSetNames[i];
                    Selected_Dynamic_Collectors_Number = loadedTeam.TeamCollectorsNumbers[i];

                    yield return StartCoroutine(GetDynamicUnitDial());
                    yield return StartCoroutine(GetDynamicUnitStats());

                    LoadDynamicUnit();

                    i++;
                }

            }
        }

        xLoadOffset = 0;
        zLoadOffset = 0;

        yield return null;
    }

    public void LoadDynamicUnit()
    {
        PreviewUnitList.Clear();

        var allUnitsDynamic = Resources.LoadAll("Units/Dynamic", typeof(GameObject));

        if (IsBaseType(Base_Type_d, "2x2"))
        {
            allUnitsDynamic = Resources.LoadAll("Units/Manual/2x2", typeof(GameObject));
        }
        else if (IsBaseType(Base_Type_d, "peanut") || Base_Type_d == "1x2")
        {
            allUnitsDynamic = Resources.LoadAll("Units/Manual/1x2", typeof(GameObject));
        }
        else if (IsBaseType(Base_Type_d, "2x4"))
        {
            allUnitsDynamic = Resources.LoadAll("Units/Manual/2x4", typeof(GameObject));
        }
        else if (IsBaseType(Base_Type_d, "3x6"))
        {
            allUnitsDynamic = Resources.LoadAll("Units/Manual/3x6", typeof(GameObject));
        }

        int importTeam = int.Parse(ImportPlayer.options[ImportPlayer.value].text);

        foreach (GameObject DynamicUnit in allUnitsDynamic)
        {
            // Team 1 and odd teams spawn on the team-1 side (z = -9.5).
            // Team 2 and even teams spawn on the team-2 side (z = 13.5).
            GameObject newUnit1;
            if (importTeam % 2 == 0)
            {
                newUnit1 = Instantiate(DynamicUnit, new Vector3(-7.5f + xLoadOffset, -1.1f, 13.5f - zLoadOffset), Quaternion.identity);
            }
            else
            {
                newUnit1 = Instantiate(DynamicUnit, new Vector3(-7.5f + xLoadOffset, -1.1f, -9.5f + zLoadOffset), Quaternion.identity);
            }

            Debug.Log("[LoadDynamicUnit] Base_Type_d='" + Base_Type_d + "' prefab='" + DynamicUnit.name + "' Unit_Script=" + (newUnit1.GetComponent<Unit_Script>() != null ? newUnit1.GetComponent<Unit_Script>().GetType().Name : "null") + " Manual=" + (newUnit1.GetComponent<Manual_Non_Single_Base_Unit_Script>() != null ? "true" : "null"));
            Debug.Log("[LoadDynamicUnit] DialInfoDynamic_SpeedPowerStr count=" + DialInfoDynamic_SpeedPowerStr.Count + (DialInfoDynamic_SpeedPowerStr.Count > 0 ? " [0]=" + DialInfoDynamic_SpeedPowerStr[0] : "") + " | DamagePowerStr count=" + DialInfoDynamic_DamagePowerStr.Count + (DialInfoDynamic_DamagePowerStr.Count > 0 ? " [0]=" + DialInfoDynamic_DamagePowerStr[0] : ""));

            if (newUnit1.GetComponent<Unit_Script>() != null)
            {
                newUnit1.GetComponent<Unit_Script>().speed_values = new List<int>(DialInfoDynamic_SpeedValue);
                newUnit1.GetComponent<Unit_Script>().attack_values = new List<int>(DialInfoDynamic_AttackValue);
                newUnit1.GetComponent<Unit_Script>().defense_values = new List<int>(DialInfoDynamic_DefenseValue);
                newUnit1.GetComponent<Unit_Script>().damage_values = new List<int>(DialInfoDynamic_DamageValue);

                newUnit1.GetComponent<Unit_Script>().speed_powers = new List<string>(DialInfoDynamic_SpeedPowerStr);
                newUnit1.GetComponent<Unit_Script>().attack_powers = new List<string>(DialInfoDynamic_AttackPowerStr);
                newUnit1.GetComponent<Unit_Script>().defense_powers = new List<string>(DialInfoDynamic_DefensePowerStr);
                newUnit1.GetComponent<Unit_Script>().damage_powers = new List<string>(DialInfoDynamic_DamagePowerStr);

                newUnit1.GetComponent<Unit_Script>().speed_powers_description = new List<string>(DialInfoDynamic_SpeedPowerDescription);
                newUnit1.GetComponent<Unit_Script>().attack_powers_description = new List<string>(DialInfoDynamic_AttackPowerDescription);
                newUnit1.GetComponent<Unit_Script>().defense_powers_description = new List<string>(DialInfoDynamic_DefensePowerDescription);
                newUnit1.GetComponent<Unit_Script>().damage_powers_description = new List<string>(DialInfoDynamic_DamagePowerDescription);

                { // snapshot first 3 powers for diagnosis
                    var _sp = newUnit1.GetComponent<Unit_Script>().speed_powers;
                    var _dp = newUnit1.GetComponent<Unit_Script>().damage_powers;
                    Debug.Log("[LoadDynamicUnit] After assignment — speed_powers[0..2]: " +
                        (_sp.Count > 0 ? _sp[0] : "(empty)") + ", " +
                        (_sp.Count > 1 ? _sp[1] : "-") + ", " +
                        (_sp.Count > 2 ? _sp[2] : "-") +
                        " | damage_powers[0..2]: " +
                        (_dp.Count > 0 ? _dp[0] : "(empty)") + ", " +
                        (_dp.Count > 1 ? _dp[1] : "-") + ", " +
                        (_dp.Count > 2 ? _dp[2] : "-"));
                }

                newUnit1.name = (Set_Name_d + "_" + Collectors_Number_d + "_" + Unit_Name_d).Replace(" ", "");

                newUnit1.GetComponent<Unit_Script>().actionTokens = 0;
                newUnit1.GetComponent<Unit_Script>().click_number = 1;
                newUnit1.GetComponent<Unit_Script>().isDynamic = 1;

                newUnit1.GetComponent<Unit_Script>().set = Set_Name_d;
                newUnit1.GetComponent<Unit_Script>().set_abbreviation = Set_Abbrev_d;
                newUnit1.GetComponent<Unit_Script>().collectors_number = Collectors_Number_d;
                newUnit1.GetComponent<Unit_Script>().unit_Name = Unit_Name_d;

                CurrentlyPreviewedUnit = newUnit1;
                CallLoadDynamicUnitImageSynch(newUnit1);

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

                newUnit1.GetComponent<Unit_Script>().team = importTeam;

                newUnit1.GetComponentInChildren<TMP_Text>().text = Unit_Name_d;


                List<GameObject> PreviewUnitList = new List<GameObject>();


                PreviewUnitList.Add(newUnit1);
                Preview_Unit(PreviewUnitList, 0);

                xLoadOffset++;

                if (xLoadOffset > 15)
                {
                    zLoadOffset++;
                    xLoadOffset = 0;
                }

                //newUnit1.transform.GetChild(1).GetComponent<Image>().sprite = DynamicUnitSculpt.sprite;
                //newUnit1.GetComponentInChildren<Image>().sprite = Sculpt.sprite;
            }
            else
            {
                newUnit1.GetComponent<Manual_Non_Single_Base_Unit_Script>().speed_values = new List<int>(DialInfoDynamic_SpeedValue);
                newUnit1.GetComponent<Manual_Non_Single_Base_Unit_Script>().attack_values = new List<int>(DialInfoDynamic_AttackValue);
                newUnit1.GetComponent<Manual_Non_Single_Base_Unit_Script>().defense_values = new List<int>(DialInfoDynamic_DefenseValue);
                newUnit1.GetComponent<Manual_Non_Single_Base_Unit_Script>().damage_values = new List<int>(DialInfoDynamic_DamageValue);

                newUnit1.GetComponent<Manual_Non_Single_Base_Unit_Script>().speed_powers = new List<string>(DialInfoDynamic_SpeedPowerStr);
                newUnit1.GetComponent<Manual_Non_Single_Base_Unit_Script>().attack_powers = new List<string>(DialInfoDynamic_AttackPowerStr);
                newUnit1.GetComponent<Manual_Non_Single_Base_Unit_Script>().defense_powers = new List<string>(DialInfoDynamic_DefensePowerStr);
                newUnit1.GetComponent<Manual_Non_Single_Base_Unit_Script>().damage_powers = new List<string>(DialInfoDynamic_DamagePowerStr);

                newUnit1.GetComponent<Manual_Non_Single_Base_Unit_Script>().speed_powers_description = new List<string>(DialInfoDynamic_SpeedPowerDescription);
                newUnit1.GetComponent<Manual_Non_Single_Base_Unit_Script>().attack_powers_description = new List<string>(DialInfoDynamic_AttackPowerDescription);
                newUnit1.GetComponent<Manual_Non_Single_Base_Unit_Script>().defense_powers_description = new List<string>(DialInfoDynamic_DefensePowerDescription);
                newUnit1.GetComponent<Manual_Non_Single_Base_Unit_Script>().damage_powers_description = new List<string>(DialInfoDynamic_DamagePowerDescription);


                newUnit1.name = (Set_Name_d + "_" + Collectors_Number_d + "_" + Unit_Name_d).Replace(" ", "");

                newUnit1.GetComponent<Manual_Non_Single_Base_Unit_Script>().actionTokens = 0;
                newUnit1.GetComponent<Manual_Non_Single_Base_Unit_Script>().click_number = 1;
                newUnit1.GetComponent<Manual_Non_Single_Base_Unit_Script>().isDynamic = 1;

                newUnit1.GetComponent<Manual_Non_Single_Base_Unit_Script>().set = Set_Name_d;
                newUnit1.GetComponent<Manual_Non_Single_Base_Unit_Script>().set_abbreviation = Set_Abbrev_d;
                newUnit1.GetComponent<Manual_Non_Single_Base_Unit_Script>().collectors_number = Collectors_Number_d;
                newUnit1.GetComponent<Manual_Non_Single_Base_Unit_Script>().unit_Name = Unit_Name_d;

                CurrentlyPreviewedUnit = newUnit1;
                CallLoadDynamicUnitImageSynch(newUnit1);

                newUnit1.GetComponent<Manual_Non_Single_Base_Unit_Script>().dial_length = Dial_Length_d;
                newUnit1.GetComponent<Manual_Non_Single_Base_Unit_Script>().last_click = Last_Click_d;
                newUnit1.GetComponent<Manual_Non_Single_Base_Unit_Script>().points = Points_d;
                newUnit1.GetComponent<Manual_Non_Single_Base_Unit_Script>().range_targets = Range_Targets_d;
                newUnit1.GetComponent<Manual_Non_Single_Base_Unit_Script>().range = Range_Value_d;

                newUnit1.GetComponent<Manual_Non_Single_Base_Unit_Script>().experience = Experience_d;
                newUnit1.GetComponent<Manual_Non_Single_Base_Unit_Script>().rarity = Rarity_d;
                newUnit1.GetComponent<Manual_Non_Single_Base_Unit_Script>().base_type = Base_Type_d;
                newUnit1.GetComponent<Manual_Non_Single_Base_Unit_Script>().speed_type = Speed_Symbol_d;
                newUnit1.GetComponent<Manual_Non_Single_Base_Unit_Script>().attack_type = Attack_Symbol_d;
                newUnit1.GetComponent<Manual_Non_Single_Base_Unit_Script>().defense_type = Defense_Symbol_d;
                newUnit1.GetComponent<Manual_Non_Single_Base_Unit_Script>().damage_type = Damage_Symbol_d;

                newUnit1.GetComponent<Manual_Non_Single_Base_Unit_Script>().IM_elevated = IM_Elevated_d;
                newUnit1.GetComponent<Manual_Non_Single_Base_Unit_Script>().IM_hindering = IM_Hindering_d;
                newUnit1.GetComponent<Manual_Non_Single_Base_Unit_Script>().IM_water = IM_Water_d;
                newUnit1.GetComponent<Manual_Non_Single_Base_Unit_Script>().IM_blocking = IM_Blocking_d;
                newUnit1.GetComponent<Manual_Non_Single_Base_Unit_Script>().IM_outdoor_blocking = IM_Outdoor_Blocking_d;
                newUnit1.GetComponent<Manual_Non_Single_Base_Unit_Script>().IM_blocking_destroy = IM_Blocking_Destroy_d;
                newUnit1.GetComponent<Manual_Non_Single_Base_Unit_Script>().IM_characters = IM_Characters_d;
                newUnit1.GetComponent<Manual_Non_Single_Base_Unit_Script>().IM_adjacent_characters = IM_Adjacent_Characters_d;

                newUnit1.GetComponent<Manual_Non_Single_Base_Unit_Script>().IT_elevated = IT_Elevated_d;
                newUnit1.GetComponent<Manual_Non_Single_Base_Unit_Script>().IT_hindering = IT_Hindering_d;
                newUnit1.GetComponent<Manual_Non_Single_Base_Unit_Script>().IT_blocking = IT_Blocking_d;
                newUnit1.GetComponent<Manual_Non_Single_Base_Unit_Script>().IT_blocking_destroy = IT_Blocking_Destroy_d;
                newUnit1.GetComponent<Manual_Non_Single_Base_Unit_Script>().IT_characters = IT_Characters_d;
                newUnit1.GetComponent<Manual_Non_Single_Base_Unit_Script>().IT_adjacent_characters = IT_Adjacent_Characters_d;

                int keywordCounter = 0;
                List<string> tempLoadedKeywords = new List<string>();

                while (Keywords_d.Contains(",") && Keywords_d.Length > 1)
                {
                    tempLoadedKeywords.Add(Keywords_d.Substring(0, Keywords_d.IndexOf(",")));
                    Keywords_d = Keywords_d.Substring(Keywords_d.IndexOf(",") + 1, Keywords_d.Length - Keywords_d.IndexOf(",") - 1);
                    keywordCounter++;
                }

                string[] tempKeyWords = new string[tempLoadedKeywords.Count];
                keywordCounter = 0;

                foreach (string keyword in tempLoadedKeywords)
                {
                    tempKeyWords[keywordCounter] = tempLoadedKeywords[keywordCounter];
                    keywordCounter++;
                }

                newUnit1.GetComponent<Manual_Non_Single_Base_Unit_Script>().keywords = tempKeyWords;
                newUnit1.GetComponent<Manual_Non_Single_Base_Unit_Script>().team_ability = Team_Ability_d;
                newUnit1.GetComponent<Manual_Non_Single_Base_Unit_Script>().trait1 = Trait1_d;
                newUnit1.GetComponent<Manual_Non_Single_Base_Unit_Script>().trait2 = Trait2_d;
                newUnit1.GetComponent<Manual_Non_Single_Base_Unit_Script>().trait3 = Trait3_d;
                newUnit1.GetComponent<Manual_Non_Single_Base_Unit_Script>().trait4 = Trait4_d;
                newUnit1.GetComponent<Manual_Non_Single_Base_Unit_Script>().trait5 = Trait5_d;
                newUnit1.GetComponent<Manual_Non_Single_Base_Unit_Script>().trait6 = Trait6_d;

                newUnit1.GetComponent<Manual_Non_Single_Base_Unit_Script>().team = importTeam;

                newUnit1.GetComponentInChildren<TMP_Text>().text = Unit_Name_d;


                List<GameObject> PreviewUnitList = new List<GameObject>();


                PreviewUnitList.Add(newUnit1);
                Preview_Unit(PreviewUnitList, 0);

                xLoadOffset++;

                if (xLoadOffset > 15)
                {
                    zLoadOffset++;
                    xLoadOffset = 0;
                }

                //newUnit1.transform.GetChild(1).GetComponent<Image>().sprite = DynamicUnitSculpt.sprite;
                //newUnit1.GetComponentInChildren<Image>().sprite = Sculpt.sprite;
            }
        }

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

    IEnumerator GetTADesc()
    {
        WWWForm form = new WWWForm();
        form.AddField("Team_Ability", teamAbility);

        using (UnityWebRequest www = UnityWebRequest.Post("https://www.stark44.com/Heroclix/getTADesc.php", form))
        {
            www.certificateHandler = new BypassCertificate();
            yield return www.SendWebRequest();

            if (www.result != UnityWebRequest.Result.Success)
            {
                Debug.Log(www.error);
            }
            else
            {
                teamAbilityDescription = www.downloadHandler.text;
            }
        }
    }

    public void Display_Speed_Power_Text(int SelectedSlot)
    {
        if (speedPowersList.Count() > 0)
        {
            if (PowerDescription.text == speedPowersList[SelectedSlot])
            {
                PowerDescription.text = speedPowersDescriptionsList[SelectedSlot];
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
            if (PowerDescription.text == attackPowersList[SelectedSlot])
            {
                PowerDescription.text = attackPowersDescriptionsList[SelectedSlot];
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
            if (PowerDescription.text == defensePowersList[SelectedSlot])
            {
                PowerDescription.text = defensePowersDescriptionsList[SelectedSlot];
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
            if (PowerDescription.text == damagePowersList[SelectedSlot])
            {
                PowerDescription.text = damagePowersDescriptionsList[SelectedSlot];
            }
            else
            {
                PowerDescription.text = damagePowersList[SelectedSlot];
            }

        }
    }

   
    public void Move_Unit()
    {
        CurrentlyPreviewedUnit.GetComponent<Unit_Script>().selected = true;
        CallLogActivity("Moved Unit: " + CurrentlyPreviewedUnit.name + " Team: " + CurrentlyPreviewedUnit.GetComponent<Unit_Script>().team + " Coordinates: X:" + CurrentlyPreviewedUnit.transform.position.x + " Y:" + CurrentlyPreviewedUnit.transform.position.y + " Z:" + CurrentlyPreviewedUnit.transform.position.z);
    }

    public void Move_Object()
    {
        CurrentlySelectedObject.GetComponent<Object_Script>().selected = true;
        CallLogActivity("Moved Object: " + CurrentlySelectedObject.name + " Coordinates: X:" + CurrentlySelectedObject.transform.position.x + " Y:" + CurrentlySelectedObject.transform.position.y + " Z:" + CurrentlySelectedObject.transform.position.z);
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


    public void TwoD6_Roll()
    {
        string CurrentRoll;
        List<int> TwoDice = new List<int>();
        TwoDice = Dice_Roll(2, 6);

        CurrentRoll = "Roll: " + (TwoDice[0] + TwoDice[1]) + " (" + TwoDice[0] + ", " + TwoDice[1] + ")";

        sendDiceToChat(CurrentRoll);
        CallLogActivity("2D6: " + CurrentRoll);
    }

    public void OneD6_Roll()
    {
        string CurrentRoll;
        List<int> OneDie = new List<int>();
        OneDie = Dice_Roll(2, 6);

        CurrentRoll = "Roll: " + OneDie[0];

        sendDiceToChat(CurrentRoll);
        CallLogActivity("1D6: " + CurrentRoll);
    }


    public void sendDiceToChat(string StringToAddToChat)
    {
        int i = 0;
        AllChatText.text = "";
        ChatLog.Add(StringToAddToChat);
        foreach (string str in ChatLog)
        {
            AllChatText.text += ChatLog[i] + "\r\n";
            i++;
        }

    }



    public List<int> Dice_Roll(int numberOfDice, int sidesOfDice)
    {
        List<int> DiceResults = new List<int>();

        for (int i = 0; i < numberOfDice; i++)
        {
            DiceResults.Add(Random.Range(0, sidesOfDice) + 1);
        }

        return DiceResults;

    }

    private Game_Save_Script CreateSaveGameObject(List<GameObject> ObjectList, List<GameObject> UnitList, List<GameObject> ManualUnitList)
    {
        string[] ObjectList_Name = new string[ObjectList.Count()];
        float[] ObjectList_LocationX = new float[ObjectList.Count()];
        float[] ObjectList_LocationY = new float[ObjectList.Count()];
        float[] ObjectList_LocationZ = new float[ObjectList.Count()];

        string[] UnitList_Name = new string[UnitList.Count()];
        float[] UnitList_LocationX = new float[UnitList.Count()];
        float[] UnitList_LocationY = new float[UnitList.Count()];
        float[] UnitList_LocationZ = new float[UnitList.Count()];
        int[] UnitList_ClickNumber = new int[UnitList.Count()];
        int[] UnitList_Actions = new int[UnitList.Count()];
        int[] UnitList_TeamNumber = new int[UnitList.Count()];
        int[] UnitList_IsDynamic = new int[UnitList.Count()];
        string[] UnitList_Set_Name = new string[UnitList.Count()];
        string[] UnitList_Collectors_Number = new string[UnitList.Count()];

        string[] ManualUnitList_Name = new string[ManualUnitList.Count()];
        float[] ManualUnitList_LocationX = new float[ManualUnitList.Count()];
        float[] ManualUnitList_LocationY = new float[ManualUnitList.Count()];
        float[] ManualUnitList_LocationZ = new float[ManualUnitList.Count()];
        int[] ManualUnitList_ClickNumber = new int[ManualUnitList.Count()];
        int[] ManualUnitList_Actions = new int[ManualUnitList.Count()];
        int[] ManualUnitList_TeamNumber = new int[ManualUnitList.Count()];
        int[] ManualUnitList_IsDynamic = new int[ManualUnitList.Count()];
        string[] ManualUnitList_Set_Name = new string[ManualUnitList.Count()];
        string[] ManualUnitList_Collectors_Number = new string[ManualUnitList.Count()];

        int i = 0;

        foreach (GameObject targetObject in ObjectList)
        {
            ObjectList_Name[i] = targetObject.GetComponent<Object_Script>().objectName;

            ObjectList_LocationX[i] = targetObject.transform.position.x;//XCoord
            ObjectList_LocationY[i] = targetObject.transform.position.y;//YCoord
            ObjectList_LocationZ[i] = targetObject.transform.position.z;//ZCoord
            i++;
        }

        i = 0;

        foreach (GameObject targetUnit in UnitList)
        {
            Unit_Script us = targetUnit.GetComponent<Unit_Script>();
            if (us == null)
            {
                Debug.LogWarning("[CreateSaveGameObject] Unit '" + targetUnit.name + "' is missing Unit_Script — skipping.");
                i++;
                continue;
            }
            UnitList_Name[i] = targetUnit.name;

            UnitList_LocationX[i] = targetUnit.transform.position.x;
            UnitList_LocationY[i] = targetUnit.transform.position.y;
            UnitList_LocationZ[i] = targetUnit.transform.position.z;
            UnitList_ClickNumber[i] = us.click_number;
            UnitList_Actions[i] = us.actionTokens;
            UnitList_TeamNumber[i] = us.team;
            UnitList_IsDynamic[i] = us.isDynamic;
            UnitList_Set_Name[i] = us.set;
            UnitList_Collectors_Number[i] = us.collectors_number;

            i++;
        }

        i = 0;

        foreach (GameObject targetManualUnit in ManualUnitList)
        {
            Unit_Script mus = targetManualUnit.GetComponent<Unit_Script>();
            if (mus == null)
            {
                Debug.LogWarning("[CreateSaveGameObject] ManualUnit '" + targetManualUnit.name + "' is missing Unit_Script — skipping.");
                i++;
                continue;
            }
            ManualUnitList_Name[i] = targetManualUnit.name;

            ManualUnitList_LocationX[i] = targetManualUnit.transform.position.x;
            ManualUnitList_LocationY[i] = targetManualUnit.transform.position.y;
            ManualUnitList_LocationZ[i] = targetManualUnit.transform.position.z;
            ManualUnitList_ClickNumber[i] = mus.click_number;
            ManualUnitList_Actions[i] = mus.actionTokens;
            ManualUnitList_TeamNumber[i] = mus.team;
            ManualUnitList_IsDynamic[i] = mus.isDynamic;
            ManualUnitList_Set_Name[i] = mus.set;
            ManualUnitList_Collectors_Number[i] = mus.collectors_number;

            i++;
        }

        Game_Save_Script saveGameData = new Game_Save_Script()
        {
            ObjectList_Name = ObjectList_Name,
            ObjectList_LocationX = ObjectList_LocationX,
            ObjectList_LocationY = ObjectList_LocationY,
            ObjectList_LocationZ = ObjectList_LocationZ,
            UnitList_Name = UnitList_Name,
            UnitList_LocationX = UnitList_LocationX,
            UnitList_LocationY = UnitList_LocationY,
            UnitList_LocationZ = UnitList_LocationZ,
            UnitList_ClickNumber = UnitList_ClickNumber,
            UnitList_Actions = UnitList_Actions,
            UnitList_TeamNumber = UnitList_TeamNumber,
            UnitList_IsDynamic = UnitList_IsDynamic,
            UnitList_Set_Name = UnitList_Set_Name,
            UnitList_Collectors_Number = UnitList_Collectors_Number,
            ManualUnitList_Name = ManualUnitList_Name,
            ManualUnitList_LocationX = ManualUnitList_LocationX,
            ManualUnitList_LocationY = ManualUnitList_LocationY,
            ManualUnitList_LocationZ = ManualUnitList_LocationZ,
            ManualUnitList_ClickNumber = ManualUnitList_ClickNumber,
            ManualUnitList_Actions = ManualUnitList_Actions,
            ManualUnitList_TeamNumber = ManualUnitList_TeamNumber,
            ManualUnitList_IsDynamic = ManualUnitList_IsDynamic,
            ManualUnitList_Set_Name = ManualUnitList_Set_Name,
            ManualUnitList_Collectors_Number = ManualUnitList_Collectors_Number
        };

        return saveGameData;
    }

    private Chat_Save_Script CreateSaveChatObject(List<string> ChatLog)
    {

        Chat_Save_Script saveChatData = new Chat_Save_Script() { ChatLog = ChatLog };

        return saveChatData;
    }

    public void SaveGame()
    {
        string storedUser = PlayerPrefs.GetString("Active_Username");
        if (string.IsNullOrEmpty(storedUser)) storedUser = PlayerPrefs.GetString("Stored_Username");
        string activePlayer = ActivePlayerText != null ? ActivePlayerText.text : "";

        if (!activePlayerIsNull && activePlayer != storedUser)
        {
            Debug.Log("[SaveGame] Blocked — not your turn. Active: '" + activePlayer + "' | You: '" + storedUser + "'");
            return;
        }

        //Find Units & Objects
        GameObject[] GameData = new GameObject[10000];
        List<GameObject> SaveGameData_Objects = new List<GameObject>();
        List<GameObject> SaveGameData_Units = new List<GameObject>();
        List<GameObject> SaveGameData_ManualUnits = new List<GameObject>();

        GameData = GameObject.FindGameObjectsWithTag("Unit");
        foreach (GameObject o in GameData)
            SaveGameData_Units.Add(o);

        GameData = GameObject.FindGameObjectsWithTag("Object");
        foreach (GameObject o in GameData)
            SaveGameData_Objects.Add(o);

        GameData = GameObject.FindGameObjectsWithTag("ManualUnit");
        foreach (GameObject o in GameData)
            SaveGameData_ManualUnits.Add(o);

        Debug.Log("[SaveGame] Collected " + SaveGameData_Units.Count + " units, " +
                  SaveGameData_ManualUnits.Count + " manual units, " +
                  SaveGameData_Objects.Count + " objects.");

        for (int dbg = 0; dbg < SaveGameData_Units.Count; dbg++)
        {
            var u = SaveGameData_Units[dbg].GetComponent<Unit_Script>();
            Debug.Log("[SaveGame] Unit[" + dbg + "] name='" + SaveGameData_Units[dbg].name +
                      "' pos=" + SaveGameData_Units[dbg].transform.position +
                      " isDynamic=" + (u != null ? u.isDynamic.ToString() : "NO Unit_Script!") +
                      " set='" + (u != null ? u.set : "") +
                      "' cn='" + (u != null ? u.collectors_number : "") + "'");
        }
        for (int dbg = 0; dbg < SaveGameData_ManualUnits.Count; dbg++)
        {
            Debug.Log("[SaveGame] ManualUnit[" + dbg + "] name='" + SaveGameData_ManualUnits[dbg].name +
                      "' pos=" + SaveGameData_ManualUnits[dbg].transform.position + "'");
        }

        Game_Save_Script currentGame = CreateSaveGameObject(SaveGameData_Objects, SaveGameData_Units, SaveGameData_ManualUnits);
        string GameJSON = JsonUtility.ToJson(currentGame);

        Chat_Save_Script currentChat = CreateSaveChatObject(ChatLog);
        string ChatJSON = JsonUtility.ToJson(currentChat);

        if (Data_Storage.Instance.Game_ID <= 0)
        {
            Debug.LogError("[SaveGame] Cannot save — Game_ID is invalid (" + Data_Storage.Instance.Game_ID + ").");
            return;
        }

        if (SaveGameData_Units.Count == 0 && SaveGameData_ManualUnits.Count == 0)
        {
            Debug.LogWarning("[SaveGame] Refusing to save — no units in scene. This would overwrite good save data with an empty state.");
            return;
        }

        Debug.Log("[SaveGame] Game_ID=" + Data_Storage.Instance.Game_ID + " | JSON length=" + GameJSON.Length +
                  " | JSON preview: " + (GameJSON.Length > 200 ? GameJSON.Substring(0, 200) + "..." : GameJSON));
        StartCoroutine(SaveGameToServer(GameJSON, ChatJSON));
    }

    IEnumerator SaveGameToServer(string SaveGameString, string SaveChatString)
    {
        WWWForm form = new WWWForm();
        form.AddField("Saved_Game", SaveGameString);
        form.AddField("Saved_Chat", SaveChatString);
        form.AddField("Game_ID", Data_Storage.Instance.Game_ID);

        Debug.Log("[SaveGameToServer] Sending save for Game_ID=" + Data_Storage.Instance.Game_ID);
        using (UnityWebRequest www = UnityWebRequest.Post("https://www.stark44.com/Heroclix/SaveGame.php", form))
        {
            www.certificateHandler = new BypassCertificate();
            yield return www.SendWebRequest();

            if (www.result != UnityWebRequest.Result.Success)
            {
                Debug.Log("[SaveGameToServer] Error: " + www.error);
            }
            else
            {
                //byte[] results = www.downloadHandler.data;
                string WasTheGameSaved = www.downloadHandler.text;
                Debug.Log("[SaveGameToServer] Response: '" + WasTheGameSaved + "'");

                if (WasTheGameSaved.Trim() == "0")
                {
                    Debug.Log("[SaveGameToServer] Game saved successfully.");

                    // Verify what actually landed in the DB
                    int savedGameID = Data_Storage.Instance.Game_ID;
                    WWWForm verifyForm = new WWWForm();
                    verifyForm.AddField("Game_ID", savedGameID);
                    using (UnityWebRequest vwww = UnityWebRequest.Post("https://www.stark44.com/Heroclix/VerifySave.php", verifyForm))
                    {
                        vwww.certificateHandler = new BypassCertificate();
                        yield return vwww.SendWebRequest();
                        Debug.Log("[SaveGameToServer] DB verify for Game_ID=" + savedGameID + ": " +
                                  (vwww.result == UnityWebRequest.Result.Success ? vwww.downloadHandler.text : "request failed: " + vwww.error));
                    }

                    CallLogActivity("Game Saved.");
                    ChatLog.Add("Game Saved Successfully.");

                    AllChatText.text = "";
                    int i = 0;

                    foreach (string str in ChatLog)
                    {
                        AllChatText.text += ChatLog[i] + "\r\n";
                        i++;
                    }
                }
                else
                {
                    Debug.LogError("[SaveGameToServer] Server returned unexpected response: '" + WasTheGameSaved + "'");
                }

            }
        }

    }


    public void PlayGame_InstantiateUnits()
    {
        var allUnitsDynamic = Resources.LoadAll("Units/Dynamic", typeof(GameObject));
        var allUnitsDynamic2x2 = Resources.LoadAll("Units/Manual/2x2", typeof(GameObject));
        var allUnitsDynamic1x2 = Resources.LoadAll("Units/Manual/1x2", typeof(GameObject));
        var allUnitsDynamic2x4 = Resources.LoadAll("Units/Manual/2x4", typeof(GameObject));
        var allUnitsDynamic3x6 = Resources.LoadAll("Units/Manual/3x6", typeof(GameObject));


        for (int i = 0; i < Set_Name_da.Count(); i++)
        {
            GameObject DynamicUnit = null;

            if (Base_Type_da[i] == "Standard" || Base_Type_da[i] == "1x1")
            {
                DynamicUnit = (GameObject)allUnitsDynamic[0];
            }
            else if (IsBaseType(Base_Type_da[i], "2x2"))
            {
                DynamicUnit = (GameObject)allUnitsDynamic2x2[0];
            }
            else if (IsBaseType(Base_Type_da[i], "peanut") || Base_Type_da[i] == "1x2")
            {
                DynamicUnit = (GameObject)allUnitsDynamic1x2[0];
            }
            else if (IsBaseType(Base_Type_da[i], "2x4"))
            {
                DynamicUnit = (GameObject)allUnitsDynamic2x4[0];
            }
            else if (IsBaseType(Base_Type_da[i], "3x6"))
            {
                DynamicUnit = (GameObject)allUnitsDynamic3x6[0];
            }

            if (DynamicUnit == null)
            {
                Debug.LogWarning("[PlayGame_InstantiateUnits] Skipping unit[" + i + "] '" + Set_Name_da[i] + " " + Collectors_Number_da[i] + "' — Base_Type_da='" + Base_Type_da[i] + "' (stats not loaded)");
                continue;
            }

            GameObject newUnit = Instantiate(DynamicUnit, new Vector3(LoadGame_UnitList_LocationX[i], LoadGame_UnitList_LocationY[i], LoadGame_UnitList_LocationZ[i]), Quaternion.identity);

            List<int> tempStatList = SpeedVal_List[i];
            List<int> tempStatList2 = AttackVal_List[i];
            List<int> tempStatList3 = DefenseVal_List[i];
            List<int> tempStatList4 = DamageVal_List[i];

            List<string> tempStatList5 = SpeedPow_List[i];
            List<string> tempStatList6 = AttackPow_List[i];
            List<string> tempStatList7 = DefensePow_List[i];
            List<string> tempStatList8 = DamagePow_List[i];

            List<string> tempStatList9 = SpeedPowDesc_List[i];
            List<string> tempStatList10 = AttackPowDesc_List[i];
            List<string> tempStatList11 = DefensePowDesc_List[i];
            List<string> tempStatList12 = DamagePowDesc_List[i];


            if (Base_Type_da[i] == "Standard" || Base_Type_da[i] == "1x1")
            {
                //for (int j = 0; j < tempStatList.Count(); j++)
                //{
                    newUnit.GetComponent<Unit_Script>().speed_values = tempStatList;
                    newUnit.GetComponent<Unit_Script>().attack_values = tempStatList2;
                    newUnit.GetComponent<Unit_Script>().defense_values = tempStatList3;
                    newUnit.GetComponent<Unit_Script>().damage_values = tempStatList4;

                    newUnit.GetComponent<Unit_Script>().speed_powers = tempStatList5;
                    newUnit.GetComponent<Unit_Script>().attack_powers = tempStatList6;
                    newUnit.GetComponent<Unit_Script>().defense_powers = tempStatList7;
                    newUnit.GetComponent<Unit_Script>().damage_powers = tempStatList8;

                    newUnit.GetComponent<Unit_Script>().speed_powers_description = tempStatList9;
                    newUnit.GetComponent<Unit_Script>().attack_powers_description = tempStatList10;
                    newUnit.GetComponent<Unit_Script>().defense_powers_description = tempStatList11;
                    newUnit.GetComponent<Unit_Script>().damage_powers_description = tempStatList12;
                //}
            }
            else
            {
                Unit_Script _powUs = newUnit.GetComponent<Unit_Script>();
                Manual_Non_Single_Base_Unit_Script _powMs = newUnit.GetComponent<Manual_Non_Single_Base_Unit_Script>();
                for (int j = 0; j < tempStatList.Count(); j++)
                {
                    if (_powUs != null)
                    {
                        _powUs.speed_values.Add(tempStatList[j]);
                        _powUs.attack_values.Add(tempStatList2[j]);
                        _powUs.defense_values.Add(tempStatList3[j]);
                        _powUs.damage_values.Add(tempStatList4[j]);
                        _powUs.speed_powers.Add(tempStatList5[j]);
                        _powUs.attack_powers.Add(tempStatList6[j]);
                        _powUs.defense_powers.Add(tempStatList7[j]);
                        _powUs.damage_powers.Add(tempStatList8[j]);
                        _powUs.speed_powers_description.Add(tempStatList9[j]);
                        _powUs.attack_powers_description.Add(tempStatList10[j]);
                        _powUs.defense_powers_description.Add(tempStatList11[j]);
                        _powUs.damage_powers_description.Add(tempStatList12[j]);
                    }
                    else if (_powMs != null)
                    {
                        _powMs.speed_values.Add(tempStatList[j]);
                        _powMs.attack_values.Add(tempStatList2[j]);
                        _powMs.defense_values.Add(tempStatList3[j]);
                        _powMs.damage_values.Add(tempStatList4[j]);
                        _powMs.speed_powers.Add(tempStatList5[j]);
                        _powMs.attack_powers.Add(tempStatList6[j]);
                        _powMs.defense_powers.Add(tempStatList7[j]);
                        _powMs.damage_powers.Add(tempStatList8[j]);
                        _powMs.speed_powers_description.Add(tempStatList9[j]);
                        _powMs.attack_powers_description.Add(tempStatList10[j]);
                        _powMs.defense_powers_description.Add(tempStatList11[j]);
                        _powMs.damage_powers_description.Add(tempStatList12[j]);
                    }
                }
            }

            

            newUnit.name = (Set_Name_da[i] + "_" + Collectors_Number_da[i] + "_" + Unit_Name_da[i]).Replace(" ", "");


            if (Base_Type_da[i] == "Standard" || Base_Type_da[i] == "1x1")
            {
                newUnit.GetComponent<Unit_Script>().actionTokens = LoadGame_UnitList_Actions[i];
                newUnit.GetComponent<Unit_Script>().click_number = LoadGame_UnitList_ClickNumber[i];
                newUnit.GetComponent<Unit_Script>().team = LoadGame_UnitList_TeamNumber[i];

                newUnit.GetComponent<Unit_Script>().isDynamic = 1;

                newUnit.GetComponent<Unit_Script>().set = Set_Name_da[i];
                newUnit.GetComponent<Unit_Script>().set_abbreviation = Set_Abbrev_da[i];
                newUnit.GetComponent<Unit_Script>().collectors_number = Collectors_Number_da[i];
                newUnit.GetComponent<Unit_Script>().unit_Name = Unit_Name_da[i];

                CurrentlyPreviewedUnit = newUnit;
                CallLoadDynamicUnitImageSynch(newUnit);

                newUnit.GetComponent<Unit_Script>().dial_length = Dial_Length_da[i];
                newUnit.GetComponent<Unit_Script>().last_click = Last_Click_da[i];
                newUnit.GetComponent<Unit_Script>().points = Points_da[i];
                newUnit.GetComponent<Unit_Script>().range_targets = Range_Targets_da[i];
                newUnit.GetComponent<Unit_Script>().range = Range_Value_da[i];

                newUnit.GetComponent<Unit_Script>().experience = Experience_da[i];
                newUnit.GetComponent<Unit_Script>().rarity = Rarity_da[i];
                newUnit.GetComponent<Unit_Script>().base_type = Base_Type_da[i];
                newUnit.GetComponent<Unit_Script>().speed_type = Speed_Symbol_da[i];
                newUnit.GetComponent<Unit_Script>().attack_type = Attack_Symbol_da[i];
                newUnit.GetComponent<Unit_Script>().defense_type = Defense_Symbol_da[i];
                newUnit.GetComponent<Unit_Script>().damage_type = Damage_Symbol_da[i];

                newUnit.GetComponent<Unit_Script>().IM_elevated = IM_Elevated_da[i];
                newUnit.GetComponent<Unit_Script>().IM_hindering = IM_Hindering_da[i];
                newUnit.GetComponent<Unit_Script>().IM_water = IM_Water_da[i];
                newUnit.GetComponent<Unit_Script>().IM_blocking = IM_Blocking_da[i];
                newUnit.GetComponent<Unit_Script>().IM_outdoor_blocking = IM_Outdoor_Blocking_da[i];
                newUnit.GetComponent<Unit_Script>().IM_blocking_destroy = IM_Blocking_destroy_da[i];
                newUnit.GetComponent<Unit_Script>().IM_characters = IM_Characters_da[i];
                newUnit.GetComponent<Unit_Script>().IM_adjacent_characters = IM_Adjacent_Characters_da[i];

                newUnit.GetComponent<Unit_Script>().IT_elevated = IT_Elevated_da[i];
                newUnit.GetComponent<Unit_Script>().IT_hindering = IT_Hindering_da[i];
                newUnit.GetComponent<Unit_Script>().IT_blocking = IT_Blocking_da[i];
                newUnit.GetComponent<Unit_Script>().IT_blocking_destroy = IT_Blocking_destroy_da[i];
                newUnit.GetComponent<Unit_Script>().IT_characters = IT_Characters_da[i];
                newUnit.GetComponent<Unit_Script>().IT_adjacent_characters = IT_Adjacent_Characters_da[i];

                int keywordCounter = 0;
                List<string> tempLoadedKeywords = new List<string>();

                while (Keywords_da[i].Contains(",") && Keywords_da[i].Length > 1)
                {
                    tempLoadedKeywords.Add(Keywords_da[i].Substring(0, Keywords_da[i].IndexOf(",")));
                    Keywords_da[i] = Keywords_da[i].Substring(Keywords_da[i].IndexOf(",") + 1, Keywords_da[i].Length - Keywords_da[i].IndexOf(",") - 1);
                    keywordCounter++;
                }

                string[] tempKeyWords = new string[tempLoadedKeywords.Count];
                keywordCounter = 0;

                foreach (string keyword in tempLoadedKeywords)
                {
                    tempKeyWords[keywordCounter] = tempLoadedKeywords[keywordCounter];
                    keywordCounter++;
                }

                newUnit.GetComponent<Unit_Script>().keywords = tempKeyWords;
                newUnit.GetComponent<Unit_Script>().team_ability = Team_Ability_da[i];
                newUnit.GetComponent<Unit_Script>().trait1 = Trait1_da[i];
                newUnit.GetComponent<Unit_Script>().trait2 = Trait2_da[i];
                newUnit.GetComponent<Unit_Script>().trait3 = Trait3_da[i];
                newUnit.GetComponent<Unit_Script>().trait4 = Trait4_da[i];
                newUnit.GetComponent<Unit_Script>().trait5 = Trait5_da[i];
                newUnit.GetComponent<Unit_Script>().trait6 = Trait6_da[i];

                newUnit.GetComponentInChildren<TMP_Text>().text = Unit_Name_da[i];
            }
            else
            {
                newUnit.GetComponent<Manual_Non_Single_Base_Unit_Script>().actionTokens = LoadGame_UnitList_Actions[i];
                newUnit.GetComponent<Manual_Non_Single_Base_Unit_Script>().click_number = LoadGame_UnitList_ClickNumber[i];
                newUnit.GetComponent<Manual_Non_Single_Base_Unit_Script>().team = LoadGame_UnitList_TeamNumber[i];

                newUnit.GetComponent<Manual_Non_Single_Base_Unit_Script>().isDynamic = 1;

                newUnit.GetComponent<Manual_Non_Single_Base_Unit_Script>().set = Set_Name_da[i];
                newUnit.GetComponent<Manual_Non_Single_Base_Unit_Script>().set_abbreviation = Set_Abbrev_da[i];
                newUnit.GetComponent<Manual_Non_Single_Base_Unit_Script>().collectors_number = Collectors_Number_da[i];
                newUnit.GetComponent<Manual_Non_Single_Base_Unit_Script>().unit_Name = Unit_Name_da[i];

                CurrentlyPreviewedUnit = newUnit;
                CallLoadDynamicUnitImageSynch(newUnit);

                newUnit.GetComponent<Manual_Non_Single_Base_Unit_Script>().dial_length = Dial_Length_da[i];
                newUnit.GetComponent<Manual_Non_Single_Base_Unit_Script>().last_click = Last_Click_da[i];
                newUnit.GetComponent<Manual_Non_Single_Base_Unit_Script>().points = Points_da[i];
                newUnit.GetComponent<Manual_Non_Single_Base_Unit_Script>().range_targets = Range_Targets_da[i];
                newUnit.GetComponent<Manual_Non_Single_Base_Unit_Script>().range = Range_Value_da[i];

                newUnit.GetComponent<Manual_Non_Single_Base_Unit_Script>().experience = Experience_da[i];
                newUnit.GetComponent<Manual_Non_Single_Base_Unit_Script>().rarity = Rarity_da[i];
                newUnit.GetComponent<Manual_Non_Single_Base_Unit_Script>().base_type = Base_Type_da[i];
                newUnit.GetComponent<Manual_Non_Single_Base_Unit_Script>().speed_type = Speed_Symbol_da[i];
                newUnit.GetComponent<Manual_Non_Single_Base_Unit_Script>().attack_type = Attack_Symbol_da[i];
                newUnit.GetComponent<Manual_Non_Single_Base_Unit_Script>().defense_type = Defense_Symbol_da[i];
                newUnit.GetComponent<Manual_Non_Single_Base_Unit_Script>().damage_type = Damage_Symbol_da[i];

                newUnit.GetComponent<Manual_Non_Single_Base_Unit_Script>().IM_elevated = IM_Elevated_da[i];
                newUnit.GetComponent<Manual_Non_Single_Base_Unit_Script>().IM_hindering = IM_Hindering_da[i];
                newUnit.GetComponent<Manual_Non_Single_Base_Unit_Script>().IM_water = IM_Water_da[i];
                newUnit.GetComponent<Manual_Non_Single_Base_Unit_Script>().IM_blocking = IM_Blocking_da[i];
                newUnit.GetComponent<Manual_Non_Single_Base_Unit_Script>().IM_outdoor_blocking = IM_Outdoor_Blocking_da[i];
                newUnit.GetComponent<Manual_Non_Single_Base_Unit_Script>().IM_blocking_destroy = IM_Blocking_destroy_da[i];
                newUnit.GetComponent<Manual_Non_Single_Base_Unit_Script>().IM_characters = IM_Characters_da[i];
                newUnit.GetComponent<Manual_Non_Single_Base_Unit_Script>().IM_adjacent_characters = IM_Adjacent_Characters_da[i];

                newUnit.GetComponent<Manual_Non_Single_Base_Unit_Script>().IT_elevated = IT_Elevated_da[i];
                newUnit.GetComponent<Manual_Non_Single_Base_Unit_Script>().IT_hindering = IT_Hindering_da[i];
                newUnit.GetComponent<Manual_Non_Single_Base_Unit_Script>().IT_blocking = IT_Blocking_da[i];
                newUnit.GetComponent<Manual_Non_Single_Base_Unit_Script>().IT_blocking_destroy = IT_Blocking_destroy_da[i];
                newUnit.GetComponent<Manual_Non_Single_Base_Unit_Script>().IT_characters = IT_Characters_da[i];
                newUnit.GetComponent<Manual_Non_Single_Base_Unit_Script>().IT_adjacent_characters = IT_Adjacent_Characters_da[i];

                int keywordCounter = 0;
                List<string> tempLoadedKeywords = new List<string>();

                while (Keywords_da[i].Contains(",") && Keywords_da[i].Length > 1)
                {
                    tempLoadedKeywords.Add(Keywords_da[i].Substring(0, Keywords_da[i].IndexOf(",")));
                    Keywords_da[i] = Keywords_da[i].Substring(Keywords_da[i].IndexOf(",") + 1, Keywords_da[i].Length - Keywords_da[i].IndexOf(",") - 1);
                    keywordCounter++;
                }

                string[] tempKeyWords = new string[tempLoadedKeywords.Count];
                keywordCounter = 0;

                foreach (string keyword in tempLoadedKeywords)
                {
                    tempKeyWords[keywordCounter] = tempLoadedKeywords[keywordCounter];
                    keywordCounter++;
                }

                newUnit.GetComponent<Manual_Non_Single_Base_Unit_Script>().keywords = tempKeyWords;
                newUnit.GetComponent<Manual_Non_Single_Base_Unit_Script>().team_ability = Team_Ability_da[i];
                newUnit.GetComponent<Manual_Non_Single_Base_Unit_Script>().trait1 = Trait1_da[i];
                newUnit.GetComponent<Manual_Non_Single_Base_Unit_Script>().trait2 = Trait2_da[i];
                newUnit.GetComponent<Manual_Non_Single_Base_Unit_Script>().trait3 = Trait3_da[i];
                newUnit.GetComponent<Manual_Non_Single_Base_Unit_Script>().trait4 = Trait4_da[i];
                newUnit.GetComponent<Manual_Non_Single_Base_Unit_Script>().trait5 = Trait5_da[i];
                newUnit.GetComponent<Manual_Non_Single_Base_Unit_Script>().trait6 = Trait6_da[i];

                newUnit.GetComponentInChildren<TMP_Text>().text = Unit_Name_da[i];
            }


           
        }
    }

    public void LoadGame()
    {
        StartCoroutine(LoadGameCoroutine());
    }

    IEnumerator LoadGameCoroutine()
    {
        Debug.Log("[LoadGame] Saved_Game length=" + Data_Storage.Instance.Saved_Game.Length +
                  " | preview: " + (Data_Storage.Instance.Saved_Game.Length > 100
                      ? Data_Storage.Instance.Saved_Game.Substring(0, 100) + "..."
                      : Data_Storage.Instance.Saved_Game));

        //Load Units & Objects
        if (Data_Storage.Instance.Saved_Game.Length > 0)
        {
            Game_Save_Script loadedGame = new Game_Save_Script();
            Chat_Save_Script loadedChat = new Chat_Save_Script();

            var allUnits = Resources.LoadAll("Units/", typeof(GameObject));
            var allObjects = Resources.LoadAll("Object_Skins/", typeof(GameObject));

            Debug.Log("[LoadGame] allUnits count=" + allUnits.Length + " | allObjects count=" + allObjects.Length);

            var JSONStartIndex = Data_Storage.Instance.Saved_Game.IndexOf('{');
            var JSONEndIndex = Data_Storage.Instance.Saved_Game.LastIndexOf('}');
            Data_Storage.Instance.Saved_Game = Data_Storage.Instance.Saved_Game.Substring(JSONStartIndex, JSONEndIndex - JSONStartIndex + 1);

            JSONStartIndex = Data_Storage.Instance.Saved_Chat.IndexOf('{');
            JSONEndIndex = Data_Storage.Instance.Saved_Chat.LastIndexOf('}');
            if (JSONStartIndex > 0 && JSONEndIndex > 0)
            {
                Data_Storage.Instance.Saved_Chat = Data_Storage.Instance.Saved_Chat.Substring(JSONStartIndex, JSONEndIndex - JSONStartIndex + 1);
            }

            loadedGame = JsonUtility.FromJson<Game_Save_Script>(Data_Storage.Instance.Saved_Game);
            loadedChat = JsonUtility.FromJson<Chat_Save_Script>(Data_Storage.Instance.Saved_Chat);

            int unitCount   = loadedGame.UnitList_Name   != null ? loadedGame.UnitList_Name.Length   : 0;
            int manualCount = loadedGame.ManualUnitList_Name != null ? loadedGame.ManualUnitList_Name.Length : 0;
            int objectCount = loadedGame.ObjectList_Name != null ? loadedGame.ObjectList_Name.Length : 0;
            Debug.Log("[LoadGame] Deserialized: " + unitCount + " units, " + manualCount + " manual units, " + objectCount + " objects.");

            if (loadedGame.UnitList_Name != null && loadedGame.UnitList_Name.Length > 0)
            {
                for (int i = 0; i < loadedGame.UnitList_Name.Length; i++)
                {
                    Debug.Log("[LoadGame] Unit[" + i + "] name='" + loadedGame.UnitList_Name[i] +
                              "' isDynamic=" + loadedGame.UnitList_IsDynamic[i] +
                              " set='" + loadedGame.UnitList_Set_Name[i] +
                              "' cn='" + loadedGame.UnitList_Collectors_Number[i] +
                              "' pos=(" + loadedGame.UnitList_LocationX[i] + "," +
                              loadedGame.UnitList_LocationY[i] + "," +
                              loadedGame.UnitList_LocationZ[i] + ")");

                    if (loadedGame.UnitList_IsDynamic[i] == 0)
                    {
                        bool matched = false;
                        foreach (GameObject allUnit in allUnits)
                        {
                            if (allUnit.name == loadedGame.UnitList_Name[i])
                            {
                                matched = true;
                                GameObject newUnit = Instantiate(allUnit, new Vector3(loadedGame.UnitList_LocationX[i], loadedGame.UnitList_LocationY[i], loadedGame.UnitList_LocationZ[i]), Quaternion.identity);
                                newUnit.name = allUnit.name;
                                newUnit.GetComponent<Unit_Script>().actionTokens = loadedGame.UnitList_Actions[i];
                                newUnit.GetComponent<Unit_Script>().click_number = loadedGame.UnitList_ClickNumber[i];
                                newUnit.GetComponent<Unit_Script>().team = loadedGame.UnitList_TeamNumber[i];
                            }
                        }
                        if (!matched) Debug.LogWarning("[LoadGame] No static prefab matched name='" + loadedGame.UnitList_Name[i] + "'");
                    }
                    else
                    {
                        Debug.Log("[LoadGame] Fetching dynamic unit data for set='" +
                                  loadedGame.UnitList_Set_Name[i] + "' cn='" + loadedGame.UnitList_Collectors_Number[i] + "'");
                        AddedDynamicCount++;

                        Selected_Dynamic_Set_Name = loadedGame.UnitList_Set_Name[i];
                        Selected_Dynamic_Collectors_Number = loadedGame.UnitList_Collectors_Number[i];

                        LoadGame_UnitList_LocationX.Add(loadedGame.UnitList_LocationX[i]);
                        LoadGame_UnitList_LocationY.Add(loadedGame.UnitList_LocationY[i]);
                        LoadGame_UnitList_LocationZ.Add(loadedGame.UnitList_LocationZ[i]);
                        LoadGame_UnitList_Actions.Add(loadedGame.UnitList_Actions[i]);
                        LoadGame_UnitList_ClickNumber.Add(loadedGame.UnitList_ClickNumber[i]);
                        LoadGame_UnitList_TeamNumber.Add(loadedGame.UnitList_TeamNumber[i]);

                        Set_Name_da.Add(loadedGame.UnitList_Set_Name[i]);
                        Collectors_Number_da.Add(loadedGame.UnitList_Collectors_Number[i]);
                        Unit_Name_da.Add("");

                        Dial_Length_da.Add(-1);
                        Last_Click_da.Add(-1);
                        Points_da.Add(-1);
                        Range_Value_da.Add(-1);
                        Range_Targets_da.Add(-1);

                        Experience_da.Add("");
                        Rarity_da.Add("");
                        Base_Type_da.Add("");
                        Speed_Symbol_da.Add("");
                        Attack_Symbol_da.Add("");
                        Defense_Symbol_da.Add("");
                        Damage_Symbol_da.Add("");

                        IM_Elevated_da.Add(false);
                        IM_Hindering_da.Add(false);
                        IM_Water_da.Add(false);
                        IM_Blocking_da.Add(false);
                        IM_Outdoor_Blocking_da.Add(false);
                        IM_Blocking_destroy_da.Add(false);
                        IM_Characters_da.Add(false);
                        IM_Adjacent_Characters_da.Add(false);

                        IT_Elevated_da.Add(false);
                        IT_Hindering_da.Add(false);
                        IT_Blocking_da.Add(false);
                        IT_Blocking_destroy_da.Add(false);
                        IT_Characters_da.Add(false);
                        IT_Adjacent_Characters_da.Add(false);

                        Keywords_da.Add("");
                        Team_Ability_da.Add("");
                        Trait1_da.Add("");
                        Trait2_da.Add("");
                        Trait3_da.Add("");
                        Trait4_da.Add("");
                        Trait5_da.Add("");
                        Trait6_da.Add("");
                        Set_Abbrev_da.Add("");

                        int dynIdx = Set_Name_da.Count - 1;
                        yield return StartCoroutine(GetDynamicUnitDial(dynIdx));
                        yield return StartCoroutine(GetDynamicUnitStats(dynIdx));
                    }
                }
            }

            if (loadedGame.ManualUnitList_Name != null && loadedGame.ManualUnitList_Name.Length > 0)
            {
                for (int i = 0; i < loadedGame.ManualUnitList_Name.Length; i++)
                {
                    if (loadedGame.ManualUnitList_IsDynamic[i] == 0)
                    {
                        bool matched = false;
                        foreach (GameObject allUnit in allUnits)
                        {
                            if (allUnit.name == loadedGame.ManualUnitList_Name[i])
                            {
                                matched = true;
                                GameObject newUnit = Instantiate(allUnit, new Vector3(loadedGame.ManualUnitList_LocationX[i], loadedGame.ManualUnitList_LocationY[i], loadedGame.ManualUnitList_LocationZ[i]), Quaternion.identity);
                                newUnit.name = allUnit.name;
                                newUnit.GetComponent<Unit_Script>().actionTokens = loadedGame.ManualUnitList_Actions[i];
                                newUnit.GetComponent<Unit_Script>().click_number = loadedGame.ManualUnitList_ClickNumber[i];
                                newUnit.GetComponent<Unit_Script>().team = loadedGame.ManualUnitList_TeamNumber[i];
                            }
                        }
                        if (!matched) Debug.LogWarning("[LoadGame] No static prefab matched manual name='" + loadedGame.ManualUnitList_Name[i] + "'");
                    }
                    else
                    {
                        Debug.Log("[LoadGame] Fetching dynamic manual unit data set='" +
                                  loadedGame.ManualUnitList_Set_Name[i] + "' cn='" + loadedGame.ManualUnitList_Collectors_Number[i] + "'");
                        AddedDynamicCount++;

                        Selected_Dynamic_Set_Name = loadedGame.ManualUnitList_Set_Name[i];
                        Selected_Dynamic_Collectors_Number = loadedGame.ManualUnitList_Collectors_Number[i];

                        LoadGame_UnitList_LocationX.Add(loadedGame.ManualUnitList_LocationX[i]);
                        LoadGame_UnitList_LocationY.Add(loadedGame.ManualUnitList_LocationY[i]);
                        LoadGame_UnitList_LocationZ.Add(loadedGame.ManualUnitList_LocationZ[i]);
                        LoadGame_UnitList_Actions.Add(loadedGame.ManualUnitList_Actions[i]);
                        LoadGame_UnitList_ClickNumber.Add(loadedGame.ManualUnitList_ClickNumber[i]);
                        LoadGame_UnitList_TeamNumber.Add(loadedGame.ManualUnitList_TeamNumber[i]);

                        Set_Name_da.Add(loadedGame.ManualUnitList_Set_Name[i]);
                        Collectors_Number_da.Add(loadedGame.ManualUnitList_Collectors_Number[i]);
                        Unit_Name_da.Add("");

                        Dial_Length_da.Add(-1);
                        Last_Click_da.Add(-1);
                        Points_da.Add(-1);
                        Range_Value_da.Add(-1);
                        Range_Targets_da.Add(-1);

                        Experience_da.Add("");
                        Rarity_da.Add("");
                        Base_Type_da.Add("");
                        Speed_Symbol_da.Add("");
                        Attack_Symbol_da.Add("");
                        Defense_Symbol_da.Add("");
                        Damage_Symbol_da.Add("");

                        IM_Elevated_da.Add(false);
                        IM_Hindering_da.Add(false);
                        IM_Water_da.Add(false);
                        IM_Blocking_da.Add(false);
                        IM_Outdoor_Blocking_da.Add(false);
                        IM_Blocking_destroy_da.Add(false);
                        IM_Characters_da.Add(false);
                        IM_Adjacent_Characters_da.Add(false);

                        IT_Elevated_da.Add(false);
                        IT_Hindering_da.Add(false);
                        IT_Blocking_da.Add(false);
                        IT_Blocking_destroy_da.Add(false);
                        IT_Characters_da.Add(false);
                        IT_Adjacent_Characters_da.Add(false);

                        Keywords_da.Add("");
                        Team_Ability_da.Add("");
                        Trait1_da.Add("");
                        Trait2_da.Add("");
                        Trait3_da.Add("");
                        Trait4_da.Add("");
                        Trait5_da.Add("");
                        Trait6_da.Add("");
                        Set_Abbrev_da.Add("");

                        int dynIdx = Set_Name_da.Count - 1;
                        yield return StartCoroutine(GetDynamicUnitDial(dynIdx));
                        yield return StartCoroutine(GetDynamicUnitStats(dynIdx));
                    }
                }
            }

            if (loadedGame.ObjectList_Name != null && loadedGame.ObjectList_Name.Length > 0)
            {
                Debug.Log("[LoadGame] Loading " + loadedGame.ObjectList_Name.Length + " objects.");
                for (int i = 0; i < loadedGame.ObjectList_Name.Length; i++)
                {
                    bool objectMatched = false;
                    foreach (GameObject allObject in allObjects)
                    {
                        string prefabObjName = allObject.GetComponent<Object_Script>() != null
                            ? allObject.GetComponent<Object_Script>().objectName : "(no Object_Script)";
                        if (prefabObjName == loadedGame.ObjectList_Name[i])
                        {
                            Debug.Log("[LoadGame] Instantiating object '" + loadedGame.ObjectList_Name[i] + "'");
                            Instantiate(allObject, new Vector3(loadedGame.ObjectList_LocationX[i], loadedGame.ObjectList_LocationY[i], loadedGame.ObjectList_LocationZ[i]), Quaternion.identity);
                            objectMatched = true;
                            break;
                        }
                    }
                    if (!objectMatched)
                        Debug.LogWarning("[LoadGame] No prefab matched saved object name '" + loadedGame.ObjectList_Name[i] + "'");
                }
            }

            ChatLog = loadedChat.ChatLog;
            int chatcount = 0;
            foreach (string str in ChatLog)
            {
                AllChatText.text += ChatLog[chatcount] + "\r\n";
                chatcount++;
            }
        }
        else
        {
            Debug.Log("[LoadGame] Saved_Game is empty — nothing to load.");
        }

        //Apply Selected Map During Load
        SelectedMapImage.mainTexture = Resources.Load("Images/Maps/" + Data_Storage.Instance.Map_Size + "/" + Data_Storage.Instance.Map_Name) as Texture;
        SelectedGamePoints.text = Data_Storage.Instance.Game_Points.ToString() + " Point Game";

        Debug.Log("[LoadGame] Calling PlayGame_InstantiateUnits with Set_Name_da.Count=" + Set_Name_da.Count);
        PlayGame_InstantiateUnits();

        CallGetTurnOrder();
        CallGetActivePlayer();
        CallGetUsers();
        if (TurnOrderPanel != null) TurnOrderPanel.SetActive(false);
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

                    int.TryParse(IteratedRowData.Substring(0, NextTab), out int _slot); DialInfoDynamic_Slot.Add(_slot);
                    NextTab = IteratedRowData.IndexOf("\t");
                    IteratedRowData = IteratedRowData.Substring(NextTab + 1, IteratedRowData.Length - NextTab - 1);
                    NextTab = IteratedRowData.IndexOf("\t");

                    int.TryParse(IteratedRowData.Substring(0, NextTab), out int _spd); DialInfoDynamic_SpeedValue.Add(_spd);
                    NextTab = IteratedRowData.IndexOf("\t");
                    IteratedRowData = IteratedRowData.Substring(NextTab + 1, IteratedRowData.Length - NextTab - 1);
                    NextTab = IteratedRowData.IndexOf("\t");

                    int.TryParse(IteratedRowData.Substring(0, NextTab), out int _atk); DialInfoDynamic_AttackValue.Add(_atk);
                    NextTab = IteratedRowData.IndexOf("\t");
                    IteratedRowData = IteratedRowData.Substring(NextTab + 1, IteratedRowData.Length - NextTab - 1);
                    NextTab = IteratedRowData.IndexOf("\t");

                    int.TryParse(IteratedRowData.Substring(0, NextTab), out int _def); DialInfoDynamic_DefenseValue.Add(_def);
                    NextTab = IteratedRowData.IndexOf("\t");
                    IteratedRowData = IteratedRowData.Substring(NextTab + 1, IteratedRowData.Length - NextTab - 1);
                    NextTab = IteratedRowData.IndexOf("\t");

                    int.TryParse(IteratedRowData.Substring(0, NextTab), out int _dmg); DialInfoDynamic_DamageValue.Add(_dmg);
                    NextTab = IteratedRowData.IndexOf("\t");
                    IteratedRowData = IteratedRowData.Substring(NextTab + 1, IteratedRowData.Length - NextTab - 1);
                    NextTab = IteratedRowData.IndexOf("\t");

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

        // Look up power descriptions by name
        for (int slot = 0; slot < DialInfoDynamic_Slot.Count; slot++)
        {
            DialInfoDynamic_SpeedPowerDescription.Add("");
            DialInfoDynamic_AttackPowerDescription.Add("");
            DialInfoDynamic_DefensePowerDescription.Add("");
            DialInfoDynamic_DamagePowerDescription.Add("");

            for (int powerCount = 0; powerCount < AllPowerNamesList.Count; powerCount++)
            {
                if (DialInfoDynamic_SpeedPowerStr[slot] == AllPowerNamesList[powerCount])
                    DialInfoDynamic_SpeedPowerDescription[slot] = AllPowerDescriptionsList[powerCount];
                if (DialInfoDynamic_AttackPowerStr[slot] == AllPowerNamesList[powerCount])
                    DialInfoDynamic_AttackPowerDescription[slot] = AllPowerDescriptionsList[powerCount];
                if (DialInfoDynamic_DefensePowerStr[slot] == AllPowerNamesList[powerCount])
                    DialInfoDynamic_DefensePowerDescription[slot] = AllPowerDescriptionsList[powerCount];
                if (DialInfoDynamic_DamagePowerStr[slot] == AllPowerNamesList[powerCount])
                    DialInfoDynamic_DamagePowerDescription[slot] = AllPowerDescriptionsList[powerCount];
            }
        }
        yield return StartCoroutine(GetDynamicUnitSpecialPowers());
        Debug.Log("Non-Load Dial Finished.");
        yield return null;
    }

    //Load Game Versions

    IEnumerator GetDynamicUnitStats(int i)
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

                    Set_Name_da[i] = IteratedRowData.Substring(0, NextTab);
                    NextTab = IteratedRowData.IndexOf("\t");
                    IteratedRowData = IteratedRowData.Substring(NextTab + 1, IteratedRowData.Length - NextTab - 1);
                    NextTab = IteratedRowData.IndexOf("\t");

                    Collectors_Number_da[i] = IteratedRowData.Substring(0, NextTab);
                    NextTab = IteratedRowData.IndexOf("\t");
                    IteratedRowData = IteratedRowData.Substring(NextTab + 1, IteratedRowData.Length - NextTab - 1);
                    NextTab = IteratedRowData.IndexOf("\t");

                    Unit_Name_da[i] = IteratedRowData.Substring(0, NextTab);
                    NextTab = IteratedRowData.IndexOf("\t");
                    IteratedRowData = IteratedRowData.Substring(NextTab + 1, IteratedRowData.Length - NextTab - 1);
                    NextTab = IteratedRowData.IndexOf("\t");

                    Dial_Length_da[i] = int.Parse(IteratedRowData.Substring(0, NextTab));
                    NextTab = IteratedRowData.IndexOf("\t");
                    IteratedRowData = IteratedRowData.Substring(NextTab + 1, IteratedRowData.Length - NextTab - 1);
                    NextTab = IteratedRowData.IndexOf("\t");

                    Last_Click_da[i] = int.Parse(IteratedRowData.Substring(0, NextTab));
                    NextTab = IteratedRowData.IndexOf("\t");
                    IteratedRowData = IteratedRowData.Substring(NextTab + 1, IteratedRowData.Length - NextTab - 1);
                    NextTab = IteratedRowData.IndexOf("\t");

                    Points_da[i] = int.Parse(IteratedRowData.Substring(0, NextTab));
                    NextTab = IteratedRowData.IndexOf("\t");
                    IteratedRowData = IteratedRowData.Substring(NextTab + 1, IteratedRowData.Length - NextTab - 1);
                    NextTab = IteratedRowData.IndexOf("\t");

                    Range_Value_da[i] = int.Parse(IteratedRowData.Substring(0, NextTab));
                    NextTab = IteratedRowData.IndexOf("\t");
                    IteratedRowData = IteratedRowData.Substring(NextTab + 1, IteratedRowData.Length - NextTab - 1);
                    NextTab = IteratedRowData.IndexOf("\t");

                    Range_Targets_da[i] = int.Parse(IteratedRowData.Substring(0, NextTab));
                    NextTab = IteratedRowData.IndexOf("\t");
                    IteratedRowData = IteratedRowData.Substring(NextTab + 1, IteratedRowData.Length - NextTab - 1);
                    NextTab = IteratedRowData.IndexOf("\t");

                    Experience_da[i] = IteratedRowData.Substring(0, NextTab);
                    NextTab = IteratedRowData.IndexOf("\t");
                    IteratedRowData = IteratedRowData.Substring(NextTab + 1, IteratedRowData.Length - NextTab - 1);
                    NextTab = IteratedRowData.IndexOf("\t");

                    Rarity_da[i] = IteratedRowData.Substring(0, NextTab);
                    NextTab = IteratedRowData.IndexOf("\t");
                    IteratedRowData = IteratedRowData.Substring(NextTab + 1, IteratedRowData.Length - NextTab - 1);
                    NextTab = IteratedRowData.IndexOf("\t");

                    Base_Type_da[i] = IteratedRowData.Substring(0, NextTab);
                    NextTab = IteratedRowData.IndexOf("\t");
                    IteratedRowData = IteratedRowData.Substring(NextTab + 1, IteratedRowData.Length - NextTab - 1);
                    NextTab = IteratedRowData.IndexOf("\t");

                    Speed_Symbol_da[i] = IteratedRowData.Substring(0, NextTab);
                    NextTab = IteratedRowData.IndexOf("\t");
                    IteratedRowData = IteratedRowData.Substring(NextTab + 1, IteratedRowData.Length - NextTab - 1);
                    NextTab = IteratedRowData.IndexOf("\t");

                    Attack_Symbol_da[i] = IteratedRowData.Substring(0, NextTab);
                    NextTab = IteratedRowData.IndexOf("\t");
                    IteratedRowData = IteratedRowData.Substring(NextTab + 1, IteratedRowData.Length - NextTab - 1);
                    NextTab = IteratedRowData.IndexOf("\t");

                    Defense_Symbol_da[i] = IteratedRowData.Substring(0, NextTab);
                    NextTab = IteratedRowData.IndexOf("\t");
                    IteratedRowData = IteratedRowData.Substring(NextTab + 1, IteratedRowData.Length - NextTab - 1);
                    NextTab = IteratedRowData.IndexOf("\t");

                    Damage_Symbol_da[i] = IteratedRowData.Substring(0, NextTab);
                    NextTab = IteratedRowData.IndexOf("\t");
                    IteratedRowData = IteratedRowData.Substring(NextTab + 1, IteratedRowData.Length - NextTab - 1);
                    NextTab = IteratedRowData.IndexOf("\t");

                    IM_Elevated_da[i] = System.Convert.ToBoolean(int.Parse(IteratedRowData.Substring(0, NextTab)));
                    NextTab = IteratedRowData.IndexOf("\t");
                    IteratedRowData = IteratedRowData.Substring(NextTab + 1, IteratedRowData.Length - NextTab - 1);
                    NextTab = IteratedRowData.IndexOf("\t");

                    IM_Hindering_da[i] = System.Convert.ToBoolean(int.Parse(IteratedRowData.Substring(0, NextTab)));
                    NextTab = IteratedRowData.IndexOf("\t");
                    IteratedRowData = IteratedRowData.Substring(NextTab + 1, IteratedRowData.Length - NextTab - 1);
                    NextTab = IteratedRowData.IndexOf("\t");

                    IM_Water_da[i] = System.Convert.ToBoolean(int.Parse(IteratedRowData.Substring(0, NextTab)));
                    NextTab = IteratedRowData.IndexOf("\t");
                    IteratedRowData = IteratedRowData.Substring(NextTab + 1, IteratedRowData.Length - NextTab - 1);
                    NextTab = IteratedRowData.IndexOf("\t");

                    IM_Blocking_da[i] = System.Convert.ToBoolean(int.Parse(IteratedRowData.Substring(0, NextTab)));
                    NextTab = IteratedRowData.IndexOf("\t");
                    IteratedRowData = IteratedRowData.Substring(NextTab + 1, IteratedRowData.Length - NextTab - 1);
                    NextTab = IteratedRowData.IndexOf("\t");

                    IM_Outdoor_Blocking_da[i] = System.Convert.ToBoolean(int.Parse(IteratedRowData.Substring(0, NextTab)));
                    NextTab = IteratedRowData.IndexOf("\t");
                    IteratedRowData = IteratedRowData.Substring(NextTab + 1, IteratedRowData.Length - NextTab - 1);
                    NextTab = IteratedRowData.IndexOf("\t");

                    IM_Blocking_destroy_da[i] = System.Convert.ToBoolean(int.Parse(IteratedRowData.Substring(0, NextTab)));
                    NextTab = IteratedRowData.IndexOf("\t");
                    IteratedRowData = IteratedRowData.Substring(NextTab + 1, IteratedRowData.Length - NextTab - 1);
                    NextTab = IteratedRowData.IndexOf("\t");

                    IM_Characters_da[i] = System.Convert.ToBoolean(int.Parse(IteratedRowData.Substring(0, NextTab)));
                    NextTab = IteratedRowData.IndexOf("\t");
                    IteratedRowData = IteratedRowData.Substring(NextTab + 1, IteratedRowData.Length - NextTab - 1);
                    NextTab = IteratedRowData.IndexOf("\t");

                    IM_Adjacent_Characters_da[i] = System.Convert.ToBoolean(int.Parse(IteratedRowData.Substring(0, NextTab)));
                    NextTab = IteratedRowData.IndexOf("\t");
                    IteratedRowData = IteratedRowData.Substring(NextTab + 1, IteratedRowData.Length - NextTab - 1);
                    NextTab = IteratedRowData.IndexOf("\t");

                    IT_Elevated_da[i] = System.Convert.ToBoolean(int.Parse(IteratedRowData.Substring(0, NextTab)));
                    NextTab = IteratedRowData.IndexOf("\t");
                    IteratedRowData = IteratedRowData.Substring(NextTab + 1, IteratedRowData.Length - NextTab - 1);
                    NextTab = IteratedRowData.IndexOf("\t");

                    IT_Hindering_da[i] = System.Convert.ToBoolean(int.Parse(IteratedRowData.Substring(0, NextTab)));
                    NextTab = IteratedRowData.IndexOf("\t");
                    IteratedRowData = IteratedRowData.Substring(NextTab + 1, IteratedRowData.Length - NextTab - 1);
                    NextTab = IteratedRowData.IndexOf("\t");

                    IT_Blocking_da[i] = System.Convert.ToBoolean(int.Parse(IteratedRowData.Substring(0, NextTab)));
                    NextTab = IteratedRowData.IndexOf("\t");
                    IteratedRowData = IteratedRowData.Substring(NextTab + 1, IteratedRowData.Length - NextTab - 1);
                    NextTab = IteratedRowData.IndexOf("\t");

                    IT_Blocking_destroy_da[i] = System.Convert.ToBoolean(int.Parse(IteratedRowData.Substring(0, NextTab)));
                    NextTab = IteratedRowData.IndexOf("\t");
                    IteratedRowData = IteratedRowData.Substring(NextTab + 1, IteratedRowData.Length - NextTab - 1);
                    NextTab = IteratedRowData.IndexOf("\t");

                    IT_Characters_da[i] = System.Convert.ToBoolean(int.Parse(IteratedRowData.Substring(0, NextTab)));
                    NextTab = IteratedRowData.IndexOf("\t");
                    IteratedRowData = IteratedRowData.Substring(NextTab + 1, IteratedRowData.Length - NextTab - 1);
                    NextTab = IteratedRowData.IndexOf("\t");

                    IT_Adjacent_Characters_da[i] = System.Convert.ToBoolean(int.Parse(IteratedRowData.Substring(0, NextTab)));
                    NextTab = IteratedRowData.IndexOf("\t");
                    IteratedRowData = IteratedRowData.Substring(NextTab + 1, IteratedRowData.Length - NextTab - 1);
                    NextTab = IteratedRowData.IndexOf("\t");

                    Keywords_da[i] = IteratedRowData.Substring(0, NextTab);
                    NextTab = IteratedRowData.IndexOf("\t");
                    IteratedRowData = IteratedRowData.Substring(NextTab + 1, IteratedRowData.Length - NextTab - 1);
                    NextTab = IteratedRowData.IndexOf("\t");

                    Team_Ability_da[i] = IteratedRowData.Substring(0, NextTab);
                    NextTab = IteratedRowData.IndexOf("\t");
                    IteratedRowData = IteratedRowData.Substring(NextTab + 1, IteratedRowData.Length - NextTab - 1);
                    NextTab = IteratedRowData.IndexOf("\t");

                    Trait1_da[i] = IteratedRowData.Substring(0, NextTab);
                    NextTab = IteratedRowData.IndexOf("\t");
                    IteratedRowData = IteratedRowData.Substring(NextTab + 1, IteratedRowData.Length - NextTab - 1);
                    NextTab = IteratedRowData.IndexOf("\t");

                    Trait2_da[i] = IteratedRowData.Substring(0, NextTab);
                    NextTab = IteratedRowData.IndexOf("\t");
                    IteratedRowData = IteratedRowData.Substring(NextTab + 1, IteratedRowData.Length - NextTab - 1);
                    NextTab = IteratedRowData.IndexOf("\t");

                    Trait3_da[i] = IteratedRowData.Substring(0, NextTab);
                    NextTab = IteratedRowData.IndexOf("\t");
                    IteratedRowData = IteratedRowData.Substring(NextTab + 1, IteratedRowData.Length - NextTab - 1);
                    NextTab = IteratedRowData.IndexOf("\t");

                    Trait4_da[i] = IteratedRowData.Substring(0, NextTab);
                    NextTab = IteratedRowData.IndexOf("\t");
                    IteratedRowData = IteratedRowData.Substring(NextTab + 1, IteratedRowData.Length - NextTab - 1);
                    NextTab = IteratedRowData.IndexOf("\t");

                    //Second To Last
                    Trait5_da[i] = IteratedRowData.Substring(0, NextTab);
                    NextTab = IteratedRowData.IndexOf("\t");
                    IteratedRowData = IteratedRowData.Substring(NextTab + 1, IteratedRowData.Length - NextTab - 1);
                    NextTab = IteratedRowData.IndexOf("\t");

                    Trait6_da[i] = IteratedRowData.Substring(0, NextTab);
                    NextTab = IteratedRowData.IndexOf("\t");
                    IteratedRowData = IteratedRowData.Substring(NextTab + 1, IteratedRowData.Length - NextTab - 1);

                    //Last
                    Set_Abbrev_da[i] = IteratedRowData.Substring(0, IteratedRowData.Length);

                    //DBUnits = DBUnits.Substring(DBUnits.IndexOf(";") + 1, DBUnits.Length - DBUnits.IndexOf(";") - 1);
                }

            }

        }
        Debug.Log("Stats Finished");
        yield return null;
    }

    IEnumerator GetDynamicUnitDial(int i)
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

        List<int> tempIntList = new List<int>();
        List<int> tempIntList2 = new List<int>();
        List<int> tempIntList3 = new List<int>();
        List<int> tempIntList4 = new List<int>();

        List<string> tempStrList = new List<string>();
        List<string> tempStrList2 = new List<string>();
        List<string> tempStrList3 = new List<string>();
        List<string> tempStrList4 = new List<string>();
        List<string> tempStrList5 = new List<string>();
        List<string> tempStrList6 = new List<string>();
        List<string> tempStrList7 = new List<string>();
        List<string> tempStrList8 = new List<string>();

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

                    int.TryParse(IteratedRowData.Substring(0, NextTab), out int _slot); DialInfoDynamic_Slot.Add(_slot);
                    NextTab = IteratedRowData.IndexOf("\t");
                    IteratedRowData = IteratedRowData.Substring(NextTab + 1, IteratedRowData.Length - NextTab - 1);
                    NextTab = IteratedRowData.IndexOf("\t");

                    int.TryParse(IteratedRowData.Substring(0, NextTab), out int _spd); DialInfoDynamic_SpeedValue.Add(_spd);
                    NextTab = IteratedRowData.IndexOf("\t");
                    IteratedRowData = IteratedRowData.Substring(NextTab + 1, IteratedRowData.Length - NextTab - 1);
                    NextTab = IteratedRowData.IndexOf("\t");

                    int.TryParse(IteratedRowData.Substring(0, NextTab), out int _atk); DialInfoDynamic_AttackValue.Add(_atk);
                    NextTab = IteratedRowData.IndexOf("\t");
                    IteratedRowData = IteratedRowData.Substring(NextTab + 1, IteratedRowData.Length - NextTab - 1);
                    NextTab = IteratedRowData.IndexOf("\t");

                    int.TryParse(IteratedRowData.Substring(0, NextTab), out int _def); DialInfoDynamic_DefenseValue.Add(_def);
                    NextTab = IteratedRowData.IndexOf("\t");
                    IteratedRowData = IteratedRowData.Substring(NextTab + 1, IteratedRowData.Length - NextTab - 1);
                    NextTab = IteratedRowData.IndexOf("\t");

                    int.TryParse(IteratedRowData.Substring(0, NextTab), out int _dmg); DialInfoDynamic_DamageValue.Add(_dmg);
                    NextTab = IteratedRowData.IndexOf("\t");
                    IteratedRowData = IteratedRowData.Substring(NextTab + 1, IteratedRowData.Length - NextTab - 1);
                    NextTab = IteratedRowData.IndexOf("\t");

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

                SetName_List.Add(Set_Name_d);
                CollectorsNumber_List.Add(Collectors_Number_d);
                Slot_List.Add(DialInfoDynamic_Slot);

                SpeedVal_List.Add(tempIntList);
                AttackVal_List.Add(tempIntList2);
                DefenseVal_List.Add(tempIntList3);
                DamageVal_List.Add(tempIntList4);

                for (int addToNestedListCount=0; addToNestedListCount < DialInfoDynamic_SpeedValue.Count; addToNestedListCount++)
                {
                    SpeedVal_List[SpeedVal_List.Count - 1].Add(DialInfoDynamic_SpeedValue[addToNestedListCount]);
                    AttackVal_List[AttackVal_List.Count - 1].Add(DialInfoDynamic_AttackValue[addToNestedListCount]);
                    DefenseVal_List[DefenseVal_List.Count - 1].Add(DialInfoDynamic_DefenseValue[addToNestedListCount]);
                    DamageVal_List[DamageVal_List.Count - 1].Add(DialInfoDynamic_DamageValue[addToNestedListCount]);
                }


            }

        }

        // Look up power descriptions by name
        for (int slot = 0; slot < DialInfoDynamic_Slot.Count; slot++)
        {
            DialInfoDynamic_SpeedPowerDescription.Add("");
            DialInfoDynamic_AttackPowerDescription.Add("");
            DialInfoDynamic_DefensePowerDescription.Add("");
            DialInfoDynamic_DamagePowerDescription.Add("");

            for (int powerCount = 0; powerCount < AllPowerNamesList.Count; powerCount++)
            {
                if (DialInfoDynamic_SpeedPowerStr[slot] == AllPowerNamesList[powerCount])
                    DialInfoDynamic_SpeedPowerDescription[slot] = AllPowerDescriptionsList[powerCount];
                if (DialInfoDynamic_AttackPowerStr[slot] == AllPowerNamesList[powerCount])
                    DialInfoDynamic_AttackPowerDescription[slot] = AllPowerDescriptionsList[powerCount];
                if (DialInfoDynamic_DefensePowerStr[slot] == AllPowerNamesList[powerCount])
                    DialInfoDynamic_DefensePowerDescription[slot] = AllPowerDescriptionsList[powerCount];
                if (DialInfoDynamic_DamagePowerStr[slot] == AllPowerNamesList[powerCount])
                    DialInfoDynamic_DamagePowerDescription[slot] = AllPowerDescriptionsList[powerCount];
            }
        }



        yield return StartCoroutine(GetDynamicUnitSpecialPowers());

        SpeedPow_List.Add(tempStrList);
        AttackPow_List.Add(tempStrList2);
        DefensePow_List.Add(tempStrList3);
        DamagePow_List.Add(tempStrList4);

        SpeedPowDesc_List.Add(tempStrList5);
        AttackPowDesc_List.Add(tempStrList6);
        DefensePowDesc_List.Add(tempStrList7);
        DamagePowDesc_List.Add(tempStrList8);

        for (int addToNestedListCount = 0; addToNestedListCount < DialInfoDynamic_SpeedPowerStr.Count; addToNestedListCount++)
        {
            SpeedPow_List[SpeedVal_List.Count - 1].Add(DialInfoDynamic_SpeedPowerStr[addToNestedListCount]);
            AttackPow_List[AttackVal_List.Count - 1].Add(DialInfoDynamic_AttackPowerStr[addToNestedListCount]);
            DefensePow_List[DefenseVal_List.Count - 1].Add(DialInfoDynamic_DefensePowerStr[addToNestedListCount]);
            DamagePow_List[DamageVal_List.Count - 1].Add(DialInfoDynamic_DamagePowerStr[addToNestedListCount]);

            SpeedPowDesc_List[SpeedVal_List.Count - 1].Add(DialInfoDynamic_SpeedPowerDescription[addToNestedListCount]);
            AttackPowDesc_List[AttackVal_List.Count - 1].Add(DialInfoDynamic_AttackPowerDescription[addToNestedListCount]);
            DefensePowDesc_List[DefenseVal_List.Count - 1].Add(DialInfoDynamic_DefensePowerDescription[addToNestedListCount]);
            DamagePowDesc_List[DamageVal_List.Count - 1].Add(DialInfoDynamic_DamagePowerDescription[addToNestedListCount]);
        }

        Debug.Log("Dial Finished");
        yield return null;
    }

    //End of Load Game Versions

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

                // Log how many slots contain "Special Power" in each stat before patching
                int _spSpd = 0, _spAtk = 0, _spDef = 0, _spDmg = 0;
                for (int _s = 0; _s < DialInfoDynamic_SpeedPowerStr.Count; _s++)
                {
                    if (DialInfoDynamic_SpeedPowerStr[_s] == "Special Power") _spSpd++;
                    if (DialInfoDynamic_AttackPowerStr[_s] == "Special Power") _spAtk++;
                    if (DialInfoDynamic_DefensePowerStr[_s] == "Special Power") _spDef++;
                    if (DialInfoDynamic_DamagePowerStr[_s] == "Special Power") _spDmg++;
                }
                Debug.Log("[GetDynamicUnitSpecialPowers] Slots with 'Special Power' BEFORE patch: spd=" + _spSpd + " atk=" + _spAtk + " def=" + _spDef + " dmg=" + _spDmg);
                if (DialInfoDynamic_SpeedPowerStr.Count > 0)
                    Debug.Log("[GetDynamicUnitSpecialPowers] SpeedPowerStr[0]=" + DialInfoDynamic_SpeedPowerStr[0] + " AttackPowerStr[0]=" + DialInfoDynamic_AttackPowerStr[0] + " DefensePowerStr[0]=" + DialInfoDynamic_DefensePowerStr[0] + " DamagePowerStr[0]=" + DialInfoDynamic_DamagePowerStr[0]);

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
                Debug.Log("[GetAllPowersList] HTTP Error: " + www2.error + " | Response: " + www2.downloadHandler.text);
            }
            else
            {
                string Powers = www2.downloadHandler.text;

                foreach (string row in Powers.Split(';'))
                {
                    if (string.IsNullOrWhiteSpace(row)) continue;
                    string[] cols = row.Trim().Split('\t');
                    if (cols.Length < 3) continue;

                    if (!int.TryParse(cols[0].Trim(), out int powerID)) continue;

                    AllPowersList.Add(powerID);
                    AllPowerNamesList.Add(cols[1].Trim());
                    AllPowerDescriptionsList.Add(cols[2].Trim());
                }

            }
        }
        Debug.Log("Powers Finished");
        yield return null;
    }



    public void CallLogActivity(string Activity_Text)
    {
        StartCoroutine(LogActivity(Activity_Text));
    }

    IEnumerator LogActivity(string Activity_Text)
    {
        WWWForm form = new WWWForm();
        form.AddField("User_ID", PlayerPrefs.GetInt("User_ID"));
        form.AddField("Game_ID", Data_Storage.Instance.Game_ID);
        form.AddField("Activity", Activity_Text);

        using (UnityWebRequest www = UnityWebRequest.Post("https://www.stark44.com/Heroclix/LogActivity.php", form))
        {
            www.certificateHandler = new BypassCertificate();
            yield return www.SendWebRequest();

            if (www.result != UnityWebRequest.Result.Success)
            {
                Debug.Log(www.error);
            }
            else
            {
                string ActivityCompleted = www.downloadHandler.text;

                if(ActivityCompleted == "0")
                {
                    Debug.Log("Activity Logged.");
                }
                else
                {
                    Debug.Log(ActivityCompleted);
                }
            }
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

    public void CallLoadDynamicUnitImageSynch(GameObject sentUnit)
    {
        //IEnumerator LDUIS =  LoadDynamicUnitImageSynch(sentUnit);

        //while (LDUIS.MoveNext())
        //    if (LDUIS.Current != null)
        //        Debug.Log(LDUIS.Current as string);

        StartCoroutine(LoadDynamicUnitImageSynch(sentUnit));

    }

    IEnumerator LoadDynamicUnitImageSynch(GameObject sentUnit)
    {
        // Build URL from sentUnit directly to avoid race conditions and null-component crashes
        // (Manual_Non_Single_Base_Unit_Script units don't have Unit_Script)
        string setAbbrev = "";
        string collectorsNum = "";
        Unit_Script us = sentUnit.GetComponent<Unit_Script>();
        Manual_Non_Single_Base_Unit_Script ms = sentUnit.GetComponent<Manual_Non_Single_Base_Unit_Script>();
        if (us != null)
        {
            setAbbrev = us.set_abbreviation;
            collectorsNum = us.collectors_number;
        }
        else if (ms != null)
        {
            setAbbrev = ms.set_abbreviation;
            collectorsNum = ms.collectors_number;
        }

        string url = "https://www.stark44.com/Heroclix/Unit_Images/" + setAbbrev + collectorsNum + ".png";

        Debug.Log(url);

        // Hide the sprite renderer while the image loads to prevent white flicker
        SpriteRenderer sr = sentUnit.GetComponentInChildren<SpriteRenderer>();
        if (sr != null)
        {
            Color c = sr.color;
            c.a = 0f;
            sr.color = c;
        }

        using (UnityWebRequest www2 = UnityWebRequestTexture.GetTexture(url))
        {
            www2.certificateHandler = new BypassCertificate();
            yield return www2.SendWebRequest();

            //while (!www2.isDone)
            //    yield return null;

            if (www2.result != UnityWebRequest.Result.Success)
            {
                Debug.Log(www2.error);
                // Keep alpha=0 on failure — no image is better than a white pane.
                // The unit is still functional; it just won't show a portrait.
            }
            else
            {
                DownloadHandlerTexture downloadHandlerTexture = www2.downloadHandler as DownloadHandlerTexture;
                Sprite sprite = Sprite.Create(downloadHandlerTexture.texture, new Rect(0, 0, downloadHandlerTexture.texture.width, downloadHandlerTexture.texture.height), new Vector2(.5f, .5f), 100);
                if (sr != null)
                {
                    sr.sprite = sprite;
                    Color c = sr.color;
                    c.a = 1f;
                    sr.color = c;
                }
            }
        }

        //yield return null;
    }



    public void CallGetUsers()
    {
        StartCoroutine(GetUsers());
    }

    IEnumerator GetUsers()
    {
        DropdownUsersList.Clear();
        SetupTurnOrderDropdown.ClearOptions();
        SelectCurrentPlayerDropdown.ClearOptions();

        DropdownUsersList.Add("");

        WWWForm form = new WWWForm();
        form.AddField("Game_ID", Data_Storage.Instance.Game_ID);

        using (UnityWebRequest www = UnityWebRequest.Post("https://www.stark44.com/Heroclix/getPlayersInGame.php",form))
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

                SetupTurnOrderDropdown.AddOptions(DropdownUsersList);
                SelectCurrentPlayerDropdown.AddOptions(DropdownUsersList);

            }
        }

    }

    public void CallGetTurnOrder()
    {
        StartCoroutine(GetTurnOrder());
    }

    IEnumerator GetTurnOrder()
    {
        WWWForm form = new WWWForm();
        form.AddField("Game_ID", Data_Storage.Instance.Game_ID);

        using (UnityWebRequest www = UnityWebRequest.Post("https://www.stark44.com/Heroclix/getTurnOrder.php", form))
        {
            www.certificateHandler = new BypassCertificate();
            yield return www.SendWebRequest();

            if (www.result != UnityWebRequest.Result.Success)
            {
                Debug.LogError("[GetTurnOrder] Request failed: " + www.error);
            }
            else
            {
                string DBUsers = www.downloadHandler.text;
                Debug.Log("[GetTurnOrder] Response: '" + DBUsers + "' | Game_ID: " + Data_Storage.Instance.Game_ID);

                if (TurnOrderText == null)
                {
                    Debug.LogError("[GetTurnOrder] TurnOrderText is not assigned in the Inspector.");
                }
                else if (DBUsers.Length > 0)
                {
                    TurnOrderText.text = DBUsers;
                    // Activate the full parent chain so the text is visible in the hierarchy
                    Transform t = TurnOrderText.transform;
                    while (t != null)
                    {
                        t.gameObject.SetActive(true);
                        t = t.parent;
                    }
                    // Hide the setup panel after parent walk (parent walk may have re-enabled it)
                    if (TurnOrderPanel != null) TurnOrderPanel.SetActive(false);
                    // Hide turn order setup UI since order is already set
                    if (Save_Turn_Order_Setup_Button != null) Save_Turn_Order_Setup_Button.gameObject.SetActive(false);
                    if (Clear_Turn_Order_Setup_Button != null) Clear_Turn_Order_Setup_Button.gameObject.SetActive(false);
                    if (Add_Player_Turn_Order_Setup_Button != null) Add_Player_Turn_Order_Setup_Button.gameObject.SetActive(false);
                    SetupTurnOrderDropdown.gameObject.SetActive(false);
                    Debug.Log("[GetTurnOrder] Turn order set to: '" + DBUsers + "'");
                }
                else
                {
                    Debug.LogWarning("[GetTurnOrder] Server returned an empty response. Check Game_ID: " + Data_Storage.Instance.Game_ID);
                }
            }
        }

        yield return null;
    }

    public void CallUpdateActivePlayer()
    {
        StartCoroutine(UpdateActivePlayer());
    }

    IEnumerator UpdateActivePlayer()
    {
        WWWForm form = new WWWForm();
        form.AddField("Game_ID", Data_Storage.Instance.Game_ID);
        form.AddField("Active_Player", ActivePlayerText.text);

        using (UnityWebRequest www = UnityWebRequest.Post("https://www.stark44.com/Heroclix/UpdateActivePlayer.php", form))
        {
            www.certificateHandler = new BypassCertificate();
            yield return www.SendWebRequest();

            if (www.result != UnityWebRequest.Result.Success)
            {
                Debug.LogError("[UpdateActivePlayer] Request failed: " + www.error);
            }
            else
            {
                string DBUsers = www.downloadHandler.text;

                if (DBUsers.Trim() == "0")
                {
                    Debug.Log("[UpdateActivePlayer] Turn passed successfully.");
                }
                else
                {
                    Debug.LogWarning("[UpdateActivePlayer] Unexpected response: '" + DBUsers + "'");
                }
            }
        }
    }

    public void CallUpdateTurnOrder()
    {
        StartCoroutine(UpdateTurnOrderSequence());
        CallLogActivity("Turn Order Changed To " + TurnOrderText.text);
    }

    IEnumerator UpdateTurnOrderSequence()
    {
        yield return StartCoroutine(UpdateTurnOrder());
        yield return StartCoroutine(GetTurnOrder());
        if (Save_Turn_Order_Setup_Button != null) Save_Turn_Order_Setup_Button.gameObject.SetActive(false);
    }

    IEnumerator UpdateTurnOrder()
    {
        WWWForm form = new WWWForm();
        form.AddField("Game_ID", Data_Storage.Instance.Game_ID);
        form.AddField("Turn_Order", TurnOrderText.text);

        using (UnityWebRequest www = UnityWebRequest.Post("https://www.stark44.com/Heroclix/UpdateTurnOrder.php", form))
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
                    Debug.Log("Turn Order Updated.");
                }
                else
                {
                    Debug.Log(DBUsers);
                }
            }
        }

    }

    public void CallGetActivePlayer()
    {
        StartCoroutine(GetActivePlayer());
    }

    IEnumerator GetActivePlayer()
    {
        WWWForm form = new WWWForm();
        form.AddField("Game_ID", Data_Storage.Instance.Game_ID);

        using (UnityWebRequest www = UnityWebRequest.Post("https://www.stark44.com/Heroclix/getActivePlayer.php", form))
        {
            www.certificateHandler = new BypassCertificate();
            yield return www.SendWebRequest();

            if (www.result != UnityWebRequest.Result.Success)
            {
                Debug.LogError("[GetActivePlayer] Request failed: " + www.error);
            }
            else
            {
                string DBUsers = www.downloadHandler.text.Trim();
                Debug.Log("[GetActivePlayer] Response: '" + DBUsers + "'");

                if (DBUsers.Length > 0)
                {
                    activePlayerIsNull = false;
                    ActivePlayerText.text = DBUsers;
                    ActivePlayerText.gameObject.SetActive(true);
                }
                else
                {
                    activePlayerIsNull = true;
                    ActivePlayerText.text = "";
                    ActivePlayerText.gameObject.SetActive(false);
                    Debug.Log("[GetActivePlayer] No active player set (newly created game).");
                }
            }
        }
    }

    public void AddPlayerToTurnOrder()
    {
        string selectedPlayer = SetupTurnOrderDropdown.options[SetupTurnOrderDropdown.value].text;
        if (string.IsNullOrWhiteSpace(selectedPlayer)) return;

        TurnOrderText.text += selectedPlayer;
        TurnOrderText.gameObject.SetActive(true);
        DropdownUsersList.Remove(selectedPlayer);

        SetupTurnOrderDropdown.ClearOptions();

        if (DropdownUsersList.Count > 1)
        {
            TurnOrderText.text += ", ";
            SetupTurnOrderDropdown.AddOptions(DropdownUsersList);
        }
    }

    public void PassTurn()
    {
        string storedUser = PlayerPrefs.GetString("Active_Username");
        if (string.IsNullOrEmpty(storedUser)) storedUser = PlayerPrefs.GetString("Stored_Username");
        Debug.Log("[PassTurn] ActivePlayerText='" + ActivePlayerText.text + "' | activePlayerIsNull=" + activePlayerIsNull + " | Active_Username='" + storedUser + "'");
        if (activePlayerIsNull || ActivePlayerText.text == storedUser)
        {
            StartCoroutine(PassTurnSequence());
        }
        else
        {
            Debug.Log("[PassTurn] Blocked — not your turn.");
        }
    }

    IEnumerator PassTurnSequence()
    {
        ActivePlayerText.text = SelectCurrentPlayerDropdown.options[SelectCurrentPlayerDropdown.value].text;
        yield return StartCoroutine(UpdateActivePlayer());
        CallLogActivity("Turn Passed To " + ActivePlayerText.text);

        // Build save data
        List<GameObject> saveUnits = new List<GameObject>(GameObject.FindGameObjectsWithTag("Unit"));
        List<GameObject> saveObjects = new List<GameObject>(GameObject.FindGameObjectsWithTag("Object"));
        List<GameObject> saveManual = new List<GameObject>(GameObject.FindGameObjectsWithTag("ManualUnit"));

        Debug.Log("[PassTurnSequence] Saving: " + saveUnits.Count + " units, " +
                  saveManual.Count + " manual units, " + saveObjects.Count + " objects.");
        for (int dbg = 0; dbg < saveUnits.Count; dbg++)
        {
            var u = saveUnits[dbg].GetComponent<Unit_Script>();
            Debug.Log("[PassTurnSequence] Unit[" + dbg + "] name='" + saveUnits[dbg].name +
                      "' pos=" + saveUnits[dbg].transform.position +
                      " isDynamic=" + (u != null ? u.isDynamic.ToString() : "NO Unit_Script!") +
                      " set='" + (u != null ? u.set : "") +
                      "' cn='" + (u != null ? u.collectors_number : "") + "'");
        }

        Game_Save_Script currentGame = CreateSaveGameObject(saveObjects, saveUnits, saveManual);
        string gameJSON = JsonUtility.ToJson(currentGame);
        Chat_Save_Script currentChat = CreateSaveChatObject(ChatLog);
        string chatJSON = JsonUtility.ToJson(currentChat);

        Debug.Log("[PassTurnSequence] JSON length=" + gameJSON.Length +
                  " | preview: " + (gameJSON.Length > 200 ? gameJSON.Substring(0, 200) + "..." : gameJSON));

        if (saveUnits.Count == 0 && saveManual.Count == 0)
        {
            Debug.LogWarning("[PassTurnSequence] Refusing to save — no units in scene. Exiting without overwriting save data.");
            Exit_Game();
            yield break;
        }

        yield return StartCoroutine(SaveGameToServer(gameJSON, chatJSON));

        Exit_Game();
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

        Unit_Script _udus = ListOfUnits[listIndex].GetComponent<Unit_Script>();
        Manual_Non_Single_Base_Unit_Script _udms = ListOfUnits[listIndex].GetComponent<Manual_Non_Single_Base_Unit_Script>();

        Debug.Log("[Update_Previewed_Dial] Unit='" + ListOfUnits[listIndex].name + "' _udus type=" + (_udus != null ? _udus.GetType().Name : "null") + " _udms=" + (_udms != null ? "set" : "null"));
        {
            var _diagSpd = _udus != null ? _udus.speed_powers : (_udms != null ? _udms.speed_powers : null);
            var _diagDmg = _udus != null ? _udus.damage_powers : (_udms != null ? _udms.damage_powers : null);
            if (_diagSpd != null)
                Debug.Log("[Update_Previewed_Dial] speed_powers count=" + _diagSpd.Count + ((_diagSpd.Count > 0) ? " [0]=" + _diagSpd[0] : "") + " | damage_powers count=" + (_diagDmg != null ? _diagDmg.Count.ToString() : "null") + ((_diagDmg != null && _diagDmg.Count > 0) ? " [0]=" + _diagDmg[0] : ""));
        }

        List<int>    _spd_vals  = _udus != null ? _udus.speed_values  : _udms.speed_values;
        List<string> _spd_pwrs  = _udus != null ? _udus.speed_powers  : _udms.speed_powers;
        List<string> _spd_descs = _udus != null ? _udus.speed_powers_description : _udms.speed_powers_description;
        List<int>    _atk_vals  = _udus != null ? _udus.attack_values  : _udms.attack_values;
        List<string> _atk_pwrs  = _udus != null ? _udus.attack_powers  : _udms.attack_powers;
        List<string> _atk_descs = _udus != null ? _udus.attack_powers_description : _udms.attack_powers_description;
        List<int>    _def_vals  = _udus != null ? _udus.defense_values  : _udms.defense_values;
        List<string> _def_pwrs  = _udus != null ? _udus.defense_powers  : _udms.defense_powers;
        List<string> _def_descs = _udus != null ? _udus.defense_powers_description : _udms.defense_powers_description;
        List<int>    _dmg_vals  = _udus != null ? _udus.damage_values  : _udms.damage_values;
        List<string> _dmg_pwrs  = _udus != null ? _udus.damage_powers  : _udms.damage_powers;
        List<string> _dmg_descs = _udus != null ? _udus.damage_powers_description : _udms.damage_powers_description;
        int DialLength = _udus != null ? _udus.dial_length : _udms.dial_length;
        valuesList.Clear();
        powersList.Clear();
        textColorList.Clear();

        speedPowersList.Clear();
        attackPowersList.Clear();
        defensePowersList.Clear();
        damagePowersList.Clear();

        speedPowersDescriptionsList.Clear();
        attackPowersDescriptionsList.Clear();
        defensePowersDescriptionsList.Clear();
        damagePowersDescriptionsList.Clear();

        if (DialLength > startingIndex && startingIndex >= 0)
        {
            currentDialIndex = startingIndex;
            int showSlots = System.Math.Min(12, DialLength - currentDialIndex);

            for (int i = currentDialIndex; i < currentDialIndex + showSlots; i++)
            {

                if (_spd_vals[i] == -1)
                {
                    valuesList.Add("KO");
                }
                else
                {
                    valuesList.Add(_spd_vals[i].ToString());
                }

                switch (_spd_pwrs[i])
                {
                    case "Flurry":
                        speedPowersList.Add(_spd_pwrs[i]);
                        speedPowersDescriptionsList.Add(_spd_descs[i]);
                        powersList.Add(new Color32(204, 0, 0, 255));
                        textColorList.Add(Color.white);
                        break;

                    case "Leap/Climb":
                        speedPowersList.Add(_spd_pwrs[i]);
                        speedPowersDescriptionsList.Add(_spd_descs[i]);
                        powersList.Add(new Color32(255, 153, 51, 255));
                        textColorList.Add(Color.black);
                        break;

                    case "Phasing/Teleport":
                        speedPowersList.Add(_spd_pwrs[i]);
                        speedPowersDescriptionsList.Add(_spd_descs[i]);
                        powersList.Add(new Color32(255, 255, 0, 255));
                        textColorList.Add(Color.black);
                        break;

                    case "Earthbound/Neutralized":
                        speedPowersList.Add(_spd_pwrs[i]);
                        speedPowersDescriptionsList.Add(_spd_descs[i]);
                        powersList.Add(new Color32(0, 255, 51, 255));
                        textColorList.Add(Color.black);
                        break;

                    case "Charge":
                        speedPowersList.Add(_spd_pwrs[i]);
                        speedPowersDescriptionsList.Add(_spd_descs[i]);
                        powersList.Add(new Color32(0, 153, 0, 255));
                        textColorList.Add(Color.black);
                        break;

                    case "Mind Control":
                        speedPowersList.Add(_spd_pwrs[i]);
                        speedPowersDescriptionsList.Add(_spd_descs[i]);
                        powersList.Add(new Color32(179, 229, 252, 255));
                        textColorList.Add(Color.black);
                        break;

                    case "Plasticity":
                        speedPowersList.Add(_spd_pwrs[i]);
                        speedPowersDescriptionsList.Add(_spd_descs[i]);
                        powersList.Add(new Color32(0, 51, 204, 255));
                        textColorList.Add(Color.white);
                        break;

                    case "Force Blast":
                        speedPowersList.Add(_spd_pwrs[i]);
                        speedPowersDescriptionsList.Add(_spd_descs[i]);
                        powersList.Add(new Color32(102, 0, 204, 255));
                        textColorList.Add(Color.white);
                        break;

                    case "Sidestep":
                        speedPowersList.Add(_spd_pwrs[i]);
                        speedPowersDescriptionsList.Add(_spd_descs[i]);
                        powersList.Add(new Color32(255, 153, 255, 255));
                        textColorList.Add(Color.black);
                        break;

                    case "Hypersonic Speed":
                        speedPowersList.Add(_spd_pwrs[i]);
                        speedPowersDescriptionsList.Add(_spd_descs[i]);
                        powersList.Add(new Color32(191, 158, 27, 255));
                        textColorList.Add(Color.black);
                        break;

                    case "Stealth":
                        speedPowersList.Add(_spd_pwrs[i]);
                        speedPowersDescriptionsList.Add(_spd_descs[i]);
                        powersList.Add(new Color32(0, 0, 0, 255));
                        textColorList.Add(Color.white);
                        break;

                    case "Running Shot":
                        speedPowersList.Add(_spd_pwrs[i]);
                        speedPowersDescriptionsList.Add(_spd_descs[i]);
                        powersList.Add(new Color32(176, 190, 197, 255));
                        textColorList.Add(Color.white);
                        break;

                    case "None":

                        speedPowersList.Add(_spd_pwrs[i]);
                        speedPowersDescriptionsList.Add(_spd_descs[i]);
                        powersList.Add(new Color32(255, 255, 255, 255));
                        textColorList.Add(Color.black);
                        break;


                    case "":

                        speedPowersList.Add(_spd_pwrs[i]);
                        speedPowersDescriptionsList.Add(_spd_descs[i]);
                        powersList.Add(new Color32(255, 255, 255, 255));
                        textColorList.Add(Color.black);
                        break;

                    default:

                        speedPowersList.Add(_spd_pwrs[i]);
                        speedPowersDescriptionsList.Add(_spd_descs[i]);
                        powersList.Add(new Color32(255, 0, 235, 255));
                        textColorList.Add(Color.black);
                        break;

                }
            }

            while (valuesList.Count < 12) { valuesList.Add("--"); }
            while (powersList.Count < 12) { powersList.Add(new Color32(220, 220, 220, 255)); }
            while (textColorList.Count < 12) { textColorList.Add(new Color32(150, 150, 150, 255)); }
            while (speedPowersList.Count < 12) { speedPowersList.Add(""); }
            while (speedPowersDescriptionsList.Count < 12) { speedPowersDescriptionsList.Add(""); }

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
                if (_atk_vals[i] == -1)
                {
                    valuesList.Add("KO");
                }
                else
                {
                    valuesList.Add(_atk_vals[i].ToString());
                }

                switch (_atk_pwrs[i])
                {
                    case "Blades/Claws/Fangs":
                        attackPowersList.Add(_atk_pwrs[i]);
                        attackPowersDescriptionsList.Add(_atk_descs[i]);
                        powersList.Add(new Color32(204, 0, 0, 255));
                        textColorList.Add(Color.white);
                        break;

                    case "Energy Explosion":
                        attackPowersList.Add(_atk_pwrs[i]);
                        attackPowersDescriptionsList.Add(_atk_descs[i]);
                        powersList.Add(new Color32(255, 153, 51, 255));
                        textColorList.Add(Color.black);
                        break;

                    case "Pulse Wave":
                        attackPowersList.Add(_atk_pwrs[i]);
                        attackPowersDescriptionsList.Add(_atk_descs[i]);
                        powersList.Add(new Color32(255, 255, 0, 255));
                        textColorList.Add(Color.black);
                        break;

                    case "Quake":
                        attackPowersList.Add(_atk_pwrs[i]);
                        attackPowersDescriptionsList.Add(_atk_descs[i]);
                        powersList.Add(new Color32(0, 255, 51, 255));
                        textColorList.Add(Color.black);
                        break;

                    case "Super Strength":
                        attackPowersList.Add(_atk_pwrs[i]);
                        attackPowersDescriptionsList.Add(_atk_descs[i]);
                        powersList.Add(new Color32(0, 153, 0, 255));
                        textColorList.Add(Color.black);
                        break;

                    case "Incapacitate":
                        attackPowersList.Add(_atk_pwrs[i]);
                        attackPowersDescriptionsList.Add(_atk_descs[i]);
                        powersList.Add(new Color32(179, 229, 252, 255));
                        textColorList.Add(Color.black);
                        break;

                    case "Penetrating/Psychic Blast":
                        attackPowersList.Add(_atk_pwrs[i]);
                        attackPowersDescriptionsList.Add(_atk_descs[i]);
                        powersList.Add(new Color32(0, 51, 204, 255));
                        textColorList.Add(Color.white);
                        break;

                    case "Smoke Cloud":
                        attackPowersList.Add(_atk_pwrs[i]);
                        attackPowersDescriptionsList.Add(_atk_descs[i]);
                        powersList.Add(new Color32(102, 0, 204, 255));
                        textColorList.Add(Color.white);
                        break;

                    case "Precision Strike":
                        attackPowersList.Add(_atk_pwrs[i]);
                        attackPowersDescriptionsList.Add(_atk_descs[i]);
                        powersList.Add(new Color32(255, 153, 255, 255));
                        textColorList.Add(Color.black);
                        break;

                    case "Poison":
                        attackPowersList.Add(_atk_pwrs[i]);
                        attackPowersDescriptionsList.Add(_atk_descs[i]);
                        powersList.Add(new Color32(191, 158, 27, 255));
                        textColorList.Add(Color.black);
                        break;

                    case "Steal Energy":
                        attackPowersList.Add(_atk_pwrs[i]);
                        attackPowersDescriptionsList.Add(_atk_descs[i]);
                        powersList.Add(new Color32(0, 0, 0, 255));
                        textColorList.Add(Color.white);
                        break;

                    case "Telekinesis":
                        attackPowersList.Add(_atk_pwrs[i]);
                        attackPowersDescriptionsList.Add(_atk_descs[i]);
                        powersList.Add(new Color32(176, 190, 197, 255));
                        textColorList.Add(Color.white);
                        break;

                    case "None":

                        attackPowersList.Add(_atk_pwrs[i]);
                        attackPowersDescriptionsList.Add(_atk_descs[i]);
                        powersList.Add(new Color32(255, 255, 255, 255));
                        textColorList.Add(Color.black);
                        break;

                    case "":

                        attackPowersList.Add(_atk_pwrs[i]);
                        attackPowersDescriptionsList.Add(_atk_descs[i]);
                        powersList.Add(new Color32(255, 255, 255, 255));
                        textColorList.Add(Color.black);
                        break;

                    default:

                        attackPowersList.Add(_atk_pwrs[i]);
                        attackPowersDescriptionsList.Add(_atk_descs[i]);
                        powersList.Add(new Color32(255, 0, 235, 255));
                        textColorList.Add(Color.black);
                        break;
                }
            }

            while (valuesList.Count < 12) { valuesList.Add("--"); }
            while (powersList.Count < 12) { powersList.Add(new Color32(220, 220, 220, 255)); }
            while (textColorList.Count < 12) { textColorList.Add(new Color32(150, 150, 150, 255)); }
            while (attackPowersList.Count < 12) { attackPowersList.Add(""); }
            while (attackPowersDescriptionsList.Count < 12) { attackPowersDescriptionsList.Add(""); }

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
                if (_def_vals[i] == -1)
                {
                    valuesList.Add("KO");
                }
                else
                {
                    valuesList.Add(_def_vals[i].ToString());
                }

                switch (_def_pwrs[i])
                {
                    case "Super Senses":
                        defensePowersList.Add(_def_pwrs[i]);
                        defensePowersDescriptionsList.Add(_def_descs[i]);
                        powersList.Add(new Color32(204, 0, 0, 255));
                        textColorList.Add(Color.white);
                        break;

                    case "Toughness":
                        defensePowersList.Add(_def_pwrs[i]);
                        defensePowersDescriptionsList.Add(_def_descs[i]);
                        powersList.Add(new Color32(255, 153, 51, 255));
                        textColorList.Add(Color.black);
                        break;

                    case "Defend":
                        defensePowersList.Add(_def_pwrs[i]);
                        defensePowersDescriptionsList.Add(_def_descs[i]);
                        powersList.Add(new Color32(255, 255, 0, 255));
                        textColorList.Add(Color.black);
                        break;

                    case "Combat Reflexes":
                        defensePowersList.Add(_def_pwrs[i]);
                        defensePowersDescriptionsList.Add(_def_descs[i]);
                        powersList.Add(new Color32(0, 255, 51, 255));
                        textColorList.Add(Color.black);
                        break;

                    case "Energy Shield/Deflection":
                        defensePowersList.Add(_def_pwrs[i]);
                        defensePowersDescriptionsList.Add(_def_descs[i]);
                        powersList.Add(new Color32(0, 153, 0, 255));
                        textColorList.Add(Color.black);
                        break;

                    case "Barrier":
                        defensePowersList.Add(_def_pwrs[i]);
                        defensePowersDescriptionsList.Add(_def_descs[i]);
                        powersList.Add(new Color32(179, 229, 252, 255));
                        textColorList.Add(Color.black);
                        break;

                    case "Mastermind":
                        defensePowersList.Add(_def_pwrs[i]);
                        defensePowersDescriptionsList.Add(_def_descs[i]);
                        powersList.Add(new Color32(0, 51, 204, 255));
                        textColorList.Add(Color.white);
                        break;

                    case "Willpower":
                        defensePowersList.Add(_def_pwrs[i]);
                        defensePowersDescriptionsList.Add(_def_descs[i]);
                        powersList.Add(new Color32(102, 0, 204, 255));
                        textColorList.Add(Color.white);
                        break;

                    case "Invincible":
                        defensePowersList.Add(_def_pwrs[i]);
                        defensePowersDescriptionsList.Add(_def_descs[i]);
                        powersList.Add(new Color32(255, 153, 255, 255));
                        textColorList.Add(Color.black);
                        break;

                    case "Impervious":
                        defensePowersList.Add(_def_pwrs[i]);
                        defensePowersDescriptionsList.Add(_def_descs[i]);
                        powersList.Add(new Color32(191, 158, 27, 255));
                        textColorList.Add(Color.black);
                        break;

                    case "Regeneration":
                        defensePowersList.Add(_def_pwrs[i]);
                        defensePowersDescriptionsList.Add(_def_descs[i]);
                        powersList.Add(new Color32(0, 0, 0, 255));
                        textColorList.Add(Color.white);
                        break;

                    case "Invulnerability":
                        defensePowersList.Add(_def_pwrs[i]);
                        defensePowersDescriptionsList.Add(_def_descs[i]);
                        powersList.Add(new Color32(176, 190, 197, 255));
                        textColorList.Add(Color.white);
                        break;

                    case "None":

                        defensePowersList.Add(_def_pwrs[i]);
                        defensePowersDescriptionsList.Add(_def_descs[i]);
                        powersList.Add(new Color32(255, 255, 255, 255));
                        textColorList.Add(Color.black);
                        break;

                    case "":

                        defensePowersList.Add(_def_pwrs[i]);
                        defensePowersDescriptionsList.Add(_def_descs[i]);
                        powersList.Add(new Color32(255, 255, 255, 255));
                        textColorList.Add(Color.black);
                        break;

                    default:

                        defensePowersList.Add(_def_pwrs[i]);
                        defensePowersDescriptionsList.Add(_def_descs[i]);
                        powersList.Add(new Color32(255, 0, 235, 255));
                        textColorList.Add(Color.black);
                        break;
                }
            }

            while (valuesList.Count < 12) { valuesList.Add("--"); }
            while (powersList.Count < 12) { powersList.Add(new Color32(220, 220, 220, 255)); }
            while (textColorList.Count < 12) { textColorList.Add(new Color32(150, 150, 150, 255)); }
            while (defensePowersList.Count < 12) { defensePowersList.Add(""); }
            while (defensePowersDescriptionsList.Count < 12) { defensePowersDescriptionsList.Add(""); }

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
                if (_dmg_vals[i] == -1)
                {
                    valuesList.Add("KO");
                }
                else
                {
                    valuesList.Add(_dmg_vals[i].ToString());
                }

                switch (_dmg_pwrs[i])
                {
                    case "Ranged Combat Expert":
                        damagePowersList.Add(_dmg_pwrs[i]);
                        damagePowersDescriptionsList.Add(_dmg_descs[i]);
                        powersList.Add(new Color32(204, 0, 0, 255));
                        textColorList.Add(Color.white);
                        break;

                    case "Battle Fury":
                        damagePowersList.Add(_dmg_pwrs[i]);
                        damagePowersDescriptionsList.Add(_dmg_descs[i]);
                        powersList.Add(new Color32(255, 153, 51, 255));
                        textColorList.Add(Color.black);
                        break;

                    case "Support":
                        damagePowersList.Add(_dmg_pwrs[i]);
                        damagePowersDescriptionsList.Add(_dmg_descs[i]);
                        powersList.Add(new Color32(255, 255, 0, 255));
                        textColorList.Add(Color.black);
                        break;

                    case "Exploit Weakness":
                        damagePowersList.Add(_dmg_pwrs[i]);
                        damagePowersDescriptionsList.Add(_dmg_descs[i]);
                        powersList.Add(new Color32(0, 255, 51, 255));
                        textColorList.Add(Color.black);
                        break;

                    case "Enhancement":
                        damagePowersList.Add(_dmg_pwrs[i]);
                        damagePowersDescriptionsList.Add(_dmg_descs[i]);
                        powersList.Add(new Color32(0, 153, 0, 255));
                        textColorList.Add(Color.black);
                        break;

                    case "Probability Control":
                        damagePowersList.Add(_dmg_pwrs[i]);
                        damagePowersDescriptionsList.Add(_dmg_descs[i]);
                        powersList.Add(new Color32(179, 229, 252, 255));
                        textColorList.Add(Color.black);
                        break;

                    case "Shape Change":
                        damagePowersList.Add(_dmg_pwrs[i]);
                        damagePowersDescriptionsList.Add(_dmg_descs[i]);
                        powersList.Add(new Color32(0, 51, 204, 255));
                        textColorList.Add(Color.white);
                        break;

                    case "Close Combat Expert":
                        damagePowersList.Add(_dmg_pwrs[i]);
                        damagePowersDescriptionsList.Add(_dmg_descs[i]);
                        powersList.Add(new Color32(102, 0, 204, 255));
                        textColorList.Add(Color.white);
                        break;

                    case "Empower":
                        damagePowersList.Add(_dmg_pwrs[i]);
                        damagePowersDescriptionsList.Add(_dmg_descs[i]);
                        powersList.Add(new Color32(255, 153, 255, 255));
                        textColorList.Add(Color.black);
                        break;

                    case "Perplex":
                        damagePowersList.Add(_dmg_pwrs[i]);
                        damagePowersDescriptionsList.Add(_dmg_descs[i]);
                        powersList.Add(new Color32(191, 158, 27, 255));
                        textColorList.Add(Color.black);
                        break;

                    case "Outwit":
                        damagePowersList.Add(_dmg_pwrs[i]);
                        damagePowersDescriptionsList.Add(_dmg_descs[i]);
                        powersList.Add(new Color32(0, 0, 0, 255));
                        textColorList.Add(Color.white);
                        break;

                    case "Leadership":
                        damagePowersList.Add(_dmg_pwrs[i]);
                        damagePowersDescriptionsList.Add(_dmg_descs[i]);
                        powersList.Add(new Color32(176, 190, 197, 255));
                        textColorList.Add(Color.white);
                        break;

                    case "None":

                        damagePowersList.Add(_dmg_pwrs[i]);
                        damagePowersDescriptionsList.Add(_dmg_descs[i]);
                        powersList.Add(new Color32(255, 255, 255, 255));
                        textColorList.Add(Color.black);
                        break;

                    case "":

                        damagePowersList.Add(_dmg_pwrs[i]);
                        damagePowersDescriptionsList.Add(_dmg_descs[i]);
                        powersList.Add(new Color32(255, 255, 255, 255));
                        textColorList.Add(Color.black);
                        break;

                    default:

                        damagePowersList.Add(_dmg_pwrs[i]);
                        damagePowersDescriptionsList.Add(_dmg_descs[i]);
                        powersList.Add(new Color32(255, 0, 235, 255));
                        textColorList.Add(Color.black);
                        break;
                }
            }

            while (valuesList.Count < 12) { valuesList.Add("--"); }
            while (powersList.Count < 12) { powersList.Add(new Color32(220, 220, 220, 255)); }
            while (textColorList.Count < 12) { textColorList.Add(new Color32(150, 150, 150, 255)); }
            while (damagePowersList.Count < 12) { damagePowersList.Add(""); }
            while (damagePowersDescriptionsList.Count < 12) { damagePowersDescriptionsList.Add(""); }

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
        AttackingUnit = CurrentlyPreviewedUnit;
        tempUnitList.Clear();
        tempUnitList.Add(CurrentlyPreviewedUnit);

        // Cache both component types — multi-base units use Manual_Non_Single_Base_Unit_Script
        Unit_Script _us = ListOfUnits[index].GetComponent<Unit_Script>();
        Manual_Non_Single_Base_Unit_Script _ms = ListOfUnits[index].GetComponent<Manual_Non_Single_Base_Unit_Script>();

        // Helper: read string field from whichever component is present
        string PU_S(string usVal, string msVal) => _us != null ? usVal : msVal;
        int    PU_I(int    usVal, int    msVal) => _us != null ? usVal : msVal;
        bool   PU_B(bool   usVal, bool   msVal) => _us != null ? usVal : msVal;

        Update_Previewed_Dial(ListOfUnits, index, 0);

        Trait1_d = PU_S(_us?.trait1, _ms?.trait1);
        Trait2_d = PU_S(_us?.trait2, _ms?.trait2);
        Trait3_d = PU_S(_us?.trait3, _ms?.trait3);
        Trait4_d = PU_S(_us?.trait4, _ms?.trait4);
        Trait5_d = PU_S(_us?.trait5, _ms?.trait5);
        Trait6_d = PU_S(_us?.trait6, _ms?.trait6);

        speedType   = PU_S(_us?.speed_type,   _ms?.speed_type);
        attackType  = PU_S(_us?.attack_type,  _ms?.attack_type);
        defenseType = PU_S(_us?.defense_type, _ms?.defense_type);
        damageType  = PU_S(_us?.damage_type,  _ms?.damage_type);

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

        rangeTargetsNum = PU_I(_us != null ? _us.range_targets : 0, _ms != null ? _ms.range_targets : 0);

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

        RangeValue.text = PU_I(_us != null ? _us.range : 0, _ms != null ? _ms.range : 0).ToString();

        CallLoadDynamicUnitImage();

        //Sculpt.GetComponent<Image>().sprite = ListOfUnits[index].GetComponent<Unit_Script>().Sculpt_Image;

        UnitName.text = PU_S(_us?.unit_Name, _ms?.unit_Name);

        UnitPreviewPoints.text = PU_I(_us != null ? _us.points : 0, _ms != null ? _ms.points : 0).ToString();

        UnitKeywords.Clear();

        string[] _keywords = _us != null ? _us.keywords : (_ms != null ? _ms.keywords : new string[0]);
        for (int i = 0; i < _keywords.Length; i++)
        {
            UnitKeywords.Add(_keywords[i]);
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


        teamAbility = PU_S(_us?.team_ability, _ms?.team_ability);

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

        // Read IM/IT values — support both single-base (Unit_Script) and multi-base (Manual_Non_Single_Base_Unit_Script)
        IT_elevated            = PU_B(_us != null && _us.IT_elevated,            _ms != null && _ms.IT_elevated);
        IT_hindering           = PU_B(_us != null && _us.IT_hindering,           _ms != null && _ms.IT_hindering);
        IT_blocking            = PU_B(_us != null && _us.IT_blocking,            _ms != null && _ms.IT_blocking);
        IT_blocking_destroy    = PU_B(_us != null && _us.IT_blocking_destroy,    _ms != null && _ms.IT_blocking_destroy);
        IT_characters          = PU_B(_us != null && _us.IT_characters,          _ms != null && _ms.IT_characters);
        IT_adjacent_characters = PU_B(_us != null && _us.IT_adjacent_characters, _ms != null && _ms.IT_adjacent_characters);

        IM_elevated            = PU_B(_us != null && _us.IM_elevated,            _ms != null && _ms.IM_elevated);
        IM_hindering           = PU_B(_us != null && _us.IM_hindering,           _ms != null && _ms.IM_hindering);
        IM_water               = PU_B(_us != null && _us.IM_water,               _ms != null && _ms.IM_water);
        IM_blocking            = PU_B(_us != null && _us.IM_blocking,            _ms != null && _ms.IM_blocking);
        IM_outdoor_blocking    = PU_B(_us != null && _us.IM_outdoor_blocking,    _ms != null && _ms.IM_outdoor_blocking);
        IM_blocking_destroy    = PU_B(_us != null && _us.IM_blocking_destroy,    _ms != null && _ms.IM_blocking_destroy);
        IM_characters          = PU_B(_us != null && _us.IM_characters,          _ms != null && _ms.IM_characters);
        IM_adjacent_characters = PU_B(_us != null && _us.IM_adjacent_characters, _ms != null && _ms.IM_adjacent_characters);

        string _previewName = PU_S(_us?.unit_Name, _ms?.unit_Name);
        Debug.Log($"[IM/IT] {_previewName}: IM_Elev={IM_elevated} IM_Hind={IM_hindering} IM_Water={IM_water} IM_Block={IM_blocking} IM_Out={IM_outdoor_blocking} IM_Des={IM_blocking_destroy} IM_Char={IM_characters} IM_Adj={IM_adjacent_characters} | IT_Elev={IT_elevated} IT_Hind={IT_hindering} IT_Block={IT_blocking} IT_Des={IT_blocking_destroy} IT_Char={IT_characters} IT_Adj={IT_adjacent_characters}");

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

        if (IT_elevated) { ITElevated.SetActive(true); }
        if (IT_hindering) { ITHindering.SetActive(true); }
        if (IT_blocking) { ITBlocking.SetActive(true); }
        if (IT_characters) { ITCharacters.SetActive(true); }
        if (IT_blocking_destroy) { ITBlockingDestroy.SetActive(true); }
        if (IT_adjacent_characters) { ITAdjacentCharacters.SetActive(true); }

        if (IM_elevated) { IMElevated.SetActive(true); }
        if (IM_hindering) { IMHindering.SetActive(true); }
        if (IM_water) { IMWater.SetActive(true); }
        if (IM_blocking) { IMBlocking.SetActive(true); }
        if (IM_outdoor_blocking) { IMBlockingOutdoor.SetActive(true); }
        if (IM_blocking_destroy) { IMBlockingDestroy.SetActive(true); }
        if (IM_characters) { IMCharacters.SetActive(true); }
        if (IM_adjacent_characters) { IMAdjacentCharacters.SetActive(true); }

        traitImg1.SetActive(Trait1_d != "");
        traitImg2.SetActive(Trait2_d != "");
        traitImg3.SetActive(Trait3_d != "");
        traitImg4.SetActive(Trait4_d != "");
        traitImg5.SetActive(Trait5_d != "");
        traitImg6.SetActive(Trait6_d != "");

        //Slot
        SlotI1.color = Color.white;
        SlotI2.color = Color.white;
        SlotI3.color = Color.white;
        SlotI4.color = Color.white;
        SlotI5.color = Color.white;
        SlotI6.color = Color.white;
        SlotI7.color = Color.white;
        SlotI8.color = Color.white;
        SlotI9.color = Color.white;
        SlotI10.color = Color.white;
        SlotI11.color = Color.white;
        SlotI12.color = Color.white;

        int calculatedSlot = PU_I(_us != null ? _us.click_number : 1, _ms != null ? _ms.click_number : 1);

        while (calculatedSlot > 12)
        {
            calculatedSlot -= 12;
        }

        switch (calculatedSlot)
        {
            case 1:
                SlotI1.color = Color.black;
                break;
            case 2:
                SlotI2.color = Color.black;
                break;
            case 3:
                SlotI3.color = Color.black;
                break;
            case 4:
                SlotI4.color = Color.black;
                break;
            case 5:
                SlotI5.color = Color.black;
                break;
            case 6:
                SlotI6.color = Color.black;
                break;
            case 7:
                SlotI7.color = Color.black;
                break;
            case 8:
                SlotI8.color = Color.black;
                break;
            case 9:
                SlotI9.color = Color.black;
                break;
            case 10:
                SlotI10.color = Color.black;
                break;
            case 11:
                SlotI11.color = Color.black;
                break;
            case 12:
                SlotI12.color = Color.black;
                break;


        }
    }

    private bool IsPointerOverUI()
    {
        if (Input.touchCount > 0)
            return EventSystem.current.IsPointerOverGameObject(Input.GetTouch(0).fingerId);
        return EventSystem.current.IsPointerOverGameObject();
    }

    private static bool IsBaseType(string baseType, string keyword)
    {
        if (string.IsNullOrEmpty(baseType)) return false;
        return baseType.IndexOf(keyword, System.StringComparison.OrdinalIgnoreCase) >= 0;
    }
}
