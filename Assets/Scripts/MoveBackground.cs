using UnityEngine;
using System.Collections;

public class MoveBackground : MonoBehaviour {

	public float speedX;
	public float speedY;

	public float random;

	public Rigidbody2D rigidbody;

	public bool spin;

	public float maxTimer;
	public float timer;

	public void Start(){

		timer = maxTimer * 100;

		rigidbody = GetComponent<Rigidbody2D> ();

		random = Random.Range (-3, 3);
	
	}

	// Update is called once per frame
	void FixedUpdate () {

		if (spin) {

			rigidbody.angularVelocity = random;
		}

		if (timer > 0) {

			timer -= Time.timeScale;
		} else {

			speedX = -speedX;
			speedY = -speedY;

			timer = maxTimer * 100;
		}
	
		transform.position += new Vector3 (speedX / 1000, speedY / 1000);
	}
}
