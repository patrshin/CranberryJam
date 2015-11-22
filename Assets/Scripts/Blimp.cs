using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using InControl;


public enum ItemType {
	DUMP,
	BOOST,
	CANNON,
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
	private GameObject AbilityStatus;
	public GameObject AbilityStatusPrefab;

	private bool[] on = new bool[3];
	private bool abilityOn = false;

	private float boostCooldownTimer = 0f;
	public float boostCooldown = 10f;
	private float boostTimer = 0f;
	public float boostTimeLimit = 0.5f;
	private float cannonTimer = 0f;
	public float cannonTimeLimit = 2f;
	private float cannonMode = 0f;

	// Use this for initialization
	void Start () {
		for (int i = 0; i < on.Length; i++) {
			on[i] = false;
		}
		rb = GetComponent<Rigidbody>();
		ChargeBar = (GameObject)Instantiate (ChargeBarPrefab);
		WeightBar = (GameObject)Instantiate (WeightBarPrefab);
		ItemStatus = (GameObject)Instantiate (ItemStatusPrefab);
		ItemStatus.GetComponent<ItemUI>().blimp = this.gameObject;
		AbilityStatus = (GameObject)Instantiate (AbilityStatusPrefab);
		AbilityStatus.GetComponent<AbilityUI>().blimp = this.gameObject;


		if (playerNum == 0) {
			//ChargeBar.GetComponent<ChargeGauge> ().pos = new Vector2 (170, 990);
			//WeightBar.GetComponent<ChargeGauge> ().pos = new Vector2 (170, 950);
			ItemStatus.transform.position = new Vector3(-4.5f, 2.4f, 0);
			AbilityStatus.transform.position = new Vector3(-3.5f, 2.4f, 0);
		} 
		else {
			//ChargeBar.GetComponent<ChargeGauge> ().pos = new Vector2 (1550, 990);
			//WeightBar.GetComponent<ChargeGauge> ().pos = new Vector2 (1550, 950);
			ItemStatus.transform.position = new Vector3(4.5f, 2.4f, 0);
			AbilityStatus.transform.position = new Vector3(3.5f, 2.4f, 0);
		}
		ChargeBar.GetComponent<ChargeGauge> ().color = Color.yellow;
		ChargeBar.GetComponent<ChargeGauge> ().charge = projectileCharge;
		ChargeBar.GetComponent<ChargeGauge> ().max = chargeCap;
		WeightBar.GetComponent<ChargeGauge> ().color = Color.green;
		WeightBar.GetComponent<ChargeGauge> ().charge = Bucket.GetComponent<Rigidbody>().mass;
		WeightBar.GetComponent<ChargeGauge> ().max = 5;
		foreach (Transform child in AbilityStatus.transform)  
		{  
			child.transform.Find("Image").GetComponent<Image>().fillAmount = 1;
		}
		
	}

	void FireInput(bool buttonPressed) {
		if (buttonPressed && projectileCharge < chargeCap) {
			projectileCharge++;
		} else if(!buttonPressed && projectileCharge > 0 && fireCooldown < 1){
			fireCooldown = 15;
			Turret turret = this.gameObject.GetComponentInChildren<Turret>();
			GameObject projectile = Instantiate(junk) as GameObject;
			projectile.transform.position = transform.position + (turret.transform.up * projectileOffset);
			Debug.Log(cannonMode);
			if (cannonMode == 1.0f) {
				projectile.GetComponent<Rigidbody>().AddForce(turret.transform.up * chargeCap * projectileSpeed);
				projectile.GetComponent<ProjectileScript>().totalCharge = chargeCap;
			}
			else {
				projectile.GetComponent<Rigidbody>().AddForce(turret.transform.up * projectileCharge * projectileSpeed);
				projectile.GetComponent<ProjectileScript>().totalCharge = projectileCharge;
			}
			projectileCharge = 0;
			projectile.GetComponent<ProjectileScript>().chargeCap = chargeCap;
			//Physics.IgnoreCollision(projectile.GetComponent<Collider>(), GetComponent<Collider>());
		}
		ChargeBar.GetComponent<ChargeGauge> ().charge = projectileCharge;
		//calculate weight proportion
		float WeightProportion = Bucket.GetComponent<Rigidbody> ().mass/5.0f;
		//Change particle emission rate based on weight proportion
		if (WeightProportion > 0.5f) {
			ParticleSystem[] emitters = GetComponentsInChildren<ParticleSystem> ();
			for (int i = 0; i < emitters.Length; i++) {
				emitters [i].emissionRate = 20 * (WeightProportion - 0.5f); 
			}
		}
		//update weight bar with weight proportion
		WeightBar.GetComponent<ChargeGauge> ().color = Color.Lerp(Color.green, Color.red, WeightProportion);
		WeightBar.GetComponent<ChargeGauge> ().charge = Bucket.GetComponent<Rigidbody>().mass;
	}

