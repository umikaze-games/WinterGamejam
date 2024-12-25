using System;
using System.Collections;
using System.Collections.Generic;
using ECM2.Examples.SideScrolling;
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

    // 爆炸特效
    public GameObject explosionPrefab;

    // 提示可以按下Ctrl的UI
    public GameObject crouchHint;

    public void Awake()
    {
        DontDestroyOnLoad(this);
    }

    private void Update()
    {
        // 如果transform的position.y小于-20，则死亡
        if (transform.position.y < -20)
        {
            Die();
        }
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
            case "Yellow":
                materialColor = MaterialColor.Yellow;
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

        // 当玩家碰到切换场景的门时，切换到当前场景编号+1的场景
        if (other.gameObject.tag == "SwitchScene")
        {
            FindObjectOfType<GameManager>()
                .ChangeScene(UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex + 1);

            // 重置角色状态
            ResetCharacter();
        }

        // 如果tag是CanUseCtrl，则显示提示UI
        if (other.gameObject.tag == "CanUseCtrl")
        {
            crouchHint.SetActive(true);
            SideScrollingCharacter character = GetComponent<SideScrollingCharacter>();
            character.canCrouch = true;
        }
    }

    // 玩家死亡执行办法
    public void Die()
    {
        Debug.Log("玩家死亡！");

        // 开启爆炸特效
        explosionPrefab.SetActive(true);

        // 禁止角色控制
        SideScrollingCharacter character = GetComponent<SideScrollingCharacter>();
        character.canControl = false;

        // 关闭角色的碰撞体
        Collider collider = GetComponent<Collider>();
        if (collider != null)
        {
            collider.enabled = false;
        }

        //角色消失
        skinnedMeshRenderer.enabled = false;

        // 2秒后重置角色状态
        Invoke("ResetCharacter", 2.1f);
        // 2秒后重新加载当前场景
        Invoke("ReloadScene", 2f);
    }

    // 切换场景时，重置角色状态
    public void ResetCharacter()
    {
        SideScrollingCharacter character = GetComponent<SideScrollingCharacter>();
        character.canControl = true;

        // 关闭角色的碰撞体
        Collider collider = GetComponent<Collider>();
        if (collider != null)
        {
            collider.enabled = true;
        }

        skinnedMeshRenderer.enabled = true;

        // 关闭爆炸特效
        explosionPrefab.SetActive(false);

        materialColor = MaterialColor.Yellow;
        ChangeRendererMaterial();

        Time.timeScale = 1;
    }

    // 重新加载当前场景
    public void ReloadScene()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(UnityEngine.SceneManagement.SceneManager.GetActiveScene()
            .buildIndex);
    }
}