using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Collectable : Ball 
{
	public GameObject collectAnim, hazardAlert, ballMiss;
	public AudioClip[] collectSounds;
	public AudioClip alert;

	void Update()
	{

	}

	void DestroyBall()
	{
		GameObject c = Instantiate(collectAnim,transform.position,Quaternion.identity) as GameObject;
		c.GetComponent<AudioSource>().PlayOneShot(collectSounds[Random.Range(0,collectSounds.Length)]);
		Destroy(c,0.5f);
		Destroy(this.gameObject);
	}

	//void MoveSpeedInc(float amt)
	//{
	//	moveSpeed += amt;
	//}

	void OnDestroy()
	{
		if(missed)
		{
			GameObject c = Instantiate (ballMiss, transform.position, Quaternion.identity) as GameObject;
			c.GetComponent<AudioSource>().PlayOneShot(alert);
			GameController.instance.scoreScr.ResetMultiplier();

            GameObject.FindObjectOfType<SeekerSpawner>().SpawnSeeker();
		}
	}
}
