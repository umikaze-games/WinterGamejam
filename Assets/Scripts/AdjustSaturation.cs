using System;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class AdjustSaturation : MonoBehaviour
{
    public Volume volume; // 拖入 Volume 对象
    private ColorAdjustments colorAdjustments;

    private void Awake()
    {
        DontDestroyOnLoad(this);
    }

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

    // 如果饱和度为-100，则设置为0，否则设置为-100
    [Button]
    public void ToggleSaturation()
    {
        if (colorAdjustments.saturation.value == -100)
        {
            SetSaturation(0);
        }
        else
        {
            SetSaturation(-100);
        }
    }
}