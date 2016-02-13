﻿using UnityEngine;
using System.Collections;

public class TestFollower : MonoBehaviour {

    public Vector3 desiredScale;
    public Vector3 currentScale;
    public bool grow;
    public float growAmount;

    public float maxTimer;
    public float timer;

    public bool touching;

	public GameObject line;
	public bool canFlip;

	public float speed;

	public Vector3 rgbdVelocity;
	public Rigidbody rgbd;
	public GameObject otherPiece;

	public bool isWASD;

	public bool isLeft;

	//public Vector2 lastPos;

	public Vector2 startPos;

	public bool useParticle;
	
	void Start () {

        timer = maxTimer;
	
		rgbd = GetComponent<Rigidbody> ();
		//lastPos = transform.position;
		startPos = transform.position;

        desiredScale = transform.localScale;

		line = GameObject.FindGameObjectWithTag ("Line");
	}

	void FixedUpdate () {

		if (isWASD) {

			if (isLeft) {

				if (Input.GetKey (KeyCode.D)) {

					//if (transform.position.x >= otherPiece.transform.localPosition.x && !canFlip) {
					if (transform.position.x >= otherPiece.transform.localPosition.x) {


						if (Input.GetKey (KeyCode.W)) {

							rgbd.velocity = new Vector2 (0, speed);
						} else if (Input.GetKey (KeyCode.S)) {

							rgbd.velocity = new Vector2 (0, -speed);
						} else {

							rgbd.velocity = Vector2.zero;
						}
					} else {

						if (Input.GetKey (KeyCode.W)) {
							
							rgbd.velocity = new Vector2 (speed, speed);
						} else if (Input.GetKey (KeyCode.S)) {
							
							rgbd.velocity = new Vector2 (speed, -speed);
						} else {
							
							rgbd.velocity = new Vector2 (speed, 0);
						}
					}
				} else if (Input.GetKey (KeyCode.A)) {
					
					if (Input.GetKey (KeyCode.W)) {
						
						rgbd.velocity = new Vector2 (-speed, speed);
					} else if (Input.GetKey (KeyCode.S)) {
						
						rgbd.velocity = new Vector2 (-speed, -speed);
					} else {
						
						rgbd.velocity = new Vector2 (-speed, 0);
					}
				} else {

					if (Input.GetKey (KeyCode.W)) {

						rgbd.velocity = new Vector2 (0, speed);
					} else if (Input.GetKey (KeyCode.S)) {

						rgbd.velocity = new Vector2 (0, -speed);
					} else {

						rgbd.velocity = new Vector2 (0, 0);
					}
				}
			} else {

				if (Input.GetKey (KeyCode.J) || Input.GetKey(KeyCode.LeftArrow)) {
					
					//if (transform.position.x <= otherPiece.transform.localPosition.x && !canFlip) {

					if (transform.position.x <= otherPiece.transform.localPosition.x) {
						
						if (Input.GetKey (KeyCode.I)) {
							
							rgbd.velocity = new Vector2 (0, speed);
						} else if (Input.GetKey (KeyCode.K)) {
							
							rgbd.velocity = new Vector2 (0, -speed);
						} else {
							
							rgbd.velocity = Vector2.zero;
						}
					} else {
						
						if (Input.GetKey (KeyCode.I)) {
							
							rgbd.velocity = new Vector2 (-speed, speed);
						} else if (Input.GetKey (KeyCode.K)) {
							
							rgbd.velocity = new Vector2 (-speed, -speed);
						} else {
							
							rgbd.velocity = new Vector2 (-speed, 0);
						}
					}
				} else if (Input.GetKey (KeyCode.L)) {
					
					if (Input.GetKey (KeyCode.I)) {
						
						rgbd.velocity = new Vector2 (speed, speed);
					} else if (Input.GetKey (KeyCode.K)) {
						
						rgbd.velocity = new Vector2 (speed, -speed);
					} else {
						
						rgbd.velocity = new Vector2 (speed, 0);
					}
				} else {
					
					if (Input.GetKey (KeyCode.I)) {
						
						rgbd.velocity = new Vector2 (0, speed);
					} else if (Input.GetKey (KeyCode.K)) {
						
						rgbd.velocity = new Vector2 (0, -speed);
					} else {
						
						rgbd.velocity = new Vector2 (0, 0);
					}
				}
			}


		} else {

			if (isLeft) {
				
				if (Input.GetAxis ("JoystickLeftX") > 0) {
					
					//if (transform.position.x >= otherPiece.transform.position.x && !canFlip) {

					if (transform.position.x >= otherPiece.transform.position.x) {
						
						rgbd.velocity = new Vector2 (0, Input.GetAxis ("JoystickLeftY") * speed);
					} else {
						
						rgbd.velocity = new Vector2 (Input.GetAxis ("JoystickLeftX") * speed, Input.GetAxis ("JoystickLeftY") * speed);
					}
				} else {
					
					rgbd.velocity = new Vector2 (Input.GetAxis ("JoystickLeftX") * speed, Input.GetAxis ("JoystickLeftY") * speed);
				}
			}
			
			if (!isLeft) {
				
				if (Input.GetAxis ("JoystickRightX") < 0) {
					
					//if (transform.position.x <= otherPiece.transform.position.x && !canFlip) {

					if (transform.position.x <= otherPiece.transform.position.x) {
						
						rgbd.velocity = new Vector2 (0, Input.GetAxis ("JoystickRightY") * speed);
					} else {
						
						rgbd.velocity = new Vector2 (Input.GetAxis ("JoystickRightX") * speed, Input.GetAxis ("JoystickRightY") * speed);
					}
				} else {
					
					rgbd.velocity = new Vector2 (Input.GetAxis ("JoystickRightX") * speed, Input.GetAxis ("JoystickRightY") * speed);
				}
			}
		}
		
		/*GetComponent<Rigidbody> ().velocity = new Vector3 (speed, 0, 0);

		rgbdVelocity = GetComponent<Rigidbody> ().velocity;



		*/


		/*void OnCollisionStay(Collision other){

		if (other.gameObject == otherPiece) {

			if(isLeft){

				rgbd.velocity = new Vector2(-1, Input.GetAxis("JoystickLeftY") * speed);
			}else{

				rgbd.velocity = new Vector2(1, Input.GetAxis("JoystickLeftY") * speed);
			}
		}
	}*/

	}

