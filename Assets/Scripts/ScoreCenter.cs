using UnityEngine;
using System.Collections;
//using GooglePlayGames;
//using GooglePlayGames.BasicApi;
using UnityEngine.SocialPlatforms;
using Steamworks;

public class ScoreCenter : MonoBehaviour 
{
	public Spawner spawnScr;
	public BGEffects bgScr;
	public GameObject floatingTextInst;
	public AudioSource audioS;
	public AudioClip[] fillSounds;
	public Renderer multiplierVisual;
	public ParticleSystem overflow;
	public TextMesh multiMesh, scoreMesh;
	public int score, multiplier = 1;
	public float period;
	public Animator scoreAnim;

	public GameObject rightObject;
	public GameObject leftObject;

	bool lerping;
	float multTimer = 2f;

    public void Awake()
    {

        if (multiplierVisual.gameObject.activeSelf == false)
        {


            scoreMesh.text = string.Format("{0:0000}", PlayerPrefs.GetInt("bestScoreLocal"));
        }
    }

	public void Update(){

        if (multiplierVisual.gameObject.activeSelf == false)
        {

            multiplierVisual.gameObject.SetActive(true);
            scoreMesh.text = string.Format("{0:0000}", score);
            scoreMesh.gameObject.transform.position = new Vector3(scoreMesh.gameObject.transform.position.x - 1.2f, scoreMesh.gameObject.transform.position.y, scoreMesh.gameObject.transform.position.z);
        }

	///	transform.position = new Vector3 (-16, 6.45f, 2);
		transform.localScale = new Vector3 (2.2f, 2.2f, 1);
	}

	public void AddScore()
	{
		//if(GameController.instance.signedIn)
		//{
			//GameController.instance.BallCountCheck();
		//	PlayGamesPlatform.Instance.Events.IncrementEvent("CgkI052F_vMSEAIQBw", 1);
		//}
		score += (multiplier * 2);												//add the multiplier to the score
		scoreMesh.text = string.Format("{0:0000}", score);
		StartCoroutine(CollectLerp(1f/multiplier));
		audioS.Stop();
		audioS.PlayOneShot(fillSounds[Random.Range(0,fillSounds.Length-1)]);
		//if(score >= 500)
			//GiftizBinding.missionComplete();
	}

	public void UpdateMultiplier()
	{
		/*if(multTimer > 1)
			multTimer -= Time.deltaTime/period;
		else
			multTimer = 1;*/
		if(multTimer > multiplier+1)
		{
			multiplier++;
			overflow.Play(false);
			StopCoroutine(RadialBounce());
			StartCoroutine(RadialBounce());
			Additions();

            int multiply;
            SteamUserStats.GetStat("Multiplier", out multiply);

            if (multiplier > multiply)
            {

                GameObject.FindGameObjectWithTag("SteamManager").GetComponent<SteamStatsAndAchievements>().Multiplier = multiplier;
                GameObject.FindGameObjectWithTag("SteamManager").GetComponent<SteamStatsAndAchievements>().m_bStoreStats = true;
            }
		}
		/*else if(multTimer < multiplier && multTimer > 1)
		{
			multiplier--;
			spawnScr.hazardChance -= 0.01f;
		}*/
		if(spawnScr.hazardChance >= 0.3f)
			spawnScr.hazardChance = 0.3f;
		multiMesh.text = string.Format("{0:00}", multiplier);
		multiplierVisual.material.SetFloat("_Cutoff",Mathf.Clamp(1-(multTimer-Mathf.FloorToInt(multTimer)),0.001f,1));
	}

	IEnumerator CollectLerp(float amt)
	{
		float t = 0, start = multTimer;
		while(t < 1)
		{
			multTimer = Mathf.Lerp(start,start+amt,t);
			t += Time.deltaTime*5;
			yield return null;
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
	
	/*IEnumerator ResetShake()
	{
		float t = 0;
		while(t < 1)
		{
			transform.localPosition = Vector3.Lerp (pos1,pos2,Mathf.PingPong(Time.time,t));
			t += Time.deltaTime*2;
			yield return null;
		}
	}*/

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
		spawnScr.multiLevel ++;
		spawnScr.multiLevel2++;
		spawnScr.hazardChance += 0.005f;
		GameController.instance.colorScr.hueShiftSpeed += 0.01f;
		spawnScr.spawnRateScale += 0.01f;
		spawnScr.ballSpeedScale += 0.01f;
		bgScr.rotSpeed += 0.001f;
		if(multiplier > 20)
			spawnScr.homingChance += 0.0005f;
	}

    //PlayerPrefs
	
	public void ResetMultiplier ()
	{
		//if(multiplier >= 5)
		spawnScr.multiLevel /= 3;

        //leftObject.transform.localScale = new Vector3 (leftObject.transform.localScale.x + .4f, leftObject.transform.localScale.y + .4f, 1);
        //rightObject.transform.localScale = new Vector3 (rightObject.transform.localScale.x + .4f, rightObject.transform.localScale.y + .4f, 1);

        leftObject.GetComponent<TestFollower>().IncreaseScale();
        rightObject.GetComponent<TestFollower>().IncreaseScale();

        GameObject[] growParticle = GameObject.FindGameObjectsWithTag("Doo");

        foreach (GameObject grop in growParticle)
        {

            grop.GetComponent<ScaleToParent>().DooParticle();
        }

        //Vector3 newScale = new Vector3 (leftObject.transform.localScale.x + .2f, leftObject.transform.localScale.y + .2f, 1);
        //leftObject.transform.localScale = Vector3.Lerp (leftObject.transform.localScale, newScale, 1);

        //newScale = new Vector3 (rightObject.transform.localScale.x + .2f, rightObject.transform.localScale.y + .2f, 1);
        //leftObject.transform.localScale = Vector3.Lerp (leftObject.transform.localScale, newScale, 1);

        ///leftObject.transform.position = new Vector2 (leftObject.transform.position.x - 1, leftObject.transform.position.y);
        ///rightObject.transform.position = new Vector2 (rightObject.transform.position.x + 1, rightObject.transform.position.y);

        //scoreAnim.Play("ScoreShake",-1,0f);
        multiplier = 1;
		multTimer = 1;
		bgScr.rotSpeed -= 0.01f;
	}
}
