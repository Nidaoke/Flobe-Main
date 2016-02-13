using UnityEngine;
using System.Collections;

public class testSphereNew : MonoBehaviour {

	public float speed;
	public Vector3 rgbdVelocity;

	public Rigidbody rgbd;
	
	public GameObject otherSphere;

	public enum sphereType{

		Left,
		Right
	};

	public sphereType type;

	// Use this for initialization
	void Start () {
	
		//GetComponent<Rigidbody> ().velocity = new Vector3 (speed, 0, 0);
		rgbd = GetComponent<Rigidbody> ();
	}
	
	// Update is called once per frame
	void FixedUpdate () {

		if (type == sphereType.Left) {

			if(Input.GetAxis("JoystickLeftX") > 0){

				if(transform.position.x >= otherSphere.transform.position.x){

					rgbd.velocity = new Vector2(0, Input.GetAxis("JoystickLeftY") * speed);
				}else{

					rgbd.velocity = new Vector2(Input.GetAxis("JoystickLeftX") * speed, Input.GetAxis("JoystickLeftY") * speed);
				}
			}else{

				rgbd.velocity = new Vector2(Input.GetAxis("JoystickLeftX") * speed, Input.GetAxis("JoystickLeftY") * speed);
			}
		}

		if (type == sphereType.Right) {

			if(Input.GetAxis("JoystickRightX") < 0){
				
				if(transform.position.x <= otherSphere.transform.position.x){
					
					rgbd.velocity = new Vector2(0, Input.GetAxis("JoystickRightY") * speed);
				}else{
					
					rgbd.velocity = new Vector2(Input.GetAxis("JoystickRightX") * speed, Input.GetAxis("JoystickRightY") * speed);
				}
			}else{
				
				rgbd.velocity = new Vector2(Input.GetAxis("JoystickRightX") * speed, Input.GetAxis("JoystickRightY") * speed);
			}
		}

		/*GetComponent<Rigidbody> ().velocity = new Vector3 (speed, 0, 0);

		rgbdVelocity = GetComponent<Rigidbody> ().velocity;



		*/
	}

	void LateUpdate(){

		/*if (type == sphereType.Left) {
			
			if(transform.position.x > otherSphere.transform.position.x){
				
				transform.position = new Vector3(otherSphere.transform.position.x, transform.position.y, transform.position.y);
				GetComponent<Rigidbody> ().velocity = Vector3.zero;

				Debug.Log("Left Breached!");
			}
		}else
		if (type == sphereType.Right) {
			
			if(transform.position.x < otherSphere.transform.position.x){
				
				transform.position = new Vector3(otherSphere.transform.position.x, transform.position.y, transform.position.y);
				GetComponent<Rigidbody> ().velocity = Vector3.zero;

				Debug.Log("Right Breached!");
			}
		}*/

		transform.position = new Vector3 (transform.position.x, transform.position.y, 0);
	}
}
