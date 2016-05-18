using UnityEngine;
using System.Collections;

public class AutoAimScript : MonoBehaviour {

    [SerializeField]
    private float Speed;

    [SerializeField]
    private GameObject Target;

    private bool _fireBullet = false;
    private GameObject _bullet;
    private bool _goingDown = false;
    private float _oldTime = 0;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        AutoAim();
	}

    Vector3 GetTrajectory(float pT, Vector3 pStartPosition, Vector3 pEndPosition, Vector3 pMiddlepoint)
    {
        return Mathf.Pow(1 - pT, 2) * pStartPosition + 2 * pT * (1 - pT) * pMiddlepoint + Mathf.Pow(pT, 2) * pEndPosition;
    }

void AutoAim()
    {
        if (Input.GetMouseButtonDown(0))
        {
            _bullet = GameObject.CreatePrimitive(PrimitiveType.Sphere);
            _bullet.transform.position = transform.position + new Vector3(0, 0.77f, -0.377f);
            _bullet.AddComponent<Rigidbody>();
            _bullet.GetComponent<Renderer>().material.color = Color.red;
            _bullet.tag = "Projectile";
            Vector3 middlePoint = (Target.transform.position - _bullet.transform.position) + new Vector3(0, 10000, 0);
            Vector3 velocity = GetTrajectory(0, _bullet.transform.position, Target.transform.position, middlePoint);
            Debug.Log(velocity);
            _bullet.GetComponent<Rigidbody>().AddForce(velocity, ForceMode.VelocityChange);
        }
    }
}
