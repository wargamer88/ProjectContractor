using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using System.Linq;
using UnityEngine.UI;

public class ExecuteEventScript : MonoBehaviour
{
    //Garbage and Tile both has this script!
    #region Variables
    //The eventWrapper where you can change the waves in the inspector and property to get the wrapper if the garbage hit the tile.
    [SerializeField]
    private List<EventTileWrapperScript> _eventWrapper = new List<EventTileWrapperScript>();
    public List<EventTileWrapperScript> EventWrapper { get { return _eventWrapper; } set { _eventWrapper = value; } }

    //Variables of the eventwrapper
    private _choices _event;
    private int _eventWave;
    private int _eventEveryXWave;
    private bool _eventEveryWave;
    private int _eventAmountOfObjects;
    private float _eventSpeedOfObjects;
    private float _eventTimeBetween;
    //current eventwrapper
    private EventTileWrapperScript _currentEvent;
    //property event to know the event from garbage
    public _choices Event { get { return _event; } }
    //property event wave to know the wave from garbage
    public int EventWave { get { return _eventWave; } set { _eventWave = value; } }
    //property of the speed for the increase speed event.
    public float EventSpeed { get { return _eventSpeedOfObjects; } }
    //Which event in the list on the tile.
    private int _eventCounter = 0;
    //reference to garbageWaveScript for the wave and spawning the object
    private GarbageWaveScript _garbageWaveScript;
    //Image of the tutorial hand
    private Image _hand;

    //spawned object counter.
    private int _spawnedLight = 0;
    private int _spawnedMedium = 0;
    private int _spawnedHeavy = 0;
    private int _spawnedSuperHeavy = 0;
    //If _boatfred is set.
    private bool _isPrefab = true;
    //SerializedField to put in the prefab
    [SerializeField]
    private GameObject _boatPrefab;
    #endregion

    /// <summary>
    /// <para>Search for the garbage wave script and hand</para>
    /// <para>Set tile foreach tile</para>
    /// </summary>
    void Start()
    {
        _garbageWaveScript = GameObject.FindObjectOfType<GarbageWaveScript>();
        _hand = GameObject.Find("Hand").GetComponent<Image>();
        foreach (EventTileWrapperScript addTile in _eventWrapper)
        {
            addTile.Tile = this.transform.name;
        }
    }

