using UnityEngine;
using System.Collections;

public class GarbageMoveScript : MonoBehaviour {

    private Rigidbody _rigidbody;

    private float _wave = 0;

    private float _frequency = 1000000000000.0f;  // Speed of sine movement
    private float _magnitude = 3f;   // Size of sine movement
    // Use this for initialization
    void Start () {
        _rigidbody = GetComponent<Rigidbody>();
        int random = Random.Range(0, 1);
        if (random == 0)
        {
            _frequency = -_frequency;
        }
    }
	
	// Update is called once per frame
	void Update () {
        //transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z - 0.1f);
        //_rigidbody.velocity = new Vector3(0, 0, -5);
        //_rigidbody.AddForce(new Vector3(_wave, 0, -1));
        //_wave = Random.Range(-0.5f, 0.5f);
        _rigidbody.AddForce(new Vector3(Mathf.Sin(Time.time * _frequency*2) * _magnitude, 0, -1) / 50, ForceMode.VelocityChange);
        //transform.position = new Vector3(transform.position.x + 0.1f, transform.position.y, transform.position.z);
        //_rigidbody.velocity = new Vector3(_rigidbody.velocity.x + 100, 0, _rigidbody.velocity.z);
	}
}
