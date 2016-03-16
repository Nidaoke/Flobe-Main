using UnityEngine;
using System.Collections;

public class LerpScale : MonoBehaviour {

	public Vector3 desiredScale;
	public float speedy;
	private Vector3 currentScale;

	void Update(){

		currentScale = transform.localScale;

		float xScale = 0;
		float yScale = 0;
		float zScale = 0;

		float tParam = 0;

		if (tParam < 1) {

			tParam += Time.deltaTime * speedy;
			if(currentScale.x != desiredScale.x)
				xScale = Mathf.Lerp (currentScale.x, desiredScale.x, tParam);
			if(currentScale.y != desiredScale.y)
				yScale = Mathf.Lerp (currentScale.y, desiredScale.y, tParam);
			if(currentScale.z != desiredScale.z)
				zScale = Mathf.Lerp (currentScale.z, desiredScale.z, tParam);
		}
			
		transform.localScale = new Vector3 (xScale, yScale, zScale);
	}
}
