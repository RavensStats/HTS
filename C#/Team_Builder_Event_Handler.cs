using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine.Networking;

public class Team_Builder_Event_Handler : MonoBehaviour
{
    List<string> sets = new List<string>() { "Select Set",
                                             "Infinity Challenge",
                                             "Hypertime" };

    public Dropdown set_Dropdown, unit_Dropdown, team_Dropdown, team_Name_Dropdown;

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
    public string selectedSet;
    public string teamAbility;
    public string SAVE_FOLDER;

    public int pointsTotal;
    public int rangeTargetsNum;
    public int selectedUnitIndex, selectedTeamUnitIndex, keywordIndex, selectedTeamLoadIndex;

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
    public List<GameObject> team = new List<GameObject>();
    public List<GameObject> tempUnitList = new List<GameObject>();

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

   

    // Start is called before the first frame update
    void Start()
    {
        pointsTotal = 0;
        pointsTotalText.text = pointsTotal + " pts";
        Populate_Set_List();
    
        SAVE_FOLDER = Application.dataPath + "/Saved_Teams/";

        if (!Directory.Exists(SAVE_FOLDER))
        {
            Directory.CreateDirectory(SAVE_FOLDER);
        }

    }

    void Populate_Set_List()
    {
        set_Dropdown.AddOptions(sets);
    }

    public void Populate_Unit_List()
    {
        selectedUnits.Clear();
        unit_Dropdown.ClearOptions();

         var localUnits = Resources.LoadAll("Units/" + selectedSet, typeof(GameObject));
        //        var units = Resources.FindObjectsOfTypeAll(typeof(GameObject)).Cast<GameObject>().Where(g => g.tag == "Unit").ToList();

        foreach (GameObject unit in localUnits)
        {
            if (unit.GetComponent<Unit_Script>().set == selectedSet)
            {
                units.Add(unit);
                selectedUnits.Add(unit.GetComponent<Unit_Script>().collectors_number + " " + unit.GetComponent<Unit_Script>().unit_Name + " " + unit.GetComponent<Unit_Script>().points.ToString() + " pts");
            }
        }
        unit_Dropdown.AddOptions(selectedUnits);
        Preview_Unit(units, 0);
    }

    public void Dropdown_IndexChanged(int index)
    {
        selectedSet = sets[index];
        currentDialIndex = 0;
    }

    public void Unit_Dropdown_IndexChanged(int index)
    {
        selectedUnitIndex = index;
        currentDialIndex = 0;
        Preview_Unit(units, selectedUnitIndex);
    }

    public void Team_Unit_Dropdown_IndexChanged(int index)
    {
        selectedTeamUnitIndex = index;
        currentDialIndex = 0;
        Preview_Unit(team, selectedTeamUnitIndex);
    }

    public void Team_Name_Dropdown_IndexChanged(int index)
    {
        selectedTeamLoadIndex = index;
    }

    public void Add_To_Team()
    {
        //var units = Resources.LoadAll("Units/" + selectedSet, typeof(GameObject));
        selectedTeam.Add(units[selectedUnitIndex].GetComponent<Unit_Script>().collectors_number + " " + units[selectedUnitIndex].GetComponent<Unit_Script>().unit_Name + " " + units[selectedUnitIndex].GetComponent<Unit_Script>().points.ToString() + " pts");
        team_Dropdown.ClearOptions();
        team_Dropdown.AddOptions(selectedTeam);
        team.Add(units[selectedUnitIndex]);
        pointsTotal += units[selectedUnitIndex].GetComponent<Unit_Script>().points;
        pointsTotalText.text = pointsTotal + " pts";
    }

    public void Remove_From_Team()
    {
        if(selectedTeam.Count > 0)
        {
            selectedTeam.Remove(selectedTeam[selectedTeamUnitIndex]);
            team_Dropdown.ClearOptions();
            team_Dropdown.AddOptions(selectedTeam);
            Team_Unit_Dropdown_IndexChanged(0);
            pointsTotal -= team[selectedTeamUnitIndex].GetComponent<Unit_Script>().points;
            pointsTotalText.text = pointsTotal + " pts";
        }

    }

    public void Clear_Team()
    {
        selectedTeam.Clear();
        team_Dropdown.ClearOptions();
        pointsTotal = 0;
        pointsTotalText.text = pointsTotal + " pts";
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
        if(keywordIndex < UnitKeywords.Count-1)
        {
        keywordIndex++;
        CurrentKeyword.text = UnitKeywords[keywordIndex];
        }

    }

