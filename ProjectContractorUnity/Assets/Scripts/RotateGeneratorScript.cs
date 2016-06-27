using UnityEngine;
using System.Collections;

public class RotateGeneratorScript : MonoBehaviour {

    //Speed of rotation
    [SerializeField]
    private float _rotationSpeed;
    //Array for the children
    private Transform[] _children;
    //Time between starting next generator
    [SerializeField]
    private float _nextRotationTimer = 0.5f;
    //Old time of the generator
    private float _oldRotationTimer;
    /// <summary>
    /// <para>Getting all the children from the genator</para>
    /// </summary>
	void Start () {
        _children = GetComponentsInChildren<Transform>();
	}
	
    /// <summary>
    /// <para>If the game is started. Everytime timer hit. Goes through the children and if there is one with x on 0. Change it and stop the loop</para>
    /// </summary>
	void Update () {
        if (Time.timeScale == 1)
        {
            if (Time.time > (_oldRotationTimer + _nextRotationTimer))
            {
                _oldRotationTimer = Time.time;
                foreach (Transform generator in _children)
                {
                    if (generator.rotation.z == 0 && generator.gameObject != this.gameObject)
                    {
                        generator.transform.eulerAngles = new Vector3(generator.transform.eulerAngles.x, generator.transform.eulerAngles.y, generator.transform.eulerAngles.z + _rotationSpeed);
                        break;
                    }
                }
            }
            foreach (Transform generator in _children)
            {
                if (generator.transform.rotation.z != 0)
                {
                    generator.transform.eulerAngles = new Vector3(generator.transform.eulerAngles.x, generator.transform.eulerAngles.y, generator.transform.eulerAngles.z + _rotationSpeed);
                }
            }
        }
	}
}
