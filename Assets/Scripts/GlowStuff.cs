using UnityEngine;
using System.Collections;

public class GlowStuff : MonoBehaviour {
	
	public float lightIntense = 0f;

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
	
		if (GameObject.FindObjectOfType<HandleTwoPlayer> ().twoPlayer) {

			if (lightIntense <= 2.3f) {

				if (goUp) {

					lightIntense += amount;
					GameObject.FindGameObjectWithTag ("Freezer").GetComponent<FreezeAllEnemies> ().BeginFreeze ();
				} else if (goDown) {

					lightIntense -= amount;
					if (lightIntense == 0) {

						Time.timeScale = 1;
						gameObject.GetComponent<GlowStuff> ().enabled = false;
					}
				}
			} else if (lightIntense > 1.65f) {

				goUp = false;
				goDown = true;

				lightIntense = 1.65f;
			}


		} else {

			if (lightEffect.intensity <= 2.3f) {

				if (goUp) {

					lightEffect.intensity += amount;

					GameObject.FindGameObjectWithTag ("Freezer").GetComponent<FreezeAllEnemies> ().BeginFreeze ();

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
}