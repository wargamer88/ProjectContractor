using UnityEngine;
using System.Collections;

public class ScreenStartScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
        FloatObjectScript[] floatscripts = GameObject.Find("Wave_Generator_UV _2").GetComponentsInChildren<FloatObjectScript>();
        foreach (FloatObjectScript scripts in floatscripts)
        {
            scripts.gameObject.GetComponent<FloatObjectScript>().enabled = false;
        }
        Time.timeScale = 0;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnMouseDown()
    {
        FloatObjectScript[] floatscripts = GameObject.Find("Wave_Generator_UV _2").GetComponentsInChildren<FloatObjectScript>();
        foreach (FloatObjectScript scripts in floatscripts)
        { 
            scripts.gameObject.GetComponent<FloatObjectScript>().enabled = true;
        }
        Time.timeScale = 1;
        Destroy(this.gameObject);
    }
}
