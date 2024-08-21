using UnityEngine;

public class BombCollision : MonoBehaviour
{
    public GameObject explosionPrefab; // Prefab for the explosion animation

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Home"))
        {
            // Instantiate the explosionPrefab at the bomb's position
            Instantiate(explosionPrefab, transform.position, Quaternion.identity);
            gameObject.SetActive(false);
        }
    }
}
