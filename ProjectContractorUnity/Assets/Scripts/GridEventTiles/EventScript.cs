using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;

[CustomEditor(typeof(EventScript))]
public class EventScript : Editor {

    public List<string> _choices = new List<string>();
    //public string test;
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();
        if (GUI.changed)
        {
            Debug.Log(_choices.Count);
        }
        string text = EditorGUILayout.TextField("");
        var choice = target as EventScript;
        if (Event.current.keyCode == KeyCode.Return)
        {
            Debug.Log(text);
        }
        EditorUtility.SetDirty(choice);
    }

        //public enum Events
        //{

        //}
        //[SerializeField]
        //private List<EventScript> _eventList = new List<EventScript>();

        //private string _eventName;
        //Use this for initialization
        //void Start () {

        //}

        //Update is called once per frame
        //void Update()
        //{
        //    if (_eventList.Count > 0)
        //    {

        //    }
        //}

        //private void _addEventToEnum()
        //{
        //}
    }
