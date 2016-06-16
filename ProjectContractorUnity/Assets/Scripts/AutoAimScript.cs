using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class AutoAimScript : MonoBehaviour
{

    [SerializeField]
    private float Speed;

    [SerializeField]
    private GameObject _chosenBall;
    public GameObject ChosenBall { set { _chosenBall = value; } }

    [SerializeField]
    private GameObject _explosionPrefab;

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
    private RaycastHit hit;

    private float _oldTime;

    [SerializeField]
    private float _depthCooldown = 10;
    private bool _isDepthCooldown = false;
    public bool IsDepthCooldown { set { _isDepthCooldown = value; } get { return _isDepthCooldown; } }

    private bool _isMouseUp = false;

    private float _newShotTimer;
    private bool _newShotTimerBool = false;

    // Use this for initialization
    void Start()
    {
        _powerupsScript = FindObjectOfType<PowerupsScript>();
        _aimPlane = GameObject.Find("AimPlane");
        _ballOffset = new Vector3(0, 10.19f, 1.82f);
	}
	
	// Update is called once per frame
    void Update()
    {
        AutoAim();
    }

    void AutoAim()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray vRay = Camera.main.ScreenPointToRay(Input.mousePosition);
            Physics.Raycast(vRay, out hit, 10000);
            if (hit.collider != null)
            {
                if ((hit.collider.gameObject.name == "AimPlane" || hit.collider.gameObject.tag == "Garbage" /*|| hit.collider.gameObject.name == "LineWall" || hit.collider.gameObject.name == "Lines"*/))
                {
                    if (Time.time > (_oldTime + 0.5f))
                    {
                        _oldTime = Time.time;
                        _createBallAndShootingAnimation();
                    }
                }
            } 
        }
    }
    private void _createBallAndShootingAnimation()
    {
        GetComponent<Animator>().Play("Fast_Shoot");
        _bullet = GameObject.Instantiate(_chosenBall);
        _newShotTimer = Time.time;
        _bullet.transform.position = transform.position + _ballOffset;
        _bullet.AddComponent<Rigidbody>();
        _bullet.GetComponent<Rigidbody>().mass = 0.01f;
        _bullet.AddComponent<BulletScript>();
        _bullet.GetComponent<BulletScript>().PowerupsScript = _powerupsScript;
        _bullet.GetComponent<BulletScript>().ExplosionPrefab = _explosionPrefab;

        Physics.IgnoreCollision(_bullet.GetComponent<SphereCollider>(), _aimPlane.GetComponent<MeshCollider>());
        Vector3 velocity = new Vector3(0, 0, 0);
        velocity = hit.point - _bullet.transform.position;
        _bullet.tag = "Projectile";
        transform.LookAt(hit.point);
        transform.eulerAngles = new Vector3(0, transform.eulerAngles.y, 0);
        _bullet.GetComponent<Rigidbody>().AddForce(velocity);
        _bullet.GetComponent<BulletScript>().ChosenBall = _bullet;
    }
}
