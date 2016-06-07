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
    [SerializeField]
    //private List<TutorialWaveScript> _tutorialWaveList;



    public int AmountOf { get { return _amountOf; } }
    public GarbageType Garbage { get { return _chosenGarbage; }}
    public int TimeBetweenSpawn { get { return _timeBetweenSpawn; } }
    //public List<TutorialWaveScript> TutorialWaveList { get { return _tutorialWaveList; } }

}
