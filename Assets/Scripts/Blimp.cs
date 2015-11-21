using UnityEngine;
using System.Collections;

public class Blimp : MonoBehaviour {

	Rigidbody rb;
	public GameObject junk;
	public float movementSpeed;

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

	void FireInput() {
		bool fire = false;
		fire = Input.GetButtonDown(fireButton); 
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
		MovementInput();
		FireInput();
	}
}
