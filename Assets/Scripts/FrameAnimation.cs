using UnityEngine;
using System.Collections;

//SpriteHandler was made to efficiently handle 2D sprite-based animation in Unity. It takes an image containing the frames of one or more animations and splits it into a List.
//The user can define how many animations are in the image and the frame index they start and end on, as well as the size of each animation frame, and the speed that the animation will run at.
//This script also contains a public function to easily switch between animation states from other scripts.
public class FrameAnimation : MonoBehaviour 
{
	public SpriteRenderer rendTarget;
	public Sprite[] frames;
	public Vector2[] animList;								//each entry = an animation on the sheet, and the x and y define the first and last frames of the animation
	public float animSpeed;					//the x and y size of the frames on this sheet, and the animation speed of this sprite (the x value is the frame advance rate in seconds, and the y is used as a timer for the frame change).
	public int startAnim;

	int curAnim;										//the entry of animList to be animated
	
	void Start()
	{
		frames = GameObject.Find("AnimationSlave/"+gameObject.name).GetComponent<AtlasSplitter>().frames;
		ChangeAnimation(startAnim);
	}
	
	//ChangeAnimation switches the current animation being shown, replacing it with the animation at (anim) in the animation list
	public void ChangeAnimation(int anim)
	{
		StopCoroutine("Animate");
		curAnim = anim;
		StartCoroutine(Animate(animList[curAnim]));
	}

	IEnumerator Animate(Vector2 anim)
	{
		int min = (int)anim.x, max = (int)anim.y, index = min;
		while(true)
		{
			rendTarget.sprite = frames[index];
			index = index >= max ? min : index+1;
			yield return new WaitForSeconds(animSpeed);
		}
	}
}
