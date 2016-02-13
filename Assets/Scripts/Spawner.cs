using UnityEngine;
using System.Collections;

public class Spawner : MonoBehaviour 
{
	public GameObject ball, hazard, homing, bomb, purple;
	public ScoreCenter scoreScr;
	public float rate, spawnRateScale, ballSpeedScale, hazardChance, homingChance;
	public int spacing = 15, multiLevel = 1, multiLevel2 = 1;
	public bool ballNext = false;													//create bool to track whether a ball should be spawned next
	int thisAngle;

	public bool hitMulti;
	public bool hitMulti2;
	public bool hitMulti3;

	public float randBomb;

	public void Update(){

		if (!hitMulti) {

			//Debug.Log("HitMulti = false");

			if(GameController.instance.scoreScr.multiplier >= 5){

				hitMulti = true;
			}

		}

		if (!hitMulti2) {

			if(GameController.instance.scoreScr.multiplier >= 10){
				
				hitMulti2 = true;
			}
		}

		if (!hitMulti3) {
			
			if(GameController.instance.scoreScr.multiplier >= 15){
				
				hitMulti3 = true;
				scoreScr.spawnScr.multiLevel /= 3;
			}
		}
	}
	
	IEnumerator Spawn() 
	{
		int hazardCounter = 0;													//keeps track of successive hazard spawns
		while(true)																//infinite loop
		{
			thisAngle = SpacingFilter(thisAngle);									//finds a new spawn angle based on the previous one
			transform.parent.rotation = Quaternion.Euler(0,0,thisAngle-45);			//rotates,adjusts to suit editor setup

			GameObject spawn = ball;

			if(hitMulti3){

				if(Random.value < .5)
					spawn = ball;
				else
					spawn = purple;
			}

															//creates an object, sets it to ball by default
			float cooldown = rate-scoreScr.multiplier/spawnRateScale;				//creates spawn cooldown, sets to default value
		


			if(!ballNext)														//if hazards are allowed to be spawned
			{
				if(hitMulti2 && Random.value < homingChance){
					randBomb = Random.value;
					if(randBomb < .5f){

						spawn = bomb;
					}else{

						spawn = homing;
					}
				}else

				if(hitMulti && Random.value < homingChance)													//if a homing object(s) was requested to be spawned
				{
					//randBomb = Random.Range(0, 1);

					spawn = bomb;
																														//increment the counter
				}
				else if(Random.value < hazardChance)								//otherwise, if a regular hazard is rolled
				{
					spawn = hazard;														//set the spawning object to the hazard
					hazardCounter++;													//increment its counter
				}
			}
			else if(hazardCounter > 0)											//if one or more hazards were spawned last	
			{
				ballNext = true;													//make sure a ball is spawned next
				hazardCounter--;													//decrement the hazard counter
				cooldown = 0;														//make the spawning between balls in this chain instant (to catch up to intended multiplier rate)
			}
			if(hazardCounter < 1)												//if the balls have caught up to the hazards
				ballNext = false;													//release the requirement to force-spawn another

			GameObject b = Instantiate(spawn,transform.position,Quaternion.identity) as GameObject;		//create the ball that was resolved to be spawned
			//b.SendMessage("MoveSpeedInc",scoreScr.multiplier/ballSpeedScale);							//increment its move speed accordingly

			if(b.GetComponent<Collectable> () != null){

				b.GetComponent<Ball>().moveSpeed += multiLevel2/ballSpeedScale;
			}else{

				b.GetComponent<Ball>().moveSpeed += multiLevel/ballSpeedScale;
			}
			yield return new WaitForSeconds(cooldown);													//wait until the next spawn cycle
		}
	}

	int SpacingFilter(int lastAngle)
	{
		int angle = Random.Range(5,120), dir = angle - lastAngle;		//rolls an angle in bounds. finds the the direction and magnitude of change.
		if(Mathf.Abs(dir) > spacing)									//if the magnitude is greater than the minimum ball spacing
			return angle;													//return it unchanged
		int spaced = lastAngle + spacing*(int)Mathf.Sign(dir);			//otherwise, create a test value.
		if(Mathf.Clamp(spaced,5,120) != spaced)							//use it to see if the spacing in the direction requested goes outside the boundaries
			return lastAngle + spacing*-(int)Mathf.Sign(dir);				//if it does, return it on the other side of the last spawned ball, spaced
		return spaced;													//if it doesn't, return it spaced
	}

//	public int RollPercentiles(float[] ranges)		//takes the % ranges (the sum must = 1f)
//	{
//		float r = Random.value;						//roll 0f to 1f
//		for(int i = 0; i < ranges.Length; i++)		//foreach float range (between 0f and 1f)
//		{
//			r -= ranges[i];								//subtracts the range from the roll
//			if(r < 0)									//until the roll is spent
//				return i;								//returns the index of the range that spent it
//		}
//		return -1;
//	}
}
