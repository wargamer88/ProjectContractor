using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class PowerupsScript : MonoBehaviour {

    [SerializeField]
    private GarbageType _debugGarbageTest = GarbageType.none;

    [SerializeField]
    private GameObject Chompy;
    [SerializeField]
    private GameObject Sharky;
    [SerializeField]
    private GameObject Whaley;

    private int _lightGarbage = 0;
    private int _mediumGarbage = 0;
    private int _heavyGarbage = 0;

    private GameObject _garbageParent = null;
    private List<GarbadgeDestoryScript> _garbageList;
    private GarbageWaveScript _garbageWaveScript;

	// Use this for initialization
	void Start () {
        _garbageWaveScript = GameObject.FindObjectOfType<GarbageWaveScript>();
	}
	
	// Update is called once per frame
	void Update () {
        _debugPowerupTest();
        _getGarbageParent();

        _lightPowerup();
        _mediumPowerup();
        _heavyPowerup();
    }

    private void _debugPowerupTest()
    {
        if (Input.GetKeyDown(KeyCode.Space) && _debugGarbageTest != GarbageType.none)
        {
            switch (_debugGarbageTest)
            {
                case GarbageType.none:
                    break;
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
    }

    private void _getGarbageParent()
    {
        if (_garbageParent == null)
        {
            _garbageParent = GameObject.Find("Garbage Parent");
        }
    }

    /// <summary>
    /// Check light powerup criteria and execute the powerup if criteria match
    /// <para>chompy: Snipes anything with 3+ hp</para>
    /// </summary>
    private void _lightPowerup()
    {
        if (_lightGarbage == 3)
        {
            Debug.Log("Chompy(light garbage) activated");
            _lightGarbage = 0;
            _garbageList = _garbageParent.GetComponentsInChildren<GarbadgeDestoryScript>().ToList();
            foreach (GarbadgeDestoryScript Garbage in _garbageList)
            {
                if (Garbage.HP >= 3)
                {
                    GameObject GO = (GameObject)Instantiate(Chompy, new Vector3(-50, Garbage.transform.position.y, Garbage.transform.position.z), Quaternion.identity);
                    GO.GetComponent<ChompyScript>().GarbageObject = Garbage.gameObject;
                    GO.GetComponent<ChompyScript>().GarbageWaveScript = _garbageWaveScript;
                }
            }
        }
    }

    /// <summary>
    /// Check medium powerup criteria and execute the powerup if criteria match
    /// <para>Sharky: Wipes the most populated lane</para>
    /// </summary>
    private void _mediumPowerup()
    {
        if (_mediumGarbage == 2)
        {
            //local variables
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

            //fill the line variables with amount of objects
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

            //check which lane if most populated
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

            //remove garbage in most populated lane
            foreach (GarbadgeDestoryScript Garbage in _garbageList)
            {
                if (Garbage.CurrentLane == mostPopulatedLane)
                {
                    GameObject GO = (GameObject)Instantiate(Sharky, new Vector3(-50, Garbage.transform.position.y, Garbage.transform.position.z), Quaternion.identity);
                    GO.GetComponent<SharkyScript>().GarbageObject = Garbage.gameObject;
                    GO.GetComponent<SharkyScript>().GarbageWaveScript = _garbageWaveScript;
                }
            }
        }
    }

    /// <summary>
    /// Check heavy powerup criteria and execute the powerup if criteria match
    /// <para>Whaley: Damages everything by 1 and pushes back the lane</para>
    /// </summary>
    private void _heavyPowerup()
    {
        if (_heavyGarbage == 2)
        {
            Debug.Log("Whaley(heavy garbage) activated");
            _heavyGarbage = 0;
            _garbageList = _garbageParent.GetComponentsInChildren<GarbadgeDestoryScript>().ToList();
            foreach (GarbadgeDestoryScript Garbage in _garbageList)
            {
                GameObject GO = (GameObject)Instantiate(Whaley, new Vector3(-50, Garbage.transform.position.y, Garbage.transform.position.z), Quaternion.identity);
                GO.GetComponent<WhaleyScript>().GarbageObject = Garbage.gameObject;
                GO.GetComponent<WhaleyScript>().GarbageWaveScript = _garbageWaveScript;
            }
        }
    }

    /// <summary>
    /// Only call if anything hitted this trash
    /// <param name="pGarbageType">Put in the type of garbage with the enum</param>
    /// </summary>
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
            case GarbageType.none:
                //nothing happens
                break;
            default:
                break;
        }
    }

    /// <summary>
    /// Only call if nothing is hit, it will reset the counts for the powerups
    /// </summary>
    public void HitNothing()
    {
        _lightGarbage = 0;
        _mediumGarbage = 0;
        _heavyGarbage = 0;
    }
}
