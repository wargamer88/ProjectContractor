using UnityEngine;
using System.Collections;

public class TileGeneratorDestroyGarbageScript : MonoBehaviour {

    #region Variables
    //A reference to the damageParticle for when Garbage hits Generator
    private GameObject _damageParticle;
    //A reference to GarbageGeneratorScript to lower the Health of Generator
    private GarbadgeGeneratorScript _generatorScript;
    //A reference to add Garbage to Destroyed Objects, so Next Wave is Possible
    private GarbageWaveScript _garbageWaveScript;
    //A Reference to NumberParticleScript so that you can place the -Health Particles
    private NumberParticleScript _numberParticle; 
    #endregion

    /// <summary>
    /// <para>Load in Damage Particle from Resources</para>
    /// <para>Find garbageWaveScript, GarbageGeneratorScript and NumberParticleScript</para>
    /// </summary>
    void Start () {
        _damageParticle = (GameObject)Resources.Load("Damage");
        _garbageWaveScript = GameObject.FindObjectOfType<GarbageWaveScript>();
        _generatorScript = GameObject.FindObjectOfType<GarbadgeGeneratorScript>();
        _numberParticle = GameObject.FindObjectOfType<NumberParticleScript>();
	}

    /// <summary>
    /// <para>Check if Garbage hits Generator using Grid</para>
    /// <para>If true, lower HP of Generator with GarbageType, Add to Destroyed List</para>
    /// <para>Place Particle and Destroy Garbage Object</para>
    /// </summary>
    /// <param name="pOther"></param>
    void OnTriggerExit(Collider pOther)
    {
        if (pOther.gameObject.tag == "Garbage")
        {
            if (pOther.GetComponent<GarbadgeDestoryScript>().GarbageType == GarbageType.Light)
            {
                _generatorScript.GeneratorHealth = _generatorScript.GeneratorHealth - _generatorScript.BasicHit;
                _garbageWaveScript.DestroyedGarbage.Add(pOther.gameObject);
                pOther.gameObject.GetComponent<GarbadgeDestoryScript>().CurrentTile.GarbageList.Remove(pOther.gameObject);
                _numberParticle.PlaceParticleAtGenerator(pOther.transform.position, GarbageType.Light);
                Vector3 tempPos = pOther.transform.position;
                tempPos.y = 3;
                Instantiate(_damageParticle, tempPos, Quaternion.identity);
                Destroy(pOther.gameObject);
            }
            else if (pOther.GetComponent<GarbadgeDestoryScript>().GarbageType == GarbageType.Medium)
            {
                _generatorScript.GeneratorHealth = _generatorScript.GeneratorHealth - _generatorScript.Mediumhit;
                _garbageWaveScript.DestroyedGarbage.Add(pOther.gameObject);
                pOther.gameObject.GetComponent<GarbadgeDestoryScript>().CurrentTile.GarbageList.Remove(pOther.gameObject);
                _numberParticle.PlaceParticleAtGenerator(pOther.transform.position, GarbageType.Medium);
                Vector3 tempPos = pOther.transform.position;
                tempPos.y = 3;
                Instantiate(_damageParticle, tempPos, Quaternion.identity);
                Destroy(pOther.gameObject);
            }
            else if (pOther.GetComponent<GarbadgeDestoryScript>().GarbageType == GarbageType.Heavy)
            {
                _generatorScript.GeneratorHealth = _generatorScript.GeneratorHealth - _generatorScript.HeavyHit;
                _garbageWaveScript.DestroyedGarbage.Add(pOther.gameObject);
                pOther.gameObject.GetComponent<GarbadgeDestoryScript>().CurrentTile.GarbageList.Remove(pOther.gameObject);
                _numberParticle.PlaceParticleAtGenerator(pOther.transform.position, GarbageType.Heavy);
                Vector3 tempPos = pOther.transform.position;
                tempPos.y = 3;
                Instantiate(_damageParticle, tempPos, Quaternion.identity);
                Destroy(pOther.gameObject);
            }
            else if (pOther.GetComponent<GarbadgeDestoryScript>().GarbageType == GarbageType.Special)
            {
                _generatorScript.GeneratorHealth = _generatorScript.GeneratorHealth - _generatorScript.SuperHeavyHit;
                _garbageWaveScript.DestroyedGarbage.Add(pOther.gameObject);
                pOther.gameObject.GetComponent<GarbadgeDestoryScript>().CurrentTile.GarbageList.Remove(pOther.gameObject);
                _numberParticle.PlaceParticleAtGenerator(pOther.transform.position, GarbageType.Special);
                Vector3 tempPos = pOther.transform.position;
                tempPos.y = 3;
                Instantiate(_damageParticle, tempPos, Quaternion.identity);
                Destroy(pOther.gameObject);
            }
        }
    }
}
