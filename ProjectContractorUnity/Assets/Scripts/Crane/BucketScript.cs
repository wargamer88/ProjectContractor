using UnityEngine;
using System.Collections;

public class BucketScript : MonoBehaviour {

    ChainScript _chainScript;
    CraneScript _craneScript;
    // Use this for initialization
    void Start () {

        _craneScript = GameObject.FindObjectOfType<CraneScript>();
        _chainScript = GameObject.FindObjectOfType<ChainScript>();
    }
	
	// Update is called once per frame
	void Update () {
	    
	}

    void OnCollisionEnter(Collision pOther)
    {
        //Destroy(other.gameObject);
        if (pOther.transform.parent.GetComponent<DestroyableScript>())
        {
            pOther.gameObject.transform.parent = this.transform.parent;
            pOther.gameObject.AddComponent<DestroyableScript>();
            _craneScript.CanMove = false;
            _chainScript.CanMove = false;
            _chainScript.GoingUp = true;
        }
    }
}
