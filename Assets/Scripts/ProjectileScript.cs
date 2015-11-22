using UnityEngine;
using System.Collections;

public class ProjectileScript : MonoBehaviour {

	SpriteRenderer spr;
	Rigidbody rb;
	bool stuck = false;
	public AudioClip bark;
	public Sprite box,chair,meat,dog,sheep,knight,cannon;
	public float totalCharge;
	public float chargeCap;
	public float off;
	
	// Use this for initialization
	void Start () {
		spr = GetComponent<SpriteRenderer>();
		rb = GetComponent<Rigidbody>();
		GetComponent<AudioSource> ().Play ();
		float strength = totalCharge / chargeCap;
		float rand = Random.Range (0f, 1f) + off;
		if (strength < 0.33f) {
			rb.mass = 1f;
			if (rand < 0.5f) {
				spr.sprite = box;
			}
			else {
				spr.sprite = chair;
			}
		} else if (strength < 0.7f) {
			rb.mass = 1.25f;
			if (rand < 0.33f) {
				spr.sprite = meat;
			}
			else if (rand < 0.66f) {
				spr.sprite = sheep;
			}
			else {
				AudioSource audio = GetComponent<AudioSource>();
				audio.PlayOneShot(bark);
				spr.sprite = dog;
			}
		} else if (strength < 0.95f) {
			rb.mass = 1.5f;
			spr.sprite = knight;
		} else {
			rb.mass = 2f;
			spr.sprite = cannon;
		}
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
		float strength = totalCharge / chargeCap;
		if (collidedWith.tag == "BucketTop") {
			GameObject bucket = collidedWith.transform.parent.gameObject;
			collidedWith.GetComponent<AudioSource>().Play ();
			collidedWith.GetComponent<ParticleSystem>().Emit(10);
			if (strength < 0.33f)
				bucket.GetComponent<Rigidbody>().mass += 0.3f; 
			else if (strength < 0.7f)
				bucket.GetComponent<Rigidbody>().mass += 0.5f; 
			else if (strength < 0.95f){
				bucket.GetComponent<Rigidbody>().mass += 0.8f; 
			}
			else {
				bucket.GetComponent<Rigidbody>().mass += 1.25f; 
			}
			Destroy (this.gameObject);
		}

		if (collidedWith.tag == "BucketBottom"  && !stuck) {
			stuck = true;
			GameObject bucket = collidedWith.transform.parent.gameObject;
			collidedWith.GetComponent<AudioSource>().Play ();
			//FixedJoint connection = new FixedJoint();
			FixedJoint connection = bucket.AddComponent<FixedJoint>();//GetComponent<Rigidbody>()
			connection.connectedBody = rb;
			connection.breakForce = 100;
			connection.breakTorque = 100;
			//Destroy (this.gameObject);
		}

	}

	void OnJoinBreak(float breakForce) {
		stuck = false;
		Debug.Log ("Unstuck!");
	}
}
