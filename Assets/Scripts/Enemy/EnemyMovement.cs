using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [Header("Horizontal Movement")]
    public float moveSpeed = 2f; // 水平移动速度
    public float moveRange = 3f; // 水平移动范围

    [Header("Vertical Oscillation")]
    public float oscillationAmplitude = 0.5f; // 上下波动幅度
    public float oscillationFrequency = 2f; // 上下波动频率

    [Header("Height Constraints")]
    public float minHeight = 0f; // 最低高度

    private Vector3 startPosition;
    private float direction = 1f; // 移动方向

    void Start()
    {
        // 记录初始位置
        startPosition = transform.position;
    }

    void Update()
    {
        // 水平移动
        float horizontalOffset = direction * moveSpeed * Time.deltaTime;
        transform.position += new Vector3(horizontalOffset, 0, 0);

        // 检查水平边界
        if (Mathf.Abs(transform.position.x - startPosition.x) >= moveRange)
        {
            direction *= -1; // 反转方向
        }

        // 垂直波动
        float verticalOffset = Mathf.Sin(Time.time * oscillationFrequency) * oscillationAmplitude;
        float newY = startPosition.y + verticalOffset;

        // 应用最低高度限制
        if (newY < minHeight)
        {
            newY = minHeight;
        }

        transform.position = new Vector3(transform.position.x, newY, transform.position.z);
    }
}