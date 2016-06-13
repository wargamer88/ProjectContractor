using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GarbageTileScript : MonoBehaviour {

    private List<GameObject> _garbageList;
    private List<GameObject> _tobeDestroyedList;

    public List<GameObject> GarbageList { get { return _garbageList; } set { _garbageList = value; } }

    void Start()
    {
        _garbageList = new List<GameObject>();
    }

    void OnTriggerEnter(Collider pOther)
    {
        if (pOther.gameObject.tag == "Garbage")
        {
            _garbageList.Add(pOther.gameObject);
            pOther.gameObject.GetComponent<GarbadgeDestoryScript>().CurrentTile = this;
        }
        else if (pOther.gameObject.tag == "Projectile")
        {
            if (_garbageList.Count > 0)
            {
                DamageGarbage(pOther); 
            }
            Destroy(pOther.gameObject);
        }
    }

    void OnTriggerExit(Collider pOther)
    {
        if (pOther.gameObject.tag == "Garbage")
        {
            _garbageList.Add(pOther.gameObject);
        }
    }

    public void DamageGarbage(Collider pOther)
    {
        _tobeDestroyedList = new List<GameObject>();
        for (int i = 0; i < _garbageList.Count; i++)
        {
            if (_garbageList[i] != null)
            {
                _garbageList[i].GetComponent<GarbadgeDestoryScript>().HP--;
                if (_garbageList[i].GetComponent<GarbadgeDestoryScript>().HP <= 0)
                {
                    _tobeDestroyedList.Add(_garbageList[i]);
                }
                _garbageList[i].GetComponent<GarbadgeDestoryScript>().CheckHealth(pOther.gameObject);
            }
            else
            {
                _tobeDestroyedList.Add(_garbageList[i]);
            }
        }
        for (int i = 0; i < _tobeDestroyedList.Count; i++)
        {
            _garbageList.Remove(_tobeDestroyedList[i]);
        }
    }
}
