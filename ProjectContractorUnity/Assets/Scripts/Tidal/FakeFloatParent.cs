using UnityEngine;
using System.Collections;

public class FakeFloatParent : MonoBehaviour {

    //next generator timer
    [SerializeField]
    private float _nextFloatingTimer = 1;
    //Old time when the previous generator started floating
    private float _oldFloatTimer;
    //array for all the fakefloating scripts
    private FakeFloatingTilderScript[] _fakeFloating;

    /// <summary>
    /// Getting all the scripts
    /// </summary>
	void Start () {
        _fakeFloating = GetComponentsInChildren<FakeFloatingTilderScript>();
	}
	
	// Update is called once per frame
	void Update () {
        if (Time.timeScale == 1)
        {
            if (Time.time > (_oldFloatTimer + _nextFloatingTimer))
            {
                _oldFloatTimer = Time.time;
                foreach (FakeFloatingTilderScript generator in _fakeFloating)
                {
                    if (generator.gameObject != this.gameObject)
                    {
                        generator.IsStarted = true;
                        break;
                    }
                }
            }
        }
    }
}
