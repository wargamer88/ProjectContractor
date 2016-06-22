using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using System;

public class PowerupsScript : MonoBehaviour {

    #region Variables
    //The different GameObjects for the Fishes
    private GameObject _chompy;
    private GameObject _sharky;
    private GameObject _whaley;

    //set Time between Jumping Fishes
    private DateTime _timeJumpingFishSpawned;

    //GameObject where all the Garbage are spawned in
    private GameObject _garbageParent = null;

    //List of Garbages using GarbageDestroyScript
    private List<GarbadgeDestoryScript> _garbageList;

    //Reference to GarbageWaveScript
    private GarbageWaveScript _garbageWaveScript; 
    #endregion

    /// <summary>
    /// <para>Load in Chompy, Sharky and Whaley from resources</para>
    /// <para>Find Garbage Wave Script and set the first time check variable</para>
    /// </summary>
    void Start () {
        _chompy = (GameObject)Resources.Load("Chompy");
        _sharky = (GameObject)Resources.Load("Sharky");
        _whaley = (GameObject)Resources.Load("Whaley");
        _garbageWaveScript = GameObject.FindObjectOfType<GarbageWaveScript>();
        _timeJumpingFishSpawned = System.DateTime.Now.AddSeconds(30);
    }
	
	/// <summary>
    /// <para>Calls a Function that gets the Parent of the Garbage</para>
    /// <para>Calls a Function that Randomly lets fishes jump</para>
    /// </summary>
	void Update () {
        _getGarbageParent();
        _rndJumpingFishes();

    }

    /// <summary>
    /// Make sure Garbage Parent is loaded once
    /// </summary>
    private void _getGarbageParent()
    {
        if (_garbageParent == null)
        {
            _garbageParent = GameObject.Find("Garbage Parent");
        }
    }
    
    /// <summary>
    /// <para>spawn random fish when time has passed and random number is lower than specified number</para>
    /// </summary>
    private void _rndJumpingFishes()
    {
        int FishRnd = 4;
        int SpawnRnd = 0;
        SpawnRnd = UnityEngine.Random.Range(0, 1000);
        if (SpawnRnd > 1) return;
        FishRnd = UnityEngine.Random.Range(0, 3);

        if (_timeJumpingFishSpawned > System.DateTime.Now) return;
        _timeJumpingFishSpawned = System.DateTime.Now.AddSeconds(10);
        GameObject GO;
        switch (FishRnd)
        {
            case 0:
                GO = (GameObject)Instantiate(_chompy, new Vector3(-120, -10, 0.4f), Quaternion.Euler(new Vector3(0, 90, 0)));
                GO.GetComponent<ChompyScript>().enabled = false;
                GO.AddComponent<FishClickedOnScript>();
                GO.GetComponent<FishClickedOnScript>().PowerupsScript = this;
                GO.AddComponent<Rigidbody>();
                GO.GetComponent<Rigidbody>().AddForce(new Vector3(50, 23, 0),ForceMode.VelocityChange);
                GO.layer = 10;
                GO.name = "Chompy";
                break;
            case 1:
                GO = (GameObject)Instantiate(_sharky, new Vector3(-120, -10, 0.4f), Quaternion.Euler(new Vector3(0, 90, 0)));
                GO.GetComponent<SharkyScript>().enabled = false;
                GO.AddComponent<FishClickedOnScript>();
                GO.GetComponent<FishClickedOnScript>().PowerupsScript = this;
                GO.AddComponent<Rigidbody>();
                GO.GetComponent<Rigidbody>().AddForce(new Vector3(50, 23, 0), ForceMode.VelocityChange);
                GO.layer = 10;
                GO.name = "Sharky";
                break;
            case 2:
                GO = (GameObject)Instantiate(_whaley, new Vector3(-160, -10, 0.4f), Quaternion.Euler(new Vector3(0, 90, 0)));
                GO.GetComponent<WhaleyScript>().enabled = false;
                GO.AddComponent<FishClickedOnScript>();
                GO.GetComponent<FishClickedOnScript>().PowerupsScript = this;
                GO.AddComponent<Rigidbody>();
                GO.GetComponent<Rigidbody>().AddForce(new Vector3(60, 27, 0), ForceMode.VelocityChange);
                GO.layer = 10;
                GO.name = "Whaley";
                break;
            default:
                break;
        }
    }
    
