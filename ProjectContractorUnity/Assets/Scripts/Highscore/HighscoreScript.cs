using UnityEngine;
using System.Collections;

public class HighscoreScript : MonoBehaviour {

    #region ScoreVariables
    private int _score;

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


    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void AddTrashScore()
    {

    }
}
