using UnityEngine;
using System.Collections;

public class PickPlayerUI : MonoBehaviour {

	public GameObject main;

	public GameObject cap1, cap2;

	public GameObject setMultiplier;
	public GameObject gameController;

	public GameObject player1;
	public GameObject player2;

	public float axisPull;
	public float lastPull;

	public float axis2Pull;
	public float last2Pull;

	public int currentSelectedPlayer;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

		axisPull = Input.GetAxis ("JoystickLeftY");
		axis2Pull = Input.GetAxis ("Joystick2LeftY");

		if ((axisPull == 1 && lastPull != 1) || Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow) || (axis2Pull == 1 && last2Pull != 1)) {
			if (currentSelectedPlayer == 1) {

				currentSelectedPlayer = 2;
			}else

			if (currentSelectedPlayer == 2) {

				currentSelectedPlayer = 1;
				}else

			if (currentSelectedPlayer == 0) {

				currentSelectedPlayer = 1;
			}
		}else

			if ((axisPull == -1 && lastPull != -1) || Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow) || (axis2Pull == -1 && last2Pull != -1)) {

			if (currentSelectedPlayer == 1) {

				currentSelectedPlayer = 2;
			}else

			if (currentSelectedPlayer == 2) {

				currentSelectedPlayer = 1;
			}else

			if (currentSelectedPlayer == 0) {

				currentSelectedPlayer = 2;
			}
		}
	
		if (currentSelectedPlayer == 1) {

			player1.transform.localScale = new Vector3 (1.2f, 1.2f, 1);
			player2.transform.localScale = new Vector3 (1, 1, 1);

			cap1.SetActive (true);
			cap2.SetActive (false);
		} else if (currentSelectedPlayer == 0) {

			player2.transform.localScale = new Vector3 (1, 1, 1);
			player1.transform.localScale = new Vector3 (1, 1, 1);

			cap1.SetActive (false);
			cap2.SetActive (false);
		} else {

			player2.transform.localScale = new Vector3 (1.2f, 1.2f, 1);
			player1.transform.localScale = new Vector3 (1, 1, 1);

			cap2.SetActive (true);
			cap1.SetActive (false);
		}

		if (Input.GetKeyDown (KeyCode.Space)) {

			if (currentSelectedPlayer == 1) {
				setMultiplier.SetActive (true);
				setMultiplier.GetComponent<HandleTwoPlayer> ().twoPlayer = false;
			}

			if (currentSelectedPlayer == 2) {
				setMultiplier.SetActive (true);
				setMultiplier.GetComponent<HandleTwoPlayer> ().twoPlayer = true;
			}

			if (currentSelectedPlayer != 0) {
				Destroy (main);
				gameController.GetComponent<GameController> ().DoThisToBegin (true);
			}
		}

		if (Input.GetKeyDown(KeyCode.Joystick1Button7) || Input.GetKeyDown(KeyCode.Joystick2Button7)) {

			if (currentSelectedPlayer == 1) {
				setMultiplier.SetActive (true);
				setMultiplier.GetComponent<HandleTwoPlayer> ().twoPlayer = false;
			}

			if (currentSelectedPlayer == 2) {
				setMultiplier.SetActive (true);
				setMultiplier.GetComponent<HandleTwoPlayer> ().twoPlayer = true;
			}

			if (currentSelectedPlayer != 0) {
				Destroy (main);
				gameController.GetComponent<GameController> ().DoThisToBegin (false);
			}
		}

		lastPull = Input.GetAxis ("JoystickLeftY");
		last2Pull = Input.GetAxis ("Joystick2LeftY");
	}

	void LateUpdate(){

		Time.timeScale = 1;
	}
}
