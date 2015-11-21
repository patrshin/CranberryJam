using UnityEngine;
using System.Collections;

public class Blimp : MonoBehaviour {

	Rigidbody rb;
	public GameObject junk;
	public int playerNumber;
	public float movementSpeed;

	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody>();
	}

	void MovementInput() {
		if(playerNumber == 1) {
			float xd = Input.GetAxis("p1_x");
			float yd = -Input.GetAxis("p1_y");
			rb.AddForce(new Vector2(xd, yd) * movementSpeed);
		} else if (playerNumber == 2) {
			float xd = Input.GetAxis("p2_x");
			float yd = -Input.GetAxis("p2_y");
			rb.AddForce(new Vector2(xd, yd) * movementSpeed);
		}
	}

	void FireInput() {
		bool fire = false;
		if(playerNumber == 1) {
			fire = Input.GetButton("Fire2");
		} else if(playerNumber == 2) { 
			fire = Input.GetButton("Fire1");
		}
		if(fire){
			float xd = 0;
			float yd = 0;
			if(playerNumber == 1) {
				xd = Input.GetAxis("p1_fire_x");
				yd = -Input.GetAxis("p1_fire_y");
			} else if (playerNumber == 2) {
				xd = Input.GetAxis("p2_fire_x");
				yd = -Input.GetAxis("p2_fire_y");
			}
			GameObject projectile = Instantiate(junk) as GameObject;
			projectile.transform.position = transform.position;
			//Rigidbody projRB = projectile.GetComponent<Rigidbody>();
			projectile.GetComponent<Rigidbody>().AddForce(xd, yd, 0);
		}
	}
	
	// Update is called once per frame
	void Update () {
		MovementInput();
		FireInput();
	}
}
