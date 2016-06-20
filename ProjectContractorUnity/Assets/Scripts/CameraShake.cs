using UnityEngine;
using System.Collections;

public class CameraShake : MonoBehaviour
{
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
    [SerializeField]
    private float decreaseFactor = 1.0f;

	Vector3 originalPos;

	void Awake()
	{
		if (camTransform == null)
		{
			camTransform = GetComponent(typeof(Transform)) as Transform;
		}
	}

	void OnEnable()
	{
		originalPos = camTransform.localPosition;
	}

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

    public void StartShake()
    {
        _scriptShakeDuration = shakeDuration;
    }
}