using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class BulletScript : MonoBehaviour {

    private List<GameObject> _walls;
    private List<GameObject> _generatorwalls;
    private PowerupsScript _powerupsScript;
    private GameObject _explosionPrefab;

    public GameObject ExplosionPrefab { set { _explosionPrefab = value; } }
    public List<GameObject> Walls { set { _walls = value; } }
    public PowerupsScript PowerupsScript { set { _powerupsScript = value; } }

    private GameObject _chosenBall;
    public GameObject ChosenBall { set { _chosenBall = value; } }

    private bool _doesExist = false;

    //ignoring collision with the level walls
    // Use this for initialization
    void Start()
    {
        if (_chosenBall.name != "Ball2(Clone)")
        {
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
        else
        {
            _generatorwalls = GameObject.FindGameObjectsWithTag("GeneratorWall").ToList();
            foreach (GameObject genWall in _generatorwalls)
            {
                Physics.IgnoreCollision(this.GetComponent<SphereCollider>(), genWall.GetComponent<MeshCollider>());
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
       
    }

    public void DestroyBullet()
    {
        Instantiate(_explosionPrefab, this.transform.position, Quaternion.identity);
        Destroy(this.gameObject);
    }

    public void BallPowerDepth(Vector3 pPosition)
    {
        if (_doesExist == false)
        {
            _chosenBall.transform.position = new Vector3(pPosition.x, pPosition.y, pPosition.z);
            _chosenBall.GetComponent<Rigidbody>().velocity = new Vector3(0, 0, 0);
            _chosenBall.GetComponent<Rigidbody>().useGravity = false;
            //_autoAimScript.IsDepthCooldown = true;
            _doesExist = true;
        }
    }

    public void BallPowerFire()
    {
        //Debug.Log("I AM A FIREBARREL");
        if (_doesExist == false)
        {
            Collider[] hitObjects = Physics.OverlapSphere(_chosenBall.transform.position, 30);
            for (int i = 0; i < hitObjects.Length; i++)
            {
                if (hitObjects[i].gameObject.tag == "Garbage")
                {
                    hitObjects[i].gameObject.GetComponent<GarbadgeDestoryScript>().HP -= 1;
                    hitObjects[i].gameObject.GetComponent<GarbadgeDestoryScript>().CheckHealth(this.gameObject);
                }
            }
            Destroy(this.gameObject);
        }
    }
}
