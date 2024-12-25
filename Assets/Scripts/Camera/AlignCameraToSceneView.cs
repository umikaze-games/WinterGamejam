using System;
using UnityEditor;
using UnityEngine;

public class AlignCameraToSceneView : MonoBehaviour
{
    private void Awake()
    {
        DontDestroyOnLoad(this);
    }

    [MenuItem("Tools/Align Camera to Scene View")]
    private static void AlignCamera()
    {
        if (Camera.main != null)
        {
            // 获取当前场景视图的 Transform
            SceneView sceneView = SceneView.lastActiveSceneView;
            if (sceneView != null)
            {
                Camera.main.transform.position = sceneView.camera.transform.position;
                Camera.main.transform.rotation = sceneView.camera.transform.rotation;
                Debug.Log("Camera aligned to Scene View!");
            }
        }
        else
        {
            Debug.LogWarning("No Main Camera found in the scene!");
        }
    }
}