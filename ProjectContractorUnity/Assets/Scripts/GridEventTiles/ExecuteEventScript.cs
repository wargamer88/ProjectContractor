using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using System.Linq;
using UnityEngine.UI;

public class ExecuteEventScript : MonoBehaviour {
    
    private _choices _event;
    private int _eventWave;
    private int _eventEveryXWave;
    private bool _eventEveryWave;
    private int _eventAmountOfObjects;
    private float _eventSpeedOfObjects;
    private float _eventTimeBetween;


    private GarbageWaveScript _garbageWaveScript;
    private GameObject _aimPlane;
    private Image _hand;

    private bool _isDestroyed = false;
    public bool IsDestroyed { set { _isDestroyed = value; } }

    private bool _isEventDone = false;

    private float _oldTime;

    private bool _isFirstTime = true;


    private int _spawned = 0;


    // Use this for initialization
    void Start () {
        _hand = GameObject.Find("Hand").GetComponent<Image>();
        _startOrRestartTile();
        //_eventList = GetComponent<EventTileScript>().Choices;
        _garbageWaveScript = GameObject.FindObjectOfType<GarbageWaveScript>();

        _aimPlane = GameObject.Find("AimPlane");
	}

    private void _startOrRestartTile()
    {
        if (this.tag != "Garbage" && GetComponent<EventTileScript>().EventWrapper.Count != 0)
        {
            //_event = GetComponent<EventTileScript>().ChosenEvent;
            //_eventWave = GetComponent<EventTileScript>().EventWave;
            _event = GetComponent<EventTileScript>().EventWrapper[0].ChosenEvent;
            _eventWave = GetComponent<EventTileScript>().EventWrapper[0].EventWave;
            _eventEveryWave = GetComponent<EventTileScript>().EventWrapper[0].IsEveryWave;
            _eventEveryXWave = GetComponent<EventTileScript>().EventWrapper[0].EveryXWave;
            _eventSpeedOfObjects = GetComponent<EventTileScript>().EventWrapper[0].SpeedOfObject;
            _eventAmountOfObjects = GetComponent<EventTileScript>().EventWrapper[0].AmountOfObject;
            _eventTimeBetween = GetComponent<EventTileScript>().EventWrapper[0].TimeBetweenSpawn;

        }
    }


    // Update is called once per frame
    void Update()
    {
        if (_garbageWaveScript.Wave != 1)
        {
            if (_hand.enabled)
            {
                _hand.enabled = false;
            }
        }
        //if (GetComponent<EventTileScript>() != null)
        //{
        //foreach (EventTileWrapperScript tileEvent in GetComponent<EventTileScript>().EventWrapper)
        //{
        if (_event != null)
        {
            switch (_event)
            {
                case _choices.None:
                    break;
                case _choices.IncreaseSpeed:
                    break;
                case _choices.SpawnBottle:
                    if (_garbageWaveScript.Wave == _eventWave)
                    {
                        GameObject bottle = _garbageWaveScript.LightGarbage[0];
                        _garbageWaveScript._spawnGarbage(1, this.transform.position.x + 1, 4, this.transform.position.z, bottle);
        
                        _event = _choices.None;
                    }
                    break;
                case _choices.ShowTutorialBottle:
                    break;
                case _choices.SpawnBarrel:
                    if (_garbageWaveScript.Wave == _eventWave)
                    {
                        GameObject Barrel = _garbageWaveScript.MediumGarbage[1];
                        _garbageWaveScript._spawnGarbage(3, this.transform.position.x + 1, 4, this.transform.position.z, Barrel);
                        _event = _choices.None;
                    }
                    break;
                case _choices.ExplodesBarrel:
                    break;
                default:
                    break;
            }
        }
    }