	public void ResetPosition(){

		transform.position = startPos;

		transform.localScale = new Vector3 (1, 1, 1);
		//lastPos = transform.position;
	}

    public void IncreaseScale()
    {
       

        grow = true;

        desiredScale = new Vector3(desiredScale.x + .6f, desiredScale.y + .6f, 1);
        //Debug.Log("GROWNIGGA!!!!");
    }

	void LateUpdate(){

		transform.position = new Vector3 (transform.position.x, transform.position.y, 0);

		if (transform.position.x < -10.58f) {

			transform.position = new Vector2(-10.58f, transform.position.y);
		}
		if (transform.position.y < -4.21f) {
			
			transform.position = new Vector2(transform.position.x, -4.21f);
		}
		if (transform.position.x > 10.51f) {
			
			transform.position = new Vector2(10.51f, transform.position.y);
		}
		if (transform.position.y > 10.65f) {
			
			transform.position = new Vector2(transform.position.x, 10.65f);
		}
	}

    public void OnCollisionEnter(Collision other)
    {

        if (other.gameObject.tag == "Follower")
        {

            touching = true;

           /* GameObject[] growParticle = GameObject.FindGameObjectsWithTag("Doo");

            foreach (GameObject grop in growParticle)
            {

                 grop.GetComponent<ScaleToParent>().DooParticle();
            }*/

           

            //Debug.Log("Hit Eachother!");
        }
    }

