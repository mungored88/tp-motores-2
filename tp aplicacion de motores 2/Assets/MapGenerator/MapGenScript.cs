using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGenScript : MonoBehaviour {

    public Transform trans;
	public float TileDimension=4f;

    public GameObject prefabFloor;
    public GameObject prefabWall;
    public GameObject prefabCurveL;
	public GameObject prefabCollumn;
    public Texture2D Map;

    private int width;
    private int height;

    
    public void PressButon() {
        GenerateMap();
    }

    private void GenerateMap() {
		float multiplierFactor = TileDimension + float.Epsilon;
		width = Map.width;
		height = Map.height;
		Color[] pixels = Map.GetPixels();
        for (int i = 0; i < height; i++) {
            for (int j = 0; j < width; j++) {
                Color pixelColor = pixels[i * height + j];
				if (pixelColor == Color.white) { //Floor
                    GameObject inst = GameObject.Instantiate(prefabFloor, trans);
                    inst.transform.position = new Vector3(j * multiplierFactor, 0, i * multiplierFactor);
                }
                if (pixelColor == Color.red) //Wall
                {
                    GameObject inst = GameObject.Instantiate(prefabWall, trans);
                    inst.transform.position = new Vector3(j * multiplierFactor, 0, i * multiplierFactor);
                    inst.transform.Rotate(new Vector3(0, FindRotationW(pixels, i, j), 0), Space.Self);
                }
                if (pixelColor == Color.green) //Retangular L Curve
                {
                    GameObject inst = GameObject.Instantiate(prefabCurveL, trans);
                    inst.transform.position = new Vector3(j * multiplierFactor, 0, i * multiplierFactor);
                    inst.transform.Rotate(new Vector3(0, FindRotationL(pixels, i, j), 0), Space.Self);
                }
				if (pixelColor == Color.blue) //Collumn
				{
                    GameObject inst = GameObject.Instantiate(prefabCollumn, trans);
                    inst.transform.position = new Vector3(j * multiplierFactor, 0, i * multiplierFactor);
                    inst.transform.Rotate(new Vector3(0, FindRotationC(pixels, i, j), 0), Space.Self);
                }
            }
        }
    }

    //rotacion pared
	private float FindRotationW(Color[] pixels, int i, int j) {
        
		float Rotation = 0f;
		
		if (i-1>=0 && (pixels[(i-1)*height + j]== Color.black)){
			Rotation = 90f;
		}
		
		else if (j-1>=0 && (pixels[i*height + (j-1)]==Color.black)){
			Rotation = 180f;
		}
		else if (i+1<height && (pixels[(i+1)*height + j]==Color.black)){
			Rotation = -90f;
		}
		return Rotation;
    }

    //rotacion columna
    private float FindRotationC(Color[] pixels, int i, int j) {
		
		float Rotation = 0f;
		
		if(i-1>=0 && j+1<width && (pixels[(i-1)*height+(j+1)]==Color.black))
			Rotation = 90f;
		
		else if (i-1>=0 && j-1>=0 && (pixels[(i-1)*height+(j-1)]==Color.black))
			Rotation = 180f;
		else if (i+1<height && j-1>=0 && (pixels[(i+1)*height+(j-1)]==Color.black))
			Rotation = -90f;
		return Rotation;
	}

    //Rotacion esquinero
	private float FindRotationL(Color[] pixels, int i, int j) {
		//posicion por default
		float rotation = 0;
		//Negro a la derecha y abajo
		if (((pixels[i * height + j - 1] == Color.black)) && ((pixels[(i - 1) * height + j] == Color.black)))
			rotation = 180;
		//Negro arriba y a la izquierda
		if (((pixels[i * height + j - 1] == Color.black)) && ((pixels[(i+1) * height + j] == Color.black)))
			rotation = -90;
		if (((pixels[i * height + j + 1] == Color.black)) && ((pixels[(i - 1) * height + j] == Color.black)))
			rotation = 90;
		return rotation;
	}
}
