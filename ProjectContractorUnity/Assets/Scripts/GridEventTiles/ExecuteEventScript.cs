using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using System.Linq;
using UnityEngine.UI;

public class ExecuteEventScript : MonoBehaviour
{
    [SerializeField]
    private List<EventTileWrapperScript> _eventWrapper = new List<EventTileWrapperScript>();

    public List<EventTileWrapperScript> EventWrapper { get { return _eventWrapper; } set { _eventWrapper = value; } }

    private _choices _event;
    private int _eventWave;
    private int _eventEveryXWave;
    private bool _eventEveryWave;
    private int _eventAmountOfObjects;
    private float _eventSpeedOfObjects;
    private float _eventTimeBetween;
    private EventTileWrapperScript _currentEvent;

    public _choices Event { get { return _event; } }
    public int EventWave { get { return _eventWave; } set { _eventWave = value; } }

    public float EventSpeed { get { return _eventSpeedOfObjects; } }

    private int _eventCounter = 0;


    private GarbageWaveScript _garbageWaveScript;
    private GameObject _aimPlane;
    private Image _hand;

    private bool _isDestroyed = false;
    public bool IsDestroyed { set { _isDestroyed = value; } }

    private bool _isEventDone = false;

    public bool IsEventDone { set { _isEventDone = value; } get { return _isEventDone; } }

    private float _oldTime;

    private bool _isFirstTime = true;


    private int _spawnedLight = 0;
    private int _spawnedMedium = 0;
    private int _spawnedHeavy = 0;

    [SerializeField]
    private GameObject _boatPrefab;


    // Use this for initialization
    void Start()
    {
        _garbageWaveScript = GameObject.FindObjectOfType<GarbageWaveScript>();
        _hand = GameObject.Find("Hand").GetComponent<Image>();
        _aimPlane = GameObject.Find("AimPlane");

       // StartOrRestartTile();
        foreach (EventTileWrapperScript addTile in _eventWrapper)
        {
            addTile.Tile = this.transform.name;
        }
        //_eventList = GetComponent<EventTileScript>().Choices;
    }

    public void StartOrRestartTile(bool restart = false)
    {
        //if (this.name == "C7") Debug.Log("Eventwrapper contains amount items: " + _eventWrapper.Count);

        
        if (this.tag == "GridTile" && EventWrapper.Count > 0 && _eventCounter < EventWrapper.Count)
        {
            //if (this.name == "C7") Debug.Log("Boven Gelukt Count: " + _eventWrapper.Count);
            
            //_event = GetComponent<EventTileScript>().ChosenEvent;
            //_eventWave = GetComponent<EventTileScript>().EventWave;
            if (_eventWrapper[_eventCounter].EventWave == _garbageWaveScript.Wave)
            {
                //Debug.Log("Gelukt");
                _event = _eventWrapper[_eventCounter].ChosenEvent;
                _eventWave = _eventWrapper[_eventCounter].EventWave;
                _eventEveryWave = _eventWrapper[_eventCounter].IsEveryWave;
                _eventEveryXWave = _eventWrapper[_eventCounter].EveryXWave;
                _eventSpeedOfObjects = _eventWrapper[_eventCounter].SpeedOfObject;
                _eventAmountOfObjects = _eventWrapper[_eventCounter].AmountOfObject;
                _eventTimeBetween = _eventWrapper[_eventCounter].TimeBetweenSpawn;
                _currentEvent = _eventWrapper[_eventCounter];
                _eventCounter++;
                //Debug.Log("EventCount: " + _eventCounter);
                //Debug.Log("EventCurrentWave: " + _eventWave);
            }
        }
        //if (restart && _eventWrapper.Count > 0)
        //{
        //    if (this.name == "C7") Debug.Log("Boven Removed Count: " + _eventWrapper.Count);
        //    Debug.Log("Removed");
        //    _isEventDone = false;
        //    _eventWrapper.RemoveAt(0);
        //}
    }


    // Update is called once per frame
    void Update()
    {
        //if (_eventWrapper.Count != 0)
        //{
        //    if (_garbageWaveScript.SpawnedGarbage.Count == _eventAmountOfObjects && _garbageWaveScript.Wave == _eventWave)
        //    {
        //        StartOrRestartTile(true);
        //    }
        //}
        //if (_isEventDone && _garbageWaveScript.Wave == _eventWave)
        //{
        //    StartOrRestartTile(true);
        //    _isEventDone = false;
        //}
        if (_garbageWaveScript.Wave != 1)
        {
            if (_hand.enabled)
            {
                _hand.enabled = false;
            }
        }
        StartOrRestartTile();

        _switchEvent();
    }

