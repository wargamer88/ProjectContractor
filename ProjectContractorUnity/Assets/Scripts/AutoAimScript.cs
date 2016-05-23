using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class AutoAimScript : MonoBehaviour {

    [SerializeField]
    private float Speed;

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

	// Use this for initialization
	void Start () {
        _powerupsScript = FindObjectOfType<PowerupsScript>();
        _aimPlane = GameObject.Find("AimPlane");
        lineRenderer = GetComponent<LineRenderer>();
	}
	
	// Update is called once per frame
    void Update() {
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
            Physics.Raycast(vRay, out hit, 1000);
            UpdateTrajectory(transform.position + new Vector3(0.18f, 10.7f, 3.2f), hit.point);
        }
        if (Input.GetMouseButtonUp(0))
                {
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
                else if (_allowshoot && (hit.collider.gameObject.name == "AimPlane" || hit.collider.gameObject.tag == "Garbage"))
                {
                    _allowshoot = false;
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

    void UpdateTrajectory(Vector3 pStartPos, Vector3 pEndPosition)
    {
        List<Vector3> positions = new List<Vector3>();
        Vector3 lastPos = pStartPos;

        positions.Add(pStartPos);
        positions.Add(pEndPosition);
            
        BuildTrajectoryLine(positions);
    }
    void BuildTrajectoryLine(List<Vector3> positions)
    {
        lineRenderer.SetVertexCount(positions.Count);
        for (var i = 0; i < positions.Count; ++i)
        {
            lineRenderer.SetPosition(i, positions[i]);
        }
    }

}
