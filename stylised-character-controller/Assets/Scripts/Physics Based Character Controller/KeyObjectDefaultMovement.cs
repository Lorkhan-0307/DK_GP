using UnityEngine;

public class KeyObjectDefaultMovement : MonoBehaviour
{
    public float moveSpeed = 1.0f;  // 위아래로 움직이는 속도
    public float rotationSpeed = 50.0f;  // 회전 속도
    public float moveHeight = 0.5f;  // 위아래 이동 범위

    private Vector3 startPosition;
    private Quaternion startRotation;
    private CharacterMovement _cm;
    
    void Start()
    {
        _cm = FindObjectOfType<CharacterMovement>();
        startPosition = transform.position;
        startRotation = transform.rotation;
    }

    void Update()
    {
        if (!_cm.isInteracting)
        {
            float newY = startPosition.y + Mathf.Sin(Time.time * moveSpeed) * moveHeight;
            transform.position = new Vector3(transform.position.x, newY, transform.position.z);

            transform.Rotate(Vector3.up, rotationSpeed * Time.deltaTime);
        }
        else
        {
            transform.position = startPosition;
            transform.rotation = startRotation;
        }
    }
}