using UnityEngine;
using System.Collections;

public class FakeFloatingTilderScript : MonoBehaviour
{
    //floating height 
    [SerializeField]
    private float floatingHeight = 0.005f;
    //time between going up and down
    [SerializeField]
    private float _timer = 2f;
    //timer to save the time.time
    private float _oldTimer = 0;
    //Changing up to down and down to up
    private bool _isChanged = true;
    //If the tidel started
    private bool _isStarted = false;
    public bool IsStarted { get { return _isStarted; } set { _isStarted = value; } }

    /// <summary>
    /// If the game is running then change over time the IsChanged and instead of + do minus and the other way around
    /// </summary>
    void Update()
    {
        if (Time.timeScale == 1)
        {
            if (_isStarted)
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
                    this.transform.position = new Vector3(this.transform.position.x, this.transform.position.y + floatingHeight, this.transform.position.z);
                }
                else if (_isChanged == false)
                {
                    this.transform.position = new Vector3(this.transform.position.x, this.transform.position.y - floatingHeight, this.transform.position.z);
                }
            }
        }

    }
}
