using UnityEngine;
using System.Collections;

public class ColorUtility : MonoBehaviour 
{
	public float hue, hueShiftSpeed, saturation, satPulse, minSat, maxSat;
	public Color currentColor;
	public Camera bgEffects;
	Color[] states = new Color[]{new Color(1,0,0),new Color(1,1,0),new Color(0,1,0),new Color(0,1,1),new Color(0,0,1),new Color(1,0,1)};
	bool pulsing;

	void Start()
	{
		currentColor = new Color(1,1f-saturation,1f-saturation);
		StartCoroutine(HSVColorCycle());
		minSat = saturation;
		maxSat = saturation * 2;
	}

	void FixedUpdate()
	{
		/*if(saturation > minSat)
		{
			float curSat = saturation;
			float t = Time.deltaTime * 3.5f;
			saturation = Mathf.Clamp (saturation,minSat,maxSat);
			saturation = Mathf.Lerp (curSat,minSat,t);
		}*/
	}

	IEnumerator HSVColorCycle()
	{
		while(true)
		{
			hue += Time.deltaTime*hueShiftSpeed;
			if(hue > 360)
				hue = 0;
			currentColor = RGBFromHue(hue);
			bgEffects.backgroundColor = currentColor;
			yield return null;
		}
	}
	
	/*IEnumerator SatPulse()
	{
		if(pulsing) 										//if the color is mid-pulse
		{
			saturation += satPulse;								//shift the goalposts
			yield break;										//exit here to avoid duping
		}
		float start = saturation;							//if the color pulse isn't running, record the start state
		saturation += satPulse;								//set the pulse burst
		pulsing = true;										//indicate that the pulse is running
		float t = 0;										//time float
		while(t < )										//for 1 unit time
		{
			saturation = Mathf.Lerp(saturation,start,t);		//lerp the saturation from top of the burst back down to the start state
			t += Time.deltaTime*1.2f;								//count down time
			yield return null;									//return here next frame
		}
		saturation = start;									//make sure the saturation is back to normal
		pulsing = false;									//indicate the pulse is over
	}*/
	
	public Color RGBFromHue(float h)
	{
		float t = h/60f;																	//find the total segment progression of the hue circle
		int s = Mathf.FloorToInt(t);														//find the current segment index
		t -= s;																				//grab the inter-segment progression
		Color start = states[s], finish = states[s+1 == states.Length ? 0 : s+1];			//retrieve the edge colors of the current segment
		float sat = 1f-saturation;															//pull the saturation value for this frame
		return new Color(Mathf.Lerp(start.r > 0 ? 1f : sat, finish.r > 0 ? 1f : sat, t),	//return a color that is t% between start to finish, multiplied by the requested saturation
		                 Mathf.Lerp(start.g > 0 ? 1f : sat, finish.g > 0 ? 1f : sat, t),	
		                 Mathf.Lerp(start.b > 0 ? 1f : sat, finish.b > 0 ? 1f : sat, t));	
	}
}
