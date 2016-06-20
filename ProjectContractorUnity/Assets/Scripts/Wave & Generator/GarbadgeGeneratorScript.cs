﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class GarbadgeGeneratorScript : MonoBehaviour {
    
    private GameObject _damageParticle;
    private GeneratorPowerScript _generatorPowerScript;
    private GarbageWaveScript _garbageWaveScript;
    private NumberParticleScript _numberParticle;
    private float _oldTimer;
    private float _timer = 0.5f;
    private bool _hitGenerator = false;

    [SerializeField]
    private float _hitTimer = 1;

    [SerializeField]
    private float _repairTime = 10;
    [SerializeField]
    private float _repairForEachTime = 1;

    private float _oldRepairForEachTimer = 0;

    [SerializeField]
    private float _basichit = 10;

    public float BasicHit { get { return _basichit; } }

    [SerializeField]
    private float _mediumhit = 20;

    public float Mediumhit { get { return _mediumhit; } }

    [SerializeField]
    private float _heavyhit = 30;

    public float HeavyHit { get { return _heavyhit; } }

    [SerializeField]
    private float _superHeavyhit = 30;

    public float SuperHeavyHit { get { return _superHeavyhit; } }

    private bool _isStartRepairTimer = false;
    private float _startRepairTimer;

    private float _generatorHealth = 100;

    public float GeneratorHealth { get { return _generatorHealth; } set { _generatorHealth = value; } }

    private bool _isDestroyed = false;

    private GameObject _segment_front;
    private GameObject _segment_back;
    private GameObject _segment_segment5;
    private GameObject _segment_segment4;
    private GameObject _segment_segment3;
    private GameObject _pickedSegment;

    private bool _generatorGotHit;
    public bool GeneratorGotHit { get { return _generatorGotHit; } set { _generatorGotHit = value; } }

    private string _lane;

    private List<string> _deadLaneList;
    public List<string> DeadLaneList { get { return _deadLaneList; } }

    // Use this for initialization
    void Start ()
    {
        _deadLaneList = new List<string>();
        _deadLaneList.Add("A");
        _numberParticle = GameObject.FindObjectOfType<NumberParticleScript>();
        _damageParticle = (GameObject)Resources.Load("Damage");
        _generatorGotHit = false;
        _generatorPowerScript = transform.parent.GetComponent<GeneratorPowerScript>();
        _garbageWaveScript = GameObject.FindObjectOfType<GarbageWaveScript>();
        _oldTimer = 0;
        if (transform.name == "GeneratorWall5")
        {
            _segment_front = GameObject.Find("segment_front1");
            _pickedSegment = _segment_front;
            _lane = "A";
        }
        if (transform.name == "GeneratorWall2")
        {
            _segment_segment5 = GameObject.Find("segment_segment5");
            _pickedSegment = _segment_segment5;
            _lane = "D";
        }
        if (transform.name == "GeneratorWall3")
        {
            _segment_segment4 = GameObject.Find("segment_segment4");
            _pickedSegment = _segment_segment4;
            _lane = "C";
        }
        if (transform.name == "GeneratorWall4")
        {
            _segment_segment3 = GameObject.Find("segment_segment3");
            _pickedSegment = _segment_segment3;
            _lane = "B";
        }
        if (transform.name == "GeneratorWall1")
        {
            _segment_back = GameObject.Find("segment_back1");
            _pickedSegment = _segment_back;
            _lane = "E";
        }
    }
	
	// Update is called once per frame
	void Update () {
        //Debug.Log("Generator Health: " + _generatorHealth);
        if (_generatorHealth <= 0 && _isDestroyed == false)
        {
            _generatorPowerScript.DestroyedGenerator++;
            if (!_deadLaneList.Contains(_lane))
            {
                _deadLaneList.Add(_lane);
            }
            _isDestroyed = true;
            //_pickedSegment.GetComponent<Renderer>().material.color = Color.gray;
        }
        else if (_generatorHealth < 20 && _generatorHealth > 0)
        {
            //_pickedSegment.GetComponent<Renderer>().material.color = Color.red;
        }
        else if (_generatorHealth < 50 && _generatorHealth > 19)
        {
            //_pickedSegment.GetComponent<Renderer>().material.color = Color.yellow;
        }
        else if (_generatorHealth < 101 && _generatorHealth > 49)
        {
            //_pickedSegment.GetComponent<Renderer>().material.color = Color.green;
        }
        if (_generatorHealth >= 100)
        {
            _generatorHealth = 100;
            _isStartRepairTimer = false;
        }

        //if (_isStartRepairTimer && !_isDestroyed)
        //{
        //    if (Time.time > (_startRepairTimer + _repairTime))
        //    {
        //        if (Time.time > (_oldRepairForEachTimer + _repairForEachTime))
        //        {
        //            Debug.Log("REPAIRS WORK");
        //            _oldRepairForEachTimer = Time.time;
        //            this._generatorHealth++;
        //        }
        //    }
        //}


	}

    void OnCollisionEnter(Collision pOther)
    {
        if (pOther.gameObject.tag == "Garbage")
        {
            _generatorGotHit = true;
            if (_garbageWaveScript.LightGarbage.Where(c => c.gameObject.name == pOther.gameObject.name).FirstOrDefault())
            {
                _generatorHealth = _generatorHealth - _basichit;
                _garbageWaveScript.DestroyedGarbage.Add(pOther.gameObject);
                if (pOther.gameObject.GetComponent<GarbadgeDestoryScript>().CurrentTile != null)
                {
                    pOther.gameObject.GetComponent<GarbadgeDestoryScript>().CurrentTile.GarbageList.Remove(pOther.gameObject);
                }
                _numberParticle.PlaceParticleAtGenerator(pOther.transform.position, GarbageType.Light);
                Instantiate(_damageParticle, pOther.transform.position, Quaternion.identity);
                Destroy(pOther.gameObject);
            }
            if (_garbageWaveScript.MediumGarbage.Where(c => c.gameObject.name == pOther.gameObject.name).FirstOrDefault())
            {
                _generatorHealth = _generatorHealth - _mediumhit;
                _garbageWaveScript.DestroyedGarbage.Add(pOther.gameObject);
                if (pOther.gameObject.GetComponent<GarbadgeDestoryScript>().CurrentTile != null)
                {
                    pOther.gameObject.GetComponent<GarbadgeDestoryScript>().CurrentTile.GarbageList.Remove(pOther.gameObject);
                }
                _numberParticle.PlaceParticleAtGenerator(pOther.transform.position, GarbageType.Medium);
                Instantiate(_damageParticle, pOther.transform.position, Quaternion.identity);
                Destroy(pOther.gameObject);
            }
            if (_garbageWaveScript.HeavyGarbage.Where(c => c.gameObject.name == pOther.gameObject.name).FirstOrDefault())
            {
                _generatorHealth = _generatorHealth - _heavyhit;
                _garbageWaveScript.DestroyedGarbage.Add(pOther.gameObject);
                if (pOther.gameObject.GetComponent<GarbadgeDestoryScript>().CurrentTile != null)
                {
                    pOther.gameObject.GetComponent<GarbadgeDestoryScript>().CurrentTile.GarbageList.Remove(pOther.gameObject);
                }
                _numberParticle.PlaceParticleAtGenerator(pOther.transform.position, GarbageType.Heavy);
                Instantiate(_damageParticle, pOther.transform.position, Quaternion.identity);
                Destroy(pOther.gameObject);
            }
            if (_garbageWaveScript.SpecialGarbage.Where(c => c.gameObject.name == pOther.gameObject.name).FirstOrDefault())
            {
                _generatorHealth = _generatorHealth - _superHeavyhit;
                _garbageWaveScript.DestroyedGarbage.Add(pOther.gameObject);
                if (pOther.gameObject.GetComponent<GarbadgeDestoryScript>().CurrentTile != null)
                {
                    pOther.gameObject.GetComponent<GarbadgeDestoryScript>().CurrentTile.GarbageList.Remove(pOther.gameObject);
                }
                _numberParticle.PlaceParticleAtGenerator(pOther.transform.position, GarbageType.Special);
                Instantiate(_damageParticle, pOther.transform.position, Quaternion.identity);
                Destroy(pOther.gameObject);
            }
        }
    }

    //void OnCollisionStay(Collision pOther)
    //{
    //    //if (_hitGenerator == false)
    //    //{
    //    //    _generatorPowerScript.Amount = _generatorPowerScript.Amount + 1;
    //    //    _hitGenerator = true;
    //    //}
    //    if (pOther.gameObject.tag == "Garbage")
    //    {
    //        _generatorGotHit = true;
    //    }
    //    if (Time.time > (_oldTimer + _hitTimer))
    //    {
    //        _oldTimer = Time.time;
    //        if (_garbageWaveScript.LightGarbage.Where(c=>c.gameObject.name == pOther.gameObject.name).FirstOrDefault())
    //        {
    //            _generatorHealth = _generatorHealth - _basichit;
    //        }
    //        if (_garbageWaveScript.MediumGarbage.Where(c => c.gameObject.name == pOther.gameObject.name).FirstOrDefault())
    //        {
    //            _generatorHealth = _generatorHealth - _mediumhit;
    //        }
    //        if (_garbageWaveScript.HeavyGarbage.Where(c => c.gameObject.name == pOther.gameObject.name).FirstOrDefault())
    //        {

    //            _generatorHealth = _generatorHealth - _heavyhit;
    //        }
    //        _isStartRepairTimer = false;
    //    }
    //}
    //void OnCollisionExit(Collision pOther)
    //{
    //    _generatorPowerScript.Amount = _generatorPowerScript.Amount - 1;
    //    _hitGenerator = false;
    //    _isStartRepairTimer = true;
    //    _startRepairTimer = Time.time;
    //}
}
