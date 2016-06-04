using UnityEngine;
using System.Collections;
//using GooglePlayGames;
//using GooglePlayGames.BasicApi;
using UnityEngine.SocialPlatforms;
using Steamworks;

public class ScoreCenter : MonoBehaviour 
{
	public bool currentlyBadBonus;
	public int badBonusPoints = 100;

	public bool increaseMultiplierGrowth;

	public float pseudoMultiplier;

	public bool matchMultipliers = true;

	public Spawner spawnScr;
	public BGEffects bgScr;
	public GameObject floatingTextInst;
	public AudioSource audioS;
	public AudioClip[] fillSounds;
	public Renderer multiplierVisual, multiplierVisual2, liveVisual, liveVisual2;
	public ParticleSystem overflow, overflow2;
	public TextMesh multiMesh, multiMesh2, scoreMesh, scoreMesh2, lifeMesh, lifeMesh2;
	public int score, score2, multiplier = 1, multiplier2 = 1;

	public float period;
	public Animator scoreAnim;

	public GameObject rightObject, leftObject, leftObject2, rightObject2;

	bool lerping;
	public float multTimer = 2f, multTimer2 = 2f;

	public void HitBadDuringBonus(){
		if(currentlyBadBonus)
			badBonusPoints--;
	}

	public void EndBadBonus(){
		AddScoreWithoutUpsettingMultiplier (badBonusPoints);
	}

    public void Awake()
    {

        if (multiplierVisual.gameObject.activeSelf == false)
        {
			
            scoreMesh.text = string.Format("{0:0000}", PlayerPrefs.GetInt("bestScoreLocal"));
        }
    }

	public void ResetPseudo(int pseudoToSet){

		matchMultipliers = false;
		pseudoMultiplier = pseudoToSet;
	}

	public void Update(){

		if (GameObject.FindObjectOfType<HandleTwoPlayer> ().twoPlayer == false) {

			//if (Input.GetKeyDown (KeyCode.Y)) {

			//ResetPseudo (3);
			//}

			if (matchMultipliers) {
				pseudoMultiplier = multiplier;
			} else {
				float tParam = 0;
				float lerpSpeed = .065f;
				if (tParam < 1) {
					tParam += Time.deltaTime * lerpSpeed;
					pseudoMultiplier = Mathf.Lerp (pseudoMultiplier, multiplier, tParam);
				}

				if (Mathf.Abs (multiplier - pseudoMultiplier) < .5f) {
					pseudoMultiplier = multiplier;
					matchMultipliers = true;
				}

			}

			if (multiplierVisual.gameObject.activeSelf == false) {

				multiplierVisual.gameObject.SetActive (true);
				scoreMesh.text = string.Format ("{0:0000}", score);
				scoreMesh.gameObject.transform.position = new Vector3 (scoreMesh.gameObject.transform.position.x, scoreMesh.gameObject.transform.position.y, scoreMesh.gameObject.transform.position.z);
			}

			///	transform.position = new Vector3 (-16, 6.45f, 2);
			transform.localScale = new Vector3 (2.2f, 2.2f, 1);
		} else {

			//liveVisual.material.SetFloat ("_Cutoff", .1f);

			lifeMesh.text = GameObject.FindObjectOfType<TwoPlayerLives> ().blueLives.ToString();
			lifeMesh2.text = GameObject.FindObjectOfType<TwoPlayerLives> ().purpleLives.ToString();

			//lifeMesh.text = string.Format("{0:0000}", 

			multiplierVisual.material.SetFloat ("_Cutoff", Mathf.Clamp (1 - (multTimer - Mathf.FloorToInt (multTimer)), 0.001f, 1));

			if (matchMultipliers) {
				pseudoMultiplier = multiplier;
			} else {
				float tParam = 0;
				float lerpSpeed = .065f;
				if (tParam < 1) {
					tParam += Time.deltaTime * lerpSpeed;
					pseudoMultiplier = Mathf.Lerp (pseudoMultiplier, multiplier, tParam);
				}

				if (Mathf.Abs (multiplier - pseudoMultiplier) < .5f) {
					pseudoMultiplier = multiplier;
					matchMultipliers = true;
				}

			}
		}

		if (multiplierVisual.gameObject.activeSelf == false) {

			if (GameObject.FindObjectOfType<HandleTwoPlayer> ().twoPlayer) {

				multiplierVisual.gameObject.SetActive (true);
				multiplierVisual2.gameObject.SetActive (true);

				multiplierVisual.gameObject.transform.position = new Vector3 (-7.5f, multiplierVisual2.gameObject.transform.position.y, multiplierVisual2.gameObject.transform.position.z);
				multiplierVisual2.gameObject.transform.position = new Vector3 (7.3f, multiplierVisual.gameObject.transform.position.y, multiplierVisual.gameObject.transform.position.z);

				scoreMesh.text = string.Format ("{0:0000}", score);
				scoreMesh2.gameObject.SetActive (true);
				scoreMesh2.text = string.Format ("{0:0000}", score2);

				scoreMesh.gameObject.transform.position = new Vector3 (-.4f, scoreMesh2.gameObject.transform.position.y, scoreMesh2.gameObject.transform.position.z);
				scoreMesh2.gameObject.transform.position = new Vector3 (2.6f, scoreMesh.gameObject.transform.position.y, scoreMesh.gameObject.transform.position.z);
			}
		}
	}

