using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;


public class Menu_Script
{
    //// Assign Material to all Tiles
    //[MenuItem("Tools/Assign Tile Material")]
    //public static void AssignTileMaterial()
    //{
    //    GameObject[] tiles = GameObject.FindGameObjectsWithTag("Tile");
    //    Material material = Resources.Load <Material>("Tile_Types/Tile_Clear_Terrain");

    //    foreach (GameObject t in tiles)
    //    {
    //        t.GetComponent<Renderer>().material = material;
    //    }
    //}

    //// Assign Material to all Tiles
    //[MenuItem("Tools/Assign Tile Script")]
    //public static void AssignTileScript()
    //{
    //    GameObject[] tiles = GameObject.FindGameObjectsWithTag("Tile");

    //    foreach (GameObject t in tiles)
    //    {
    //        t.AddComponent<Tile_Script>();
    //    }
    //}

    ////Rename GameObjects

    //[MenuItem("Tools/Rename Objects")]
    //public static void Rename_Objects()
    //{
    //    GameObject[] RenameObject;
    //    RenameObject = GameObject.FindGameObjectsWithTag("Rename");

    //    foreach (GameObject RnOb in RenameObject)
    //    {
    //        RnOb.name = "Atk" + RnOb.name.Substring(3, RnOb.name.Length - 3);


    //        //Rename objects with 1 and 2 digit copy numbers
    //        //if (RnOb.name.Length == 18)
    //        //{
    //        //    if (RnOb.name.Substring(13, 1) == "g")
    //        //    {
    //        //        RnOb.name = RnOb.name.Substring(0, 14);
    //        //    }
    //        //}
    //        //else if(RnOb.name.Length == 19)
    //        //{
    //        //    if (RnOb.name.Substring(14, 1) == "g")
    //        //    {
    //        //        RnOb.name = RnOb.name.Substring(0, 15);
    //        //    }
    //        //}
    //        //RnOb.name = "Atk" + RnOb.name.Substring(3, RnOb.name.Length - 3);

    //    }
    //}

    //[MenuItem("Tools/DynamicUnitTest")]
    //public static void DynamicUnitTest()
    //{
    //    var allUnits = Resources.LoadAll("Units/Dynamic", typeof(GameObject));

    //    foreach (GameObject DynamicUnit in allUnits)
    //    {
    //        GameObject newUnit = GameObject.Instantiate(DynamicUnit, new Vector3(1.5f, 0.0f, 1.5f), Quaternion.identity);
    //        DynamicUnit.GetComponent<Unit_Script>().unit_Name = "Test";
    //        DynamicUnit.name = "Test_Unit";
    //        Debug.Log(DynamicUnit.name);
    //    }
    //}

}
