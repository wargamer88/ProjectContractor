using UnityEngine;
using System.Collections;

public class GarbageHPScript : MonoBehaviour {

    //Health of the current prefab. Script is on the garbage prefab.
    [SerializeField]
    private int _hp;
    //property to read the start health of the object.
    public int HP { get { return _hp; } }

    //damage material
    [SerializeField]
    private Material _damage;

    public Material DamageMaterial { get { return _damage; } }

    //Time to show damage
    [SerializeField]
    private float _showDamageTimer = 1;
    public float ShowDamageTimer { get { return _showDamageTimer; } }
}
