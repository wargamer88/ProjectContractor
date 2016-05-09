using UnityEngine;
using System.Collections;

public class BucketScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	    
	}

    void OnCollisionEnter(Collision other)
    {
        //Destroy(other.gameObject);
        if (other.transform.parent.GetComponent<DestroyableScript>())
        {
            Destroy(other.gameObject);
        }
    }
}
