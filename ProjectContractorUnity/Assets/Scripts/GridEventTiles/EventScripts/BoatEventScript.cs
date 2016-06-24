using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class BoatEventScript : MonoBehaviour {

    #region Variables
    //List of garbage
    private List<GameObject> _garbageList;
    //If there are more boats all in the list
    private List<BoatEventScript> _this;
    //Position the boat needs to go
    private Vector3 _targetPostion;
    //property so the execute eventScript can read if it is in position
    public Vector3 TargetPosition { get { return _targetPostion; } }
    //speed of the boat.
    private float _speed;
    #endregion

    /// <summary>
    /// <para>List of garbage and boats so boat don't hit the garbage or other boats</para>
    /// </summary>
    void Start()
    {
        _garbageList = GameObject.FindGameObjectsWithTag("Garbage").ToList();
        _this = GameObject.FindObjectsOfType<BoatEventScript>().ToList();
        if (this.GetComponent<BoxCollider>())
        {
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
	
    /// <summary>
    /// <para>When it is on position destory the boat</para>
    /// </summary>
	void Update () {
        if (this.transform.position == _targetPostion)
        {
            Destroy(this.gameObject);
        }
    }
    /// <summary>
    /// <para>Move the boat to the position</para>
    /// </summary>
    void FixedUpdate()
    {
        if (_targetPostion != null)
        {
            this.transform.position = Vector3.MoveTowards(this.transform.position, _targetPostion,_speed);
        }
    }
    /// <summary>
    /// <para>Set the position and the speed where the boat needs to go</para>
    /// </summary>
    /// <param name="pTargetPostion">Position where it needs to go</param>
    /// <param name="pSpeed">The speed how fast it needs to go</param>
    public void SetTargetPositionAndSpeed(Vector3 pTargetPostion, float pSpeed)
    {
        _speed = pSpeed;
        _targetPostion = pTargetPostion;
    }

    /// <summary>
    /// <para>In cease there is a ciollision. It does the same as the start</para>
    /// </summary>
    /// <param name="pOther">The object that is still able to hit the boat</param>
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

    }
}
