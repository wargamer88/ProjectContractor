using UnityEngine;
using System.Collections;

public class FlickingScript : MonoBehaviour {

    private bool isDragging = false;
    private Plane dragPlane;
    private Vector3 moveTo;
 
    float dragDamper = 50.0f;
    float addToY = 5.0f;
 
    void Update()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        float dist;

        if (Input.GetMouseButtonDown(0))
        {
            if (Physics.Raycast(ray, out hit))
            {
                if (hit.transform.root.transform == transform)
                {
                    isDragging = true;
                    GetComponent<Rigidbody>().useGravity = false;

                    // defined drag plane:
                    //dragPlane = new Plane(-ray.direction.normalized, hit.point);
                    dragPlane = new Plane(Vector3.up, transform.position + Vector3.up * addToY);
                }
            }
        }

        if (isDragging)
        {
            bool hasHit = dragPlane.Raycast(ray, out dist);

            if (hasHit)
            {
                moveTo = ray.GetPoint(dist);

            }
        }



        if (Input.GetMouseButtonUp(0) && isDragging)
        {

            isDragging = false;
            GetComponent<Rigidbody>().useGravity = true;

        }
    }

    void FixedUpdate()
    {

        if (!isDragging) return;

        var velocity = (moveTo - transform.position);
        GetComponent<Rigidbody>().velocity = Vector3.Lerp(GetComponent<Rigidbody>().velocity, velocity, dragDamper * Time.deltaTime);

    }
}
