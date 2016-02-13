using UnityEngine;
using System.Collections;

public class Scale1Direction : MonoBehaviour {

    public bool castParticle = true;

	public bool goLeft;

	public Vector3 desiredScale;
	public float growAmount;

	public Vector3 originalPosition;

	public Vector3 originalScale;

	public void Start(){

		originalScale = transform.localScale;
		originalPosition = transform.localPosition;
	}

	public void Update(){

        if (castParticle)
        {

            //Cast
            castParticle = false;
        }

		if (transform.localScale.x < desiredScale.x) {

			transform.localScale += new Vector3 (growAmount, 0, 0);
		} else {

			if(transform.localScale.x != desiredScale.x){

				transform.localScale = new Vector3(desiredScale.x, 1, transform.localScale.z);
			}
		}

		if(!goLeft)
			transform.localPosition = new Vector3 (originalPosition.x + transform.localScale.x / 2, originalPosition.y, originalPosition.z);
		else
			transform.localPosition = new Vector3 (originalPosition.x - transform.localScale.x / 2, originalPosition.y, originalPosition.z);

	}

}