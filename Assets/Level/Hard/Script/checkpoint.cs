using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    public bool isFinishLine = false;
    public Material checkpointMaterial;
    public Material activatedMaterial;
    
    private Renderer rend;
    
    void Start()
    {
        rend = GetComponent<Renderer>();
        if (rend && checkpointMaterial)
        {
            rend.material = checkpointMaterial;
        }
    }
    
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // Change appearance to show activation
            if (rend && activatedMaterial)
            {
                rend.material = activatedMaterial;
            }
            
            // Play activation sound or effect
            AudioSource audio = GetComponent<AudioSource>();
            if (audio)
            {
                audio.Play();
            }
        }
    }
}