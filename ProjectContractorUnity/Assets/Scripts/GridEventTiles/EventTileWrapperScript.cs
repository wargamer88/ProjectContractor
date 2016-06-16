using UnityEngine;
using System.Collections;

[System.Serializable]
public class EventTileWrapperScript {

    private int _indexNumber = 0;
    public int IndexNumber { get { return _indexNumber; } set { _indexNumber = value; } }

    [SerializeField]
    private _choices _choice;
    [SerializeField]
    private int _wave;
    [SerializeField]
    private bool _isEveryWave;
    [SerializeField]
    private int _everyXWave;
    [SerializeField]
    private int _amountOfObject;
    [SerializeField]
    private float _speedOfObject;
    [SerializeField]
    private float _timeBetweenSpawn;

    private string _tile;

    public _choices ChosenEvent { get { return _choice; } }
    public int EventWave { get { return _wave; } }
    public bool IsEveryWave { get { return _isEveryWave; } }
    public int EveryXWave { get { return _everyXWave; } }
    public int AmountOfObject { get { return _amountOfObject; } }
    public float SpeedOfObject { get { return _speedOfObject; } }
    public float TimeBetweenSpawn { get { return _timeBetweenSpawn; } }

    public string Tile { get { return _tile; } set { _tile = value; } }


}
