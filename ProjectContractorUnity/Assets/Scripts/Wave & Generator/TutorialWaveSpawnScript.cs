using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TutorialWaveSpawnScript : MonoBehaviour {

    private GameObject _chosenGarbage;
    private GameObject _garbageParent;
    private bool _startWave = false;
    private List<int> _spawnXPoint = new List<int>() { -20, -10, 0, 10, 20 };
    private GameObject _aimPlane;

    private int _spawnAmount;
    private GarbageType _garbage;
    private List<GameObject> _light;
    private List<GameObject> _medium;
    private List<GameObject> _heavy;
    private List<GameObject> _special;
    private float _spawnTime;

    private bool _canSpawn = true;


    private List<GameObject> _spawnedGarbage;

    private List<GameObject> _destroyedGarbage;

    public List<GameObject> DestroyedGarbage { get { return _destroyedGarbage; } set { _destroyedGarbage = value; } }

    private int _health;
    private bool _isComplete;

    public bool IsComplete { get {return _isComplete; } set { _isComplete = value; } }
    // Use this for initialization
    void Start ()
    {
        _spawnedGarbage = new List<GameObject>();
        _destroyedGarbage = new List<GameObject>();
        _garbageParent = new GameObject();
        _garbageParent.name = "Garbage Parent";
        _aimPlane = GameObject.Find("AimPlane");
    }
	
	// Update is called once per frame
	void Update () {
        if (_startWave)
        {
            _garbageType();
            if (_canSpawn && _spawnedGarbage.Count < _spawnAmount)
            {
                _garbageSpawn();
            }
            else if (_spawnedGarbage.Count == _destroyedGarbage.Count && _destroyedGarbage.Count == _spawnAmount)
            {
                _isComplete = true;
                _spawnedGarbage = new List<GameObject>();
                _destroyedGarbage = new List<GameObject>();
            }
        }
	}

    public void SpawnTutorial(bool pComplete, int pSpawnAmount = 0, GarbageType pGarbageType = GarbageType.none, List<GameObject> pLight = null, List<GameObject> pMedium = null, List<GameObject> pHeavy = null, List<GameObject> pSpecial = null, float pSpawnTime  = 0)
    {
        _spawnAmount = pSpawnAmount;
        _garbage = pGarbageType;
        _light = pLight;
        _medium = pMedium;
        _heavy = pHeavy;
        _special = pSpecial;
        _spawnTime = pSpawnTime;
        _startWave = true;
        _isComplete = pComplete;
    }
    private void _garbageType()
    {
        switch (_garbage)
        {
            case GarbageType.none:
                Debug.Log("HOW???");
                break;
            case GarbageType.Light:
                _chosenGarbage = _light[Random.Range(0, _light.Count)];
                _health = 1;
                
                break;
            case GarbageType.Medium:
                _chosenGarbage = _medium[Random.Range(0, _medium.Count)];
                _health = 2;
                break;
            case GarbageType.Heavy:
                _chosenGarbage = _heavy[Random.Range(0, _heavy.Count)];
                _health = 3;
                break;
            case GarbageType.Special:
                _chosenGarbage = _special[Random.Range(0, _special.Count)];
                _health = 5;
                break;
            default:
                break;
        }
    }
    private void _garbageSpawn()
    {
        GameObject gameSpawnObject = GameObject.Instantiate(_chosenGarbage, new Vector3(), Quaternion.identity) as GameObject;
        gameSpawnObject.transform.parent = _garbageParent.transform;
        int randomSpawn = 0;

        randomSpawn = Random.Range(0, 5);
        gameSpawnObject.transform.position = new Vector3(_spawnXPoint[randomSpawn], 1, 95);
        Physics.IgnoreCollision(gameSpawnObject.GetComponent<BoxCollider>(), _aimPlane.GetComponent<MeshCollider>());
        gameSpawnObject.tag = "Garbage";
        gameSpawnObject.AddComponent<GarbageMoveScript>();
        gameSpawnObject.AddComponent<Rigidbody>();
        gameSpawnObject.GetComponent<Rigidbody>().useGravity = false;
        gameSpawnObject.GetComponent<Rigidbody>().mass = 1000;
        //float randomRotationX = Random.Range(0, 360);
        //float randomRotationY = Random.Range(0, 360);
        //float randomRotationZ = Random.Range(0, 360);
        //gameSpawnObject.transform.rotation = new Quaternion(0,0,0,0);
        gameSpawnObject.GetComponent<Rigidbody>().constraints = /*.FreezePositionX | RigidbodyConstraints.FreezePositionY | */RigidbodyConstraints.FreezeRotation;
        gameSpawnObject.AddComponent<GarbadgeDestoryScript>();
        gameSpawnObject.GetComponent<GarbadgeDestoryScript>().HP = _health;
        gameSpawnObject.gameObject.name = gameSpawnObject.gameObject.name.Replace("(Clone)", "");
        gameSpawnObject.GetComponent<GarbadgeDestoryScript>().GarbageType = _garbage;
        gameSpawnObject.GetComponent<GarbadgeDestoryScript>().CurrentLane = randomSpawn;
        //if (addSpawnGarbage)
        //{
        //    _spawnedGarbage.Add(gameSpawnObject);
        //}
        _spawnedGarbage.Add(gameSpawnObject);
        gameSpawnObject.GetComponent<GarbadgeDestoryScript>().GarbageType = _garbage;
        _canSpawn = false;
        StartCoroutine(_waitForSeconds());
    }
    private IEnumerator _waitForSeconds()
    {
        yield return new WaitForSeconds(_spawnTime);
        _canSpawn = true;
    }
}
