using UnityEngine;
using System.Collections;

public class CameraControls : MonoBehaviour {

	// Use this for initialization
	void Start () {
	    
	}
	
	// Update is called once per frame
	void Update () {
        

        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit))
            {
                if (hit.transform.GetComponentInParent<Destroyable>().DestroyableGO)
                {
                    Destroy(hit.transform.gameObject);
                }
                Debug.Log("You selected the " + hit.transform.name);
            }

                //Instantiate(particle, transform.position, transform.rotation);
        }
    }
}
