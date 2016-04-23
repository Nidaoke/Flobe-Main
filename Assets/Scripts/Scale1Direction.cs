using UnityEngine;
using System.Collections;

public class Scale1Direction : MonoBehaviour 
{

	public bool dontMove;

    public bool castParticle = true;

	public bool goLeft;

	public Vector3 desiredScale;
	public float growAmount;

	public Vector3 originalPosition;

	public Vector3 originalScale;

	//For making it only scale up the first time ~Adam
	bool mFirstTimeScalingDone = false;

	//Reference to the other bar that is set in-editor ~Adam
	[SerializeField] private Scale1Direction mOtherBar;

	public void Start()
	{
		originalScale = transform.localScale;
		originalPosition = transform.localPosition;
	}

	public void Update(){

		if (castParticle)
		{
			castParticle = false;
		}

		if (transform.localScale != desiredScale) {

			float tParam = 0;
			float speed = .9f;

			float xIe;
			float yIe;
			float zIe;

			if (tParam < 1) {

				tParam += Time.deltaTime * speed;
				xIe = Mathf.Lerp (transform.localScale.x, desiredScale.x, tParam);
				yIe = Mathf.Lerp (transform.localScale.y, desiredScale.y, tParam);
				zIe = Mathf.Lerp (transform.localScale.z, desiredScale.z, tParam);
				transform.localScale = new Vector3 (xIe, yIe, zIe);

				mOtherBar.MatchScaling();
			}
		}

		if (!dontMove) {

			if(!goLeft)
				transform.localPosition = new Vector3 (originalPosition.x + transform.localScale.x / 2, originalPosition.y, originalPosition.z);
			else
				transform.localPosition = new Vector3 (originalPosition.x - transform.localScale.x / 2, originalPosition.y, originalPosition.z);
		}
	}

	/*

	public void Update(){

        if (castParticle)
        {
            castParticle = false;
        }

		if (transform.localScale.x < desiredScale.x && !mFirstTimeScalingDone) 
		{

			transform.localScale += new Vector3 (growAmount, 0, 0);
			mOtherBar.MatchScaling();
		} 
		else if(!mFirstTimeScalingDone)
		{
			if(transform.localScale.x != desiredScale.x){

				transform.localScale = new Vector3(desiredScale.x, 1, transform.localScale.z);

				mFirstTimeScalingDone = true;

				if(mOtherBar != null)
				{
					mOtherBar.FirstScaleDone();
				}
			}
		}

		if(!goLeft)
			transform.localPosition = new Vector3 (originalPosition.x + transform.localScale.x / 2, originalPosition.y, originalPosition.z);
		else
			transform.localPosition = new Vector3 (originalPosition.x - transform.localScale.x / 2, originalPosition.y, originalPosition.z);

	}

*/

	//Gets called by the other bar the first time the purple fills up ~Adam
	public void MatchScaling()
	{
		transform.localScale = mOtherBar.transform.localScale;
	}
	public void FirstScaleDone()
	{
		mFirstTimeScalingDone = true;
		transform.localScale = new Vector3(desiredScale.x, 1, transform.localScale.z);
	}
}