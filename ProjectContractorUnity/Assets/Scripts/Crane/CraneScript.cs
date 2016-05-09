using UnityEngine;
using System.Collections;

public class CraneScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKey(KeyCode.A))
        {
            transform.position += new Vector3(0, 0, -0.1f);
        }
        if (Input.GetKey(KeyCode.D))
        {
            transform.position += new Vector3(0, 0, 0.1f);
        }
    }

    public void moveLeft()
    {
        transform.position += new Vector3(0, 0, 0.01f);
    }
}
