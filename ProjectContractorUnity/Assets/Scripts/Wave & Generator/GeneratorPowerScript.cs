using UnityEngine;
using System.Collections;

public class GeneratorPowerScript : MonoBehaviour {

    private float _generatorEngergy = 100;
    private float _generatorHP = 100;
    private float _oldTime = 0;
    [SerializeField]
    private float _timerGenerator = 0;
    [SerializeField]
    private float _timerDamage = 0;

    private int _destroyedGenerator = 0;
    public int DestroyedGenerator { set { _destroyedGenerator = value; } get { return _destroyedGenerator; } }
    public float GeneratorEnergy { set { _generatorEngergy = value;} get { return _generatorEngergy; } }

    private int _amount = 0;
    public int Amount { set { _amount = value; } get { return _amount; } }

    // Use this for initialization
    void Start () {

	}
	
	// Update is called once per frame
	void Update () { 
        if (Time.time > (_oldTime + _timerGenerator))
        {
            _oldTime = Time.time;
            _generatorEngergy = _generatorEngergy - _amount;
        }
        if (_generatorEngergy < 50)
        {
            //Still going to need it?
        }
        if (_destroyedGenerator == 5)
        {
            Debug.Log("YOU ARE DEAD");
        }
    }
}
