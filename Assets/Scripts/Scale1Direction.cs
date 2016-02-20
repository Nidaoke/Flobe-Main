using UnityEngine;
using System.Collections;

public class Scale1Direction : MonoBehaviour 
{

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

            //Cast
            castParticle = false;
        }

		if (transform.localScale.x < desiredScale.x && !mFirstTimeScalingDone) 
		{

			transform.localScale += new Vector3 (growAmount, 0, 0);
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

	//Gets called by the other bar the first time the purple fills up ~Adam
	public void FirstScaleDone()
	{
		mFirstTimeScalingDone = true;
		transform.localScale = new Vector3(desiredScale.x, 1, transform.localScale.z);
	}
}