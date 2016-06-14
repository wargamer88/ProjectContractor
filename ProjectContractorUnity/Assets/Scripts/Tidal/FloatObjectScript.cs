using UnityEngine;
using System.Collections;

public class FloatObjectScript : MonoBehaviour {

    [SerializeField]
    private float waterLevel = 4;
    [SerializeField]
    private float floatHeight = 2;
    [SerializeField]
    private float bounceDamp = 0.05f;
    [SerializeField]
    private Vector3 buoyancyCentreOffset;


    private float _forceFactor;
    private Vector3 _actionPoint;
    private Vector3 _uplift;

    private Rigidbody _rigidbody;

	// Use this for initialization
	void Start () {
        _rigidbody = GetComponent<Rigidbody>();
        Physics.IgnoreCollision(this.GetComponent<MeshCollider>(), GameObject.Find("AimPlane").GetComponent<MeshCollider>());
	}

    // Update is called once per frame
	
    void Update()
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
