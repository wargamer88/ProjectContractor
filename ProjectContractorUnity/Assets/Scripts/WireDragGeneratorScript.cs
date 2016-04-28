using UnityEngine;
using System.Collections;

public class WireDragGeneratorScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    /// <summary>
    /// Drag the end of the wire
    /// </summary>
    void OnMouseDrag()
    {
        float distance_to_screen = Camera.main.WorldToScreenPoint(gameObject.transform.position).z;
        transform.position = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, distance_to_screen));

    }

    void OnCollisionEnter(Collision pOther)
    {
        if (!pOther.transform.name.Contains("Bone"))
        {
            if (pOther.transform.name.Contains("Generator"))
            {
                this.gameObject.AddComponent<HingeJoint>();
                this.gameObject.GetComponent<HingeJoint>().useLimits = true;
                Destroy(this.GetComponent<WireDragGeneratorScript>());
            }
        }
    }

}
