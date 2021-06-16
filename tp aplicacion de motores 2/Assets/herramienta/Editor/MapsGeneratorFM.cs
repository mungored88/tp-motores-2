﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class MapsGeneratorFM : EditorWindow
{
    Material levelMaterial;
    GameObject GoPrefab;
    int i = 0;

    public Transform trans;
    public float TileDimension = 4f;

    public GameObject prefabFloor;
    public GameObject prefabWall;
    public GameObject prefabCorner;
    public GameObject prefabCollumn;
    public GameObject prefabCenterC;
    public Texture2D  Map2D;

    private int width;
    private int height;

    [MenuItem("Custom/MapsGFM")]

    public static void OpenWindow()
    {
        var window = GetWindow(typeof(MapsGeneratorFM));
        window.Show();
    }

    private void OnEnable()
    {
        CheckEvent();
    }

    private void OnGUI()
    {
        EditorGUILayout.Space();
        GUILayout.Label("- Insert Prefabs", EditorStyles.boldLabel);

            GUILayout.BeginHorizontal();
            GUILayout.Label(" White || Floor", GUILayout.Width(150));
            prefabFloor = EditorGUILayout.ObjectField(prefabFloor, typeof(GameObject), true) as GameObject;
            GUILayout.EndHorizontal();

            GUILayout.BeginHorizontal();
            GUILayout.Label(" Red || Wall", GUILayout.Width(150));
            prefabWall = EditorGUILayout.ObjectField(prefabWall, typeof(GameObject), true) as GameObject;
            GUILayout.EndHorizontal();

            GUILayout.BeginHorizontal();
            GUILayout.Label(" Green || Outer Corner", GUILayout.Width(150));
            prefabCorner = EditorGUILayout.ObjectField(prefabCorner, typeof(GameObject), true) as GameObject;
            GUILayout.EndHorizontal();

            GUILayout.BeginHorizontal();
            GUILayout.Label(" Blue || Collumn", GUILayout.Width(150));
            prefabCollumn = EditorGUILayout.ObjectField(prefabCollumn, typeof(GameObject), true) as GameObject;
            GUILayout.EndHorizontal();

            GUILayout.BeginHorizontal();
            GUILayout.Label(" Cyan || Center Collumn", GUILayout.Width(150));
            prefabCenterC = EditorGUILayout.ObjectField(prefabCenterC, typeof(GameObject), true) as GameObject;
            GUILayout.EndHorizontal();

        EditorGUILayout.Space();
        GUILayout.Label("- Insert Map Blueprint", EditorStyles.boldLabel);
        Map2D = EditorGUILayout.ObjectField(Map2D, typeof(Texture2D), true) as Texture2D;

        EditorGUILayout.Space();
        GUILayout.Label("- Insert Material", EditorStyles.boldLabel);
        levelMaterial = EditorGUILayout.ObjectField(levelMaterial, typeof(Material), true) as Material;

        EditorGUILayout.Space();
        if (GUILayout.Button("Generate"))
        {
            EmptyObjectDad();
        }

        if (GUI.Button(GUILayoutUtility.GetRect(20, 20), "Make Prefab"))
        {
            GetWindow(typeof(CustomPrefab)).Show();
        }

    }

    void CheckEvent()
    {
        minSize = new Vector2(300, 300);
        maxSize = new Vector3 (300, 300);
    }

    //Creo el mapa adentro de un empty object
    private void EmptyObjectDad()
    {
        GameObject level = new GameObject("Level" + i++);
        GenerateMap(level);
    }

    // Cambio el material
    void MaterialChange()
    {
        foreach (GameObject obj in Selection.gameObjects)
        {
            Material mat = obj.GetComponent<Material>();
        }
    }

    // Genero mapas segun un plano
    private void GenerateMap(GameObject level)
    {
        float multiplierFactor = TileDimension + float.Epsilon;
        width = Map2D.width;
        height = Map2D.height;
        Color[] pixels = Map2D.GetPixels();
        for (int i = 0; i < height; i++)
        {
            for (int j = 0; j < width; j++)
            {
                Color pixelColor = pixels[i * height + j];
                if (pixelColor == Color.white)
                {                              //Floor
                    GameObject inst = GameObject.Instantiate(prefabFloor, trans);
                    inst.transform.position = new Vector3(j * multiplierFactor, 0, i * multiplierFactor);
                    inst.transform.parent = level.transform;
                    getMaterial(inst);
                }
                if (pixelColor == Color.red)   //Wall
                {
                    GameObject inst = GameObject.Instantiate(prefabWall, trans);
                    inst.transform.position = new Vector3(j * multiplierFactor, 0, i * multiplierFactor);
                    inst.transform.Rotate(new Vector3(0, FindRotationW(pixels, i, j), 0), Space.Self);
                    inst.transform.parent = level.transform;
                    getMaterial(inst);
                }
                if (pixelColor == Color.green) //Corner
                {
                    GameObject inst = GameObject.Instantiate(prefabCorner, trans);
                    inst.transform.position = new Vector3(j * multiplierFactor, 0, i * multiplierFactor);
                    inst.transform.Rotate(new Vector3(0, FindRotationL(pixels, i, j), 0), Space.Self);
                    inst.transform.parent = level.transform;
                    getMaterial(inst);
                }
                if (pixelColor == Color.blue) //Collumn
                {
                    GameObject inst = GameObject.Instantiate(prefabCollumn, trans);
                    inst.transform.position = new Vector3(j * multiplierFactor, 0, i * multiplierFactor);
                    inst.transform.Rotate(new Vector3(0, FindRotationC(pixels, i, j), 0), Space.Self);
                    inst.transform.parent = level.transform;
                    getMaterial(inst);
                }
                if (pixelColor == Color.cyan) //Center Collumn
                { 
                    GameObject inst = GameObject.Instantiate(prefabCenterC, trans);
                    inst.transform.position = new Vector3(j * multiplierFactor, 0, i * multiplierFactor);
                    inst.transform.parent = level.transform;
                    getMaterial(inst);
                }
            }
        }
    }

    private void getMaterial(GameObject inst)
    {
        MeshRenderer[] meshes;
        meshes = inst.GetComponentsInChildren<MeshRenderer>();
        foreach(MeshRenderer mesh in meshes)
        {
            mesh.material = levelMaterial;
        }
        MaterialChange();
    }

    //rotacion pared
    private float FindRotationW(Color[] pixels, int i, int j)
    {
        float Rotation = 0f;

        if (i - 1 >= 0 && (pixels[(i - 1) * height + j] == Color.black))
        {
            Rotation = 90f;
        }
        else if (j - 1 >= 0 && (pixels[i * height + (j - 1)] == Color.black))
        {
            Rotation = 180f;
        }
        else if (i + 1 < height && (pixels[(i + 1) * height + j] == Color.black))
        {
            Rotation = -90f;
        }
        return Rotation;
    }

    //rotacion columna
    private float FindRotationC(Color[] pixels, int i, int j)
    {
        float Rotation = 0f;

        if (i - 1 >= 0 && j + 1 < width && (pixels[(i - 1) * height + (j + 1)] == Color.black))
            Rotation = 90f;
        else if (i - 1 >= 0 && j - 1 >= 0 && (pixels[(i - 1) * height + (j - 1)] == Color.black))
            Rotation = 180f;
        else if (i + 1 < height && j - 1 >= 0 && (pixels[(i + 1) * height + (j - 1)] == Color.black))
            Rotation = -90f;
        return Rotation;
    }

    //Rotacion esquinero
    private float FindRotationL(Color[] pixels, int i, int j)
    {
        //posicion por default
        float rotation = 0;
        //Negro a la derecha y abajo
        if (((pixels[i * height + j - 1] == Color.black)) && ((pixels[(i - 1) * height + j] == Color.black)))
            rotation = 180;
        //Negro arriba y a la izquierda
        if (((pixels[i * height + j - 1] == Color.black)) && ((pixels[(i + 1) * height + j] == Color.black)))
            rotation = -90;
        if (((pixels[i * height + j + 1] == Color.black)) && ((pixels[(i - 1) * height + j] == Color.black)))
            rotation = 90;
        return rotation;
    }

}
