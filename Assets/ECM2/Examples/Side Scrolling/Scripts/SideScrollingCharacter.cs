using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace ECM2.Examples.SideScrolling
{
    /// <summary>
    /// This example shows how to implement a typical side-scrolling movement with side to side rotation snap.
    /// </summary>
    public class SideScrollingCharacter : Character
    {
        // 是否可以操作
        public bool canControl = true;

        public bool canCrouch = false;

        // 提示可以按下Ctrl的UI
        public GameObject crouchHint;


        protected override void Awake()
        {
            // Call base method implementation

            base.Awake();

            // Disable Character rotation, well handle it here (snap move direction)

            SetRotationMode(RotationMode.None);
        }

        private void Update()
        {
            // 如果可以操作
            if (canControl)
            {
                // Add horizontal movement (in world space)

                float moveInput = Input.GetAxisRaw("Horizontal");
                SetMovementDirection(Vector3.right * moveInput);

                // Crouch input
                if (canCrouch)
                {
                    if (Input.GetKeyDown(KeyCode.LeftControl) || Input.GetKeyDown(KeyCode.C))
                    {
                        Crouch();

                        // 取消提示UI
                        if (crouchHint != null)
                        {
                            crouchHint.SetActive(false);
                        }
                    }


                    else if (Input.GetKeyUp(KeyCode.LeftControl) || Input.GetKeyUp(KeyCode.C))
                        UnCrouch();
                }


                // Jump input

                if (Input.GetButtonDown("Jump"))
                    Jump();
                else if (Input.GetButtonUp("Jump"))
                    StopJumping();

                // Snap side to side rotation

                if (moveInput != 0.0f)
                    SetYaw(moveInput * 90.0f);

                // 如果按下S键，从平台下落
                if (Input.GetKeyDown(KeyCode.S))
                {
                    FallThrough();
                }
            }
        }

        // 从平台下落
        private void FallThrough()
        {
            // 从平台下落
            if (IsOnGround())
            {
                Collider platformCollider = characterMovement.groundCollider;
                if (platformCollider != null)
                {
                    // 检查平台是否是指定的 Layer
                    int FloatingPlatformLayer = LayerMask.NameToLayer("FloatingPlatform");
                    if (platformCollider.gameObject.layer == FloatingPlatformLayer)
                    {
                        // 忽略与平台的碰撞
                        IgnoreCollision(platformCollider, true);

                        // 恢复碰撞（在短时间后）
                        StartCoroutine(ResetPlatformCollision(platformCollider, 0.4f));

                        PauseGroundConstraint(0.2f);
                        SetMovementMode(MovementMode.Falling);
                    }
                }
            }
        }

        private IEnumerator ResetPlatformCollision(Collider platformCollider, float delay)
        {
            yield return new WaitForSeconds(delay);

            // 恢复碰撞
            IgnoreCollision(platformCollider, false);
        }
    }
}