using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class TidalInputWave : MonoBehaviour {

    private Vector3 _startPosition;
    private Vector3 _endPosition;
    private Vector3 _velocity;
    private bool _startMouse = true;
    private int _totalEnergy = 0;
    private GameObject _energyText;

	// Use this for initialization
	void Start () {
        _energyText = GameObject.Find("EnergyValue");

    }
	
	// Update is called once per frame
	void Update () {
        MouseInput();
	}

    void MouseInput()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (_startMouse)
            {
                _startPosition = Input.mousePosition;
                _startMouse = false;
            }
        }
        else if (Input.GetMouseButtonUp(0))
        {
            _endPosition = Input.mousePosition;
            _velocity = _endPosition - _startPosition;
            int magnitude = Mathf.FloorToInt(_velocity.magnitude);
            _totalEnergy += magnitude;
            _energyText.GetComponent<Text>().text = _totalEnergy.ToString();
            _startMouse = true;

        }
    }
}
