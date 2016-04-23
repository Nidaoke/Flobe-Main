using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Collectable : Ball 
{
	public GameObject collectAnim, hazardAlert, ballMiss;
	public AudioClip[] collectSounds;
	public AudioClip alert;

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

		//Destroy(Instantiate(bluePop, objJ.transform.position, Quaternion.identity), 1);

		if(missed)
		{
			GameObject c = Instantiate (ballMiss, transform.position, Quaternion.identity) as GameObject;
			c.GetComponent<AudioSource>().PlayOneShot(alert);

			if (tag == "Purple") {

				GameController.instance.scoreScr.ResetMultiplier (false);
			} else {

				GameController.instance.scoreScr.ResetMultiplier(true);
			}

			if (!GameObject.FindObjectOfType<Line> ().invincible) {

				GameObject.FindObjectOfType<SeekerSpawner>().SpawnSeeker();
			}
		}
	}
}
