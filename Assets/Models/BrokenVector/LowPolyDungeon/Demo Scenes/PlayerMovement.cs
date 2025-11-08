using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    public InputActionAsset InputActions;
    public Rigidbody rb;
    public float baseSpeed = 12f;
    
    [SerializeField] private float maxSpeed = 10f;

    [SerializeField] private GameObject Camera; 
    
    private InputAction moveAction;
    private Vector2 moveAmount;
    private InputAction lookAction;
    private Vector2 lookDirection;

    private void OnEnable()
    {
        InputActions.FindActionMap("Player").Enable();
    }
    
    private void OnDisable()
    {
        InputActions.FindActionMap("Player").Disable();
    }

    private void Awake()
    {
        moveAction = InputSystem.actions.FindAction("Move");
        lookAction = InputSystem.actions.FindAction("Look");
    }
    
    void Start()
    {

    }

    void Update()
    {
        moveAmount = moveAction.ReadValue<Vector2>();
        lookDirection = lookAction.ReadValue<Vector2>();
    }

    void FixedUpdate()
    {
        if (maxSpeed < rb.linearVelocity.magnitude) return;
        Move();
        Debug.Log(rb.linearVelocity);
    }

    private void Move()
    {
        if (Keyboard.current.wKey.isPressed)
        {
            Vector3 direction = new Vector3(moveAmount.x, 0, moveAmount.y).normalized;
            
            rb.AddForce((Camera.transform.forward * baseSpeed * Time.deltaTime), ForceMode.VelocityChange);
            
        }
        if (Keyboard.current.sKey.isPressed)
        {
            rb.AddForce((-Camera.transform.forward * baseSpeed * Time.deltaTime), ForceMode.VelocityChange);
        }
        if (Keyboard.current.aKey.isPressed)
        {
            rb.AddForce((-Camera.transform.right * baseSpeed * Time.deltaTime), ForceMode.VelocityChange);
        }
        if (Keyboard.current.dKey.isPressed)
        {
            rb.AddForce((Camera.transform.right * baseSpeed * Time.deltaTime), ForceMode.VelocityChange);
        }
    }
}
