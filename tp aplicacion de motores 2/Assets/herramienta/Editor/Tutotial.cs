using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class Tutotial : EditorWindow
{
    private void OnGUI()
    {
        GUI.DrawTexture(GUILayoutUtility.GetAspectRect(1), (Texture)Resources.Load("Tutorial Completo"), ScaleMode.ScaleToFit);
        CheckEvent();
    }
    void CheckEvent()
    {
        minSize = new Vector2(600, 600);
        maxSize = new Vector3(600, 600);
    }
}
