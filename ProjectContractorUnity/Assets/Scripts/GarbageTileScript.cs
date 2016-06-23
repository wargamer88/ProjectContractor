using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GarbageTileScript : MonoBehaviour {

    #region Variables
    //A list of Garbage, to see which Garbage are in the Tile
    private List<GameObject> _garbageList;
    //A List of Garbage that can be removed from GarbageList, because its null
    private List<GameObject> _tobeDestroyedList;
    //A Reference to HighScoreScript for Score
    private HighscoreScript _highscore; 

    //Property for GarbageList
    public List<GameObject> GarbageList { get { return _garbageList; } set { _garbageList = value; } }
    #endregion

    /// <summary>
    /// <para>_garbageList get Initialized</para>
    /// <para>finding HighscoreScript</para>
    /// </summary>
    void Start()
    {
        _garbageList = new List<GameObject>();
        _highscore = GameObject.FindObjectOfType<HighscoreScript>();
    }

    /// <summary>
    /// <para>If Garbage Hits Tile add to List</para>
    /// <para>If Projectile hits Tile Damage Garbage and Destroy Particle</para>
    /// </summary>
    /// <param name="pOther"></param>
    void OnTriggerEnter(Collider pOther)
    {
        if (pOther.gameObject.tag == "Garbage")
        {
            _garbageList.Add(pOther.gameObject);
            pOther.gameObject.GetComponent<GarbadgeDestoryScript>().CurrentTile = this;
        }
        else if (pOther.gameObject.tag == "Projectile")
        {
            if (_garbageList.Count > 0)
            {
                DamageGarbage(pOther);
            }
            else
            {
                _highscore.ComboCounter = 0;
            }
            Destroy(pOther.gameObject);
        }
    }

    /// <summary>
    /// <para>If Garbage leaves tile, remove from List</para>
    /// </summary>
    /// <param name="pOther"></param>
    void OnTriggerExit(Collider pOther)
    {
        if (pOther.gameObject.tag == "Garbage")
        {
            _garbageList.Remove(pOther.gameObject);
        }
    }

    /// <summary>
    /// <para>Damage all the Garbage and increase Combo</para>
    /// </summary>
    /// <param name="pOther"></param>
    public void DamageGarbage(Collider pOther)
    {
        _tobeDestroyedList = new List<GameObject>();
        for (int i = 0; i < _garbageList.Count; i++)
        {
            if (_garbageList[i] != null)
            {
                //Lower Garbage Health
                _garbageList[i].GetComponent<GarbadgeDestoryScript>().HP--;
                //Check if HP <= 0 then add to toBeDestroyedList and increase Combo
                if (_garbageList[i].GetComponent<GarbadgeDestoryScript>().HP <= 0)
                {
                    _tobeDestroyedList.Add(_garbageList[i]);
                    _highscore.ComboCounter += 1;
                    _highscore.ComboCheck();
                }
                //Check Health of Garbage in GarbageDestroyScript
                _garbageList[i].GetComponent<GarbadgeDestoryScript>().CheckHealth(pOther.gameObject);
            }
            else
            {
                //if current Garbage is already NULL then add to ToBeDestroyedList
                _tobeDestroyedList.Add(_garbageList[i]);
            }
        }
        //Remove all the Empty GarbageObjects from GarbageList
        for (int i = 0; i < _tobeDestroyedList.Count; i++)
        {
            _garbageList.Remove(_tobeDestroyedList[i]);
        }
    }
}
