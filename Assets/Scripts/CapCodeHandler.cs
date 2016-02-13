using UnityEngine;
using System.Collections;

public class CapCodeHandler : MonoBehaviour {

	public GameObject old;
	public GameObject newer;

	public GameObject line;

	public bool isLeft;

	// Use this for initialization
	void Start () {
	
		newer.SetActive (false);
		old.SetActive (true);
	}
	
	// Update is called once per frame
	void Update () {
	
		if (line.GetComponent<Line> ().purple.activeSelf) {

			if(!isLeft){

				old.SetActive(false);
				newer.SetActive(true);
			}else{

				old.SetActive(true);
				newer.SetActive(false);
			}
		} else if (line.GetComponent<Line> ().purple2.activeSelf) {

			if(isLeft){
				
				old.SetActive(false);
				newer.SetActive(true);
			}else{
				
				old.SetActive(true);
				newer.SetActive(false);
			}
		}else{

			old.SetActive(true);
			newer.SetActive(false);
		}
	}
}
