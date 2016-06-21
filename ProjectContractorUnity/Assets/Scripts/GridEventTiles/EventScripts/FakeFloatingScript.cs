using UnityEngine;
using System.Collections;

public class FakeFloatingScript : MonoBehaviour {

    private float _floating = 0;
    private float _timer = 1f;
    private float _oldTimer = 0;
    private bool _isChanged = true;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (Time.timeScale == 1)
        {
            if (Time.time > (_oldTimer + _timer))
            {
                _oldTimer = Time.time;
                if (_isChanged)
                {
                    _isChanged = false;
                }
                else if (_isChanged == false)
                {
                    _isChanged = true;
                }
            }
            if (_isChanged)
            {
                this.transform.position = new Vector3(this.transform.position.x, this.transform.position.y + 0.005f, this.transform.position.z);
            }
            else if (_isChanged == false)
            {
                this.transform.position = new Vector3(this.transform.position.x, this.transform.position.y - 0.005f, this.transform.position.z);
            }
        }
	
	}
}
