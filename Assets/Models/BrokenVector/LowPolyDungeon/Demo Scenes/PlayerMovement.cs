using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    public Rigidbody rb;

    [SerializeField] private GameObject Camera;
    private Vector3 Orientation;
    
    [SerializeField] private PlayerManager PlayerManager;

    private void Awake()
    {
        Orientation = new Vector3(Camera.transform.forward.x, 0, Camera.transform.forward.z);
    }
    
    void Start()
    {

    }

    void Update()
    {
    }

    void FixedUpdate()
    {
        Move();
        Look();
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
        moveHorizontal *= PlayerManager.speed;
        moveVertical *= PlayerManager.speed;
        movement = new Vector3(moveHorizontal/2, 0, moveVertical);
        Quaternion rotation = Quaternion.Euler(0f, +45f, 0f);
        movement = rotation * movement;
        
        rb.linearVelocity = movement;
    }

    private void Look()
    {
        Ray ray = Camera.GetComponent<Camera>().ScreenPointToRay(Mouse.current.position.ReadValue());
        RaycastHit hit;
        
        int groundLayer = LayerMask.NameToLayer("Ground");
        int mask = 1 << groundLayer;

        Debug.DrawRay(ray.origin, ray.direction * 100f, Color.yellow);
        if (Physics.Raycast(ray, out hit, 100f, mask))
        {
            Vector3 finalDir = new Vector3(hit.point.x, 0, hit.point.z);

            //transform.forward = Vector3.Slerp(transform.forward, finalDir, Time.deltaTime * acceleration);
            transform.LookAt(finalDir);
        }
    }
}
