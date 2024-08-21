using UnityEngine;

public class DragAndThrowController : MonoBehaviour
{
    private Vector2 startPoint;
    private Vector2 endPoint;
    private bool isDragging = false;
    private Rigidbody2D rb;

    public float forceMultiplier = 5.0f;
    public GameObject house;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.isKinematic = true; // Disable physics simulation at the start
    }

    void Update()
    {
        HandleDragging();
    }

    private void HandleDragging()
    {
        if (Input.GetMouseButtonDown(0))
        {
            startPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            isDragging = true;
        }

        if (Input.GetMouseButtonUp(0) && isDragging)
        {
            endPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 force = (startPoint - endPoint) * forceMultiplier;
            rb.isKinematic = false; // Enable physics simulation
            rb.AddForce(force, ForceMode2D.Impulse);
            isDragging = false;
        }

        // Reload the scene when the R key is pressed
        if (Input.GetKeyDown(KeyCode.R))
        {
            GameManager.Instance.RestartGame();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject == house)
        {
            rb.velocity = Vector2.zero; // Stop any remaining movement
            rb.angularVelocity = 0f; // Stop any rotation

            // Hide the house sprite
            house.GetComponent<SpriteRenderer>().enabled = false;

            // Notify the GameManager
            GameManager.Instance.OnGameClear();
        }
    }
}
