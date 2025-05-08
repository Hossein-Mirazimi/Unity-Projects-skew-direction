using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMovement : MonoBehaviour
{
    // * parameters
    [Header("Movement")]
    [SerializeField] float moveSpeed    = 6f;
    [SerializeField] float acceleration = 40f;

    [Header("Rotation")]
    [SerializeField] bool  rotateToFaceMovement = true;

    // * refs
    Rigidbody2D rb;

    Vector2 inputDir;

    void Awake() => rb = GetComponent<Rigidbody2D>();

    void Update()
    {
        float x = Input.GetAxisRaw("Horizontal");
        float y = Input.GetAxisRaw("Vertical");

        inputDir = new Vector2(x, y);
    }

    void FixedUpdate()
    {
        // * Movement
        Vector2 targetVel = inputDir * moveSpeed;
        Vector2 needed    = targetVel - rb.linearVelocity;
        rb.AddForce(needed * acceleration, ForceMode2D.Force);

        // * Rotation
        if (rotateToFaceMovement && inputDir != Vector2.zero)
        {
            float angle = Mathf.Atan2(inputDir.y, inputDir.x) * Mathf.Rad2Deg;
            rb.MoveRotation(angle - 90f);
        }
    }
}
