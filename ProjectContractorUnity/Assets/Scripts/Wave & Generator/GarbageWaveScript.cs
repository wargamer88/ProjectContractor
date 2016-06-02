﻿﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
public enum GarbageType
{
    none,
    Light,
    Medium,
    Heavy,
    Special,
}

public class GarbageWaveScript : MonoBehaviour
{

    private List<int> _spawnXPoint = new List<int>() { -20, -10, 0, 10, 20 };
    private bool _canSpawn = true;
    private GameObject _aimPlane;

    [SerializeField]
    private float _timeToRespawnObjects = 1;
    [SerializeField]
    private float _timeToRespawnIncreaseForEachWave = 1;
    [SerializeField]
    private float _increaseAmountOfObjectForEachWave = 1;

    [SerializeField]
    private List<GameObject> _lightGarbage;
    [SerializeField]
    private List<GameObject> _mediumGarbage;
    [SerializeField]
    private List<GameObject> _heavyGarbage;
    [SerializeField]
    private List<GameObject> _specialGarbage;

    public List<GameObject> LightGarbage { get { return _lightGarbage; } }
    public List<GameObject> MediumGarbage { get { return _mediumGarbage; } }
    public List<GameObject> HeavyGarbage { get { return _heavyGarbage; } }
    public List<GameObject> SpecialGarbage { get { return _specialGarbage; } }

    [SerializeField]
    private int _LightRange = 5;
    [SerializeField]
    private int _mediumRange = 8;
    [SerializeField]
    private int _heavyRange = 10;

    [SerializeField]
    private float _spawnAmount = 10;

    private bool _nextWave = false;

    private List<GameObject> _spawnedGarbage;

    private List<GameObject> _destroyedGarbage;
    public List<GameObject> DestroyedGarbage { get { return _destroyedGarbage; } set { _destroyedGarbage = value; } }


    private GameObject _chosenGarbage;
    private GarbageType _garbageType;
    private GameObject _garbageParent;

    private int _waveNumber = 1;

    [SerializeField]
    private float _spawnGarbageIndependantFromWavesTimer = 10;

    private float _oldTimer;

    [SerializeField]
    private List<TutorialWaveScript> _tutorialScriptWrapper = new List<TutorialWaveScript>();

    private GarbadgeGeneratorScript[] _generators;
    private bool _someGeneratorGotHit = false;
    private HighscoreScript _highScoreScript;

    public int TutorialWavesLeft { get { return _tutorialScriptWrapper.Count; } }
    bool test = false;

    // Use this for initialization
    void Start()
    {
        _highScoreScript = GameObject.FindObjectOfType<HighscoreScript>();
        _spawnedGarbage = new List<GameObject>();
        _destroyedGarbage = new List<GameObject>();
        _aimPlane = GameObject.Find("AimPlane");
        _garbageParent = new GameObject();
        _garbageParent.name = "Garbage Parent";
        _generators = GameObject.FindObjectsOfType<GarbadgeGeneratorScript>();
    }

