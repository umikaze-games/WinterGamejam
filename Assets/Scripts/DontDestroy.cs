using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DontDestroy : MonoBehaviour
{
    private void Awake()
    {
        // 如果该脚本存在于脚本中，就会被销毁
        DontDestroyOnLoad(this);
    }
}
