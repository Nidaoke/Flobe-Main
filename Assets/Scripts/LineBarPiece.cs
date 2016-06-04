using UnityEngine;
using System.Collections;

public class LineBarPiece : MonoBehaviour {

	public Sprite[] colors;
	public RuntimeAnimatorController[] anims;

	public enum barPieceThing{
		left,right,middle
	}
	public barPieceThing barPiece;

	public enum possibleColors
	{
		purple,blue,orange
	}
	public possibleColors myColor;

	void Update(){
		if (myColor == possibleColors.blue) 
			GetComponent<Renderer> ().enabled = false;
		else 
			GetComponent<Renderer> ().enabled = true;
		if (myColor == possibleColors.purple) {
			GetComponent<SpriteRenderer> ().sprite = colors [0];
			GetComponent<Animator> ().runtimeAnimatorController = anims [0];
		} else if (myColor == possibleColors.orange) {
			GetComponent<SpriteRenderer> ().sprite = colors [2];
			GetComponent<Animator> ().runtimeAnimatorController = anims [2];
		} else {
			GetComponent<SpriteRenderer> ().sprite = colors [1];
			GetComponent<Animator> ().runtimeAnimatorController = anims [1];
		}
	}
}
