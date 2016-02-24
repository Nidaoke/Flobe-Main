using UnityEngine;
using System.Collections;
//using GooglePlayGames;
//using GooglePlayGames.BasicApi;
using UnityEngine.SocialPlatforms;

public class TallyScore : MonoBehaviour 
{
	public Texture fade;
	public GUISkin gameStyle, tallyStyle;
	public GameObject leaderButt, retryButt, achieveButt;

	bool newScore;
	public int score;
	string result;
	Rect eScreen = new Rect(0,0,480,800);

	public void GameOver(int s)
	{
		if (Input.GetKeyDown (KeyCode.Joystick1Button6))
			Application.Quit ();

		//retryButt.SetActive(true);
		//leaderButt.SetActive(true);
		//achieveButt.SetActive(true);
		score = s;
		if(!PlayerPrefs.HasKey("bestScoreLocal"))
		{
			PlayerPrefs.SetInt("bestScoreLocal", 0);
		}
		if(score > PlayerPrefs.GetInt("bestScoreLocal"))
		{
			newScore = true;
			PlayerPrefs.SetInt("bestScoreLocal", score);
		}
		if(newScore)
			StartCoroutine(FlashNewHighScore());
		result = string.Format("{0:0000}", score);
		//Social.ReportScore(score, "CgkI052F_vMSEAIQBg", (bool success) => {});
	}
	
	void Update()
	{
        result = string.Format("{0:0000}", score);
        if (Input.GetKeyDown (KeyCode.Escape)) {

			Application.Quit();
		}

		if(Input.GetKeyDown(KeyCode.Space))
			Reset();
		if (Input.GetKeyDown (KeyCode.Joystick1Button7))
			Reset ();

		if (Input.touchCount == 1)
		{
			Vector3 touch = Camera.main.ScreenToWorldPoint(Input.GetTouch(0).position);
			Vector2 touchPos = new Vector2(touch.x, touch.y);
			if(retryButt.GetComponent<Collider2D>() == Physics2D.OverlapPoint(touchPos))
				Reset ();
			if(leaderButt.GetComponent<Collider2D>() == Physics2D.OverlapPoint(touchPos))
				Social.ShowLeaderboardUI();
			if(achieveButt.GetComponent<Collider2D>() == Physics2D.OverlapPoint(touchPos))
				Social.ShowAchievementsUI();
		}
	}
	
	IEnumerator FlashNewHighScore()
	{
		while(true)
		{
			gameStyle.label.normal.textColor = Color.gray;
			yield return new WaitForSeconds(0.2f);
			gameStyle.label.normal.textColor = Color.white;
			yield return new WaitForSeconds(0.2f);
		}
	}

	void OnGUI()
	{
		Vector3 scale = new Vector3(Screen.width/eScreen.width / 3,Screen.height/eScreen.height,1);
		GUI.matrix = Matrix4x4.TRS(Vector3.zero, Quaternion.identity, scale);

		//GUI.Box(new Rect(100,150,eScreen.width-200,eScreen.height-400),"",gameStyle.box);

		GUI.Box(new Rect(555,150,eScreen.width-100-50,300),result,gameStyle.box);

		if(newScore)
			GUI.Label(new Rect(125,350,eScreen.width-200-50,50),"new best",gameStyle.label);
		
		//GUI.Box(new Rect(125,eScreen.height-125-105,eScreen.width-200-50,100),"",gameStyle.box);
		
		//GUI.Box(new Rect(125,eScreen.height-125-225,eScreen.width-200-50,100),"",gameStyle.box);

	}

	void Reset()
	{
		score = 0;
		newScore = false;
		retryButt.SetActive(false);
		StopAllCoroutines();
		GameController.instance.ResetGame();
	}
}
