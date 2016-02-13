using UnityEngine;
using System.Collections;

public class ObjectiveGenerator : MonoBehaviour 
{
	public ScoreCenter scoreScr;
	public TextMesh objMesh;
	public Renderer progressCircle, progressFade, objectiveFade;
	public Transform line, brackets, patternFrame;
	public Transform[] patternQuads;
	public Texture fade, marker;
	public Texture[] balls, progressCircles;
	
	int objective, place = 0, multTarget, streak, lastType;
	int[] pattern = new int[0];
	public float tempProgress, objectiveProgress, objectiveTarget, v = 0.8f, s = 1.8f;
	Color fadeCol;
	bool lineOver, lerping;
	
	void Start()
	{
		fadeCol = objectiveFade.material.color;
	}
	
	IEnumerator Countdown(int gap)
	{
		yield return new WaitForSeconds(1);
		float countdown = gap;
		while(countdown > 0)
		{
			countdown -= Time.deltaTime;
			yield return null;
		}
		NewObjective();
		StartCoroutine(Fade());
	}
	
	IEnumerator Fade()
	{
		float t = 0, start = (int)fadeCol.a, finish = (int)(1-fadeCol.a);
		while(t < 1)
		{
			t += Time.deltaTime;
			fadeCol.a = Mathf.Lerp(start,finish,t);
			objectiveFade.material.color = fadeCol;
			yield return null;
		}
	}
	
	void NewObjective()
	{
		objective = Random.Range(0,4);
		switch(objective)
		{
		case 0:	
			objectiveTarget = Random.Range(3,10);
			objMesh.text = "COLOR\nSTREAK\n"+objectiveTarget;
			streak = 0;
			//scoreScr.patternDelegate = StreakCheck;
			break;
			
		case 1: 
			objectiveProgress = scoreScr.score;
			objectiveTarget = objectiveProgress+scoreScr.multiplier*20;
			objMesh.text = "GET\n"+(int)(objectiveTarget-objectiveProgress)+"\nPOINTS";
			break;
			
		case 2: 
			objectiveTarget = 10;
			multTarget = (int)Mathf.Clamp(scoreScr.multiplier-5,2,Mathf.Infinity);
			objMesh.text = "STAY\nOVER\nx"+multTarget;
			break;
			
		case 3: 
			objectiveTarget = Random.Range(3,9);
			AssignPattern();
			//scoreScr.patternDelegate = PatternCheck;
			objMesh.text = "";
			break;
		}
		progressCircle.material.mainTexture = progressCircles[objective];
		StartCoroutine(ObjectiveCheckup());
	}
	
	IEnumerator ObjectiveCheckup()
	{
		float lastProgress = 0;
		while(objectiveProgress < objectiveTarget)
		{
			switch(objective)
			{
			case 0:	
				if(streak != lastProgress && !lerping)
					StartCoroutine(ProgressionLerp(streak > lastProgress ? streak : 1));
				break;

			case 1:	
				if(scoreScr.score > lastProgress)
				{
					StartCoroutine(ProgressionLerp(scoreScr.score));
					objMesh.text = "GET\n"+(int)(objectiveTarget-objectiveProgress)+"\nPOINTS";
				}
				break;

			case 2:
				progressFade.enabled = multTarget >= scoreScr.multiplier;
				if(!progressFade.enabled)
					objectiveProgress += Time.deltaTime;
				break;
				
			case 3:
				objectiveProgress = place;
				break;
			}
			progressCircle.material.SetFloat("_Cutoff",Mathf.Clamp(1-objectiveProgress/objectiveTarget,0.001f,1f));
			lastProgress = objectiveProgress;
			yield return null;
		}
		StopAllCoroutines();
		objectiveProgress = objectiveTarget = tempProgress = 0;
		if(objective == 3)
		{
			//scoreScr.patternDelegate = null;
			place = 0;
			StartCoroutine(PatternLerp(9));
		}
		StartCoroutine(Fade());
		StartCoroutine(Countdown(3));
	}
	
	IEnumerator ProgressionLerp(int amt)
	{
		lerping = true;
		float t = 0, start = objectiveProgress;
		while(t < 1)
		{
			objectiveProgress = Mathf.Lerp(start,amt,t);
			t += Time.deltaTime*5;
			yield return null;
		}
		lerping = false;
	}
	
	void AssignPattern()																//sets a new pattern from the given parameters
	{
		pattern = new int[(int)objectiveTarget];											//initializes a new pattern of objectiveTarget size
		for(int i = 0; i < 8; i++)												//for each pattern entry
		{
			if(i > pattern.Length-1)
				patternQuads[i].GetComponent<Renderer>().enabled = false;
			else
			{
				pattern[i] = (int)Mathf.Round(Random.value);										//sets a 0 or a 1
				patternQuads[i].GetComponent<Renderer>().material.mainTexture = balls[pattern[i]];	//sets the quad that represents it to use the correct texture
			}
		}
		patternFrame.localPosition = new Vector3(1,0,0);									//sets the start position of the quads
		StartCoroutine(PatternLerp(0));														//lerps them into view
	}
	
	IEnumerator PatternLerp(int amt)													//Lerps the position of the quads to a given pattern place. Updates the scale of the quads as they move
	{
		place = amt;																		//set the pattern progression place
		float t = 0, start = patternFrame.position.x, finish = amt*-0.5f;					//sets the lerp timer, the start x position of the lerp, and the finish end x position
		while(t < 1)																			//while the lerp progression < 100%
		{
			t += Time.deltaTime*5;																	//increments the progression
			patternFrame.localPosition = new Vector3(Mathf.Lerp(start,finish,t),0,0);				//updates the x position of the quads
			foreach(Transform q in patternQuads)													//iterate through the quads
				q.localScale = Vector3.one*(0.5f-Mathf.Pow(Mathf.Abs(q.position.x),2)*v)*s;				//scale the quad accoriding to its distance from x = 0; scale = y = 0.5s-v|x|^2
			yield return null;																		//return on next frame
		}
		patternFrame.localPosition = new Vector3(finish,0,0);									//to avoid overshooting, sets the final position
	}

	void StreakCheck(int type)
	{
		if(type == lastType)												//if the type collected is the same as the last one
			streak++;														//increase the type streak by one
		else
		{
			lastType = type;
			streak = 0;
		}
	}
	
	void PatternCheck(int type)
	{
		StartCoroutine(PatternLerp(type != pattern[place] ? 0 : place+1));						//lerps to the relevant pattern placing. if the last collected was correct, increment. else, reset to start.
	}
	
	public void GameOver()
	{
		StopAllCoroutines();
		//brackets.gameObject.SetActive(false);
		place = multTarget = 0;
		objectiveProgress = objectiveTarget = 0;
		//scoreScr.patternDelegate = null;
		objMesh.text = "";
		progressCircle.material.SetFloat("_Cutoff",1);
		fadeCol.a = 1;
		objectiveFade.material.color = fadeCol;
		progressFade.enabled = false;
	}
}