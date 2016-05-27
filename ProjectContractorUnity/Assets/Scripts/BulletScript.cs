using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class BulletScript : MonoBehaviour {

    private List<GameObject> _walls;
    private PowerupsScript _powerupsScript;

    public List<GameObject> Walls { set { _walls = value; } }
    public PowerupsScript PowerupsScript { set { _powerupsScript = value; } }

    private GameObject _chosenBall;
    public GameObject ChosenBall { set { _chosenBall = value; } }

    private bool _doesExist = false;

    //ignoring collision with the level walls
    // Use this for initialization
    void Start()
    {
        _walls = GameObject.FindGameObjectsWithTag("LineWall").ToList();
        foreach (GameObject wall in _walls)
        {
            Physics.IgnoreCollision(this.GetComponent<SphereCollider>(), wall.GetComponent<MeshCollider>());
        }
    }

    // Update is called once per frame
    void Update()
    {
       
    }

    void OnCollisionEnter(Collision pOther)
    {
        if (pOther.gameObject.GetComponent<FloorScript>())
        {
            DestroyBullet(false);
        }
        if (pOther.gameObject.name == "AimPlane")
        {
            if (_chosenBall.name == "Ball2(Clone)")
            {
                Debug.Log("Depth Hit Plane");
                _ballPowerDepth(pOther.contacts[0].point);
            }
            else if (_chosenBall.name == "Ball3(Clone)")
            {
                _ballPowerFire(pOther.contacts[0].point);
            }
        }
    }

    public void DestroyBullet(bool pHitTrash, GarbageType pGarbageType = GarbageType.Light)
    {
        if (pHitTrash)
        {
            _powerupsScript.HitTrash(pGarbageType);
        }
        else
        {
            _powerupsScript.HitNothing();
        }
        Destroy(this.gameObject);
    }

    private void _ballPowerDepth(Vector3 pPosition)
    {
        if (_doesExist == false)
        {
            _chosenBall.transform.position = new Vector3(pPosition.x, pPosition.y + 0.5f, pPosition.z);
            _chosenBall.GetComponent<Rigidbody>().velocity = new Vector3(0, 0, 0);
            _chosenBall.GetComponent<Rigidbody>().useGravity = false;
            //_autoAimScript.IsDepthCooldown = true;
            _doesExist = true;
        }
    }

    private void _ballPowerFire(Vector3 pPosition)
    {
        if (_doesExist == false)
        {
            _chosenBall.transform.position = new Vector3(pPosition.x, pPosition.y + 0.5f, pPosition.z);
            _chosenBall.GetComponent<Rigidbody>().velocity = new Vector3(0, 0, 0);
            _chosenBall.GetComponent<Rigidbody>().useGravity = false;

        }
    }


}
