using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class BallGoingThroughWallScript : MonoBehaviour {

    private List<GameObject> _walls;

    public List<GameObject> Walls { set{_walls = value; } }

    // Use this for initialization
    void Start () {
        _walls = GameObject.FindGameObjectsWithTag("LineWall").ToList();
        foreach (GameObject wall in _walls)
        {
            Physics.IgnoreCollision(this.GetComponent<SphereCollider>(), wall.GetComponent<MeshCollider>());
        }
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
