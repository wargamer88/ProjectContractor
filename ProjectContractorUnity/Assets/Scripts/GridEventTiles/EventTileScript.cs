using UnityEngine;
using System.Collections;
//using UnityEditor;
using System.Collections.Generic;
using System;

//[CustomEditor(typeof(EventTileScript))]
public enum _choices
{
    None, IncreaseSpeed, SpawnBottle, ShowTutorialBottle, SpawnBarrel, ExplodesBarrel
}
[System.Serializable]
public class EventTileScript : MonoBehaviour {

    [SerializeField]
    private List<EventTileWrapperScript> _eventWrapper;

    public List<EventTileWrapperScript> EventWrapper { get { return _eventWrapper; }set { _eventWrapper = value; } }

    public string Tile { get { return this.gameObject.name; } }
    
}
