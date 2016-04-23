using UnityEngine;
using System.Collections;

public class SideCapCode : MonoBehaviour {

	public bool flush;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerStay2D(Collider2D other){

		if (other.gameObject.layer == 20) {

			if (other.gameObject.GetComponent<PolygonCollider2D> () != null) {

				if (other.gameObject.GetComponent<Rigidbody2D> ().velocity == Vector2.zero) {

					if (GameObject.FindObjectOfType<Line> ().invincible) {


					} else {

						flush = true;
					}
				}
			}
		}
	}
}
