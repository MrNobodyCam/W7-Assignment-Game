using UnityEngine;
using System.Collections;

public class BallController : MonoBehaviour
{
    // Movement settings
    public float moveSpeed = 10f;
    public float jumpForce = 7f;
    public float gravity = 20f;
    
    // Ground check
    public float groundCheckDistance = 0.3f;
    public LayerMask groundLayer;
    private bool isGrounded = false;
    
    // Checkpoint system
    private Vector3 lastCheckpoint;
    
    // Components
    private Rigidbody rb;
    private Transform playerTransform;
    
    // UI References (assign these in the inspector)
    public GameObject winPanel;
    public GameObject gameOverPanel;
    
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        playerTransform = transform;
        
        // Set initial checkpoint as starting position
        lastCheckpoint = playerTransform.position;
        
        // Hide UI panels at start
        if (winPanel) winPanel.SetActive(false);
        if (gameOverPanel) gameOverPanel.SetActive(false);
    }
    
    void Update()
    {
        // Check if grounded
        CheckGrounded();
        
        // Movement controls
        HandleMovement();
        
        // Check if player fell off the map
        CheckFallOutOfBounds();
    }
    
    void CheckGrounded()
    {
        RaycastHit hit;
        isGrounded = Physics.Raycast(transform.position, Vector3.down, out hit, groundCheckDistance, groundLayer);
    }
    
    void HandleMovement()
    {
        // Get input
        float horizontalInput = 0;
        float verticalInput = 0;
        
        // W key - Forward
        if (Input.GetKey(KeyCode.W))
        {
            verticalInput = 1;
        }
        
        // A key - Left
        if (Input.GetKey(KeyCode.A))
        {
            horizontalInput = -1;
        }
        
        // D key - Right
        if (Input.GetKey(KeyCode.D))
        {
            horizontalInput = 1;
        }
        
        // Calculate move direction relative to camera
        Vector3 moveDirection = new Vector3(horizontalInput, 0, verticalInput).normalized;
        
        // Apply movement force if there's input
        if (moveDirection.magnitude > 0.1f)
        {
            // Get camera's forward and right vectors for relative movement
            Vector3 cameraForward = Camera.main.transform.forward;
            Vector3 cameraRight = Camera.main.transform.right;
            
            // Project vectors onto XZ plane
            cameraForward.y = 0;
            cameraRight.y = 0;
            cameraForward.Normalize();
            cameraRight.Normalize();
            
            // Create camera relative movement direction
            Vector3 relativeMovement = cameraRight * horizontalInput + cameraForward * verticalInput;
            
            // Apply force to the rigidbody
            rb.AddForce(relativeMovement * moveSpeed, ForceMode.Acceleration);
        }
        
    
    }
    
    void CheckFallOutOfBounds()
    {
        // If player falls below a certain height, respawn at last checkpoint
        if (transform.position.y < -10)
        {
            RespawnAtCheckpoint();
        }
    }
    
    void RespawnAtCheckpoint()
    {
        // Reset position and velocity
        rb.velocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
        transform.position = lastCheckpoint;
    }
    
    void OnTriggerEnter(Collider other)
    {
        // Handle checkpoint
        if (other.CompareTag("Checkpoint"))
        {
            lastCheckpoint = other.transform.position;
            // Optional: Add particle effect or sound for checkpoint
        }
        
        // Handle finish
        if (other.CompareTag("Finish"))
        {
            if (winPanel) winPanel.SetActive(true);
            // Optional: Add celebration effect or sound
            
            // Pause game or show restart option
            Time.timeScale = 0.5f; // Slow down time for dramatic effect
        }
        
        // Handle obstacles that cause instant death/respawn
        if (other.CompareTag("DeathZone"))
        {
            RespawnAtCheckpoint();
        }
    }
    
    void OnCollisionEnter(Collision collision)
    {
        // Handle spinner obstacles - apply torque force to the player
        if (collision.gameObject.CompareTag("Spinner-Obstacle"))
        {
            Vector3 direction = (transform.position - collision.transform.position).normalized;
            rb.AddForce(direction * 15f, ForceMode.Impulse);
        }
    }
}