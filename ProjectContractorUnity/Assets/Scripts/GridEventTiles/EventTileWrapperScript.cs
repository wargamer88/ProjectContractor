﻿using UnityEngine;
using System.Collections;

[System.Serializable]
public class EventTileWrapperScript {
    private int _waveListCounter = 0;

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

    public _choices ChosenEvent { get { return _choice; } }
    public int EventWave { get { return _wave; } }
    public bool IsEveryWave { get { return _isEveryWave; } }
    public int EveryXWave { get { return _everyXWave; } }
    public int AmountOfObject { get { return _amountOfObject; } }
    public float SpeedOfObject { get { return _speedOfObject; } }


    public int WaveListCounter { get { return _waveListCounter; } set { _waveListCounter = value; } }


}
