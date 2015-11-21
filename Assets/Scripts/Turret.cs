using UnityEngine;
using System.Collections;
using InControl;

public class Turret : MonoBehaviour {

	public int playerNum;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		var inputDevice = (playerNum == 1) ? InputManager.Devices[1]: InputManager.Devices[0];

	}
}
