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

    private GameObject _chosenBall;

    private GameObject _bullet;
    private PowerupsScript _powerupsScript;

    private int _cooldown = 0;
    private bool _allowshoot = true;
    
    private Vector3 _moveTarget;
    private bool _isMoving = false;
    private Vector3 _currentPos;

    private GameObject _aimPlane;
    LineRenderer lineRenderer;
    private RaycastHit hit;

    private float _oldTime;

	// Use this for initialization
    void Start()
    {
        _powerupsScript = FindObjectOfType<PowerupsScript>();
        _aimPlane = GameObject.Find("AimPlane");
        lineRenderer = GetComponent<LineRenderer>();
       // GetComponent<Animator>().Stop();
	}
	
	// Update is called once per frame
    void Update()
    {
        _input();
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
            if (Physics.Raycast(vRay, out hit, 1000))
            {
                if (hit.collider.gameObject.name != "Platform")
                {
                    lineRenderer.enabled = true;
                    Vector3 velocity = hit.point - (transform.position + new Vector3(0.18f, 10.7f, 3.2f));
                    UpdateTrajectory(transform.position + new Vector3(0.18f, 10.7f, 3.2f), velocity);
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
                else if (_allowshoot && (hit.collider.gameObject.name == "AimPlane" || hit.collider.gameObject.tag == "Garbage" || hit.collider.gameObject.name == "LineWall" || hit.collider.gameObject.name == "Lines"))
                {
                    GetComponent<Animator>().Play("Shoot");
                    _allowshoot = false;
                    //_bullet = GameObject.Instantiate(_chosenBall);
                    _bullet = GameObject.CreatePrimitive(PrimitiveType.Sphere);
                    _bullet.transform.localScale = new Vector3(2, 2, 2);
                    _bullet.transform.position = transform.position + new Vector3(0.18f, 10.7f, 3.2f);
                    _bullet.AddComponent<Rigidbody>();
                    _bullet.GetComponent<Renderer>().material.color = Color.red;
                    _bullet.AddComponent<BulletScript>();
                    _bullet.GetComponent<BulletScript>().PowerupsScript = _powerupsScript;
                    _bullet.AddComponent<BallGoingThroughWallScript>();
                    Physics.IgnoreCollision(_bullet.GetComponent<SphereCollider>(), _aimPlane.GetComponent<MeshCollider>());
                    _bullet.tag = "Projectile";
                    Vector3 velocity = hit.point - _bullet.transform.position;
                    Debug.Log(velocity);
                    transform.LookAt(hit.point);
                    transform.eulerAngles = new Vector3(0, transform.eulerAngles.y, 0);
                    _bullet.GetComponent<Rigidbody>().AddForce(velocity, ForceMode.VelocityChange);
                    _bullet.GetComponent<BulletScript>().ChosenBall = _chosenBall;
                }
            } 
        }
        if (!_allowshoot)
        {
            _cooldown++;
            Debug.Log("Timer: " + _cooldown);
            if (_cooldown > (1.0f / Time.deltaTime) * 1.5f)
            {
                _allowshoot = true;
                _cooldown = 0;
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

            position += (velocity * 2) * timeDelta + 0.5f * Physics.gravity * timeDelta * timeDelta;
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



}
