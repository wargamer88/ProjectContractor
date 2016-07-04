using UnityEngine;
using System.Collections;

public class IdleCloseScript : MonoBehaviour {

    private float _oldTime;
    private float _endIdle = 30;
	
	void Update ()
    {
        if (Time.time != 0)
        {
            if (Time.time > (_oldTime + 1))
            {
                _oldTime = Time.time;
                _endIdle--;
                if (_endIdle == 0)
                {
                    StartCoroutine(FindObjectOfType<DBconnection>().UploadScore(FindObjectOfType<HighscoreScript>().Score));
                }
            }
        }
	}
    public void RestartIdle()
    {
        _endIdle = 30;
    }
}
