using UnityEngine;
using System.Collections.Generic;

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
    

    public int[] SimonOrder;
    private int _simonIndex = 0;

    // Use this for initialization
    void Start () {
        _redGate = GameObject.Find("RedGate");
        _blueGate = GameObject.Find("BlueGate");
        _greenGate = GameObject.Find("GreenGate");
        _yellowGate = GameObject.Find("YellowGate");
        _purpleGate = GameObject.Find("PurpleGate");
	}
	
	// Update is called once per frame
	void Update () {
        SimonSays();
        ClickOnGates();
    }

    void ClickOnGates()
    {
        if (Input.GetMouseButtonDown(0))
        {

        }
    }

    void SimonSays()
    {
        if (SimonOrder[_simonIndex] == 0)
        {
            _simonShowed = true;
            _simonIndex++;
            if (Halo != null)
            {
                Halo.enabled = false;
            }
        }
        if (!_simonShowed)
        {
            if (!_haloShowed)
            {
                int Gate = SimonOrder[_simonIndex];
                _simonIndex++;
                Debug.Log(_simonIndex);
                Halo = null;
                switch (Gate)
                {
                    //Red Gate
                    case 1:
                        Halo = (Behaviour)_redGate.GetComponent("Halo");
                        Halo.enabled = true;
                        break;
                    //Blue Gate
                    case 2:
                        Halo = (Behaviour)_blueGate.GetComponent("Halo");
                        Halo.enabled = true;
                        break;
                    //Green Gate
                    case 3:
                        Halo = (Behaviour)_greenGate.GetComponent("Halo");
                        Halo.enabled = true;
                        break;
                    //Yellow Gate
                    case 4:
                        Halo = (Behaviour)_yellowGate.GetComponent("Halo");
                        Halo.enabled = true;
                        break;
                    //Purple Gate
                    case 5:
                        Halo = (Behaviour)_purpleGate.GetComponent("Halo");
                        Halo.enabled = true;
                        break;
                }
                _haloShowed = true;
            }
            else if(_haloShowed)
            {
                _haloTimer++;
                if(_haloTimer > 60)
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
