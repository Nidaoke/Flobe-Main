using UnityEngine;
using System.Collections;

public class TheForce : MonoBehaviour {

    public Rigidbody rgbd;

    public float power;
    public float radius;
    public Vector3 position;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

        if (Input.GetKeyDown(KeyCode.Space))
        {

            //rb.AddExplosionForce(power, explosionPos, radius, 3.0F);
            // rgbd.AddExplosionForce(power, transform.position, radius, 1);

            Collider[] colliders = Physics.OverlapSphere(transform.position, radius);
            foreach (Collider hit in colliders)
            {
                Rigidbody rb = hit.GetComponent<Rigidbody>();

                if (rb != null)
                    rb.AddExplosionForce(power, transform.position, radius, 0.0F);

            }
        }
	
	}
}
