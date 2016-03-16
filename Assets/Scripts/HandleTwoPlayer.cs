using UnityEngine;
using System.Collections;

public class HandleTwoPlayer : MonoBehaviour {

	public bool twoPlayer;

	public GameObject leftFollower2, rightFollower2, leftFollower1, rightFollower1;
	public Line line, line2;

	void Start(){
		if (twoPlayer) {
			rightFollower2.SetActive (true);
			leftFollower2.SetActive (true);
			line.twoPlayer = true;
			line2.twoPlayer = true;
			line2.gameObject.SetActive (false);

			rightFollower1.transform.position = new Vector3 (3.38f, -1.74879f, 10);
			rightFollower2.transform.position = new Vector3 (-3.26f, 1.85f, 10);
			leftFollower1.transform.position = new Vector3 (3.25f, 1.868786f, 10);
			leftFollower2.transform.position = new Vector3 (-3.5f, -1.76f, 10);

		} else {
			rightFollower2.SetActive (false);
			leftFollower2.SetActive (false);
			line.twoPlayer = false;
			line2.twoPlayer = false;

			rightFollower1.transform.position = new Vector3 (2, 0, 10);
			leftFollower1.transform.position = new Vector3 (-2, 0, 10);
		}
	}
}
