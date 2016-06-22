using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class WhaleyScript : MonoBehaviour {

    #region Variables
    //The speed of Sharky
    [SerializeField]
    private float _speed = 10;

    //Bools for the different states Whaley is in
    private bool _rising = true;
    private bool _diving = false;

    //particle variable for the explosion when Sharky destroys the garbage
    private GameObject _damageParticle;

    //List with all the Garbage Sharky should destroy
    private List<GarbadgeDestoryScript> _garbage;

    //store HighscoreScript which will be loaded in Start
    private HighscoreScript _highscoreScript;

    //store GarbageWaveScript which it gets from PowerupsScript
    private GarbageWaveScript _garbageWaveScript;

    //properties
    public GarbageWaveScript GarbageWaveScript { get { return _garbageWaveScript; } set { _garbageWaveScript = value; } }
    public List<GarbadgeDestoryScript> Garbage { get { return _garbage; } set { _garbage = value; } } 
    #endregion

    /// <summary>
    /// <para>load HighscoreScript</para>
    /// <para>load Explosion prefab from resources</para>
    /// </summary>
    void Start ()
    {
        _highscoreScript = FindObjectOfType<HighscoreScript>();
        _damageParticle = (GameObject)Resources.Load("Explosion");
    }

    /// <summary>
    /// <para>First the rising of Whaley is executed</para>
    /// <para>When Whaley is above the water it gives Garbage 1 hp damage and puts them back to their startpoint</para>
    /// <para>if garbage has 0 hp the garbage in is destroyed and ExplosionParticle is instantiated</para>
    /// <para>and then the diving of Whaley is executed</para>
    /// </summary>
    void Update () {
        float step = _speed * Time.deltaTime;
        if (_rising)
        {
            transform.position = Vector3.MoveTowards(transform.position, new Vector3(-0.6f, 3f, -13.8f), step);

            if (transform.position == new Vector3(-0.6f, 3f, -13.8f))
            {
                foreach (GarbadgeDestoryScript Garbage in _garbage)
                {
                    if (Garbage == null) continue;

                    Garbage.HP--;
                    Instantiate(_damageParticle, Garbage.transform.position, Quaternion.identity);
                    Garbage.transform.position = Garbage.OriginalPosition;
                    if (Garbage.HP == 0)
                    {
                        _garbageWaveScript.DestroyedGarbage.Add(Garbage.gameObject);
                        Destroy(Garbage.gameObject);
                        _highscoreScript.AddTrashScore(Garbage.GetComponent<GarbadgeDestoryScript>().GarbageType);
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
