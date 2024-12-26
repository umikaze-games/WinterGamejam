using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateAndChangeColor : MonoBehaviour
{
    // Rotation speed
    public float rotationSpeed = 100f;

    private void Update()
    {
        // Rotate the object around its Y-axis
        transform.Rotate(Vector3.up, rotationSpeed * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // GameManger的CanUseQ设为true
            GameManager gameManager = FindObjectOfType<GameManager>();
            if (gameManager != null)
            {
                gameManager.canUseQ = true;

                // 该物体消失
                Destroy(gameObject);
            }
        }
    }
}