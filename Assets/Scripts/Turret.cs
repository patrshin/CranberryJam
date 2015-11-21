using UnityEngine;
using System.Collections;
using InControl;

public class Turret : MonoBehaviour {

	public int playerNum;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		var inputDevice = (playerNum == 1) ? InputManager.Devices[1]: InputManager.Devices[0];
		//Quaternion rotate = this.transform.rotation;
		var max = Mathf.Abs (inputDevice.RightStickY);
		if (Mathf.Abs (inputDevice.RightStickX) > Mathf.Abs (inputDevice.RightStickY )) {
			max = Mathf.Abs (inputDevice.RightStickX);
		}
		
		//rotating player
		if (Mathf.Abs (inputDevice.RightStickX)> 0.2 || Mathf.Abs (inputDevice.RightStickY )> 0.2) {
			
			Vector3 ThumbPos = new Vector3(inputDevice.RightStickX, inputDevice.RightStickY, 0);
			Vector3 playerPos = this.transform.position - transform.parent.position;
			
			var angle = Vector3.Angle (playerPos, ThumbPos);
			var cross = Vector3.Cross (playerPos, ThumbPos);
			if (cross.z < 0) 
				angle = -angle;
			if (Mathf.Abs (angle) > 2) {
				if (angle >= 0) {
					transform.RotateAround(transform.parent.position, Vector3.forward, 10 * max);
				}
				else if (angle < 0){
					transform.RotateAround(transform.parent.position, Vector3.forward, -10 * max);
				}
			}
			
		}
	}
}
