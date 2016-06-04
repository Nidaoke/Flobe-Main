using UnityEngine;
using System.Collections;

public class SideCapCode : MonoBehaviour {

	public int fakeCount;
	//GameObject[] fakes;

	public bool flush;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (fakeCount >= 80)
			flush = true;
	}
	void OnTriggerEnter2D(Collider2D other){
		if (other.gameObject.layer == 20) {
			if (other.gameObject.GetComponentInParent<PolygonCollider2D> () != null) {
				fakeCount++;
			}
		}
	}
}