﻿using UnityEngine;
using System.Collections;

public class Hazard : Ball 
{
	public float sizeVariance;

	void Start()
	{
		transform.localScale = Vector3.one*Mathf.Clamp(Random.Range(transform.localScale.x-sizeVariance,transform.localScale.x+sizeVariance),0.25f,10f);
	}

	//void MoveSpeedInc(float amt)
	//{
	//	moveSpeed += amt;
	//}
}