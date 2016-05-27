using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class AutoAimScript : MonoBehaviour
{

    [SerializeField]
    private float Speed;

    [SerializeField]
    private float _reticleOffset;

    [SerializeField]
    private List<GameObject> _balls;

    private GameObject _chosenBall;

    private GameObject _bullet;
    private Vector3 _ballOffset;
    private PowerupsScript _powerupsScript;

    private int _cooldown = 0;
    private bool _allowshoot = true;
    private float _DEBUGcounter = 0;
    
    private Vector3 _moveTarget;
    private bool _isMoving = false;
    private Vector3 _currentPos;

    private GameObject _aimPlane;
    LineRenderer lineRenderer;
    private RaycastHit hit;

    private float _oldTime;


    [SerializeField]
    private float _depthCooldown = 10;
    public bool IsDepthCooldown { set { _isDepthCooldown = value; } }

    private bool _isDepthCooldown = false;


    [SerializeField]
    private float _flameCooldown = 10;

    public bool IsFlameCooldown { set { _isFlameCooldown = value; } }

    private bool _isFlameCooldown = false;

    private bool _isMouseUp = false;

    private float _newShotTimer;
    private bool _newShotTimerBool = false;

    private float _shootAnimationTimer;
    private float _reloadAnimationTimer;
    private float _completeAnimationTimer;

    // Use this for initialization
    void Start()
    {
        _powerupsScript = FindObjectOfType<PowerupsScript>();
        _aimPlane = GameObject.Find("AimPlane");
        lineRenderer = GetComponent<LineRenderer>();
        _ballOffset = new Vector3(0, 10.19f, 1.82f);
       // GetComponent<Animator>().Stop();
	}
	
	// Update is called once per frame
    void Update()
    {
        _input();
        _cooldownTimer();
        AutoAim();
        MoveCatapult();
    }

    void MoveCatapult()
    {
        if (_isMoving)
        {
            transform.position = Vector3.MoveTowards(transform.position, new Vector3(_moveTarget.x, transform.position.y, transform.position.z), 0.5f);
        }
	}

    void AutoAim()
    {

        if (Input.GetMouseButton(0))
        {
                Ray vRay = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(vRay, out hit, 10000))
            {
                if (hit.collider.gameObject.name != "Platform")
                {
                    lineRenderer.enabled = true;
                    Vector3 velocity = hit.point - (transform.position + _ballOffset);
                    UpdateTrajectory(transform.position + _ballOffset, velocity);
                }
            }
        }
        if (Input.GetMouseButtonUp(0))
        {
            if (hit.collider != null)
            {
                lineRenderer.enabled = false;
                if (hit.collider.gameObject.name == "Platform")
                {
                    if (hit.point.x < -27f)
                    {
                        _moveTarget = new Vector3(-27f, 0, 0);
                    }
                    else if (hit.point.x > 33f)
                    {
                        _moveTarget = new Vector3(33f, 0, 0);
                    }
                    else
                    {
                        _moveTarget = new Vector3(hit.point.x, 0, 0);
                    }
                    _isMoving = true;
                }
                if ((hit.collider.gameObject.name == "AimPlane" || hit.collider.gameObject.tag == "Garbage" || hit.collider.gameObject.name == "LineWall" || hit.collider.gameObject.name == "Lines"))
                {
                    if (Time.time > (_newShotTimer + (_shootAnimationTimer + _reloadAnimationTimer)) && _allowshoot && (_chosenBall == _balls[0]))  //_allowshoot && (_chosenBall == _balls[0]) )
                    {
                        _allowshoot = false;
                        _createBallAndShootingAnimation();
                    }
                    else if (Time.time > (_newShotTimer + (_shootAnimationTimer + _reloadAnimationTimer)) && _isDepthCooldown == false && (_chosenBall == _balls[1]))
                    {
                        _isDepthCooldown = true;
                        _createBallAndShootingAnimation();
                    }
                    else if (Time.time > (_newShotTimer + (_shootAnimationTimer + _reloadAnimationTimer)) && _isFlameCooldown == false && (_chosenBall == _balls[2]))
                    {
                        _isFlameCooldown = true;
                        _createBallAndShootingAnimation();
                    }
                }
            } 
        }

        if (!_allowshoot)
        {
            if (GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("Finished"))
            {
                _allowshoot = true;
            }
            if (GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("Shoot"))
            {
                _shootAnimationTimer = (GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).length);
            }
            if (GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("Reload"))
            {
                _reloadAnimationTimer = (GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).length);
            }
            if (GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("Nothing"))
            {
                _allowshoot = false;
            }
        }
    }

    void UpdateTrajectory(Vector3 initialPosition, Vector3 initialVelocity)
    {
        int numSteps = 20; // for example
        float timeDelta = 1.0f / initialVelocity.magnitude; // for example

        LineRenderer lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.SetVertexCount(numSteps);
            
        Vector3 position = initialPosition;
        Vector3 velocity = initialVelocity;
        for (int i = 0; i < numSteps; ++i)
        {
            lineRenderer.SetPosition(i, position);

            position += (velocity * _reticleOffset) * timeDelta + 0.5f * Physics.gravity * timeDelta * timeDelta;
            velocity += Physics.gravity * timeDelta;
        }
    }
    private void _input()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            if (_balls != null)
            {
                _chosenBall = _balls[0];
            }
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            if (_balls != null)
            {
                _chosenBall = _balls[1];
            }
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            if (_balls != null)
            {
                _chosenBall = _balls[2];
            }
        }
    }
    private void _cooldownTimer()
    {
        if (_isDepthCooldown)
        {
            if (Time.time > (_newShotTimer + _depthCooldown))
            {
                _isDepthCooldown = false;
            }
        }
        if (_isFlameCooldown)
        {
            if (Time.time > (_newShotTimer + _flameCooldown))
            {
                _isFlameCooldown = false;
            }
        }
    }
    private void _createBallAndShootingAnimation()
    {
        
        GetComponent<Animator>().Play("Shoot");
        _bullet = GameObject.Instantiate(_chosenBall);
        _newShotTimer = Time.time;
        //_bullet = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        _bullet.transform.localScale = new Vector3(2, 2, 2);
        _bullet.transform.position = transform.position + _ballOffset;
        _bullet.AddComponent<Rigidbody>();
        _bullet.GetComponent<Renderer>().material.color = Color.red;
        _bullet.AddComponent<BulletScript>();
        _bullet.GetComponent<BulletScript>().PowerupsScript = _powerupsScript;
        _bullet.AddComponent<BallGoingThroughWallScript>();
        if (_chosenBall != _balls[1])
        {
            Physics.IgnoreCollision(_bullet.GetComponent<SphereCollider>(), _aimPlane.GetComponent<MeshCollider>());
        }
        _bullet.tag = "Projectile";
        Vector3 velocity = hit.point - _bullet.transform.position;
        Debug.Log(velocity);
        transform.LookAt(hit.point);
        transform.eulerAngles = new Vector3(0, transform.eulerAngles.y, 0);
        _bullet.GetComponent<Rigidbody>().AddForce(velocity, ForceMode.VelocityChange);
        _bullet.GetComponent<BulletScript>().ChosenBall = _bullet;
    }

}