    void OnTriggerEnter(Collider pOther)
    {
        //if (pOther.GetComponent<ExecuteEventScript>().EventWrapper.Count != 0)
        //{
        //    Debug.Log(pOther.GetComponent<ExecuteEventScript>().EventWrapper[0].EventWave);
        //}
        //if (pOther.GetComponent<ExecuteEventScript>().EventWrapper.Count != 0)
        //{
        //    if (this.tag == "Garbage" && _garbageWaveScript.Wave == pOther.GetComponent<ExecuteEventScript>().EventWrapper[0].EventWave)
        //    {
        //        pOther.GetComponent<ExecuteEventScript>().StartOrRestartTile(true);
        //    }
        //}


        if (this.tag == "Garbage" && pOther.GetComponent<ExecuteEventScript>().EventWrapper.Count != 0)
        {
            //Debug.Log(_eventWave);
            //Debug.Log("garbagewave " + _garbageWaveScript.Wave);
            if (pOther.GetComponent<ExecuteEventScript>().Event == _choices.ShowTutorialBottle && _garbageWaveScript.Wave == pOther.GetComponent<ExecuteEventScript>().EventWave)
            {
                _hand.enabled = true;
                _isEventDone = true;
            }
            else if (pOther.GetComponent<ExecuteEventScript>().Event == _choices.ExplodesBarrel && _garbageWaveScript.Wave == pOther.GetComponent<ExecuteEventScript>().EventWave)
            {
                GameObject bottle = _garbageWaveScript.LightGarbage[0];
                for (int i = 0; i < 3; i++)
                {
                    _garbageWaveScript._spawnGarbage(1, this.transform.position.x + i, 1, this.transform.position.z, bottle);
                }
                Destroy(pOther);
                Destroy(this);
            }
            else if (pOther.GetComponent<ExecuteEventScript>().Event == _choices.IncreaseSpeed && _garbageWaveScript.Wave == pOther.GetComponent<ExecuteEventScript>().EventWave)
            {
                this.GetComponent<GarbageMoveScript>().Speed = -pOther.GetComponent<ExecuteEventScript>().EventSpeed;
            }
            else if (pOther.GetComponent<ExecuteEventScript>().Event == _choices.ChangeLanes && _garbageWaveScript.Wave == pOther.GetComponent<ExecuteEventScript>().EventWave)
            {
                string letter = pOther.name.Substring(0, 1);
                string numberstring = pOther.name.Substring(1, pOther.name.Length -1);
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
                GameObject nextTile = GameObject.Find(nextLetter +""+ nextNumber.ToString());
                this.GetComponent<GarbageMoveScript>().ChangeLane(nextTile.transform.position);

            }
            else if (pOther.GetComponent<ExecuteEventScript>().Event == _choices.MiltiTrash && _garbageWaveScript.Wave == pOther.GetComponent<ExecuteEventScript>().EventWave)
            {

                pOther.GetComponent<ExecuteEventScript>().EventWave = 0;
                int random = UnityEngine.Random.Range(0, 3);
                _garbageWaveScript._spawnGarbage(1, pOther.transform.position.x + 5, 4, pOther.transform.position.z - 3, _garbageWaveScript.LightGarbage[random], -pOther.GetComponent<ExecuteEventScript>().EventSpeed);
                random = UnityEngine.Random.Range(0, 3);
                _garbageWaveScript._spawnGarbage(1, pOther.transform.position.x - 5, 4, pOther.transform.position.z -3, _garbageWaveScript.LightGarbage[random], -pOther.GetComponent<ExecuteEventScript>().EventSpeed);
                random = UnityEngine.Random.Range(0, 3);
                _garbageWaveScript._spawnGarbage(1, pOther.transform.position.x + 0.1f, 4, pOther.transform.position.z + 10, _garbageWaveScript.LightGarbage[random], -pOther.GetComponent<ExecuteEventScript>().EventSpeed);
                _garbageWaveScript.DestroyedGarbage.Add(this.gameObject);
                Destroy(this.gameObject); 
            }
        }
        if (pOther.GetComponent<BoatEventScript>() && _event == _choices.BoatToTile)
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
            //_eventAmountOfObjects
            Destroy(pOther);

            //for (int i = 0; i < _eventAmountOfObjects; i++)
            //{
            //    _event = _choices.SpawnRandomLight;
            //    _eventAmountOfObjects = 1;
            //    _eventSpeedOfObjects = 1;
            //    this.EventWrapper.Add(_currentEvent);
            //    if (i + 1 == _eventAmountOfObjects)
            //    {
            //        Destroy(pOther);
            //    }
            //}

        }

