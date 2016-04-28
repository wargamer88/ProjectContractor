using UnityEngine;
using System.Collections;

public class OnCityMouseClickScript : MonoBehaviour {

    private bool _cityIsClicked = false;
    private Camera _camera;
	// Use this for initialization
	void Start () {
	    _camera = GameObject.FindObjectOfType<Camera>();
    }
	
	// Update is called once per frame
	void Update () {
	    if(_cityIsClicked)
        {
            Vector3 zooming = new Vector3(0, 0, -0.1f);
            //_camera.transform.position = _camera.transform.position - zooming;
            _camera.transform.position = Vector3.MoveTowards(_camera.transform.position, this.transform.position, 0.1f);
            Debug.Log(_camera.transform.position);
            if (_camera.transform.position.z > -5)
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
