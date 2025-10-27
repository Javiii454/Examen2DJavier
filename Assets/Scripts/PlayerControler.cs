using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerControler : MonoBehaviour
{

    private Rigidbody2D playerRigidBody;

    private BoxCollider2D playerCollier;

    private InputAction moveAction;

    [SerializeField] private Vector2 moveInput;

    [SerializeField] private float playerVelocity = 10f;

    private InputAction jumpAction;

    [SerializeField] private float jumpHeight = 2f;

    private Animator animator;

    private Vector2 sensorSize = new Vector2(0.5f, 0.5f);

    [SerializeField] private Transform sensorPosition;

   


   
    void Awake()
    {
        playerRigidBody = GetComponent<Rigidbody2D>();

        playerCollier = GetComponent<BoxCollider2D>();

        animator = GetComponent<Animator>();

        moveAction = InputSystem.actions["Move"];

        jumpAction = InputSystem.actions["Jump"];
    }

    // Update is called once per frame
    void Update()
    {
        moveInput = moveAction.ReadValue<Vector2>();
        Movement();


        if (jumpAction.WasPressedThisFrame() && IsGrounded())
        {

            Jump();


        }
        animator.SetBool("IsJumping", !IsGrounded());
       
       
    }
    

    void FixedUpdate()
    {
        playerRigidBody.linearVelocity = new Vector2(playerVelocity * moveInput.x, playerRigidBody.linearVelocity.y);
    }

    void Movement()
    {
        if (moveInput.x > 0)
        {
            transform.rotation = Quaternion.Euler(0, 0, 0);
            animator.SetBool("IsRunning", true);
        }
        else if (moveInput.x < 0)
        {
            transform.rotation = Quaternion.Euler(0, 180, 0);
            animator.SetBool("IsRunning", true);
        }
        else
        {
            animator.SetBool("IsRunning", false);
        }
    }

    void Jump()
    {
        playerRigidBody.AddForce(transform.up * Mathf.Sqrt(jumpHeight * -2 * Physics2D.gravity.y), ForceMode2D.Impulse);
        
        

    }

    bool IsGrounded()
    {

        Collider2D[] ground = Physics2D.OverlapBoxAll(sensorPosition.position, sensorSize, 3);

        foreach (Collider2D item in ground)
        {
            if (item.gameObject.layer == 3)
            {
                return true;
            }
        }
        return false;

    }
    
    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(sensorPosition.position, sensorSize);
    }
    

    
}
