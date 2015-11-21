using UnityEngine;
using System.Collections;

public class Blimp : MonoBehaviour {

	Rigidbody2D rb;
	public float movementSpeed;

	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
	void Update () {
		float xd = Input.GetAxis("X Axis");
		float yd = Input.GetAxis("Y Axis");
		rb.AddForce(new Vector2(xd, yd) * 50);
	}
}
