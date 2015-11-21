﻿using UnityEngine;
using System.Collections;
using InControl;

public class Blimp : MonoBehaviour {

	Rigidbody rb;
	public GameObject junk;
	public float movementSpeed;
	public int playerNum;

	public string moveXAxis;
	public string moveYAxis;
	public string fireXAxis;
	public string fireYAxis;
	public string fireButton;

	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody>();
	}

	void MovementInput() {
		float xd = Input.GetAxis(moveXAxis);
		float yd = -Input.GetAxis(moveYAxis);
		rb.AddForce(new Vector2(xd, yd) * movementSpeed);
	}

	void FireInput(bool fired) {
		bool fire = false;
		fire = fired; 
		if(fire){
			float xd = Input.GetAxis(fireXAxis);
			float yd = -Input.GetAxis(fireYAxis);
			GameObject projectile = Instantiate(junk) as GameObject;
			projectile.transform.position = transform.position;
			//Rigidbody projRB = projectile.GetComponent<Rigidbody>();
			Debug.Log (xd + ", " + yd);
			projectile.GetComponent<Rigidbody>().AddForce(xd, yd, 0);
		}
	}
	
	// Update is called once per frame
	void Update () {
		var inputDevice = (playerNum == 1) ? InputManager.Devices[1]: InputManager.Devices[0];
		float xd = inputDevice.LeftStickX;
		float yd = inputDevice.LeftStickY;
		rb.AddForce(new Vector2(xd, yd) * movementSpeed);
		FireInput(inputDevice.Action1);
	}
}
