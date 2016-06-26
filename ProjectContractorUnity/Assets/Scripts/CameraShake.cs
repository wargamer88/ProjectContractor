using UnityEngine;
using System.Collections;

public class CameraShake : MonoBehaviour
{
    #region Variables
    // Transform of the camera to shake. Grabs the gameObject's transform
    // if null.
    private Transform camTransform;

    // How long the object should shake for.
    private float _scriptShakeDuration = 0f;
    [SerializeField]
    private float shakeDuration = 0f;

    // Amplitude of the shake. A larger value shakes the camera harder.
    [SerializeField]
    private float shakeAmount = 0.7f;
    //How fast the shakeDuration lowers
    [SerializeField]
    private float decreaseFactor = 1.0f;

    // Original Position of the Camera
    private Vector3 originalPos;
    #endregion

    /// <summary>
    /// <para> When Awake get the Transform of the Camera</para>
    /// </summary>
    void Awake()
	{
		if (camTransform == null)
		{
			camTransform = GetComponent(typeof(Transform)) as Transform;
		}
	}

    /// <summary>
    /// <para>When enabled get OriginalPos of Camera</para>
    /// </summary>
	void OnEnable()
	{
		originalPos = camTransform.localPosition;
	}

    /// <summary>
    /// <para>Shake the Camera</para>
    /// </summary>
	void Update()
	{
		if (_scriptShakeDuration > 0)
		{
			camTransform.localPosition = originalPos + Random.insideUnitSphere * shakeAmount;

            _scriptShakeDuration -= Time.deltaTime * decreaseFactor;
		}
		else
		{
            _scriptShakeDuration = 0f;
			camTransform.localPosition = originalPos;
		}
	}

    /// <summary>
    /// <para>when Start Shake set the Timer for how long Shake should last</para>
    /// </summary>
    public void StartShake()
    {
        _scriptShakeDuration = shakeDuration;
    }

    public void StopShake()
    {
        _scriptShakeDuration = 0;
    }
}