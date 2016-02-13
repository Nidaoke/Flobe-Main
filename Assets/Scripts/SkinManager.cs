using UnityEngine;
using System.IO;
using System.Collections;
using System.Collections.Generic;

public class SkinManager : MonoBehaviour 
{
	public Renderer guideLines, line, multiplier;
	public string[] texturePacks;
	public Vector2 scrollPos = Vector2.zero;
	public GUISkin gameStyle;
	int touchIntention, skinTabY;
	public float scrollSlide, scrollGravity;
	bool skinsOpen;

	void Start()
	{
		ApplySkin(texturePacks[0]);
	}

	void ApplySkin(string path) 
	{
		Object[] pack = Resources.LoadAll(path);
		guideLines.material.mainTexture = pack[0] as Texture;
		line.material.mainTexture = pack[1] as Texture;
		multiplier.material.mainTexture = pack[2] as Texture;
		transform.GetChild(0).GetComponent<Renderer>().material.mainTexture = pack[3] as Texture;
		//Spawner.ballType1 = pack[4] as GameObject;
		//Spawner.ballType2 = pack[5] as GameObject;
	}

	void OnGUI()
	{ 
		if(GUI.Button(new Rect(0,Screen.height-skinTabY-25,50,25),"SKINS",gameStyle.button))
		{
			StopAllCoroutines();
			StartCoroutine(ShopAnim(skinsOpen));
		}
		scrollPos = GUI.BeginScrollView(new Rect(0,Screen.height-skinTabY,Screen.width,200),scrollPos,new Rect(0,0,texturePacks.Length*200,200),GUIStyle.none,GUIStyle.none); 
		for(int i = 0; i < texturePacks.Length; i++)
			if(GUI.Button(new Rect(i*200,0,200,200),texturePacks[i]) && touchIntention == 0)
				ApplySkin(texturePacks[i]);
		GUI.EndScrollView();
	}

	IEnumerator ShopAnim(bool dir)
	{
		float t = 0, val;
		while(t < 1)
		{
			val = dir ? Mathf.Lerp(200,0,t) : Mathf.Lerp(0,200,t);
			skinTabY = (int)val;
			t += Time.deltaTime*2;
			yield return null;
		}
		skinsOpen = !dir;
	}
	
	void Update()
	{
		if(Input.touchCount > 0)
		{
			Touch touch = Input.touches[0];
			if(touch.position.y > 200)
				return;
			if(touch.phase == TouchPhase.Began)
			{
				touchIntention = 0;
				scrollSlide = 0;
			}
			else if(touch.phase == TouchPhase.Moved)
			{
				float dt = Time.deltaTime/touch.deltaTime;
				if (dt == 0 || float.IsNaN(dt) || float.IsInfinity(dt))
					dt = 1.0f;
				Vector2 glassDelta = touch.deltaPosition * dt;
				scrollPos.x -= glassDelta.x;
				scrollSlide = glassDelta.x;
				touchIntention = 1;
			}
		}
		else if(scrollSlide != 0) 
		{
			scrollPos.x -= scrollSlide;
			if(scrollSlide > 0)
			{
				scrollSlide -= Time.deltaTime*scrollGravity;
				if(scrollSlide <= 0)
					scrollSlide = 0;
			}
			else
			{
				scrollSlide += Time.deltaTime*scrollGravity;
				if(scrollSlide >= 0)
					scrollSlide = 0;
			}
		}
	}
}
