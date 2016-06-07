using UnityEngine;
using System.Collections;

public class FishClickedOnScript : MonoBehaviour {

    private PowerupsScript _powerupsScript;

    public PowerupsScript PowerupsScript { set { _powerupsScript = value; } }

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	    
	}

    void OnMouseDown()
    {
        if (this.gameObject.name == "Chompy")
        {
            _powerupsScript.FishClickedOn(GarbageType.Light);
            Destroy(this.gameObject);
        }
        if (this.gameObject.name == "Sharky")
        {
            _powerupsScript.FishClickedOn(GarbageType.Medium);
            Destroy(this.gameObject);
        }
        if (this.gameObject.name == "Whaley")
        {
            _powerupsScript.FishClickedOn(GarbageType.Heavy);
            Destroy(this.gameObject);
        }
    }
}
