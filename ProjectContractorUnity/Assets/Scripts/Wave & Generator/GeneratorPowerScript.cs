using UnityEngine;
using System.Collections;

public class GeneratorPowerScript : MonoBehaviour {

    private float _generatorEngergy = 100;
    private float _oldTime = 0;
    [SerializeField]
    private float _timer = 0;
    public float GeneratorEnergy { set { _generatorEngergy = value;} get { return _generatorEngergy; } }

    private int _amount = 0;
    public int Amount { set { _amount = value; } get { return _amount; } }
    // Use this for initialization
    void Start () {

	}
	
	// Update is called once per frame
	void Update () {
        Debug.Log(_generatorEngergy);
        if (Time.time > (_oldTime + _timer))
        {
            _oldTime = Time.time;
            _generatorEngergy = _generatorEngergy - _amount;
        }
        if (_generatorEngergy < 50)
        {

        }
    }
}
