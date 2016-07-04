using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

/// <summary>
/// <para>GarbageType is enum that can be used to indicate the object.</para>
/// </summary>
public enum GarbageType
{
    none,
    Light,
    Medium,
    Heavy,
    Special,
}

public class GarbageWaveScript : MonoBehaviour
{
    #region variables
    //Garbage list that is filled from the inspector
    [SerializeField]
    private List<GameObject> _lightGarbage;
    [SerializeField]
    private List<GameObject> _mediumGarbage;
    [SerializeField]
    private List<GameObject> _heavyGarbage;
    [SerializeField]
    private List<GameObject> _specialGarbage;
    //Properties so you can check if the object is in the list
    public List<GameObject> LightGarbage { get { return _lightGarbage; } }
    public List<GameObject> MediumGarbage { get { return _mediumGarbage; } }
    public List<GameObject> HeavyGarbage { get { return _heavyGarbage; } }
    public List<GameObject> SpecialGarbage { get { return _specialGarbage; } }
    //Amount of object that needs to be spawned in a wave.
    private int _spawnAmount = 0;
    public int SpawnAmount { get { return _spawnAmount; } set { _spawnAmount = value; } }
    //true or false when wave is done
    private bool _nextWave = false;
    //List of spawned objects
    private List<GameObject> _spawnedGarbage;
    //List of destroyed garbage and a property so other script can also add the object if it will be destroyed
    private List<GameObject> _destroyedGarbage;
    public List<GameObject> DestroyedGarbage { get { return _destroyedGarbage; } set { _destroyedGarbage = value; } }
    //Field for the garbage type
    private GarbageType _garbageType;
    //An empty object to putt all the garbage in
    private GameObject _garbageParent;
    //Wave number and a property to look at which wave the game is
    private int _waveNumber = 0;
    public int Wave { get { return _waveNumber; } }
    //getting all the garbage generator scripts in the game
    private GarbadgeGeneratorScript[] _generators;
    //set if the generator is hit to true
    private bool _someGeneratorGotHit = false;
    //variable to put in the highscore script in
    private HighscoreScript _highScoreScript;
    //boolean if the garabge can spawn so the while can continue
    private bool _canContinue = false;
    //Dead lane list contains A,B,C,D and/or E if the generators are destoryed on that lane.
    private List<string> _deadLaneList;
    public List<string> DeadLaneList { get { return _deadLaneList; } set { _deadLaneList = value; } }
    //counter to reset when a place is found to place the object in the tile
    private int _renewCounter = 0;
    //Get the script that looks if all the generators are still alive
    private GeneratorPowerScript _generatorPowerScript;
    //Boolean for if the Game has Ended
    private bool _endGame = false;
    //Property for EndGame used in GeneratorPowerScript
    public bool EndGame { set { _endGame = value; } }
    //Script to make sure New building can spawn
    private BuildCityScript _cityConstruction;

    private bool _startWave = true;
    public bool StartWave { get { return _startWave; } set { _startWave = value; } }
    #endregion
    /// <summary>
    ///<para>Start create the lists and searching for objects in the game</para>
    /// </summary>
    void Start()
    {
        _deadLaneList = new List<string>();
        _highScoreScript = GameObject.FindObjectOfType<HighscoreScript>();
        _spawnedGarbage = new List<GameObject>();
        _destroyedGarbage = new List<GameObject>();
        _garbageParent = new GameObject();
        _garbageParent.name = "Garbage Parent";
        _generators = GameObject.FindObjectsOfType<GarbadgeGeneratorScript>();
        _generatorPowerScript = GameObject.FindObjectOfType<GeneratorPowerScript>();
        _cityConstruction = GameObject.FindObjectOfType<BuildCityScript>();
    }

    /// <summary>
    /// <para>Update checked if the amount of spawned and destroyed the same is and destroyed and should spawn (spawn amount) the same is</para>
    /// <para>When is true update wave number and reset the ammount for the wave</para>
    /// </summary>
    void Update()
    {
        if (_spawnedGarbage.Count <= _destroyedGarbage.Count && (_destroyedGarbage.Count >= _spawnAmount || _destroyedGarbage.Count >= _spawnedGarbage.Count))
        {
            //Check if there is still garbage left. Hotfix 2.0
            if (GameObject.FindGameObjectsWithTag("Garbage").Length  == 0 && !_endGame && _startWave)
            {
                _waveNumber++;
                _cityConstruction.DoneBuilding = false;
                _nextWave = true;
                _spawnAmount = 0;
                _startWave = false;
                if (_waveNumber != 1)
                {
                    _checkWave();
                }
            }
            
        }
        if (_nextWave)
        {
            this.GetComponent<WaveCanvasScript>().ChangeWaveNumber(_waveNumber);

            _spawnedGarbage = new List<GameObject>();
            _destroyedGarbage = new List<GameObject>();
            _nextWave = false;

        }
    }

    /// <summary>
    /// Check if something hit the generator in the wave.
    /// </summary>
    private void _checkWave()
    {
        for (int i = 0; i < _generators.Length; i++)
        {
            if (_generators[i].GeneratorGotHit)
            {

                _someGeneratorGotHit = true;
            }
        }
        _highScoreScript.WaveClear(_someGeneratorGotHit);
        for (int i = 0; i < _generators.Length; i++)
        {
            _generators[i].GeneratorGotHit = false;
        }
        _someGeneratorGotHit = false;
    }

