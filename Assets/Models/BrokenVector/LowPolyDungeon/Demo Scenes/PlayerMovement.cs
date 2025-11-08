using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    public InputActionAsset InputActions;
    public Rigidbody rb;
    public float acceleration = 12f;
    
    [SerializeField] private float maxSpeed = 10f;

    [SerializeField] private GameObject Camera;
    private Vector3 Orientation;
    
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
        Orientation = new Vector3(Camera.transform.forward.x, 0, Camera.transform.forward.z);
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
        //if (maxSpeed < rb.linearVelocity.magnitude) return;
        Move();
    }

    private void Move()
    {
        float moveHorizontal = 0;
        float moveVertical = 0;
        Vector3 movement;
        
        if (Keyboard.current.wKey.isPressed)
        {
            //rb.AddForce((Camera.transform.forward * acceleration * Time.deltaTime), ForceMode.VelocityChange);
            //transform.position += Camera.transform.forward * acceleration * Time.deltaTime;
            moveVertical += 1;
        }
        if (Keyboard.current.sKey.isPressed)
        {
            //rb.AddForce((-Camera.transform.forward * acceleration * Time.deltaTime), ForceMode.VelocityChange);
            //transform.position -= Camera.transform.forward * acceleration * Time.deltaTime;
            moveVertical -= 1;
        }
        
        if (Keyboard.current.aKey.isPressed)
        {
            //rb.AddForce((-Camera.transform.right * acceleration * Time.deltaTime), ForceMode.Acceleration);
            //transform.position -= Camera.transform.right * acceleration * Time.deltaTime;
            moveHorizontal -= 1;
        }
        if (Keyboard.current.dKey.isPressed)
        {
            //rb.AddForce((Camera.transform.right * acceleration * Time.deltaTime), ForceMode.VelocityChange);
            //transform.position += Camera.transform.right * acceleration * Time.deltaTime;
            moveHorizontal += 1;
        }
        moveHorizontal *= acceleration * Time.deltaTime;
        moveVertical *= acceleration * Time.deltaTime;
        movement = new Vector3(moveHorizontal/2, 0, moveVertical);
        Quaternion rotation = Quaternion.Euler(0f, +45f, 0f);
        movement = rotation * movement;
        transform.position += movement;
    }
}
