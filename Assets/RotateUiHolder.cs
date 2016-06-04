using UnityEngine;
using System.Collections;

public class RotateUiHolder : MonoBehaviour {

	public Sprite[] holderUIs;
	BackgroundNumbers[] backgrounds;

	public HandleSideBar[] sides;

	// Use this for initialization
	void Start () {
		StartCoroutine (StartIsh ());
	}

	public IEnumerator StartIsh(){
		yield return new WaitForSeconds (.1f);
		backgrounds = GameObject.FindObjectsOfType<BackgroundNumbers> ();
		sides = GameObject.FindObjectsOfType<HandleSideBar> ();
		foreach (BackgroundNumbers background in backgrounds) {
			if (background.gameObject.activeSelf) {
				switch (background.GetComponent<BackgroundNumbers> ().number) {
				case BackgroundNumbers.Numbers.bg1:
					GetComponent<SpriteRenderer> ().sprite = holderUIs [0];
					foreach (HandleSideBar side in sides) {
						side.SetColor ("Blue");
					}
					break;
				case BackgroundNumbers.Numbers.bg2:
					GetComponent<SpriteRenderer> ().sprite = holderUIs [0];
					foreach (HandleSideBar side in sides) {
						side.SetColor ("Blue");
					}
					break;
				case BackgroundNumbers.Numbers.bg3:
					GetComponent<SpriteRenderer> ().sprite = holderUIs [1];
					foreach (HandleSideBar side in sides) {
						side.SetColor ("Purple");
					}
					break;
				case BackgroundNumbers.Numbers.bg4:
					GetComponent<SpriteRenderer> ().sprite = holderUIs [1];
					foreach (HandleSideBar side in sides) {
						side.SetColor ("Purple");
					}
					break;
				}
			}
		}
	}
}
