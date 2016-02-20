using UnityEngine;
using System.Collections;
//using GooglePlayGames;
//using GooglePlayGames.BasicApi;
//using GoogleMobileAds;
//using GoogleMobileAds.Api;
using UnityEngine.SocialPlatforms;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour 
{
	public static GameController instance;									//gamecontroller script reference for all other scripts
	public BGEffects bgScr;
	public Spawner spawnScr;
	public ScoreCenter scoreScr;
	public Line lineScr;
	public TallyScore tallyScr;
	public ColorUtility colorScr;
	//public AdHandler adScr;
	public AudioSource audioS;												//references for all other scripts that gamecontroller communicates with
	//public Animator anim;
	public Camera bgEffects;
	public GameObject title, tutorial, leaderButt, achieveButt;
	public SpriteRenderer lineRend;
	public Transform animationSlave;
	//pausepublic Transform[] pauseQuads;
	public bool gameOver, signedIn;
	public GameObject flobeTitle;
	public GUISkin tallyStyle;
	public float offset;

	public AudioClip introSong;
	public AudioClip[] songs;

	public GameObject currentBackground;
	public GameObject[] backgrounds;

	//public GameObject line;

	public GameObject leftObject;
	public GameObject rightObject;
	
	/*//giftiz things
	public static Rect m_screenRect	= new Rect(0, 0, 640, 960);
	public Texture2D m_textureInvisible	= null;
	public Texture2D m_textureNaked	= null;
	public Texture2D m_textureBadge	= null;
	public Texture2D m_textureWarning = null;
	public GUIStyle m_styleButton = null;
	public Rect m_giftizButton;*/

	public bool animationsReady, failAnimationComplete, preGame = true;
	Vector3[] lastTouches, unpausePoints = new Vector3[2];
	int sessionBalls, retries;

	void Awake()
	{
		instance = this;
	}

	void Start()
	{

      //  Time.timeScale

		//PlayGamesPlatform.Activate();									//On start the game begins Google Play sign in process
		//anim.StopPlayback();
		
		if(PlayerPrefs.HasKey("ballTotal"))
		{
			sessionBalls = PlayerPrefs.GetInt("ballTotal");				//if the player has not played the game before their ball total will be 0 and will be set as such. This is to track achievements.
		}
		else
			PlayerPrefs.SetInt("ballTotal", 0);
		if(PlayerPrefs.HasKey ("timesPlayed"))
		{
			retries = PlayerPrefs.GetInt("timesPlayed");				//if the player has not played the game before, timesplayed will be 0. This is to track a short difficulty modifier added to the start of the game to get things moving faster if they have played before.
		}
		else 
			PlayerPrefs.SetInt("timesPlayed", 0);
		StartCoroutine(CheckAnimations());
		//adScr.RequestBanner(GoogleMobileAds.Api.AdSize.SmartBanner,GoogleMobileAds.Api.AdPosition.Top);

		audioS.clip = introSong;

		foreach (GameObject bg in backgrounds) {
			
			bg.SetActive(false);
		}
		
		currentBackground = backgrounds[Random.Range(0, backgrounds.Length)];
		currentBackground.SetActive(true);
	}

	IEnumerator CheckAnimations()
	{
		foreach(Transform t in animationSlave)
		{
			if(!t.GetComponent<AtlasSplitter>().ready)
				yield return null;
		}
		animationsReady = true;
	}

	void Update(){

		if (!preGame && !gameOver) {

            //Debug.Log("KEK!");
			
			if(Input.GetKeyDown (KeyCode.Space) || Input.GetKeyDown(KeyCode.Joystick1Button7)){

                Debug.Log(Time.timeScale);

                if (Time.timeScale == 1)
                {
                    Debug.Log("WHY!!!!");
                    Time.timeScale = 0;
                }else
                if (Time.timeScale == 0)
                {

                    Time.timeScale = 1;
                }
            }
		}

		if (preGame) {

			

            if (Input.GetKeyDown(KeyCode.Space))
            {
                GameObject[] followers = GameObject.FindGameObjectsWithTag("Follower");
                foreach (GameObject follower in followers)
                {

                    follower.GetComponent<TestFollower>().isWASD = true;
                }
                GameBegin();
            }
            if (Input.GetKeyDown(KeyCode.Joystick1Button7))
            {
                GameObject[] followers = GameObject.FindGameObjectsWithTag("Follower");
                foreach (GameObject follower in followers)
                {

                    follower.GetComponent<TestFollower>().isWASD = false;
                }
                GameBegin();
            }
        }

		if(Input.GetKeyDown(KeyCode.Joystick1Button6)){
			
			Application.Quit();
		}

		if (Input.GetKeyDown (KeyCode.Escape)) {

			Application.Quit();
		}

	}

	void FixedUpdate()
	{

		if(preGame)											//if pregame setup is running
		{
			if (Input.touchCount == 1)
			{
			Vector3 touch = Camera.main.ScreenToWorldPoint(Input.GetTouch(0).position);
			Vector2 touchPos = new Vector2(touch.x, touch.y);
				//if(leaderButt.GetComponent<Collider2D>() == Physics2D.OverlapPoint(touchPos))
					//Social.ShowLeaderboardUI();
				//if(achieveButt.GetComponent<Collider2D>() == Physics2D.OverlapPoint(touchPos))
					//Social.ShowAchievementsUI();
			}
			if(animationsReady && Input.touches.Length > 1)
				GameBegin();								//begin the game (pregame = false, spawn + score scripts on)

		}
		else if(!gameOver) 						//if the game isn't pregame, paused or in the gameOver state
		{
			//if(Input.touches.Length > 1)
			{
				scoreScr.UpdateMultiplier();
				//lastTouches = new Vector3[]{Input.touches[0].position,Input.touches[1].position};

				lastTouches = new Vector3[]{

					rightObject.transform.position,
					leftObject.transform.position
				};

				lineScr.UpdateLine(lastTouches);

				if(rightObject.GetComponent<TestFollower> ().useParticle){
					
					ParticleSystem[] particles0 = lineScr.pieces[0].GetComponentsInChildren<ParticleSystem> ();
					
					foreach(ParticleSystem newPartcile in particles0){
						
						newPartcile.enableEmission = true;
					}
					
					//lineScr.pieces[0].GetComponentsInChildren<ParticleSystem> ().enableEmission = true;
				}else{
					
					ParticleSystem[] particles0 = lineScr.pieces[0].GetComponentsInChildren<ParticleSystem> ();
					
					foreach(ParticleSystem newPartcile in particles0){
						
						newPartcile.enableEmission = false;
					}
					
					//lineScr.pieces[0].GetComponentsInChildren<ParticleSystem> ().enableEmission = false;
				}

				if(leftObject.GetComponent<TestFollower> ().useParticle){

					ParticleSystem[] particles0 = lineScr.pieces[1].GetComponentsInChildren<ParticleSystem> ();
					
					foreach(ParticleSystem newPartcile in particles0){
						
						newPartcile.enableEmission = true;
					}
				}else{

					ParticleSystem[] particles0 = lineScr.pieces[1].GetComponentsInChildren<ParticleSystem> ();
					
					foreach(ParticleSystem newPartcile in particles0){
						
						newPartcile.enableEmission = false;
					}
				}

				audioS.pitch += 0.00001f;
			}
	
				//Pause(true);
		}

	}
	
	/*public void BallCountCheck()
	{
		sessionBalls++;
		Debug.Log (sessionBalls);
		switch(sessionBalls)
		{
		case 100:
			Social.ReportProgress("CgkI052F_vMSEAIQAQ", 100.0f, (bool success) => {});
			break;
		case 500:
			Social.ReportProgress("CgkI052F_vMSEAIQAg", 100.0f, (bool success) => {});
			break;
		case 1000:
			Social.ReportProgress("CgkI052F_vMSEAIQAw", 100.0f, (bool success) => {});
			break;
		case 2000:
			Social.ReportProgress("CgkI052F_vMSEAIQBA", 100.0f, (bool success) => {});
			break;
		case 4000:
			Social.ReportProgress("CgkI052F_vMSEAIQBQ", 100.0f, (bool success) => {});
			break;
		}
	}*/

	IEnumerator MusicFade(int start)
	{
		float t = 0;
		while(t < 1)
		{
			t += Time.deltaTime;
			audioS.pitch = Mathf.Lerp(start,1-start,t);
			yield return null;
		}
	}

	void GameBegin()
	{
		preGame = false;
		StartCoroutine(MoveTitleUp());
		lineScr.Active(true);
		lineScr.UpdateLine (new Vector3[]{

			rightObject.transform.position,
			leftObject.transform.position
		});
		//lineScr.UpdateLine(new Vector3[]{Input.touches[0].position,Input.touches[1].position});
		spawnScr.hazardChance = 0.15f;
		spawnScr.StartCoroutine("Spawn");
		scoreScr.gameObject.SetActive(true);
		scoreScr.enabled = true;
		//adScr.ShowBanner();

		audioS.clip = songs[Random.Range(0, songs.Length)];
		audioS.Play ();

	}

	IEnumerator EndGame()
	{
		//anim.enabled = true;
		gameOver = true;
		StartCoroutine(MusicFade(1));
		spawnScr.StopAllCoroutines();
		foreach(GameObject b in GameObject.FindGameObjectsWithTag("Ball"))
		{
			var rb = b.GetComponent<Rigidbody2D>();
			if(rb)
			{
				b.GetComponent<Rigidbody2D>().isKinematic = true;
				Destroy(b,2f);
			}
			else
				print(b.name);
		}

		foreach(GameObject b in GameObject.FindGameObjectsWithTag("Purple"))
		{
			var rb = b.GetComponent<Rigidbody2D>();
			if(rb)
			{
				b.GetComponent<Rigidbody2D>().isKinematic = true;
				Destroy(b,2f);
			}
			else
				print(b.name);
		}
		yield return new WaitForSeconds(2f);

		ShowTally();
	}

	void ShowTally()
	{

        if (tallyScr.enabled == false)
        {

            Debug.Log("CheckEd!");

            //adScr.HideBanner();
            lineScr.Active(false);
            tallyScr.enabled = true;
            tallyScr.GameOver(scoreScr.score);
            scoreScr.GameOver();
        }
        
	}

	public void ResetGame()
	{
		spawnScr.hitMulti = false;
		spawnScr.hitMulti2 = false;
		spawnScr.hitMulti3 = false;

		//flobeTitle.Play("FlobeTitle",-1,0f);
		StartCoroutine(MusicFade(0));
		tallyScr.enabled = false;
		//paused = false;
		gameOver = false;
		preGame = true;

		lineScr.GetComponent<Line> ().hitMultiplier = false;

		lineScr.purple.transform.localPosition = lineScr.purple.GetComponent<Scale1Direction> ().originalPosition;
		lineScr.purple.transform.localScale = lineScr.purple.GetComponent<Scale1Direction> ().originalScale;

		lineScr.purple2.transform.localPosition = lineScr.purple2.GetComponent<Scale1Direction> ().originalPosition;
		lineScr.purple2.transform.localScale = lineScr.purple2.GetComponent<Scale1Direction> ().originalScale;

		//rightObject.transform.position = rightObject.GetComponent<TestFollower> ().startPos;
		//leftObject.transform.position = leftObject.GetComponent<TestFollower> ().startPos;
		leftObject.GetComponent<TestFollower> ().ResetPosition ();
		rightObject.GetComponent<TestFollower> ().ResetPosition ();

		GameBegin ();
		//tutorial.SetActive(true);
		bgScr.rotSpeed = 0.05f;
		retries ++;
		spawnScr.multiLevel = 1 + retries/2;
		spawnScr.multiLevel2 = 1 + retries/2;
		if(spawnScr.multiLevel >= 10)
			spawnScr.multiLevel = 10;
		//skinScr.enabled = true;

		audioS.clip = songs[Random.Range(0, songs.Length)];
		audioS.Play ();

		foreach (GameObject bg in backgrounds) {
			
			bg.SetActive(false);
		}
		
		currentBackground = backgrounds[Random.Range(0, backgrounds.Length)];
		currentBackground.SetActive(true);

        //Application.LoadLevel(0);
        SceneManager.LoadScene(1);
	}
		
	

	
	
	bool TouchRepeatButton(Vector2 position, float size, Vector2 check)
	{
		return Vector2.Distance(position,check) < size;
	}
	
	void OnApplicationQuit ()
	{
		PlayerPrefs.SetInt("ballTotal", sessionBalls);
	}
	
	IEnumerator MoveTitleUp ()
	{
		tutorial.SetActive(false);
        //leaderButt.SetActive (false);
        //achieveButt.SetActive(false);
        //flobeTitle.SetFloat("AnimSpeed",-1.5f);

        if (flobeTitle != null)
        {

            flobeTitle.gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 5);
        }

        Destroy(flobeTitle.gameObject, 3f);
		//flobeTitle.Play("FlobeTitle");
		yield return null;
	}
	
	/*public static Rect UpdateDimension(Rect position, float dy = 0.0f)
	{
		position.y += dy;
		return (new Rect(
			(position.x / m_screenRect.width) * Screen.width,
			(position.y / m_screenRect.height) * Screen.height,
			(position.width / m_screenRect.width) * Screen.width,
			(position.height / m_screenRect.height) * Screen.height
			));
	}
	
	public void OnGUI () 
	{
		m_styleButton.active.background = GetGiftizButtonTexture(); // get correct texture
		m_styleButton.normal.background = m_styleButton.active.background; 
		if (GUI.Button(UpdateDimension(m_giftizButton), "", m_styleButton) == true) 
			GiftizBinding.buttonClicked(); // Giftiz Button has been clicked
	} 
	
	public Texture2D GetGiftizButtonTexture () 
	{ 
		Texture2D inter = null; 
		// depending on button state, select the right image texture
		switch (GiftizBinding.giftizButtonState) 
		{ 
			case GiftizBinding.GiftizButtonState.Invisible : inter = m_textureInvisible; break; 	
			case GiftizBinding.GiftizButtonState.Naked : inter = m_textureNaked; break; 
			case GiftizBinding.GiftizButtonState.Badge : inter = m_textureBadge; break; 	
			case GiftizBinding.GiftizButtonState.Warning : inter = m_textureWarning; break; 
		} 
		return inter; 
	}*/
}
