using UnityEngine;
using System.Collections;

public class TitleAnim : MonoBehaviour 
{
	public Animator flobeTitle;
	void StopSpeed ()
	{
		flobeTitle.SetFloat("AnimSpeed",0.0f);
	}
}
