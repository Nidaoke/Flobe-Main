using UnityEngine;
using System.Collections;

public class LerpRotation : MonoBehaviour {

	public Transform desiredRot;

	public bool begin;

	public float speed;

	void Update () {
		if (begin)
			transform.rotation = Quaternion.Lerp (transform.rotation, desiredRot.localRotation, Time.time * speed);
	}

	public void DoIt(){
		begin = true;
	}
}
