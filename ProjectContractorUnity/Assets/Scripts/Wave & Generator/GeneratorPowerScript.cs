using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GeneratorPowerScript : MonoBehaviour {

    #region Variables
    //Amount of Waves in the game
    private int _amountOfWaves = 0;
    //GarbageWaveScript for knowin the current wave
    private GarbageWaveScript _garbageWaveScript;
    //bool to know if the Score is already send
    private bool _scoreSend = false;

    //amount of destroyed Generators
    private int _destroyedGenerator = 0;

    private GameObject _gefeliciteerd;
    private GameObject _verslagen;
    private bool _wonOrLost = false;
    private System.DateTime _timeForSendScore;

    //properties
    public int DestroyedGenerator { set { _destroyedGenerator = value; } get { return _destroyedGenerator; } }
    #endregion

    /// <summary>
    /// get all the ExecuteEventScripts and egt the highest wave number
    /// </summary>
    void Start () {
        _gefeliciteerd = GameObject.Find("Gefeliciteerd");
        _gefeliciteerd.SetActive(false);

        _verslagen = GameObject.Find("Verslagen");
        _verslagen.SetActive(false);

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

    /// <summary>
    /// <para>check for the lost condition, all 5 generators destroyed</para>
    /// <para>or Check for win condition, current wave is higher than max amount calculated in Start</para>
    /// <para>when one of those conditions is true, send the score to HEIM Mainframe</para>
    /// </summary>
    void Update () {
        if (!_wonOrLost)
        {
            //                     LOST                            WON
            if (_destroyedGenerator == 5 || _garbageWaveScript.Wave > _amountOfWaves)
            {
                _wonOrLost = true;
                if (_destroyedGenerator == 5)
                {
                    _verslagen.SetActive(true);
                    _verslagen.GetComponentInChildren<Text>().text = "Score\n" + FindObjectOfType<HighscoreScript>().Score.ToString();
                    _timeForSendScore = System.DateTime.UtcNow.AddSeconds(5);
                }
                else if (_garbageWaveScript.Wave > _amountOfWaves)
                {
                    _gefeliciteerd.SetActive(true);
                    _gefeliciteerd.GetComponentInChildren<Text>().text = "Score\n" + FindObjectOfType<HighscoreScript>().Score.ToString();
                    _timeForSendScore = System.DateTime.UtcNow.AddSeconds(5);
                }
            }
        }
        if (_wonOrLost && !_scoreSend && System.DateTime.UtcNow > _timeForSendScore) //Making sure the score is send once
        {
            Debug.Log("Game Ended");
            StartCoroutine(FindObjectOfType<DBconnection>().UploadScore(FindObjectOfType<HighscoreScript>().Score));
            _scoreSend = true;
        }

    }
}
