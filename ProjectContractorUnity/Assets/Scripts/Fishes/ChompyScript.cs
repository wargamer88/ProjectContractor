﻿using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class ChompyScript : MonoBehaviour {

    [SerializeField]
    private float _speed = 10;

    private List<GameObject> _walls;

    private GameObject _garbageObject;
    private GarbageWaveScript _garbageWaveScript;
    public GameObject GarbageObject { get { return _garbageObject; } set { _garbageObject = value; } }
    public GarbageWaveScript GarbageWaveScript { get { return _garbageWaveScript; } set { _garbageWaveScript = value; } }

    // Use this for initialization
    void Start () {
        _walls = GameObject.FindGameObjectsWithTag("LineWall").ToList();
        foreach (GameObject wall in _walls)
        {
            Physics.IgnoreCollision(this.GetComponent<BoxCollider>(), wall.GetComponent<MeshCollider>());
        }
        this.transform.rotation = new Quaternion(0, -1, 0, 1);
    }
	
	// Update is called once per frame
	void Update () {
        if (GarbageObject == null)
        {
            Destroy(this.gameObject);
        }
        float step = _speed * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, GarbageObject.transform.position, step);
    }

    void OnCollisionEnter(Collision pOther)
    {
        if(pOther.gameObject == _garbageObject)
        {
            Destroy(this.gameObject);
            _garbageWaveScript.DestroyedGarbage.Add(_garbageObject);
            Destroy(_garbageObject);
        }
        else
        {
            if (pOther.gameObject.tag != "SpecialWeapon" || pOther.gameObject.tag != "Projectile")
            {
                Physics.IgnoreCollision(this.GetComponent<BoxCollider>(), pOther.gameObject.GetComponent<BoxCollider>()); 
            }
            else
            {
                Physics.IgnoreCollision(this.GetComponent<BoxCollider>(), pOther.gameObject.GetComponent<SphereCollider>());
            }
        }
    }
}
