using UnityEngine;

public class CoinInteraction : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.CompareTag("Player"));
        {
            Destroy(gameObject);
        }
    }
}
