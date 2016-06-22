using UnityEngine;
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

    //properties
    public int DestroyedGenerator { set { _destroyedGenerator = value; } get { return _destroyedGenerator; } }
    #endregion

    /// <summary>
    /// get all the ExecuteEventScripts and egt the highest wave number
    /// </summary>
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

    /// <summary>
    /// <para>check for the lost condition, all 5 generators destroyed</para>
    /// <para>or Check for win condition, current wave is higher than max amount calculated in Start</para>
    /// <para>when one of those conditions is true, send the score to HEIM Mainframe</para>
    /// </summary>
    void Update () {
        //                     LOST                            WON
        if (_destroyedGenerator == 5 || _garbageWaveScript.Wave > _amountOfWaves)
        {
            if (!_scoreSend) //Making sure the score is send once
            {
                Debug.Log("Game Ended");
                StartCoroutine(FindObjectOfType<DBconnection>().UploadScore(FindObjectOfType<HighscoreScript>().Score));
                _scoreSend = true;
            }
        }
    }
}
