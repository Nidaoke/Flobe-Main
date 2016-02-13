using UnityEngine;
using System.Collections;

public class ScaleToParent : MonoBehaviour {

    public GameObject line;

	// Use this for initialization
	void Start () {

        line = GameObject.FindGameObjectWithTag("Line");
	}
	
	// Update is called once per frame
	void Update () {

        // Debug.Log(GetComponent<ParticleSystemRenderer>().lengthScale = line.transform.localScale.x);

        GetComponent<ParticleSystemRenderer>().lengthScale = line.transform.localScale.x / 1.8f;

        if (Input.GetKeyDown(KeyCode.P))
        {

           
        }
	}

    public void DooParticle()
    {

        if (!GetComponent<ParticleSystem>().isPlaying)
        {

            GetComponent<ParticleSystem>().Play();
        }
    }
}
