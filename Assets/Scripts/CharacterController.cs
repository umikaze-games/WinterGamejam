using System;
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Serialization;


public enum MaterialColor
{
    Black,
    Blue,
    Brown,
    DarkGreen,
    Green,
    Grey,
    Orange,
    Red,
    White,
    Yellow
}

public class CharacterController : MonoBehaviour
{
    // 材质编号
    public MaterialColor materialColor;

    // 新的材质
    public Material[] newMaterials;

    public SkinnedMeshRenderer skinnedMeshRenderer;

    private void Update()
    {
    }

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

    public void OnTriggerEnter(Collider other)
    {
        // 如果tag是Red，就切换成红色材质
        if (other.gameObject.CompareTag("Red"))
        {
            Debug.Log("碰到了红色物体！");
            materialColor = MaterialColor.Red;
            ChangeRendererMaterial();
        }
        
        // 使用switch语句，根据tag切换材质
        switch (other.gameObject.tag)
        {
            case "Red":
                materialColor = MaterialColor.Red;
                ChangeRendererMaterial();
                break;
            case "Green":
                materialColor = MaterialColor.Green;
                ChangeRendererMaterial();
                break;
            case "Blue":
                materialColor = MaterialColor.Blue;
                ChangeRendererMaterial();
                break;
            default:
                break;
        }
    }
}