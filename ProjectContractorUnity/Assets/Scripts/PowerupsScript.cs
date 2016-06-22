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

    //Bools to check if a Powerup has spawned
    private bool _chompySpawned = false;
    private bool _sharkySpawned = false;
    private bool _whaleySpawned = false;

    //Bools to Check if you caught a Powerup
    private bool _caughtChompy = false;
    private bool _caughtSharky = false;
    private bool _caughtWhaley = false;

    //GameObject where all the Garbage are spawned in
    private GameObject _garbageParent = null;

    //List of Garbages using GarbageDestroyScript
    private List<GarbadgeDestoryScript> _garbageList;

    //Reference to GarbageWaveScript
    private GarbageWaveScript _garbageWaveScript; 
    #endregion

    /// <summary>
    /// <para>Load in Chompy, Sharky and Whaley from resources</para>
    /// <para>Find Garbage Wave Script and set Timer</para>
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
    /// <para>Calls a Function that Checks status of Powerups</para>
    /// </summary>
	void Update () {
        _getGarbageParent();
        _rndJumpingFishes();
        _checkPowerupStatus();

        //-----------------Temporary DEBUG
        if (Input.GetKeyDown(KeyCode.Keypad1))
        {
            _lightPowerup();
        }
        if (Input.GetKeyDown(KeyCode.Keypad2))
        {
            _mediumPowerup();
        }
        if (Input.GetKey(KeyCode.Keypad3))
        {
            _heavyPowerup();
        }
        //-----------------------------------
    }

    /// <summary>
    /// Return Garbage Parent
    /// </summary>
    private void _getGarbageParent()
    {
        if (_garbageParent == null)
        {
            _garbageParent = GameObject.Find("Garbage Parent");
        }
    }
    
    /// <summary>
    /// <para>Functons that randomly let fishes Jump</para>
    /// </summary>
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
        _timeJumpingFishSpawned = System.DateTime.Now.AddSeconds(10);
        GameObject GO;
        switch (FishRnd)
        {
            case 0:
                GO = (GameObject)Instantiate(_chompy, new Vector3(-120, -10, 0.4f), Quaternion.Euler(new Vector3(0, 90, 0)));
                GO.GetComponent<ChompyScript>().enabled = false;
                GO.AddComponent<FishClickedOnScript>();
                GO.GetComponent<FishClickedOnScript>().PowerupsScript = this;
                GO.GetComponent<FishClickedOnScript>().Jumping = false;
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
                GO.GetComponent<FishClickedOnScript>().Jumping = false;
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
                GO.GetComponent<FishClickedOnScript>().Jumping = false;
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
    /// <para>Checks if you clicked on one of the Fishes</para>
    /// <para>Instantiate the Powerup</para>
    /// </summary>
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
                    GO = (GameObject)Instantiate(_chompy, new Vector3(-54.8f, 1.8f, 0.4f), Quaternion.Euler(new Vector3(0, 270, 0)));
                    GO.GetComponent<ChompyScript>().enabled = false;
                    GO.AddComponent<FishClickedOnScript>();
                    GO.GetComponent<FishClickedOnScript>().PowerupsScript = this;
                    GO.name = "Chompy";
                    break;
                case 1: //Right
                    GO = (GameObject)Instantiate(_chompy, new Vector3(54.8f, 1.8f, 0.4f), Quaternion.Euler(new Vector3(0, 90, 0)));
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
                    GO = (GameObject)Instantiate(_sharky, new Vector3(-61.4f, 1.8f, 20), Quaternion.Euler(new Vector3(0, 90, 0)));
                    GO.GetComponent<SharkyScript>().enabled = false;
                    GO.AddComponent<FishClickedOnScript>();
                    GO.GetComponent<FishClickedOnScript>().PowerupsScript = this;
                    GO.name = "Sharky";
                    break;
                case 1: //Right
                    GO = (GameObject)Instantiate(_sharky, new Vector3(61.4f, 1.8f, 20), Quaternion.Euler(new Vector3(0, 270, 0)));
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
                    GO = (GameObject)Instantiate(_whaley, new Vector3(-74.5f, 1.1f, 51.1f), Quaternion.Euler(new Vector3(0, 270, 0)));
                    GO.GetComponent<WhaleyScript>().enabled = false;
                    GO.AddComponent<FishClickedOnScript>();
                    GO.GetComponent<FishClickedOnScript>().PowerupsScript = this;
                    GO.name = "Whaley";
                    break;
                case 1: //Right
                    GO = (GameObject)Instantiate(_whaley, new Vector3(74.5f, 1.1f, 51.1f), Quaternion.Euler(new Vector3(0, 90, 0)));
                    GO.GetComponent<WhaleyScript>().enabled = false;
                    GO.AddComponent<FishClickedOnScript>();
                    GO.GetComponent<FishClickedOnScript>().PowerupsScript = this;
                    GO.name = "Whaley";
                    break;
            }
        }
    }
    
    /// <summary>
    /// <para>If clicked on fish return the right type</para>
    /// </summary>
    /// <param name="pJumping"></param>
    /// <param name="pPowerupType"></param>
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
    /// <para>Check light powerup criteria and execute the powerup if criteria match</para>
    /// <para>chompy: Snipes anything with 3+ hp</para>
    /// </summary>
    private void _lightPowerup()
    {
        Debug.Log("Chompy(light garbage) activated");
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
    /// <para>Check medium powerup criteria and execute the powerup if criteria match</para>
    /// <para>Sharky: Wipes the most populated lane</para>
    /// </summary>
    private void _mediumPowerup()
    {
        //local variables
        Debug.Log("Sharky(medium garbage) activated");
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
    /// <para>Check heavy powerup criteria and execute the powerup if criteria match</para>
    /// <para>Whaley: Damages everything by 1 and pushes back the lane</para>
    /// </summary>
    private void _heavyPowerup()
    {
        _garbageList = _garbageParent.GetComponentsInChildren<GarbadgeDestoryScript>().ToList();
        GameObject GO = (GameObject)Instantiate(_whaley, new Vector3(-0.6f, -27.4f, -39.1f), Quaternion.Euler(new Vector3(0, 0, 0)));
        GO.GetComponent<WhaleyScript>().GarbageWaveScript = _garbageWaveScript;
        GO.GetComponent<WhaleyScript>().Garbage = _garbageList;
        GO.GetComponent<BoxCollider>().enabled = false;
        Debug.Log("Whaley(heavy garbage) activated");
    }
}
