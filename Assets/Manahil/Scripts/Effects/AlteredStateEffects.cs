using UnityEngine;

public class AlteredStateEffects : MonoBehaviour
{
    public IntoxicationSystem intoxicationSystem;
    public Transform cameraPivot;

    public float maxTilt = 30f;
    public float swaySpeed = 4f;
    public float swayAmount = 0.2f;
    public float shakeAmount = 0.05f;
    public float shakeSpeed = 10f;

    private Vector3 originalLocalPosition;

    void Start()
    {
        originalLocalPosition = transform.localPosition;
    }

    void Update()
    {
        if (intoxicationSystem == null || cameraPivot == null)
            return;

        float intox = intoxicationSystem.intoxication;

        ApplyTilt(intox);
        ApplySway(intox);
    }

    void ApplyTilt(float intox)
    {
        float tilt = Mathf.Sin(Time.time * swaySpeed) * maxTilt * intox;

        Vector3 currentRotation = cameraPivot.localEulerAngles;
        cameraPivot.localEulerAngles = new Vector3(currentRotation.x, 0f, tilt);
    }

    void ApplySway(float intox)
    {
        float swayX = Mathf.Sin(Time.time * swaySpeed * 1.3f) * swayAmount * intox;
        float swayY = Mathf.Cos(Time.time * swaySpeed * 1.7f) * swayAmount * intox;

        float shakeX = (Random.value - 0.5f) * 2f * shakeAmount * intox;
        float shakeY = (Random.value - 0.5f) * 2f * shakeAmount * intox;

        Vector3 sway = new Vector3(swayX, swayY, 0f);
        Vector3 shake = new Vector3(shakeX, shakeY, 0f);

        transform.localPosition = originalLocalPosition + sway + shake;
    }
}


