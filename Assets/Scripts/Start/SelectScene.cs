using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SelectScene : MonoBehaviour
{
    // 按钮数组
    public Button[] buttons;

    // 场景编号数组
    public int[] sceneIndex;


    // 切换场景
    public void ChangeScene(int index)
    {
        SceneManager.LoadScene(index);
    }
}