    /// <summary>
    /// <para>Spawn the garbage for the wave.</para>
    /// </summary>
    /// <param name="pHealth">Health for the garbage if nothing is in HP script on the prefab</param>
    /// <param name="pX">X position of the spawning garbage</param>
    /// <param name="pY">Y position of the spawning garbage</param>
    /// <param name="pZ">Z position of the spawning garbage</param>
    /// <param name="pGarbage">Object that needs to be spawned</param>
    /// <param name="pSpeed">Speed of the object. If not filled it's -1</param>
    /// <param name="pTile">Tile name of the tile the object spawns in</param>
    public void SpawnGarbage(float pHealth, float pX, float pY, float pZ, GameObject pGarbage, float pSpeed = -1, string pTile = "")
    {
        if (_lightGarbage.Contains(pGarbage))
        {
            _garbageType = GarbageType.Light;
        }
        else if (_mediumGarbage.Contains(pGarbage))
        {
            _garbageType = GarbageType.Medium;
        }
        else if (_heavyGarbage.Contains(pGarbage))
        {
            _garbageType = GarbageType.Heavy;
        }
        else if (_specialGarbage.Contains(pGarbage))
        {
            _garbageType = GarbageType.Special;
        }
        GameObject garbage = pGarbage;
        GameObject gameSpawnObject = GameObject.Instantiate(garbage, new Vector3(), new Quaternion(garbage.transform.eulerAngles.x, garbage.transform.eulerAngles.y, garbage.transform.eulerAngles.z, 1)) as GameObject;
        gameSpawnObject.transform.parent = _garbageParent.transform;
        pY = garbage.transform.position.y;
        gameSpawnObject.tag = "Garbage"; //Set tag of spawning object
        if (pX != 0)
        {
            if (gameSpawnObject.name == "Log(Clone)")
            {
                gameSpawnObject.transform.position = new Vector3(pX, 2.5f, pZ);
            }
            else if (gameSpawnObject.name == "TV(Clone)")
            {
                gameSpawnObject.transform.eulerAngles = new Vector3(0, 180, 0);
                gameSpawnObject.transform.position = new Vector3(pX, 0f, pZ);
            }
            else
            {
                gameSpawnObject.transform.position = new Vector3(pX, pY, pZ);
            }
        }
        #region Overlapping of objects check
        //Change position if garbage overlap
        while (!_canContinue)
        {
            Collider[] allColliders = Physics.OverlapSphere(gameSpawnObject.transform.position, 5);
            foreach (Collider collider in allColliders)
            {
                _renewCounter++;
                if (collider.tag == "Garbage" && collider.gameObject != gameSpawnObject)
                {
                    int randomPos = Random.Range(0, 4);
                    if (randomPos == 0)
                    {
                        gameSpawnObject.transform.position = new Vector3(gameSpawnObject.transform.position.x + 1.5f, gameSpawnObject.transform.position.y, gameSpawnObject.transform.position.z);
                        _canContinue = false;
                        _renewCounter = 0;
                        break;
                    }
                    else if (randomPos == 1)
                    {
                        gameSpawnObject.transform.position = new Vector3(gameSpawnObject.transform.position.x - 1.5f, gameSpawnObject.transform.position.y, gameSpawnObject.transform.position.z);
                        _canContinue = false;
                        _renewCounter = 0;
                        break;
                    }
                    else if (randomPos == 2)
                    {
                        gameSpawnObject.transform.position = new Vector3(gameSpawnObject.transform.position.x, gameSpawnObject.transform.position.y, gameSpawnObject.transform.position.z + 1.5f);
                        _canContinue = false;
                        _renewCounter = 0;
                        break;
                    }
                    else if (randomPos == 3)
                    {

                        gameSpawnObject.transform.position = new Vector3(gameSpawnObject.transform.position.x, gameSpawnObject.transform.position.y, gameSpawnObject.transform.position.z - 1.5f);
                        _canContinue = false;
                        _renewCounter = 0;
                        break;
                    }
                    else
                    {
                        _canContinue = false;
                    }
                }
                if (_renewCounter == allColliders.Length)
                {
                    _canContinue = true;
                    _renewCounter = 0;
                }
            }
        }
        _canContinue = false;
        #endregion

        //change rotation except for the object beneath
        gameSpawnObject.gameObject.name = gameSpawnObject.gameObject.name.Replace("(Clone)", "");
        if (gameSpawnObject.name != "HippyVan" && gameSpawnObject.name != "Plastic_Bottle" && gameSpawnObject.name != "Log")
        {
            float randomRotationY = Random.Range(0, 360);
            gameSpawnObject.transform.rotation = Quaternion.Euler(new Vector3(0, randomRotationY, 0));
        }
        else if (gameSpawnObject.name == "Plastic_Bottle" && gameSpawnObject.name != "Log")
        {
            float randomRotationZ = Random.Range(-45, 45);
            gameSpawnObject.transform.rotation = Quaternion.Euler(new Vector3(0, 0, randomRotationZ));
        }

        #region Adding and changing components 
        //Adding and changing components to the spawning objects
        gameSpawnObject.AddComponent<GarbageMoveScript>();
        gameSpawnObject.AddComponent<Rigidbody>();
        gameSpawnObject.GetComponent<Rigidbody>().useGravity = false;
        gameSpawnObject.GetComponent<Rigidbody>().mass = 1000;

        gameSpawnObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezePositionY | RigidbodyConstraints.FreezeRotation;
        gameSpawnObject.AddComponent<GarbadgeDestoryScript>();

        gameSpawnObject.GetComponent<GarbadgeDestoryScript>().GarbageType = _garbageType;
        gameSpawnObject.GetComponent<GarbadgeDestoryScript>().CurrentLane = pTile.Substring(0, 1);
        gameSpawnObject.AddComponent<ExecuteEventScript>();
        gameSpawnObject.GetComponent<GarbageMoveScript>().Speed = pSpeed;
        #endregion
        //Add to the list
        _spawnedGarbage.Add(gameSpawnObject);
    }
}