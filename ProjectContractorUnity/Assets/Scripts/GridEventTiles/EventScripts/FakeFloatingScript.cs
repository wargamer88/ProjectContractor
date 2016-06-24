using UnityEngine;
using System.Collections;

public class FakeFloatingScript : MonoBehaviour {
    
    //timer to switch
    private float _timer = 1f;
    //timer to save the time.time
    private float _oldTimer = 0;
    //Changing up to down and down to up
    private bool _isChanged = true;

	/// <summary>
    /// If the game is running then change over time the IsChanged and instead of + do minus and the other way around
    /// </summary>
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
