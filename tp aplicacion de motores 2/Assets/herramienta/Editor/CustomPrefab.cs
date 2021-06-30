using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;


public class CustomPrefab : EditorWindow
{
    Vector2 scrollPos;
    GameObject PrefabGameObject;
    string ObjectName = "Default";

    string folderName = "DefaultFolder";
    string pathToFolder = "";

    string nameToChange = "Renombre";

     

    private void OnGUI()
    {
        EditorGUILayout.BeginVertical(GUILayout.ExpandWidth(true), GUILayout.ExpandHeight(true));
        scrollPos = EditorGUILayout.BeginScrollView(scrollPos, false, false);

        GUILayout.Label("- Folder Creator", EditorStyles.boldLabel);
        EditorGUILayout.Space();
        
        CreateFolders();

        GUILayout.Space(20);

        GUILayout.Label("- Prefab Creator", EditorStyles.boldLabel);
        EditorGUILayout.Space();
        PrefabGameObject = (GameObject)EditorGUILayout.ObjectField("Insert Prefab:", PrefabGameObject, typeof(GameObject), true);

        if (PrefabGameObject != null)
        {
            if (!AssetDatabase.Contains(PrefabGameObject)) 
            {
                ObjectName = EditorGUILayout.TextField("Name:", ObjectName);

                if (GUI.Button(GUILayoutUtility.GetRect(20, 20), "Create Prefab"))
                    CreatePrefab();
            }
            else
                PrefabSettings();
        }

        EditorGUILayout.EndScrollView();
        EditorGUILayout.EndVertical();
    }

    void PrefabSettings()
    {
        string currentPath = AssetDatabase.GetAssetPath(PrefabGameObject); 

        EditorGUILayout.LabelField(currentPath, EditorStyles.boldLabel);

        if (GUI.Button(GUILayoutUtility.GetRect(50, 50, 50, 50), "Open Prefab"))
        {
            AssetDatabase.OpenAsset(PrefabGameObject); 
        }

        GUILayout.Space(5);

        
        if (GUI.Button(GUILayoutUtility.GetRect(50, 50, 50, 50), "Borrar"))
        {
            AssetDatabase.MoveAssetToTrash(currentPath); 
            SaveAssets();
        }
             

        nameToChange = EditorGUILayout.TextField("New Name:", nameToChange);

        if (GUI.Button(GUILayoutUtility.GetRect(50, 50, 50, 50), "Renombrar Asset"))
        {
            AssetDatabase.RenameAsset(currentPath, nameToChange); 
            SaveAssets();
        }

    }

    void CreateFolders()
    {
               
        folderName = EditorGUILayout.TextField("Name:", folderName);
        pathToFolder = EditorGUILayout.TextField("Path Name:", pathToFolder);

        if (GUI.Button(GUILayoutUtility.GetRect(20, 20), "Create Folder"))
        {
            if (folderName == "")
            {
                var window = (MapsGeneratorFM)GetWindow(typeof(MapsGeneratorFM));
                window.ShowNotification(new GUIContent("Missing folder name"));
            }
            else
            {
                var pathTemp = pathToFolder;

                if (!string.IsNullOrEmpty(pathTemp) && !string.IsNullOrWhiteSpace(pathTemp))
                    pathTemp = "Assets/" + pathTemp;

                if (AssetDatabase.IsValidFolder(pathTemp))
                {

                    AssetDatabase.CreateFolder(pathTemp, folderName);

                }
                else
                {

                    pathTemp = "Assets";
                    AssetDatabase.CreateFolder(pathTemp, folderName);
                }
                pathToFolder = pathTemp + "/" + folderName;
                SaveAssets();
            }
        }
    }

    

    void CreatePrefab()
    {
       
        if (PrefabGameObject != null)
        {
            if (!AssetDatabase.IsValidFolder(pathToFolder))
            {
                pathToFolder = "Assets";
            }
          
            PrefabUtility.SaveAsPrefabAsset(PrefabGameObject, pathToFolder +"/"  + ObjectName + ".prefab");
        }
        SaveAssets();
    }

    void SaveAssets()
    {
        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();
    }

    
}
