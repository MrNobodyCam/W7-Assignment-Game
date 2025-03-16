using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed = 20f;
    private Vector3 respawnPosition;

    Rigidbody m_Rigidbody;
    Vector3 m_Movement;

    void Start()
    {
        m_Rigidbody = GetComponent<Rigidbody>();
        
        // Set initial respawn position to starting position
        respawnPosition = transform.position;
        
        // Check if there's a saved checkpoint position
        if (PlayerPrefs.HasKey("CheckpointX"))
        {
            float x = PlayerPrefs.GetFloat("CheckpointX");
            float y = PlayerPrefs.GetFloat("CheckpointY");
            float z = PlayerPrefs.GetFloat("CheckpointZ");
            respawnPosition = new Vector3(x, y, z);
        }
    }

    void FixedUpdate()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        m_Movement.Set(horizontal, 0f, vertical);

        m_Rigidbody.AddForce(m_Movement * speed);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Checkpoint"))
        {
            // Update respawn position when touching a checkpoint
            respawnPosition = other.transform.position;
        }
    }
    
    void OnCollisionEnter(Collision collision)
    {
        // Check if player hit a white obstacle box
        if (collision.gameObject.CompareTag("Obstacle"))
        {
            // Respawn player at last checkpoint
            RespawnAtCheckpoint();
        }
    }
    
    void RespawnAtCheckpoint()
    {
        // Reset velocity
        m_Rigidbody.velocity = Vector3.zero;
        m_Rigidbody.angularVelocity = Vector3.zero;
        
        // Move to respawn position
        transform.position = respawnPosition;
        
        Debug.Log("Player respawned at checkpoint");
    }
}