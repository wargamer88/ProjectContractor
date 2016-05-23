using UnityEngine;
using System.Collections;

public class PowerupsScript : MonoBehaviour {

    //if reached 3 powerup is spawned(a fish)
    private int powerupCounter = 0;

    private int _lightGarbage = 0;
    private int _mediumGarbage = 0;
    private int _heavyGarbage = 0;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void HitTrash(GarbageType pGarbageType)
    {
        switch (pGarbageType)
        {
            case GarbageType.Light:
                _lightGarbage++;
                break;
            case GarbageType.Medium:
                _mediumGarbage++;
                break;
            case GarbageType.Heavy:
                _heavyGarbage++;
                break;
            default:
                break;
        }

        powerupCounter++;
        Debug.Log(powerupCounter);
    }
    
    public void HitNothing()
    {
        powerupCounter = 0;
        Debug.Log(powerupCounter);
    }
}
