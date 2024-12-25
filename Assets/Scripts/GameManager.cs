using System;
using System.Collections;
using System.Collections.Generic;
using ECM2.Examples.SideScrolling;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    // 是否可以使用Q
    public bool canUseQ = false;
    
    // 保证该脚本切换场景时不被销毁
    private void Awake()
    {
        DontDestroyOnLoad(this);
    }

    // 切换场景方法
    public void ChangeScene(int index)
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(index);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q) && canUseQ)
        {
            FindObjectOfType<AdjustSaturation>().ToggleSaturation();

            // 找到玩家，其身上的SideScrollingCharacter，调整其重力值
            GameObject player = GameObject.FindWithTag("Player");
            if (player != null)
            {
                SideScrollingCharacter character = player.GetComponent<SideScrollingCharacter>();
                if (character != null)
                {
                    // 如果重力Scale大于1.75，则设置为0.5。如果小于0.5，则设置为1.75
                    if (character.gravityScale == 1.75f)
                    {
                        character.gravityScale = 0.9f;
                    }
                    else if (character.gravityScale == 0.9f)
                    {
                        character.gravityScale = 1.75f;
                    }
                }
            }
        }
    }
}