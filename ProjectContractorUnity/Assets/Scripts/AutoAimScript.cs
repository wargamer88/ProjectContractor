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
    
	// Use this for initialization
	void Start () {
        _powerupsScript = FindObjectOfType<PowerupsScript>();
    }
	
	// Update is called once per frame
	void Update () {
        AutoAim();
	}

void AutoAim()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (_allowshoot)
            {
                Ray vRay = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;
                if (Physics.Raycast(vRay, out hit, 1000))
                {
                    _bullet = GameObject.CreatePrimitive(PrimitiveType.Sphere);
                    _bullet.transform.position = transform.position + new Vector3(0.18f, 10.7f, 3.2f);
                    _bullet.AddComponent<Rigidbody>();
                    _bullet.GetComponent<Renderer>().material.color = Color.red;
                    _bullet.AddComponent<BulletScript>();
                    _bullet.GetComponent<BulletScript>().PowerupsScript = _powerupsScript;
                    _bullet.tag = "Projectile";
                    Vector3 velocity = hit.point - _bullet.transform.position;
                    Debug.Log(velocity);
                    _bullet.GetComponent<Rigidbody>().AddForce(velocity * Speed, ForceMode.VelocityChange);
                    _allowshoot = false;
                }
            }
        }

        if (!_allowshoot)
        {
            _cooldown ++;
            Debug.Log("Timer: " + _cooldown);
            if (_cooldown > (1.0f / Time.deltaTime) * 1.5f)
            {
                _allowshoot = true;
                _cooldown = 0;
            }

            
        }
    }
}
