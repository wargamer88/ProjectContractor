using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class AnimatePlayButton : MonoBehaviour {

    #region Variables
    //Array of Sprites to Animate
    [SerializeField]
    private Sprite[] _sprites;

    //Current image
    private int _index = 0;
    //the IndexCounter
    private float _indexChanger = 0;
    #endregion

    /// <summary>
    /// <para>Call AnimateImage</para>
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
        if (_sprites.Length == 0)
            return;

        _indexChanger += 0.15f;
        _index = (int)_indexChanger;
        if (_index >= _sprites.Length)
        {
            _indexChanger = 0;
            _index = 0;
        }
        GetComponent<Image>().sprite = _sprites[_index];
    }
}
