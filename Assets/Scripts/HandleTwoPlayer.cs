using UnityEngine;
using System.Collections;

public class HandleTwoPlayer : MonoBehaviour {

	public bool twoPlayer;

	public GameObject livesUI;

	public GameObject leftFollower2, rightFollower2, leftFollower1, rightFollower1, lives1, lives2, uiLeft, uiRight;
	public Line line, line2;

	public void WhenGameBegins(){
		if (twoPlayer) {

			uiLeft.GetComponent<HandleSideBar> ().SetColor ("Blue");
			uiRight.GetComponent<HandleSideBar> ().SetColor ("Purple");

			livesUI.SetActive (true);

			lives1.SetActive (true);
			lives2.SetActive (true);

			rightFollower2.SetActive (true);
			leftFollower2.SetActive (true);
			line.twoPlayer = true;
			line2.twoPlayer = true;
			line2.gameObject.SetActive (false);

			rightFollower2.transform.position = new Vector3 (4.38f, -1.76f, 10);
			rightFollower1.transform.position = new Vector3 (-2.26f, -1.76f, 10);
			leftFollower2.transform.position = new Vector3 (2.25f, -1.76f, 10);
			leftFollower1.transform.position = new Vector3 (-4.5f, -1.76f, 10);

		} else {
			rightFollower2.SetActive (false);
			leftFollower2.SetActive (false);
			line.twoPlayer = false;
			line2.twoPlayer = false;

			lives1.SetActive (false);
			lives2.SetActive (false);

			rightFollower1.transform.position = new Vector3 (2, 0, 10);
			leftFollower1.transform.position = new Vector3 (-2, 0, 10);
		}
	}
}
