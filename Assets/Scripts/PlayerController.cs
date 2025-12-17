using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public float speed = 6f;
    public float jumpForce = 7f;

    Rigidbody2D rb;
    Animator anim;

    InputAction moveRight;
    InputAction moveLeft;
    InputAction jump;
    InputAction shoot;

    bool isGrounded = true;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();

        moveRight = new InputAction("MoveRight", binding: "<Keyboard>/d");
        moveLeft = new InputAction("MoveLeft", binding: "<Keyboard>/a");
        jump = new InputAction("Jump", binding: "<Keyboard>/space");
        shoot = new InputAction("Shoot", binding: "<Keyboard>/l");
    }

    void OnEnable()
    {
        moveRight.Enable();
        moveLeft.Enable();
        jump.Enable();
        shoot.Enable();
    }

    void OnDisable()
    {
        moveRight.Disable();
        moveLeft.Disable();
        jump.Disable();
        shoot.Disable();
    }

    void Update()
    {
        if (jump.WasPressedThisFrame() && isGrounded)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
            anim.SetTrigger("Jump");
            isGrounded = false;
        }

        if (shoot.WasPressedThisFrame())
        {
            anim.SetTrigger("Shoot");
        }
    }

    void FixedUpdate()
    {
        float direction = 0f;

        if (moveRight.IsPressed())
            direction = 1f;

        if (moveLeft.IsPressed())
            direction = -1f;

        rb.linearVelocity = new Vector2(direction * speed, rb.linearVelocity.y);

        anim.SetBool("Walk", direction != 0);

        if (direction > 0)
            transform.localScale = new Vector3(1, 1, 1);

        if (direction < 0)
            transform.localScale = new Vector3(-1, 1, 1);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.contacts[0].normal.y > 0.5f)
            isGrounded = true;
    }
}
