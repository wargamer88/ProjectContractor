using UnityEngine;
using System.Collections;

public class Destroyable : MonoBehaviour {

    [SerializeField]
    private bool _destroyable = true;

    public bool DestroyableGO { get { return _destroyable; } }
}
