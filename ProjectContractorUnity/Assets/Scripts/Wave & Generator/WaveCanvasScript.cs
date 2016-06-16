using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class WaveCanvasScript : MonoBehaviour {

    private Text _waveNumberText;
	// Use this for initialization
	void Start () {
        _waveNumberText = GameObject.Find("Wave").GetComponent<Text>();
        ChangeWaveNumber(1);
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void ChangeWaveNumber(int pWaveNumber)
    {
        _waveNumberText.text = pWaveNumber.ToString();
    }
}
