using UnityEngine;
using System.Collections;
using Steamworks;

public class Line : MonoBehaviour 
{
	#region variables

	public bool invincible;

	public int lives = 5;

	public int triggesPressed; //testing

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

	public GameObject purple, purple2, orange, orangeMulti, orangeMulti2;
	public bool hitMultiplier, hitMultiplier2, hitmultiplierForTwoPlayer;

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

		if (twoPlayer) {

			if (!player1) {

				rightTrigger = Input.GetAxis ("JoystickRightTrigger");
			} else {

				rightTrigger = Input.GetAxis ("Joystick2RightTrigger");
			}
		} else {

			rightTrigger = Input.GetAxis ("JoystickRightTrigger");
		}

		if ((scoreScr.GetComponent<ScoreCenter> ().multiplier >= 10 || scoreScr.GetComponent<ScoreCenter> ().multiplier2 >= 10)) {

			if (twoPlayer) {

				if (!hitmultiplierForTwoPlayer) {

					scoreScr.ResetPseudo (3);

					hitmultiplierForTwoPlayer = true;
					//GameObject.FindGameObjectWithTag ("Freezer").GetComponent<FreezeAllEnemies> ().BeginFreeze ();

					if (!glow1.activeSelf) {

						glow1.SetActive (true);
						glow2.SetActive (true);
					}	
				}

				hitmultiplierForTwoPlayer = true;
			}
		}

		if ((scoreScr.GetComponent<ScoreCenter> ().multiplier >= 20 && !hitMultiplier2) || GameObject.FindGameObjectWithTag ("Spawner").GetComponent<Spawner> ().hitMulti4 && !hitMultiplier2) {
			if (!twoPlayer) {

				scoreScr.ResetPseudo (5);

				hitMultiplier2 = true;

				if (!glow3.activeSelf) {

					glow3.SetActive (true);
					glow4.SetActive (true);
				}	

				purple.GetComponent<Scale1Direction> ().desiredScale = new Vector3 (.333f, 1, 2);
				purple2.GetComponent<Scale1Direction> ().desiredScale = new Vector3 (.333f, 1, 2);
			}
		}

		if ((scoreScr.GetComponent<ScoreCenter> ().multiplier >= 10 && !hitMultiplier) || GameObject.FindGameObjectWithTag("Spawner").GetComponent<Spawner> ().hitMulti3 && !hitMultiplier) {

			if (!twoPlayer) {

				hitMultiplier = true;

				if (purpleFill.GetComponent<ParticleSystem>().isPlaying == false)
				{

					purpleFill.GetComponent<ParticleSystem>().Play();
				}


				scoreScr.ResetPseudo (2);
				scoreScr.increaseMultiplierGrowth = true;
			}
		
			//Debug.Log("DO GLOW THINGY!");
            GameObject.FindGameObjectWithTag("SteamManager").GetComponent<SteamStatsAndAchievements>().UnlockAchievement(new SteamStatsAndAchievements.Achievement_t(SteamStatsAndAchievements.Achievement.ACH_DOUBLE_TROUBLE, "ACH_DOUBLE_TROUBLE", "a"));
           // GameObject.FindGameObjectWithTag("SteamManager").GetComponent<SteamStatsAndAchievements> ().UnlockAchievement(GameObject.FindGameObjectWithTag("SteamManager").GetComponent<SteamStatsAndAchievements> ().)
        }

