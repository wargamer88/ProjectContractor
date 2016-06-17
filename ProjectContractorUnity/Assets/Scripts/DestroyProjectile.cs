using UnityEngine;
using System.Collections;

public class DestroyProjectile : MonoBehaviour {

    private HighscoreScript _highscore;

    // Use this for initialization
    void Start()
    {
        _highscore = GameObject.FindObjectOfType<HighscoreScript>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "Projectile")
        {
            Destroy(other.gameObject);
            _highscore.ComboCounter = 0;
        }
    }
}
