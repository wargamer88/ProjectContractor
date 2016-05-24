using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class PowerupsScript : MonoBehaviour {

    //if reached 3 powerup is spawned(a fish)
    private int powerupCounter = 0;

    private int _lightGarbage = 0;
    private int _mediumGarbage = 0;
    private int _heavyGarbage = 0;

    private GameObject _garbageParent = null;
    private List<GarbadgeDestoryScript> _garbageList;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            _heavyGarbage++;
        }

        Debug.Log(_heavyGarbage);


        if (_garbageParent == null)
        {
            _garbageParent = GameObject.Find("Garbage Parent");
        }
        //chompy: Snipes anything with 3+ hp
        if (_lightGarbage == 3)
        {
            Debug.Log("Chompy(light garbage) activated");
            _lightGarbage = 0;
            _garbageList = _garbageParent.GetComponentsInChildren<GarbadgeDestoryScript>().ToList();
            foreach (GarbadgeDestoryScript Garbage in _garbageList)
            {
                if(Garbage.HP >= 3)
                {
                    Destroy(Garbage.gameObject);
                }
            }
        }
        //Sharky: Wipes the most populated lane
        if (_mediumGarbage == 2)
        {
            Debug.Log("Sharky(medium garbage) activated");
            _mediumGarbage = 0;
        }
        //Whaley: Damages everything by 1 and pushes back the lane
        if (_heavyGarbage == 2)
        {
            Debug.Log("Whaley(heavy garbage) activated");
            _heavyGarbage = 0;
            _garbageList = _garbageParent.GetComponentsInChildren<GarbadgeDestoryScript>().ToList();
            foreach (GarbadgeDestoryScript Garbage in _garbageList)
            {
                Garbage.HP--;
                if (Garbage.HP == 0)
                {
                    Destroy(Garbage.gameObject);
                }
            }
        }
    }

    public void HitTrash(GarbageType pGarbageType)
    {
        switch (pGarbageType)
        {
            case GarbageType.Light:
                _lightGarbage++;
                break;
            case GarbageType.Medium:
                _mediumGarbage++;
                break;
            case GarbageType.Heavy:
                _heavyGarbage++;
                break;
            default:
                break;
        }
    }
    
    public void HitNothing()
    {
        /**
        _lightGarbage = 0;
        _mediumGarbage = 0;
        _heavyGarbage = 0;
        /**/
    }
}
