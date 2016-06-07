using UnityEngine;
using System.Collections;

public class PO : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "SpecialWeapon")
        {
            if (this.gameObject.tag != "LineWall")
            {
                other.gameObject.GetComponent<BulletScript>().BallPowerDepth(other.gameObject.transform.position);
            }
            else
            {
                Destroy(other.gameObject);
            }
        }
        else if (other.gameObject.tag == "FireBarrel")
        {
            other.gameObject.GetComponent<BulletScript>().BallPowerFire();
        }
    }
}
