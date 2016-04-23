using UnityEngine;
using System.Collections;

public class FakeTester : MonoBehaviour {

	public Vector2 rgbdVel;
	public GameObject pop;

	/*public void OnDestroy(){

		Instantiate (pop, transform.position, Quaternion.identity);
	}*/
	
	// Update is called once per frame
	void Update () {
		rgbdVel = GetComponent<Rigidbody2D> ().velocity;
		if (transform.position.y <= -6) {

			Destroy (gameObject);
		}
	}
}
