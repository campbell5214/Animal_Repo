using UnityEngine;

public class movement : MonoBehaviour
{
    public float speed = 5f;
    public float jumpForce = 10f;
    public float initialJumpCooldown = 3f; // Cooldown time for initial jumps
    public float secondaryJumpCooldown = 2f; // Cooldown time for secondary jumps
    public int maxJumps = 3; // Maximum number of jumps allowed per cooldown period

    private Rigidbody rb;
    private int jumpsRemaining;
    private float lastJumpTime;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        ResetJumpCooldown();
    }

    void Update()
    {
        // Get input from arrow keys or WASD for vertical movement
        float verticalInput = Input.GetAxis("Vertical");

        // Calculate movement direction (only along the z-axis)
        Vector3 movement = new Vector3(0f, 0f, verticalInput).normalized;

        // Move the object
        transform.Translate(movement * speed * Time.deltaTime);

        // Jumping (triggered by spacebar)
        if (Input.GetKeyDown(KeyCode.Space) && CanJump())
        {
            Jump();
        }
    }

    bool CanJump()
    {
        // Check if there are remaining jumps and if the jump cooldown has passed
        return jumpsRemaining > 0 && Time.time - lastJumpTime >= initialJumpCooldown;
    }

    void Jump()
    {
        // Apply jump force
        rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);

        // Update jump parameters
        jumpsRemaining--;
        if (jumpsRemaining == 0)
        {
            Invoke("ResetJumpCooldown", secondaryJumpCooldown);
        }
        lastJumpTime = Time.time;
    }

    void ResetJumpCooldown()
    {
        jumpsRemaining = maxJumps;
        lastJumpTime = Time.time;
    }
}
