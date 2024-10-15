using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class CameraRotationBasedOnMouse : MonoBehaviour
{
    public float mouseSensitivity = 400f; //마우스 감도
    private float MouseY;
    private float MouseX;

    public bool isInteracting = false;
    private CharacterMovement _cm;
    private Vector3 original_transform;
    private Quaternion original_rotation;
    public bool hasStarted = false;

    private void Start()
    {
        _cm = FindObjectOfType<CharacterMovement>();
        var transform1 = this.transform;
        original_transform = transform1.position;
        original_rotation = transform1.rotation;
#if UNITY_EDITOR
        hasStarted = true;
#endif
    }

    private void Update()
    {
        if (!hasStarted)
        {
            var transform1 = this.transform;
            transform1.position = original_transform;
            transform1.rotation = original_rotation;
        }
        else
        {
            isInteracting = _cm.isInteracting;
            if (!isInteracting) Rotate();
        }
    }

    private void Rotate()
    {
        // Input System의 마우스 입력 처리
        var mouseDelta = Mouse.current.delta.ReadValue(); // 현재 마우스 이동값

        MouseX += mouseDelta.x * mouseSensitivity * Time.deltaTime;
        MouseY -= mouseDelta.y * mouseSensitivity * Time.deltaTime;

        MouseY = Mathf.Clamp(MouseY, -10f, 90f); // Clamp를 통해 최소값 최대값을 넘지 않도록함

        transform.localRotation = Quaternion.Euler(MouseY, MouseX, 0f); // 각 축을 한꺼번에 계산
    }
}