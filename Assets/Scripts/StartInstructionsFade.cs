using UnityEngine;
using System.Collections;

public class StartInstructionsFade : MonoBehaviour 
{
	[SerializeField] private float mFadeDelay = 0.5f;
	[SerializeField] private float mFadeDuration = 1f;
	[SerializeField] private SpriteRenderer mSpriteRenderer;

	Vector3 oldPosition;

	void Start(){

		oldPosition = transform.position;
	}

	void Reset(){

		transform.position = oldPosition;
		mSpriteRenderer.material.color = new Color (1, 1, 1, 1);
		GetComponent<Rigidbody> ().useGravity = false;
	}

	//This is called by the GameControler script's GameBegin() function ~Adam
	public void StartFade()
	{
		if(mSpriteRenderer != null)
		{
			StartCoroutine(FadeOutInstructions());
		}
		else
		{
			Destroy(this.gameObject);
		}
	}//END of StartFade()

	IEnumerator FadeOutInstructions()
	{
        // GetComponent<Rigidbody>().velocity = new Vector3(0, -5, 0);
        GetComponent<Rigidbody>().useGravity = true;

		yield return new WaitForSeconds(mFadeDelay);

		float alpha = mSpriteRenderer.material.color.a;
		for (float t = 0.0f; t < 1.0f; t += Time.deltaTime/mFadeDuration)
		{
			Color newColor = new Color(1, 1, 1, Mathf.Lerp(alpha,0,t));
			mSpriteRenderer.material.color = newColor;
           // Debug.Log(mSpriteRenderer.material.color.a);
			yield return null;
		}

		yield return new WaitForSeconds (5);
		Reset ();
	}//END of FadeOutInstructions()
}
