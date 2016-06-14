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
    private Sprite _finishWaveImage;

    [SerializeField]
    private int _finishWaveScore;

    [SerializeField]
    private Sprite _noDamageImage;

    [SerializeField]
    private int _noDamageScore;

    [SerializeField]
    private Sprite _3inRowImage;

    [SerializeField]
    private int _3inRowScore;

    [SerializeField]
    private Sprite _5inRowImage;

    [SerializeField]
    private int _5inRowScore;

    [SerializeField]
    private Sprite _10inRowImage;

    [SerializeField]
    private int _10inRowScore;

    [SerializeField]
    private Sprite _15inRowImage;

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
        _comboUI = GameObject.Find("ComboImage");
        _waveClearUI = GameObject.Find("WaveCleared");
	}
	
	// Update is called once per frame
	void Update () {
        CheckComboTimer();
	}

    private void CheckComboTimer()
    {
        if (_comboUI.GetComponent<Image>().enabled == true || _waveClearUI.GetComponent<Image>().enabled == true)
        {
            _timer++;
            if (_timer >= _maxTimeShowComboText)
            {
                _comboUI.GetComponent<Image>().enabled = false;
                _waveClearUI.GetComponent<Image>().enabled = false;
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
        //Debug.Log("Combo: " + _comboCounter);
        if (_comboCounter == 3)
        {
            _score += _3inRowScore;
            if (_3inRowImage != null)
            {
                _comboUI.GetComponent<Image>().sprite = _3inRowImage;
                _comboUI.GetComponent<Image>().enabled = true; 
            }
        }
        else if(_comboCounter == 5)
        {
            _score += _5inRowScore;
            if (_5inRowImage != null)
            {
                _comboUI.GetComponent<Image>().sprite = _5inRowImage;
                _comboUI.GetComponent<Image>().enabled = true; 
            }
        }
        else if(_comboCounter == 10)
        {
            _score += _10inRowScore;
            if (_10inRowImage != null)
            {
                _comboUI.GetComponent<Image>().sprite = _10inRowImage;
                _comboUI.GetComponent<Image>().enabled = true; 
            }
        }
        else if(_comboCounter == 15)
        {
            _score += _15inRowScore;
            if (_15inRowImage != null)
            {
                _comboUI.GetComponent<Image>().sprite = _15inRowImage;
                _comboUI.GetComponent<Image>().enabled = true; 
            }
        }
        UpdateScore();
    }

    public void WaveClear(bool pHitAnything)
    {
        if (pHitAnything)
        {
            _score += _noDamageScore;
            if (_noDamageImage != null)
            {
                _comboUI.GetComponent<Image>().sprite = _noDamageImage;
                _comboUI.GetComponent<Image>().enabled = true; 
            }
            pHitAnything = false;
        }
        if (_finishWaveImage != null)
        {
            _waveClearUI.GetComponent<Image>().sprite = _finishWaveImage;
            _waveClearUI.GetComponent<Image>().enabled = true; 
        }
        _score += _finishWaveScore;
    }
}