	public void AddScoreWithoutUpsettingMultiplier(int scoreToAdd){
		score += (multiplier * scoreToAdd);												//add the multiplier to the score
		scoreMesh.text = string.Format ("{0:0000}", score);
	}

	public void AddScore()
	{
		if (GameObject.FindObjectOfType<HandleTwoPlayer> ().twoPlayer == false) {

			if (!GameObject.FindObjectOfType<Line> ().invincibleToColors) {

				score += (multiplier * 2);												//add the multiplier to the score
				scoreMesh.text = string.Format ("{0:0000}", score);
				StartCoroutine (CollectLerp (1f / multiplier));
				audioS.Stop ();
				audioS.PlayOneShot (fillSounds [Random.Range (0, fillSounds.Length - 1)]);
				//if(score >= 500)
				//GiftizBinding.missionComplete();
			} else {
				scoreMesh.text = string.Format ("{0:0000}", score);
				audioS.Stop();
				audioS.PlayOneShot(fillSounds[Random.Range(0,fillSounds.Length-1)]);
			}
		}
	}

	public void AddScoreMultiplier(int player){

		if (player == 1) {

			score += (multiplier * 2);
			scoreMesh.text = string.Format ("{0:0000}", score);
			StartCoroutine (CollectLerp (1f / multiplier));
			audioS.Stop ();
			audioS.PlayOneShot (fillSounds [Random.Range (0, fillSounds.Length - 1)]);
		} else {

			score2 += (multiplier2 * 2);
			scoreMesh2.text = string.Format ("{0:0000}", score2);
			StartCoroutine (CollectLerp2 (1f / multiplier2));
			audioS.Stop ();
			audioS.PlayOneShot (fillSounds [Random.Range (0, fillSounds.Length - 1)]);
		}
	}

	public void UpdateMultiplier()
	{

		if (multTimer > multiplier + 1) {
			multiplier++;
			overflow.Play (false);
			//StopCoroutine (RadialBounce ());
			//StartCoroutine (RadialBounce ());
			Additions ();

			int multiply;
			SteamUserStats.GetStat ("Multiplier", out multiply);

			if (multiplier > multiply) {

				GameObject.FindGameObjectWithTag ("SteamManager").GetComponent<SteamStatsAndAchievements> ().Multiplier = multiplier;
				GameObject.FindGameObjectWithTag ("SteamManager").GetComponent<SteamStatsAndAchievements> ().m_bStoreStats = true;
			}
		}

		if (spawnScr.hazardChance >= 0.3f)
			spawnScr.hazardChance = 0.3f;
		multiMesh.text = string.Format ("{0:00}", multiplier);
		multiplierVisual.material.SetFloat ("_Cutoff", Mathf.Clamp (1 - (multTimer - Mathf.FloorToInt (multTimer)), 0.001f, 1));

		if (multTimer2 > multiplier2 + 1) {

			multiplier2++;
			overflow2.Play (false);

			Additions2Player ();
		}

		if (spawnScr.hazardChance >= 0.3f)
			spawnScr.hazardChance = 0.3f;
		multiMesh2.text = string.Format ("{0:00}", multiplier2);
		multiplierVisual2.material.SetFloat ("_Cutoff", Mathf.Clamp (1 - (multTimer2 - Mathf.FloorToInt (multTimer2)), 0.001f, 1));
	}

	IEnumerator CollectLerp(float amt)
	{

		if (increaseMultiplierGrowth == false) {
			float t = 0, start = multTimer;
			while (t < 1) {
				multTimer = Mathf.Lerp (start, start + amt, t);
				t += Time.deltaTime * 5;
				yield return null;
			}
		} else {

			float t = 0, start = multTimer;
			while (t < 1) {
				multTimer = Mathf.Lerp (start, (start + (amt * 2.3f)), t);
				t += Time.deltaTime * 5;
				yield return null;
			}
		}
	}

