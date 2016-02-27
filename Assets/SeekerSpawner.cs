using UnityEngine;
using System.Collections;

public class SeekerSpawner : MonoBehaviour {

    public int spacing = 15;
    public int thisAngle;

    public GameObject seeker;
    public Spawner spawnCode;

    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

        if (Input.GetKeyDown(KeyCode.H))
        {

            //SpawnSeeker();
        }
	}

    public void SpawnSeeker()
    {

        thisAngle = SpacingFilter(thisAngle);                                   //finds a new spawn angle based on the previous one
        transform.parent.rotation = Quaternion.Euler(0, 0, thisAngle);			//rotates,adjusts to suit editor setup

        GameObject b = Instantiate(seeker, transform.position, Quaternion.identity) as GameObject;       //create the ball that was resolved to be spawned
                                                                                                         //b.SendMessage("MoveSpeedInc",scoreScr.multiplier/ballSpeedScale);							//increment its move speed accordingly
        b.GetComponent<Ball>().moveSpeed += spawnCode.multiLevel / spawnCode.ballSpeedScale;
        b.GetComponent<Ball>().isBottomSeeker = true;
        //b.GetComponent<Ball>().moveSpeed += multiLevel / ballSpeedScale;
        //b.GetComponent<Ball>().moveSpeed += spawnCode.multiLevel / spawnCode.ballSpeedScale;
    }

    public int SpacingFilter(int lastAngle)
    {
        int angle = Random.Range(-40, 40), dir = angle - lastAngle;      //rolls an angle in bounds. finds the the direction and magnitude of change.
        if (Mathf.Abs(dir) > spacing)                                   //if the magnitude is greater than the minimum ball spacing
            return angle;                                                   //return it unchanged
        int spaced = lastAngle + spacing * (int)Mathf.Sign(dir);            //otherwise, create a test value.
        if (Mathf.Clamp(spaced, -40, 40) != spaced)                          //use it to see if the spacing in the direction requested goes outside the boundaries
            return lastAngle + spacing * -(int)Mathf.Sign(dir);             //if it does, return it on the other side of the last spawned ball, spaced
        return spaced;                                                  //if it doesn't, return it spaced
    }
}