    public void Display_Speed_Power_Text(int SelectedSlot)
    {
        if (speedPowersList.Count() > 0)
        {
            PowerDescription.text = speedPowersList[SelectedSlot];
        }
    }

    public void Display_Attack_Power_Text(int SelectedSlot)
    {
        if (attackPowersList.Count() > 0)
        {
            PowerDescription.text = attackPowersList[SelectedSlot];
        }
    }

    public void Display_Defense_Power_Text(int SelectedSlot)
    {
        if (defensePowersList.Count() > 0)
        {
            PowerDescription.text = defensePowersList[SelectedSlot];
        }
    }

    public void Display_Damage_Power_Text(int SelectedSlot)
    {
        if (damagePowersList.Count() > 0)
        {
            PowerDescription.text = damagePowersList[SelectedSlot];
        }
    }

    public void Previous_Click()
    {
        if (units.Count() > 0 && currentDialIndex > 0)
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

        if (units.Count() > 0 && currentDialIndex + 12 < CurrentlyPreviewedUnit.GetComponent<Unit_Script>().dial_length)
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

    private Team_Save_Script CreateSaveTeamObject()
    {
        string[] tempNameArray = new string[team.Count()];
        int teamPoints = 0;
        string teamName = TeamName.text;
        int i = 0;

        foreach (GameObject targetGameObject in team)
        {
            tempNameArray[i] = targetGameObject.GetComponent<Unit_Script>().name;
            teamPoints += targetGameObject.GetComponent<Unit_Script>().points;
            i++;
        }

        Team_Save_Script saveTeam = new Team_Save_Script() { teamUnitNames = tempNameArray, teamName = teamName, teamPoints = teamPoints };
        //saveTeam.teamUnitNames = tempNameArray[];
        //saveTeam.teamName = teamName;
        //saveTeam.teamPoints = teamPoints;

        return saveTeam;
    }


    public void SaveTeam()
    {
        Team_Save_Script currentTeam = CreateSaveTeamObject();
        string json = JsonUtility.ToJson(currentTeam);

        using (StreamWriter writer = new StreamWriter(SAVE_FOLDER + TeamName.text + ".team"))
        {
            writer.WriteLine(json);
        }

        loadedTeamsList.Clear();
        team_Name_Dropdown.ClearOptions();
        TeamName.text = "";
        team.Clear();
        team_Dropdown.ClearOptions();
        pointsTotalText.text = "";
    }

    public void LoadTeamNames()
    {
        DirectoryInfo directoryInfo = new DirectoryInfo(SAVE_FOLDER);
        FileInfo[] savedTeams = directoryInfo.GetFiles("*.team");

        foreach (FileInfo savedTeam in savedTeams)
        {
            loadedTeamsList.Add(savedTeam.Name.Substring(0, savedTeam.Name.IndexOf(".")));
        }
        team_Name_Dropdown.AddOptions(loadedTeamsList);

    }

    public void LoadTeam()
    {
        Team_Save_Script loadedTeam = new Team_Save_Script();
        team.Clear();
        team_Name_Dropdown.ClearOptions();

        var allUnits = Resources.LoadAll("Units/", typeof(GameObject));

        if (loadedTeamsList.Count() > 0)
        {
            TeamName.gameObject.SetActive(true);
            team_Name_Dropdown.gameObject.SetActive(false);
            team_Dropdown.ClearOptions();

            using (StreamReader reader = new StreamReader(SAVE_FOLDER + loadedTeamsList[selectedTeamLoadIndex] + ".team"))
            {
                //                string json = reader.ReadToEnd();
                DirectoryInfo d = new DirectoryInfo(SAVE_FOLDER);
                FileInfo[] f = d.GetFiles(loadedTeamsList[selectedTeamLoadIndex] + ".team");
                loadedTeam = JsonUtility.FromJson<Team_Save_Script>(File.ReadAllText(f[0].FullName));
                TeamName.text = loadedTeam.teamName;

                for (int i = 0; i < loadedTeam.teamUnitNames.Length; i++)
                {

                    loadedUnitsList.Add(loadedTeam.teamUnitNames[i]);
                }

                for (int i = 0; i < loadedUnitsList.Count(); i++)
                {
                    foreach(GameObject allUnit in allUnits)
                    {
                        if(allUnit.name == loadedUnitsList[i])
                        {
                            team.Add(allUnit);
                        }
                    }
                }

                team_Dropdown.AddOptions(loadedUnitsList);
                pointsTotal = loadedTeam.teamPoints;
                pointsTotalText.text = loadedTeam.teamPoints.ToString();

            }

            loadedTeamsList.Clear();
            loadedUnitsList.Clear();
            team_Name_Dropdown.ClearOptions();
        }
        else
        {
            TeamName.gameObject.SetActive(false);
            team_Name_Dropdown.gameObject.SetActive(true);
            LoadTeamNames();
            Team_Name_Dropdown_IndexChanged(0);
        }

    }


    public void DeleteTeam()
    {
        if (loadedTeamsList.Count() > 0)
        {
            TeamName.gameObject.name = "";
            TeamName.gameObject.SetActive(true);
            team_Name_Dropdown.gameObject.SetActive(false);

            File.Delete(SAVE_FOLDER + loadedTeamsList[selectedTeamLoadIndex] + ".meta");
            File.Delete(SAVE_FOLDER + loadedTeamsList[selectedTeamLoadIndex] + ".team");

            TeamName.text = "";
        }
        else
        {
            TeamName.gameObject.SetActive(false);
            team_Name_Dropdown.gameObject.SetActive(true);
            LoadTeamNames();
            Team_Name_Dropdown_IndexChanged(0);
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

        int DialLength = ListOfUnits[listIndex].GetComponent<Unit_Script>().dial_length ;
        valuesList.Clear();
        powersList.Clear();
        textColorList.Clear();
        speedPowersList.Clear();
        attackPowersList.Clear();
        defensePowersList.Clear();
        damagePowersList.Clear();

        if (DialLength >= startingIndex + 12 && startingIndex >= 0)
        {
            currentDialIndex = startingIndex;

            for (int i = currentDialIndex; i <= currentDialIndex + 11; i++)
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
                        powersList.Add(new Color32(153, 51, 51, 255));
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

                    default:
                        speedPowersList.Add(ListOfUnits[listIndex].GetComponent<Unit_Script>().speed_powers[i]);
                        powersList.Add(new Color32(255, 255, 255, 255));
                        textColorList.Add(Color.black);
                        break;
                }
            }

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


            for (int i = currentDialIndex; i <= currentDialIndex + 11; i++)
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
                        powersList.Add(new Color32(153, 51, 51, 255));
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

                    default:
                        attackPowersList.Add(ListOfUnits[listIndex].GetComponent<Unit_Script>().attack_powers[i]);
                        powersList.Add(new Color32(255, 255, 255, 255));
                        textColorList.Add(Color.black);
                        break;
                }
            }


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

