using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Game_Save_Script
{
    public string[] ObjectList_Name;
    public float[] ObjectList_LocationX;
    public float[] ObjectList_LocationY;
    public float[] ObjectList_LocationZ;

    public string[] UnitList_Name;
    public float[] UnitList_LocationX;
    public float[] UnitList_LocationY;
    public float[] UnitList_LocationZ;
    public int[] UnitList_ClickNumber;
    public int[] UnitList_Actions;
    public int[] UnitList_TeamNumber;
    public int[] UnitList_IsDynamic;
    public string[] UnitList_Set_Name;
    public string[] UnitList_Collectors_Number;

    public string[] ManualUnitList_Name;
    public float[] ManualUnitList_LocationX;
    public float[] ManualUnitList_LocationY;
    public float[] ManualUnitList_LocationZ;
    public int[] ManualUnitList_ClickNumber;
    public int[] ManualUnitList_Actions;
    public int[] ManualUnitList_TeamNumber;
    public int[] ManualUnitList_IsDynamic;
    public string[] ManualUnitList_Set_Name;
    public string[] ManualUnitList_Collectors_Number;
}
