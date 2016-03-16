using UnityEngine;
using System.Collections;
using Steamworks;

public class Line : MonoBehaviour 
{
	#region variables

	public bool twoPlayer;
	public bool player1;

    public float purpBlueTimer;
    public float maxPurpBlueTimer;

    public bool hitABlue;
    public bool hitAPurp;

    public int min;
    public int sec;

    public float timeCount;
    public float startTime;

   // public float gameTime;

    public GameObject purpleFill;

	public Spawner spawnScr;
	public ScoreCenter scoreScr;
	public Transform[] pieces;
	public GameObject collectSplat, hitEffect;
	public AudioSource audioS;
	public AudioClip[] failSounds;

	public GameObject glow1, glow2;

	public float rightTrigger;
	public float rightTriggerLast;

	public GameObject bluePop;

	public GameObject purple, purple2;
	public bool hitMultiplier;

	public float timer;
	float maxTimer;

	public bool decreaseTimer;

	#endregion 

	public void Start(){

		maxTimer = timer;

        startTime = Time.time;

        maxPurpBlueTimer = purpBlueTimer;

        purpBlueTimer = 0;
	}

	public void Active(bool on)
	{
		for (int i = 0; i < 3; i++) 
			pieces[i].gameObject.SetActive(on);
	}

	public void Update(){

		//Time.timeScale = 0;

        if (purpBlueTimer > 0)
        {

            purpBlueTimer -= Time.timeScale;
        }
        else
        {

            hitAPurp = false;
            hitABlue = false;
        }

        if (hitABlue && hitAPurp)
        {

            GameObject.FindGameObjectWithTag("SteamManager").GetComponent<SteamStatsAndAchievements>().UnlockAchievement(new SteamStatsAndAchievements.Achievement_t(SteamStatsAndAchievements.Achievement.ACH_DOUBLE_DIPPER, "ACH_DOUBLE_DIPPER", "a"));
        }

        timeCount = Time.time * 2 - startTime;
        min = (int)(timeCount / 60);
        sec = (int)(timeCount % 60);

        if (min == 5)
        {

            GameObject.FindGameObjectWithTag("SteamManager").GetComponent<SteamStatsAndAchievements>().UnlockAchievement(new SteamStatsAndAchievements.Achievement_t(SteamStatsAndAchievements.Achievement.ACH_SURVIVOR, "ACH_SURVIVOR", "a"));
        }

		rightTrigger = Input.GetAxis ("JoystickRightTrigger");

		if (scoreScr.GetComponent<ScoreCenter> ().multiplier >= 10 && !hitMultiplier) {

			if (!twoPlayer) {
				if (purpleFill.GetComponent<ParticleSystem>().isPlaying == false)
				{

					purpleFill.GetComponent<ParticleSystem>().Play();
				}

				hitMultiplier = true;
			}
		
			//Debug.Log("DO GLOW THINGY!");
            GameObject.FindGameObjectWithTag("SteamManager").GetComponent<SteamStatsAndAchievements>().UnlockAchievement(new SteamStatsAndAchievements.Achievement_t(SteamStatsAndAchievements.Achievement.ACH_DOUBLE_TROUBLE, "ACH_DOUBLE_TROUBLE", "a"));
           // GameObject.FindGameObjectWithTag("SteamManager").GetComponent<SteamStatsAndAchievements> ().UnlockAchievement(GameObject.FindGameObjectWithTag("SteamManager").GetComponent<SteamStatsAndAchievements> ().)
        }

		if (!twoPlayer) {
			if (hitMultiplier) {

				if(!glow1.activeSelf){

					glow1.SetActive(true);
					glow2.SetActive(true);
				}

				if(!purple2.activeSelf)
					purple.SetActive (true);
			} else {

				glow1.SetActive(false);
				glow2.SetActive (false);

				purple.SetActive(false);
				purple2.SetActive(false);
			}

			if (purple.activeSelf) {

				//if (Input.GetKeyDown (KeyCode.Joystick1Button5)) {

				if((rightTrigger == -1 && rightTriggerLast != -1) || Input.GetKeyDown(KeyCode.LeftShift)){

					purple.SetActive(false);
					purple2.SetActive(true);
				}
			} else if (purple2.activeSelf) {

				if((rightTrigger == -1 && rightTriggerLast != -1) || Input.GetKeyDown(KeyCode.LeftShift)){

					purple.SetActive(true);
					purple2.SetActive(false);
				}
			}
		}

		rightTriggerLast = Input.GetAxis ("JoystickRightTrigger");
	}

