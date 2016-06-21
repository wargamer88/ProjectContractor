using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class BulletScript : MonoBehaviour {

    #region Variables
    //The particle for Explosion
    private GameObject _explosionPrefab;

    //List of all the walls in the game
    private List<GameObject> _walls;

    //List of the Generator Walls
    private List<GameObject> _generatorwalls;  
    #endregion

    /// <summary>
    /// <para>Find Explosion Particle</para>
    /// <para>Ignore Collision with all walls and Generator Walls</para>
    /// </summary>
    void Start()
    {
        _explosionPrefab = (GameObject)Resources.Load("Explosion");
        _walls = GameObject.FindGameObjectsWithTag("LineWall").ToList();
        foreach (GameObject wall in _walls)
        {
            Physics.IgnoreCollision(this.GetComponent<SphereCollider>(), wall.GetComponent<MeshCollider>());
        }
        _generatorwalls = GameObject.FindGameObjectsWithTag("GeneratorWall").ToList();
        foreach (GameObject genWall in _generatorwalls)
        {
            Physics.IgnoreCollision(this.GetComponent<SphereCollider>(), genWall.GetComponent<MeshCollider>());
        }
    }

    /// <summary>
    /// <para>Instantiate Explosion Prefab on the spot when the Bullets hits something</para>
    /// <para>Destroy gameobject Bullet</para>
    /// </summary>
    public void DestroyBullet()
    {
        Instantiate(_explosionPrefab, this.transform.position, Quaternion.identity);
        Destroy(this.gameObject);
    }
}
