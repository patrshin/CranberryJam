using UnityEngine;
using System.Collections;
using InControl;

public class Blimp : MonoBehaviour {

	Rigidbody rb;
	public GameObject junk;
	public float movementSpeed;
	public int playerNum;
	int fireCooldown = 0;
	public float projectileSpeed;

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

	void FireInput(bool fire) {
		var inputDevice = (playerNum == 1) ? InputManager.Devices[1]: InputManager.Devices[0];
		if(fire && fireCooldown < 1){
			fireCooldown = 60;
			Transform turret = this.gameObject.transform.GetChild(0);
			float xd = inputDevice.RightStickX;
			float yd = inputDevice.RightStickY;
			GameObject projectile = Instantiate(junk) as GameObject;
			float angle = turret.rotation.z;
			Debug.Log(angle);
			projectile.transform.position = transform.position + new Vector3(0.5f * Mathf.Cos(angle), 0.5f * Mathf.Sin(angle),0);
			//Rigidbody projRB = projectile.GetComponent<Rigidbody>();
			Debug.Log (xd + ", " + yd);
			projectile.GetComponent<Rigidbody>().AddForce(new Vector3(xd, yd, 0) * projectileSpeed);
			Physics.IgnoreCollision(projectile.GetComponent<Collider>(), GetComponent<Collider>());
		}
	}
	
	// Update is called once per frame
	void Update () {
		var inputDevice = (playerNum == 1) ? InputManager.Devices[1]: InputManager.Devices[0];
		float xd = inputDevice.LeftStickX;
		float yd = inputDevice.LeftStickY;
		rb.AddForce(new Vector2(xd, yd) * movementSpeed);
		fireCooldown--;
		FireInput(inputDevice.Action1);
	}
}
