using UnityEngine;
using System.Collections;

public class FloatingText : MonoBehaviour 
{
	public GUIStyle style;
	
	float duration;
	string text;
	Vector2 pos, moveStart, moveEnd;
	Color color;
	
	public void Init(Vector2 moveTo, float dur, string txt, Color col)
	{
		text = txt;
		duration = 1/dur;
		moveStart = Camera.main.WorldToScreenPoint(transform.position);
		pos = moveStart;
		moveEnd = moveStart + moveTo;
		color = col;
		StartCoroutine(MoveToEndState());
	}
	
	IEnumerator MoveToEndState()
	{
		float t = 0;
		while(t < 1f)																//if the box timer is over 0
		{
			pos = Vector2.Lerp(moveStart,moveEnd,t);							//lerp towards the end box position
			t += Time.deltaTime*duration;											//count down the timer
			yield return null;
		}
		Destroy(gameObject);
	}
	
	void OnGUI()
	{
		GUI.color = color;
		GUI.Label(new Rect(pos.x,Screen.height-pos.y,0,0),text,style);
	}
}
