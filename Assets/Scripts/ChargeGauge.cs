using UnityEngine;
using System.Collections;

public class ChargeGauge : MonoBehaviour {

	public float barDisplay; //current progress
	public Vector2 pos;
	public Vector2 size = new Vector2(200,15);
	public Texture2D emptyTex;
	public Texture2D fullTex;
	public float charge; 
	public float max;
	public Color color;
	
	float native_width = 1920f;
	float native_height = 1080f;
	
	void OnGUI() {
		GUIStyle myStyle = new GUIStyle(GUI.skin.box);
		myStyle.normal.background = fullTex;
		float rx = Screen.width / native_width;
		float ry = Screen.height / native_height;
		GUI.matrix = Matrix4x4.TRS(new Vector3(0, 0, 0), Quaternion.identity, new Vector3 (rx, ry, 1f));
		//now create your GUI normally, as if you were in your native resolution
		//The GUI.matrix will scale everything automatically.
		
		//draw the background:
		GUI.color = Color.black;
		
		GUI.BeginGroup(new Rect(pos.x, pos.y, size.x, size.y));
		GUI.Box(new Rect(0,0, size.x, size.y), emptyTex);
		GUI.color = color;
		//draw the filled-in part:
		GUI.BeginGroup(new Rect(0,0, size.x * barDisplay, size.y));
		GUI.Box(new Rect(0,0, size.x, size.y), emptyTex, myStyle);
		
		GUI.EndGroup();
		GUI.EndGroup();
	}
	void Update() {

		barDisplay = (charge)/max;
	}
}
