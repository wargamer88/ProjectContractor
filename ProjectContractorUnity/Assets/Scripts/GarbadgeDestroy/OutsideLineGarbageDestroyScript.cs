using UnityEngine;
using System.Collections;

public class OutsideLineGarbageDestroyScript : MonoBehaviour {
    /// <summary>
    /// <para>If the collision is of the tag garbage. Add it to the list and destroy the object.</para>
    /// </summary>
    /// <param name="pOther"></param>
	void OnCollisionEnter(Collision pOther)
    {
        if (pOther.gameObject.tag == "Garbage")
        {
            GameObject.FindObjectOfType<GarbageWaveScript>().DestroyedGarbage.Add(pOther.gameObject);
            Destroy(pOther.gameObject);
        }
    }
}
