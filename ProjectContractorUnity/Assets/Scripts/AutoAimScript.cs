using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class AutoAimScript : MonoBehaviour
{
    #region Variables
    //The bullet that gets instantiated
    [SerializeField]
    private GameObject _chosenBullet;

    //The object that is the instantiated Chosenball
    private GameObject _bullet;

    //The offset from the Catapult where the bullet will spawn
    private Vector3 _ballOffset;

    //The Aimplane, the plane that gets raycasted to see where to shoot the bullet at
    private GameObject _aimPlane;

    //The variable where the Hit.Point get saved thats gonna be used for Velocity
    private RaycastHit _hit;

    //Used for timing the next bullet
    private float _oldTime;
    #endregion

    /// <summary>
    /// <para>Finding the AimPlane</para>
    /// <para>Getting the Ball Offset</para>
    /// </summary>
    void Start()
    {
        _aimPlane = GameObject.Find("AimPlane");
        _ballOffset = new Vector3(0, 10.19f, 1.82f);
	}

    /// <summary>
    /// <para>Calling autoAim for shooting</para>
    /// </summary>
    void Update()
    {
        _autoAim();
    }

    /// <summary>
    /// <para>Script where it is checked if you can shoot and where to shoot</para>
    /// </summary>
    private void _autoAim()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray vRay = Camera.main.ScreenPointToRay(Input.mousePosition);
            Physics.Raycast(vRay, out _hit, 10000);
            if (_hit.collider != null)
            {
                if ((_hit.collider.gameObject.name == "AimPlane" || _hit.collider.gameObject.tag == "Garbage" ))
                {
                    if (Time.time > (_oldTime + 0.5f))
                    {
                        _oldTime = Time.time;
                        _createBallAndShootingAnimation();
                    }
                }
            }
            if (GameObject.FindObjectOfType<IdleCloseScript>())
            {
                GameObject.FindObjectOfType<IdleCloseScript>().RestartIdle();
            }
        }
    }

    /// <summary>
    /// <para>Instantiating the Bullet with right Position and Velocity</para>
    /// <para>Playing Animation and Adding required components to script</para>
    /// </summary>
    private void _createBallAndShootingAnimation()
    {
        //Get a Random Rotation for Object
        Quaternion randomRotation = Random.rotation;

        //Make sure to play Shooting Animation, by switching back and forth
        GetComponent<Animator>().Play("Fast_Shoot");

        //Instantiating Bullet
        _bullet = GameObject.Instantiate(_chosenBullet);

        //Adding Position and Random Rotation
        _bullet.transform.position = transform.position + _ballOffset;
        _bullet.transform.rotation = randomRotation;

        //Adding Physics
        _bullet.AddComponent<Rigidbody>();
        _bullet.GetComponent<Rigidbody>().mass = 0.01f;

        //Adding BulletScript, where all the behaviour of the bullet is in
        _bullet.AddComponent<BulletScript>();

        //Make sure Bullet doenst collide with aimPlane.
        Physics.IgnoreCollision(_bullet.GetComponent<SphereCollider>(), _aimPlane.GetComponent<MeshCollider>());

        //Calculate Velocity
        Vector3 velocity = new Vector3(0, 0, 0);
        velocity = _hit.point - _bullet.transform.position;

        //Give bullet the Tag Projectile (used when finding what object it is with collision)
        _bullet.tag = "Projectile";

        //Make the catapult rotate towards the point that the bullet goes to
        transform.LookAt(_hit.point);
        transform.eulerAngles = new Vector3(0, transform.eulerAngles.y, 0);

        //Add the Velocity to the Rigidbody
        _bullet.GetComponent<Rigidbody>().AddForce(velocity);
    }
}
