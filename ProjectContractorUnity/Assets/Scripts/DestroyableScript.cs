using UnityEngine;
using System.Collections;

public class DestroyableScript : MonoBehaviour
{

    [SerializeField]
    private bool _destroyable = true;

    public bool Destroyable { get { return _destroyable; } }
}


