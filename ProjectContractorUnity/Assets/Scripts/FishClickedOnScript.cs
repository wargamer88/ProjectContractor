using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class FishClickedOnScript : MonoBehaviour {
    
    #region Variables
    //PowerupsScript got from PowerupsScript
    private PowerupsScript _powerupsScript;

    //LineWall list for IgnoreCollision
    private List<GameObject> _walls;

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
        if (this.transform.position.y < -20)
        {
            Destroy(this.gameObject);
        }
	}

    /// <summary>
    /// <para>when clicked on the fish call PowerupsScript.FishClickedOn with the corresponding GarbageType</para>
    /// </summary>
    void OnMouseDown()
    {
        if (this.gameObject.name == "Chompy")
        {
            _powerupsScript.FishClickedOn(GarbageType.Light);
            Destroy(this.gameObject);
        }
        if (this.gameObject.name == "Sharky")
        {
            _powerupsScript.FishClickedOn(GarbageType.Medium);
            Destroy(this.gameObject);
        }
        if (this.gameObject.name == "Whaley")
        {
            _powerupsScript.FishClickedOn(GarbageType.Heavy);
            Destroy(this.gameObject);
        }
    }
}
