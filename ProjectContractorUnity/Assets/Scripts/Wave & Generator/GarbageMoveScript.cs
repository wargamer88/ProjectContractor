using UnityEngine;
using System.Collections;

public class GarbageMoveScript : MonoBehaviour {

    private Rigidbody _rigidbody;

    private float _wave = 0;

    private float _frequency = 10000000000000000f;  // Speed of sine movement
    private float _magnitude = 2f;   // Size of sine movement

    private float _oldTime;
    private float _changeDirection = 0.5f;
    // Use this for initialization
    void Start () {
        _rigidbody = GetComponent<Rigidbody>();
        int random = Random.Range(0, 1);
        if (random == 0)
        {
            _frequency = -_frequency;
        }
        
        _wave = Random.Range(10, 150) / 100;

    }
	
	// Update is called once per frame
	void Update () {
        //transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z - 0.1f);
        //_rigidbody.velocity = new Vector3(0, 0, -5);
        //_rigidbody.AddForce(new Vector3(_wave, 0, -1));
        //_wave = Random.Range(-0.5f, 0.5f);
        //if (Time.time > (_oldTime + _changeDirection))
        //{
        //    _oldTime = Time.time;
        //    _wave = -Random.Range(10, 150)/100;

        //}
        //_rigidbody.AddForce(new Vector3(_wave, 0, -1) / 50, ForceMode.VelocityChange);
        _rigidbody.AddForce(new Vector3(Mathf.Sin(Time.time * _frequency) * _magnitude, 0, -1) / 50, ForceMode.VelocityChange);
        //transform.position = new Vector3(transform.position.x + 0.1f, transform.position.y, transform.position.z);
        //_rigidbody.velocity = new Vector3(_rigidbody.velocity.x + 100, 0, _rigidbody.velocity.z);
    }
}
