using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class MapsGeneratorFM : EditorWindow
{
    //Color color;
    Material textura;
    Vector3 position;
    //Vector3 _lastPosition;
    private bool newPosition;
    GameObject GoPrefab;

    Texture2D Map2D;

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
        GUILayout.Label("- Insert Map Texture", EditorStyles.boldLabel);
        Map2D = EditorGUILayout.ObjectField(Map2D, typeof(Texture2D), true) as Texture2D;
        EditorGUILayout.Space();
        EditorGUILayout.Space();

        //color = EditorGUILayout.ColorField("Color", color);
        //if (GUILayout.Button("color select"))
        //{
        //    ColorChange();
        //}
        GUILayout.Label("- Insert Material", EditorStyles.boldLabel);
        textura = EditorGUILayout.ObjectField(textura, typeof(Material), true) as Material;

        EditorGUILayout.Space();

        if (newPosition)
        {
            GUILayout.Label("- Insert Inicial Position", EditorStyles.boldLabel);
            position = EditorGUILayout.Vector3Field("Posicion", position);
            
            if (GUILayout.Button("Generar"))
            {
                PlaneSpawner();
            }
        }
        //else
        //{
        //    if (GUILayout.Button("Generar a la derecha"))
        //        PlaneSpawnerNextPosition(new Vector3(10, 0, 0));

        //    if (GUILayout.Button("Generar a la izquierda"))
        //        PlaneSpawnerNextPosition(new Vector3(-10, 0, 0));

        //    if (GUILayout.Button("Generar abajo"))
        //        PlaneSpawnerNextPosition(new Vector3(0, 0, -10));

        //    if (GUILayout.Button("Generar arriba"))
        //        PlaneSpawnerNextPosition(new Vector3(0, 0, 10));
        //}

        if (GUI.Button(GUILayoutUtility.GetRect(20, 20), "Prefab"))
        {
            GetWindow(typeof(CustomPrefab)).Show();
        }

        //EditorGUILayout.Space();
        //GUILayout.Label("Generar multible editable", EditorStyles.boldLabel);
        //EditorGUILayout.BeginHorizontal();
        //newPosition = EditorGUILayout.Toggle(newPosition);
        //GUILayout.Label("On/Off", EditorStyles.boldLabel);
        //EditorGUILayout.EndHorizontal();

    }
    void CheckEvent()
    {
        minSize = new Vector2(300, 300);
        maxSize = new Vector3 (300, 300);
    }
    private void PlaneSpawner()
    {
        if (GoPrefab == null)
        {
            GoPrefab = new GameObject("map");
        }
        GameObject plane = GameObject.CreatePrimitive(PrimitiveType.Plane);
        plane.transform.position = position;
        plane.transform.parent = GoPrefab.transform;
        //_lastPosition = position;
        plane.GetComponent<MeshRenderer>().material = textura;
        MaterialChange();
    }
    //void ColorChange()
    //{
    //    foreach (GameObject obj in Selection.gameObjects)
    //    {
    //        Renderer renderer = obj.GetComponent<Renderer>();

    //        if (renderer != null)
    //        {
    //            renderer.sharedMaterial.color = color;
    //        }
    //    }
    //}
    void MaterialChange()
    {
        foreach (GameObject obj in Selection.gameObjects)
        {
            Material mat = obj.GetComponent<Material>();
          
        }
    }
    //void PlaneSpawnerNextPosition(Vector3 changePos)
    //{
    //    if (GoPrefab == null)
    //    {
    //        GoPrefab = new GameObject("map");
    //    }
    //    GameObject plane = GameObject.CreatePrimitive(PrimitiveType.Plane);
    //    _lastPosition += changePos;
    //    plane.transform.position = _lastPosition;
    //    plane.transform.parent = GoPrefab.transform;
    //    plane.GetComponent<MeshRenderer>().material = textura;
    //    MaterialChange();
    //}

}
