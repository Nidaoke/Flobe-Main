using UnityEngine;
using System.Collections;

public class BonusManager : MonoBehaviour {

	bool setToFreeze;

	public GameObject cardPicker;

	public int totalBlues, totalPurples, totalOranges;

	public bool inProcessofFlushing;

	public GameObject bonusText;

	public GameObject spawner1, spawner2;

	public GameObject bottom1, bottom2;

	public GameObject cap1, cap2;

	public void SpawnEnemies(bool good){

		Line[] lines = GameObject.FindObjectsOfType<Line> ();
		foreach (Line line in lines) {
			if (good)
				line.invincibleToColors = true;
			else
				line.invincibleToEnemies = true;
		}

		if (good) {

		} else {
			GameObject[] greenBads = GameObject.FindGameObjectsWithTag ("GreenFake");
			foreach (GameObject bad in greenBads) {
				GameObject.FindObjectOfType<Spawner> ().SpawnGreen ();
			}
			GameObject[] redbads = GameObject.FindGameObjectsWithTag ("RedFake");
			foreach (GameObject bad in redbads) {
				GameObject.FindObjectOfType<Spawner> ().SpawnRed ();
			}
			GameObject[] bombBads = GameObject.FindGameObjectsWithTag ("BombFake");
			foreach (GameObject bad in bombBads) {
				GameObject.FindObjectOfType<Spawner> ().SpawnBomb ();
			}
		}

		cap1.GetComponent<SideCapCode> ().flush = false;
		cap2.GetComponent<SideCapCode> ().flush = false;

		bottom1.SetActive (false);
		bottom2.SetActive (false);
	}
	
	// Update is called once per frame
	void Update () {

		if (setToFreeze) {
			Ball[] baylls = GameObject.FindObjectsOfType<Ball> ();
			foreach (Ball ball in baylls) {
				ball.Freeze ();
			}
			TestFollower[] testies = GameObject.FindObjectsOfType<TestFollower> ();
			foreach (TestFollower test in testies) {
				test.blockSpeed = true;
			}
		}

		if (Input.GetKeyDown (KeyCode.U)) {

			Flush ();
		}
	
		totalBlues = spawner1.GetComponent<SpawnFakes> ().blues;
		totalPurples = spawner1.GetComponent<SpawnFakes> ().purples;
		totalOranges = spawner1.GetComponent<SpawnFakes> ().oranges;

		if (cap1.GetComponent<SideCapCode> ().flush || cap2.GetComponent<SideCapCode> ().flush) {
			if (!inProcessofFlushing) {
				inProcessofFlushing = true;
				Flush ();
			}
		}
	}

	public IEnumerator UnFlush(){

		yield return new WaitForSeconds (3);

		bottom1.SetActive (true);
		bottom2.SetActive (true);

		yield return new WaitForSeconds (28);

		FakeTester[] fakes = GameObject.FindObjectsOfType<FakeTester> ();
		foreach (FakeTester fake in fakes) {
			yield return new WaitForSeconds (.0001f);
			GameObject.FindObjectOfType<ScoreCenter> ().score += GameObject.FindObjectOfType<ScoreCenter> ().multiplier;
			Destroy (fake.gameObject);
		}

		yield return new WaitForSeconds (10);
		//GameObject.FindObjectOfType<Spawner> ().pauseSpawning = false;
		Line[] lines = GameObject.FindObjectsOfType<Line> ();
		foreach (Line line in lines) {

			line.invincibleToColors = line.invincibleToEnemies = false;
		}

		cap1.GetComponent<SideCapCode> ().flush = false;
		cap2.GetComponent<SideCapCode> ().flush = false;

		//bottom1.SetActive (true);
		//bottom2.SetActive (true);

		totalBlues = 0;
		totalOranges = 0;
		totalPurples = 0;

		spawner1.GetComponent<SpawnFakes> ().blues = 0;
		spawner2.GetComponent<SpawnFakes> ().blues = 0;
		spawner1.GetComponent<SpawnFakes> ().purples = 0;
		spawner2.GetComponent<SpawnFakes> ().purples = 0;
		spawner1.GetComponent<SpawnFakes> ().oranges = 0;
		spawner2.GetComponent<SpawnFakes> ().oranges = 0;
	}

	public IEnumerator DoFlushyThings(){
		setToFreeze = true;
		GameObject.FindObjectOfType<Spawner> ().pauseSpawning = true;
		yield return new WaitForSeconds (3);

		cardPicker.SetActive (true);

		//GameObject.FindObjectOfType<Spawner> ().pauseSpawning = true;

		/*Line[] lines = GameObject.FindObjectsOfType<Line> ();
		foreach (Line line in lines) {

			line.invincible = true;
		}

		yield return new WaitForSeconds (5);

		inProcessofFlushing = false;

		StartCoroutine (UnFlush ());

		for(int i = 0; i < totalBlues; i++)
		{
			StartCoroutine (GameObject.FindObjectOfType<Spawner> ().SpawnBlue ());

		}

		for(int i = 0; i < totalPurples; i++)
		{
			StartCoroutine (GameObject.FindObjectOfType<Spawner> ().SpawnPurple ());
		}

		for(int i = 0; i < totalOranges; i++)
		{
			StartCoroutine (GameObject.FindObjectOfType<Spawner> ().SpawnOrange ());
		}

		cap1.GetComponent<SideCapCode> ().flush = false;
		cap2.GetComponent<SideCapCode> ().flush = false;

		bottom1.SetActive (false);
		bottom2.SetActive (false);
		*/
	}

	public void Flush(){
		GameObject.FindObjectOfType<LerpDown> ().StartFade ();
		StartCoroutine (DoFlushyThings ());
	}
}