    /// <summary>
    /// <para>Start Or Restart tile is a function for the wave when it's started and set the next action in the wrapper </para>
    /// </summary>
    private void _startOrRestartTile()
    {
        //check if Tile has the tag because also garbage has this script.
        if (this.tag == "GridTile" && EventWrapper.Count > 0 && _eventCounter < EventWrapper.Count)
        {
            if (_eventWrapper[_eventCounter].EventWave == _garbageWaveScript.Wave)
            {
                //set all the wrapper variables in script variables.
                _event = _eventWrapper[_eventCounter].ChosenEvent;
                _eventWave = _eventWrapper[_eventCounter].EventWave;
                _eventEveryWave = _eventWrapper[_eventCounter].IsEveryWave;
                _eventEveryXWave = _eventWrapper[_eventCounter].EveryXWave;
                _eventSpeedOfObjects = _eventWrapper[_eventCounter].SpeedOfObject;
                _eventAmountOfObjects = _eventWrapper[_eventCounter].AmountOfObject;
                _eventTimeBetween = _eventWrapper[_eventCounter].TimeBetweenSpawn;
                _currentEvent = _eventWrapper[_eventCounter];
                //Get the count of the total amount of spawning objects;
                if (!_garbageWaveScript.DeadLaneList.Contains(this.gameObject.name.Substring(0,1)))
                {
                    _garbageWaveScript.SpawnAmount += _eventAmountOfObjects;
                }
                //set the spawned objects
                _spawnedLight = 0;
                _spawnedMedium = 0;
                _spawnedHeavy = 0;
                _spawnedSuperHeavy = 0;

                _eventCounter++;
                
            }
        }
    }
    /// <summary>
    /// <para>If it is not the first wave. Disbale the hand if it was enabled</para>
    /// <para>Get the new wave to read the event wrapper</para>
    /// <para>Look at the events</para>
    /// </summary>
    void Update()
    {
        if (_garbageWaveScript.Wave != 1)
        {
            if (_hand.enabled)
            {
                _hand.enabled = false;
            }
        }
        _startOrRestartTile();

        _switchEvent();
    }
    /// <summary>
    /// <para>OnTriggerEnter will do twice once for the tile and once for the garabge. When the hit it looks if there is a event on the tile</para>
    /// </summary>
    /// <param name="pOther">Hit of other object</param>
    void OnTriggerEnter(Collider pOther)
    {
        if (pOther.GetComponent<ExecuteEventScript>())
        {
            //Tutorial hand is shown if garbage hit the tile with the event.
            if (this.tag == "Garbage" && pOther.GetComponent<ExecuteEventScript>().EventWrapper.Count != 0)
            {
                if (pOther.GetComponent<ExecuteEventScript>().Event == _choices.ShowTutorialBottle && _garbageWaveScript.Wave == pOther.GetComponent<ExecuteEventScript>().EventWave)
                {
                    _hand.enabled = true;
                }
                //if the event is explode barrel it looks and put new objects.
                else if (pOther.GetComponent<ExecuteEventScript>().Event == _choices.ExplodesBarrel && _garbageWaveScript.Wave == pOther.GetComponent<ExecuteEventScript>().EventWave)
                {
                    GameObject bottle = _garbageWaveScript.LightGarbage[0];
                    for (int i = 0; i < 3; i++)
                    {
                        _garbageWaveScript.SpawnGarbage(1, this.transform.position.x + i, 1, this.transform.position.z, bottle);
                    }
                    Destroy(pOther);
                    Destroy(this);
                }
                //If event is increase speed the garbage will increase the speed with the speed that is in the inspector
                else if (pOther.GetComponent<ExecuteEventScript>().Event == _choices.IncreaseSpeed && _garbageWaveScript.Wave == pOther.GetComponent<ExecuteEventScript>().EventWave)
                {
                    this.GetComponent<GarbageMoveScript>().Speed = -pOther.GetComponent<ExecuteEventScript>().EventSpeed;
                }
                //If event is change lane. The garbage that hit this tile will randomly change lane to the right or left if possible.
                else if (pOther.GetComponent<ExecuteEventScript>().Event == _choices.ChangeLanes && _garbageWaveScript.Wave == pOther.GetComponent<ExecuteEventScript>().EventWave)
                {
                    string letter = pOther.name.Substring(0, 1);
                    string numberstring = pOther.name.Substring(1, pOther.name.Length - 1);
                    int number = Convert.ToInt32(numberstring);
                    List<string> possibleLetters = new List<string>();
                    if (letter == "A")
                    {
                        possibleLetters.Add("B");
                    }
                    else if (letter == "B")
                    {
                        possibleLetters.Add("A");
                        possibleLetters.Add("B");
                    }
                    else if (letter == "C")
                    {
                        possibleLetters.Add("B");
                        possibleLetters.Add("D");
                    }
                    else if (letter == "D")
                    {
                        possibleLetters.Add("C");
                        possibleLetters.Add("E");
                    }
                    else if (letter == "E")
                    {
                        possibleLetters.Add("D");
                    }
                    string nextLetter = possibleLetters[UnityEngine.Random.Range(0, possibleLetters.Count)];
                    int nextNumber = number - 1;
                    GameObject nextTile = GameObject.Find(nextLetter + "" + nextNumber.ToString());
                    this.GetComponent<GarbageMoveScript>().ChangeLane(nextTile.transform.position);

                }
                //If event is miltitrash the first object that hit this tile will change into 3 other garbage and destroy the original one
                else if (pOther.GetComponent<ExecuteEventScript>().Event == _choices.MiltiTrash && _garbageWaveScript.Wave == pOther.GetComponent<ExecuteEventScript>().EventWave)
                {
                    pOther.GetComponent<ExecuteEventScript>().EventWave = 0;
                    int random = UnityEngine.Random.Range(0, 3);
                    _garbageWaveScript.SpawnGarbage(1, pOther.transform.position.x + 5, 4, pOther.transform.position.z - 3, _garbageWaveScript.LightGarbage[random], -pOther.GetComponent<ExecuteEventScript>().EventSpeed, pOther.GetComponent<ExecuteEventScript>().gameObject.name);
                    random = UnityEngine.Random.Range(0, 3);
                    _garbageWaveScript.SpawnGarbage(1, pOther.transform.position.x - 5, 4, pOther.transform.position.z - 3, _garbageWaveScript.LightGarbage[random], -pOther.GetComponent<ExecuteEventScript>().EventSpeed, pOther.GetComponent<ExecuteEventScript>().gameObject.name);
                    random = UnityEngine.Random.Range(0, 3);
                    _garbageWaveScript.SpawnGarbage(1, pOther.transform.position.x + 0.1f, 4, pOther.transform.position.z + 10, _garbageWaveScript.LightGarbage[random], -pOther.GetComponent<ExecuteEventScript>().EventSpeed, pOther.GetComponent<ExecuteEventScript>().gameObject.name);
                    _garbageWaveScript.SpawnAmount += 3;
                    _garbageWaveScript.DestroyedGarbage.Add(this.gameObject);
                    Destroy(this.gameObject);
                }
            }
        }
        //If the other hit has the component script Boat Event and if the target position of the boat is this tile then spawn an garbage. I use everyXwave variable to choose wich kind of garbage will spawn
        if (pOther.GetComponent<BoatEventScript>() && _event == _choices.BoatToTile && pOther.GetComponent<BoatEventScript>().TargetPosition == this.transform.position)
        {
            if (_eventEveryXWave == 1)
            {
                _event = _choices.SpawnRandomLight;
            }
            if (_eventEveryXWave == 2)
            {
                _event = _choices.SpawnRandomMedium;
            }
            if (_eventEveryXWave == 3)
            {
                _event = _choices.SpawnRandomHeavy;
            }
            Destroy(pOther.gameObject);

        }
    }
    /// <summary>
    /// <para>Switch event are events that start at the start of the wave. Mostly spawning objects.</para>
    /// </summary>
    private void _switchEvent()
    {
        switch (_event)
        {
            //Spawns a bottle for the tutorial
            case _choices.SpawnBottle:
                SpawnObjectScript.SpawnBottle(_eventWave, _eventTimeBetween, _eventAmountOfObjects, _garbageWaveScript, this.transform.position,this);
                break;
            //Spawn random light. Also if everywave or every amount of wave is filled.
            case _choices.SpawnRandomLight:
                _spawnedLight = SpawnObjectScript.SpawnRandomLight(_eventWave, _eventTimeBetween, _eventAmountOfObjects, _spawnedLight, _garbageWaveScript, this.transform.position,this, _eventSpeedOfObjects);
                if (_eventEveryWave && _eventEveryXWave != 0)
                {
                    if (_garbageWaveScript.Wave == _eventWave)
                    {
                        _eventWave = _eventWave + _eventEveryXWave;
                        _eventWrapper.Add(_currentEvent);
                    }
                }
                else if (_eventEveryWave)
                {
                    if (_garbageWaveScript.Wave == _eventWave)
                    {
                        _eventWave++;
                        _eventWrapper.Add(_currentEvent);
                    }
                }
                break;
            //Spawn random Medium. Also if everywave or every amount of wave is filled.
            case _choices.SpawnRandomMedium:
                _spawnedMedium = SpawnObjectScript.SpawnRandomMedium(_eventWave, _eventTimeBetween, _eventAmountOfObjects, _spawnedMedium, _garbageWaveScript, this.transform.position,this, _eventSpeedOfObjects);
                if (_eventEveryWave && _eventEveryXWave != 0)
                {
                    if (_garbageWaveScript.Wave == _eventWave)
                    {
                        _eventWave = _eventWave + _eventEveryXWave;
                        _eventWrapper.Add(_currentEvent);
                    }
                }
                else if(_eventEveryWave)
                {
                    if (_garbageWaveScript.Wave == _eventWave)
                    {
                        _eventWave++;
                        _eventWrapper.Add(_currentEvent);
                    }
                }
                break;
            //Spawn random heavy. Also if everywave or every amount of wave is filled.
            case _choices.SpawnRandomHeavy:
                _spawnedHeavy = SpawnObjectScript.SpawnRandomHeavy(_eventWave, _eventTimeBetween, _eventAmountOfObjects, _spawnedHeavy, _garbageWaveScript, this.transform.position,this, _eventSpeedOfObjects);
                if (_eventEveryWave && _eventEveryXWave != 0)
                {
                    if (_garbageWaveScript.Wave == _eventWave)
                    {
                        _eventWave = _eventWave + _eventEveryXWave;
                        _eventWrapper.Add(_currentEvent);
                    }
                }
                else if(_eventEveryWave)
                {
                    if (_garbageWaveScript.Wave == _eventWave)
                    {
                        _eventWave++;
                        _eventWrapper.Add(_currentEvent);
                    }
                }
                break;
            //Spawn random super heavy. Also if everywave or every amount of wave is filled.
            case _choices.SpawnSuperHeavy:
                _spawnedSuperHeavy = SpawnObjectScript.SpawnSuperHeavy(_eventWave, _eventTimeBetween, _eventAmountOfObjects, _spawnedSuperHeavy, _garbageWaveScript, this.transform.position,this, _eventSpeedOfObjects);
                break;
             //Spawn only a barrel
            case _choices.SpawnBarrel:
                if (_garbageWaveScript.Wave == _eventWave)
                {
                    GameObject Barrel = _garbageWaveScript.MediumGarbage[1];
                    _garbageWaveScript.SpawnGarbage(3, this.transform.position.x + 1, 1, this.transform.position.z, Barrel);
                    _event = _choices.None;
                }
                break;
            //When boat event first check if prefab is set to show the prefab otherwise the cube
            //create a invisiable cube that can hit the grid and change the event so the tile can read the event.
            case _choices.BoatEvent:
                GameObject boatPrefab;
                if (_boatPrefab != null)
                {
                    if (_isPrefab)
                    {
                        boatPrefab = GameObject.Instantiate(_boatPrefab);
                        boatPrefab.transform.position = new Vector3(-75, boatPrefab.transform.position.y, this.transform.position.z);
                        boatPrefab.AddComponent<BoatEventScript>();
                        boatPrefab.GetComponent<BoatEventScript>().SetTargetPositionAndSpeed(new Vector3(this.transform.position.x + 1000, this.transform.position.y, this.transform.position.z), _eventSpeedOfObjects + 0.2f);
                        _isPrefab = false;
                    }
                }
                else
                {
                    boatPrefab = GameObject.CreatePrimitive(PrimitiveType.Cube);
                    boatPrefab.GetComponent<BoxCollider>().enabled = false;
                    boatPrefab.transform.position = new Vector3(-75, this.transform.position.y, this.transform.position.z);
                    boatPrefab.AddComponent<BoatEventScript>();
                    boatPrefab.GetComponent<BoatEventScript>().SetTargetPositionAndSpeed(new Vector3(this.transform.position.x + 1000, this.transform.position.y, this.transform.position.z), _eventSpeedOfObjects + 0.2f);
                    boatPrefab.transform.localScale = new Vector3(10, 10, 10);
                }
                GameObject boat;
                boat = GameObject.CreatePrimitive(PrimitiveType.Cube);
                boat.GetComponent<MeshRenderer>().enabled = false;
                boat.transform.position = new Vector3(-75, this.transform.position.y, this.transform.position.z);
                boat.AddComponent<BoatEventScript>();
                boat.AddComponent<Rigidbody>();
                boat.GetComponent<Rigidbody>().useGravity = false;
                boat.GetComponent<BoatEventScript>().SetTargetPositionAndSpeed(this.transform.position,_eventSpeedOfObjects);
                boat.transform.localScale = new Vector3(10, 10, 10);
                _event = _choices.BoatToTile;
                break;
            default:
                break;
        }
    }

}