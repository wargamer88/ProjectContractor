using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using System;

public class PowerupsScript : MonoBehaviour {

    [SerializeField]
    private GarbageType _debugGarbageTest = GarbageType.none;

    [SerializeField]
    private GameObject Chompy;
    [SerializeField]
    private GameObject Sharky;
    [SerializeField]
    private GameObject Whaley;

    private DateTime _timeJumpingFishSpawned;

    private int _lightGarbage = 0;
    private int _mediumGarbage = 0;
    private int _heavyGarbage = 0;

    private bool _chompySpawned = false;
    private bool _sharkySpawned = false;
    private bool _whaleySpawned = false;

    private bool _caughtChompy = false;
    private bool _caughtSharky = false;
    private bool _caughtWhaley = false;

    private GameObject _garbageParent = null;
    private List<GarbadgeDestoryScript> _garbageList;
    private GarbageWaveScript _garbageWaveScript;

	// Use this for initialization
	void Start () {
        _garbageWaveScript = GameObject.FindObjectOfType<GarbageWaveScript>();
        _timeJumpingFishSpawned = System.DateTime.Now.AddSeconds(30);
    }
	
	// Update is called once per frame
	void Update () {
        //_debugPowerupTest();
        _getGarbageParent();

        _rndJumpingFishes();
        _checkPowerupStatus();

        if (Input.GetKeyDown(KeyCode.Space))
        {
            _mediumPowerup();
        }
        //_lightPowerup();
        //_mediumPowerup();
        //_heavyPowerup();
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
    
    private void _rndJumpingFishes()
    {
        

        int FishRnd = 4;
        int SpawnRnd = 0;
        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            //FishRnd = 2;
        }
        else
        {
            SpawnRnd = UnityEngine.Random.Range(0, 1000);
            if (SpawnRnd > 1) return;
            FishRnd = UnityEngine.Random.Range(0, 3);
        }

        if (_timeJumpingFishSpawned > System.DateTime.Now) return;
        _timeJumpingFishSpawned = System.DateTime.Now.AddSeconds(30);
        GameObject GO;
        switch (FishRnd)
        {
            case 0:
                GO = (GameObject)Instantiate(Chompy, new Vector3(-120, -10, 0.4f), Quaternion.Euler(new Vector3(0, 270, 0)));
                GO.GetComponent<ChompyScript>().enabled = false;
                GO.AddComponent<FishClickedOnScript>();
                GO.GetComponent<FishClickedOnScript>().PowerupsScript = this;
                GO.GetComponent<FishClickedOnScript>().Jumping = false;
                GO.AddComponent<Rigidbody>();
                GO.GetComponent<Rigidbody>().AddForce(new Vector3(50, 23, 0),ForceMode.VelocityChange);
                GO.name = "Chompy";
                break;
            case 1:
                GO = (GameObject)Instantiate(Sharky, new Vector3(-120, -10, 0.4f), Quaternion.Euler(new Vector3(0, 90, 0)));
                GO.GetComponent<SharkyScript>().enabled = false;
                GO.AddComponent<FishClickedOnScript>();
                GO.GetComponent<FishClickedOnScript>().PowerupsScript = this;
                GO.GetComponent<FishClickedOnScript>().Jumping = false;
                GO.AddComponent<Rigidbody>();
                GO.GetComponent<Rigidbody>().AddForce(new Vector3(50, 23, 0), ForceMode.VelocityChange);
                GO.name = "Sharky";
                break;
            case 2:
                GO = (GameObject)Instantiate(Whaley, new Vector3(-160, -10, 0.4f), Quaternion.Euler(new Vector3(0, 270, 0)));
                GO.GetComponent<WhaleyScript>().enabled = false;
                GO.AddComponent<FishClickedOnScript>();
                GO.GetComponent<FishClickedOnScript>().PowerupsScript = this;
                GO.GetComponent<FishClickedOnScript>().Jumping = false;
                GO.AddComponent<Rigidbody>();
                GO.GetComponent<Rigidbody>().AddForce(new Vector3(60, 27, 0), ForceMode.VelocityChange);
                GO.name = "Whaley";
                break;
            default:
                break;
        }
    }

