using UnityEngine;
using System.Collections;

public class GarbadgeDestoryScript : MonoBehaviour {
    private float _hp;
    public float HP { get { return _hp; } set { _hp = value; } }
                                       // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnCollisionEnter(Collision pOther)
    {
        if (pOther.transform.tag == "Projectile")
        {
            _hp--;
            if (_hp == 0)
            {
                Destroy(this.gameObject);
                Destroy(pOther.gameObject);
            }
        }
    }
}
