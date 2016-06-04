using UnityEngine;
using System.Collections;

public class MoveBackground : MonoBehaviour {

	public float speedX;
	public float speedY;

	public bool justRotate;

	public float scaley;

	public float secondaryTimer;

	public bool randomMovement;

	public float random;

	public Rigidbody2D rgbd;

	public bool spin;

	public float maxTimer;
	public float timer;

	public void Start(){

		if (randomMovement) {
			speedX = Random.Range (-4.3f, 4.3f);
			speedY = Random.Range (-3.2f, 3.2f);
			secondaryTimer = Random.Range (3, 8);
			StartCoroutine(ChangeDir(secondaryTimer));
			maxTimer = 9999999999999;
			scaley = Random.Range (.11f, .44f);

			transform.localScale = new Vector2 (scaley, scaley);
		}

		timer = maxTimer * 100;

		rgbd = GetComponent<Rigidbody2D> ();

		random = Random.Range (-3, 3);
	
	}

	public IEnumerator ChangeDir(float timey){
		yield return new WaitForSeconds (timey);

		speedX = Random.Range (-4.3f, 4.3f);
		speedY = Random.Range (-3.2f, 3.2f);

		secondaryTimer = Random.Range (3, 8);
		StartCoroutine(ChangeDir(secondaryTimer));
	}

	// Update is called once per frame
	void FixedUpdate () {

		if (justRotate) {
			rgbd.angularVelocity = -1.2f;
		}

		if (spin) {

			rgbd.angularVelocity = random;
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
