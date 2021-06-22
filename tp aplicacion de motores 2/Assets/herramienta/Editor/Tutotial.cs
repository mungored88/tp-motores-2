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
        GUILayout.Label("Instrucciones:", EditorStyles.boldLabel);
        GUILayout.Label("1- Inserta los prefab en la ranura Prefab de cada color");
        GUILayout.Label("2- Inserta una textura del mapa deseado en la ranura Map blueprint");
        GUILayout.Label("3- Pulsa Generar para visualizar tu mapa");
        EditorGUILayout.Space();
        EditorGUILayout.Space();
        GUILayout.Label("Creador de Prefab:", EditorStyles.boldLabel);
        GUILayout.Label("1- Arrastre el nivel a la ventana de prefab");
        GUILayout.Label("2- Cambie el nombre del prefab a su voluntad");
        GUILayout.Label("3- Pulsa Create Prefab para guardar su prefab");
        GUILayout.Label("Nota:", EditorStyles.boldLabel);
        GUILayout.Label("Puede crear una carpeta y guardar su prefab en ese lugar");
        GUILayout.Label("1- Escriba el nombre de la carpeta");
        GUILayout.Label("2- puede crear un Path si desea ubicarlo dentro de otra carpeta");
        GUILayout.Label("3- Pulse el boton Crear Carpeta");

    }
}
