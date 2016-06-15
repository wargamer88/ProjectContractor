using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class WhaleyScript : MonoBehaviour {

    [SerializeField]
    private float _speed = 10;
    private bool _rising = true;
    private bool _diving = false;

    private List<GameObject> _walls;
    private List<GarbadgeDestoryScript> _garbage;

    private GameObject _garbageObject;
    private GarbageWaveScript _garbageWaveScript;
    public GameObject GarbageObject { get { return _garbageObject; } set { _garbageObject = value; } }
    public GarbageWaveScript GarbageWaveScript { get { return _garbageWaveScript; } set { _garbageWaveScript = value; } }
    public List<GarbadgeDestoryScript> Garbage { get { return _garbage; } set { _garbage = value; } }

    // Use this for initialization
    void Start () {

    }
	
	// Update is called once per frame
	void Update () {
        float step = _speed * Time.deltaTime;
        if (_rising)
        {
            transform.position = Vector3.MoveTowards(transform.position, new Vector3(-0.6f, 2f, -13.8f), step);

            if (transform.position == new Vector3(-0.6f, 2f, -13.8f))
            {
                foreach (GarbadgeDestoryScript Garbage in _garbage)
                {
                    if (Garbage == null) continue;

                    Garbage.HP--;
                    Garbage.transform.position = Garbage.OriginalPosition;
                    if (Garbage.HP == 0)
                    {
                        _garbageWaveScript.DestroyedGarbage.Add(Garbage.gameObject);
                        Destroy(Garbage.gameObject);
                    }
                }
                _rising = false;
                _diving = true;

            }
        }
        else if (_diving)
        {
            transform.position = Vector3.MoveTowards(transform.position, new Vector3(-1, -165, 158), step);
            if (transform.position == new Vector3(-1, -165, 158))
            {
                Destroy(this.gameObject);
            }
        }





    }
}