    // Update is called once per frame
    void Update()
    {
        if (_tutorialScriptWrapper.Count > 0)
        {
            _tutorial();
        }
        else
        {
            float health = _chooseGarbage();

            if (_canSpawn && _spawnedGarbage.Count < _spawnAmount)
            {
                _spawnGarbage(health);
            }
            else if (_spawnedGarbage.Count == _destroyedGarbage.Count && _destroyedGarbage.Count == _spawnAmount)
            {
                _waveNumber++;
                _nextWave = true;
                CheckWave();
            }
            if (_nextWave)
            {
                _timeToRespawnObjects = _timeToRespawnObjects - _timeToRespawnIncreaseForEachWave;
                _spawnAmount = _spawnAmount + _increaseAmountOfObjectForEachWave;
                // complex algorithm beneath to upgrade wave


                this.GetComponent<WaveCanvasScript>().ChangeWaveNumber(_waveNumber);

                _spawnedGarbage = new List<GameObject>();
                _destroyedGarbage = new List<GameObject>();
                _nextWave = false;

            }
            _spawnGarbageAnyTime(health);

            #region old garbage spawning
            //if (_canSpawn)
            //{
            //    int random = Random.Range(0, 5);
            //    List<int> oldRandoms = new List<int>();
            //    GameObject cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
            //    cube.transform.position = new Vector3(_spawnXPoint[random], 0, 95);
            //    cube.AddComponent<GarbageMoveScript>();
            //    //cube.GetComponent<BoxCollider>().isTrigger = true;
            //    cube.AddComponent<Rigidbody>();
            //    cube.GetComponent<Rigidbody>().useGravity = false;
            //    cube.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezePositionX /*| RigidbodyConstraints.FreezePositionY*/ | RigidbodyConstraints.FreezeRotation;
            //    cube.GetComponent<Renderer>().material.color = Color.green;
            //    cube.AddComponent<GarbadgeDestoryScript>();
            //    oldRandoms.Add(random);
            //    random = Random.Range(0, 4);
            //    if (!oldRandoms.Contains(random))
            //    {
            //        //GameObject.Instantiate(PrimitiveType.Cube, new Vector3(), Quaternion.identity);
            //        GameObject cube1 = GameObject.CreatePrimitive(PrimitiveType.Cube);
            //        cube1.transform.position = new Vector3(_spawnXPoint[random], 0, 95);
            //        cube1.AddComponent<GarbageMoveScript>();
            //        //cube.GetComponent<BoxCollider>().isTrigger = true;
            //        cube1.AddComponent<Rigidbody>();
            //        cube1.GetComponent<Rigidbody>().useGravity = false;
            //        cube1.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezePositionX /*| RigidbodyConstraints.FreezePositionY*/ | RigidbodyConstraints.FreezeRotation;
            //        cube1.GetComponent<Renderer>().material.color = Color.blue;
            //        cube1.AddComponent<GarbadgeDestoryScript>();
            //        oldRandoms.Add(random);
            //    }
            //    random = Random.Range(0, 4);
            //    if (!oldRandoms.Contains(random))
            //    {
            //        //GameObject.Instantiate(PrimitiveType.Cube, new Vector3(), Quaternion.identity);
            //        GameObject cube2 = GameObject.CreatePrimitive(PrimitiveType.Cube);
            //        cube2.transform.position = new Vector3(_spawnXPoint[random], 0, 95);
            //        cube2.AddComponent<GarbageMoveScript>();
            //        //cube.GetComponent<BoxCollider>().isTrigger = true;
            //        cube2.AddComponent<Rigidbody>();
            //        cube2.GetComponent<Rigidbody>().useGravity = false;
            //        cube2.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezePositionX /*| RigidbodyConstraints.FreezePositionY*/ | RigidbodyConstraints.FreezeRotation;
            //        cube2.GetComponent<Renderer>().material.color = Color.black;
            //        cube2.AddComponent<GarbadgeDestoryScript>();
            //        oldRandoms.Add(random);
            //    }
            //    random = Random.Range(0, 4);
            //    if (!oldRandoms.Contains(random))
            //    {
            //        //GameObject.Instantiate(PrimitiveType.Cube, new Vector3(), Quaternion.identity);
            //        GameObject cube3 = GameObject.CreatePrimitive(PrimitiveType.Cube);
            //        cube3.transform.position = new Vector3(_spawnXPoint[random], 0, 95);
            //        cube3.AddComponent<GarbageMoveScript>();
            //        //cube.GetComponent<BoxCollider>().isTrigger = true;
            //        cube3.AddComponent<Rigidbody>();
            //        cube3.GetComponent<Rigidbody>().useGravity = false;
            //        cube3.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezePositionX /*| RigidbodyConstraints.FreezePositionY*/ | RigidbodyConstraints.FreezeRotation;
            //        cube3.GetComponent<Renderer>().material.color = Color.yellow;
            //        cube3.AddComponent<GarbadgeDestoryScript>();
            //        oldRandoms.Add(random);
            //    }
            //    _canSpawn = false;
            //    StartCoroutine(_waitForSeconds());
            //}
            #endregion
        }
    }

    private void CheckWave()
    {
        for (int i = 0; i < _generators.Length; i++)
        {
            if (_generators[i].GeneratorGotHit)
            {
                Debug.Log("Generator Got HIT!");
                _someGeneratorGotHit = true;
                _generators[i].GeneratorGotHit = false;
            }
        }
        _highScoreScript.WaveClear(_someGeneratorGotHit);
        _someGeneratorGotHit = false;
    }

    private IEnumerator _waitForSeconds()
    {
        yield return new WaitForSeconds(_timeToRespawnObjects);
        _canSpawn = true;
    }

