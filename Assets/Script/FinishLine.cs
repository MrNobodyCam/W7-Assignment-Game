using UnityEngine;
using UnityEngine.UI;

public class FinishLine : MonoBehaviour
{
    // public Text finishText; // For UI Text component
    // OR
    public GameObject finishPanel; // For a more complex UI

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // Show finish message
            // if (finishText != null)
            //     finishText.gameObject.SetActive(true);
            if (finishPanel != null)
                finishPanel.SetActive(true);

            Debug.Log("Player finished the level!");
            // Optional: Add more finish effects here
        }
    }
}