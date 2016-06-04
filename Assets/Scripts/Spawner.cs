using UnityEngine;
using System.Collections;

public class Spawner : MonoBehaviour 
{
	bool stopSpawningPurple, stopSpawningBlue;

	public GameObject ball, hazard, homing, bomb, purple, orange;
	public ScoreCenter scoreScr;
	public float rate, spawnRateScale, ballSpeedScale, hazardChance, homingChance;
	public int spacing = 15, multiLevel = 1, multiLevel2 = 1;
	public bool ballNext = false;													//create bool to track whether a ball should be spawned next
	int thisAngle;

	public bool pauseSpawning;

	public bool hitMulti;
	public bool hitMulti2;
	public bool hitMulti3;
	public bool hitMulti4;
	public bool hitMulti4_2;

	public float randBomb;

	public IEnumerator SpawnBlue(){

		yield return new WaitForSeconds (Random.value * 45);

		thisAngle = SpacingFilter(thisAngle);									//finds a new spawn angle based on the previous one
		transform.parent.rotation = Quaternion.Euler(0,0,thisAngle);			//rotates,adjusts to suit editor setup

		GameObject b = Instantiate (ball, transform.position, Quaternion.identity) as GameObject;

		b.GetComponent<Ball>().moveSpeed += (scoreScr.pseudoMultiplier * 3)/(ballSpeedScale * 3);

		b.GetComponent<Ball> ().oldSpeed = b.GetComponent<Ball> ().moveSpeed;

		//b.GetComponent<Ball> ().approachEffect = null;
		//b.GetComponent<Ball> ().particle.SetActive (false);
	}

	public IEnumerator SpawnGreen(){

		yield return new WaitForSeconds (Random.value * 45);

		thisAngle = SpacingFilter(thisAngle);									//finds a new spawn angle based on the previous one
		transform.parent.rotation = Quaternion.Euler(0,0,thisAngle);			//rotates,adjusts to suit editor setup

		GameObject b = Instantiate (hazard, transform.position, Quaternion.identity) as GameObject;

		b.GetComponent<Ball>().moveSpeed += (scoreScr.pseudoMultiplier * 3)/(ballSpeedScale * 3);

		b.GetComponent<Ball> ().oldSpeed = b.GetComponent<Ball> ().moveSpeed;

		//b.GetComponent<Ball> ().approachEffect = null;
		//b.GetComponent<Ball> ().particle.SetActive (false);
	}

	public IEnumerator SpawnRed(){

		yield return new WaitForSeconds (Random.value * 45);

		thisAngle = SpacingFilter(thisAngle);									//finds a new spawn angle based on the previous one
		transform.parent.rotation = Quaternion.Euler(0,0,thisAngle);			//rotates,adjusts to suit editor setup

		GameObject b = Instantiate (homing, transform.position, Quaternion.identity) as GameObject;

		b.GetComponent<Ball>().moveSpeed += (scoreScr.pseudoMultiplier * 3)/(ballSpeedScale * 3);

		b.GetComponent<Ball> ().oldSpeed = b.GetComponent<Ball> ().moveSpeed;

		//b.GetComponent<Ball> ().approachEffect = null;
		//b.GetComponent<Ball> ().particle.SetActive (false);
	}

	public IEnumerator SpawnBomb(){

		yield return new WaitForSeconds (Random.value * 45);

		thisAngle = SpacingFilter(thisAngle);									//finds a new spawn angle based on the previous one
		transform.parent.rotation = Quaternion.Euler(0,0,thisAngle);			//rotates,adjusts to suit editor setup

		GameObject b = Instantiate (bomb, transform.position, Quaternion.identity) as GameObject;

		b.GetComponent<Ball>().moveSpeed += (scoreScr.pseudoMultiplier * 3)/(ballSpeedScale * 3);

		b.GetComponent<Ball> ().oldSpeed = b.GetComponent<Ball> ().moveSpeed;

		//b.GetComponent<Ball> ().approachEffect = null;
		//b.GetComponent<Ball> ().particle.SetActive (false);
	}

	public IEnumerator SpawnPurple(){

		yield return new WaitForSeconds (Random.value * 45);

		thisAngle = SpacingFilter(thisAngle);									//finds a new spawn angle based on the previous one
		transform.parent.rotation = Quaternion.Euler(0,0,thisAngle);			//rotates,adjusts to suit editor setup

		GameObject b = Instantiate(purple,transform.position,Quaternion.identity) as GameObject;		//create the ball that was resolved to be spawned

		b.GetComponent<Ball>().moveSpeed += (scoreScr.pseudoMultiplier * 3)/(ballSpeedScale * 3);

		b.GetComponent<Ball> ().oldSpeed = b.GetComponent<Ball> ().moveSpeed;

		//b.GetComponent<Ball> ().approachEffect = null;
		//b.GetComponent<Ball> ().particle.SetActive (false);
	}

	public IEnumerator SpawnOrange(){

		yield return new WaitForSeconds (Random.value * 45);

		thisAngle = SpacingFilter(thisAngle);									//finds a new spawn angle based on the previous one
		transform.parent.rotation = Quaternion.Euler(0,0,thisAngle);			//rotates,adjusts to suit editor setup

		GameObject b = Instantiate(orange,transform.position,Quaternion.identity) as GameObject;		//create the ball that was resolved to be spawned

		b.GetComponent<Ball>().moveSpeed += (scoreScr.pseudoMultiplier * 3)/(ballSpeedScale * 3);

		b.GetComponent<Ball> ().oldSpeed = b.GetComponent<Ball> ().moveSpeed;

		//b.GetComponent<Ball> ().approachEffect = null;
		//b.GetComponent<Ball> ().particle.SetActive (false);
	}

