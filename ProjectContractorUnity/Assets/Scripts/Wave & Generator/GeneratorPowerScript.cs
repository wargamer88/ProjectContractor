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

    private int _amountOfWaves = 0;
    private GarbageWaveScript _garbageWaveScript;
    private bool _scoreSend = false;

    private int _destroyedGenerator = 0;
    public int DestroyedGenerator { set { _destroyedGenerator = value; } get { return _destroyedGenerator; } }
    public float GeneratorEnergy { set { _generatorEngergy = value;} get { return _generatorEngergy; } }

    private int _amount = 0;
    public int Amount { set { _amount = value; } get { return _amount; } }

    // Use this for initialization
    void Start () {
        _garbageWaveScript = FindObjectOfType<GarbageWaveScript>();
        foreach (ExecuteEventScript exe in FindObjectsOfType<ExecuteEventScript>())
        {
            foreach (EventTileWrapperScript ETWS in exe.EventWrapper)
            {
                if (ETWS.EventWave > _amountOfWaves)
                {
                    _amountOfWaves = ETWS.EventWave;
                }
            }
        }
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

        //          LOST                            WON
        if (_destroyedGenerator == 5 || _garbageWaveScript.Wave > _amountOfWaves)
        {
            if (!_scoreSend) //Making sure the score is send once
            {
                Debug.Log("Game Ended");
                //FindObjectOfType<DBconnection>().UploadScore(FindObjectOfType<HighscoreScript>().Score);
                StartCoroutine(FindObjectOfType<DBconnection>().UploadScore(FindObjectOfType<HighscoreScript>().Score));
                _scoreSend = true;
            }
        }
    }
}
