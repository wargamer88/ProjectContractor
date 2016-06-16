using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class SharkyScript : MonoBehaviour {

    [SerializeField]
    private float _speed = 10;
    private float _posX;

    private bool _rising = true;
    private bool _floating = false;
    private bool _diving = false;

    private List<GarbadgeDestoryScript> _garbage;

    private GameObject _garbageObject;
    private GarbageWaveScript _garbageWaveScript;
    public GameObject GarbageObject { get { return _garbageObject; } set { _garbageObject = value; } }
    public GarbageWaveScript GarbageWaveScript { get { return _garbageWaveScript; } set { _garbageWaveScript = value; } }
    public List<GarbadgeDestoryScript> Garbage { get { return _garbage; } set { _garbage = value; } }
    public float PosX { get { return _posX; } set { _posX = value; } }

    // Use this for initialization
    void Start () {
    }
	
	// Update is called once per frame
	void Update () {
        float step = _speed * Time.deltaTime;
        if (_rising)
        {
            transform.position = Vector3.MoveTowards(transform.position, new Vector3(_posX, 0.37f, -23.1f), step);
            if (transform.position == new Vector3(_posX, 0.37f, -23.1f))
            {
                _rising = false;
                _floating = true;

                foreach (GarbadgeDestoryScript Garbage in _garbage)
                {
                    if (Garbage == null) continue;
                    Destroy(Garbage.gameObject);
                    _garbageWaveScript.DestroyedGarbage.Add(Garbage.gameObject);
                }
            }
        }
        else if (_floating)
        {
            transform.position = Vector3.MoveTowards(transform.position, new Vector3(_posX, 0.37f, 85), step);
            if (transform.position == new Vector3(_posX, 0.37f, 85))
            {
                _floating = false;
                _diving = true;
            }
        }
        else if (_diving)
        {
            transform.position = Vector3.MoveTowards(transform.position, new Vector3(_posX, -165, 158), step);
            if (transform.position == new Vector3(_posX, -165, 158))
            {
                Destroy(this.gameObject);
            }
        }
    }

    void OnCollisionEnter(Collision pOther)
    {
        if (_garbage.Contains(pOther.gameObject.GetComponent<GarbadgeDestoryScript>()))
        {
            _garbageWaveScript.DestroyedGarbage.Add(pOther.gameObject);
            Destroy(pOther.gameObject);
        }
    }
}