	public void UpdateLine(Vector3[] ts)
	{
		//Vector3[] wPos = new Vector3[]{Camera.main.ScreenToWorldPoint(ts[0]),Camera.main.ScreenToWorldPoint(ts[1])};

		Vector3[] wPos = new Vector3[]{

			ts [0],
			ts [1]
		};
		Vector2 center = (wPos[0]+wPos[1])/2;
		transform.Translate((Vector3)center-transform.position,Space.World);
		transform.LookAt((Vector2)wPos[0],Vector3.right);
		transform.Rotate(0,90,0);
		transform.localScale = new Vector3(Vector3.Distance(wPos[0],wPos[1]),(Vector3.Angle(Vector3.Cross(wPos[0],wPos[1]),transform.up) > 45f ? 1 : -1)*0.5f,transform.localScale.z);

		for(int i = 0; i < 2; i++)
		{
			Vector2 pos = (Vector2)wPos[i];
			pieces[i].position = pos;
			pieces[i].right = (center-pos).normalized;
		}
		
		float dist = Vector3.Distance (wPos[0],wPos[1]);
		//Debug.Log (dist);
		//if(dist < 0.6f)
			//GameController.instance.SendMessage("Pause");
	}

	void OnTriggerStay2D(Collider2D obj){

        if (GameController.instance.gameOver)
            return;
        CheckHit(obj.gameObject, obj.transform.position, obj);

        if (obj.gameObject.layer == 10) {

			if(obj.gameObject.GetComponent<Bomb>() != null){
				
                if(obj.gameObject.GetComponent<Bomb> ().moveSpeed > 0)
				    obj.gameObject.GetComponent<Bomb> ().Boom();
			}
		}
	}

	void CheckHit(GameObject obj, Vector3 point, Collider2D coll)
	{

		if (obj.layer == 9) {

			if (hitMultiplier) {
				
				if(Vector2.Distance(obj.transform.position, pieces[0].position) < Vector2.Distance(obj.transform.position, pieces[1].position)){

					if(purple.activeSelf){

						if(obj.tag == "Purple"){
							
							HitPurpleGood (obj);
						}else if(obj.tag == "Ball"){
							
							HitBad ();
                        }
					}else if(purple2.activeSelf){

						if(obj.tag == "Ball"){

							HitBlueGood (obj);
						}else if(obj.tag == "Purple"){
							
							HitBad ();
                        }
					}
				}else{

					if(purple.activeSelf){

						if(obj.tag == "Ball"){

							HitBlueGood (obj);
						}else if(obj.tag == "Purple"){
							
							HitBad ();
                        }
					}else if(purple2.activeSelf){

						if(obj.tag == "Purple"){

							HitPurpleGood (obj);
						}else if(obj.tag == "Ball"){
							
							HitBad ();
                        }
					}
				}
			} else {

				HitBlueGood (obj);
			}		




		} else if (obj.layer == 10) {

			if(obj.gameObject.GetComponent<Ball> () != null){
				if(obj.gameObject.GetComponent<Ball> ().pop != null){
                    if (obj.GetComponent<Bomb>() == null)
                    {
                        Destroy(Instantiate(bluePop, obj.transform.position, Quaternion.identity), 1);
                    }
                }
			}
            if (obj.gameObject.GetComponent<Bomb>() != null)
            {
                CircleCollider2D[] cirCols = obj.GetComponents<CircleCollider2D>();
                foreach (CircleCollider2D cirCol2D in cirCols)
                {
                    if (coll.isTrigger)
                    {
                    }
                    else
                    {
						HitBad ();
                    }
                }
                obj.gameObject.GetComponent<Bomb>().warning.SetActive(false);
            }
            else
            {

				HitBad ();
            }
		}
	}

	void HitPurpleGood(GameObject objJ){
		Destroy(Instantiate(bluePop, objJ.transform.position, Quaternion.identity), 1);
		GameObject.FindGameObjectWithTag("SteamManager").GetComponent<SteamStatsAndAchievements>().PurplesCollected++;
		GameObject.FindGameObjectWithTag("SteamManager").GetComponent<SteamStatsAndAchievements>().m_bStoreStats = true;
		hitAPurp = true;
		purpBlueTimer = 20f;
		scoreScr.AddScore ();
		objJ.SendMessage ("DestroyBall");
	}

	void HitBlueGood(GameObject objJ){
		if (objJ.GetComponent<Bomb>() == null)
		{
			Destroy(Instantiate(bluePop, objJ.transform.position, Quaternion.identity), 1);
		}
		GameObject.FindGameObjectWithTag("SteamManager").GetComponent<SteamStatsAndAchievements>().BluesCollected++;
		GameObject.FindGameObjectWithTag("SteamManager").GetComponent<SteamStatsAndAchievements>().m_bStoreStats = true;
		hitABlue = true;
		purpBlueTimer = 20f;
		scoreScr.AddScore ();
		objJ.SendMessage ("DestroyBall");
	}

	void HitBad(){
		audioS.PlayOneShot (failSounds [Random.Range (0, failSounds.Length - 1)]);
		GameController.instance.StartCoroutine ("EndGame");
		GetComponent<Line>().enabled = false;
	}
}