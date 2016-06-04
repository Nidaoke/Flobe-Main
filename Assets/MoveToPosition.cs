using UnityEngine;
using System.Collections;

public class MoveToPosition : MonoBehaviour {

	public Transform posToMoveTo;
	public bool startMoving;
	public float speed;
	
	// Update is called once per frame
	void Update () {
		float step = speed * Time.deltaTime;

		if (startMoving) {
			transform.position = Vector3.MoveTowards (transform.position, posToMoveTo.position, step);
		}
	}

	public void ActivateMoving(){
		startMoving = true;
	}

	public void StopMoving(){
		startMoving = false;
	}
}
