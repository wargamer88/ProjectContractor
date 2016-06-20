using UnityEngine;
using System.Collections;

public class OutsideLineGarbageDestroyScript : MonoBehaviour {

	void OnCollisionEnter(Collision pOther)
    {
        if (pOther.gameObject.tag == "Garbage")
        {
            Destroy(pOther.gameObject);
        }
    }
}
