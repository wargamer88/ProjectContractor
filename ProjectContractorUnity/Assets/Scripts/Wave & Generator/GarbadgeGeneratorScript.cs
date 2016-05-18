using UnityEngine;
using System.Collections;

public class GarbadgeGeneratorScript : MonoBehaviour {

    private GeneratorPowerScript _generatorPowerScript;
    private float _oldTimer;
    private float _timer = 0.5f;
    private bool _hitGenerator = false;
    

	// Use this for initialization
	void Start () {
        _generatorPowerScript = transform.parent.GetComponent<GeneratorPowerScript>();
        _oldTimer = 0;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnCollisionStay(Collision pOther)
    {
        if (_hitGenerator == false)
        {
            _generatorPowerScript.Amount = _generatorPowerScript.Amount - 1;
            _hitGenerator = true;
        }
    }
    void OnCollisionExit(Collision pOther)
    {
        _generatorPowerScript.Amount = _generatorPowerScript.Amount + 1;
        _hitGenerator = false;
    }
}
