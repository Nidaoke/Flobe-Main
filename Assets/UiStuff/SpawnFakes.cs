using UnityEngine;
using System.Collections;

public class SpawnFakes : MonoBehaviour {

	public GameObject fakeBlue, fakePurple, fakeOrange;

	public int blues, purples, oranges;

	public bool isRight;

	public float spawnTime = 2;

	void Update(){

		if (Input.GetKeyDown (KeyCode.Quote)) {

			AddBlueEnemy ();
			AddPurpleEnemy ();
			AddOrangeEnemy ();
		}
	}
	
	public void AddBlueEnemy(){
		var x1 = transform.position.x - GetComponent<Renderer> ().bounds.size.x / 2;
		var x2 = transform.position.x + GetComponent<Renderer> ().bounds.size.x / 2;

		var spawnPoint = new Vector2 (Random.Range (x1, x2), transform.position.y);

		blues++;

		Instantiate (fakeBlue, spawnPoint, Quaternion.identity);
	}

	public void AddOrangeEnemy(){
		var x1 = transform.position.x - GetComponent<Renderer> ().bounds.size.x / 2;
		var x2 = transform.position.x + GetComponent<Renderer> ().bounds.size.x / 2;

		var spawnPoint = new Vector2 (Random.Range (x1, x2), transform.position.y);

		oranges++;

		Instantiate (fakeOrange, spawnPoint, Quaternion.identity);
	}

	public void AddPurpleEnemy(){
		var x1 = transform.position.x - GetComponent<Renderer> ().bounds.size.x / 2;
		var x2 = transform.position.x + GetComponent<Renderer> ().bounds.size.x / 2;

		var spawnPoint = new Vector2 (Random.Range (x1, x2), transform.position.y);

		purples++;

		Instantiate (fakePurple, spawnPoint, Quaternion.identity);
	}
}