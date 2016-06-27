using UnityEngine;
using System.Collections;

public class FloatObjectScript : MonoBehaviour {

    //WaterLevel is where you input the height of the water
    [SerializeField]
    private float waterLevel = 4;
    //Floatheight is how heigh the object should float
    [SerializeField]
    private float floatHeight = 2;
    //bounceDamp is for how big it should bounce
    [SerializeField]
    private float bounceDamp = 0.05f;
    //buoyancyCentreOffset is the offset of the bounciness
    [SerializeField]
    private Vector3 buoyancyCentreOffset;


    private float _forceFactor;
    private Vector3 _actionPoint;
    private Vector3 _uplift;

    private Rigidbody _rigidbody;

	// Use this for initialization
	void Start () {
        _rigidbody = GetComponent<Rigidbody>();
        if (this.tag != "Garbage")
        {
            Physics.IgnoreCollision(this.GetComponent<MeshCollider>(), GameObject.Find("AimPlane").GetComponent<MeshCollider>());
        }
        
	}

    // Update is called once per frame
	
    void FixedUpdate()
    {
        _actionPoint = transform.position + transform.TransformDirection(buoyancyCentreOffset);
        _forceFactor = 1f - ((_actionPoint.y - waterLevel) / floatHeight);

        if (_forceFactor > 0f)
        {
            _uplift = -Physics.gravity * (_forceFactor - _rigidbody.velocity.y * bounceDamp);
            _rigidbody.AddForceAtPosition(_uplift, _actionPoint);
        }
    }
}
