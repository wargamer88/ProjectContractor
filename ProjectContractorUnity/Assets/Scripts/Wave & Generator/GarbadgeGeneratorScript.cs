using UnityEngine;
using System.Collections;

public class GarbadgeGeneratorScript : MonoBehaviour {

    private GeneratorPowerScript _generatorPowerScript;
    private GarbageWaveScript _garbageWaveScript;
    private float _oldTimer;
    private float _timer = 0.5f;
    private bool _hitGenerator = false;

    [SerializeField]
    private float _hitTimer = 1;

    [SerializeField]
    private float _repairTime = 10;

    [SerializeField]
    private float _basichit = 1;
    [SerializeField]
    private float _mediumhit = 2;
    [SerializeField]
    private float _heavyhit = 3;

    private bool _startRepairTimer = false;

    private float _generatorHealth = 100;

    private bool _isDestroyed = false;
    

	// Use this for initialization
	void Start () {
        _generatorPowerScript = transform.parent.GetComponent<GeneratorPowerScript>();
        _garbageWaveScript = GameObject.FindObjectOfType<GarbageWaveScript>();
        _oldTimer = 0;
	}
	
	// Update is called once per frame
	void Update () {
        if (_generatorHealth <= 0 && _isDestroyed == false)
        {
            _generatorPowerScript.DestroyedGenerator++;
            _isDestroyed = true;
        }
        if (_generatorHealth >= 100)
        {
            _generatorHealth = 100;
            _startRepairTimer = false;
        }

	}

    void OnCollisionStay(Collision pOther)
    {
        //if (_hitGenerator == false)
        //{
        //    _generatorPowerScript.Amount = _generatorPowerScript.Amount + 1;
        //    _hitGenerator = true;
        //}

        if (Time.time > (_oldTimer + _hitTimer))
        {
            if (_garbageWaveScript.BasicGarbage.Contains(pOther.gameObject))
            {
                _generatorHealth = _generatorHealth - _basichit;
            }
            if (_garbageWaveScript.MediumGarbage.Contains(pOther.gameObject))
            {

                _generatorHealth = _generatorHealth - _mediumhit;
            }
            if (_garbageWaveScript.HeavyGarbage.Contains(pOther.gameObject))
            {

                _generatorHealth = _generatorHealth - _heavyhit;
            }
            _startRepairTimer = false;
        }
    }
    void OnCollisionExit(Collision pOther)
    {
        _generatorPowerScript.Amount = _generatorPowerScript.Amount - 1;
        _hitGenerator = false;
        _startRepairTimer = true;
    }
}
