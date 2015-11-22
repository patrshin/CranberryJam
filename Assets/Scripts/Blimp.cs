using UnityEngine;
using System.Collections;
using InControl;


public enum ItemType {
	DUMP,
	BOOST,
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
	private GameObject ItemStatus;
	public GameObject ItemStatusPrefab;

	private bool[] on = new bool[2];

	private float boostTimer = 0f;
	private float boostTimeLimit = 0.5f;

	// Use this for initialization
	void Start () {
		for (int i = 0; i < on.Length; i++) {
			on[0] = false;
		}
		rb = GetComponent<Rigidbody>();
		ChargeBar = (GameObject)Instantiate (ChargeBarPrefab);
		WeightBar = (GameObject)Instantiate (WeightBarPrefab);
		ItemStatus = (GameObject)Instantiate (ItemStatusPrefab);
		ItemStatus.GetComponent<ItemUI>().blimp = this.gameObject;

		if (playerNum == 0) {
			ChargeBar.GetComponent<ChargeGauge> ().pos = new Vector2 (220, 970);
			WeightBar.GetComponent<ChargeGauge> ().pos = new Vector2 (220, 950);
			ItemStatus.transform.position = new Vector3(-7.2f, -2.6f, -2.0f);
		} 
		else {
			ChargeBar.GetComponent<ChargeGauge> ().pos = new Vector2 (1500, 970);
			WeightBar.GetComponent<ChargeGauge> ().pos = new Vector2 (1500, 950);
			ItemStatus.transform.position = new Vector3(7.2f,2.6f, -2.0f);
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
			fireCooldown = 25;
			Turret turret = this.gameObject.GetComponentInChildren<Turret>();
			GameObject projectile = Instantiate(junk) as GameObject;
			projectile.transform.position = transform.position + (turret.transform.up * projectileOffset);
			projectile.GetComponent<Rigidbody>().AddForce(turret.transform.up * projectileCharge * projectileSpeed);
			projectile.GetComponent<ProjectileScript>().totalCharge = projectileCharge;
			projectileCharge = 0;
			projectile.GetComponent<ProjectileScript>().chargeCap = chargeCap;
			//Physics.IgnoreCollision(projectile.GetComponent<Collider>(), GetComponent<Collider>());
		}
		ChargeBar.GetComponent<ChargeGauge> ().charge = projectileCharge;
		//calculate weight proportion
		float WeightProportion = Bucket.GetComponent<Rigidbody> ().mass/5.0f;
		//Change particle emission rate based on weight proportion
		if (WeightProportion > 0.15f) {
			ParticleSystem[] emitters = GetComponentsInChildren<ParticleSystem> ();
			for (int i = 0; i < emitters.Length; i++) {
				Debug.Log (emitters [i]);
				emitters [i].emissionRate = 12 * WeightProportion;
			}
		}
		//update weight bar with weight proportion
		WeightBar.GetComponent<ChargeGauge> ().color = Color.Lerp(Color.green, Color.red, WeightProportion);
		WeightBar.GetComponent<ChargeGauge> ().charge = Bucket.GetComponent<Rigidbody>().mass;
	}

	void ItemInput (bool trigger, ItemType item, bool[] on) {
		if (trigger) {
			Debug.Log ("ITEM");
			if (currItem == ItemType.BOOST) {
				on[0] = true;
			}
			if (currItem == ItemType.DUMP) {
				on[1] = true;
			}
			currItem = ItemType.NONE;
		}
		if (on [0] == true && boostTimer < boostTimeLimit) {
			boostTimer += Time.deltaTime;
			rb.AddForce (new Vector2 (0f, 1f) * movementSpeed * 2f);
		} else if (on[0] == true && boostTimer > boostTimeLimit) {
			boostTimer = 0;
			on[0] = false;
		}
		if (on[1] == true) {
			if (Bucket.GetComponent<Rigidbody>().mass-.5f < .25f) {
				Bucket.GetComponent<Rigidbody>().mass = .25f;
			}
			else {
				Bucket.GetComponent<Rigidbody>().mass -= .5f;
			}
			GameObject projectile = Instantiate(junk) as GameObject;
			projectile.transform.position = new Vector3(Bucket.transform.position.x-0.65f, Bucket.transform.position.y+0.3f,0);
			projectile.GetComponent<Rigidbody>().AddForce(projectileCharge * projectileSpeed * Vector3.right);
			projectile.GetComponent<Rigidbody>().AddForce(projectileSpeed * Vector3.right * 2);
			projectile.GetComponent<Rigidbody>().AddForce(projectileSpeed * Vector3.left * 2);
			GameObject projectile1 = Instantiate(junk) as GameObject;
			projectile1.transform.position = new Vector3(Bucket.transform.position.x+0.65f, Bucket.transform.position.y+0.3f,0);
			projectile1.GetComponent<Rigidbody>().AddForce(projectileCharge * projectileSpeed * Vector3.right);
			projectile1.GetComponent<Rigidbody>().AddForce(projectileSpeed * Vector3.right * 2);
			projectile1.GetComponent<Rigidbody>().AddForce(projectileSpeed * Vector3.left * 2);
			on[1] = false;
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
		ItemInput (inputDevice.Action2, currItem, on);
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
