using UnityEngine;
using System.Collections;

public class Bomb : Ball 
{
	public float sizeVariance;

	public GameObject explosion;
	public GameObject sparks;

	public GameObject warning;

	public Animator anim;

	public override void ReverseDirection(){
		//Do Nothing!
	}
	
	void Start()
	{

		//gameObject.layer = 15;
		transform.localScale = Vector3.one*Mathf.Clamp(Random.Range(transform.localScale.x-sizeVariance,transform.localScale.x+sizeVariance),0.25f,10f);

		anim = GetComponent<Animator> ();
	}
	
	//void MoveSpeedInc(float amt)
	//{
	//	moveSpeed += amt;
	//}

	public void Boom(){

		//Debug.Log ("BOOM!");

		StartCoroutine (Explode ());
		//explosion.SetActive (true);
		//Destroy (this);
	}

	void OnDestroy(){
		GameObject.FindObjectOfType<SpawnMonsterFakes> ().AddBombEnemy ();
	}

	IEnumerator Explode(){

		moveSpeed = 0;

		GetComponent<Bomb> ().warning.SetActive(false);

		sparks.SetActive (true);
		yield return new WaitForSeconds(2);
		sparks.SetActive (false);
		explosion.SetActive (true);

       // GameObject[] collectables = GameObject.FindGameObjectsWithTag("Ball");
        //foreach (GameObject ball in collectables)
        //{

           // ball.GetComponent<Rigidbody2D>.exp
       // }

		 GetComponent<Animator>().enabled = false;
        GetComponent<SpriteRenderer>().sprite = null;

        GameObject.FindGameObjectWithTag("SteamManager").GetComponent<SteamStatsAndAchievements>().BombsExploded++;
        GameObject.FindGameObjectWithTag("SteamManager").GetComponent<SteamStatsAndAchievements>().m_bStoreStats = true;

        yield return new WaitForSeconds (1f);

        Destroy (gameObject);
		Debug.Log ("Boom!");
	}
}
