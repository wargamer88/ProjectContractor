using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GarbageTileScript : MonoBehaviour {

    private List<GameObject> _garbageList;
    private List<GameObject> _tobeDestroyedList;

    void Start()
    {
        _garbageList = new List<GameObject>();
    }

    void OnTriggerEnter(Collider pOther)
    {
        if (pOther.gameObject.tag == "Garbage")
        {
            _garbageList.Add(pOther.gameObject);
        }
        else if (pOther.gameObject.tag == "Projectile")
        {
            if (_garbageList.Count > 0)
            {
                _damageGarbage(pOther); 
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

    private void _damageGarbage(Collider pOther)
    {
        _tobeDestroyedList = new List<GameObject>();
        for (int i = 0; i < _garbageList.Count; i++)
        {
            _garbageList[i].GetComponent<GarbadgeDestoryScript>().HP--;
            if (_garbageList[i].GetComponent<GarbadgeDestoryScript>().HP <= 0)
            {
                _tobeDestroyedList.Add(_garbageList[i]);
            }
            _garbageList[i].GetComponent<GarbadgeDestoryScript>().CheckHealth(pOther.gameObject);
        }
        for (int i = 0; i < _tobeDestroyedList.Count; i++)
        {
            _garbageList.Remove(_tobeDestroyedList[i]);
        }
    }
}
