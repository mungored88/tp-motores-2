using System.Collections;
using System.Collections.Generic;
using UnityEditor.AnimatedValues;
using UnityEngine;
using UnityEditor;

public class MapsGeneratorFM : EditorWindow
{
    int i = 0;
    AnimBool animBool;

    public Transform trans;
    public float TileDimension = 4f;

    public GameObject prefabFloor;
    public GameObject prefabWall;
    public GameObject prefabCorner;
    public GameObject prefabCollumn;
    public GameObject prefabFullCollumn;
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
        animBool = new AnimBool(false); //Creación de nuestra clase para poder animar cosas.
        animBool.valueChanged.AddListener(Repaint);
    }

    private void OnGUI()
    {
        EditorGUILayout.Space();
        animBool.target = EditorGUILayout.Foldout(animBool.target, "- Tutorial"); //flecha para ocultar cosas
        if (animBool.target == true)
        {
            EditorGUILayout.BeginFadeGroup(animBool.faded); //Esto es para agregarle animación a nuestro foldOut

            GUI.DrawTexture(GUILayoutUtility.GetRect(80, 120), (Texture)Resources.Load("Mini Tutorial"), ScaleMode.ScaleToFit);

            if (GUI.Button(GUILayoutUtility.GetRect(20, 20), "More info"))
            {
                GetWindow(typeof(Tutotial)).Show();
            }
            EditorGUILayout.EndFadeGroup();
        }

        EditorGUILayout.Space();
        GUILayout.Label("- Insert Prefabs", EditorStyles.boldLabel);

            GUILayout.BeginHorizontal();
            GUILayout.Label(" White || Floor", GUILayout.Width(150), GUILayout.ExpandWidth(true));
            prefabFloor = EditorGUILayout.ObjectField(prefabFloor, typeof(GameObject), true) as GameObject;
            GUILayout.EndHorizontal();

            GUILayout.BeginHorizontal();
            GUILayout.Label(" Red || Wall", GUILayout.Width(150), GUILayout.ExpandWidth(true));
            prefabWall = EditorGUILayout.ObjectField(prefabWall, typeof(GameObject), true) as GameObject;
            GUILayout.EndHorizontal();

            GUILayout.BeginHorizontal();
            GUILayout.Label(" Green || Outer Corner", GUILayout.Width(150), GUILayout.ExpandWidth(true));
            prefabCorner = EditorGUILayout.ObjectField(prefabCorner, typeof(GameObject), true) as GameObject;
            GUILayout.EndHorizontal();

            GUILayout.BeginHorizontal();
            GUILayout.Label(" Blue || Collumn", GUILayout.Width(150), GUILayout.ExpandWidth(true));
            prefabCollumn = EditorGUILayout.ObjectField(prefabCollumn, typeof(GameObject), true) as GameObject;
            GUILayout.EndHorizontal();

            GUILayout.BeginHorizontal();
            GUILayout.Label(" Cyan || Center Collumn", GUILayout.Width(150), GUILayout.ExpandWidth(true));
            prefabCenterC = EditorGUILayout.ObjectField(prefabCenterC, typeof(GameObject), true) as GameObject;
            GUILayout.EndHorizontal();

            GUILayout.BeginHorizontal();
            GUILayout.Label(" Magenta || Full Collumn", GUILayout.Width(150), GUILayout.ExpandWidth(true));
            prefabFullCollumn = EditorGUILayout.ObjectField(prefabFullCollumn, typeof(GameObject), true) as GameObject;
            GUILayout.EndHorizontal();

        EditorGUILayout.Space();
        GUILayout.Label("- Insert Map Blueprint", EditorStyles.boldLabel);
        Map2D = EditorGUILayout.ObjectField(Map2D, typeof(Texture2D), true) as Texture2D;
        
        EditorGUILayout.Space();
        EditorGUILayout.Space();
        if (GUILayout.Button("Generate"))
        {
            if (!Map2D)
            {
                var window = (MapsGeneratorFM)GetWindow(typeof(MapsGeneratorFM));
                window.ShowNotification(new GUIContent("Missing slot"));
            }
            else EmptyObjectDad();
        }

        EditorGUILayout.Space();
        EditorGUILayout.Space();
        if (GUI.Button(GUILayoutUtility.GetRect(20, 20), "Make Prefab"))
        {
            GetWindow(typeof(CustomPrefab)).Show();
        }

    }

    void CheckEvent()
    {
        minSize = new Vector2(400, 350);
        maxSize = new Vector3 (400, 350);
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
                }
                if (pixelColor == Color.red)   //Wall
                {
                    GameObject inst = GameObject.Instantiate(prefabWall, trans);
                    inst.transform.position = new Vector3(j * multiplierFactor, 0, i * multiplierFactor);
                    inst.transform.Rotate(new Vector3(0, FindRotationW(pixels, i, j), 0), Space.Self);
                    inst.transform.parent = level.transform;
                }
                if (pixelColor == Color.green) //Corner
                {
                    GameObject inst = GameObject.Instantiate(prefabCorner, trans);
                    inst.transform.position = new Vector3(j * multiplierFactor, 0, i * multiplierFactor);
                    inst.transform.Rotate(new Vector3(0, FindRotationL(pixels, i, j), 0), Space.Self);
                    inst.transform.parent = level.transform;
                }
                if (pixelColor == Color.blue) //Collumn
                {
                    GameObject inst = GameObject.Instantiate(prefabCollumn, trans);
                    inst.transform.position = new Vector3(j * multiplierFactor, 0, i * multiplierFactor);
                    inst.transform.Rotate(new Vector3(0, FindRotationC(pixels, i, j), 0), Space.Self);
                    inst.transform.parent = level.transform;
                }
                if (pixelColor == Color.cyan) //Center Collumn
                { 
                    GameObject inst = GameObject.Instantiate(prefabCenterC, trans);
                    inst.transform.position = new Vector3(j * multiplierFactor, 0, i * multiplierFactor);
                    inst.transform.parent = level.transform;
                }
                if (pixelColor == Color.magenta) //Full Collumn
                {
                    GameObject inst = GameObject.Instantiate(prefabFullCollumn, trans);
                    inst.transform.position = new Vector3(j * multiplierFactor, 0, i * multiplierFactor);
                    inst.transform.parent = level.transform;
                }
            }
        }
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
