<<<<<<< HEAD
﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class WaterCubesScript : MonoBehaviour {

    [SerializeField]
    private GameObject _cubePrefab;

    [SerializeField]
    private int _amountCubesX = 1;
    [SerializeField]
    private int _amountCubesY = 1;
    [SerializeField]
    private int _amountCubesZ = 1;
    [SerializeField]
    private float _rotateAroundX = 0;
    [SerializeField]
    private float _rotateAroundY = 0;
    [SerializeField]
    private float _rotateAroundZ = 0;

    private GameObject[,,] _cubesArray;

    private float _secTimer = 0;


    // Use this for initialization
    void Start () {
        _cubesArray = new GameObject[_amountCubesX, _amountCubesY, _amountCubesZ];

        float parentX = this.transform.position.x;
        float parentY = this.transform.position.y;
        float parentZ = this.transform.position.z;

        for (int X = 0; X < _amountCubesX; X++)
        {
            for (int Y = 0; Y < _amountCubesY; Y++)
            {
                for (int Z = 0; Z < _amountCubesZ; Z++)
                {
                    GameObject Cube = (GameObject)Instantiate(_cubePrefab, new Vector3(X + parentX, Y + parentY, Z + parentZ), Quaternion.identity);
                    
                    Cube.transform.parent = this.gameObject.transform;
                    //Cube.GetComponent<Renderer>().enabled = false;
                    _cubesArray[X, Y, Z] = Cube;
                }
            }
        }
        this.gameObject.transform.Rotate(new Vector3(_rotateAroundX, _rotateAroundY, _rotateAroundZ));
        
        for (int X = 0; X < _amountCubesX; X++)
        {
            for (int Y = 0; Y < _amountCubesY; Y++)
            {
                for (int Z = 0; Z < _amountCubesZ; Z++)
                {
                    //add cube directly under the current cube
                    if (Y-1 > -1)
                    {
                        _cubesArray[X, Y, Z].GetComponent<CubeScript>().ConnectedCubes.Add(_cubesArray[X, Y - 1, Z]);
                    }
                    //add cube directly next to the current cube in the X direction
                    if (X + 1 < _amountCubesX)
                    {
                        _cubesArray[X, Y, Z].GetComponent<CubeScript>().ConnectedCubes.Add(_cubesArray[X + 1, Y, Z]);
                    }

                    //GameObject Cube = (GameObject)Instantiate(_cubePrefab, new Vector3(X + parentX, Y + parentY, Z + parentZ), Quaternion.identity);
                    //Cube.transform.parent = this.gameObject.transform;
                    //Cube.GetComponent<Renderer>().enabled = false;
                    //_cubesArray[X, Y, Z] = Cube;
                }
            }
        }

        
    }
	
	// Update is called once per frame
	void Update () {
        //_secTimer += Time.deltaTime;
        //if (_secTimer > 1)
        //{
        //    _secTimer = 0;
            //for (int X = _amountCubesX-1; X > -1; X--)
            //{
            //    for (int Y = _amountCubesY-1; Y > -1; Y--)
            //    {
            //        for (int Z = _amountCubesZ-1; Z > -1; Z--)
            //        {
            //            //check water level of current cube, if there is no water continue to next cube
            //            float water = _cubesArray[X, Y, Z].GetComponent<CubeScript>().AmountOfWater;
            //            if (water == 0) continue;

            //            //if there is a cube below current cube put all water in cube below
            //            if (Y - 1 > -1)
            //            {
            //                _cubesArray[X, Y, Z].GetComponent<CubeScript>().AmountOfWater = 0;
            //                _cubesArray[X, Y - 1, Z].GetComponent<CubeScript>().AmountOfWater = water;
            //                continue;
            //            }

            //            //if (X + 1 < _amountCubesX)
            //            //{
            //            //    _cubesArray[X, Y, Z].GetComponent<CubeScript>().AmountOfWater = 0;
            //            //    _cubesArray[X + 1, Y, Z].GetComponent<CubeScript>().AmountOfWater = water;
            //            //}
            //        }
            //    }
            //}

            //last FOR to happen, makes infinite water
            /**/
            for (int Z = 0; Z < _amountCubesZ; Z++)
            {
                _cubesArray[0, _amountCubesY - 1, Z].GetComponent<CubeScript>().AmountOfWater = 5;
            }
            /**/
        //}

    }
}
=======
﻿using UnityEngine;
using System.Collections;

public class WaterCubesScript : MonoBehaviour {

    [SerializeField]
    private GameObject _cubePrefab;

    [SerializeField]
    private int _amountCubesX = 1;
    [SerializeField]
    private int _amountCubesY = 1;
    [SerializeField]
    private int _amountCubesZ = 1;

    private GameObject[,,] _cubesArray;

    private float _secTimer = 0;


    // Use this for initialization
    void Start () {
        _cubesArray = new GameObject[_amountCubesX, _amountCubesY, _amountCubesZ];

        float parentX = this.transform.position.x;
        float parentY = this.transform.position.y;
        float parentZ = this.transform.position.z;

        for (int X = 0; X < _amountCubesX; X++)
        {
            for (int Y = 0; Y < _amountCubesY; Y++)
            {
                for (int Z = 0; Z < _amountCubesZ; Z++)
                {
                    GameObject Cube = (GameObject)Instantiate(_cubePrefab, new Vector3(X + parentX, Y + parentY, Z + parentZ), Quaternion.identity);
                    Cube.transform.parent = this.gameObject.transform;
                    //Cube.GetComponent<Renderer>().enabled = false;
                    _cubesArray[X, Y, Z] = Cube;
                }
            }
        }

        for (int Z = 0; Z < _amountCubesZ; Z++)
        {
            _cubesArray[0, 2, Z].GetComponent<CubeScript>().AmountOfWater = 5;
        }
    }
	
	// Update is called once per frame
	void Update () {
        _secTimer += Time.deltaTime;
        if (_secTimer > 1)
        {
            _secTimer = 0;
            for (int X = _amountCubesX-1; X > 0; X--)
            {
                for (int Y = _amountCubesY-1; Y > 0; Y--)
                {
                    for (int Z = _amountCubesZ-1; Z > 0; Z--)
                    {
                        //check water level of current cube, if there is no water continue to next cube
                        float water = _cubesArray[X, Y, Z].GetComponent<CubeScript>().AmountOfWater;
                        if (water == 0) continue;

                        //if there is a cube below current cube put all water in cube below
                        if (Y - 1 > -1)
                        {
                            _cubesArray[X, Y, Z].GetComponent<CubeScript>().AmountOfWater = 0;
                            _cubesArray[X, Y - 1, Z].GetComponent<CubeScript>().AmountOfWater = water;
                            //continue;
                        }

                        if (X + 1 < _amountCubesX)
                        {
                            _cubesArray[X, Y, Z].GetComponent<CubeScript>().AmountOfWater = 0;
                            _cubesArray[X + 1, Y, Z].GetComponent<CubeScript>().AmountOfWater = water;
                        }
                    }
                }
            }

            //last FOR to happen, makes infinite water
            /**/
            for (int Z = 0; Z < _amountCubesZ; Z++)
            {
                _cubesArray[0, 2, Z].GetComponent<CubeScript>().AmountOfWater = 5;
            }
            /**/
        }

    }
}
>>>>>>> origin/master
