using UnityEngine;
using System.Collections;

public class BGEffects : MonoBehaviour 
{
	public float rotSpeed;
	public ConstantForce cam;

	void Start()
	{
		StartCoroutine(RandomSpin());
	}

	IEnumerator RandomSpin() 
	{
		while(true)
		{
			cam.torque = new Vector3(Random.Range(-1f,1f),Random.Range(-1f,1f),Random.Range(-1f,1f)).normalized*rotSpeed;
			yield return new WaitForSeconds(Random.value*20);
		}
	}
}
