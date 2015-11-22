using UnityEngine;
using System.Collections;

public class WeaponSpawner : MonoBehaviour {

	public GameObject junk;
	public float baseTimer;
	private float time;
	private float newTime;
	private float oscil = 5f;
	private float oscilTime = 0f;
	private int dir = 1;

	// Use this for initialization
	void Start () {
	
		time = Random.Range (0f, 14f);
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
		if (oscilTime > oscil) {
			dir *= -1;
			oscilTime = 0f;
		} else {
			oscilTime += Time.deltaTime;
		}
		transform.RotateAround(transform.position, Vector3.forward, 0.25f* dir);
	}

	void fire() {
		GameObject projectile = Instantiate(junk) as GameObject;
		projectile.GetComponent<AudioSource> ().volume = 0.5f;
		projectile.transform.position = transform.position;
		projectile.GetComponent<Rigidbody> ().AddForce (transform.up * Random.Range (100f, 400f));
		projectile.GetComponent<ProjectileScript>().totalCharge = Random.Range(0f,120f);
		projectile.GetComponent<ProjectileScript>().chargeCap = Random.Range(0f,100f);
	}
}
