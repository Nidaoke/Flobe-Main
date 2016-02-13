using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AtlasSplitter : MonoBehaviour 
{
	public bool ready;
	public Texture2D frameAtlas;
	//public Texture2D[] frames;
	public Sprite[] frames;
	public int size, frameCount;

	//List<Texture2D> frameList = new List<Texture2D>();
	List<Sprite> frameList = new List<Sprite>();
	
	void Start()
	{
		StartCoroutine(SplitAtlas());
	}

	IEnumerator SplitAtlas() 
	{
		int gridSize = frameAtlas.width/size;				//splits the atlas into a grid, with frameSize squares
		for(int column = 0; column < gridSize; column++)																	//reads bottom left frame to top right
		{
			for(int row = 0; row < gridSize; row++)
			{
				if(frameCount == 0)
					yield break;
				frameList.Add(Sprite.Create(frameAtlas,new Rect(row*size,column*size,size,size),Vector2.one/2,size));																						//adds the frame to the animation list
				frameCount--;
				yield return null;
			}
		}
		frames = frameList.ToArray();
		ready = true;
	}
	
//	IEnumerator SplitAtlas() 
//	{
//		int gridSize = frameAtlas.width/frameSize;				//splits the atlas into a grid, with frameSize squares
//		for(int column = 0; column < gridSize; column++)																	//reads bottom left frame to top right
//		{
//			for(int row = 0; row < gridSize; row++)
//			{
//				if(frameCount == 0)
//					break;
//				Texture2D frame = new Texture2D(frameSize,frameSize,TextureFormat.ARGB32,false);			//creates a new frame to the specified dimensions
//				frame.SetPixels(frameAtlas.GetPixels(row*frameSize,column*frameSize,frameSize,frameSize));
//				frame.Apply(false,true);																					//applies all the pixel changes to the frame texture
//				frameList.Add(frame);																							//adds the frame to the animation list
//				frameCount--;
//				yield return null;
//			}
//		}
//		frames = frameList.ToArray();
//		ready = true;
//	}
}
