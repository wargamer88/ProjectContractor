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

    private bool _cabinDrag;

    public bool CabinDrag
    {
        set { _cabinDrag = value; }
    }
    
    private Vector3 _startPosition;

    public Vector3 StartPosition
    {
        set { _startPosition = value; }
    }

    private Vector3 _position;
    private CraneScript _craneScript;
    private float _mouseX;
    private float _oldMouseX;
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
            if (Input.GetMouseButtonDown(0) || _goingDown)
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

       
        if (_cabinDrag)
        {
            _mouseDragFunction();
    }

    }
    void OnMouseDown()
    {
        _startPosition = Input.mousePosition;
    }

    void OnMouseDrag()
    {
        _mouseDragFunction();
    }

    private void _mouseDragFunction()
    {
        if (Input.mousePosition.x > _startPosition.x)
        {
            transform.parent.position += new Vector3(0, 0, -0.1f);
        }
        if (Input.mousePosition.x < _startPosition.x)
        {
            transform.parent.position += new Vector3(0, 0, 0.1f);
        }
    }

}
