using UnityEngine;
using System.Collections;

public class TwoPlayerLives : MonoBehaviour {

	public int blueLives = 5;
	public int purpleLives = 5;
	public GameObject[] blueBubbles;
	public GameObject[] purpleBubbles;

	public GameObject effect;

	public void Update(){

		if (blueLives == 4) {

			if (blueBubbles [0] != null) {

				effect = Instantiate (blueBubbles [0].GetComponent<FakeTester> ().pop, blueBubbles [0].transform.position, Quaternion.identity) as GameObject;
				effect.transform.position = new Vector3 (effect.transform.position.x, effect.transform.position.y, -11);
				Destroy (blueBubbles [0]);
			}
		} else if (blueLives == 3) {

			if (blueBubbles [4] != null) {
				effect = Instantiate (blueBubbles [4].GetComponent<FakeTester> ().pop, blueBubbles [4].transform.position, Quaternion.identity) as GameObject;
				effect.transform.position = new Vector3 (effect.transform.position.x, effect.transform.position.y, -11);
				Destroy (blueBubbles [4]);
			}
		} else if (blueLives == 2) {

			if (blueBubbles [1] != null) {
				effect = Instantiate (blueBubbles [1].GetComponent<FakeTester> ().pop, blueBubbles [1].transform.position, Quaternion.identity) as GameObject;
				effect.transform.position = new Vector3 (effect.transform.position.x, effect.transform.position.y, -11);
				Destroy (blueBubbles [1]);
			}
		} else if (blueLives == 1) {

			if (blueBubbles [3] != null) {
				effect = Instantiate (blueBubbles [3].GetComponent<FakeTester> ().pop, blueBubbles [3].transform.position, Quaternion.identity) as GameObject;
				effect.transform.position = new Vector3 (effect.transform.position.x, effect.transform.position.y, -11);
				Destroy (blueBubbles [3]);
			}
		} else if (blueLives == 0) {

			if (blueBubbles [2] != null) {
				effect = Instantiate (blueBubbles [2].GetComponent<FakeTester> ().pop, blueBubbles [2].transform.position, Quaternion.identity) as GameObject;
				effect.transform.position = new Vector3 (effect.transform.position.x, effect.transform.position.y, -11);
				Destroy (blueBubbles [2]);
			}
		}

		if (purpleLives == 4) {

			if (purpleBubbles [0] != null) {

				effect = Instantiate (purpleBubbles [0].GetComponent<FakeTester> ().pop, purpleBubbles [0].transform.position, Quaternion.identity) as GameObject;
				effect.transform.position = new Vector3 (effect.transform.position.x, effect.transform.position.y, -11);
				Destroy (purpleBubbles [0]);
			}
		} else if (purpleLives == 3) {

			if (purpleBubbles [4] != null) {
				effect = Instantiate (purpleBubbles [4].GetComponent<FakeTester> ().pop, purpleBubbles [4].transform.position, Quaternion.identity) as GameObject;
				effect.transform.position = new Vector3 (effect.transform.position.x, effect.transform.position.y, -11);
				Destroy (purpleBubbles [4]);
			}
		} else if (purpleLives == 2) {

			if (purpleBubbles [1] != null) {
				effect = Instantiate (purpleBubbles [1].GetComponent<FakeTester> ().pop, purpleBubbles [1].transform.position, Quaternion.identity) as GameObject;
				effect.transform.position = new Vector3 (effect.transform.position.x, effect.transform.position.y, -11);
				Destroy (purpleBubbles [1]);
			}
		} else if (purpleLives == 1) {

			if (purpleBubbles [3] != null) {
				effect = Instantiate (purpleBubbles [3].GetComponent<FakeTester> ().pop, purpleBubbles [3].transform.position, Quaternion.identity) as GameObject;
				effect.transform.position = new Vector3 (effect.transform.position.x, effect.transform.position.y, -11);
				Destroy (purpleBubbles [3]);
			}
		} else if (purpleLives == 0) {

			if (purpleBubbles [2] != null) {
				effect = Instantiate (purpleBubbles [2].GetComponent<FakeTester> ().pop, purpleBubbles [2].transform.position, Quaternion.identity) as GameObject;
				effect.transform.position = new Vector3 (effect.transform.position.x, effect.transform.position.y, -11);
				Destroy (purpleBubbles [2]);
			}
		}
	}
	
	public void TakeLife(string colorToKill){

		if (colorToKill == "Blue") {

			blueLives--;
		} else if (colorToKill == "Purple") {

			purpleLives--;
		}
	}
}