    /// <summary>
    /// <para>start the powerup corresponding with the pPowerupType</para>
    /// </summary>
    /// <param name="pPowerupType"></param>
    public void FishClickedOn(GarbageType pPowerupType)
    {
        switch (pPowerupType)
        {
            case GarbageType.Light:
                _lightPowerup();
                break;
            case GarbageType.Medium:
                _mediumPowerup();
                break;
            case GarbageType.Heavy:
                _heavyPowerup();
                break;
        }
    }

    /// <summary>
    /// <para>fire Light powerup, it will check for Heavy garbage to destroy</para>
    /// <para>chompy: Snipes anything with 3+ hp</para>
    /// </summary>
    private void _lightPowerup()
    {
        _garbageList = _garbageParent.GetComponentsInChildren<GarbadgeDestoryScript>().ToList();
        foreach (GarbadgeDestoryScript Garbage in _garbageList)
        {
            if (Garbage.GarbageType == GarbageType.Heavy)
            {
                GameObject GO = (GameObject)Instantiate(_chompy, new Vector3(-50, 3, Garbage.transform.position.z), Quaternion.Euler(new Vector3(0, 90, 0)));
                GO.GetComponent<ChompyScript>().GarbageObject = Garbage.gameObject;
                GO.GetComponent<ChompyScript>().GarbageWaveScript = _garbageWaveScript;
            }
        }
    }

    /// <summary>
    /// <para>Check for most populated lane and spawn Sharky there</para>
    /// <para>Sharky: Wipes the most populated lane</para>
    /// </summary>
    private void _mediumPowerup()
    {
        //local variables
        _garbageList = _garbageParent.GetComponentsInChildren<GarbadgeDestoryScript>().ToList();
        int lane0 = 0;
        int lane1 = 0;
        int lane2 = 0;
        int lane3 = 0;
        int lane4 = 0;
        int mostTrash = 0;
        string mostPopulatedLane = "";

        //fill the line variables with amount of objects
        foreach (GarbadgeDestoryScript Garbage in _garbageList)
        {
            string letter = Garbage.CurrentTile.gameObject.name.Substring(0, 1);
            switch (letter)
            {
                case "A":
                    lane0++;
                    break;
                case "B":
                    lane1++;
                    break;
                case "C":
                    lane2++;
                    break;
                case "D":
                    lane3++;
                    break;
                case "E":
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
            mostPopulatedLane = "A";
        }
        if (lane1 > mostTrash)
        {
            mostTrash = lane1;
            mostPopulatedLane = "B";
        }
        if (lane2 > mostTrash)
        {
            mostTrash = lane2;
            mostPopulatedLane = "C";
        }
        if (lane3 > mostTrash)
        {
            mostTrash = lane3;
            mostPopulatedLane = "D";
        }
        if (lane4 > mostTrash)
        {
            mostTrash = lane4;
            mostPopulatedLane = "E";
        }

        List<GarbadgeDestoryScript> currentGarbage = new List<GarbadgeDestoryScript>();
        //add garbage in most populated lane to Sharky Garbage list
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
            case "A":
                sharkyPosX = -20*2;
                break;
            case "B":
                sharkyPosX = -10 * 2;
                break;
            case "C":
                sharkyPosX = 0;
                break;
            case "D":
                sharkyPosX = 10 * 2;
                break;
            case "E":
                sharkyPosX = 20 * 2;
                break;
            default:
                break;
        }


        GameObject GO = (GameObject)Instantiate(_sharky, new Vector3(sharkyPosX, -27.4f, -39.1f), Quaternion.identity);
        GO.GetComponent<SharkyScript>().Garbage = currentGarbage;
        GO.GetComponent<SharkyScript>().GarbageWaveScript = _garbageWaveScript;
        GO.GetComponent<SharkyScript>().PosX = sharkyPosX;
        GO.GetComponent<BoxCollider>().enabled = false;
    }

    /// <summary>
    /// <para>Spawn Whaley and pass the complete Garbage list on to Whaley</para>
    /// <para>Whaley: Damages everything by 1 and pushes back the lane</para>
    /// </summary>
    private void _heavyPowerup()
    {
        _garbageList = _garbageParent.GetComponentsInChildren<GarbadgeDestoryScript>().ToList();
        GameObject GO = (GameObject)Instantiate(_whaley, new Vector3(-0.6f, -27.4f, -39.1f), Quaternion.Euler(new Vector3(0, 0, 0)));
        GO.GetComponent<WhaleyScript>().GarbageWaveScript = _garbageWaveScript;
        GO.GetComponent<WhaleyScript>().Garbage = _garbageList;
        GO.GetComponent<BoxCollider>().enabled = false;
    }
}
