using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class CrankScript : MonoBehaviour {

    private bool _simonShowed;
    private int _simonSaysTimer;
    private int _gatesShowed;

    //halo
    private Behaviour Halo;
    private bool _haloShowed;
    private int _haloTimer = 0;

    //Gates
    private GameObject _redGate;
    private GameObject _blueGate;
    private GameObject _greenGate;
    private GameObject _yellowGate;
    private GameObject _purpleGate;
    private GameObject _correctCube;
    private Camera _camera;

    public List<int> SimonOrder;
    private int _simonIndex = 0;
    private int _startIndex = 0;
    private int _endIndex = 0;
    List<int> _OpenedGatesList;

    private bool _correctOrder = false;
    private int _correctTimer = 0;

    private int _tempIndex = 0;
    private bool _gameFinished = false;

    private int _gateAmount = 0;

    private bool _canClickGate = false;

    private List<int> _tempSimon;

    [SerializeField]
    private int _lifes = 3;

    private bool _retry = false;

    private GameObject _tempGate;
    private bool _startAnimation;
    private bool _closingAnimation;

    // Use this for initialization
    void Start () {
        _redGate = GameObject.Find("RedGate");
        _blueGate = GameObject.Find("BlueGate");
        _greenGate = GameObject.Find("GreenGate");
        _yellowGate = GameObject.Find("YellowGate");
        _purpleGate = GameObject.Find("PurpleGate");
        _camera = GameObject.Find("Main Camera").GetComponent<Camera>();
        _startIndex = _simonIndex;
        _OpenedGatesList = new List<int>();
        _correctCube = GameObject.Find("CorrectCube");
        _tempSimon = new List<int>();

    }
	
	// Update is called once per frame
	void Update () {
        if (!_gameFinished)
        {
            _clickOnGates();
            _checkGates();
            _simonSays();
            _animation();
        }
        else
        {
           // if (Halo.enabled == true)
           // {
           //     Halo.enabled = false;
           // }
        }
    }

    void _clickOnGates()
    {
        if (_canClickGate)
        {
            if (Input.GetMouseButtonDown(0))
            {
                //Raycasting the position clicked
                RaycastHit vHit = new RaycastHit();
                Ray vRay = _camera.ScreenPointToRay(Input.mousePosition);
                if (Physics.Raycast(vRay, out vHit, 1000))
                {
                    if (vHit.collider.gameObject.name == "RedGate" || vHit.collider.gameObject.name == "BlueGate" || vHit.collider.gameObject.name == "GreenGate" ||
                        vHit.collider.gameObject.name == "YellowGate" || vHit.collider.gameObject.name == "PurpleGate")
                    {
                        if (vHit.collider.gameObject.transform.position.y == 0) //-73.25f)
                        {
                            switch (vHit.collider.gameObject.name)
                            {
                                case "RedGate":
                                    _OpenedGatesList.Add(1);
                                    Debug.Log("RedGate");
                                    break;
                                case "BlueGate":
                                    _OpenedGatesList.Add(2);
                                    Debug.Log("BlueGate");
                                    break;
                                case "GreenGate":
                                    _OpenedGatesList.Add(3);
                                    Debug.Log("GreenGate");
                                    break;
                                case "YellowGate":
                                    _OpenedGatesList.Add(4);
                                    Debug.Log("YellowGate");
                                    break;
                                case "PurpleGate":
                                    _OpenedGatesList.Add(5);
                                    Debug.Log("PurpleGate");
                                    break;
                                default:
                                    break;
                            }
                            //vHit.collider.gameObject.transform.position = new Vector3(vHit.collider.gameObject.transform.position.x, 15 /*-61.94f*/, vHit.collider.gameObject.transform.position.z);
                            _startAnimation = true;
                            _animation(vHit.collider.gameObject);           
                        }
                        else
                        {
                            //vHit.collider.gameObject.transform.position = new Vector3(vHit.collider.gameObject.transform.position.x, 0 /*-73.25f*/, vHit.collider.gameObject.transform.position.z);
                            _animation(vHit.collider.gameObject);
                            _closingAnimation = true;
                            switch (vHit.collider.gameObject.name)
                            {
                                case "RedGate":
                                    _OpenedGatesList.Remove(1);
                                    break;
                                case "BlueGate":
                                    _OpenedGatesList.Remove(2);
                                    break;
                                case "GreenGate":
                                    _OpenedGatesList.Remove(3);
                                    break;
                                case "YellowGate":
                                    _OpenedGatesList.Remove(4);
                                    break;
                                case "PurpleGate":
                                    _OpenedGatesList.Remove(5);
                                    break;
                                default:
                                    break;
                            }
                        }
                    }
                }
            }
        }
    }

    private void _animation(GameObject pGate = null)
    {
        if (pGate != null)
        {
            _tempGate = pGate;
        }
        if (_startAnimation)
        {
            if (_tempGate.transform.position.y <= 15)
            {
                _tempGate.transform.position = new Vector3(_tempGate.transform.position.x, _tempGate.transform.position.y + 0.5f, _tempGate.transform.position.z);
            }
            else
            {
                _tempGate.transform.position = new Vector3(_tempGate.transform.position.x, 15, _tempGate.transform.position.z);
                _startAnimation = false;
            }
        }
        if(_closingAnimation)
        {
            if (_tempGate.transform.position.y >= 0)
            {
                _tempGate.transform.position = new Vector3(_tempGate.transform.position.x, _tempGate.transform.position.y - 0.5f, _tempGate.transform.position.z);
            }
            else
            {
                _tempGate.transform.position = new Vector3(_tempGate.transform.position.x, 0, _tempGate.transform.position.z);
                _closingAnimation = false;
            }
        }
    }


    void _checkGates()
    {
        //Debug.Log("gate: " + _OpenedGatesList.Count);
        if (_endIndex != 0 && _OpenedGatesList.Count > (_endIndex - _startIndex) && _correctOrder == false)
        {
            int count = _endIndex - _startIndex;
            _tempIndex = _startIndex;
            //for (int i = 0; i < count; i++)
            //{
            _tempSimon = _tempSimon.Distinct().ToList();
            //_tempSimon = (List<int>)tempTempSimon;
            _OpenedGatesList = _OpenedGatesList.Distinct().ToList();
                if (_tempSimon.SequenceEqual(_OpenedGatesList) )
                {
                    _correctOrder = true;
                    _gateAmount = 0;
                    _tempSimon = new List<int>();
                }
                else if(_gateAmount == _OpenedGatesList.Count)
                {
                    _correctOrder = false;
                    _retry = true;
                }
            //} 
        }

        if (_correctOrder)
        {
            _correctCube.GetComponent<Renderer>().enabled = true;
            _correctTimer++;
            if (_correctTimer > 60)
            {
                _correctCube.GetComponent<Renderer>().enabled = false;
                _startIndex = _simonIndex;
                _endIndex = 0;
                _OpenedGatesList = new List<int>();
                _simonShowed = false;
                _redGate.transform.position = new Vector3(_redGate.transform.position.x,0 /*-73.25f*/, _redGate.transform.position.z);
                _blueGate.transform.position = new Vector3(_blueGate.transform.position.x,0 /*-73.25f*/, _blueGate.transform.position.z);
                _greenGate.transform.position = new Vector3(_greenGate.transform.position.x,0 /*-73.25f*/, _greenGate.transform.position.z);
                _yellowGate.transform.position = new Vector3(_yellowGate.transform.position.x,0 /*-73.25f*/, _yellowGate.transform.position.z);
                _purpleGate.transform.position = new Vector3(_purpleGate.transform.position.x,0 /*-73.25f*/, _purpleGate.transform.position.z);
                _haloShowed = false;
                _correctOrder = false;
                _correctTimer = 0;
                _canClickGate = false;
            }
        }
        else if(_retry)
        {
            _correctCube.GetComponent<Renderer>().enabled = false;
            _simonIndex = 0;
            _gateAmount = 0;
            _tempSimon = new List<int>();
            _lifes--;
            _startIndex = 0;
            _endIndex = 0;
            _OpenedGatesList = new List<int>();
            _simonShowed = false;
            _haloShowed = false;
            _correctOrder = false;
            _correctTimer = 0;
            _canClickGate = false;
            _retry = false;
            _redGate.transform.position = new Vector3(_redGate.transform.position.x, 0 /*-73.25f*/, _redGate.transform.position.z);
            _blueGate.transform.position = new Vector3(_blueGate.transform.position.x, 0 /*-73.25f*/, _blueGate.transform.position.z);
            _greenGate.transform.position = new Vector3(_greenGate.transform.position.x, 0 /*-73.25f*/, _greenGate.transform.position.z);
            _yellowGate.transform.position = new Vector3(_yellowGate.transform.position.x, 0 /*-73.25f*/, _yellowGate.transform.position.z);
            _purpleGate.transform.position = new Vector3(_purpleGate.transform.position.x, 0 /*-73.25f*/, _purpleGate.transform.position.z);
        }
    }

    void _simonSays()
    {
        if (SimonOrder[_simonIndex] == 0)
        {
            _simonShowed = true;

            if(_haloTimer > 50)
            {
                _endIndex = _simonIndex - 1;
                _simonIndex++;
                _haloTimer = 0;
                if (Halo != null)
                {
                    Halo.enabled = false;
                }
                _canClickGate = true;
            }
            else
            {
                _haloTimer++;
                _canClickGate = false;
            }
        }
        if (!_simonShowed)
        {
            if (!_haloShowed)
            {
                int Gate = SimonOrder[_simonIndex];
                if (SimonOrder[_simonIndex] == 6)
                {
                    _gameFinished = true;
                    _simonIndex = 0;
                }
                _simonIndex++;
                _gateAmount++;
                if (SimonOrder[_simonIndex] == 0)
                {
                    _canClickGate = true;
                }

                Halo = null;
                switch (Gate)
                {
                    //Red Gate
                    case 1:
                        Halo = (Behaviour)_redGate.GetComponent("Halo");
                        Halo.enabled = true;
                        _tempSimon.Add(1);
                        break;
                    //Blue Gate
                    case 2:
                        Halo = (Behaviour)_blueGate.GetComponent("Halo");
                        Halo.enabled = true;
                        _tempSimon.Add(2);
                        break;
                    //Green Gate
                    case 3:
                        Halo = (Behaviour)_greenGate.GetComponent("Halo");
                        Halo.enabled = true;
                        _tempSimon.Add(3);
                        break;
                    //Yellow Gate
                    case 4:
                        Halo = (Behaviour)_yellowGate.GetComponent("Halo");
                        Halo.enabled = true;
                        _tempSimon.Add(4);
                        break;
                    //Purple Gate
                    case 5:
                        Halo = (Behaviour)_purpleGate.GetComponent("Halo");
                        Halo.enabled = true;
                        _tempSimon.Add(5);
                        break;
                }
                _haloShowed = true;
            }
            else if(_haloShowed)
            {
                _haloTimer++;
                if(_haloTimer > 50)
                {
                    _haloShowed = false;
                    if (Halo != null)
                    {
                        Halo.enabled = false;
                        _haloTimer = 0;
                    }
                }
            }
        }
    }
} 
