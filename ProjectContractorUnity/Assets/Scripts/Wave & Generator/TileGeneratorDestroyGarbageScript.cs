using UnityEngine;
using System.Collections;

public class TileGeneratorDestroyGarbageScript : MonoBehaviour {

    private GarbadgeGeneratorScript _generatorScript;
    private GarbageWaveScript _garbageWaveScript;

    // Use this for initialization
    void Start () {
        _garbageWaveScript = GameObject.FindObjectOfType<GarbageWaveScript>();
        _generatorScript = GameObject.FindObjectOfType<GarbadgeGeneratorScript>();
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
                Destroy(pOther.gameObject);
            }
            else if (pOther.GetComponent<GarbadgeDestoryScript>().GarbageType == GarbageType.Medium)
            {
                _generatorScript.GeneratorHealth = _generatorScript.GeneratorHealth - _generatorScript.Mediumhit;
                _garbageWaveScript.DestroyedGarbage.Add(pOther.gameObject);
                pOther.gameObject.GetComponent<GarbadgeDestoryScript>().CurrentTile.GarbageList.Remove(pOther.gameObject);
                Destroy(pOther.gameObject);
            }
            else if (pOther.GetComponent<GarbadgeDestoryScript>().GarbageType == GarbageType.Heavy)
            {
                _generatorScript.GeneratorHealth = _generatorScript.GeneratorHealth - _generatorScript.HeavyHit;
                _garbageWaveScript.DestroyedGarbage.Add(pOther.gameObject);
                pOther.gameObject.GetComponent<GarbadgeDestoryScript>().CurrentTile.GarbageList.Remove(pOther.gameObject);
                Destroy(pOther.gameObject);
            }
        }
    }
}
