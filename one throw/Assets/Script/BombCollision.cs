using UnityEngine;

public class BombCollision : MonoBehaviour
{
    public GameObject explosionPrefab; // Prefab for the explosion animation

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Home"))
        {
            // Instantiate the explosionPrefab at the bomb's position
            GameObject explosion = Instantiate(explosionPrefab, transform.position, Quaternion.identity);

            // Set the animator trigger to start the explosion animation
            Animator animator = explosion.GetComponent<Animator>();
            if (animator != null)
            {
                animator.SetTrigger("triggerExplosion");
            }

            // Hide or destroy the bomb object
            gameObject.SetActive(false);
        }
    }
}