	IEnumerator CollectLerp2(float amt)
	{

		if (increaseMultiplierGrowth == false) {
			float t = 0, start = multTimer2;
			while (t < 1) {
				multTimer2 = Mathf.Lerp (start, start + amt, t);
				t += Time.deltaTime * 5;
				yield return null;
			}
		} else {

			float t = 0, start = multTimer2;
			while (t < 1) {
				multTimer2 = Mathf.Lerp (start, (start + (amt * 2.3f)), t);
				t += Time.deltaTime * 5;
				yield return null;
			}
		}
	}

	IEnumerator RadialBounce()
	{
		float t = 0;
		while(t < 1)
		{
			transform.localScale = Vector3.one*Mathf.Lerp(1.25f,1f,t);
			t += Time.deltaTime*5;
			yield return null;
		}
	}

	public void GameOver()
	{
		StopAllCoroutines();
		
		multTimer = 1;
		multiplier = 1;

		scoreMesh.text = "0000";
		multiMesh.text = "01";
        score = 0;
        this.enabled = false;
		gameObject.SetActive(false);
	}
	
	void Additions ()
	{
		if(GameObject.FindObjectOfType<HandleTwoPlayer> ().twoPlayer == false){

			spawnScr.multiLevel ++;
			spawnScr.multiLevel2++;
			spawnScr.hazardChance += 0.005f;
			GameController.instance.colorScr.hueShiftSpeed += 0.01f;
			spawnScr.spawnRateScale += 0.01f;
			spawnScr.ballSpeedScale += 0.01f;
			if (bgScr != null)
				bgScr.rotSpeed += 0.001f;
			if(multiplier > 20)
				spawnScr.homingChance += 0.0005f;
		}
	}

	void Additions2Player ()
	{

		spawnScr.multiLevel++;
		spawnScr.multiLevel2++;
		spawnScr.hazardChance += 0.005f;
		GameController.instance.colorScr.hueShiftSpeed += 0.01f;
		spawnScr.spawnRateScale += 0.01f;
		spawnScr.ballSpeedScale += 0.01f;
		if (bgScr != null)
			bgScr.rotSpeed += 0.001f;
		if (multiplier > 20 || multiplier2 > 20)
			spawnScr.homingChance += 0.0005f;


	}

    //PlayerPrefs
	
	public void ResetMultiplier (bool blue)
	{
		if (GameObject.FindObjectOfType<HandleTwoPlayer> ().twoPlayer == false) {

			if (!GameObject.FindObjectOfType<Line> ().invincibleToColors) {

				matchMultipliers = true;

				//if(multiplier >= 5)
				spawnScr.multiLevel /= 3;

				//leftObject.transform.localScale = new Vector3 (leftObject.transform.localScale.x + .4f, leftObject.transform.localScale.y + .4f, 1);
				//rightObject.transform.localScale = new Vector3 (rightObject.transform.localScale.x + .4f, rightObject.transform.localScale.y + .4f, 1);

				if (GameObject.FindObjectOfType<HandleTwoPlayer> ().twoPlayer == false) {

					if (leftObject2.activeSelf) {

						leftObject2.GetComponent<TestFollower2> ().IncreaseScale ();
					}
					if (rightObject2.activeSelf) {

						rightObject2.GetComponent<TestFollower2> ().IncreaseScale ();
					}

					leftObject.GetComponent<TestFollower> ().IncreaseScale ();
					rightObject.GetComponent<TestFollower> ().IncreaseScale ();

					GameObject[] growParticle = GameObject.FindGameObjectsWithTag ("Doo");

					foreach (GameObject grop in growParticle) {

						grop.GetComponent<ScaleToParent> ().DooParticle ();
					}
				}

				multiplier = 1;
				multTimer = 1;
				if (bgScr != null)
					bgScr.rotSpeed -= 0.01f;
			}
		} else {

			if (blue) {

				Line[] lines = GameObject.FindObjectsOfType<Line> ();

				foreach (Line g in lines) {

					if (g.player1) {

						g.HitBad (null);
					}
				}

				if (!GameObject.FindObjectOfType<HandleTwoPlayer> ().twoPlayer) {
					matchMultipliers = true;
					spawnScr.multiLevel /= 3;

					multiplier = 1;
					multTimer = 1;
					if (bgScr != null)
						bgScr.rotSpeed -= 0.01f;
				}
			} else {

				Line[] lines = GameObject.FindObjectsOfType<Line> ();

				foreach (Line g in lines) {

					if (g.player1 == false) {

						g.HitBad (null);
					}
				}

				if (!GameObject.FindObjectOfType<HandleTwoPlayer> ().twoPlayer) {
					matchMultipliers = true;
					spawnScr.multiLevel /= 3;

					multiplier = 1;
					multTimer = 1;
					if (bgScr != null)
						bgScr.rotSpeed -= 0.01f;
				}
			}
		}
	}
}
