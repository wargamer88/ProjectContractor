using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class SharkyScript : MonoBehaviour {

    #region Variables
    //The speed of Sharky
    [SerializeField]
    private float _speed = 10;

    //this position is set from PowerupsScript, to know which lane it should swim in
    private float _posX;
    
    //Bools for the different states Sharky is in
    private bool _rising = true;
    private bool _floating = false;
    private bool _diving = false;

    //particle variable for the explosion when Sharky destroys the garbage
    private GameObject _explosionParticle;
    
    //List with all the Garbage Sharky should destroy
    private List<GarbadgeDestoryScript> _garbage;
    
    //store GarbageWaveScript which it gets from PowerupsScript
    private GarbageWaveScript _garbageWaveScript;

    //store HighscoreScript which will be loaded in Start
    private HighscoreScript _highscoreScript;

    //properties
    public GarbageWaveScript GarbageWaveScript { get { return _garbageWaveScript; } set { _garbageWaveScript = value; } }
    public List<GarbadgeDestoryScript> Garbage { get { return _garbage; } set { _garbage = value; } }
    public float PosX { get { return _posX; } set { _posX = value; } }
    #endregion

    /// <summary>
    /// <para>load HighscoreScript</para>
    /// <para>load Explosion prefab from resources</para>
    /// </summary>
    void Start ()
    {
        _highscoreScript = FindObjectOfType<HighscoreScript>();
        _explosionParticle = (GameObject)Resources.Load("Explosion");
    }
	
	/// <summary>
    /// <para>First the rising of Sharky is executed</para>
    /// <para>Then the floating of sharky</para>
    /// <para>and then the diving of sharky</para>
    /// </summary>
	void Update () {
        float step = _speed * Time.deltaTime;

        if (_rising)
        {
            transform.position = Vector3.MoveTowards(transform.position, new Vector3(_posX, 2f, -23.1f), step);
            if (transform.position == new Vector3(_posX, 2f, -23.1f))
            {
                _rising = false;
                _floating = true;

                foreach (GarbadgeDestoryScript Garbage in _garbage)
                {
                    if (Garbage == null) continue;
                    Destroy(Garbage.gameObject);
                    _highscoreScript.AddTrashScore(Garbage.GetComponent<GarbadgeDestoryScript>().GarbageType);
                    Instantiate(_explosionParticle, Garbage.transform.position, Quaternion.identity);
                    _garbageWaveScript.DestroyedGarbage.Add(Garbage.gameObject);
                }
            }
        }
        else if (_floating)
        {
            transform.position = Vector3.MoveTowards(transform.position, new Vector3(_posX, 2f, 160), step);
            if (transform.position == new Vector3(_posX, 2f, 160))
            {
                _floating = false;
                _diving = true;
            }
        }
        else if (_diving)
        {
            transform.position = Vector3.MoveTowards(transform.position, new Vector3(_posX, -165, 200), step);
            if (transform.position == new Vector3(_posX, -165, 200))
            {
                Destroy(this.gameObject);
            }
        }
    }
}
