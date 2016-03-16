using UnityEngine;
using System.Collections;

public class Ball : MonoBehaviour 
{
	public GameObject approachEffect;
	public float moveSpeed;
	protected bool missed;
	protected Vector3 dir;

	public float oldSpeed;

    public bool isBottomSeeker = false;

	public GameObject pop;

	void Start(){

		oldSpeed = moveSpeed;
	}

	void Awake() 
	{
		dir = -transform.position.normalized;
		StartCoroutine(Move());
	}

	void Update(){

		float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;

		transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);

		transform.rotation *= Quaternion.Euler (0, 0, 90);
	}

	public void Pause(bool on)
	{
		if(on)
			StopCoroutine(Move());
		else
			StartCoroutine(Move());
//		Debug.Log(gameObject.name+" "+on);
	}

	public void Freeze(){
		float tParam = 0;
		float speedy = .7f;

		if (tParam < 1) {

			tParam += Time.deltaTime * speedy;
			moveSpeed = Mathf.Lerp (moveSpeed, 0, tParam);
		}

		//moveSpeed = 0;
		//moveSpeed = Mathf.Lerp(moveSpeed, 0, Time.time);
	}

	public void UnFreeze(){

		float tParam = 0;
		float speedy = .7f;

		if (tParam < 1) {

			tParam += Time.deltaTime * speedy;
			moveSpeed = Mathf.Lerp (moveSpeed, oldSpeed, tParam);
		}
	}
	
	IEnumerator Move()
	{
        yield return new WaitForSeconds(.1f);

        if (!isBottomSeeker)
        {

            StartCoroutine(ApproachEffect());
            Rigidbody2D rb = GetComponent<Rigidbody2D>();
            while (transform.position.y > -4.5f)
            {
                if (GameController.instance.gameOver)
                    yield break;
                rb.MovePosition(transform.position + dir * Time.deltaTime * moveSpeed);
                yield return new WaitForFixedUpdate();
            }
            missed = true;
            Destroy(this.gameObject);
        }
        else
        {

            StartCoroutine(ApproachEffect());
            Rigidbody2D rb = GetComponent<Rigidbody2D>();
            while (transform.position.y < 14f)
            {
                if (GameController.instance.gameOver)
                    yield break;
                rb.MovePosition(transform.position + dir * Time.deltaTime * moveSpeed);
                yield return new WaitForFixedUpdate();
            }

            missed = true;
            Destroy(this.gameObject);
        }


    }

	IEnumerator ApproachEffect()
	{
		while(true)
		{
			RaycastHit hit;
			if(Physics.Raycast(new Ray(transform.position,dir),out hit,moveSpeed+transform.localScale.x))
			{
				if(hit.collider.gameObject.layer == 13)
				{
					GameObject ae = Instantiate(approachEffect,hit.point,Quaternion.identity) as GameObject;
					ae.transform.right = dir;
					Destroy(ae,1f);
					yield break;
				}
			}
			yield return null;
		}
	}
}
