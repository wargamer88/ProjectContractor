using UnityEngine;
using System.Collections;

public class RaycastCameraScript : MonoBehaviour
{

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        //raycast for destroying objects
        if (Input.GetMouseButtonDown(0))
        {
            //FindObjectOfType<CraneScript>().gameObject.x = 

            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit))
            {
                if (hit.transform.GetComponentInParent<DestroyableScript>())
                {
                    if (hit.transform.GetComponentInParent<DestroyableScript>().Destroyable)
                    {
                        Destroy(hit.transform.gameObject);
                    }
                }
                Debug.Log("You selected the " + hit.transform.name);
            }
        }
    }
}
