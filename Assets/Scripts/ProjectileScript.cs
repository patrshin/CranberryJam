using UnityEngine;
using System.Collections;

public class ProjectileScript : MonoBehaviour {
	
	Rigidbody rb;
	
	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody>();
		//rb.velocity = Vector3.forward;
	}
	
	// Update is called once per frame
	void Update () {
		Vector3 viewportPos = Camera.main.WorldToViewportPoint(transform.position);
		if (viewportPos.y < -0.5f)
			Destroy (this.gameObject);
	}
	
	public void SetVelocity(Vector3 direction, float speed) {
		rb.AddForce(direction * speed);
	}

	void OnTriggerEnter(Collider coll) {
		GameObject collidedWith = coll.gameObject;
		if (collidedWith.tag == "BucketTop") {
			GameObject bucket = collidedWith.transform.parent.gameObject;
			bucket.GetComponent<Rigidbody>().mass += 0.2f; 
			Destroy (this.gameObject);
		}

		if (collidedWith.tag == "BucketBottom") {
			GameObject bucket = collidedWith.transform.parent.gameObject;
			//FixedJoint connection = new FixedJoint();
			FixedJoint connection = bucket.AddComponent<FixedJoint>();//GetComponent<Rigidbody>()
			connection.connectedBody = rb;
			connection.breakForce = 100;
			connection.breakTorque = 100;
			//Destroy (this.gameObject);
		}

	}
}
