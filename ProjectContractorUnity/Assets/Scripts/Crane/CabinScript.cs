using UnityEngine;
using System.Collections;

public class CabinScript : MonoBehaviour {

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
        if (pOther.transform.GetComponent<DestroyableScript>())
        {
            Destroy(pOther.gameObject);
            _craneScript.CanMove = true;
            _chainScript.CanMove = true;
            _chainScript.GoingDown = false;
            _chainScript.GoingUp = false;
        }
    }
}