        //    //second Part deleting Part of Wrapper
        //    if (GetComponent<ExecuteEventScript>() != null)
        //    {
        //        //if (!_eventEveryWave && _eventEveryXWave == 0 && _eventAmountOfObjects == 0 || _garbageWaveScript.DestroyedGarbage.Count+1 == _eventAmountOfObjects)
        //        //{
        //            if (GetComponent<ExecuteEventScript>().EventWrapper.Count != 0)
        //            {
        //                if (this.gameObject.name == this.GetComponent<ExecuteEventScript>().EventWrapper[0].Tile && _isEventDone)
        //                {
        //                    if (_garbageWaveScript.Wave == this._eventWave && this.GetComponent<ExecuteEventScript>().EventWrapper.Count == 1)
        //                    {
        //                        GetComponent<ExecuteEventScript>().EventWrapper.RemoveAt(0);
        //                        _startOrRestartTile();
        //                        // Debug.Log(this.GetComponent<EventTileScript>().EventWrapper.Count);
        //                    }
        //                    else if (_garbageWaveScript.Wave == this._eventWave && this.GetComponent<ExecuteEventScript>().EventWrapper.Count > 1)
        //                    {
        //                        GetComponent<ExecuteEventScript>().EventWrapper.RemoveAt(0);
        //                        //Debug.Log(this.GetComponent<EventTileScript>().EventWrapper.Count);
        //                        _startOrRestartTile();
        //                    }
        //                }
        //            }
        //        //}
        //    }
    }

    void OnTriggerExit(Collider pOther)
    {
        //if (pOther.GetComponent<ExecuteEventScript>().EventWrapper.Count != 0)
        //{
        //    if (this.tag == "Garbage" && _garbageWaveScript.Wave == pOther.GetComponent<ExecuteEventScript>().EventWrapper[0].EventWave && pOther.GetComponent<ExecuteEventScript>().IsEventDone)
        //    {
        //        //_isEventDone = false;
        //        pOther.GetComponent<ExecuteEventScript>().IsEventDone = false;
        //        pOther.GetComponent<ExecuteEventScript>().StartOrRestartTile(true);
        //    }
        //}

        
        #region oude code
        //if (!_eventEveryWave && _eventEveryXWave == 0 && _eventAmountOfObjects == 0 || _garbageWaveScript.DestroyedGarbage.Count+1 == _eventAmountOfObjects)
        //    //{
        //    if (_isEventDone)
        //    {
        //        if (_garbageWaveScript.Wave == this._eventWave && this.GetComponent<ExecuteEventScript>().EventWrapper.Count == 1)
        //        {
        //            GetComponent<ExecuteEventScript>().EventWrapper.RemoveAt(0);
        //            _isEventDone = false;
        //            _startOrRestartTile();
        //            // Debug.Log(this.GetComponent<EventTileScript>().EventWrapper.Count);
        //        }
        //        else if (_garbageWaveScript.Wave == this._eventWave && this.GetComponent<ExecuteEventScript>().EventWrapper.Count > 1)
        //        {
        //            GetComponent<ExecuteEventScript>().EventWrapper.RemoveAt(0);
        //            _isEventDone = false;
        //            //Debug.Log(this.GetComponent<EventTileScript>().EventWrapper.Count);
        //            _startOrRestartTile();
        //        }
        //    }
        //    //} 
        #endregion
    }

    #region oude code
    //public void NextWave()
    //{
    //    if (!_eventEveryWave && _eventEveryXWave == 0 && _eventAmountOfObjects == 0 || _garbageWaveScript.DestroyedGarbage.Count+1 == _eventAmountOfObjects)
    //    {
    //        if (_garbageWaveScript.Wave == this._eventWave && this.GetComponent<ExecuteEventScript>().EventWrapper.Count == 1)
    //        {
    //            GetComponent<ExecuteEventScript>().EventWrapper.RemoveAt(0);
    //            _startOrRestartTile();
    //            // Debug.Log(this.GetComponent<EventTileScript>().EventWrapper.Count);
    //        }
    //        else if (_garbageWaveScript.Wave == this._eventWave && this.GetComponent<ExecuteEventScript>().EventWrapper.Count > 1)
    //        {
    //            GetComponent<ExecuteEventScript>().EventWrapper.RemoveAt(0);
    //            //Debug.Log(this.GetComponent<EventTileScript>().EventWrapper.Count);
    //            _startOrRestartTile();
    //        }
    //    }
    //} 
    #endregion

    private void _switchEvent()
    {
        switch (_event)
        {
            case _choices.None:
                break;
            case _choices.IncreaseSpeed:
                break;
            case _choices.SpawnBottle:
                SpawnObjectScript.SpawnBottle(_eventWave, _eventTimeBetween, _eventAmountOfObjects, _eventEveryXWave, _eventEveryWave, _garbageWaveScript, this.transform.position);
                #region Old SpawnBottle
                //if (_garbageWaveScript.Wave == _eventWave)
                //{
                //    Debug.Log(_spawned);
                //    if (Time.time > (_oldTime + _eventTimeBetween) || _isFirstTime)
                //    {
                //        if ( _spawned != _eventAmountOfObjects)
                //        {
                //            _oldTime = Time.time;
                //            if (_eventEveryXWave != 0 && _eventEveryWave)
                //            {
                //                _eventWave = _eventWave + _eventEveryXWave;
                //                GameObject bottle = _garbageWaveScript.LightGarbage[0];
                //                _garbageWaveScript._spawnGarbage(1, this.transform.position.x + 1, 1, this.transform.position.z, bottle);
                //                _spawned++;
                //            }
                //            else if (_eventEveryWave)
                //            {
                //                _eventWave++;
                //                GameObject bottle = _garbageWaveScript.LightGarbage[0];
                //                _garbageWaveScript._spawnGarbage(1, this.transform.position.x + 1, 1, this.transform.position.z, bottle);
                //                _spawned++;
                //                //_event = _choices.SpawnBottle;
                //            }
                //            else
                //            {
                //                //if (_eventEveryWave)
                //                //{
                //                GameObject bottle = _garbageWaveScript.LightGarbage[0];
                //                _garbageWaveScript._spawnGarbage(1, this.transform.position.x + 1, 1, this.transform.position.z, bottle);
                //                _isEventDone = true;
                //               // _event = _choices.None;
                //                _spawned++;
                //                //}
                //            }
                //            _isFirstTime = false;
                //        }
                //    }
                //}
                #endregion
                break;
            case _choices.SpawnRandomLight:
                _spawnedLight = SpawnObjectScript.SpawnRandomLight(_eventWave, _eventTimeBetween, _eventAmountOfObjects, _spawnedLight, _eventEveryXWave, _eventEveryWave, _garbageWaveScript, this.transform.position,this, _eventSpeedOfObjects);
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
            case _choices.SpawnRandomMedium:
                _spawnedMedium = SpawnObjectScript.SpawnRandomMedium(_eventWave, _eventTimeBetween, _eventAmountOfObjects, _spawnedMedium, _eventEveryXWave, _eventEveryWave, _garbageWaveScript, this.transform.position, _eventSpeedOfObjects);
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
            case _choices.SpawnRandomHeavy:
                _spawnedHeavy = SpawnObjectScript.SpawnRandomHeavy(_eventWave, _eventTimeBetween, _eventAmountOfObjects, _spawnedHeavy, _eventEveryXWave, _eventEveryWave, _garbageWaveScript, this.transform.position, _eventSpeedOfObjects);
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
            case _choices.SpawnSuperHeavy:
                SpawnObjectScript.SpawnSuperHeavy(_eventWave, _eventTimeBetween, _eventAmountOfObjects, _eventEveryXWave, _eventEveryWave, _garbageWaveScript, this.transform.position, _eventSpeedOfObjects);
                break;
            case _choices.ShowTutorialBottle:
                break;
            case _choices.SpawnBarrel:
                if (_garbageWaveScript.Wave == _eventWave)
                {
                    GameObject Barrel = _garbageWaveScript.MediumGarbage[1];
                    _garbageWaveScript._spawnGarbage(3, this.transform.position.x + 1, 1, this.transform.position.z, Barrel);
                    _event = _choices.None;
                }
                break;
            case _choices.ExplodesBarrel:
                break;
            case _choices.BoatEvent:
                GameObject boat;
                if (_boatPrefab != null)
                {
                    boat = GameObject.Instantiate(_boatPrefab);
                }
                else
                {
                    boat = GameObject.CreatePrimitive(PrimitiveType.Cube);
                }
                boat.transform.position = new Vector3(-75, this.transform.position.y, this.transform.position.z);
                //boat.AddComponent<BoxCollider>();
                boat.AddComponent<BoatEventScript>();
                boat.AddComponent<Rigidbody>();
                boat.GetComponent<BoatEventScript>().SetTargetPositionAndSpeed(this.transform.position,_eventSpeedOfObjects);
                boat.transform.localScale = new Vector3(10, 10, 10);
                _event = _choices.BoatToTile;
                break;
            default:
                break;
        }
    }

}