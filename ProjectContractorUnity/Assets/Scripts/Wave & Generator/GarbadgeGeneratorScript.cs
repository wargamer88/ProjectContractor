using UnityEngine;
using System.Collections;
using System.Linq;

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

    private GameObject _segment_front;
    private GameObject _segment_back;
    private GameObject _segment_segment5;
    private GameObject _segment_segment4;
    private GameObject _segment_segment3;
    private GameObject _pickedSegment;


    // Use this for initialization
    void Start () {
        _generatorPowerScript = transform.parent.GetComponent<GeneratorPowerScript>();
        _garbageWaveScript = GameObject.FindObjectOfType<GarbageWaveScript>();
        _oldTimer = 0;
        if (transform.name == "GeneratorWall5")
        {
            _segment_front = GameObject.Find("segment_front1");
            _pickedSegment = _segment_front;
        }
        if (transform.name == "GeneratorWall2")
        {
            _segment_segment5 = GameObject.Find("segment_segment5");
            _pickedSegment = _segment_segment5;
        }
        if (transform.name == "GeneratorWall3")
        {
            _segment_segment4 = GameObject.Find("segment_segment4");
            _pickedSegment = _segment_segment4;
        }
        if (transform.name == "GeneratorWall4")
        {
            _segment_segment3 = GameObject.Find("segment_segment3");
            _pickedSegment = _segment_segment3;
        }
        if (transform.name == "GeneratorWall1")
        {
            _segment_back = GameObject.Find("segment_back1");
            _pickedSegment = _segment_back;
        }
    }
	
	// Update is called once per frame
	void Update () {
        if (_generatorHealth <= 0 && _isDestroyed == false)
        {
            _generatorPowerScript.DestroyedGenerator++;
            _isDestroyed = true;
            _pickedSegment.GetComponent<Renderer>().material.color = Color.gray;
        }
        if (_generatorHealth < 20)
        {
            _pickedSegment.GetComponent<Renderer>().material.color = Color.red;
        }
        else if (_generatorHealth < 50)
        {
            _pickedSegment.GetComponent<Renderer>().material.color = Color.yellow;
        }
        else
        {
            _pickedSegment.GetComponent<Renderer>().material.color = Color.green;
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
            if (_garbageWaveScript.LightGarbage.Where(c=>c.gameObject.name == pOther.gameObject.name).FirstOrDefault())
            {
                _generatorHealth = _generatorHealth - _basichit;
            }
            if (_garbageWaveScript.MediumGarbage.Where(c => c.gameObject.name == pOther.gameObject.name).FirstOrDefault())
            {
                _generatorHealth = _generatorHealth - _mediumhit;
            }
            if (_garbageWaveScript.HeavyGarbage.Where(c => c.gameObject.name == pOther.gameObject.name).FirstOrDefault())
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
