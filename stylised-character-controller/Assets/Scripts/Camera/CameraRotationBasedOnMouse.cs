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

    private void Start()
    {
        _cm = FindObjectOfType<CharacterMovement>();
    }

    private void Update()
    {
        isInteracting = _cm.isInteracting;
        if(!isInteracting) Rotate();
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