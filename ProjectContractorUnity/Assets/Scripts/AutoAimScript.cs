using UnityEngine;
using System.Collections;

public class AutoAimScript : MonoBehaviour {

    [SerializeField]
    private float Speed;

    [SerializeField]
    private GameObject Target;

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
            GameObject bullet = GameObject.CreatePrimitive(PrimitiveType.Sphere);
            bullet.transform.position = transform.position + new Vector3(0, 0.77f, -0.377f);
            bullet.AddComponent<Rigidbody>();
            bullet.GetComponent<Renderer>().material.color = Color.red;
            Vector3 middlePoint = (Target.transform.position - bullet.transform.position) + new Vector3(0, 10000, 0);
            Debug.Log(middlePoint);
            Vector3 velocity = GetTrajectory(1, bullet.transform.position, Target.transform.position, middlePoint);
            Debug.Log(velocity);
            bullet.GetComponent<Rigidbody>().AddForce(velocity *2, ForceMode.VelocityChange);
        }
    }
}


//function BallisticVel(target: Transform, angle: float): Vector3 { var dir = target.position - transform.position; // get target direction var h = dir.y; // get height difference dir.y = 0; // retain only the horizontal direction var dist = dir.magnitude ; // get horizontal distance var a = angle * Mathf.Deg2Rad; // convert angle to radians dir.y = dist * Mathf.Tan(a); // set dir to the elevation angle dist += h / Mathf.Tan(a); // correct for small height differences // calculate the velocity magnitude var vel = Mathf.Sqrt(dist * Physics.gravity.magnitude / Mathf.Sin(2 * a)); return vel * dir.normalized; }

//var target: Transform; var grenadePrefab: Transform;

//function Update() { if (Input.MouseButtonDown(0)) { var grenade: Transform = Instantiate(grenadePrefab,...,...); grenade.rigidbody.velocity = BallisticVel(target, 45); // pass the angle and the target transform } } The higher the angle, the lesser the error when the heights are different (but the velocity is also lower, what may be a good thing for a grenade). The function doesn't check for invalid parameters, thus it may throw exceptions if the angle is too high (near to 90) or too low (near to 0).