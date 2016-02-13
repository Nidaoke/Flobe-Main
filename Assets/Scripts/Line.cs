﻿using UnityEngine;
using System.Collections;

public class Line : MonoBehaviour 
{
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

	public void Start(){

		maxTimer = timer;
	}

	public void Active(bool on)
	{
		for (int i = 0; i < 3; i++) 
			pieces[i].gameObject.SetActive(on);
	}

	public void Update(){

		if (Input.GetKeyDown (KeyCode.Tab)) {

			hitMultiplier = true;
		}

		rightTrigger = Input.GetAxis ("JoystickRightTrigger");

	//	if (Input.GetAxis ("JoystickRightTrigger")) {

			//Debug.Log(Input.GetAxis("JoystickRightTrigger"));
	//	}

		if (scoreScr.GetComponent<ScoreCenter> ().multiplier >= 15 && !hitMultiplier) {

            if (purpleFill.GetComponent<ParticleSystem>().isPlaying == false)
            {

                purpleFill.GetComponent<ParticleSystem>().Play();
            }

			hitMultiplier = true;
			Debug.Log("DO GLOW THINGY!");
		}

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

		/*if (Input.GetKey (KeyCode.Joystick1Button5)) {

			Debug.Log ("FUKCH!");
		}*/

		if (purple.activeSelf) {

			//if (Input.GetKeyDown (KeyCode.Joystick1Button5)) {

			if((rightTrigger == -1 && rightTriggerLast != -1) || Input.GetKeyDown(KeyCode.Q)){

				purple.SetActive(false);
				purple2.SetActive(true);
			}
		} else if (purple2.activeSelf) {

			if((rightTrigger == -1 && rightTriggerLast != -1) || Input.GetKeyDown(KeyCode.Q)){
				
				purple.SetActive(true);
				purple2.SetActive(false);
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

	void OnCollisionEnter2D(Collision2D col)
	{
		//if(GameController.instance.gameOver)
		//	return;
		//CheckHit(col.gameObject, col.contacts[0].point);
	}

	void OnTriggerStay2D(Collider2D obj){

        if (GameController.instance.gameOver)
            return;
        CheckHit(obj.gameObject, obj.transform.position, obj);

        if (obj.gameObject.layer == 10) {

			if(obj.gameObject.GetComponent<Bomb>() != null){
				

				obj.gameObject.GetComponent<Bomb> ().Boom();
			}
		}
	}

	void CheckHit(GameObject obj, Vector3 point, Collider2D coll)
	{


		if (obj.layer == 9) {

			if (hitMultiplier) {
				
				if(Vector2.Distance(obj.transform.position, pieces[0].position) < Vector2.Distance(obj.transform.position, pieces[1].position)){
					
					//Debug.Log("Hit Right Side!!!!");

					if(purple.activeSelf){

						if(obj.tag == "Purple"){
							
							//GameObject popper = obj.gameObject.GetComponent<Ball> ().pop;
							Destroy(Instantiate(bluePop, obj.transform.position, Quaternion.identity), 1);
							
							//obj.gameObject.GetComponent<Ball> ().pop.SetActive(true);
							
							scoreScr.AddScore ();
							//Destroy (Instantiate (collectSplat, obj.transform.position, transform.rotation), 0.5f);
							obj.SendMessage ("DestroyBall");
						}else if(obj.tag == "Ball"){
							
							audioS.PlayOneShot (failSounds [Random.Range (0, failSounds.Length - 1)]);
							//			GameObject p = Instantiate(hitEffect,point,Quaternion.identity) as GameObject;
							//			p.
							GameController.instance.StartCoroutine ("EndGame");
						}
					}else if(purple2.activeSelf){

						if(obj.tag == "Ball"){

                            //GameObject popper = obj.gameObject.GetComponent<Ball> ().pop;

                            if (obj.GetComponent<Bomb>() == null)
                            {

                                Destroy(Instantiate(bluePop, obj.transform.position, Quaternion.identity), 1);
                            }

                            //obj.gameObject.GetComponent<Ball> ().pop.SetActive(true);

                            scoreScr.AddScore ();
							//Destroy (Instantiate (collectSplat, obj.transform.position, transform.rotation), 0.5f);
							obj.SendMessage ("DestroyBall");
						}else if(obj.tag == "Purple"){
							
							audioS.PlayOneShot (failSounds [Random.Range (0, failSounds.Length - 1)]);
							//			GameObject p = Instantiate(hitEffect,point,Quaternion.identity) as GameObject;
							//			p.
							GameController.instance.StartCoroutine ("EndGame");
						}
					}
				}else{

					if(purple.activeSelf){

						if(obj.tag == "Ball"){

                            if (obj.GetComponent<Bomb>() == null)
                            {

                                Destroy(Instantiate(bluePop, obj.transform.position, Quaternion.identity), 1);
                            }
							
							//GameObject popper = obj.gameObject.GetComponent<Ball> ().pop;
							
							
							//obj.gameObject.GetComponent<Ball> ().pop.SetActive(true);
							
							scoreScr.AddScore ();
							//Destroy (Instantiate (collectSplat, obj.transform.position, transform.rotation), 0.5f);
							obj.SendMessage ("DestroyBall");
						}else if(obj.tag == "Purple"){
							
							audioS.PlayOneShot (failSounds [Random.Range (0, failSounds.Length - 1)]);
							//			GameObject p = Instantiate(hitEffect,point,Quaternion.identity) as GameObject;
							//			p.
							GameController.instance.StartCoroutine ("EndGame");
						}
					}else if(purple2.activeSelf){

						if(obj.tag == "Purple"){

                            //GameObject popper = obj.gameObject.GetComponent<Ball> ().pop;

                            if (obj.GetComponent<Bomb>() == null)
                            {

                                Destroy(Instantiate(bluePop, obj.transform.position, Quaternion.identity), 1);
                            }

                            //obj.gameObject.GetComponent<Ball> ().pop.SetActive(true);

                            scoreScr.AddScore ();
							//Destroy (Instantiate (collectSplat, obj.transform.position, transform.rotation), 0.5f);
							obj.SendMessage ("DestroyBall");
						}else if(obj.tag == "Ball"){
							
							audioS.PlayOneShot (failSounds [Random.Range (0, failSounds.Length - 1)]);
							//			GameObject p = Instantiate(hitEffect,point,Quaternion.identity) as GameObject;
							//			p.
							GameController.instance.StartCoroutine ("EndGame");
						}
					}
				}
			} else {

                //GameObject popper = obj.gameObject.GetComponent<Ball> ().pop;

                if (obj.GetComponent<Bomb>() == null)
                {

                    Destroy(Instantiate(bluePop, obj.transform.position, Quaternion.identity), 1);
                }

                //obj.gameObject.GetComponent<Ball> ().pop.SetActive(true);

                scoreScr.AddScore ();
				//Destroy (Instantiate (collectSplat, obj.transform.position, transform.rotation), 0.5f);
				obj.SendMessage ("DestroyBall");
			}		




		} else if (obj.layer == 10) {

			if(obj.gameObject.GetComponent<Ball> () != null){

				if(obj.gameObject.GetComponent<Ball> ().pop != null){

                    //GameObject popper = obj.gameObject.GetComponent<Ball> ().pop;
                    if (obj.GetComponent<Bomb>() == null)
                    {

                        Destroy(Instantiate(bluePop, obj.transform.position, Quaternion.identity), 1);
                    }

                    //obj.gameObject.GetComponent<Ball> ().pop.SetActive(true);
                }

			}

            if (obj.gameObject.GetComponent<Bomb>() != null)
            {

                //if(coll.radius == 7)

                CircleCollider2D[] cirCols = obj.GetComponents<CircleCollider2D>();
                foreach (CircleCollider2D cirCol2D in cirCols)
                {

                    //if (cirCol2D.isTrigger)
                    //{
                    //
                    // Debug.Log("HI!");
                    //}

                    if (coll.isTrigger)
                    {


                    }
                    else
                    {

                        //  obj.gameObject.GetComponent<SpriteRenderer>().sprite = null;
                        //  obj.gameObject.GetComponent<Animator>().enabled = false;

                        audioS.PlayOneShot(failSounds[Random.Range(0, failSounds.Length - 1)]);
                        //			GameObject p = Instantiate(hitEffect,point,Quaternion.identity) as GameObject;
                        //			p.
                        GameController.instance.StartCoroutine("EndGame");
                    }
                }

                obj.gameObject.GetComponent<Bomb>().warning.SetActive(false);
             
            }
            else
            {

                audioS.PlayOneShot(failSounds[Random.Range(0, failSounds.Length - 1)]);
                //			GameObject p = Instantiate(hitEffect,point,Quaternion.identity) as GameObject;
                //			p.
                GameController.instance.StartCoroutine("EndGame");
            }

		//	if(obj.gameObject.GetComponent<Bomb>() != null){

				//decreaseTimer = true;
				//obj.gameObject.GetComponent<Bomb> ().Boom();

			//}else{

			
		//	}
		
		}
		else{
			Debug.Log(obj.name);

			Debug.Log(obj.layer);
		}
	}
}