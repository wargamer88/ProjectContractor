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
    #endregion

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

        _indexChanger += 1f;
        _index = (int)_indexChanger;
        if (_index >= _materials.Length)
        {
            _indexChanger = 0;
            _index = 0;
        }
        GetComponent<MeshRenderer>().material = _materials[_index];
    }
}
