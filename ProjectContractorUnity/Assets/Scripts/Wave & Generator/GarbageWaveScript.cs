﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GarbageWaveScript : MonoBehaviour {

    private List<int> _spawnXPoint = new List<int>() { -20, -10,0, 10,20 };
    private bool _canSpawn = true;

    [SerializeField]
    private float _respawnTime = 1;

    [SerializeField]
    private List<GameObject> _basicGarbage;
    [SerializeField]
    private List<GameObject> _mediumGarbage;
    [SerializeField]
    private List<GameObject> _heavyGarbage;

    [SerializeField]
    private int _basicRange = 5;
    [SerializeField]
    private int _mediumRange = 8;
    [SerializeField]
    private int _heavyRange = 10;


    private GameObject _chosenGarbage;
    // Use this for initialization
    void Start () {
	}

    // Update is called once per frame
    void Update() {

        //int randomNumber = Random.Range(0, _heavyRange);
        //if (randomNumber < 6)
        //{
        //    _chosenGarbage = _basicGarbage[Random.Range(0, _basicGarbage.Count)];
        //}
        //else if (randomNumber < 9)
        //{
        //    _chosenGarbage = _mediumGarbage[Random.Range(0, _mediumGarbage.Count)];
        //}
        //else
        //{
        //    _chosenGarbage = _heavyGarbage[Random.Range(0, _heavyGarbage.Count)];
        //}

        //GameObject gameSpawnObject = GameObject.Instantiate(_chosenGarbage, new Vector3(), Quaternion.identity) as GameObject;
        //if (_canSpawn)
        //{
        //    int randomSpawn = Random.Range(1, 4);
        //    gameSpawnObject.transform.position = new Vector3(_spawnXPoint[randomSpawn], 0, 95);
        //    gameSpawnObject.AddComponent<GarbageMoveScript>();
        //    gameSpawnObject.AddComponent<Rigidbody>();
        //    gameSpawnObject.GetComponent<Rigidbody>().useGravity = false;
        //    gameSpawnObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezePositionX /*| RigidbodyConstraints.FreezePositionY*/ | RigidbodyConstraints.FreezeRotation;
        //    gameSpawnObject.AddComponent<GarbadgeDestoryScript>();
        //    _canSpawn = false;
        //    StartCoroutine(_waitForSeconds());
        //}


        if (_canSpawn)
        {
            int random = Random.Range(0, 5);
            List<int> oldRandoms = new List<int>();
            GameObject cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
            cube.transform.position = new Vector3(_spawnXPoint[random], 0, 95);
            cube.AddComponent<GarbageMoveScript>();
            //cube.GetComponent<BoxCollider>().isTrigger = true;
            cube.AddComponent<Rigidbody>();
            cube.GetComponent<Rigidbody>().useGravity = false;
            cube.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezePositionX /*| RigidbodyConstraints.FreezePositionY*/ | RigidbodyConstraints.FreezeRotation;
            cube.GetComponent<Renderer>().material.color = Color.green;
            cube.AddComponent<GarbadgeDestoryScript>();
            oldRandoms.Add(random);
            random = Random.Range(0, 4);
            if (!oldRandoms.Contains(random))
            {
                //GameObject.Instantiate(PrimitiveType.Cube, new Vector3(), Quaternion.identity);
                GameObject cube1 = GameObject.CreatePrimitive(PrimitiveType.Cube);
                cube1.transform.position = new Vector3(_spawnXPoint[random], 0, 95);
                cube1.AddComponent<GarbageMoveScript>();
                //cube.GetComponent<BoxCollider>().isTrigger = true;
                cube1.AddComponent<Rigidbody>();
                cube1.GetComponent<Rigidbody>().useGravity = false;
                cube1.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezePositionX /*| RigidbodyConstraints.FreezePositionY*/ | RigidbodyConstraints.FreezeRotation;
                cube1.GetComponent<Renderer>().material.color = Color.blue;
                cube1.AddComponent<GarbadgeDestoryScript>();
                oldRandoms.Add(random);
            }
            random = Random.Range(0, 4);
            if (!oldRandoms.Contains(random))
            {
                //GameObject.Instantiate(PrimitiveType.Cube, new Vector3(), Quaternion.identity);
                GameObject cube2 = GameObject.CreatePrimitive(PrimitiveType.Cube);
                cube2.transform.position = new Vector3(_spawnXPoint[random], 0, 95);
                cube2.AddComponent<GarbageMoveScript>();
                //cube.GetComponent<BoxCollider>().isTrigger = true;
                cube2.AddComponent<Rigidbody>();
                cube2.GetComponent<Rigidbody>().useGravity = false;
                cube2.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezePositionX /*| RigidbodyConstraints.FreezePositionY*/ | RigidbodyConstraints.FreezeRotation;
                cube2.GetComponent<Renderer>().material.color = Color.black;
                cube2.AddComponent<GarbadgeDestoryScript>();
                oldRandoms.Add(random);
            }
            random = Random.Range(0, 4);
            if (!oldRandoms.Contains(random))
            {
                //GameObject.Instantiate(PrimitiveType.Cube, new Vector3(), Quaternion.identity);
                GameObject cube3 = GameObject.CreatePrimitive(PrimitiveType.Cube);
                cube3.transform.position = new Vector3(_spawnXPoint[random], 0, 95);
                cube3.AddComponent<GarbageMoveScript>();
                //cube.GetComponent<BoxCollider>().isTrigger = true;
                cube3.AddComponent<Rigidbody>();
                cube3.GetComponent<Rigidbody>().useGravity = false;
                cube3.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezePositionX /*| RigidbodyConstraints.FreezePositionY*/ | RigidbodyConstraints.FreezeRotation;
                cube3.GetComponent<Renderer>().material.color = Color.yellow;
                cube3.AddComponent<GarbadgeDestoryScript>();
                oldRandoms.Add(random);
            }
            _canSpawn = false;
            StartCoroutine(_waitForSeconds());
        }
        }

    private IEnumerator _waitForSeconds()
    {
        yield return new WaitForSeconds(_respawnTime);
        _canSpawn = true;
    }
}