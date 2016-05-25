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
    

    void OnCollionEnter(Collision pOther)
    {
        if (pOther.gameObject.name == "Aimplane")
        {
            if (_chosenBall.name == "Ball2")
            {
                Debug.Log(pOther.contacts[0]);
                _ballPowerDepth(pOther.contacts[0].point);
            }
            else if (_chosenBall.name == "Ball3")
            {
                _ballPowerFire();
            }
        }
    }

    private void _ballPowerDepth(Vector3 pPosition)
    {
        _chosenBall.transform.position = pPosition;
    }

    private void _ballPowerFire()
    {

    }


}