    public void OnCollisionExit(Collision other)
    {

        if (other.gameObject.tag == "Follower")
        {

            touching = false;
        }
    }

	void Update () {

       // currentScale = transform.localScale;

        if (grow)
        {

            //if (transform.localScale.x < desiredScale.x)
            /// {

            //transform.localScale += new Vector3(growAmount, 0, 0);
            // }
            // else
            // {

            // transform.localScale = new Vector3(desiredScale.x, transform.localScale.y, 1);
            //grow = false;
            // }

            // if (transform.localScale.y < desiredScale.y)
            // {

            //  transform.localScale += new Vector3(0, growAmount, 0);
            // }
            // else
            // {

            //   transform.localScale = new Vector3(transform.localScale.x, desiredScale.y, 1);
            //grow = false;
            // }

            //if (transform.localScale == desiredScale)
            //{

            //  grow = false;
            //Time.timeScale = 1f;
            //}

            if (touching)
            {

                timer -= Time.timeScale;

                Time.timeScale = .3f;

                transform.localScale += new Vector3(growAmount, 0, 0);
                transform.localScale += new Vector3(0, growAmount, 0);
            }
            else
            {

                transform.localScale += new Vector3(growAmount * 10, 0, 0);
                transform.localScale += new Vector3(0, growAmount * 10, 0);
            }

            if (timer <= 0)
            {
                Time.timeScale = 1;
                timer = maxTimer;
                grow = false;
                transform.localScale = desiredScale;
            }


        }

		//if (!canFlip) {

			/*if(line != null){

				if(line.GetComponent<Line>().hitMultiplier){
					
					canFlip = true;
				}else{

				canFlip = false;
				}


			}else{

				if(GameObject.FindGameObjectWithTag("Line") != null){

					line = GameObject.FindGameObjectWithTag("Line");
				}
			}
		//}
		*/

		if (Input.GetKeyDown (KeyCode.LeftControl)) {

			isWASD = !isWASD;
		}

		/*if (isLeft) {

			if(transform.position.x < otherPiece.transform.position.x){

				rgbd.velocity = new Vector2(Input.GetAxis("JoystickLeftX") * speed, Input.GetAxis("JoystickLeftY") * speed);
			}else{
				
				//rgbd.velocity = new Vector2(-.1f, 0);
				transform.position = new Vector2(otherPiece.transform.position.x, transform.position.y);
				rgbd.velocity = new Vector2(0, Input.GetAxis("JoystickLeftY") * speed);
			}

		} else {

			if(transform.position.x > otherPiece.transform.position.x){

				rgbd.velocity = new Vector2(Input.GetAxis("JoystickRightX") * speed, Input.GetAxis("JoystickRightY") * speed);
			}else{
				
				//rgbd.velocity = new Vector2(.1f, 0);
				transform.position = new Vector2(otherPiece.transform.position.x, transform.position.y);
				rgbd.velocity = new Vector2(0, Input.GetAxis("JoystickLeftY") * speed);
			}
		}*/

		/*if (rgbd.velocity.x == 0) {
			
			transform.position = new Vector2(lastPos.x, transform.position.y);
		}
		
		if (rgbd.velocity.y == 0) {
			
			transform.position = new Vector2(transform.position.x, lastPos.y);
		}

		lastPos = new Vector2 (transform.position.x, transform.position.y);*/

		if (isLeft) {

			if (Input.GetAxis ("JoystickLeftY") == 0 && Input.GetAxis ("JoystickLeftX") == 0) {
				
				useParticle = false;
			}else useParticle = true;
		} else {

			if (Input.GetAxis ("JoystickRightY") == 0 && Input.GetAxis ("JoystickRightX") == 0) {
				
				useParticle = false;
			}else useParticle = true;
		}
       //gameObject.tag == "Doo"
	}
}