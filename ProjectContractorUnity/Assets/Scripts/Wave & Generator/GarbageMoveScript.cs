using UnityEngine;
using System.Collections;

public class GarbageMoveScript : MonoBehaviour {

    Rigidbody _rigidbody;
	// Use this for initialization
	void Start () {
        _rigidbody = GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void Update () {
        //transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z - 0.1f);
        //_rigidbody.velocity = new Vector3(0, 0, -5);
        _rigidbody.AddRelativeForce(new Vector3(0, 0, -1));
	}
}
