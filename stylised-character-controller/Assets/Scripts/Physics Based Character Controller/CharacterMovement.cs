using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class CharacterMovement : MonoBehaviour
{
    
    public Transform cameraTransform;
    public float moveSpeed = 5f;
    public float rotationSpeed = 10f;
    public float jumpForce = 4.5f; 
    public float initialJumpGravity = -30f; 
    public float jumpGravity = -8f; 
    public float fallGravity = -50f;

    public bool isInteracting;

    [SerializeField] Animator _animator;

    private Vector2 _moveContext;
    private CharacterController _characterController;
    private float _verticalVelocity;
    private bool _isGrounded;

    private static readonly int Magnitude = Animator.StringToHash("magnitude");
    private static readonly int JumpAnim = Animator.StringToHash("jump_anim");
    private static readonly int IsDown = Animator.StringToHash("is_down");

    private InputAction menuAction;

    private Collider _currentInteractionCollider;


    [SerializeField] private GameObject interactionUI;
    
    private SwitchSceneManager _switchSceneManager;
    
        
    private float _noInputTimer = 0f;
    private const float TIMEOUT_DURATION = 120f;
    
    public void ExitScene()
    {
        _switchSceneManager.SwitchScene("Title", "LoadingScene", () => {
            PageManager.ChangeImmediate("MainPage");
        });
    }
    

    
    public void ResetInputTimer()
    {
        _noInputTimer = 0f;
    }

    private void Start()
    {
        _characterController = GetComponent<CharacterController>();
        interactionUI.SetActive(false);
        isInteracting = false;

        menuAction = GetComponent<PlayerInput>().actions["Menu"];
        
    
#if UNITY_EDITOR
        _switchSceneManager = FindObjectOfType<SwitchSceneManager>();
#else 
        _switchSceneManager = FindObjectOfType<SwitchSceneManager>();
#endif
        
    }

    public void MoveInputAction(InputAction.CallbackContext context)
    {
        _moveContext = context.ReadValue<Vector2>();
        ResetInputTimer();
    }

    public void MenuInputAction(InputAction.CallbackContext context)
    {
        ExitScene();
    }

    public void JumpInputAction(InputAction.CallbackContext context)
    {
        if (context.performed && _isGrounded)
        {
            _verticalVelocity = jumpForce; // 초기 점프 속도
            _animator.SetTrigger(JumpAnim); // 점프 애니메이션 트리거 발동
            _animator.SetBool(IsDown, false); // 공중에 있을 때 is_down을 false로 설정
        }
    }
    
    public void InteractionInputAction(InputAction.CallbackContext context)
    {
        if (context.performed && _currentInteractionCollider != null && !isInteracting && interactionUI.activeSelf)
        {
            bool hasInteracted = false;
            
            CutsceneObject interactionObject = _currentInteractionCollider.GetComponent<CutsceneObject>();
            if (interactionObject != null)
            {
                hasInteracted = interactionObject.PlayCutScene(); // 해당 오브젝트의 PlayCutScene 함수 호출
            }

            if (hasInteracted)
            {
                interactionUI.SetActive(false);
                isInteracting = true;
            }
        }
    }

    private void FixedUpdate()
    {
        if(menuAction.triggered) ExitScene();
        // Increase the timer if no input has been detected
        _noInputTimer += Time.deltaTime;
        
        //Debug.Log("Time is " + _noInputTimer);

        // Check if the timer exceeds the allowed time
        if (_noInputTimer >= TIMEOUT_DURATION)
        {
            _noInputTimer = 0f;
            ExitScene();
        }
        
        if (!isInteracting)
        {
            HandleMovement();
        }
    }

    private void HandleMovement()
    {
        _isGrounded = _characterController.isGrounded;

        if (_isGrounded && _verticalVelocity < 0)
        {
            _verticalVelocity = -2f; // 약간의 음수 값을 주어 땅에 붙어있도록 설정
        }

        // 애니메이션 상태 확인
        AnimatorStateInfo stateInfo = _animator.GetCurrentAnimatorStateInfo(0);
        bool isLanding = stateInfo.IsName("flower|Down"); // "Down" 상태인지 확인 (착지 애니메이션)

        Vector3 moveDirection = Vector3.zero;

        _animator.SetFloat(Magnitude, _moveContext.magnitude);

        if (_moveContext.sqrMagnitude > 0.01f)
        {
            // 카메라의 방향을 기준으로 캐릭터 이동 방향 설정
            Vector3 forward = cameraTransform.forward;
            Vector3 right = cameraTransform.right;

            forward.y = 0f;  // 수직 방향 제거하여 평면 상에서 이동
            right.y = 0f;
            forward.Normalize();
            right.Normalize();

            moveDirection = forward * _moveContext.y + right * _moveContext.x;
            moveDirection.Normalize();

            // 캐릭터 회전 (카메라 방향을 따라)
            Quaternion targetRotation = Quaternion.LookRotation(moveDirection);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        }

        // 수직 속도 업데이트 (중력 적용)
        if (_verticalVelocity > 0)
        {
            // 올라가는 동안 중력 적용
            if (_verticalVelocity == jumpForce)
            {
                _verticalVelocity += initialJumpGravity * Time.deltaTime; // 초기 큰 중력
            }
            else
            {
                _verticalVelocity += jumpGravity * Time.deltaTime; // 이후 감소하는 중력
            }
        }
        else
        {
            // 내려가는 동안 중력 적용
            _verticalVelocity += fallGravity * Time.deltaTime;
            _animator.SetBool(IsDown, true); // 땅에 착지하면 is_down을 true로 설정
        }

        // 캐릭터 이동 (수평 + 수직)
        moveDirection *= moveSpeed;
        moveDirection.y = _verticalVelocity;
        _characterController.Move(moveDirection * Time.deltaTime);
    }
    
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.GetComponent<Transform>().name);
        if (other.CompareTag("Interactive"))
        {
            interactionUI.SetActive(true);
            _currentInteractionCollider = other;
        }
        else if (other.CompareTag("Fall"))
        {
#if UNITY_EDITOR
            UnityEngine.SceneManagement.SceneManager.LoadScene("Sky_Map");
            
#else
            _switchSceneManager.SwitchScene("Title", "Sky_Map", () => {
                PageManager.ChangeImmediate("SkyPlayPage");
            });
#endif
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (_currentInteractionCollider == other)
        {
            _currentInteractionCollider = null;
            interactionUI.SetActive(false);
        }
    }

    public void SetIsNotInteracting()
    {
        isInteracting = false;
    }
    
    public void SetTransform(Transform targetTransform)
    {
            
    }
}
