using UnityEngine;
using System.Collections;

public class ChangeSprite : MonoBehaviour {

	public Sprite purpleJar;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

		if (GameObject.FindObjectOfType<Line> () != null) {

			if (GameObject.FindObjectOfType<Line> ().twoPlayer) {

				GetComponent<SpriteRenderer> ().sprite = purpleJar;
			}
		}
	}
}
