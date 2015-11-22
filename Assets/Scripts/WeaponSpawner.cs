using UnityEngine;
using System.Collections;

public class WeaponSpawner : MonoBehaviour {

	public GameObject junk;
	public float baseTimer;
	private float time;
	private float newTime;

	// Use this for initialization
	void Start () {
		time = Random.Range (0f, 3f);
		newTime = Random.Range (baseTimer - 1f, baseTimer + 1f);
	}
	
	// Update is called once per frame
	void Update () {
		if (time < newTime) {
			time += Time.deltaTime;
		} else {
			fire ();
			time = 0;
			newTime = Random.Range (baseTimer - 1f, baseTimer + 1f);
		}
	}

	void fire() {
		GameObject projectile = Instantiate(junk) as GameObject;
		projectile.transform.position = transform.position;
		projectile.GetComponent<Rigidbody> ().AddForce (transform.up * Random.Range (5f, 10f));
		projectile.GetComponent<ProjectileScript>().totalCharge = Random.Range(0f,120f);
	}
}
