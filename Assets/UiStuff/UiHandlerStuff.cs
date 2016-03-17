using UnityEngine;
using System.Collections;

public class UiHandlerStuff : MonoBehaviour {

	public bool overrideZ;
	public float zToOverride;

	public Vector2 positionForSetting;
	public float personalScale;
	public float width;
	//public float height;

	// Use this for initialization
	void Start () {

	}

	// Update is called once per frame
	void Update () {

		width = Camera.main.orthographicSize * 2.0f * Screen.width / Screen.height;

		if(overrideZ)
			transform.position = Camera.main.ViewportToWorldPoint
				(new Vector3(positionForSetting.x,positionForSetting.y,zToOverride));
		else
			transform.position = Camera.main.ViewportToWorldPoint
				(new Vector3(positionForSetting.x,positionForSetting.y,1));

		transform.localScale = new Vector3 (personalScale * width / 24.15894f, transform.localScale.y, 1);
	}
}
