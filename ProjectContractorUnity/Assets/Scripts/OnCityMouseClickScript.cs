using UnityEngine;
using System.Collections;

public class OnCityMouseClickScript : MonoBehaviour {

    private bool _cityIsClicked = false; // if city is clicked
    private Camera _camera; //Camera for the camera position
	// Use this for initialization
	void Start () {
	    _camera = GameObject.FindObjectOfType<Camera>();
    }
	
	// Update is called once per frame
	void Update () {
	    if(_cityIsClicked)
        {
            _camera.transform.position = Vector3.MoveTowards(_camera.transform.position, this.transform.position, 0.1f); // moving to city
            if (_camera.transform.position.z > -5) //load scene if camera is bigger then -5 in the Z axis.
            {
                Application.LoadLevel(1);
            }
        }
	}
    /// <summary>
    /// OnMouseDown is basicly onMouseClick.
    /// When you press a city. A minigame is loaded.
    /// </summary>
    void OnMouseDown()
    {
        _cityIsClicked = true;
    }
}
