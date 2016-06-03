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

    public List<EventTileWrapperScript> EventWrapper { get { return _eventWrapper; } }

    public string Tile { get { return this.gameObject.name; } }

    //string[] _choices = new string[] {"test","test2" };
    //List<string> _choices = new List<string>() {"None","Increase Speed", "Spawn Bottle", "Show Tutorial Bottle", "Spawn Barrel", "Explodes Barrel" };

    //int _choiceIndex = 0;

    //[SerializeField]
    //private _choices _choice;
    //[SerializeField]
    //private int _wave;

    //public _choices ChosenEvent { get { return _choice; } }
    //public int EventWave { get { return _wave; } }

    // public List<string> Choices { get { return _choices; } }

    //public List<string> events = new List<string>();
    //public override void OnInspectorGUI()
    //{
    //    // Draw the default inspector
    //    DrawDefaultInspector();
    //    _choiceIndex = EditorGUILayout.Popup(_choiceIndex, _choices.ToArray());
    //    var someClass = target as EventTileScript;
    //    // Update the selected choice in the underlying object
    //    someClass._choice = _choices[_choiceIndex];
    //    // Save the changes back to the object
    //    EditorUtility.SetDirty(target);
    //}
    //void OnGUI()
    //{
    //    // Custom inspector code goes here
    //    EditorUtility.SetDirty(target);
    //}
}
