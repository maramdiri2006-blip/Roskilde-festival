using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class VisualEffectsController : MonoBehaviour
{
    public IntoxicationSystem intoxicationSystem;
    public Volume volume;

    private ColorAdjustments colorAdjustments;
    private DepthOfField depthOfField;

    void Start()
    {
        if (volume != null && volume.profile != null)
        {
            volume.profile.TryGet(out colorAdjustments);
            volume.profile.TryGet(out depthOfField);
        }

        if (depthOfField != null)
        {
            depthOfField.mode.value = DepthOfFieldMode.Gaussian;
            depthOfField.gaussianStart.value = 100f;
            depthOfField.gaussianEnd.value = 200f;
            depthOfField.gaussianMaxRadius.value = 0f;
        }
        ApplyColorEffects(0f);
        ApplyBlurEffects(0f);
    }

    void Update()
    {
        if (intoxicationSystem == null || volume == null)
            return;

        float intox = intoxicationSystem.intoxication;

        ApplyColorEffects(intox);
        ApplyBlurEffects(intox);
    }

    void ApplyColorEffects(float intox)
    {
        if (colorAdjustments == null)
            return;

        colorAdjustments.postExposure.value = Mathf.Lerp(0f, -1.5f, intox);
        colorAdjustments.saturation.value = Mathf.Lerp(0f, -60f, intox);
        colorAdjustments.contrast.value = Mathf.Lerp(0f, 40f, intox);
    }

    void ApplyBlurEffects(float intox)
    {
        if (depthOfField == null)
            return;

        depthOfField.mode.value = DepthOfFieldMode.Gaussian;
        depthOfField.gaussianStart.value = Mathf.Lerp(100f, 0.5f, intox);
        depthOfField.gaussianEnd.value = Mathf.Lerp(200f, 3f, intox);
        depthOfField.gaussianMaxRadius.value = intox * 2f;
    }
}
