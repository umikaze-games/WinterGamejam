using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Serialization;


public enum MaterialColor
{
    Red,
    Green,
    Blue,
    Yellow,
    Black,
    White
}

public class CharacterController : MonoBehaviour
{
    // 材质编号
    public MaterialColor materialColor;

    // 新的材质
    public Material[] newMaterials;

    public SkinnedMeshRenderer skinnedMeshRenderer;

    // 切换材质的方法
    [Button]
    public void ChangeRendererMaterial()
    {
        if (skinnedMeshRenderer != null)
        {
            // 获取当前的材质数组
            Material[] materials = skinnedMeshRenderer.materials;

            // 替换第一个材质（或其他索引的材质）
            if (materials.Length > 0)
            {
                materials[0] = newMaterials[(int)materialColor];
            }

            // 将修改后的材质数组赋值回去
            skinnedMeshRenderer.materials = materials;
        }
        else
        {
            Debug.LogError("未找到 SkinnedMeshRenderer 组件！");
        }
    }
}