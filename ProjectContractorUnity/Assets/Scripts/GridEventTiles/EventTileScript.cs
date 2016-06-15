using UnityEngine;
using System.Collections;
//using UnityEditor;
using System.Collections.Generic;
using System;

//[CustomEditor(typeof(EventTileScript))]
[System.Serializable]
public enum _choices
{
    None, IncreaseSpeed, SpawnBottle, ShowTutorialBottle, SpawnBarrel, ExplodesBarrel,
    SpawnRandomLight,
    SpawnRandomMedium,
    SpawnRandomHeavy,
    SpawnSuperHeavy,
    BoatEvent,
    BoatToTile,
    ChangeLanes,
    MiltiTrash,
}
[System.Serializable]
public class EventTileScript {

    //[SerializeField]
    private List<EventTileWrapperScript> _eventWrapper;

    public List<EventTileWrapperScript> EventWrapper { get { return _eventWrapper; } set { _eventWrapper = value; } }

    void Start()
    {
        foreach (EventTileWrapperScript tile in _eventWrapper)
        {
            //tile.Tile = this.gameObject.name;
        }
    }

    //public string Tile { get { return this.gameObject.name; } }

}
