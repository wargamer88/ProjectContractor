using UnityEngine;
using System.Collections;

public class GarbageMoveScript : MonoBehaviour {

    private Rigidbody _rigidbody;

    private float _speed = -1;
    public float Speed { set { _speed = value; } }

    private float _wave = 0;

    private float _frequency = 10000000000000000f;  // _speed of sine movement
    private float _magnitude = 2f;   // Size of sine movement

    private float _oldTime;
    private float _changeDirection = 0.5f;
    private bool _changeLane = false;
    private bool _changeLaneDone = false;
    private Vector3 _newPosition;
    private Rigidbody _oldSpeed;
    private bool _isSaveOldRigidbody = true;

    private bool _isMinus = false;
    private bool _isEntered = true;
    private bool _thisIsEntered = false;
    // Use this for initialization
    void Start () {
        Physics.IgnoreCollision(this.GetComponent<BoxCollider>(), GameObject.Find("AimPlane").GetComponent<MeshCollider>());
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
        _startChangeLane();
    }

    public void ChangeLane(Vector3 pNewPosition)
    {
        _rigidbody.constraints &= ~RigidbodyConstraints.FreezePositionX;
        _newPosition = pNewPosition;
        _changeLane = true;
    }

    private void _startChangeLane()
    {
        if (_changeLane)
        {
            if (this.transform.position.x > _newPosition.x && _thisIsEntered == false)
            {
                _isEntered = false;
                _thisIsEntered = true;
            }
            else if (_isEntered == true)
            {
                _isEntered = true;
                _thisIsEntered = true;
                _rigidbody.AddForce(new Vector3(-_speed * 5, 0, _speed) / 50, ForceMode.VelocityChange);

            }
            else if (_isEntered == false)
            {
                _rigidbody.AddForce(new Vector3(_speed * 5, 0, _speed) / 50, ForceMode.VelocityChange);
            }
            if (this.transform.position.x >= _newPosition.x && _isEntered)
            {
                _changeLane = false;
                _changeLaneDone = true;
                _isMinus = false;
            }
            else if (this.transform.position.x <= _newPosition.x && _isEntered == false)
            {
                _changeLane = false;
                _changeLaneDone = true;
                _isMinus = true;
            }

        }
        else
        {
            if (_changeLaneDone)
            {
                if (_isMinus)
                {
                    _rigidbody.AddForce(new Vector3(-_speed * 100, 0, _speed) / 50, ForceMode.VelocityChange);
                }
                else if (_isMinus == false)
                {
                    _rigidbody.AddForce(new Vector3(_speed * 100, 0, _speed) / 50, ForceMode.VelocityChange);
                }
                if (_rigidbody.velocity.x < 1 && _rigidbody.velocity.x > -1 && _isMinus == true)
                {
                    _rigidbody.velocity = new Vector3(-1.75f, 0, _rigidbody.velocity.z);
                    _changeLaneDone = false;
                    _rigidbody.constraints = RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezePositionY | RigidbodyConstraints.FreezeRotation;
                }
                else if (_rigidbody.velocity.x < 1 && _rigidbody.velocity.x > -1 && _isMinus == false)
                {
                    _rigidbody.velocity = new Vector3(1.75f, 0, _rigidbody.velocity.z);
                    _changeLaneDone = false;
                    _rigidbody.constraints = RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezePositionY | RigidbodyConstraints.FreezeRotation;
                }
            }
            else
            {
                _rigidbody.AddForce(new Vector3(0, 0, _speed) / 50, ForceMode.VelocityChange);
            }
        }
    }
}