    void OnTriggerEnter(Collider pOther)
    {
        if (this.tag == "Garbage" && pOther.GetComponent<EventTileScript>().EventWrapper.Count != 0)
        {
            //Debug.Log(_eventWave);
            //Debug.Log("garbagewave " + _garbageWaveScript.Wave);
            if (pOther.GetComponent<EventTileScript>().EventWrapper[0].ChosenEvent == _choices.ShowTutorialBottle && _garbageWaveScript.Wave == pOther.GetComponent<EventTileScript>().EventWrapper[0].EventWave)
            {
                _hand.enabled = true;
                Debug.Log("help");
                _isEventDone = true;
                pOther.GetComponent<EventTileScript>().EventWrapper.RemoveAt(0);
            }
            else if(pOther.GetComponent<EventTileScript>().EventWrapper[0].ChosenEvent == _choices.ExplodesBarrel && _garbageWaveScript.Wave == pOther.GetComponent<EventTileScript>().EventWrapper[0].EventWave)
            {
                Debug.Log("EXPLODE BARREL");
                GameObject bottle = _garbageWaveScript.LightGarbage[0];
                for (int i = 0; i < 3; i++)
                {
                    _garbageWaveScript._spawnGarbage(1, this.transform.position.x + i, 1, this.transform.position.z, bottle);
                }
                Destroy(pOther);
                Destroy(this);
            }
        }

        //second Part deleting Part of Wrapper
        if (GetComponent<EventTileScript>() != null)
        {
            if (!_eventEveryWave && _eventEveryXWave == 0 && _eventAmountOfObjects == 0)
            {
                if (GetComponent<EventTileScript>().EventWrapper.Count != 0)
                {
                    if (this.gameObject.name == this.GetComponent<EventTileScript>().EventWrapper[0].Tile && _isEventDone)
                    {
                        if (_garbageWaveScript.Wave == this._eventWave && this.GetComponent<EventTileScript>().EventWrapper.Count == 1)
                        {
                            GetComponent<EventTileScript>().EventWrapper.RemoveAt(0);
                            _startOrRestartTile();
                            // Debug.Log(this.GetComponent<EventTileScript>().EventWrapper.Count);
                        }
                        else if (_garbageWaveScript.Wave == this._eventWave && this.GetComponent<EventTileScript>().EventWrapper.Count > 1)
                        {
                            GetComponent<EventTileScript>().EventWrapper.RemoveAt(0);
                            //Debug.Log(this.GetComponent<EventTileScript>().EventWrapper.Count);
                            _startOrRestartTile();
                        }
                    }
                }
            }
        }
    }
    void OnTriggerStay(Collider pOther)
	{
        if (!_eventEveryWave && _eventEveryXWave == 0 && _eventAmountOfObjects == 0)
        {
            if (_isEventDone)
            {
                if (this.GetComponent<EventTileScript>() != null)
                {
                    if (this.GetComponent<EventTileScript>().EventWrapper.Count != 0)
                    {
                        if (this.transform.name == this.GetComponent<EventTileScript>().EventWrapper[0].Tile)
                        {
                            if (_garbageWaveScript.Wave == this._eventWave && this.GetComponent<EventTileScript>().EventWrapper.Count == 1)
                            {
                                GetComponent<EventTileScript>().EventWrapper.RemoveAt(0);
                                //_startOrRestartTile();
                                _isEventDone = false;
                                // Debug.Log(this.GetComponent<EventTileScript>().EventWrapper.Count);
                            }
                            else if (_garbageWaveScript.Wave == this._eventWave && this.GetComponent<EventTileScript>().EventWrapper.Count > 1)
                            {
                                GetComponent<EventTileScript>().EventWrapper.RemoveAt(0);
                                //Debug.Log(this.GetComponent<EventTileScript>().EventWrapper.Count);
                                //_startOrRestartTile();
                                _isEventDone = false;
                            }
                        }
                    }
                }
            }
        }
    }

    void OnTriggerExit(Collider pOther)
    {
        if (!_eventEveryWave && _eventEveryXWave == 0 && _eventAmountOfObjects == 0)
        {
            if (_garbageWaveScript.Wave == this._eventWave && this.GetComponent<EventTileScript>().EventWrapper.Count == 1)
            {
                GetComponent<EventTileScript>().EventWrapper.RemoveAt(0);
                _startOrRestartTile();
                // Debug.Log(this.GetComponent<EventTileScript>().EventWrapper.Count);
            }
            else if (_garbageWaveScript.Wave == this._eventWave && this.GetComponent<EventTileScript>().EventWrapper.Count > 1)
            {
                GetComponent<EventTileScript>().EventWrapper.RemoveAt(0);
                //Debug.Log(this.GetComponent<EventTileScript>().EventWrapper.Count);
                _startOrRestartTile();
            }
        }
    }

    public void NextWave()
    {
        if (!_eventEveryWave && _eventEveryXWave == 0 && _eventAmountOfObjects == 0)
        {
            if (_garbageWaveScript.Wave == this._eventWave && this.GetComponent<EventTileScript>().EventWrapper.Count == 1)
            {
                GetComponent<EventTileScript>().EventWrapper.RemoveAt(0);
                _startOrRestartTile();
                // Debug.Log(this.GetComponent<EventTileScript>().EventWrapper.Count);
            }
            else if (_garbageWaveScript.Wave == this._eventWave && this.GetComponent<EventTileScript>().EventWrapper.Count > 1)
            {
                GetComponent<EventTileScript>().EventWrapper.RemoveAt(0);
                //Debug.Log(this.GetComponent<EventTileScript>().EventWrapper.Count);
                _startOrRestartTile();
            }
        }
    }

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
                SpawnObjectScript.SpawnRandomLight(_eventWave, _eventTimeBetween, _eventAmountOfObjects, _eventEveryXWave, _eventEveryWave, _garbageWaveScript, this.transform.position);
                break;
            case _choices.SpawnRandomMedium:
                SpawnObjectScript.SpawnRandomMedium(_eventWave, _eventTimeBetween, _eventAmountOfObjects, _eventEveryXWave, _eventEveryWave, _garbageWaveScript, this.transform.position);
                break;
            case _choices.SpawnRandomHeavy:
                SpawnObjectScript.SpawnRandomHeavy(_eventWave, _eventTimeBetween, _eventAmountOfObjects, _eventEveryXWave, _eventEveryWave, _garbageWaveScript, this.transform.position);
                break;
            case _choices.SpawnSuperHeavy:
                SpawnObjectScript.SpawnSuperHeavy(_eventWave, _eventTimeBetween, _eventAmountOfObjects, _eventEveryXWave, _eventEveryWave, _garbageWaveScript, this.transform.position);
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
            default:
                break;
        }
    }

}
