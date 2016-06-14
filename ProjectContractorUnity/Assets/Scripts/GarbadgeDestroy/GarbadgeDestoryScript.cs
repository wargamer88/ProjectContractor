using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GarbadgeDestoryScript : MonoBehaviour {

    private float _hp;
    [SerializeField]
    private GarbageType _garbageType;
    private int _currentLane;
    public float HP { get { return _hp; } set { _hp = value; } }

    private GarbageTileScript _currentTile;

    public int CurrentLane { get { return _currentLane; } set { _currentLane = value; } }

    public GarbageTileScript CurrentTile { get { return _currentTile; } set { _currentTile = value; } }

    private GarbageWaveScript _garbageWaveScript;
    private HighscoreScript _highscore;

    public GarbageType GarbageType { get { return _garbageType; } set { _garbageType = value; } }

                                       // Use this for initialization
    void Start () {
        if (HP == 0)
        {
            _hp = GetComponent<GarbageHPScript>().HP;
        }
        _garbageWaveScript = GameObject.FindObjectOfType<GarbageWaveScript>();
        _highscore = GameObject.FindObjectOfType<HighscoreScript>();
	}
	
	// Update is called once per frame
	void Update () {
        if (_hp == 3)
        {
            this.GetComponent<Renderer>().material.color = Color.green;
        }
        if (_hp == 2)
        {
            this.GetComponent<Renderer>().material.color = Color.yellow;
        }
        if (_hp == 1)
        {
            this.GetComponent<Renderer>().material.color = Color.red;
        }
    }

    void OnCollisionEnter(Collision pOther)
    {
        if (pOther.transform.tag == "Projectile")
        {
            if (_currentTile != null)
            {
                _currentTile.DamageGarbage(pOther.collider);
            }

            Destroy(pOther.gameObject);
        }
        if (pOther.transform.tag == "SpecialWeapon")
        {
            _hp = _hp - 3;
            Destroy(pOther.gameObject);
            CheckHealth(pOther.gameObject);
        }
        else if (pOther.transform.tag == "FireBarrel")
        {
            pOther.gameObject.GetComponent<BulletScript>().BallPowerFire();
        }
    }


    public void CheckHealth(GameObject pOther)
    {
        if (_hp <= 0)
        {
            if (_garbageWaveScript.TutorialWavesLeft > 0)
            {
                pOther.GetComponent<BulletScript>().DestroyBullet(true, _garbageType);
                _garbageWaveScript.GetComponent<TutorialWaveSpawnScript>().DestroyedGarbage.Add(pOther);
                Destroy(this.gameObject);
            }
            else
            {
                pOther.GetComponent<BulletScript>().DestroyBullet(true, _garbageType);
                _garbageWaveScript.DestroyedGarbage.Add(pOther);
                Destroy(this.gameObject);
            }
            _highscore.AddTrashScore(_garbageType);
            _highscore.ComboCounter += 1;
            _highscore.ComboCheck();
        }
    }

}
