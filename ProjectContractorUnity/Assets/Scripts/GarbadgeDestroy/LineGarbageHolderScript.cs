using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class LineGarbageHolderScript : MonoBehaviour {

    /// <summary>
    /// <para>If the garbage hit the wall. Try to set it back in the game.</para>
    /// </summary>
    /// <param name="pOther"></param>
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