	public void Update(){

		if (!hitMulti) {

			//Debug.Log("HitMulti = false");

			if(GameController.instance.scoreScr.multiplier >= 5){

				hitMulti = true;
			}

		}

		if (!hitMulti2) {

			if(GameController.instance.scoreScr.multiplier >= 10 || GameController.instance.scoreScr.multiplier2 >= 10){
				
				hitMulti2 = true;
			}
		}

		if (!hitMulti3) {
			
			if(GameController.instance.scoreScr.multiplier >= 10 || GameController.instance.scoreScr.multiplier2 >= 10){
				
				hitMulti3 = true;
				scoreScr.spawnScr.multiLevel /= 3;
			}
		}

		if (!hitMulti4) {

			if(GameController.instance.scoreScr.multiplier >= 20){

				hitMulti4 = true;
				scoreScr.spawnScr.multiLevel /= 3;
			}
		}

		if (!hitMulti4_2) {

			//if(GameController.instance.scoreScr.multiplier >= 10){

				if ((GameController.instance.scoreScr.multiplier >= 10 || GameController.instance.scoreScr.multiplier2 >= 10)) {

					hitMulti4_2 = true;
				}
			//}
		}
	}

	public void StopSpawning(string colorToStop){

		if (colorToStop == "Purple") {

			stopSpawningPurple = true;
		} else if (colorToStop == "Blue") {

			stopSpawningBlue = true;
		}
	}
	
	IEnumerator Spawn() 
	{
		int hazardCounter = 0;													//keeps track of successive hazard spawns
		while(true)																//infinite loop
		{
			thisAngle = SpacingFilter(thisAngle);									//finds a new spawn angle based on the previous one
			transform.parent.rotation = Quaternion.Euler(0,0,thisAngle);			//rotates,adjusts to suit editor setup

			GameObject spawn = ball;

			if(hitMulti3){

				if (Random.value < .5)
					spawn = ball;
				else {
					if (GameObject.FindObjectOfType<Line> ().leftBar.activeSelf || GameObject.FindObjectOfType<Line> ().rightBar.activeSelf)
						spawn = purple;
					else
						spawn = ball;
				}
			}

			if(hitMulti4){

				float val = Random.value;

				if (val < .333f)
					spawn = ball;
				else if (val < .666f) {
					if (GameObject.FindObjectOfType <Line> ().player1) {
						//!!if (GameObject.FindObjectOfType <Line> ().purple.activeSelf || GameObject.FindObjectOfType <Line> ().purple2.activeSelf) {
						//!!spawn = purple;
						//!!}
					} else
						spawn = purple;
				}
				else
					spawn = orange;
			}

			if (GameObject.FindObjectOfType<HandleTwoPlayer> ().twoPlayer) {

				if(Random.value < .5)
					spawn = ball;
				else
					spawn = purple;

				if (stopSpawningBlue)
					spawn = purple;
				if (stopSpawningPurple)
					spawn = ball;
			}

			if (GameObject.FindObjectOfType<HandleTwoPlayer> ().twoPlayer && (hitMulti4_2)) {

				float val = Random.value;

				if (val < .333f)
					spawn = ball;
				else if (val < .666f)
					spawn = purple;
				else
					spawn = orange;

				if (stopSpawningBlue) {

					val = Random.value;
					if (val < .5f)
						spawn = purple;
					else
						spawn = orange;
				}

				if (stopSpawningPurple) {

					val = Random.value;
					if (val < .5f)
						spawn = ball;
					else
						spawn = orange;
				}
			}

															//creates an object, sets it to ball by default
			float cooldown = rate-scoreScr.pseudoMultiplier/spawnRateScale;				//creates spawn cooldown, sets to default value
		


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

			if(!pauseSpawning){
				
				GameObject b = Instantiate(spawn,transform.position,Quaternion.identity) as GameObject;		//create the ball that was resolved to be spawned

				//if(b.GetComponent<Collectable> () != null){

					//b.GetComponent<Ball>().moveSpeed += multiLevel2/ballSpeedScale;
				//}else{

					//b.GetComponent<Ball>().moveSpeed += multiLevel/ballSpeedScale;
				//}

				b.GetComponent<Ball>().moveSpeed += (scoreScr.pseudoMultiplier * 3)/(ballSpeedScale * 3);

				b.GetComponent<Ball> ().oldSpeed = b.GetComponent<Ball> ().moveSpeed;

			}

			yield return new WaitForSeconds(cooldown);													//wait until the next spawn cycle
		
		}
	}

	int SpacingFilter(int lastAngle)
	{
		int angle = Random.Range(-70,70), dir = angle - lastAngle;		//rolls an angle in bounds. finds the the direction and magnitude of change.
		if(Mathf.Abs(dir) > spacing)									//if the magnitude is greater than the minimum ball spacing
			return angle;													//return it unchanged
		int spaced = lastAngle + spacing*(int)Mathf.Sign(dir);			//otherwise, create a test value.
		if(Mathf.Clamp(spaced,-70,70) != spaced)							//use it to see if the spacing in the direction requested goes outside the boundaries
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
