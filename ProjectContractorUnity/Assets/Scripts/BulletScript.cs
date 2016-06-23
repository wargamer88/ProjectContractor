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

    //Reference to Highscore to Reset Combo
    private HighscoreScript _highscore;
    #endregion

    /// <summary>
    /// <para>Find Explosion Particle</para>
    /// <para>Find HighscoreScript</para>
    /// <para>Ignore Collision with all walls and Generator Walls</para>
    /// </summary>
    void Start()
    {
        _explosionPrefab = (GameObject)Resources.Load("Explosion");
        _highscore = GameObject.FindObjectOfType<HighscoreScript>();
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
    /// <para>Calls a function that checks if projectile needs to be destroyed</para>
    /// </summary>
    void Update()
    {
        _checkProjectileHeight();
    }

    /// <summary>
    /// <para>Check if bullet is below certain point, if so remove projectile</para>
    /// </summary>
    private void _checkProjectileHeight()
    {
        if (this.transform.position.y < -4)
        {
            _highscore.ComboCounter = 0;
            Destroy(this.gameObject);
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
