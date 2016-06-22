using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GarbadgeDestoryScript : MonoBehaviour {

    #region Variables
    //field and property for the garbage type 
    private GarbageType _garbageType;
    public GarbageType GarbageType { get { return _garbageType; } set { _garbageType = value; } }

    //The lane where the garbage is spawning
    private string _currentLane;
    public string CurrentLane { get { return _currentLane; } set { _currentLane = value; } }
    public GarbageTileScript CurrentTile { get { return _currentTile; } set { _currentTile = value; } }
    //Health of the spawning object
    private float _hp;
    public float HP { get { return _hp; } set { _hp = value; } }
    //field of original position for objects
    private Vector3 _originalPosition;
    public Vector3 OriginalPosition { get { return _originalPosition; } }
    //Current tile of object
    private GarbageTileScript _currentTile;
    //field to put in the object that fill be filled at start for garbageWave
    private GarbageWaveScript _garbageWaveScript;
    //field for the highscore object
    private HighscoreScript _highscore;
    //field for particle object
    private NumberParticleScript _numberParticle; 
    # endregion


    /// <summary>
    /// <para>Set original position of object</para>
    /// <para>If no HP in GarbageHPScript then use default</para>
    /// <para>search for objects</para>
    /// </summary>
    void Start () {
        _originalPosition = this.gameObject.transform.position;
        if (HP == 0 && GetComponent<GarbageHPScript>())
        {
            _hp = GetComponent<GarbageHPScript>().HP;
        }
        _garbageWaveScript = GameObject.FindObjectOfType<GarbageWaveScript>();
        _highscore = GameObject.FindObjectOfType<HighscoreScript>();
        _numberParticle = GameObject.FindObjectOfType<NumberParticleScript>();
	}
    /// <summary>
    /// <para>Unity function event when something is hitting</para>
    /// <para>If the hit is a projectile it call destory bullet and call method damageGarbage in current tile</para>
    /// </summary>
    /// <param name="pOther">Other object that is hitting</param>
    void OnCollisionEnter(Collision pOther)
    {
        if (pOther.transform.tag == "Projectile")
        {
            if (_currentTile != null)
            {
                _currentTile.DamageGarbage(pOther.collider);
            }
            pOther.gameObject.GetComponent<BulletScript>().DestroyBullet();
        }
    }

    /// <summary>
    /// <para>Check the health of the garbage and if the health is zero or lower call particle, add the score, add to destroyed list and destroy the bullet and garbage</para>
    /// </summary>
    /// <param name="pOther">Can be a projecttile but it is something that can hit the garbage</param>
    public void CheckHealth(GameObject pOther)
    {
        if (_hp <= 0)
        {
            _numberParticle.PlaceParticleAtGarbage(this.transform.position, _garbageType);
            _highscore.AddTrashScore(_garbageType);
            pOther.GetComponent<BulletScript>().DestroyBullet();
            _garbageWaveScript.DestroyedGarbage.Add(pOther);
            Destroy(this.gameObject);
        }
    }

}
