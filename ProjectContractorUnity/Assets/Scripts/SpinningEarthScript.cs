using UnityEngine;
using System.Collections;

public class SpinningEarthScript : MonoBehaviour {

    private float _baseAngle = 0.0f;

    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    /// <summary>
    /// Code to start the position of globe
    /// </summary>
    void OnMouseDown()
    {
        Vector3 pos = Camera.main.WorldToScreenPoint(transform.position);
        pos = Input.mousePosition - pos;
        _baseAngle = Mathf.Atan2(pos.y, pos.x) * Mathf.Rad2Deg;
        _baseAngle -= Mathf.Atan2(transform.right.y, transform.right.x) * Mathf.Rad2Deg;
    }

    /// <summary>
    /// OnMouseDrag is creating a point and using that point to rotate.
    /// Need start position or _baseAngle from OnMouseDown (still needs to be implemented properly)
    /// </summary>
    void OnMouseDrag()
    {
        Vector3 pos = Camera.main.WorldToScreenPoint(transform.position);
        pos = Input.mousePosition - pos;
        float posX = pos.x;
        float posY = pos.y;
        transform.rotation = Quaternion.Euler(new Vector3(0, -posX,0));
    }
}
