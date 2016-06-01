using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class TutorialWaveScript {

    [SerializeField]
    private int _amountOf;
    [SerializeField]
    private GarbageType _chosenGarbage;
    [SerializeField]
    private int _timeBetweenSpawn;



    public int AmountOf { get { return _amountOf; } }
    public GarbageType Garbage { get { return _chosenGarbage; }}
    private int TimeBetweenSpawn { get { return _timeBetweenSpawn; } }

}
