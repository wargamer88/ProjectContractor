using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class ChompyScript : MonoBehaviour {

    [SerializeField]
    private float _speed = 10;

    private GameObject _damageParticle;

    private List<GameObject> _walls;

    private GameObject _garbageObject;
    private GarbageWaveScript _garbageWaveScript;
    private HighscoreScript _highscoreScript;
    public GameObject GarbageObject { get { return _garbageObject; } set { _garbageObject = value; } }
    public GarbageWaveScript GarbageWaveScript { get { return _garbageWaveScript; } set { _garbageWaveScript = value; } }

    // Use this for initialization
    void Start ()
    {
        _highscoreScript = FindObjectOfType<HighscoreScript>();
        _damageParticle = (GameObject)Resources.Load("Explosion");

        //this.transform.rotation = new Quaternion(0, -1, 0, 1);
        this.GetComponent<BoxCollider>().enabled = false;
    }
	
	// Update is called once per frame
	void Update () {

        

        if (_garbageObject == null)
        {
            Destroy(this.gameObject);
        }
        float step = _speed * Time.deltaTime;
        Vector3 tempPos = _garbageObject.transform.position;
        tempPos.y = 3;
        transform.position = Vector3.MoveTowards(transform.position, tempPos, step);

        if ((this.transform.position - tempPos).magnitude < 10)
        {
            //this.GetComponent<BoxCollider>().enabled = true;
            DestroyGarbageObject();
        }
    }

    void DestroyGarbageObject()
    {
        if (_garbageObject.gameObject == _garbageObject)
        {
            Destroy(this.gameObject);
            _garbageWaveScript.DestroyedGarbage.Add(_garbageObject);
            _highscoreScript.AddTrashScore(_garbageObject.GetComponent<GarbadgeDestoryScript>().GarbageType);
            Destroy(_garbageObject);
            Instantiate(_damageParticle, _garbageObject.transform.position, Quaternion.identity);
        }
        else
        {
            //if (pOther.gameObject.tag != "SpecialWeapon" || pOther.gameObject.tag != "Projectile")
            //{
            //    Physics.IgnoreCollision(this.GetComponent<BoxCollider>(), pOther.gameObject.GetComponent<BoxCollider>()); 
            //}
            //else 
            //{
            //    Physics.IgnoreCollision(this.GetComponent<BoxCollider>(), pOther.gameObject.GetComponent<SphereCollider>());
            //}
        }
    }
}
