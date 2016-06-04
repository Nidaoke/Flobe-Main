using UnityEngine;
using System.Collections;

public class BonusManager : MonoBehaviour {

	public bool setToFreeze;

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
			StartCoroutine (GameObject.FindObjectOfType<Spawner> ().FakeSpawn (true));
			GameObject.FindObjectOfType<ScoreCenter> ().currentlyGoodBonus = true;
		} else {
			StartCoroutine (GameObject.FindObjectOfType<Spawner> ().FakeSpawn (false));
			GameObject.FindObjectOfType<ScoreCenter> ().currentlyBadBonus = true;
		}

		setToFreeze = false;

		TestFollower[] testies = GameObject.FindObjectsOfType<TestFollower> ();
		foreach (TestFollower test in testies) {
			test.blockSpeed = false;
		}

		cap1.GetComponent<SideCapCode> ().flush = false;
		cap2.GetComponent<SideCapCode> ().flush = false;

		bottom1.SetActive (false);
		bottom2.SetActive (false);
	}
	
	// Update is called once per frame
	void Update () {

		if (cardPicker == null) {
			//Debug.Log ("Help!");
			//cardPicker = GameObject.FindGameObjectWithTag ("CardHolder");
		}

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

			//Flush ();
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

	public IEnumerator EndBonus(){
		if (GameObject.FindObjectOfType<ScoreCenter> ().currentlyBadBonus) {
			GameObject.FindObjectOfType<ScoreCenter> ().EndBadBonus ();
			GameObject.FindObjectOfType<ScoreCenter> ().currentlyBadBonus = false;

		} else if (GameObject.FindObjectOfType<ScoreCenter> ().currentlyGoodBonus) {
			GameObject.FindObjectOfType<ScoreCenter> ().EndGoodBonus ();
			GameObject.FindObjectOfType<ScoreCenter> ().currentlyGoodBonus = false;

		}
		yield return new WaitForSeconds(5);
		GameObject.FindObjectOfType<ScoreCenter> ().badBonusPoints = 100;
		GameObject.FindObjectOfType<ScoreCenter> ().goodBonusPoints = 0;
		GameObject.FindObjectOfType<StartInstructionsFade> ().StartFade ();
		yield return new WaitForSeconds (5f);
		GameObject.FindObjectOfType<Spawner> ().pauseSpawning = false;
		GameObject.FindObjectOfType<Line> ().invincibleToColors = GameObject.FindObjectOfType<Line> ().invincibleToEnemies = false;
		bottom1.SetActive (true);
		bottom2.SetActive (true);
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
		if (cardPicker != null)
			cardPicker.SetActive (true);
	}

	public void Flush(){
		GameObject.FindObjectOfType<LerpDown> ().StartFade ();
		StartCoroutine (DoFlushyThings ());
	}
}