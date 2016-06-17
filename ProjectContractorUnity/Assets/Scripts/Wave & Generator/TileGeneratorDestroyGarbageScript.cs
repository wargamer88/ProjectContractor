using UnityEngine;
using System.Collections;

public class TileGeneratorDestroyGarbageScript : MonoBehaviour {

    private GameObject _damageParticle;
    private GarbadgeGeneratorScript _generatorScript;
    private GarbageWaveScript _garbageWaveScript;
    private NumberParticleScript _numberParticle;

    // Use this for initialization
    void Start () {
        _damageParticle = (GameObject)Resources.Load("Damage");
        _garbageWaveScript = GameObject.FindObjectOfType<GarbageWaveScript>();
        _generatorScript = GameObject.FindObjectOfType<GarbadgeGeneratorScript>();
        _numberParticle = GameObject.FindObjectOfType<NumberParticleScript>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

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
                Instantiate(_damageParticle, pOther.transform.position, Quaternion.identity);
                Destroy(pOther.gameObject);
            }
            else if (pOther.GetComponent<GarbadgeDestoryScript>().GarbageType == GarbageType.Medium)
            {
                _generatorScript.GeneratorHealth = _generatorScript.GeneratorHealth - _generatorScript.Mediumhit;
                _garbageWaveScript.DestroyedGarbage.Add(pOther.gameObject);
                pOther.gameObject.GetComponent<GarbadgeDestoryScript>().CurrentTile.GarbageList.Remove(pOther.gameObject);
                _numberParticle.PlaceParticleAtGenerator(pOther.transform.position, GarbageType.Medium);
                Instantiate(_damageParticle, pOther.transform.position, Quaternion.identity);
                Destroy(pOther.gameObject);
            }
            else if (pOther.GetComponent<GarbadgeDestoryScript>().GarbageType == GarbageType.Heavy)
            {
                _generatorScript.GeneratorHealth = _generatorScript.GeneratorHealth - _generatorScript.HeavyHit;
                _garbageWaveScript.DestroyedGarbage.Add(pOther.gameObject);
                pOther.gameObject.GetComponent<GarbadgeDestoryScript>().CurrentTile.GarbageList.Remove(pOther.gameObject);
                _numberParticle.PlaceParticleAtGenerator(pOther.transform.position, GarbageType.Heavy);
                Instantiate(_damageParticle, pOther.transform.position, Quaternion.identity);
                Destroy(pOther.gameObject);
            }
            else if (pOther.GetComponent<GarbadgeDestoryScript>().GarbageType == GarbageType.Special)
            {
                _generatorScript.GeneratorHealth = _generatorScript.GeneratorHealth - _generatorScript.SuperHeavyHit;
                _garbageWaveScript.DestroyedGarbage.Add(pOther.gameObject);
                pOther.gameObject.GetComponent<GarbadgeDestoryScript>().CurrentTile.GarbageList.Remove(pOther.gameObject);
                _numberParticle.PlaceParticleAtGenerator(pOther.transform.position, GarbageType.Special);
                Instantiate(_damageParticle, pOther.transform.position, Quaternion.identity);
                Destroy(pOther.gameObject);
            }
        }
    }
}
