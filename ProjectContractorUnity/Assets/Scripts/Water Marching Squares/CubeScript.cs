
﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CubeScript : MonoBehaviour {

    private float _amountOfWater = 0;
    private int _roundedAmountOfWater = 0;
    private List<GameObject> _connectedCubes = new List<GameObject>();
    private float _secTimer = 0;

    public float AmountOfWater { get { return _amountOfWater; } set { _amountOfWater = value; } }
    public List<GameObject> ConnectedCubes { get { return _connectedCubes; } set { _connectedCubes = value; } }

    // Use this for initialization
    void Start () {
	    
	}
	
	// Update is called once per frame
	void Update () {
        if (_amountOfWater > 0)
        {
            _secTimer += Time.deltaTime;
            if (_secTimer > 1)
            {
                if (_connectedCubes.Count > 0)
                {
                    foreach (GameObject Cube in _connectedCubes)
                    {
                        //check if the water can go down, if not go to else
                        if (Cube.transform.localPosition.x == this.gameObject.transform.localPosition.x && Cube.transform.localPosition.y == this.gameObject.transform.localPosition.y - 1 && Cube.transform.localPosition.z == this.gameObject.transform.localPosition.z && Cube.GetComponent<CubeScript>().AmountOfWater < 5)
                        {
                            Cube.GetComponent<CubeScript>().AmountOfWater = _amountOfWater;
                            _amountOfWater = 0;
                            break;
                        }
                        else if (Cube.transform.localPosition.x == this.gameObject.transform.localPosition.x + 1 && Cube.transform.localPosition.y == this.gameObject.transform.localPosition.y && Cube.transform.localPosition.z == this.gameObject.transform.localPosition.z && Cube.GetComponent<CubeScript>().AmountOfWater < 5)
                        {
                            Cube.GetComponent<CubeScript>().AmountOfWater = _amountOfWater;
                            _amountOfWater = 0;
                            break;
                        }

                    }
                }
                _secTimer = 0;

            }
        }
        


        _roundedAmountOfWater = Mathf.RoundToInt(_amountOfWater);

        switch (_roundedAmountOfWater)
        {
            case 0:
                this.GetComponent<Renderer>().enabled = false;
                break;
            case 1:
                this.GetComponent<Renderer>().enabled = true;
                break;
            case 2:
                this.GetComponent<Renderer>().enabled = true;
                break;
            case 3:
                this.GetComponent<Renderer>().enabled = true;
                break;
            case 4:
                this.GetComponent<Renderer>().enabled = true;
                break;
            case 5:
                this.GetComponent<Renderer>().enabled = true;
                break;
            default:
                break;
        }
    }
}
