using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class LineGarbageHolderScript : MonoBehaviour {

    private List<GameObject> _garbage;
	void Start () {
        if (GameObject.FindGameObjectsWithTag("Garbage").Length > 0)
        {
            _garbage = GameObject.FindGameObjectsWithTag("Garbage").ToList();
        }
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnTriggerEnter(Collider pOther)
    {
        if (pOther.tag == "Garbage")
        {
            if (pOther.GetComponent<Rigidbody>().velocity.x > 0)
            {
                pOther.GetComponent<Rigidbody>().velocity = new Vector3(-2, pOther.GetComponent<Rigidbody>().velocity.y, pOther.GetComponent<Rigidbody>().velocity.z);
            }
            else if (pOther.GetComponent<Rigidbody>().velocity.x < 0)
            {
                pOther.GetComponent<Rigidbody>().velocity = new Vector3(2, pOther.GetComponent<Rigidbody>().velocity.y, pOther.GetComponent<Rigidbody>().velocity.z);
            }

            pOther.GetComponent<Rigidbody>().position = new Vector3(pOther.GetComponent<Rigidbody>().position.x, pOther.GetComponent<Rigidbody>().position.y, pOther.GetComponent<Rigidbody>().position.z);
        }
    }
}
