using UnityEngine;
using System.Collections;
using InControl;

public class Blimp : MonoBehaviour {

	Rigidbody rb;
	public GameObject junk;
	public float movementSpeed;
	public int playerNum;
	int fireCooldown = 0;
	public int projectileCharge = 0;
	public float chargeCap;
	public float projectileSpeed;
	public float projectileOffset;
	public float stabilizationScale;

	private GameObject ChargeBar;
	public GameObject ChargeBarPrefab;

	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody>();
		ChargeBar = (GameObject)Instantiate (ChargeBarPrefab);
		if (playerNum == 0) {
			ChargeBar.GetComponent<ChargeGauge> ().pos = new Vector2 (220, 970);
		} 
		else {
			ChargeBar.GetComponent<ChargeGauge> ().pos = new Vector2 (1500, 970);
		}
		ChargeBar.GetComponent<ChargeGauge> ().charge = projectileCharge;
		ChargeBar.GetComponent<ChargeGauge> ().max = chargeCap;
	}

	void FireInput(bool buttonPressed) {
		if (buttonPressed && projectileCharge < chargeCap) {
			projectileCharge++;
		} else if(!buttonPressed && projectileCharge > 0 && fireCooldown < 1){
			fireCooldown = 15;
			Turret turret = this.gameObject.GetComponentInChildren<Turret>();
			GameObject projectile = Instantiate(junk) as GameObject;
			projectile.transform.position = transform.position + (turret.transform.up * projectileOffset);
			projectile.GetComponent<Rigidbody>().AddForce(turret.transform.up * projectileCharge * projectileSpeed);
			projectileCharge = 0;
			//Physics.IgnoreCollision(projectile.GetComponent<Collider>(), GetComponent<Collider>());
		}
		ChargeBar.GetComponent<ChargeGauge> ().charge = projectileCharge;
	}
	
	// Update is called once per frame
	void Update () {
		var inputDevice = (playerNum == 1) ? InputManager.Devices[1]: InputManager.Devices[0];
		if (inputDevice == null) {
			Destroy(this.gameObject);
		}
		float xd = inputDevice.LeftStickX;
		float yd = inputDevice.LeftStickY;
		rb.AddForce(new Vector2(xd, yd) * movementSpeed);
		fireCooldown--;
		FireInput(inputDevice.RightBumper || Input.GetMouseButton(0));
		Stabilize ();
	}

	void Stabilize() {
		Debug.Log (transform.rotation.z);
		if (Mathf.Abs (transform.rotation.z) > 0.1)
			rb.AddTorque (new Vector3(0, 0, transform.rotation.z * stabilizationScale));
	}
}
