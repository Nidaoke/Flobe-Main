using UnityEngine;
using System.Collections;

public class SpawnFakes : MonoBehaviour {

	public GameObject fakeBlue, fakePurple;

	public float spawnTime = 2;

	void Update(){

		/*if (Input.GetKeyDown (KeyCode.U))
			AddBlueEnemy ();
		if (Input.GetKeyDown (KeyCode.Y))
			AddPurpleEnemy ();*/
	}
	
	public void AddBlueEnemy(){
		var x1 = transform.position.x - GetComponent<Renderer> ().bounds.size.x / 2;
		var x2 = transform.position.x + GetComponent<Renderer> ().bounds.size.x / 2;

		var spawnPoint = new Vector2 (Random.Range (x1, x2), transform.position.y);

		Instantiate (fakeBlue, spawnPoint, Quaternion.identity);
	}

	public void AddPurpleEnemy(){
		var x1 = transform.position.x - GetComponent<Renderer> ().bounds.size.x / 2;
		var x2 = transform.position.x + GetComponent<Renderer> ().bounds.size.x / 2;

		var spawnPoint = new Vector2 (Random.Range (x1, x2), transform.position.y);

		Instantiate (fakePurple, spawnPoint, Quaternion.identity);
	}
}