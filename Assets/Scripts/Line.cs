using UnityEngine;
using System.Collections;
using Steamworks;

public class Line : MonoBehaviour 
{
	#region variables

	public enum threeBarPlace
	{
		blueLeft,
		blueMiddle,
		blueRight
	}
	public threeBarPlace barPlace = threeBarPlace.blueLeft;

	public bool invincibleToEnemies, invincibleToColors;

	public int lives = 5;

	//public int triggesPressed; //testing

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

	public GameObject glow1, glow2, glow3, glow4;

	public float rightTrigger;
	public float rightTriggerLast;

	public GameObject bluePop;

	//public GameObject purple, purple2, orange, orangeMulti, orangeMulti2;
	public GameObject orangeMulti, orangeMulti2;
	public GameObject leftBar, middleBar, rightBar;
	public bool multiplierOnePlayer10, multiplierOnePlayer20, multiplierTwoPlayer10;

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

	public void RightTrigger(){

		if (player1) {
			rightTrigger = Input.GetAxis ("JoystickRightTrigger");
		} else {
			rightTrigger = Input.GetAxis ("Joystick2RightTrigger");
		}

		if (((rightTrigger < -.5f) && (rightTriggerLast > -.5f)) || (Input.GetKeyDown (KeyCode.LeftShift))) {

			if (!twoPlayer) {
				if (multiplierOnePlayer20) {
					ShiftTwoPlaces ();
				} else if (multiplierOnePlayer10) {
					ShiftOnePlace ();
				}
			}
		}

		if (player1) {
			rightTriggerLast = Input.GetAxis ("JoystickRightTrigger");
		} else {
			rightTriggerLast = Input.GetAxis ("Joystick2RightTrigger");
		}
	}

	void ShiftOnePlace(){
		if (leftBar.GetComponent<LineBarPiece> ().myColor == LineBarPiece.possibleColors.blue)
			leftBar.GetComponent<LineBarPiece> ().myColor = LineBarPiece.possibleColors.purple;
		else
			leftBar.GetComponent<LineBarPiece> ().myColor = LineBarPiece.possibleColors.blue;

		if (rightBar.GetComponent<LineBarPiece> ().myColor == LineBarPiece.possibleColors.blue)
			rightBar.GetComponent<LineBarPiece> ().myColor = LineBarPiece.possibleColors.purple;
		else
			rightBar.GetComponent<LineBarPiece> ().myColor = LineBarPiece.possibleColors.blue;
	}

	void ShiftTwoPlaces(){
		if (leftBar.GetComponent<LineBarPiece> ().myColor == LineBarPiece.possibleColors.blue)
			leftBar.GetComponent<LineBarPiece> ().myColor = LineBarPiece.possibleColors.purple;
		else if (leftBar.GetComponent<LineBarPiece> ().myColor == LineBarPiece.possibleColors.purple)
			leftBar.GetComponent<LineBarPiece> ().myColor = LineBarPiece.possibleColors.orange;
		else
			leftBar.GetComponent<LineBarPiece> ().myColor = LineBarPiece.possibleColors.blue;

		if (rightBar.GetComponent<LineBarPiece> ().myColor == LineBarPiece.possibleColors.blue)
			rightBar.GetComponent<LineBarPiece> ().myColor = LineBarPiece.possibleColors.purple;
		else if (rightBar.GetComponent<LineBarPiece> ().myColor == LineBarPiece.possibleColors.purple)
			rightBar.GetComponent<LineBarPiece> ().myColor = LineBarPiece.possibleColors.orange;
		else
			rightBar.GetComponent<LineBarPiece> ().myColor = LineBarPiece.possibleColors.blue;

		if (middleBar.GetComponent<LineBarPiece> ().myColor == LineBarPiece.possibleColors.blue)
			middleBar.GetComponent<LineBarPiece> ().myColor = LineBarPiece.possibleColors.purple;
		else if (middleBar.GetComponent<LineBarPiece> ().myColor == LineBarPiece.possibleColors.purple)
			middleBar.GetComponent<LineBarPiece> ().myColor = LineBarPiece.possibleColors.orange;
		else
			middleBar.GetComponent<LineBarPiece> ().myColor = LineBarPiece.possibleColors.blue;
	}

