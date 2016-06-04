using UnityEngine;
using System.Collections;

public class HandleSideBar : MonoBehaviour {

	public Sprite[] colors;

	public Sprite selectedColor;

	// Use this for initialization
	void Start () {
		selectedColor = colors[0];
		GetComponent<SpriteRenderer> ().sprite = selectedColor;
	}
	
	// Update is called once per frame
	void Update () {
		if (GetComponent<SpriteRenderer> ().sprite != selectedColor) {
			GetComponent<SpriteRenderer> ().sprite = selectedColor;
		}
	}

	public void SetColor(string color){
		if (color == "Blue")
			selectedColor = colors [0];
		if (color == "Purple")
			selectedColor = colors [1];
		if (color == "Orange")
			selectedColor = colors [2];
	}
}