    private void _checkPowerupStatus()
    {
        //Spawning clickable Chompy
        if (_caughtChompy && !_chompySpawned)
        {
            _caughtChompy = false;
            _chompySpawned = true;
            int rnd = UnityEngine.Random.Range(0, 2);
            GameObject GO;
            switch (rnd)
            {
                case 0: //Left
                    GO = (GameObject)Instantiate(Chompy, new Vector3(-54.8f, 1.8f, 0.4f), Quaternion.Euler(new Vector3(0, 270, 0)));
                    GO.GetComponent<ChompyScript>().enabled = false;
                    GO.AddComponent<FishClickedOnScript>();
                    GO.GetComponent<FishClickedOnScript>().PowerupsScript = this;
                    GO.name = "Chompy";
                    break;
                case 1: //Right
                    GO = (GameObject)Instantiate(Chompy, new Vector3(54.8f, 1.8f, 0.4f), Quaternion.Euler(new Vector3(0, 90, 0)));
                    GO.GetComponent<ChompyScript>().enabled = false;
                    GO.AddComponent<FishClickedOnScript>();
                    GO.GetComponent<FishClickedOnScript>().PowerupsScript = this;
                    GO.name = "Chompy";
                    break;
            }
        }
        //Spawning clickable Sharky
        if (_caughtSharky && !_sharkySpawned)
        {
            _caughtSharky = false;
            _sharkySpawned = true;
            int rnd = UnityEngine.Random.Range(0, 2);
            GameObject GO;
            switch (rnd)
            {
                case 0: //Left
                    GO = (GameObject)Instantiate(Sharky, new Vector3(-61.4f, 1.8f, 20), Quaternion.Euler(new Vector3(0, 90, 0)));
                    GO.GetComponent<SharkyScript>().enabled = false;
                    GO.AddComponent<FishClickedOnScript>();
                    GO.GetComponent<FishClickedOnScript>().PowerupsScript = this;
                    GO.name = "Sharky";
                    break;
                case 1: //Right
                    GO = (GameObject)Instantiate(Sharky, new Vector3(61.4f, 1.8f, 20), Quaternion.Euler(new Vector3(0, 270, 0)));
                    GO.GetComponent<SharkyScript>().enabled = false;
                    GO.AddComponent<FishClickedOnScript>();
                    GO.GetComponent<FishClickedOnScript>().PowerupsScript = this;
                    GO.name = "Sharky";
                    break;
            }

        }
        //Spawning clickable Whaley
        if (_caughtWhaley && !_whaleySpawned)
        {
            _caughtWhaley = false;
            _whaleySpawned = true;
            int rnd = UnityEngine.Random.Range(0, 2);
            GameObject GO;
            switch (rnd)
            {
                case 0: //Left
                    GO = (GameObject)Instantiate(Whaley, new Vector3(-74.5f, 1.1f, 51.1f), Quaternion.Euler(new Vector3(0, 270, 0)));
                    GO.GetComponent<WhaleyScript>().enabled = false;
                    GO.AddComponent<FishClickedOnScript>();
                    GO.GetComponent<FishClickedOnScript>().PowerupsScript = this;
                    GO.name = "Whaley";
                    break;
                case 1: //Right
                    GO = (GameObject)Instantiate(Whaley, new Vector3(74.5f, 1.1f, 51.1f), Quaternion.Euler(new Vector3(0, 90, 0)));
                    GO.GetComponent<WhaleyScript>().enabled = false;
                    GO.AddComponent<FishClickedOnScript>();
                    GO.GetComponent<FishClickedOnScript>().PowerupsScript = this;
                    GO.name = "Whaley";
                    break;
            }
        }
    }
    
    public void FishClickedOn(bool pJumping, GarbageType pPowerupType)
    {
        switch (pPowerupType)
        {
            case GarbageType.Light:
                if (pJumping)
                {
                    _caughtChompy = true;
                }
                else
                {
                    _chompySpawned = false;
                    _lightPowerup();
                }
                break;
            case GarbageType.Medium:
                if (pJumping)
                {
                    _caughtSharky = true;
                }
                else
                {
                    _sharkySpawned = false;
                    _mediumPowerup();
                }
                break;
            case GarbageType.Heavy:
                if (pJumping)
                {
                    _caughtWhaley = true;
                }
                else
                {
                    _whaleySpawned = false;
                    _heavyPowerup();
                }
                break;
        }
    }

