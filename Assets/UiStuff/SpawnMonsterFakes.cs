using UnityEngine;
using System.Collections;

public class SpawnMonsterFakes : MonoBehaviour {

	public GameObject fakeGreen, fakeRed, fakeBomb;

	public int greens, reds, bombs;

	public bool isRight;

	public float spawnTime = 2;

	void Update(){

		if (Input.GetKeyDown (KeyCode.Quote)) {

			AddGreenEnemy ();
			AddBombEnemy ();
			AddRedEnemy ();
		}
	}

	public void AddGreenEnemy(){
		var x1 = transform.position.x - GetComponent<Renderer> ().bounds.size.x / 2;
		var x2 = transform.position.x + GetComponent<Renderer> ().bounds.size.x / 2;

		var spawnPoint = new Vector2 (Random.Range (x1, x2), transform.position.y);

		greens++;

		Instantiate (fakeGreen, spawnPoint, Quaternion.identity);
	}

	public void AddRedEnemy(){
		var x1 = transform.position.x - GetComponent<Renderer> ().bounds.size.x / 2;
		var x2 = transform.position.x + GetComponent<Renderer> ().bounds.size.x / 2;

		var spawnPoint = new Vector2 (Random.Range (x1, x2), transform.position.y);

		reds++;

		Instantiate (fakeRed, spawnPoint, Quaternion.identity);
	}

	public void AddBombEnemy(){
		var x1 = transform.position.x - GetComponent<Renderer> ().bounds.size.x / 2;
		var x2 = transform.position.x + GetComponent<Renderer> ().bounds.size.x / 2;

		var spawnPoint = new Vector2 (Random.Range (x1, x2), transform.position.y);

		bombs++;

		Instantiate (fakeBomb, spawnPoint, Quaternion.identity);
	}
}