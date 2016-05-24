﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public enum GarbageType
{
    Light,
    Medium,
    Heavy,
}

public class GarbageWaveScript : MonoBehaviour {

    private List<int> _spawnXPoint = new List<int>() { -20, -10,0, 10,20 };
    private bool _canSpawn = true;
    private GameObject _aimPlane;

    [SerializeField]
    private float _respawnTime = 1;
    [SerializeField]
    private float _inscreaseTime = 1;
    [SerializeField]
    private float _waveScale = 1;

    [SerializeField]
    private List<GameObject> _lightGarbage;
    [SerializeField]
    private List<GameObject> _mediumGarbage;
    [SerializeField]
    private List<GameObject> _heavyGarbage;

    public List<GameObject> LightGarbage { get { return _lightGarbage; } }
    public List<GameObject> MediumGarbage { get { return _mediumGarbage; } }
    public List<GameObject> HeavyGarbage { get { return _heavyGarbage; } }

    [SerializeField]
    private int _LightRange = 5;
    [SerializeField]
    private int _mediumRange = 8;
    [SerializeField]
    private int _heavyRange = 10;

    [SerializeField]
    private int _spawnAmount = 10;

    private bool _nextWave = false;

    private List<GameObject> _spawnedGarbage;

    private List<GameObject> _destroyedGarbage;
    public List<GameObject> DestroyedGarbage { get { return _destroyedGarbage; } set { _destroyedGarbage = value; } }


    private GameObject _chosenGarbage;
    private GarbageType _garbageType;
    private GameObject _garbageParent;

    // Use this for initialization
    void Start () {
        _spawnedGarbage = new List<GameObject>();
        _destroyedGarbage = new List<GameObject>();
        _aimPlane = GameObject.Find("AimPlane");
        _garbageParent = new GameObject();
        _garbageParent.name = "Garbage Parent";
	}

    // Update is called once per frame
    void Update() {
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
        if (_canSpawn && _spawnedGarbage.Count < _spawnAmount)
        {
            GameObject gameSpawnObject = GameObject.Instantiate(_chosenGarbage, new Vector3(), Quaternion.identity) as GameObject;
            gameSpawnObject.transform.parent = _garbageParent.transform;
            int randomSpawn = Random.Range(0, 5);
            gameSpawnObject.transform.position = new Vector3(_spawnXPoint[randomSpawn], 1, 95);
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
            gameSpawnObject.GetComponent<GarbadgeDestoryScript>().HP = health;
            gameSpawnObject.gameObject.name = gameSpawnObject.gameObject.name.Replace("(Clone)", "");
            gameSpawnObject.GetComponent<GarbadgeDestoryScript>().GarbageType = _garbageType;
            gameSpawnObject.GetComponent<GarbadgeDestoryScript>().CurrentLane = randomSpawn;
            
            _spawnedGarbage.Add(gameSpawnObject);
            gameSpawnObject.GetComponent<GarbadgeDestoryScript>().GarbageType = _garbageType;
            //gameSpawnObject.AddComponent<MeshCollider>();
            //gameSpawnObject.GetComponent<MeshCollider>().convex = true;
            _canSpawn = false;
            StartCoroutine(_waitForSeconds());
        }
        else if (_spawnedGarbage.Count == _destroyedGarbage.Count && _destroyedGarbage.Count == _spawnAmount)
        {
            Debug.Log("YEEEEEEEEEEEEEEEEEEEEES");
            _nextWave = true;
        }
        Debug.Log(_destroyedGarbage.Count);
        if (_nextWave)
        {
            // _respawnTime = _waveScale + _respawnTime;
            _spawnedGarbage = new List<GameObject>();
            _destroyedGarbage = new List<GameObject>();
            _nextWave = false;
            //_respawnTime = _inscreaseTime + _respawnTime;

        }

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

    private IEnumerator _waitForSeconds()
    {
        yield return new WaitForSeconds(_respawnTime);
        _canSpawn = true;
    }
}