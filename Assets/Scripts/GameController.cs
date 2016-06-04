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
	#region variables

	public GameObject playerClicker;

	public bool gameHasBegun;

	public static GameController instance;									//gamecontroller script reference for all other scripts
	public BGEffects bgScr;
	public Spawner spawnScr;
	public ScoreCenter scoreScr;
	public Line lineScr;
	public Line lineScr2;
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

	public GameObject leftObject2;
	public GameObject rightObject2;

	public bool animationsReady, failAnimationComplete, preGame = true;
	Vector3[] lastTouches, lastTouches2, unpausePoints = new Vector3[2];
	int sessionBalls, retries;


	[SerializeField] private StartInstructionsFade mStartInstructions; //For having instructions fade out when the game starts ~Adam

	#endregion

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

	public void DoThisToBegin(bool wasd){

		if (wasd) {

			GameObject[] followers = GameObject.FindGameObjectsWithTag ("Follower");
			foreach (GameObject follower in followers) {

				follower.GetComponent<TestFollower> ().isWASD = true;
			}
			GameBegin ();
		} else {

			GameObject[] followers = GameObject.FindGameObjectsWithTag("Follower");
			foreach (GameObject follower in followers)
			{

				follower.GetComponent<TestFollower>().isWASD = false;
			}
			GameBegin();
		}
	}

	void Update(){

		if (!preGame && gameHasBegun && !gameOver) {

            //Debug.Log("KEK!");
			
			if(Input.GetKeyDown (KeyCode.Space) || Input.GetKeyDown(KeyCode.Joystick1Button7) || Input.GetKeyDown(KeyCode.Joystick2Button7)){

                //Debug.Log(Time.timeScale);

                if (Time.timeScale == 1)
                {
                    
                    Time.timeScale = 0;
                }else
                if (Time.timeScale == 0)
                {

                    Time.timeScale = 1;
                }
            }
		}

		if (Input.GetKeyUp (KeyCode.Space) || Input.GetKeyUp (KeyCode.Joystick1Button7)) {

			gameHasBegun = true;
		}

		if (preGame) 
		{

			if (Input.GetKeyDown (KeyCode.Space)) {

				playerClicker.SetActive (true);
			}

			if (Input.GetKeyDown (KeyCode.JoystickButton7) || Input.GetKeyDown(KeyCode.Joystick2Button7)) {

				playerClicker.SetActive (true);
			}

			/*if ( (Application.platform != RuntimePlatform.LinuxPlayer && Input.GetKeyDown(KeyCode.Space)) ||
				(Application.platform == RuntimePlatform.LinuxPlayer && Input.GetKeyDown(KeyCode.F)) )
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
            }*/
        }

		if(Input.GetKeyDown(KeyCode.Joystick1Button6) || Input.GetKeyDown(KeyCode.Joystick2Button6)){
			
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

				if (lineScr.twoPlayer) {
					lastTouches2 = new Vector3[]{

						rightObject2.transform.position,
						leftObject2.transform.position
					};

					lineScr2.UpdateLine(lastTouches2);
				}

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

				if (lineScr.twoPlayer) {

					if(rightObject2.GetComponent<TestFollower2> ().useParticle){

						ParticleSystem[] particles0 = lineScr2.pieces[0].GetComponentsInChildren<ParticleSystem> ();

						foreach(ParticleSystem newPartcile in particles0){

							newPartcile.enableEmission = true;
						}

						//lineScr.pieces[0].GetComponentsInChildren<ParticleSystem> ().enableEmission = true;
					}else{

						ParticleSystem[] particles0 = lineScr2.pieces[0].GetComponentsInChildren<ParticleSystem> ();

						foreach(ParticleSystem newPartcile in particles0){

							newPartcile.enableEmission = false;
						}

						//lineScr.pieces[0].GetComponentsInChildren<ParticleSystem> ().enableEmission = false;
					}

					if(leftObject2.GetComponent<TestFollower2> ().useParticle){

						ParticleSystem[] particles0 = lineScr2.pieces[1].GetComponentsInChildren<ParticleSystem> ();

						foreach(ParticleSystem newPartcile in particles0){

							newPartcile.enableEmission = true;
						}
					}else{

						ParticleSystem[] particles0 = lineScr2.pieces[1].GetComponentsInChildren<ParticleSystem> ();

						foreach(ParticleSystem newPartcile in particles0){

							newPartcile.enableEmission = false;
						}
					}
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
		GameObject.FindObjectOfType<HandleTwoPlayer> ().WhenGameBegins ();

		preGame = false;
		StartCoroutine(MoveTitleUp());
		lineScr.Active(true);
		lineScr.UpdateLine (new Vector3[]{

			rightObject.transform.position,
			leftObject.transform.position
		});

		if (lineScr.twoPlayer) {

			lineScr2.Active(true);
			lineScr2.UpdateLine (new Vector3[]{

				rightObject2.transform.position,
				leftObject2.transform.position
			});
		}
		//lineScr.UpdateLine(new Vector3[]{Input.touches[0].position,Input.touches[1].position});
		spawnScr.hazardChance = 0.15f;
		spawnScr.StartCoroutine("Spawn");
		scoreScr.gameObject.SetActive(true);
		scoreScr.enabled = true;
		//adScr.ShowBanner();

		if(!PlayerPrefs.HasKey("LastSong")){

			PlayerPrefs.SetInt ("LastSong", 12);
		}

		PlaySongs ();

		//audioS.clip = songs[Random.Range(0, songs.Length)];
		//audioS.Play ();

		//For having instructions fade out when the game starts ~Adam
		if(mStartInstructions != null)
		{
			mStartInstructions.StartFade();
		}
	}

	void PlaySongs(){

		int songNum = Random.Range (0, songs.Length);

		if (songNum == PlayerPrefs.GetInt ("LastSong")) {

			PlaySongs ();
		} else {

			audioS.clip = songs [songNum];
			audioS.Play ();

			PlayerPrefs.SetInt ("LastSong", songNum);
		}
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

            //Debug.Log("CheckEd!");

            //adScr.HideBanner();
            lineScr.Active(false);
			if(lineScr.twoPlayer) lineScr2.Active(false);
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

		lineScr.GetComponent<Line> ().multiplierOnePlayer10 = false;

		//!!lineScr.purple.transform.localPosition = lineScr.purple.GetComponent<Scale1Direction> ().originalPosition;
		//!!lineScr.purple.transform.localScale = lineScr.purple.GetComponent<Scale1Direction> ().originalScale;

		//!!lineScr.purple2.transform.localPosition = lineScr.purple2.GetComponent<Scale1Direction> ().originalPosition;
		//!!lineScr.purple2.transform.localScale = lineScr.purple2.GetComponent<Scale1Direction> ().originalScale;

		if (lineScr.twoPlayer) {
			lineScr2.GetComponent<Line> ().multiplierOnePlayer10 = false;

			//!!lineScr2.purple.transform.localPosition = lineScr2.purple.GetComponent<Scale1Direction> ().originalPosition;
			//!!lineScr2.purple.transform.localScale = lineScr2.purple.GetComponent<Scale1Direction> ().originalScale;

			//!!lineScr2.purple2.transform.localPosition = lineScr2.purple2.GetComponent<Scale1Direction> ().originalPosition;
			//!!lineScr2.purple2.transform.localScale = lineScr2.purple2.GetComponent<Scale1Direction> ().originalScale;
		}

		//rightObject.transform.position = rightObject.GetComponent<TestFollower> ().startPos;
		//leftObject.transform.position = leftObject.GetComponent<TestFollower> ().startPos;
		leftObject.GetComponent<TestFollower> ().ResetPosition ();
		rightObject.GetComponent<TestFollower> ().ResetPosition ();

		if (lineScr.twoPlayer) {
			leftObject2.GetComponent<TestFollower2> ().ResetPosition ();
			rightObject2.GetComponent<TestFollower2> ().ResetPosition ();
		}

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

		/*foreach (GameObject bg in backgrounds) {
			
			bg.SetActive(false);
		}
		
		currentBackground = backgrounds[Random.Range(0, backgrounds.Length)];
		currentBackground.SetActive(true);

        //Application.LoadLevel(0);*/
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
}
