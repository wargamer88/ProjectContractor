using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class HighscoreScript : MonoBehaviour {

    #region ScoreVariables
    private int _score = 0;

    [SerializeField]
    private int _lightTrashScore;

    [SerializeField]
    private int _mediumTrashScore;

    [SerializeField]
    private int _heavyTrashScore;

    [SerializeField]
    private int _superHeavyTrashScore;

    [SerializeField]
    private string _finishWaveText;

    [SerializeField]
    private int _finishWaveScore;

    [SerializeField]
    private string _noDamageText;

    [SerializeField]
    private int _noDamageScore;

    [SerializeField]
    private string _3inRowText;

    [SerializeField]
    private int _3inRowScore;

    [SerializeField]
    private string _5inRowText;

    [SerializeField]
    private int _5inRowScore;

    [SerializeField]
    private string _10inRowText;

    [SerializeField]
    private int _10inRowScore;

    [SerializeField]
    private string _15inRowText;

    [SerializeField]
    private int _15inRowScore;
    #endregion

    [SerializeField]
    private float _maxTimeShowComboText;

    private GameObject _scoreUI;
    private GameObject _comboUI;
    private GameObject _waveClearUI;

    private int _comboCounter;
    private float _timer;

    public int ComboCounter { get { return _comboCounter; } set { _comboCounter = value; } }

    // Use this for initialization
    void Start () {
        _score = 0;
        _timer = 0;
        _scoreUI = GameObject.Find("Score");
        _comboUI = GameObject.Find("ComboText");
        _waveClearUI = GameObject.Find("WaveCleared");
	}
	
	// Update is called once per frame
	void Update () {
        CheckComboTimer();
	}

    private void CheckComboTimer()
    {
        if (_comboUI.GetComponent<Text>().enabled == true || _waveClearUI.GetComponent<Text>().enabled == true)
        {
            _timer++;
            if (_timer >= _maxTimeShowComboText)
            {
                _comboUI.GetComponent<Text>().enabled = false;
                _waveClearUI.GetComponent<Text>().enabled = false;
                _timer = 0;
            }
        }
    }

    public void AddTrashScore(GarbageType pGarbageType)
    {
        switch (pGarbageType)
        {
            case GarbageType.none:
                break;
            case GarbageType.Light:
                _score += _lightTrashScore;
                break;
            case GarbageType.Medium:
                _score += _mediumTrashScore;
                break;
            case GarbageType.Heavy:
                _score += _heavyTrashScore;
                break;
            case GarbageType.Special:
                _score += _superHeavyTrashScore;
                break;
        }
        UpdateScore();
    }

    private void UpdateScore()
    {
        _scoreUI.GetComponent<Text>().text = _score.ToString();
    }

    public void ComboCheck()
    {
        Debug.Log("Combo: " + _comboCounter);
        if (_comboCounter == 3)
        {
            _score += _3inRowScore;
            _comboUI.GetComponent<Text>().text = _3inRowText;
            _comboUI.GetComponent<Text>().enabled = true;
        }
        else if(_comboCounter == 5)
        {
            _score += _5inRowScore;
            _comboUI.GetComponent<Text>().text = _5inRowText;
            _comboUI.GetComponent<Text>().enabled = true;
        }
        else if(_comboCounter == 10)
        {
            _score += _10inRowScore;
            _comboUI.GetComponent<Text>().text = _10inRowText;
            _comboUI.GetComponent<Text>().enabled = true;
        }
        else if(_comboCounter == 15)
        {
            _score += _15inRowScore;
            _comboUI.GetComponent<Text>().text = _15inRowText;
            _comboUI.GetComponent<Text>().enabled = true;
        }
        UpdateScore();
    }

    public void WaveClear(bool pHitAnything)
    {
        if (pHitAnything)
        {
            _score += _noDamageScore;
            _comboUI.GetComponent<Text>().text = _noDamageText;
            _comboUI.GetComponent<Text>().enabled = true;
        }
        _waveClearUI.GetComponent<Text>().text = _finishWaveText;
        _waveClearUI.GetComponent<Text>().enabled = true;
        _score += _finishWaveScore;
    }
}
