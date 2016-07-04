using UnityEngine;
using System.Collections;

public class DestroyProjectile : MonoBehaviour {

    #region Variables
    //a Reference to HighScoreScript for ComboCounter
    private HighscoreScript _highscore; 
    #endregion

    /// <summary>
    /// <para>find the _highscore Script</para>
    /// </summary>
    void Start()
    {
        _highscore = GameObject.FindObjectOfType<HighscoreScript>();
    }

    /// <summary>
    /// <para>if Projectile makes collision delete object and reset Combo</para>
    /// </summary>
    /// <param name="other"></param>
    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "Projectile")
        {
            Destroy(other.gameObject);
            _highscore.ComboCounter = 0;
        }
    }
}
