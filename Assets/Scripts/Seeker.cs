﻿using UnityEngine;
using System.Collections;

public class Seeker : Ball
{
	void Start() 
	{
		//dir = (GameController.instance.lineScr.transform.position - transform.position).normalized;

		transform.localScale = Vector3.one*Mathf.Clamp(Random.Range(transform.localScale.x,transform.localScale.x),0.25f,10f);

		transform.up = dir;
	}

	////void MoveSpeedInc(float amt)
	//{
	//	moveSpeed += amt;
	//}
}