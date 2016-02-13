using UnityEngine;
using System.Collections;

public class TestJoystick : MonoBehaviour {

	public Vector2 startingPos;

	public bool touch1;

	public float multiplier;

	// Use this for initialization
	void Start () {
	
		startingPos = new Vector2 (transform.position.x, transform.position.y);
	}
	
	// Update is called once per frame
	void Update () {
		if (touch1) {

			transform.position = new Vector2 (Input.GetAxis ("JoystickLeftX") * multiplier, Input.GetAxis ("JoystickLeftY") * (multiplier * 1.5f));
		} else {

			transform.position = new Vector2 (Input.GetAxis ("JoystickRightX") * multiplier, Input.GetAxis ("JoystickRightY") * (multiplier * 1.5f));
		}
	}
}
