using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class ChompyScript : MonoBehaviour {

    #region variables
    //Set the speed of Chompy
    [SerializeField]
    private float _speed = 10;

    //particle variable for the explosion when Chompy hits the garbage
    private GameObject _explosionParticle;

    //Garbage object to destroy by chompy
    private GameObject _garbageObject;
    
    //store GarbageWaveScript which it gets from PowerupsScript
    private GarbageWaveScript _garbageWaveScript;

    //store HighscoreScript which will be loaded in Start
    private HighscoreScript _highscoreScript;

    //properties
    public GameObject GarbageObject { get { return _garbageObject; } set { _garbageObject = value; } }
    public GarbageWaveScript GarbageWaveScript { get { return _garbageWaveScript; } set { _garbageWaveScript = value; } }
    #endregion

    /// <summary>
    /// <para>load HighscoreScript</para>
    /// <para>load Explosion prefab from resources</para>
    /// <para>disable the BoxCollider if this script is active</para>
    /// </summary>
    void Start ()
    {
        _highscoreScript = FindObjectOfType<HighscoreScript>();
        _explosionParticle = (GameObject)Resources.Load("Explosion");

        this.GetComponent<BoxCollider>().enabled = false;
    }

    /// <summary>
    /// <para>Movement of Chompy(Y pos is fixed)</para>
    /// <para>Destroying of Chompy and Garbage</para>
    /// </summary>
    void Update () {
        
        //movement
        if (_garbageObject == null)
        {
            Destroy(this.gameObject);
        }
        float step = _speed * Time.deltaTime;
        Vector3 tempPos = _garbageObject.transform.position;
        tempPos.y = 3;
        transform.position = Vector3.MoveTowards(transform.position, tempPos, step);
        
        //destroying
        if ((this.transform.position - tempPos).magnitude < 10)
        {
            _destroyGarbageObject();
        }
    }

    /// <summary>
    /// <para>only be called if Chompy and trash have to be destroyed</para>
    /// <para>adding garbage to GarbageWaveScript.DestroyedGarbage for equal spawned and destroyed garbage</para>
    /// <para>adding garbage type to HighscoreScript.AddTrashScore for score updating</para>
    /// <para>when Chompy and Garbage are destroyed instantiate the ExplosionParticle</para>
    /// </summary>
    private void _destroyGarbageObject()
    {
        _garbageWaveScript.DestroyedGarbage.Add(_garbageObject);
        _highscoreScript.AddTrashScore(_garbageObject.GetComponent<GarbadgeDestoryScript>().GarbageType);
        Destroy(this.gameObject);
        Destroy(_garbageObject);
        Instantiate(_explosionParticle, _garbageObject.transform.position, Quaternion.identity);
    }
}
