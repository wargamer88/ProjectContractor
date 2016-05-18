using UnityEngine;
using System.Collections;

public class GarbadgeDestoryScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnCollisionEnter(Collision pOther)
    {
        if (pOther.transform.name == "Sphere")
        {
            Destroy(this.gameObject);
            Destroy(pOther.gameObject);
        }
    }
}