	public void OnDestroy(){

		if (twoPlayer) {
			if (player1)
				GameObject.FindObjectOfType<TwoPlayerLives> ().blueLives = 0;
			else
				GameObject.FindObjectOfType<TwoPlayerLives> ().purpleLives = 0;
		}
	}

	public void Update(){

		RightTrigger ();

		if (purpBlueTimer > 0) //DoubleDipperAchievement
			purpBlueTimer -= Time.timeScale;
		else {
			hitAPurp = hitABlue = false;
		}
        if (hitABlue && hitAPurp)
        {
            GameObject.FindGameObjectWithTag("SteamManager").GetComponent<SteamStatsAndAchievements>().UnlockAchievement(new SteamStatsAndAchievements.Achievement_t(SteamStatsAndAchievements.Achievement.ACH_DOUBLE_DIPPER, "ACH_DOUBLE_DIPPER", "a"));
        }

        timeCount = Time.time * 2 - startTime; //SurvivorAchievement
        min = (int)(timeCount / 60);
        sec = (int)(timeCount % 60);
        if (min == 5)
        {
            GameObject.FindGameObjectWithTag("SteamManager").GetComponent<SteamStatsAndAchievements>().UnlockAchievement(new SteamStatsAndAchievements.Achievement_t(SteamStatsAndAchievements.Achievement.ACH_SURVIVOR, "ACH_SURVIVOR", "a"));
        }

		if (twoPlayer) {
			if ((scoreScr.multiplier >= 10 || scoreScr.multiplier2 >= 10) && !multiplierTwoPlayer10) {
				scoreScr.ResetPseudo (3);
				multiplierTwoPlayer10 = true;

				if (!glow1.activeSelf) {
					glow1.SetActive (true);
					glow2.SetActive (true);
				}
			}

			if (multiplierTwoPlayer10) {
				if (!orangeMulti.activeSelf && !orangeMulti2.activeSelf)
					orangeMulti.SetActive (true);
				if (!orangeMulti2.activeSelf)
					orangeMulti.SetActive (true);
				else
					orangeMulti2.SetActive (true);
			}
		} else {
			if (scoreScr.multiplier >= 20 && !multiplierOnePlayer20) { //If doesn't work,  || GameObject.FindGameObjectWithTag ("Spawner").GetComponent<Spawner> ().hitMulti4 && !multiplierOnePlayer20) {
				middleBar.SetActive(true);
				rightBar.GetComponent<Scale1Direction> ().desiredScale.x = (1 / 3f);
				middleBar.GetComponent<Scale1Direction> ().desiredScale.x = (1 / 3f);
				leftBar.GetComponent<Scale1Direction> ().desiredScale.x = (1 / 3f);
				scoreScr.ResetPseudo (5);
				multiplierOnePlayer20 = true;
				//!!purple.GetComponent<Scale1Direction> ().desiredScale = new Vector3 (.33334f, 1, 2);
				//!!purple2.GetComponent<Scale1Direction> ().desiredScale = new Vector3 (.33334f, 1, 2);
				if (!glow3.activeSelf) {
					glow3.SetActive (true);
					glow4.SetActive (true);
				}
			}

			if (scoreScr.multiplier >= 10 && !multiplierOnePlayer10) { //If doesn't work || GameObject.FindGameObjectWithTag("Spawner").GetComponent<Spawner> ().hitMulti3 && !multiplierOnePlayer10) {
				leftBar.SetActive (true);
				rightBar.SetActive (true);

				multiplierOnePlayer10 = scoreScr.increaseMultiplierGrowth = true;
				scoreScr.ResetPseudo (2);
				GameObject.FindGameObjectWithTag("SteamManager").GetComponent<SteamStatsAndAchievements> ().UnlockAchievement(new SteamStatsAndAchievements.Achievement_t(SteamStatsAndAchievements.Achievement.ACH_DOUBLE_TROUBLE, "ACH_DOUBLE_TROUBLE", "a"));
				if (!purpleFill.GetComponent<ParticleSystem> ().isPlaying)
					purpleFill.GetComponent<ParticleSystem> ().Play ();
			}

			//!!if (multiplierOnePlayer20)
				//!!orange.SetActive (true);
			if (multiplierOnePlayer10) {
				if (!glow1.activeSelf) {
					glow1.SetActive (true);
					glow2.SetActive (true);
				}
				/*if (!purple2.activeSelf)
					purple.SetActive (true);*/
			} else {
				glow1.SetActive (false);
				glow2.SetActive (false);
				/*purple.SetActive (false);
				purple2.SetActive (false);*/
			}
		}
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

	void CheckHit(GameObject obj, Vector3 point, Collider2D coll){
		if (obj.layer == 9) {
			if (!twoPlayer) {
				if (multiplierOnePlayer20) {
					if (Vector3.Distance (pieces [4].position, point) < Vector3.Distance (pieces [3].position, point))
						HitLeftSide (obj);
					else if (Vector3.Distance (pieces [5].position, point) < Vector3.Distance (pieces [3].position, point))
						HitRightSide (obj);
					else {
						if (player1)
							HitCenter (obj, 1);
						else
							HitCenter (obj, 2);
					}
				} else if (multiplierOnePlayer10) {
					if (Vector3.Distance (pieces [4].position, point) < Vector3.Distance (pieces [5].position, point))
						HitLeftSide (obj);
					else
						HitRightSide (obj);
				} else {
					if (obj.tag == "Ball")
						HitBlueGood (obj);
				}
			}
		} else if (obj.layer == 10) {
			if (obj.gameObject.GetComponent<Ball> () != null) {
				if (obj.gameObject.GetComponent<Ball> ().pop != null) {
					if (obj.GetComponent<Bomb> () == null) {
						Destroy (Instantiate (bluePop, obj.transform.position, Quaternion.identity), 1);
					}
				}
			}
			if (obj.gameObject.GetComponent<Bomb> () == null) {
				HitBad (obj);
			}
		} else if (obj.layer == 15) {
			HitBad (obj);
			obj.gameObject.GetComponent<Bomb>().warning.SetActive(false);
		}
	}

	/*void CheckHit(GameObject obj, Vector3 point, Collider2D coll)
	{

		if (obj.layer == 9) {

			if (twoPlayer) {

				if (multiplierTwoPlayer10) {

					if (player1) {

						if (orangeMulti.activeSelf) { //Orange on Right

							if (Vector2.Distance (obj.transform.position, pieces [1].position) < Vector2.Distance (obj.transform.position, pieces [0].position)) {

								if (obj.tag == "Orange") {

									HitOrangeGood (obj, 1);
								} else {

									HitBad (obj);
								}

							} else {

								if (obj.tag == "Ball") {

									HitBlueGood (obj);
								} else {

									HitBad (obj);
								}
							}
						} else if (orangeMulti2.activeSelf) {

							if (Vector2.Distance (obj.transform.position, pieces [0].position) < Vector2.Distance (obj.transform.position, pieces [1].position)) {

								if (obj.tag == "Orange") {

									HitOrangeGood (obj, 1);
								} else {

									HitBad (obj);
								}

							} else {

								if (obj.tag == "Ball") {

									HitBlueGood (obj);
								} else {

									HitBad (obj);
								}
							}
						} //Orange on Left
					}
					if (!player1) {

						if (orangeMulti2.activeSelf) { //Orange on Right

							if (Vector2.Distance (obj.transform.position, pieces [1].position) < Vector2.Distance (obj.transform.position, pieces [0].position)) {

								if (obj.tag == "Orange") {

									HitOrangeGood (obj, 2);
								} else {

									HitBad (obj);
								}

							} else {

								if (obj.tag == "Purple") {

									HitPurpleGood (obj);
								} else {

									HitBad (obj);
								}
							}
						} else if (orangeMulti.activeSelf) {

							if (Vector2.Distance (obj.transform.position, pieces [0].position) < Vector2.Distance (obj.transform.position, pieces [1].position)) {

								if (obj.tag == "Orange") {

									HitOrangeGood (obj, 2);
								} else {

									HitBad (obj);
								}

							} else {

								if (obj.tag == "Purple") {

									HitPurpleGood (obj);
								} else {

									HitBad (obj);
								}
							}
						} //Orange on Left
					}
				} else {

					if (twoPlayer) {

						if (obj.tag == "Orange") {

							HitBad (obj);
						}

						if (player1) {

							if (obj.tag == "Ball") {

								HitBlueGood (obj);
							} else if (obj.tag == "Purple") {

								HitBad (obj);
							}
						} else {

							if (obj.tag == "Ball") {

								HitBad (obj);
							} else if (obj.tag == "Purple") {

								HitPurpleGood (obj);
							}
						}
					}
				}
			} else {

				if (multiplierOnePlayer20) {

					if(Vector2.Distance(obj.transform.position, pieces [3].position) < Vector2.Distance (obj.transform.position, pieces[5].position)){ //Center than Right
						if (Vector2.Distance (obj.transform.position, pieces [3].position) < Vector2.Distance (obj.transform.position, pieces [4].position)) { //Center than left
							if (obj.tag == "Orange") {

								HitOrangeGood (obj, 1);
							} else {

								HitBad (obj);
							}
						}
					}

					if (Vector2.Distance (obj.transform.position, pieces [4].position) < Vector2.Distance (obj.transform.position, pieces [5].position)) {

						if (Vector2.Distance (obj.transform.position, pieces [4].position) < Vector2.Distance (obj.transform.position, pieces [3].position)) {

							HitLeftSide (obj);
						}
					}

					if (Vector2.Distance (obj.transform.position, pieces [5].position) < Vector2.Distance (obj.transform.position, pieces [4].position)) {

						if (Vector2.Distance (obj.transform.position, pieces [5].position) < Vector2.Distance (obj.transform.position, pieces [3].position)) {

							HitRightSide (obj);
						}
					}
				}else if (multiplierOnePlayer10) {

					if (Vector2.Distance (obj.transform.position, pieces [0].position) < Vector2.Distance (obj.transform.position, pieces [1].position)) {

						HitRightSide (obj);

					} else {

						HitLeftSide (obj);
					}
				} else {

					if (twoPlayer) {

						if (player1) {

							if (obj.tag == "Ball") {

								HitBlueGood (obj);
							} else if (obj.tag == "Purple") {

								HitBad (obj);
							}
						} else {

							if (obj.tag == "Ball") {

								HitBad (obj);
							} else if (obj.tag == "Purple") {

								HitPurpleGood (obj);
							}
						}
					} else {

						if (obj.tag == "Ball")
							HitBlueGood (obj);
						else
							HitBad (obj);
					}
				}	
			}

		} else if (obj.layer == 10) {

			if (obj.gameObject.GetComponent<Ball> () != null) {
				if (obj.gameObject.GetComponent<Ball> ().pop != null) {
					if (obj.GetComponent<Bomb> () == null) {
						Destroy (Instantiate (bluePop, obj.transform.position, Quaternion.identity), 1);
					}
				}
			}
			if (obj.gameObject.GetComponent<Bomb> () == null) {

				HitBad (obj);
			}
			//}//
		} else if (obj.layer == 15) {

			HitBad (obj);
			obj.gameObject.GetComponent<Bomb>().warning.SetActive(false);
		}
	}*/

	void HitRightSide(GameObject obj2){

		if (rightBar.GetComponent<LineBarPiece> ().myColor == LineBarPiece.possibleColors.blue) {
			if (obj2.tag == "Ball")
				HitBlueGood (obj2);
			else
				HitBad (obj2);
		} else if (rightBar.GetComponent<LineBarPiece> ().myColor == LineBarPiece.possibleColors.purple) {
			if (obj2.tag == "Purple")
				HitPurpleGood (obj2);
			else
				HitBad (obj2);
		} else if (rightBar.GetComponent<LineBarPiece> ().myColor == LineBarPiece.possibleColors.orange) {
			if (obj2.tag == "Orange")
				HitOrangeGood (obj2);
			else
				HitBad (obj2);
		}
	}

	void HitLeftSide(GameObject obj2){

		if (leftBar.GetComponent<LineBarPiece> ().myColor == LineBarPiece.possibleColors.blue) {
			if (obj2.tag == "Ball")
				HitBlueGood (obj2);
			else
				HitBad (obj2);
		} else if (leftBar.GetComponent<LineBarPiece> ().myColor == LineBarPiece.possibleColors.purple) {
			if (obj2.tag == "Purple")
				HitPurpleGood (obj2);
			else
				HitBad (obj2);
		} else if (leftBar.GetComponent<LineBarPiece> ().myColor == LineBarPiece.possibleColors.orange) {
			if (obj2.tag == "Orange")
				HitOrangeGood (obj2);
			else
				HitBad (obj2);
		}
	}

	void HitCenter(GameObject objO, int player){

		if (middleBar.GetComponent<LineBarPiece> ().myColor == LineBarPiece.possibleColors.blue) {
			if (objO.tag == "Ball")
				HitBlueGood (objO);
			else
				HitBad (objO);
		} else if (middleBar.GetComponent<LineBarPiece> ().myColor == LineBarPiece.possibleColors.purple) {
			if (objO.tag == "Purple")
				HitPurpleGood (objO);
			else
				HitBad (objO);
		} else if (middleBar.GetComponent<LineBarPiece> ().myColor == LineBarPiece.possibleColors.orange) {
			if (objO.tag == "Orange")
				HitOrangeGood (objO);
			else
				HitBad (objO);
		}
	}

	void HitOrangeGood (GameObject obj9){
		if (twoPlayer) {

			Destroy(Instantiate(bluePop, obj9.transform.position, Quaternion.identity), 1);
			//scoreScr.AddScoreMultiplier (player);
			obj9.SendMessage ("DestroyBall");
		} else {

			Destroy(Instantiate(bluePop, obj9.transform.position, Quaternion.identity), 1);
			scoreScr.AddScore ();
			obj9.SendMessage ("DestroyBall");
		}

		if (!twoPlayer) {

			GameObject[] sideSpawners = GameObject.FindGameObjectsWithTag ("SideSpawner");
			GameObject sideSpawnerToSpawn = sideSpawners [Random.Range (0, sideSpawners.Length)];
			sideSpawnerToSpawn.GetComponent<SpawnFakes> ().AddOrangeEnemy ();
		} else {

			if (player1) {

				GameObject[] sideSpawners = GameObject.FindGameObjectsWithTag ("SideSpawner");
				foreach (GameObject spawner in sideSpawners) {

					if (!spawner.GetComponent<SpawnFakes> ().isRight) {

						spawner.GetComponent<SpawnFakes> ().AddOrangeEnemy ();
					}
				}
			} else {

				GameObject[] sideSpawners = GameObject.FindGameObjectsWithTag ("SideSpawner");
				foreach (GameObject spawner in sideSpawners) {

					if (spawner.GetComponent<SpawnFakes> ().isRight) {

						spawner.GetComponent<SpawnFakes> ().AddOrangeEnemy ();
					}
				}
			}
		}
	}

	void HitPurpleGood(GameObject objJ){

		if (twoPlayer) {

			Destroy(Instantiate(bluePop, objJ.transform.position, Quaternion.identity), 1);
			GameObject.FindGameObjectWithTag("SteamManager").GetComponent<SteamStatsAndAchievements>().PurplesCollected++;
			GameObject.FindGameObjectWithTag("SteamManager").GetComponent<SteamStatsAndAchievements>().m_bStoreStats = true;
			hitAPurp = true;
			purpBlueTimer = 20f;
			scoreScr.AddScoreMultiplier (2);
			objJ.SendMessage ("DestroyBall");
		} else {

			Destroy(Instantiate(bluePop, objJ.transform.position, Quaternion.identity), 1);
			GameObject.FindGameObjectWithTag("SteamManager").GetComponent<SteamStatsAndAchievements>().PurplesCollected++;
			GameObject.FindGameObjectWithTag("SteamManager").GetComponent<SteamStatsAndAchievements>().m_bStoreStats = true;
			hitAPurp = true;
			purpBlueTimer = 20f;
			scoreScr.AddScore ();
			objJ.SendMessage ("DestroyBall");
		}

		if (twoPlayer) {

			GameObject[] sideSpawners = GameObject.FindGameObjectsWithTag ("SideSpawner");

			foreach (GameObject spawner in sideSpawners) {

				if (spawner.GetComponent<SpawnFakes> ().isRight) {

					spawner.GetComponent<SpawnFakes> ().AddPurpleEnemy ();
				}
			}
		} else {

			GameObject[] sideSpawners = GameObject.FindGameObjectsWithTag ("SideSpawner");
			GameObject sideSpawnerToSpawn = sideSpawners [Random.Range (0, sideSpawners.Length)];
			sideSpawnerToSpawn.GetComponent<SpawnFakes> ().AddPurpleEnemy ();
		}


	}

	void HitBlueGood(GameObject objJ){
		if (objJ.GetComponent<Bomb>() == null)
		{
			Destroy(Instantiate(bluePop, objJ.transform.position, Quaternion.identity), 1);
		}

		if (twoPlayer) {

			GameObject.FindGameObjectWithTag ("SteamManager").GetComponent<SteamStatsAndAchievements> ().BluesCollected++;
			GameObject.FindGameObjectWithTag ("SteamManager").GetComponent<SteamStatsAndAchievements> ().m_bStoreStats = true;
			hitABlue = true;
			purpBlueTimer = 20f;
			scoreScr.AddScoreMultiplier (1);
			objJ.SendMessage ("DestroyBall");
		} else {

			GameObject.FindGameObjectWithTag("SteamManager").GetComponent<SteamStatsAndAchievements>().BluesCollected++;
			GameObject.FindGameObjectWithTag("SteamManager").GetComponent<SteamStatsAndAchievements>().m_bStoreStats = true;
			hitABlue = true;
			purpBlueTimer = 20f;
			scoreScr.AddScore ();
			objJ.SendMessage ("DestroyBall");
		}



		if (twoPlayer) {

			GameObject[] sideSpawners = GameObject.FindGameObjectsWithTag ("SideSpawner");

			foreach (GameObject spawner in sideSpawners) {

				if (!spawner.GetComponent<SpawnFakes> ().isRight) {

					spawner.GetComponent<SpawnFakes> ().AddBlueEnemy ();
				}
			}
		} else {

			GameObject[] sideSpawners = GameObject.FindGameObjectsWithTag ("SideSpawner");
			GameObject sideSpawnerToSpawn = sideSpawners [Random.Range (0, sideSpawners.Length)];
			sideSpawnerToSpawn.GetComponent<SpawnFakes> ().AddBlueEnemy ();
		}


	}

	public void HitBad(GameObject obj3){

		Debug.Log ("HitBad!");

		if (!invincibleToColors && !invincibleToEnemies) {

			Debug.Log ("I Hate My Life");

			if (twoPlayer) {

				lives--;

				if (player1) {

					GameObject[] growParticle = GameObject.FindGameObjectsWithTag ("Doo");

					foreach (GameObject grop in growParticle) {

						grop.GetComponent<ScaleToParent> ().DooParticle ();
					}
				} else {

					GameObject[] growParticle = GameObject.FindGameObjectsWithTag ("Doo2");

					foreach (GameObject grop in growParticle) {

						grop.GetComponent<ScaleToParent> ().DooParticle ();
					}
				}

				if (player1)
					GameObject.FindObjectOfType<TwoPlayerLives> ().TakeLife ("Blue");
				else
					GameObject.FindObjectOfType<TwoPlayerLives> ().TakeLife ("Purple");

				if (obj3 != null) {

					Destroy (obj3.gameObject);
					Destroy (Instantiate (bluePop, obj3.transform.position, Quaternion.identity), 1);
				}
				audioS.PlayOneShot (failSounds [Random.Range (0, failSounds.Length - 1)]);
			} else {

				audioS.PlayOneShot (failSounds [Random.Range (0, failSounds.Length - 1)]);
				GameController.instance.StartCoroutine ("EndGame");
				GetComponent<Line> ().enabled = false;
			}
		} else {

			if (obj3.layer == 10) {

				Debug.Log ("Inv to c or e");

				if (twoPlayer) {

					lives--;

					if (player1)
						GameObject.FindObjectOfType<TwoPlayerLives> ().TakeLife ("Blue");
					else
						GameObject.FindObjectOfType<TwoPlayerLives> ().TakeLife ("Purple");

					if (obj3 != null) {

						Destroy (obj3.gameObject);
						Destroy (Instantiate (bluePop, obj3.transform.position, Quaternion.identity), 1);
					}
					audioS.PlayOneShot (failSounds [Random.Range (0, failSounds.Length - 1)]);
				} else {

					Debug.Log ("1 pl");

					if (!invincibleToEnemies) {
						audioS.PlayOneShot (failSounds [Random.Range (0, failSounds.Length - 1)]);
						GameController.instance.StartCoroutine ("EndGame");
						GetComponent<Line> ().enabled = false;
					} else {
						Destroy (obj3.gameObject);
						scoreScr.HitBadDuringBonus ();
					}
				}
			} else {

				if (obj3.tag == "Ball" || obj3.tag == "Orange" || obj3.tag == "Purple") {

					if (invincibleToColors)
						Destroy (obj3.gameObject);
					else {

					}
				}
			}
		}

		if (lives <= 0) {

			audioS.PlayOneShot (failSounds [Random.Range (0, failSounds.Length - 1)]);

			if (player1)
				spawnScr.StopSpawning ("Blue");
			else
				spawnScr.StopSpawning ("Purple");

			Line[] lines = GameObject.FindObjectsOfType<Line> ();

			if (lines.Length < 2) {

				audioS.PlayOneShot (failSounds [Random.Range (0, failSounds.Length - 1)]);
				GameController.instance.StartCoroutine ("EndGame");
				GetComponent<Line> ().enabled = false;
			}

			foreach (Line l in lines) {

				if (l.gameObject != gameObject) {

					Debug.Log ("DIE!");

					if (l.gameObject.activeSelf == false) {

						audioS.PlayOneShot (failSounds [Random.Range (0, failSounds.Length - 1)]);
						GameController.instance.StartCoroutine ("EndGame");
						GetComponent<Line> ().enabled = false;
					} else {

						foreach (Transform p in pieces) {

							p.gameObject.SetActive (false);
						}

						audioS.PlayOneShot (failSounds [Random.Range (0, failSounds.Length - 1)]);

						gameObject.SetActive (false);
					}
				}
			}
		}
	}
}