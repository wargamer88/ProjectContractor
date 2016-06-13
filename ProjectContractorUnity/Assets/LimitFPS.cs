using UnityEngine;
using System.Collections;

public class LimitFPS : MonoBehaviour {

    [SerializeField]
    private int _limitFPSTo;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        QualitySettings.vSyncCount = 0;  // VSync must be disabled
        Application.targetFrameRate = _limitFPSTo;
    }
}
