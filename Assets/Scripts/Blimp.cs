using UnityEngine;
using System.Collections;
using InControl;


public enum ItemType {
	DUMP,
	BOOST,
	HEAVY,
	NONE
};

public class Blimp : MonoBehaviour {

	Rigidbody rb;
	public GameObject junk;
	public GameObject Bucket;
	public float movementSpeed;
	public int playerNum;
	int fireCooldown = 0;
	public int projectileCharge = 0;
	public float chargeCap;
	public float projectileSpeed;
	public float projectileOffset;
	public float stabilizationScale;
	public ItemType currItem = ItemType.NONE;
	
	private GameObject WeightBar;
	public GameObject WeightBarPrefab;
	private GameObject ChargeBar;
	public GameObject ChargeBarPrefab;
	//private GameObject ItemStatus;

	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody>();
		ChargeBar = (GameObject)Instantiate (ChargeBarPrefab);
		WeightBar = (GameObject)Instantiate (WeightBarPrefab);

		if (playerNum == 0) {
			ChargeBar.GetComponent<ChargeGauge> ().pos = new Vector2 (220, 970);
			WeightBar.GetComponent<ChargeGauge> ().pos = new Vector2 (220, 950);
		} 
		else {
			ChargeBar.GetComponent<ChargeGauge> ().pos = new Vector2 (1500, 970);
			WeightBar.GetComponent<ChargeGauge> ().pos = new Vector2 (1500, 950);
		}
		ChargeBar.GetComponent<ChargeGauge> ().color = Color.yellow;
		ChargeBar.GetComponent<ChargeGauge> ().charge = projectileCharge;
		ChargeBar.GetComponent<ChargeGauge> ().max = chargeCap;
		WeightBar.GetComponent<ChargeGauge> ().color = Color.green;
		WeightBar.GetComponent<ChargeGauge> ().charge = Bucket.GetComponent<Rigidbody>().mass;
		WeightBar.GetComponent<ChargeGauge> ().max = 5;

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
		float WeightProportion = Bucket.GetComponent<Rigidbody> ().mass/5.0f;
		WeightBar.GetComponent<ChargeGauge> ().color = Color.Lerp(Color.green, Color.red, WeightProportion);
		WeightBar.GetComponent<ChargeGauge> ().charge = Bucket.GetComponent<Rigidbody>().mass;
	}

	void ItemInput (bool trigger, ItemType item) {
		if (trigger) {
			if (item == ItemType.BOOST) {

			}
			if (item == ItemType.DUMP) {
				
			}
			if (item == ItemType.HEAVY) {
				
			}
			item = ItemType.NONE;
		}
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
		ItemInput (inputDevice.Action2, currItem);
	}

	void OnTriggerEnter(Collider col) {
		if (col.gameObject.tag == "Item") {
			currItem = col.gameObject.GetComponent<Item>().item;
			Destroy(col.gameObject);
		}
	}

	void Stabilize() {
		if (Mathf.Abs (transform.rotation.z) > 0.1)
			rb.AddTorque (new Vector3(0, 0, transform.rotation.z * stabilizationScale));
	}
}
