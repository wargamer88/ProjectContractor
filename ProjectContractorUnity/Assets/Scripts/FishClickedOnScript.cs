using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class FishClickedOnScript : MonoBehaviour {
    
    #region Variables
    //PowerupsScript got from PowerupsScript
    private PowerupsScript _powerupsScript;

    //LineWall list for IgnoreCollision
    private List<GameObject> _walls;

    //Bool for when the fish is clicked on and has to go diving
    private bool FishClickedOn = false;

    //properties
    public PowerupsScript PowerupsScript { set { _powerupsScript = value; } } 
    #endregion

    /// <summary>
    /// <para>Making sure the IgnoreCollision are all resolved</para>
    /// </summary>
    void Start () {
        _walls = GameObject.FindGameObjectsWithTag("LineWall").ToList();
        foreach (GameObject wall in _walls)
        {
            Physics.IgnoreCollision(this.GetComponent<BoxCollider>(), wall.GetComponent<MeshCollider>());
        }
        Physics.IgnoreCollision(this.GetComponent<Collider>(), GameObject.Find("AimPlane").GetComponent<Collider>());
    }
	
	/// <summary>
    /// <para>making sure when this gameobject becomes beneath Y:-20 it will be destroyed</para>
    /// </summary>
	void Update () {
        transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.LookRotation(GetComponent<Rigidbody>().velocity, Vector3.up), Time.deltaTime * 100f);
        if (this.transform.position.y < -20)
        {
            if (this.gameObject.name == "Chompy" && FishClickedOn)
            {
                _powerupsScript.FishClickedOn(GarbageType.Light);
            }
            if (this.gameObject.name == "Sharky" && FishClickedOn)
            {
                _powerupsScript.FishClickedOn(GarbageType.Medium);
            }
            if (this.gameObject.name == "Whaley" && FishClickedOn)
            {
                _powerupsScript.FishClickedOn(GarbageType.Heavy);
            }
            Destroy(this.gameObject);
        }

        if (FishClickedOn)
        {
            GetComponent<Rigidbody>().AddForce(new Vector3(0, -27, 0));
        }
    }

    /// <summary>
    /// <para>when clicked on the fish call PowerupsScript.FishClickedOn with the corresponding GarbageType</para>
    /// </summary>
    void OnMouseDown()
    {
        if (this.gameObject.name == "Chompy")
        {
            FishClickedOn = true;
        }
        if (this.gameObject.name == "Sharky")
        {
            FishClickedOn = true;
        }
        if (this.gameObject.name == "Whaley")
        {
            FishClickedOn = true;
        }
    }
}
