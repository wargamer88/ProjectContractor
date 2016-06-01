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
    /// <summary>
    /// If collision is with a object that has Destroyable script it needs to destroy.
    /// Set parameters of CanMove in other scripts on true and change GoingDown in other script to False so can move again the same with GoingUp.
    /// </summary>
    /// <param name="pOther"></param>
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

    void OnMouseDown()
    {
        _chainScript.StartPosition = Input.mousePosition;
    }

    void OnMouseDrag()
    {
        _chainScript.CabinDrag = true;
    }
    void OnMouseUp()
    {
        _chainScript.CabinDrag = false;
    }
}
