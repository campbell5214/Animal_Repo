using UnityEngine;
using System.Collections;
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
    private Animator animator; // Reference to the Animator component
    private bool isSpeedBoosted = false;
    private float speedBoostDuration = 2f;
    private float originalSpeed;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>(); // Get the Animator component
        ResetJumpCooldown();
        originalSpeed = speed;
    }

    void Update()
    {
        // Get input from arrow keys or WASD for horizontal and vertical movement
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        // Calculate movement direction
        Vector3 movement = new Vector3(horizontalInput, 0f, verticalInput).normalized;

        // Move the object
        if (!isSpeedBoosted)
            transform.Translate(movement * speed * Time.deltaTime);
        else
            transform.Translate(movement * (speed * 2f) * Time.deltaTime); // Double speed during boost

        // Update animator parameters
        if (animator != null)
        {
            // Set trigger for run animation if the character is moving
            if (movement.magnitude > 0)
            {
                animator.SetTrigger("Run");
            }

            // Jumping (triggered by spacebar)
            if (Input.GetKeyDown(KeyCode.Space) && CanJump())
            {
                Jump();
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

        // Trigger jump animation
        if (animator != null)
        {
            animator.SetTrigger("Jump"); // Trigger the "Jump" animation
        }
    }

    void ResetJumpCooldown()
    {
        jumpsRemaining = maxJumps;
        lastJumpTime = Time.time;
    }

    // Collision detection with pizza
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Pizza"))
        {
            StartCoroutine(SpeedBoost());
            Destroy(other.gameObject); // Destroy the pizza
        }
    }

    IEnumerator SpeedBoost()
    {
        isSpeedBoosted = true;
        yield return new WaitForSeconds(speedBoostDuration);
        isSpeedBoosted = false;
    }
}
