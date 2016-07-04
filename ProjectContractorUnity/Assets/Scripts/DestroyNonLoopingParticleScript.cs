using UnityEngine;
using System.Collections;

public class DestroyNonLoopingParticleScript : MonoBehaviour {
	/// <summary>
    /// <para>If animation Stopped destroy Particle</para>
    /// </summary>
	void Update () {
        if (GetComponent<ParticleSystem>().isStopped)
        {
            Destroy(this.gameObject);
        }
    }
}
