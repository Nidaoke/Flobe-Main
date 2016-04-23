using UnityEngine;
using System.Collections;

public class FreezeAllEnemies : MonoBehaviour {

	/*public Hazard[] greens;
	public Seeker[] missiles;
	public Bomb[] bombs;*/

	public Ball[] enemies;

	public void BeginFreeze(){

		GatherEnemies ();
		ActivateFreeze ();
	}

	void GatherEnemies(){

		/*greens = GameObject.FindObjectsOfType<Hazard> ();
		missiles = GameObject.FindObjectsOfType<Seeker> ();
		bombs = GameObject.FindObjectsOfType<Bomb> ();*/

		enemies = GameObject.FindObjectsOfType<Ball> ();
	}

	void ActivateFreeze(){

		foreach (Ball b in enemies) {
			//Debug.Log ("Called Freeze");
			b.Freeze ();
		}

		GameObject.FindGameObjectWithTag ("Spawner").GetComponent<Spawner> ().pauseSpawning = true;
		GameObject.FindObjectOfType<StartInstructionsFade> ().StartFade ();

		StartCoroutine (UndoFreeze ());
	}

	IEnumerator UndoFreeze(){

		yield return new WaitForSeconds (6);

		GatherEnemies ();

		foreach (Ball b in enemies) {

			b.UnFreeze ();
		}
		GameObject.FindGameObjectWithTag ("Spawner").GetComponent<Spawner> ().pauseSpawning = false;
	}
}
