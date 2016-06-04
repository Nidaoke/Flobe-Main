using UnityEngine;
using System.Collections;

public class CardHolderScript : MonoBehaviour {

	[SerializeField] private float mFadeDelay = 0.5f;
	[SerializeField] private float mFadeDuration = 1f;
	//[SerializeField] private SpriteRenderer mSpriteRenderer;
	Vector3 oldPosition;
	public GameObject desiredRot, desiredRot2;
	public GameObject cardHolder;
	public GameObject topCard, bottomCard;
	public bool selectedACard;
	public GameObject main;
	public GameObject cap1, cap2;
	public float axisPull;
	public float lastPull;
	public float axis2Pull;
	public float last2Pull;
	public int currentSelectedCard;
	public GameObject front, front2;

	// Use this for initialization
	void Start () {
		oldPosition = transform.position;
	}

	IEnumerator FadeOutInstructions()
	{
		// GetComponent<Rigidbody>().velocity = new Vector3(0, -5, 0);
		//GetComponent<Rigidbody>().useGravity = true;

		Ball[] balls = GameObject.FindObjectsOfType<Ball> ();
		foreach (Ball ball in balls)
			Destroy (ball.gameObject);

		yield return new WaitForSeconds (mFadeDelay);

		GameObject.FindObjectOfType<BonusManager> ().setToFreeze = false;

		transform.position = new Vector3 (1000, 1000, 1000);

		desiredRot.transform.rotation = desiredRot2.transform.rotation = new Quaternion (0, 0, 0, 0);
		selectedACard = false;
		transform.position = oldPosition;

		Instantiate (cardHolder, oldPosition, Quaternion.identity);

		Destroy (this.gameObject);

		yield return new WaitForSeconds (5);
		//Reset ();
	}

	// Update is called once per frame
	void Update () {

		axisPull = Input.GetAxis ("JoystickLeftY");
		axis2Pull = Input.GetAxis ("Joystick2LeftY");

		if (!selectedACard) {
			if ((axisPull == 1 && lastPull != 1) || Input.GetKeyDown (KeyCode.W) || Input.GetKeyDown (KeyCode.UpArrow) || (axis2Pull == 1 && last2Pull != 1)) {
				if (currentSelectedCard == 1) {
					currentSelectedCard = 2;
				} else if (currentSelectedCard == 2) {
					currentSelectedCard = 1;
				} else if (currentSelectedCard == 0) {
					currentSelectedCard = 1;
				}
			} else {
				if ((axisPull == -1 && lastPull != -1) || Input.GetKeyDown (KeyCode.S) || Input.GetKeyDown (KeyCode.DownArrow) || (axis2Pull == -1 && last2Pull != -1)) {
					if (currentSelectedCard == 1) {
						currentSelectedCard = 2;
					} else if (currentSelectedCard == 2) {
						currentSelectedCard = 1;
					} else if (currentSelectedCard == 0) {
						currentSelectedCard = 2;
					}
				}
			}
		}

		if (currentSelectedCard == 1) {

			topCard.transform.localScale = new Vector3 (.3f, .3f, 1);
			bottomCard.transform.localScale = new Vector3 (.25f, .25f, 1);

			cap1.SetActive (true);
			cap2.SetActive (false);
		} else if (currentSelectedCard == 0) {

			bottomCard.transform.localScale = new Vector3 (.25f, .25f, 1);
			topCard.transform.localScale = new Vector3 (.25f, .25f, 1);

			cap1.SetActive (false);
			cap2.SetActive (false);
		} else {

			bottomCard.transform.localScale = new Vector3 (.3f, .3f, 1);
			topCard.transform.localScale = new Vector3 (.25f, .25f, 1);

			cap2.SetActive (true);
			cap1.SetActive (false);
		}

		if ((Input.GetKeyDown (KeyCode.Space) || (Input.GetKeyDown(KeyCode.Joystick1Button7) || Input.GetKeyDown(KeyCode.Joystick2Button7))) && !selectedACard) {
			if (currentSelectedCard != 0) {
				if (currentSelectedCard == 1) {
					topCard.GetComponent<LerpRotation> ().DoIt ();
					if (front.GetComponent<PickRandomCard> ().good) {
						Debug.Log ("SendGood!");
						GameObject.FindObjectOfType<BonusManager> ().SpawnEnemies (true);
					} else {
						Debug.Log ("SendBad!");
						GameObject.FindObjectOfType<BonusManager> ().SpawnEnemies (false);
					}
				}

				if (currentSelectedCard == 2) {
					bottomCard.GetComponent<LerpRotation> ().DoIt ();
					if (front2.GetComponent<PickRandomCard> ().good) {
						Debug.Log ("SendGood!");
						GameObject.FindObjectOfType<BonusManager> ().SpawnEnemies (true);
					} else {
						Debug.Log ("SendBad!");
						GameObject.FindObjectOfType<BonusManager> ().SpawnEnemies (false);
					}
				}

				selectedACard = true;
				StartCoroutine (FadeOutInstructions ());
			}
		}

		lastPull = Input.GetAxis ("JoystickLeftY");
		last2Pull = Input.GetAxis ("Joystick2LeftY");
	}

	void LateUpdate(){

		Time.timeScale = 1;
	}
}