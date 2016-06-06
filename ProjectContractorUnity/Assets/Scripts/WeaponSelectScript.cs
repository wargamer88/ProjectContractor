using UnityEngine;
using System.Collections;

public class WeaponSelectScript : MonoBehaviour {
    
    private AutoAimScript _AimScript;
    
    // Use this for initialization
    void Start () {
        _AimScript = GameObject.FindObjectOfType<AutoAimScript>();
    }
	
	// Update is called once per frame
	void Update () {
        if (this.gameObject.name == "Ball1")
        {
            if (_AimScript.IsBombCooldown)
            {
                this.gameObject.GetComponent<Renderer>().enabled = false;
            }
            else
            {
                this.gameObject.GetComponent<Renderer>().enabled = true;
            }
        }
        if (this.gameObject.name == "Ball2")
        {
            if (_AimScript.IsDepthCooldown)
            {
                this.gameObject.GetComponent<Renderer>().enabled = false;
            }
            else
            {
                this.gameObject.GetComponent<Renderer>().enabled = true;
            }
        }
        if (this.gameObject.name == "Ball3")
        {
            if (_AimScript.IsFlameCooldown)
            {
                this.gameObject.GetComponent<Renderer>().enabled = false;
            }
            else
            {
                this.gameObject.GetComponent<Renderer>().enabled = true;
            }
        }
    }

    void OnMouseDown()
    {
        if (this.gameObject.name == "Ball1")
        {
            _AimScript.ChosenBall = _AimScript.Balls[0];
        }
        else if (this.gameObject.name == "Ball2")
        {
            _AimScript.ChosenBall = _AimScript.Balls[1];
        }
        else if (this.gameObject.name == "Ball3")
        {
            _AimScript.ChosenBall = _AimScript.Balls[2];
        }
    }
}
