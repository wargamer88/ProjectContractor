using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class AutoAimScript : MonoBehaviour
{

    [SerializeField]
    private float Speed;

    [SerializeField]
    private List<GameObject> _balls;
    public List<GameObject> Balls { set { _balls = value; } get { return _balls; } }

    private GameObject _chosenBall;
    public GameObject ChosenBall { set { _chosenBall = value; } }


    private GameObject _bullet;
    private Vector3 _ballOffset;
    private PowerupsScript _powerupsScript;

    private int _cooldown = 0;
    private bool _allowshoot = true;
    public bool Allowshoot { get { return _allowshoot; } }

    private float _DEBUGcounter = 0;
    
    private Vector3 _moveTarget;
    private bool _isMoving = false;
    private Vector3 _currentPos;

    private GameObject _aimPlane;
    private GameObject _depthMinePlane;
    LineRenderer lineRenderer;
    private RaycastHit hit;

    private float _oldTime;

    [SerializeField]
    private float _depthCooldown = 10;
    private bool _isDepthCooldown = false;
    public bool IsDepthCooldown { set { _isDepthCooldown = value; } get { return _isDepthCooldown; } }



    [SerializeField]
    private float _flameCooldown = 10;
    private bool _isFlameCooldown = false;
    public bool IsFlameCooldown { set { _isFlameCooldown = value; } get { return _isFlameCooldown; } }

    private bool _isBombCooldown = false;
    public bool IsBombCooldown { set { _isBombCooldown = value; } get { return _isBombCooldown; } }



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
        _depthMinePlane = GameObject.Find("DepthMinePlane");
        lineRenderer = GetComponent<LineRenderer>();
        _ballOffset = new Vector3(0, 10.19f, 1.82f);
        if (_balls != null)
        {
            _chosenBall = _balls[0];
        }
       // GetComponent<Animator>().Stop();
	}
	
	// Update is called once per frame
    void Update()
    {
        _input();
        _cooldownTimer();
        AutoAim();
    }

    void AutoAim()
    {
        if (Input.GetMouseButton(0))
        {
            Ray vRay = Camera.main.ScreenPointToRay(Input.mousePosition);
            Physics.Raycast(vRay, out hit, 10000);
        }
        if (Input.GetMouseButtonUp(0))
        {
            if (hit.collider != null)
            {

                if ((hit.collider.gameObject.tag == "Garbage" && _chosenBall == _balls[0] /*|| hit.collider.gameObject.name == "LineWall" || hit.collider.gameObject.name == "Lines"*/))
                {
                    if (Time.time > (_newShotTimer + (_shootAnimationTimer + _reloadAnimationTimer)) && _allowshoot && (_chosenBall == _balls[0]))  //_allowshoot && (_chosenBall == _balls[0]) )
                    {
                        _isBombCooldown = true;
                        _allowshoot = false;
                        _createBallAndShootingAnimation();
                    }
                }
                else if ((hit.collider.gameObject.tag == "GridTile" || hit.collider.gameObject.tag == "Garbage") && (_chosenBall == _balls[1] || _chosenBall == _balls[2]))
                {
                    if (Time.time > (_newShotTimer + (_shootAnimationTimer + _reloadAnimationTimer)) && _isDepthCooldown == false && (_chosenBall == _balls[1]))
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

        if (_isBombCooldown)
        {
            if (Time.time > (_newShotTimer + (_shootAnimationTimer + _reloadAnimationTimer)))  //_allowshoot && (_chosenBall == _balls[0]) )
            {
                _isBombCooldown = false;
            }
        }
    }
    private void _createBallAndShootingAnimation()
    {
        
        GetComponent<Animator>().Play("Shoot");
        _bullet = GameObject.Instantiate(_chosenBall);
        _newShotTimer = Time.time;
        //_bullet = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        _bullet.transform.position = transform.position + _ballOffset;
        _bullet.AddComponent<Rigidbody>();
        _bullet.GetComponent<Rigidbody>().mass = 0.01f;
        //_bullet.GetComponent<Rigidbody>().useGravity = false;
        _bullet.AddComponent<BulletScript>();
        _bullet.GetComponent<BulletScript>().PowerupsScript = _powerupsScript;
        //Physics.IgnoreCollision(_bullet.GetComponent<SphereCollider>(), _aimPlane.GetComponent<MeshCollider>());
        Vector3 velocity = new Vector3(0, 0, 0);
        if (_chosenBall == _balls[0])
        {
            Physics.IgnoreCollision(_bullet.GetComponent<SphereCollider>(), _depthMinePlane.GetComponent<BoxCollider>());
            velocity = hit.collider.gameObject.transform.position - _bullet.transform.position;
            _bullet.tag = "Projectile";
        }
        else if (_chosenBall == _balls[1])
        {
            _bullet.tag = "SpecialWeapon";
            _bullet.GetComponent<SphereCollider>().radius = _bullet.GetComponent<SphereCollider>().radius * 5;
            velocity = hit.point - _bullet.transform.position;
        }
        else if (_chosenBall == _balls[2])
        {
            _bullet.tag = "FireBarrel";
            velocity = hit.point - _bullet.transform.position;
        }
        transform.LookAt(hit.point);
        transform.eulerAngles = new Vector3(0, transform.eulerAngles.y, 0);
        _bullet.GetComponent<Rigidbody>().AddForce(velocity);
        _bullet.GetComponent<BulletScript>().ChosenBall = _bullet;

    }

}
