using System;
using UnityEngine;

public class DragAndDropWithGravity : MonoBehaviour
{
    private Camera _mainCamera; // 主相机，用于捕捉鼠标射线
    private bool _isDragging; // 是否正在拖动
    private Transform _selectedObject; // 当前被选中的物体
    private Rigidbody _selectedRigidbody; // 当前被选中物体的刚体组件


    void Start()
    {
        _mainCamera = Camera.main; // 初始化主相机
    }

    void Update()
    {
        // 检测鼠标左键按下事件
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;
            Ray ray = _mainCamera.ScreenPointToRay(Input.mousePosition); // 从鼠标位置发射射线

            // 使用 Layer Mask 忽略杯子的碰撞体
            int layerMask = LayerMask.GetMask("Default", "Draggable"); // 指定需要检测的层

            // 检测射线是否碰到物体
            if (Physics.Raycast(ray, out hit, Mathf.Infinity, layerMask))
            {
                // 检查碰撞的物体是否有"Draggable"标签
                if (hit.transform.CompareTag("Draggable"))
                {
                    _isDragging = true; // 设置拖动状态
                    _selectedObject = hit.transform; // 获取选中物体
                    _selectedRigidbody = _selectedObject.GetComponent<Rigidbody>(); // 获取刚体组件

                    if (_selectedRigidbody != null)
                    {
                        _selectedRigidbody.useGravity = false; // 禁用重力，使物体悬浮
                        _selectedRigidbody.velocity = Vector3.zero; // 清除物体的速度，防止物理效果干扰
                    }
                }
            }
        }

        // 鼠标左键保持按下并且处于拖动状态
        if (_isDragging && Input.GetMouseButton(0))
        {
            DragObject(); // 拖动物体
        }

        // 鼠标左键释放，结束拖动
        if (Input.GetMouseButtonUp(0) && _isDragging)
        {
            ReleaseObject(); // 释放物体
        }
    }

    // 拖动物体的逻辑
    private void DragObject()
    {
        Ray ray = _mainCamera.ScreenPointToRay(Input.mousePosition); // 从鼠标位置发射射线

        // 定义一个平面，平面与物体的当前位置一致，法向量为Z轴方向
        Plane plane = new Plane(Vector3.forward, _selectedObject.position);
        float distance;

        // 射线与平面相交时，计算交点
        if (plane.Raycast(ray, out distance))
        {
            Vector3 point = ray.GetPoint(distance); // 获取射线与平面的交点

            // 更新物体的位置，仅更新X和Y坐标，保持Z坐标不变
            _selectedObject.position = new Vector3(point.x, point.y, _selectedObject.position.z);
        }
    }

    // 释放物体的逻辑
    private void ReleaseObject()
    {
        _isDragging = false; // 结束拖动状态

        if (_selectedRigidbody != null)
        {
            _selectedRigidbody.useGravity = true; // 恢复重力，使物体开始坠落
            _selectedRigidbody = null; // 清除刚体引用
        }

        _selectedObject = null; // 清除选中物体引用
    }
}