using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Floor : MonoBehaviour
{
    public enum State
    {
        BlackAndWhite,
        Colored
    }

    // 状态和颜色字段
    [SerializeField] public State currentState = State.Colored;

    [SerializeField] public MaterialColor currentColor = MaterialColor.White;
}