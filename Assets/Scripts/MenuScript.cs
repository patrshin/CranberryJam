using UnityEngine;
using System.Collections;

public class MenuScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}

	public void StartGame() {
		Application.LoadLevel (1);
	}

	public void BackToMenu() {
		Application.LoadLevel (0);
	}

	public void QuitGame() {
		#if UNITY_EDITOR
		UnityEditor.EditorApplication.isPlaying = false;
		#endif
		Application.Quit ();
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
