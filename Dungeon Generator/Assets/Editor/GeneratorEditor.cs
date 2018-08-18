using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

class GeneratorEditor : EditorWindow {

    public GameObject levelGenerator;
    public LevelGenerator generator;

    private void Awake()
    {
        levelGenerator = GameObject.Find("Level Generator");
        generator = levelGenerator.GetComponent<LevelGenerator>();
    }

    [MenuItem("Window/Dungeon Generator")]

    public static void ShowWindow()
    {
        EditorWindow.GetWindow(typeof(GeneratorEditor));
    }

    void OnGUI()
    {
        GUILayout.Label("Level Generator Settings", EditorStyles.boldLabel);
        EditorGUILayout.HelpBox("Grid Dimensions determine the width, height, and length of the level layout grid. X and Z determine the ground layout. Y determines the height. /n Room Dimensions determine the size of the rooms used to generate the level.", MessageType.Info);
        generator.gridSizeVector = EditorGUILayout.Vector3Field("Grid Dimensions:", generator.gridSizeVector);
        generator.roomSizeVector = EditorGUILayout.Vector3Field("Room Dimensions:", generator.roomSizeVector);
        generator.numberOfRooms = EditorGUILayout.IntField("Number of Rooms: ", generator.numberOfRooms);
        GUILayout.Label("Room Contents");
        generator._blockAmount = EditorGUILayout.IntField("Number of Blocks Possible (per room): ", generator._blockAmount);
        generator._blockChance = EditorGUILayout.IntField("Block Chance: ", generator._blockChance);

        if (GUILayout.Button("Generate Level"))
        {
            generator.CreateLevelHolder();
            generator.CreateRooms();
            generator.SetRoomDoors();
            generator.InstantiateRooms();
        }
        if (GUILayout.Button("Destroy"))
        {
            DestroyImmediate(generator.level);
        }
        if (GUILayout.Button("Close"))
        {
            this.Close();
        }
    }
}
