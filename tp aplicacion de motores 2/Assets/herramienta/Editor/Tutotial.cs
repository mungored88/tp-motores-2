using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class Tutotial : EditorWindow
{
    private void OnGUI()
    {
        GUILayout.Label("Tutorial", EditorStyles.boldLabel);
        EditorGUILayout.Space();
        EditorGUILayout.Space();
        GUILayout.Label("Introduction:", EditorStyles.boldLabel);
        GUILayout.Label("1- Insert prefabs into each color's Prefab slot");
        GUILayout.Label("2- Inserts a texture of the desired map into the Map blueprint slot");
        GUILayout.Label("3- Press Generate to view your map");
        EditorGUILayout.Space();
        EditorGUILayout.Space();
        GUILayout.Label("Prefab Creator:", EditorStyles.boldLabel);
        GUILayout.Label("1- Drag the level to the prefab window");
        GUILayout.Label("2- Rename the prefab at your will");
        GUILayout.Label("3- Press Create Prefab to save your prefab");
        GUILayout.Label("Nota:", EditorStyles.boldLabel);
        GUILayout.Label("You can create a folder and save your prefab there");
        GUILayout.Label("1- Type the name of the folder");
        GUILayout.Label("2- you can create a Path if you want to place it inside another folder");
        GUILayout.Label("3- Press the Create Folder button");

    }
}
