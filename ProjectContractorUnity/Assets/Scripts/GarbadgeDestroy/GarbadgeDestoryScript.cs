using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GarbadgeDestoryScript : MonoBehaviour {

    private float _hp;
    private GarbageType _garbageType;
    public float HP { get { return _hp; } set { _hp = value; } }

    private GarbageWaveScript _garbageWaveScript;

    public GarbageType GarbageType { get { return _garbageType; } set { _garbageType = value; } }

                                       // Use this for initialization
    void Start () {
        _garbageWaveScript = GameObject.FindObjectOfType<GarbageWaveScript>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnCollisionEnter(Collision pOther)
    {
        if (pOther.transform.tag == "Projectile")
        {
            _hp--;
            Destroy(pOther.gameObject);
            if (_hp == 0)
            {
                pOther.gameObject.GetComponent<BulletScript>().DestroyBullet(true);
                _garbageWaveScript.DestroyedGarbage.Add(pOther.gameObject);
                Destroy(this.gameObject);
            }
        }
    }


}