    private float _chooseGarbage()
    {
        float health = 0;
        int randomNumber = Random.Range(0, _heavyRange);
        if (randomNumber < _LightRange)
        {
            _chosenGarbage = _lightGarbage[Random.Range(0, _lightGarbage.Count)];
            _garbageType = GarbageType.Light;
            health = 1;
        }
        else if (randomNumber < _mediumRange)
        {
            _chosenGarbage = _mediumGarbage[Random.Range(0, _mediumGarbage.Count)];
            _garbageType = GarbageType.Medium;
            health = 2;
        }
        else
        {
            _chosenGarbage = _heavyGarbage[Random.Range(0, _heavyGarbage.Count)];
            _garbageType = GarbageType.Heavy;
            health = 3;
        }
        return health;
    }

    private void _spawnGarbage(float pHealth, float pX = 0 , float pY = 1, float pZ = 95)
    {
        GameObject gameSpawnObject = GameObject.Instantiate(_chosenGarbage, new Vector3(), Quaternion.identity) as GameObject;
        //List<GameObject> garbages = GameObject.FindGameObjectsWithTag("Garbage").ToList();
        gameSpawnObject.transform.parent = _garbageParent.transform;
        int randomSpawn = 0;
        bool addSpawnGarbage;
        if (pZ != 95)
        {
            randomSpawn = (int)pX;
            gameSpawnObject.transform.position = new Vector3(_spawnXPoint[randomSpawn], pY, pZ);
            addSpawnGarbage = false;
        }
        else
        {
            randomSpawn = Random.Range(0, 5);
            gameSpawnObject.transform.position = new Vector3(_spawnXPoint[randomSpawn], pY, pZ);
            addSpawnGarbage = true;
        }
        //foreach (GameObject garbage in garbages)
        //{
        //    Physics.IgnoreCollision(gameSpawnObject.GetComponent<BoxCollider>(), garbage.GetComponent<BoxCollider>());
        //}
        Physics.IgnoreCollision(gameSpawnObject.GetComponent<BoxCollider>(), _aimPlane.GetComponent<MeshCollider>());
        gameSpawnObject.tag = "Garbage";
        gameSpawnObject.AddComponent<GarbageMoveScript>();
        gameSpawnObject.AddComponent<Rigidbody>();
        gameSpawnObject.GetComponent<Rigidbody>().useGravity = false;
        gameSpawnObject.GetComponent<Rigidbody>().mass = 1000;
        //float randomRotationX = Random.Range(0, 360);
        //float randomRotationY = Random.Range(0, 360);
        //float randomRotationZ = Random.Range(0, 360);
        //gameSpawnObject.transform.rotation = new Quaternion(0,0,0,0);
        gameSpawnObject.GetComponent<Rigidbody>().constraints = /*.FreezePositionX | RigidbodyConstraints.FreezePositionY | */RigidbodyConstraints.FreezeRotation;
        gameSpawnObject.AddComponent<GarbadgeDestoryScript>();
        gameSpawnObject.GetComponent<GarbadgeDestoryScript>().HP = pHealth;
        gameSpawnObject.gameObject.name = gameSpawnObject.gameObject.name.Replace("(Clone)", "");
        gameSpawnObject.GetComponent<GarbadgeDestoryScript>().GarbageType = _garbageType;
        gameSpawnObject.GetComponent<GarbadgeDestoryScript>().CurrentLane = randomSpawn;
        if (addSpawnGarbage)
        {
            _spawnedGarbage.Add(gameSpawnObject);
        }
        //_spawnedGarbage.Add(gameSpawnObject);
        gameSpawnObject.GetComponent<GarbadgeDestoryScript>().GarbageType = _garbageType;
        _canSpawn = false;
        StartCoroutine(_waitForSeconds());
    }

    private void _spawnGarbageAnyTime(float pHealth)
    {
        if (Time.time > (_oldTimer + _spawnGarbageIndependantFromWavesTimer))
        {
            _oldTimer = Time.time;
            float X = Random.Range(0, 5);
            _spawnGarbage(pHealth,X,1,40);
        }
    }

    private void _tutorial()
    {
        if (this.GetComponent<TutorialWaveSpawnScript>().IsComplete)
        {
            this.GetComponent<TutorialWaveSpawnScript>().IsComplete = false;
            _tutorialScriptWrapper.RemoveAt(0);
            test = true;
        }
        else
        {
            this.GetComponent<TutorialWaveSpawnScript>().SpawnTutorial(false, _tutorialScriptWrapper[0].AmountOf, (GarbageType)_tutorialScriptWrapper[0].Garbage, _lightGarbage, _mediumGarbage, _heavyGarbage, _specialGarbage, _timeToRespawnObjects);
        }
    }
}