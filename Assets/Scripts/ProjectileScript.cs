using UnityEngine;
using System.Collections;

public class ProjectileScript : MonoBehaviour {
	
	Rigidbody rb;
	
	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	
	public void SetVelocity(Vector3 direction, float speed) {
		rb.AddForce(direction * speed);
	}
}
