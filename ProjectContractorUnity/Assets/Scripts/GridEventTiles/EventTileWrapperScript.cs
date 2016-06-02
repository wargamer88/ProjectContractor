using UnityEngine;
using System.Collections;

[System.Serializable]
public class EventTileWrapperScript {
    int _choiceIndex = 0;

    [SerializeField]
    private _choices _choice;
    [SerializeField]
    private int _wave;

    public _choices ChosenEvent { get { return _choice; } }
    public int EventWave { get { return _wave; } }
}
