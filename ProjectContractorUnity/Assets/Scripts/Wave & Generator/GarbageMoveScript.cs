using UnityEngine;
using System.Collections;

public class GarbageMoveScript : MonoBehaviour {

    private Rigidbody _rigidbody;

    private float _wave = 0;


    // Use this for initialization
    void Start () {
        _rigidbody = GetComponent<Rigidbody>();
        _wave = Random.Range(-15, 15);
    }
	
	// Update is called once per frame
	void Update () {
        //transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z - 0.1f);
        //_rigidbody.velocity = new Vector3(0, 0, -5);
        _rigidbody.AddRelativeForce(new Vector3(_wave, 0, -1));
        _rigidbody.AddRelativeTorque(new Vector3(_wave, 0, -1));
	}
}
