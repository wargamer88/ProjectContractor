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

    /// <summary>
    /// Check if it does not hit with Chain.
    /// Check if parent has Destroyable script. If this is true then create the script ont the object itself.
    /// Set CanMove on false to other scripts and GoingUp true so it will go automaticly
    /// </summary>
    /// <param name="pOther"></param>
    void OnCollisionEnter(Collision pOther)
    {
        if (pOther.transform.name != "Chain")
        {
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
}
