using UnityEngine;
using System.Collections;

public class FishClickedOnScript : MonoBehaviour {

    private PowerupsScript _powerupsScript;
    private bool _jumping = false;

    public PowerupsScript PowerupsScript { set { _powerupsScript = value; } }
    public bool Jumping { set { _jumping = value; } }

    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        if (this.transform.position.y < -20)
        {
            Destroy(this.gameObject);
        }
	}

    void OnMouseDown()
    {



        if (this.gameObject.name == "Chompy")
        {
            if (_jumping)
            {
                _powerupsScript.FishClickedOn(true, GarbageType.Light);
            }
            else
            {
                _powerupsScript.FishClickedOn(false, GarbageType.Light);
            }
            Destroy(this.gameObject);
        }
        if (this.gameObject.name == "Sharky")
        {
            if (_jumping)
            {
                _powerupsScript.FishClickedOn(true, GarbageType.Medium);
            }
            else
            {
                _powerupsScript.FishClickedOn(false, GarbageType.Medium);
            }
            Destroy(this.gameObject);
        }
        if (this.gameObject.name == "Whaley")
        {
            if (_jumping)
            {
                _powerupsScript.FishClickedOn(true, GarbageType.Heavy);
            }
            else
            {
                _powerupsScript.FishClickedOn(false, GarbageType.Heavy);
            }
            Destroy(this.gameObject);
        }
    }
}
