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
            _mediumGarbage++;
        }

        Debug.Log(_mediumGarbage);


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
            _garbageList = _garbageParent.GetComponentsInChildren<GarbadgeDestoryScript>().ToList();
            int lane0 = 0;
            int lane1 = 0;
            int lane2 = 0;
            int lane3 = 0;
            int lane4 = 0;
            int mostTrash = 0;
            int mostPopulatedLane = 0;

            foreach (GarbadgeDestoryScript Garbage in _garbageList)
            {
                switch (Garbage.CurrentLane)
                {
                    case 0:
                        lane0++;
                        break;
                    case 1:
                        lane1++;
                        break;
                    case 2:
                        lane2++;
                        break;
                    case 3:
                        lane3++;
                        break;
                    case 4:
                        lane4++;
                        break;
                    default:
                        break;
                }
        }
            if (lane0 > mostTrash)
            {
                mostTrash = lane0;
                mostPopulatedLane = 0;
            }
            if (lane1 > mostTrash)
            {
                mostTrash = lane1;
                mostPopulatedLane = 1;
            }
            if (lane2 > mostTrash)
            {
                mostTrash = lane2;
                mostPopulatedLane = 2;
            }
            if (lane3 > mostTrash)
            {
                mostTrash = lane3;
                mostPopulatedLane = 3;
            }
            if (lane4 > mostTrash)
            {
                mostTrash = lane4;
                mostPopulatedLane = 4;
            }

            foreach (GarbadgeDestoryScript Garbage in _garbageList)
            {
                if (Garbage.CurrentLane == mostPopulatedLane)
                {
                    Destroy(Garbage.gameObject);
                }
            }
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
                Garbage.transform.position = new Vector3(Garbage.transform.position.x, Garbage.transform.position.y, 95);
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