		if (!twoPlayer) {

			if (hitMultiplier2)
				orange.SetActive (true);

			if (hitMultiplier) {

				if (!glow1.activeSelf) {

					glow1.SetActive (true);
					glow2.SetActive (true);
				}

				if (!purple2.activeSelf)
					purple.SetActive (true);
			} else {

				glow1.SetActive (false);
				glow2.SetActive (false);

				purple.SetActive (false);
				purple2.SetActive (false);
			}

			if (purple.activeSelf) {

				//if (Input.GetKeyDown (KeyCode.Joystick1Button5)) {

				if ((rightTrigger == -1 && rightTriggerLast != -1) || Input.GetKeyDown (KeyCode.LeftShift)) {

					triggesPressed++;

					purple.SetActive (false);
					purple2.SetActive (true);
				}
			} else if (purple2.activeSelf) {

				if ((rightTrigger == -1 && rightTriggerLast != -1) || Input.GetKeyDown (KeyCode.LeftShift)) {

					triggesPressed++;

					purple.SetActive (true);
					purple2.SetActive (false);
				}
			}
		} if (twoPlayer){

			if (!hitmultiplierForTwoPlayer && (scoreScr.GetComponent<ScoreCenter> ().multiplier >= 10 || scoreScr.GetComponent<ScoreCenter> ().multiplier2 >= 10)) {

				hitmultiplierForTwoPlayer = true;
			}

			if (hitmultiplierForTwoPlayer) {

				if (!orangeMulti.activeSelf && !orangeMulti2.activeSelf) {

					orangeMulti.SetActive (true);
				}

				if (!orangeMulti2.activeSelf) {

					orangeMulti.SetActive (true);
				} else {

					orangeMulti2.SetActive (true);
				}

			}

			if ((rightTrigger == -1 && rightTriggerLast != -1) || Input.GetKeyDown (KeyCode.LeftShift)) {

				if (orangeMulti.activeSelf) {

					orangeMulti.SetActive (false);
					orangeMulti2.SetActive (true);
				} else if (orangeMulti2.activeSelf) {

					orangeMulti.SetActive (true);
					orangeMulti2.SetActive (false);
				}
			}

		}

		if (twoPlayer) {

			if (!player1) {

				rightTriggerLast = Input.GetAxis ("JoystickRightTrigger");
			} else {

				rightTriggerLast = Input.GetAxis ("Joystick2RightTrigger");
			}
		} else {

			rightTriggerLast = Input.GetAxis ("JoystickRightTrigger");
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

	void CheckHit(GameObject obj, Vector3 point, Collider2D coll)
	{

		if (obj.layer == 9) {

			if (twoPlayer) {

				if (hitmultiplierForTwoPlayer) {

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

				if (hitMultiplier2) {

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
				}else if (hitMultiplier) {

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
	}

	void HitRightSide(GameObject obj2){

		if (obj2.tag == "Orange") {
			HitBad (obj2);
		}

		if (purple.activeSelf) {

			if (obj2.tag == "Purple") {

				HitPurpleGood (obj2);
			} else if (obj2.tag == "Ball") {

				HitBad (obj2);
			}
		} else if (purple2.activeSelf) {

			if (obj2.tag == "Ball") {

				HitBlueGood (obj2);
			} else if (obj2.tag == "Purple") {

				HitBad (obj2);
			}
		}
	}

	void HitLeftSide(GameObject obj2){

		if (obj2.tag == "Orange") {

			HitBad (obj2);
		}

		if (purple.activeSelf) {

			if (obj2.tag == "Ball") {

				HitBlueGood (obj2);
			} else if (obj2.tag == "Purple") {

				HitBad (obj2);
			}
		} else if (purple2.activeSelf) {

			if (obj2.tag == "Purple") {

				HitPurpleGood (obj2);
			} else if (obj2.tag == "Ball") {

				HitBad (obj2);
			}
		}
	}

	void HitOrangeGood(GameObject obj9, int player){

		if (twoPlayer) {

			Destroy(Instantiate(bluePop, obj9.transform.position, Quaternion.identity), 1);
			scoreScr.AddScoreMultiplier (player);
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

		if (!invincible) {

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

					audioS.PlayOneShot (failSounds [Random.Range (0, failSounds.Length - 1)]);
					GameController.instance.StartCoroutine ("EndGame");
					GetComponent<Line> ().enabled = false;
				}
			} else {

				if (obj3.tag == "Ball" || obj3.tag == "Orange" || obj3.tag == "Purple") {

					Destroy (obj3.gameObject);
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