            for (int i = currentDialIndex; i <= currentDialIndex + 11; i++)
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
                        powersList.Add(new Color32(153, 51, 51, 255));
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

                    default:
                        defensePowersList.Add(ListOfUnits[listIndex].GetComponent<Unit_Script>().defense_powers[i]);
                        powersList.Add(new Color32(255, 255, 255, 255));
                        textColorList.Add(Color.black);
                        break;
                }
            }


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

            for (int i = currentDialIndex; i <= currentDialIndex + 11; i++)
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
                        powersList.Add(new Color32(153, 51, 51, 255));
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

                    default:
                        damagePowersList.Add(ListOfUnits[listIndex].GetComponent<Unit_Script>().damage_powers[i]);
                        powersList.Add(new Color32(255, 255, 255, 255));
                        textColorList.Add(Color.black);
                        break;
                }
            }


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

        Sculpt.GetComponent<Image>().sprite = ListOfUnits[index].GetComponent<Unit_Script>().Sculpt_Image;

        UnitName.text = ListOfUnits[index].GetComponent<Unit_Script>().unit_Name;

        UnitPreviewPoints.text = ListOfUnits[index].GetComponent<Unit_Script>().points.ToString();

        UnitKeywords.Clear();

        for (int i=0; i< ListOfUnits[index].GetComponent<Unit_Script>().keywords.Length; i++)
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

        if (ListOfUnits[index].GetComponent<Unit_Script>().trait1 != "") { traitImg1.SetActive(true); }
        if (ListOfUnits[index].GetComponent<Unit_Script>().trait2 != "") { traitImg2.SetActive(true); }
        if (ListOfUnits[index].GetComponent<Unit_Script>().trait3 != "") { traitImg3.SetActive(true); }
        if (ListOfUnits[index].GetComponent<Unit_Script>().trait4 != "") { traitImg4.SetActive(true); }
        if (ListOfUnits[index].GetComponent<Unit_Script>().trait5 != "") { traitImg5.SetActive(true); }
        if (ListOfUnits[index].GetComponent<Unit_Script>().trait6 != "") { traitImg6.SetActive(true); }


    }
}
