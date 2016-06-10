using UnityEngine;
using System.Collections;

public class GarbageMoveScript : MonoBehaviour {

    private Rigidbody _rigidbody;

    private float _speed = -1;
    public float Speed { set { _speed = value; } }

    private float _wave = 0;

    private float _frequency = 10000000000000000f;  // Speed of sine movement
    private float _magnitude = 2f;   // Size of sine movement

    private float _oldTime;
    private float _changeDirection = 0.5f;
    private bool _changeLane = false;
    private Vector3 _newPosition;
    private Rigidbody _oldSpeed;
    private bool _isSaveOldRigidbody = true;
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
	void FixedUpdate() {
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
        _rigidbody.AddForce(new Vector3(Mathf.Sin(Time.time * _frequency) * _magnitude, 0, _speed) / 50, ForceMode.VelocityChange);
        //transform.position = new Vector3(transform.position.x + 0.1f, transform.position.y, transform.position.z);
        //_rigidbody.velocity = new Vector3(_rigidbody.velocity.x + 100, 0, _rigidbody.velocity.z);


        if (_changeLane)
        {
            if (_isSaveOldRigidbody)
            {
                _oldSpeed = _rigidbody;
                _isSaveOldRigidbody = false;
            }
            this.transform.position = Vector3.MoveTowards(this.transform.position, _newPosition, 0.5f);

            if (this.transform.position.z <= _newPosition.z + 1)
            {
                _changeLane = false;
                _rigidbody = _oldSpeed;
            }
        }
    }

    public void ChangeLane(Vector3 pNewPosition)
    {
        _newPosition = pNewPosition;
        _changeLane = true;
    }
}