    /// <summary>
    /// Check light powerup criteria and execute the powerup if criteria match
    /// <para>chompy: Snipes anything with 3+ hp</para>
    /// </summary>
    private void _lightPowerup()
    {
        Debug.Log("Chompy(light garbage) activated");
        _lightGarbage = 0;
        _garbageList = _garbageParent.GetComponentsInChildren<GarbadgeDestoryScript>().ToList();
        foreach (GarbadgeDestoryScript Garbage in _garbageList)
        {
            if (Garbage.GarbageType == GarbageType.Heavy)
            {
                GameObject GO = (GameObject)Instantiate(Chompy, new Vector3(-50, Garbage.transform.position.y, Garbage.transform.position.z), Quaternion.identity);
                GO.GetComponent<ChompyScript>().GarbageObject = Garbage.gameObject;
                GO.GetComponent<ChompyScript>().GarbageWaveScript = _garbageWaveScript;
            }
        }
    }

    /// <summary>
    /// Check medium powerup criteria and execute the powerup if criteria match
    /// <para>Sharky: Wipes the most populated lane</para>
    /// </summary>
    private void _mediumPowerup()
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

        List<GarbadgeDestoryScript> currentGarbage = new List<GarbadgeDestoryScript>();
        //remove garbage in most populated lane
        foreach (GarbadgeDestoryScript Garbage in _garbageList)
        {
            if (Garbage.CurrentLane == mostPopulatedLane)
            {
                currentGarbage.Add(Garbage);
            }
        }

        int sharkyPosX = 0;
        switch (mostPopulatedLane)
        {
            case 0:
                sharkyPosX = -20;
                break;
            case 1:
                sharkyPosX = -10;
                break;
            case 2:
                sharkyPosX = 0;
                break;
            case 3:
                sharkyPosX = 10;
                break;
            case 4:
                sharkyPosX = 20;
                break;
            default:
                break;
        }


        GameObject GO = (GameObject)Instantiate(Sharky, new Vector3(sharkyPosX, -27.4f, -39.1f), Quaternion.identity);
        GO.GetComponent<SharkyScript>().Garbage = currentGarbage;
        GO.GetComponent<SharkyScript>().GarbageWaveScript = _garbageWaveScript;
        GO.GetComponent<SharkyScript>().PosX = sharkyPosX;
        GO.GetComponent<BoxCollider>().enabled = false;
    }

    /// <summary>
    /// Check heavy powerup criteria and execute the powerup if criteria match
    /// <para>Whaley: Damages everything by 1 and pushes back the lane</para>
    /// </summary>
    private void _heavyPowerup()
    {
        _heavyGarbage = 0;
        _garbageList = _garbageParent.GetComponentsInChildren<GarbadgeDestoryScript>().ToList();
        GameObject GO = (GameObject)Instantiate(Whaley, new Vector3(-0.6f, -27.4f, -39.1f), Quaternion.Euler(new Vector3(0, 180, 0)));
        GO.GetComponent<WhaleyScript>().GarbageWaveScript = _garbageWaveScript;
        GO.GetComponent<WhaleyScript>().Garbage = _garbageList;
        GO.GetComponent<BoxCollider>().enabled = false;
        Debug.Log("Whaley(heavy garbage) activated");
    }








    /// <summary>
    /// Only call if anything hitted this trash
    /// <param name="pGarbageType">Put in the type of garbage with the enum</param>
    /// </summary>
    public void HitTrash(GarbageType pGarbageType)
    {
        //switch (pGarbageType)
        //{
        //    case GarbageType.Light:
        //        _lightGarbage++;
        //        Debug.Log("Light Garbage hit, Light garbage count: " + _lightGarbage);
        //        break;
        //    case GarbageType.Medium:
        //        _mediumGarbage++;
        //        Debug.Log("Medium Garbage hit, Medium garbage count: " + _mediumGarbage);
        //        break;
        //    case GarbageType.Heavy:
        //        _heavyGarbage++;
        //        Debug.Log("Heavy Garbage hit, Heavy garbage count: " + _heavyGarbage);
        //        break;
        //    case GarbageType.none:
        //        //nothing happens
        //        break;
        //    default:
        //        break;
        //}
    }

    /// <summary>
    /// Only call if nothing is hit, it will reset the counts for the powerups
    /// </summary>
    public void HitNothing()
    {
        //Debug.Log("Hit Nothing, Counters Reset");
        //_lightGarbage = 0;
        //_mediumGarbage = 0;
        //_heavyGarbage = 0;
    }
}
