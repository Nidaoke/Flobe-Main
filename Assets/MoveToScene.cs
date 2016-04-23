using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class MoveToScene : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

		if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.Joystick1Button7) || Input.GetKeyDown(KeyCode.Joystick2Button7))
        {

            SceneManager.LoadScene(1);
        }
	}
}
