using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class BoatEventScript : MonoBehaviour {

    private List<GameObject> _garbageList;
    private GameObject _floor;
    private GameObject _depthMinePlane;
    private List<BoatEventScript> _this;

    private Vector3 _targetPostion;
    public Vector3 TargetPosition { get { return _targetPostion; } }
    private float _speed;

    void Start()
    {
        _garbageList = GameObject.FindGameObjectsWithTag("Garbage").ToList();
        _floor = GameObject.FindObjectOfType<FloorScript>().gameObject;
        _depthMinePlane = GameObject.FindObjectOfType<PO>().gameObject;
        _this = GameObject.FindObjectsOfType<BoatEventScript>().ToList();
        if (this.GetComponent<BoxCollider>())
        {
            Physics.IgnoreCollision(this.GetComponent<BoxCollider>(), _floor.GetComponent<BoxCollider>());
            Physics.IgnoreCollision(this.GetComponent<BoxCollider>(), _depthMinePlane.GetComponent<BoxCollider>());
            foreach (GameObject garbage in _garbageList)
            {
                Physics.IgnoreCollision(this.GetComponent<BoxCollider>(), garbage.GetComponent<BoxCollider>());
            }
            foreach (BoatEventScript boat in _this)
            {
                if (boat.GetComponent<BoxCollider>())
                {
                    Physics.IgnoreCollision(boat.gameObject.GetComponent<BoxCollider>(), this.GetComponent<BoxCollider>());
                }
            }
        }
    }
	
	void Update () {
	}

    void FixedUpdate()
    {
        if (_targetPostion != null)
        {
            this.transform.position = Vector3.MoveTowards(this.transform.position, _targetPostion,_speed);
        }
    }

    public void SetTargetPositionAndSpeed(Vector3 pTargetPostion, float pSpeed)
    {
        _speed = pSpeed;
        _targetPostion = pTargetPostion;
    }

    void OnCollisionEnter(Collision pOther)
    {
        if (pOther.gameObject.tag == "Garbage")
        {
            _this = GameObject.FindObjectsOfType<BoatEventScript>().ToList();
            _garbageList = GameObject.FindGameObjectsWithTag("Garbage").ToList();

            foreach (BoatEventScript boat in _this)
            {
                if (boat.GetComponent<BoxCollider>())
                {
                    Physics.IgnoreCollision(boat.gameObject.GetComponent<BoxCollider>(), this.GetComponent<BoxCollider>());
                }
            }
            
            foreach (GameObject garbage in _garbageList)
            {
                Physics.IgnoreCollision(this.GetComponent<BoxCollider>(), garbage.GetComponent<BoxCollider>());
            }
        }
        Debug.Log(_targetPostion);

    }
}
