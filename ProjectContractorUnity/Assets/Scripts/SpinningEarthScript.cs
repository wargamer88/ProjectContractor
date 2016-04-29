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
    void OnMouseDown()
    {
        Vector3 pos = Camera.main.WorldToScreenPoint(transform.position);
        pos = Input.mousePosition - pos;
        _baseAngle = Mathf.Atan2(pos.y, pos.x) * Mathf.Rad2Deg;
        _baseAngle -= Mathf.Atan2(transform.right.y, transform.right.x) * Mathf.Rad2Deg;
    }

    void OnMouseDrag()
    {
        Vector3 pos = Camera.main.WorldToScreenPoint(transform.position);
        pos = Input.mousePosition - pos;
        float posX = pos.x;
        float posY = pos.y;
        //transform.Rotate(new Vector3(posX, 0, posY));
        //float ang = Mathf.Atan2(pos.y, pos.x) * Mathf.Rad2Deg - _baseAngle;
        //transform.rotation = Quaternion.AngleAxis(ang,Vector3.right);
        transform.rotation = Quaternion.Euler(new Vector3(0, -posX,0));
    }
}
