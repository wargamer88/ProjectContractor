using UnityEngine;
using System.Collections;

public class DestroyProjectile : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnCollisionEnter(Collision pOther)
    {
        if (pOther.gameObject.tag == "Projectile")
        {
            Destroy(pOther.gameObject);
        }
    }
}
