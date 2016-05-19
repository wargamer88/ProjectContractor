using UnityEngine;
using System.Collections;

public class CatapultRotation : MonoBehaviour {

    Vector3 previouseMousePosition;
    float _timeHeld = 0;
    bool _heldDown = false;

	// Use this for initialization
	void Start () {
        previouseMousePosition = Input.mousePosition;
	}
	
	// Update is called once per frame
	void Update () {
        RotateCatapult();
        FireCatapult();
	}

    void RotateCatapult()
    {
        if (Input.GetMouseButton(1))
        {
            if (Input.mousePosition.y < Screen.height/2)
            {
                transform.position = transform.position + new Vector3(0.1f, 0, 0);
            }
            if (Input.mousePosition.y > Screen.height / 2)
            {
                transform.position = transform.position - new Vector3(0.1f, 0, 0);
            }
        }
    }

    void FireCatapult()
    {
        if (Input.GetMouseButton(0))
        {
            _timeHeld += 100f;
            Debug.Log(_timeHeld);
            _heldDown = true;
            if (_timeHeld >= 1000)
            {
                _timeHeld = 1000;
            }
        }
        if (Input.GetMouseButtonUp(0))
        {
            if (_heldDown)
            {
                _heldDown = false;
                GameObject bullet = GameObject.CreatePrimitive(PrimitiveType.Sphere);
                bullet.transform.position = transform.position + new Vector3(0, 0.77f, -0.377f);
                bullet.AddComponent<Rigidbody>();
                bullet.tag = "Projectile";
                bullet.GetComponent<Renderer>().material.color = Color.red;
                Vector3 direction = (transform.position + transform.forward + (transform.up/2)) - transform.position;
                bullet.GetComponent<Rigidbody>().AddForce(direction * _timeHeld);
                _timeHeld = 0;
            }
        }
    }
}
