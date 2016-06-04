using UnityEngine;
using System.Collections;

public class PickRandomCard : MonoBehaviour {

	public Sprite[] kharmas;
	public bool good;
	public int cardToPick;

	// Use this for initialization
	void Start () {
		cardToPick = Random.Range (0, kharmas.Length);
		GetComponent<SpriteRenderer> ().sprite = kharmas [cardToPick];
		if (GetComponent<SpriteRenderer> ().sprite == kharmas [0])
			good = true;
		else
			good = false;
	}
}