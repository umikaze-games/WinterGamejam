using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
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
        if (Input.GetKeyDown(KeyCode.Q))
        {
            FindObjectOfType<AdjustSaturation>().ToggleSaturation();
        }
    }
    
}