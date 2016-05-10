using UnityEngine;
using System.Collections;

public class ChainScript : MonoBehaviour {

    private bool _canMove = true;

    public bool CanMove
    {
        get { return _canMove = true; }
        set { _canMove = value; }
    }

    private bool _goingDown = false;

    public bool GoingDown
    {
        get { return _goingDown = true; }
        set { _goingDown = value; }
    }

    private bool _goingUp = false;
    public bool GoingUp
    {
        get { return _goingUp = true; }
        set { _goingUp = value; }
    }
    CraneScript _craneScript;
    // Use this for initialization
    void Start () {
        _craneScript = GameObject.FindObjectOfType<CraneScript>();

	}
	
	// Update is called once per frame
	void Update () {

        if (Input.GetKey(KeyCode.W) || _goingUp)
        {
            transform.position += new Vector3(0, 0.5f, 0);
            if (transform.position.y > 29)
            {
                _goingUp = false;
                _craneScript.CanMove = true;
            }
        }

        if (_canMove)
        {
            if (Input.GetKey(KeyCode.S) || _goingDown)
            {
                _goingDown = true;
                transform.position += new Vector3(0, -0.5f, 0);
                if(transform.position.y < 8)
                {
                    _goingUp = true;
                    _goingDown = false;
                }
            }
        }
        if (_goingDown)
        {
            _craneScript.CanMove = false;
        }

       
    }
}
