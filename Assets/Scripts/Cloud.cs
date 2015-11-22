using UnityEngine;
using System.Collections;

public class Cloud : MonoBehaviour {

	public float speed;

	public Sprite cloud1;
	public Sprite cloud2;
	public Sprite cloud3;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		transform.position += new Vector3(speed * Time.deltaTime, 0, 0);
		Vector3 viewPortPos = Camera.main.WorldToViewportPoint (transform.position);
		if (viewPortPos.x > 1.3) {
			transform.position = new Vector3(-8, Random.Range(-4.0f, 4.0f), 2);
			int spriteNum = Random.Range(0, 3);
			switch(spriteNum) {
			default:
			case 0:
				GetComponent<SpriteRenderer>().sprite = cloud1;
				break;
			case 1:
				GetComponent<SpriteRenderer>().sprite = cloud2;
				break;
			case 2:
				GetComponent<SpriteRenderer>().sprite = cloud3;
				break;
			}
		}
	}
}
