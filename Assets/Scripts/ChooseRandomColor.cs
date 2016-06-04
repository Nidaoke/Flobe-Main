using UnityEngine;
using System.Collections;

public class ChooseRandomColor : MonoBehaviour {

	public Sprite[] colors;

	// Use this for initialization
	void Start () {
		GetComponent<SpriteRenderer> ().sprite = colors[Random.Range(0, colors.Length)];
	}
}
