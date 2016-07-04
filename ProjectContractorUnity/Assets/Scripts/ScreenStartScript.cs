using UnityEngine;
using System.Collections;

public class ScreenStartScript : MonoBehaviour {

    /// <summary>
    /// <para>disabling all the FloatObjectScript in the the parant Wave_Generator</para>
    /// <para>and also to make sure nothing is moving the timeScale is set to 0</para>
    /// </summary>
    void Start () {
        FloatObjectScript[] floatscripts = GameObject.Find("Wave_Generator").GetComponentsInChildren<FloatObjectScript>();
        foreach (FloatObjectScript scripts in floatscripts)
        {
            scripts.gameObject.GetComponent<FloatObjectScript>().enabled = false;
        }
        Time.timeScale = 0;
	}

    /// <summary>
    /// <para>enabling all the FloatObjectScript in the the parant Wave_Generator</para>
    /// <para>and also to make sure nothing is moving the timeScale is set to 1</para>
    /// <para>And at last destroying this GameObject</para>
    /// </summary>
    void OnMouseDown()
    {
        FloatObjectScript[] floatscripts = GameObject.Find("Wave_Generator").GetComponentsInChildren<FloatObjectScript>();
        foreach (FloatObjectScript scripts in floatscripts)
        { 
            scripts.gameObject.GetComponent<FloatObjectScript>().enabled = true;
        }
        FindObjectOfType<PowerupsScript>().GameStarted = true;
        Time.timeScale = 1;
        Destroy(this.gameObject);
    }
}
