using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class AnimatePlayButton : MonoBehaviour {

    #region Variables
    //Array of materials to Animate
    [SerializeField]
    private Material[] _materials;

    //Current image
    private int _index = 0;
    //the IndexCounter
    private float _indexChanger = 0;

    private System.DateTime _oldTime;
    #endregion

    void Start()
    {
        _oldTime = System.DateTime.UtcNow;
    }

    /// <summary>
    /// <para>Call AnimateQuad</para>
    /// </summary>
    void Update()
    {
        _animateImage();
    }

    /// <summary>
    /// <para>ShowAnimation of PlayButton</para>
    /// </summary>
    private void _animateImage()
    {
        if (_materials.Length == 0)
            return;

        if (System.DateTime.UtcNow >= (_oldTime.AddMilliseconds(70)))
        {
            _oldTime = System.DateTime.UtcNow;
            _indexChanger += 1f;
        }
        _index = (int)_indexChanger;
        if (_index >= _materials.Length)
        {
            _indexChanger = 0;
            _index = 0;
        }
        GetComponent<MeshRenderer>().material = _materials[_index];
    }
}
