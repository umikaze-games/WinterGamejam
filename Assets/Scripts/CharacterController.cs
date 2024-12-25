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

        // 当该物体图层为"Floor"时，执行方法
        if (other.gameObject.layer == LayerMask.NameToLayer("Floor"))
        {
            Debug.Log("碰到了地板！");

            // 得到该物体上的Floor脚本
            Floor floor = other.GetComponent<Floor>();

            // 如果玩家的颜色和地板的颜色不一样，暂停游戏
            if (floor != null && floor.currentState == Floor.State.Colored && floor.currentColor != materialColor)
            {
                Die();
            }
        }
    }

    // 玩家死亡执行办法
    public void Die()
    {
        Debug.Log("玩家死亡！");
        // 时间归零
        Time.timeScale = 0;
    }
}