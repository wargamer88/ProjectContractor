using UnityEngine;
using System.Collections;

public class PowerupsScript : MonoBehaviour {

    //if reached 3 powerup is spawned(a fish)
    private int powerupCounter = 0;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void HitTrash()
    {
        powerupCounter++;
        Debug.Log(powerupCounter);
    }
    
    public void HitNothing()
    {
        powerupCounter = 0;
        Debug.Log(powerupCounter);
    }
}
