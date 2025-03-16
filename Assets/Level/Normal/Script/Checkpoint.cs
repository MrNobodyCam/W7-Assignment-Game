using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    public int checkpointID = 0;
    public Color activeColor = Color.green;
    public Color inactiveColor = Color.red;
    
    private Renderer checkpointRenderer;
    private bool isActivated = false;
    
    void Start()
    {
        checkpointRenderer = GetComponent<Renderer>();
        if (checkpointRenderer == null)
            checkpointRenderer = GetComponentInChildren<Renderer>();
            
        if (checkpointRenderer != null)
            checkpointRenderer.material.color = inactiveColor;
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // Save checkpoint position
            PlayerPrefs.SetFloat("CheckpointX", transform.position.x);
            PlayerPrefs.SetFloat("CheckpointY", transform.position.y);
            PlayerPrefs.SetFloat("CheckpointZ", transform.position.z);
            PlayerPrefs.SetInt("LastCheckpoint", checkpointID);
            
            // Change color
            if (checkpointRenderer != null)
                checkpointRenderer.material.color = activeColor;
                
            isActivated = true;
            
            Debug.Log("Checkpoint " + checkpointID + " reached!");
        }
    }
}