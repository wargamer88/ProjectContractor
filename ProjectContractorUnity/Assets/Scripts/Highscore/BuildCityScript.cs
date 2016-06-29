using UnityEngine;
using System.Collections;

public class BuildCityScript : MonoBehaviour {

    #region Variables
    //The array of Scaffoldings
    [SerializeField]
    private GameObject[] _scaffoldings;
    private GameObject[] _buildings;
    [SerializeField]
    private float[] _finalBuildingHeights;
    //The final Position of the Scaffolding
    private Vector3 _finalPosition;
    private float _currentFinalHeightBuilding;
    private float _underIsland = -23.3f;

    private bool _scaffoldingSpawned = false;

    private bool _scaffoldingDownTimer = false;

    private bool _moveScaffoldingDown = false;

    private float _scaffoldingTimer = 0;

    private bool _doneBuilding = false;

    public bool DoneBuilding { set { _doneBuilding = value; } }
    //Reference to GarbageWaveScript to get the Wave number
    private GarbageWaveScript _garbageWave;

    private GameObject _currentScaffolding;
    private GameObject _currentBuilding;

    private bool _scaffoldingUp = false;
    private bool _buidlingUp = false;
    #endregion

    // Use this for initialization
    void Start()
    {
        _garbageWave = GameObject.FindObjectOfType<GarbageWaveScript>();
        _buildings = GameObject.FindGameObjectsWithTag("SkyScrapers");
    }

    // Update is called once per frame
    void Update()
    {
        _checkWaveForBuilding();
        _moveUpScaffolding();
        _timerScaffoldingUp();
    }

    private void _moveUpScaffolding()
    {
        if (_scaffoldingSpawned)
        {
            Debug.Log("CurrentBuildingHeight: " + _currentBuilding.transform.position.y);
            Debug.Log("FinalHeight: " + _currentFinalHeightBuilding);
            _currentScaffolding.transform.position = new Vector3(_currentScaffolding.transform.position.x, _currentScaffolding.transform.position.y + 1, _currentScaffolding.transform.position.z);
            _currentBuilding.transform.position = new Vector3(_currentBuilding.transform.position.x, _currentBuilding.transform.position.y + 1f, _currentBuilding.transform.position.z);
            if (_currentScaffolding.transform.position.y >= _finalPosition.y)
            {
                _currentScaffolding.transform.position = _finalPosition;
                _scaffoldingUp = true;
            }
            if (_currentBuilding.transform.position.y <= _currentFinalHeightBuilding)
            {
                _currentBuilding.transform.position = new Vector3(_currentBuilding.transform.position.x, _currentFinalHeightBuilding, _currentBuilding.transform.position.z);
                _buidlingUp = true;
            }
            if (_scaffoldingUp && _buidlingUp)
            {
                _scaffoldingUp = false;
                _buidlingUp = false;
                _scaffoldingSpawned = false;
                _scaffoldingDownTimer = true;
            }
        }
        else if (_moveScaffoldingDown)
        {
            _currentScaffolding.transform.position = new Vector3(_currentScaffolding.transform.position.x, _currentScaffolding.transform.position.y - 1, _currentScaffolding.transform.position.z);
            if (_currentScaffolding.transform.position.y <= _underIsland)
            {
                Destroy(_currentScaffolding);
                _moveScaffoldingDown = false;
            }
        }
    }

    private void _timerScaffoldingUp()
    {
        if (_scaffoldingDownTimer)
        {
            _scaffoldingTimer++;

            if (_scaffoldingTimer >= 80)
            {
                _moveScaffoldingDown = true;
                _scaffoldingTimer = 0;
                _scaffoldingDownTimer = false;
            }
        }
    }

    private void _checkWaveForBuilding()
    {
        if (_garbageWave.Wave == 5 && !_doneBuilding)
        {
            _currentScaffolding = Instantiate(_scaffoldings[0], new Vector3(_scaffoldings[0].transform.position.x, _underIsland, _scaffoldings[0].transform.position.z), _scaffoldings[0].transform.rotation) as GameObject;
            _currentBuilding = _buildings[0];
            _currentFinalHeightBuilding = _finalBuildingHeights[0];
            _finalPosition = _scaffoldings[0].transform.position;
            _scaffoldingSpawned = true;
            _doneBuilding = true;
        }
        else if(_garbageWave.Wave == 7 && !_doneBuilding)
        {
            _currentScaffolding = Instantiate(_scaffoldings[1], new Vector3(_scaffoldings[1].transform.position.x, _underIsland, _scaffoldings[1].transform.position.z), _scaffoldings[2].transform.rotation) as GameObject;
            _currentBuilding = _buildings[1];
            _currentFinalHeightBuilding = _finalBuildingHeights[1];
            _finalPosition = _scaffoldings[1].transform.position;
            _scaffoldingSpawned = true;
            _doneBuilding = true;
        }
        else if (_garbageWave.Wave == 10 && !_doneBuilding)
        {
            _currentScaffolding = Instantiate(_scaffoldings[2], new Vector3(_scaffoldings[2].transform.position.x, _underIsland, _scaffoldings[2].transform.position.z), _scaffoldings[2].transform.rotation) as GameObject;
            _currentBuilding = _buildings[2];
            _currentFinalHeightBuilding = _finalBuildingHeights[2];
            _finalPosition = _scaffoldings[2].transform.position;
            _scaffoldingSpawned = true;
            _doneBuilding = true;
        }
        else if (_garbageWave.Wave == 12 && !_doneBuilding)
        {
            _currentScaffolding = Instantiate(_scaffoldings[3], new Vector3(_scaffoldings[3].transform.position.x, _underIsland, _scaffoldings[3].transform.position.z), _scaffoldings[3].transform.rotation) as GameObject;
            _currentBuilding = _buildings[3];
            _currentFinalHeightBuilding = _finalBuildingHeights[3];
            _finalPosition = _scaffoldings[3].transform.position;
            _scaffoldingSpawned = true;
            _doneBuilding = true;
        }
    }
}