	void ItemInput (bool trigger, ItemType item, bool[] on) {
		if (trigger) {
			if (currItem == ItemType.BOOST) {
				on[0] = true;
			}
			if (currItem == ItemType.DUMP) {
				on[1] = true;
			}
			if (currItem == ItemType.CANNON) {
				on[2] = true;
			}
		}
		if (on [0] == true) {
			currItem = ItemType.NONE;
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
			projectile.GetComponent<Rigidbody>().AddForce(projectileSpeed * Vector3.left*2f);
			projectile.GetComponent<ProjectileScript>().chargeCap = 10f;
			projectile.GetComponent<ProjectileScript>().off = 0f;
			GameObject projectile1 = Instantiate(junk) as GameObject;
			projectile1.transform.position = new Vector3(Bucket.transform.position.x+0.65f, Bucket.transform.position.y+0.3f,0);
			projectile1.GetComponent<ProjectileScript>().chargeCap = 10f;
			projectile1.GetComponent<ProjectileScript>().off = 0f;
			projectile1.GetComponent<Rigidbody>().AddForce(projectileSpeed * Vector3.right*2f);
			on[1] = false;
			currItem = ItemType.NONE;
		}
		if (on [2] == true && cannonTimer < cannonTimeLimit) {
			cannonTimer += Time.deltaTime;
			cannonMode = 1.0f;
		} else if (on[2] == true && cannonTimer >= cannonTimeLimit) {
			cannonMode = 0.0f;
			cannonTimer = 0;
			on[2] = false;
			currItem = ItemType.NONE;
		}
	}

	void AbilityInput (bool trigger) {
		if (abilityOn == false && boostCooldownTimer < boostCooldown) {
			boostCooldownTimer += Time.deltaTime;
			foreach (Transform child in AbilityStatus.transform)  
			{  
				child.transform.Find("Image").GetComponent<Image>().fillAmount = ((boostCooldown-boostCooldownTimer)/boostCooldown);
			}
		}
		if (trigger && boostCooldownTimer >= boostCooldown) {
			abilityOn = true;
		} 
		if (abilityOn == true && boostTimer < boostTimeLimit) {
			if (!AbilityStatus.GetComponent<AudioSource>().isPlaying) {
				AbilityStatus.GetComponent<AudioSource>().Play(); 
			}
			boostTimer += Time.deltaTime;
			rb.AddForce (new Vector2 (0f, 1f) * movementSpeed * 2f);
		} else if (abilityOn == true && boostTimer >= boostTimeLimit) {
			AbilityStatus.GetComponent<AudioSource>().Stop ();
			boostTimer = 0;
			abilityOn = false;
			currItem = ItemType.NONE;
			boostCooldownTimer = 0f;
			foreach (Transform child in AbilityStatus.transform)  
			{  
				child.transform.Find("Image").GetComponent<Image>().fillAmount = 1;
			}
		} 
	}
	
	// Update is called once per frame
	void Update () {
		var inputDevice = (playerNum == 1) ? InputManager.Devices[1]: InputManager.Devices[0];
		Vector3 guiPos = transform.position;
		guiPos.y *= -1;
		WeightBar.GetComponent<ChargeGauge> ().pos = Camera.main.WorldToScreenPoint (guiPos + new Vector3(0, -0.75f, 0));
		ChargeBar.GetComponent<ChargeGauge> ().pos = Camera.main.WorldToScreenPoint (guiPos + new Vector3(0, -0.55f, 0));
		WeightBar.GetComponent<ChargeGauge>().pos.x -= WeightBar.GetComponent<ChargeGauge> ().size.x / 2;
		ChargeBar.GetComponent<ChargeGauge>().pos.x -= ChargeBar.GetComponent<ChargeGauge> ().size.x / 2;
		if (inputDevice == null) {
			Destroy(this.gameObject);
		}
		float xd = inputDevice.LeftStickX;
		float yd = inputDevice.LeftStickY;
		rb.AddForce(new Vector2(xd, yd) * movementSpeed);
		fireCooldown--;
		FireInput(inputDevice.RightBumper || Input.GetMouseButton(0));
		Stabilize ();
		ItemInput (inputDevice.LeftTrigger, currItem, on);
		AbilityInput (inputDevice.RightTrigger);
	}

	void OnTriggerEnter(Collider col) {
		if (col.gameObject.tag == "Item") {
			currItem = col.gameObject.GetComponent<Item>().item;
			for (int i = 0; i < on.Length; i++) {
				on[i] = false;
			}
			cannonTimer = 0f;
			cannonMode = 0f;
			Destroy(col.gameObject);
		}
	}

	void Stabilize() {
		if (Mathf.Abs (transform.rotation.z) > 0.1)
			rb.AddTorque (new Vector3(0, 0, transform.rotation.z * stabilizationScale));
	}
}
