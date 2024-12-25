using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class AdjustSaturation : MonoBehaviour
{
    public Volume volume; // 拖入 Volume 对象
    private ColorAdjustments colorAdjustments;

    void Start()
    {
        if (volume.profile.TryGet(out colorAdjustments))
        {
            Debug.Log("Color Adjustments found!");
        }
        else
        {
            Debug.LogError("No Color Adjustments in Volume profile!");
        }
    }

    // 调整 Saturation 的方法
    [Button]
    public void SetSaturation(float value)
    {
        if (colorAdjustments != null)
        {
            colorAdjustments.saturation.Override(value);
            Debug.Log($"Saturation set to: {value}");
        }
    }
}