using UnityEngine;

public class ResetPlayerPosition : MonoBehaviour
{
    public Transform spawnPoint; // 指定重置点

    void Start()
    {
        GameObject player = GameObject.FindWithTag("Player");
        if (player != null && spawnPoint != null)
        {
            player.transform.position = spawnPoint.position;
        }

        // 如果当前场景是1~4场景，GameManager脚本中的canUseQ设置为Flase
        if (UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex >= 1 &&
            UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex <= 4)
        {
            GameManager gameManager = FindObjectOfType<GameManager>();
            if (gameManager != null)
            {
                gameManager.canUseQ = false;
            }
        }

        // 如果当前场景是比4场景大的场景，GameManager脚本中的canUseQ设置为True
        if (UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex > 4)
        {
            GameManager gameManager = FindObjectOfType<GameManager>();
            if (gameManager != null)
            {
                gameManager.canUseQ = true;
            }
        }
    }
}