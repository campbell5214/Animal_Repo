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
    private Animator animator; 

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>(); 
        ResetJumpCooldown();
    }

    void Update()
    {
        // Get input
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        // Calculate movement direction
        Vector3 movement = new Vector3(horizontalInput, 0f, verticalInput).normalized;

        // Move the object
        transform.Translate(movement * speed * Time.deltaTime);

        // Update animator parameters
        if (animator != null)
        {
            // Set the "Speed_f" parameter based on movement speed
            animator.SetFloat("Speed_f", movement.magnitude * speed);

            // Trigger "Jump_b" parameter if jump is pressed and allowed
            if (Input.GetKeyDown(KeyCode.Space) && CanJump())
            {
                Jump();
                animator.SetBool("Jump_b", true);
            }
            else
            {
                animator.SetBool("Jump_b", false);
            }
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
