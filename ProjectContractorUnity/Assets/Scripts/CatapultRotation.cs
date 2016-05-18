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
                transform.Rotate(new Vector3(transform.rotation.x, (transform.rotation.y + 1), transform.rotation.z));
            }
            if (Input.mousePosition.y > Screen.height / 2)
            {
                transform.Rotate(new Vector3(transform.rotation.x, (transform.rotation.y - 1), transform.rotation.z));
            }
        }
    }

    void FireCatapult()
    {
        if (Input.GetMouseButton(0))
        {
            _timeHeld += 0.01f;
            _heldDown = true;
            if (_timeHeld >= 5)
            {
                _timeHeld = 5;
            }
        }
        if (Input.GetMouseButtonUp(0))
        {
            if (_heldDown)
            {
                _heldDown = false;
                GameObject bullet = GameObject.CreatePrimitive(PrimitiveType.Sphere);
                bullet.transform.position = transform.position;
                bullet.AddComponent<Rigidbody>();
                bullet.GetComponent<Renderer>().material.color = Color.red;
                bullet.GetComponent<Rigidbody>().velocity = 
            }
        }
    }
}
