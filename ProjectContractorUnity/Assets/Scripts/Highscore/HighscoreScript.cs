using UnityEngine;
using UnityEngine.UI;
using System.Collections;

/// <summary>
/// <para>The ComboTypes for example FinishWave etc.</para>
/// </summary>
public enum ComboType
{
    FinishWave,
    Untouchable,
    ThreeinRow,
    FiveinRow,
    TeninRow,
    FifteeninRow
}

public class HighscoreScript : MonoBehaviour {

    #region Variables
    #region ScoreVariables
    //The amount of score that the player has
    private int _score = 0;
    public int Score { get { return _score; } }

    //The amount that Light, Medium, Heavy and Super Heavy trash gives yoy
    [SerializeField]
    private int _lightTrashScore;
    [SerializeField]
    private int _mediumTrashScore;
    [SerializeField]
    private int _heavyTrashScore;
    [SerializeField]
    private int _superHeavyTrashScore;

    //All the Combo Images and Scores
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

    //The amount of time to show the Images
    [SerializeField]
    private float _maxTimeShowCombo;

    //The GameObjects used to show the different Images
    private GameObject _scoreUI;
    private GameObject _comboUI;
    private GameObject _nietaanterakenUI;
    private GameObject _superUI;
    private GameObject _geweldig;
    private GameObject _ongelooflijk;
    private GameObject _superheld;
    private GameObject _waveClearUI;

    //Reference to the Particle Script
    private NumberParticleScript _numberParticle;

    //The Timer used for Showing the Images
    private float _timer;

    //The counter to see how your combo is progressing
    private int _comboCounter;

    //The property for reading and editing the ComboCounter
    public int ComboCounter { get { return _comboCounter; } set { _comboCounter = value; } }

    private CharacterScript _characterScript;
    #endregion

    /// <summary>
    /// <para>set all the default Variables</para>
    /// <para>find all the Images needed</para>
    /// <para>NumberParticleScript is put in here</para>
    /// </summary>
    void Start () {
        _score = 0;
        _timer = 0;
        _scoreUI = GameObject.Find("Score");
        _nietaanterakenUI = GameObject.Find("Nietaanteraken");
        _superUI = GameObject.Find("Super");
        _geweldig = GameObject.Find("Geweldig");
        _ongelooflijk = GameObject.Find("Ongelooflijk");
        _superheld = GameObject.Find("Superheld");
        _comboUI = GameObject.Find("ComboImage");
        _waveClearUI = GameObject.Find("WaveCleared");
        _numberParticle = GameObject.FindObjectOfType<NumberParticleScript>();
        _characterScript = GameObject.FindObjectOfType<CharacterScript>();

    }
	
	/// <summary>
    /// <para>Calls the Function which checks the Combo Timer to see when to hide Image</para>
    /// </summary>
	void Update () {
        _checkComboTimer();
        _updateScore();
    }

    /// <summary>
    /// <para>Checks when to hide the Combo Images</para>
    /// </summary>
    private void _checkComboTimer()
    {
        if (_comboUI.GetComponent<Image>().enabled == true || _waveClearUI.GetComponent<Image>().enabled == true || _superUI.GetComponent<Image>().enabled == true ||
            _nietaanterakenUI.GetComponent<Image>().enabled == true || _geweldig.GetComponent<Image>().enabled == true || _ongelooflijk.GetComponent<Image>().enabled == true
            || _superheld.GetComponent<Image>().enabled == true)
        {
            _timer++;
            if (_timer >= _maxTimeShowCombo)
            {
                DisableUI();
                _timer = 0;
            }
        }

    }

    /// <summary>
    /// <para>Adds the Score with the specific type of Trash</para>
    /// </summary>
    /// <param name="pGarbageType"></param>
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
        _updateScore();
    }

    /// <summary>
    /// <para>Adds the score to the HuD</para>
    /// </summary>
    private void _updateScore()
    {
        _scoreUI.GetComponent<Text>().text = _score.ToString();
    }

    /// <summary>
    /// <para>Checks the progress of the Combo and adds Score if a Combo is triggered</para>
    /// </summary>
    public void ComboCheck()
    {
        if (_comboCounter == 5)
        {
            _score += _3inRowScore;
            _numberParticle.PlaceParticleCombo(ComboType.ThreeinRow);
            if (_3inRowImage != null)
            {
                _superUI.GetComponent<Image>().sprite = _3inRowImage;
                _superUI.GetComponent<Image>().enabled = true; 
            }
        }
        else if(_comboCounter == 10)
        {
            _score += _5inRowScore;
            _numberParticle.PlaceParticleCombo(ComboType.FiveinRow);
            _characterScript.CheerCharacters();
            if (_5inRowImage != null)
            {
               _geweldig.GetComponent<Image>().sprite = _5inRowImage;
               _geweldig.GetComponent<Image>().enabled = true; 
            }
        }
        else if(_comboCounter == 15)
        {
            _score += _10inRowScore;
            _numberParticle.PlaceParticleCombo(ComboType.TeninRow);
            _characterScript.CheerCharacters();
            if (_10inRowImage != null)
            {
                _ongelooflijk.GetComponent<Image>().sprite = _10inRowImage;
                _ongelooflijk.GetComponent<Image>().enabled = true; 
            }
        }
        else if(_comboCounter == 20)
        {
            _score += _15inRowScore;
            _numberParticle.PlaceParticleCombo(ComboType.FifteeninRow);
            _characterScript.CheerCharacters();
            if (_15inRowImage != null)
            {
                _superheld.GetComponent<Image>().sprite = _15inRowImage;
                _superheld.GetComponent<Image>().enabled = true; 
            }
            _comboCounter = 0;
        }
        _updateScore();
    }

    /// <summary>
    /// <para>Check if generator has been hit, if not gives bonus points</para>
    /// <para>Gives you points for finishing wave</para>
    /// </summary>
    /// <param name="pHitAnything"></param>
    public void WaveClear(bool pHitAnything)
    {
        DisableUI();
        if (!pHitAnything)
        {
            _score += _noDamageScore;
            _numberParticle.PlaceParticleCombo(ComboType.Untouchable);
            if (_noDamageImage != null)
            {
                _nietaanterakenUI.GetComponent<Image>().sprite = _noDamageImage;
                _nietaanterakenUI.GetComponent<Image>().enabled = true; 
            }
            pHitAnything = true;
        }
        if (_finishWaveImage != null)
        {
            _waveClearUI.GetComponent<Image>().sprite = _finishWaveImage;
            _waveClearUI.GetComponent<Image>().enabled = true; 
        }
        _score += _finishWaveScore;
        _numberParticle.PlaceParticleCombo(ComboType.FinishWave);
        _characterScript.CheerCharacters();
    }
    /// <summary>
    /// <para>Disable all Images</para>
    /// </summary>
    public void DisableUI()
    {
        _comboUI.GetComponent<Image>().enabled = false;
        _nietaanterakenUI.GetComponent<Image>().enabled = false;
        _superUI.GetComponent<Image>().enabled = false;
        _geweldig.GetComponent<Image>().enabled = false;
        _ongelooflijk.GetComponent<Image>().enabled = false;
        _superheld.GetComponent<Image>().enabled = false;
        _waveClearUI.GetComponent<Image>().enabled = false;
    }
}
