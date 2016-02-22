using UnityEngine;
using System.Collections;

public class GlowStuff : MonoBehaviour {

	public Light lightEffect;

	public bool goUp;
	public bool goDown;

	public float amount;

	// Use this for initialization
	void Start () {
	
		lightEffect = GetComponent<Light> ();
	}
	
	// Update is called once per frame
	void Update () {
	
		//Debug.Log (lightEffect.intensity);

		if (lightEffect.intensity <= 2.3f) {

			if (goUp) {

				lightEffect.intensity += amount;

				Time.timeScale = .5f;

			} else if (goDown) {

				lightEffect.intensity -= amount;

				if(lightEffect.intensity == 0){

					Time.timeScale = 1;
                    gameObject.GetComponent<GlowStuff>().enabled = false;
				}
			}
		} else if (lightEffect.intensity > 1.65f) {

			goUp = false;
			goDown = true;

			lightEffect.intensity = 1.65f;
		}
	}
}
