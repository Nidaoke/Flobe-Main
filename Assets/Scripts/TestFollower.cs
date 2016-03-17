using UnityEngine;
using System.Collections;

public class TestFollower : MonoBehaviour {

	public GameObject holder1, holder2;

    public Vector3 desiredScale;
    public Vector3 currentScale;
    public bool grow;
    public float growAmount;

    public ScoreCenter scoree;

    public float maxTimer;
    public float timer;

    public bool touching;

	public GameObject line;
	public bool canFlip;

	public float speed;

	public Vector3 rgbdVelocity;
	public Rigidbody2D rgbd;
	public GameObject otherPiece;

	public bool isWASD;

	public bool isLeft;

	//public Vector2 lastPos;

	public Vector2 startPos;

	public bool useParticle;
	
	void Start () {

        scoree = GameObject.FindObjectOfType<ScoreCenter>();

        timer = maxTimer;
	
		rgbd = GetComponent<Rigidbody2D> ();
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
						
						if (Input.GetKey (KeyCode.I) || Input.GetKey(KeyCode.UpArrow)) {
							
							rgbd.velocity = new Vector2 (0, speed);
						} else if (Input.GetKey (KeyCode.K) || Input.GetKey(KeyCode.DownArrow)) {
							
							rgbd.velocity = new Vector2 (0, -speed);
						} else {
							
							rgbd.velocity = Vector2.zero;
						}
					} else {
						
						if (Input.GetKey (KeyCode.I) || Input.GetKey(KeyCode.UpArrow)) {
							
							rgbd.velocity = new Vector2 (-speed, speed);
						} else if (Input.GetKey (KeyCode.K) || Input.GetKey(KeyCode.DownArrow)) {
							
							rgbd.velocity = new Vector2 (-speed, -speed);
						} else {
							
							rgbd.velocity = new Vector2 (-speed, 0);
						}
					}
				} else if (Input.GetKey (KeyCode.L) || Input.GetKey(KeyCode.RightArrow)) {
					
					if (Input.GetKey (KeyCode.I) || Input.GetKey(KeyCode.UpArrow)) {
						
						rgbd.velocity = new Vector2 (speed, speed);
					} else if (Input.GetKey (KeyCode.K) || Input.GetKey(KeyCode.DownArrow)) {
						
						rgbd.velocity = new Vector2 (speed, -speed);
					} else {
						
						rgbd.velocity = new Vector2 (speed, 0);
					}
				} else {
					
					if (Input.GetKey (KeyCode.I) || Input.GetKey(KeyCode.UpArrow)) {
						
						rgbd.velocity = new Vector2 (0, speed);
					} else if (Input.GetKey (KeyCode.K) || Input.GetKey(KeyCode.DownArrow)) {
						
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

		transform.position = new Vector3 (transform.position.x, transform.position.y, 10);

		if (transform.position.x < holder1.transform.position.x + .35f) {

			transform.position = new Vector3(holder1.transform.position.x + .35f, transform.position.y, 10);
		}
		if (transform.position.y < -4.21f) {

			transform.position = new Vector3(transform.position.x, -4.21f, 10);
		}
		if (transform.position.x > holder2.transform.position.x - .35f) {

			transform.position = new Vector3(holder2.transform.position.x - .35f, transform.position.y, 10);
		}
		if (transform.position.y > 10.65f) {

			transform.position = new Vector3(transform.position.x, 10.65f, 10);
		}
	}

    public void OnCollisionEnter(Collision other)
    {

        if (other.gameObject.tag == "Follower")
        {

            touching = true;

           
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

        speed = 7 + ((float) scoree.multiplier / 8);

        if (grow)
        {
			grow = false;
			transform.localScale = desiredScale;
            /*if (touching)
            {

                timer -= Time.timeScale;

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
            }*/


        }

		

		if (Input.GetKeyDown (KeyCode.LeftControl)) {

			isWASD = !isWASD;
		}

		

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
