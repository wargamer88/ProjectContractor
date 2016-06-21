using UnityEngine;
using System.Collections;

public class NumberParticleScript : MonoBehaviour {

    #region Variables
    #region ParticleVariables
    //GameObjects with the Particles
    [SerializeField]
    private GameObject _plus3Particle;
    [SerializeField]
    private GameObject _plus5Particle;
    [SerializeField]
    private GameObject _plus7Particle;
    [SerializeField]
    private GameObject _plus10Particle;
    [SerializeField]
    private GameObject _plus15Particle;
    [SerializeField]
    private GameObject _plus20Particle;
    [SerializeField]
    private GameObject _plus30Particle;
    [SerializeField]
    private GameObject _minus10Particle;
    [SerializeField]
    private GameObject _minus20Particle;
    [SerializeField]
    private GameObject _minus30Particle;
    [SerializeField]
    private GameObject _minus50Particle;
    #endregion

    //Location of the Combo Particle
    [SerializeField]
    private Vector3 _locationComboParticle;

    //Location of the Wave Clear Particle
    [SerializeField]
    private Vector3 _locationWaveClearParticle; 
    #endregion


    /// <summary>
    /// <para>Places the right AddScore particle according to which garbage it is</para>
    /// <para>Places it above the destroyed Garbage position</para>
    /// </summary>
    /// <param name="pPosition"></param>
    /// <param name="pGarbageType"></param>
    public void PlaceParticleAtGarbage(Vector3 pPosition, GarbageType pGarbageType)
    {
        pPosition = pPosition + new Vector3(0, 20, 0);
        switch (pGarbageType)
        {
            case GarbageType.Light:
                Instantiate(_plus3Particle, pPosition, Quaternion.identity);
                break;
            case GarbageType.Medium:
                Instantiate(_plus5Particle, pPosition, Quaternion.identity);
                break;
            case GarbageType.Heavy:
                Instantiate(_plus7Particle, pPosition, Quaternion.identity);
                break;
            case GarbageType.Special:
                Instantiate(_plus10Particle, pPosition, Quaternion.identity);
                break;
            default:
                break;
        }
    }

    /// <summary>
    /// <para>Places the lose HP Particle according to the Type of Garbage</para>
    /// <para>Places it above the destroyed Garbage</para>
    /// </summary>
    /// <param name="pPosition"></param>
    /// <param name="pGarbageType"></param>
    public void PlaceParticleAtGenerator(Vector3 pPosition, GarbageType pGarbageType)
    {
        pPosition = pPosition + new Vector3(0, 10, 0);
        switch (pGarbageType)
        {
            case GarbageType.Light:
                Instantiate(_minus10Particle, pPosition, Quaternion.identity);
                break;
            case GarbageType.Medium:
                Instantiate(_minus20Particle, pPosition, Quaternion.identity);
                break;
            case GarbageType.Heavy:
                Instantiate(_minus30Particle, pPosition, Quaternion.identity);
                break;
            case GarbageType.Special:
                Instantiate(_minus50Particle, pPosition, Quaternion.identity);
                break;
            default:
                break;
        }
    }

    /// <summary>
    /// <para>Places the right Combo Particle according to ComboType</para>
    /// <para>Places it on the location put in the Inspector</para>
    /// </summary>
    /// <param name="pComboType"></param>
    public void PlaceParticleCombo(ComboType pComboType)
    {
        switch (pComboType)
        {
            case ComboType.FinishWave:
                Instantiate(_plus10Particle, _locationWaveClearParticle, Quaternion.identity);
                break;
            case ComboType.Untouchable:
                Instantiate(_plus15Particle, _locationComboParticle, Quaternion.identity);
                break;
            case ComboType.ThreeinRow:
                Instantiate(_plus5Particle, _locationComboParticle, Quaternion.identity);
                break;
            case ComboType.FiveinRow:
                Instantiate(_plus10Particle, _locationComboParticle, Quaternion.identity);
                break;
            case ComboType.TeninRow:
                Instantiate(_plus20Particle, _locationComboParticle, Quaternion.identity);
                break;
            case ComboType.FifteeninRow:
                Instantiate(_plus30Particle, _locationComboParticle, Quaternion.identity);
                break;
            default:
                break;
        }
    }
}
