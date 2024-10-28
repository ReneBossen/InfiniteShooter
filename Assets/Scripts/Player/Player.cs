using System;
using UnityEngine;

public class Player : MonoBehaviour
{
    public static Player Instance { get; private set; }
    private Rigidbody2D rb2d;
    [Header("Movement")]
    [SerializeField] private float jumpForce;
    [SerializeField] private float movementSpeed;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private Transform groundCheck;

    private bool isGrounded;

    private void Awake()
    {
        if (Instance != null)
        {
            Debug.LogError("Instance is not null");
        }
        else Instance = this;
        rb2d = GetComponent<Rigidbody2D>();
    }

    private void OnEnable()
    {
        PlayerInput.OnJumpPressed += Jump;
    }
    private void OnDisable()
    {
        PlayerInput.OnJumpPressed -= Jump;
    }

    private void Jump(object sender, EventArgs e)
    {
        if (isGrounded)
        {
            rb2d.AddForce(jumpForce * Time.deltaTime * Vector2.up, ForceMode2D.Impulse);
            if (rb2d.velocity.y > 7)
            {
                rb2d.velocity = new Vector2(rb2d.velocity.x, 7);
            }
        }
    }

    private void FixedUpdate()
    {
        Movement();
    }
    private void Update()
    {
        CheckIfGrounded();
        Debug.Log("Y velocity fra jump; " + rb2d.velocity.y);
    }

    private void Movement()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");

        transform.Translate(Vector3.right * moveHorizontal * movementSpeed * Time.deltaTime);
    }

    private void CheckIfGrounded()
    {
        isGrounded = Physics2D.Raycast(groundCheck.position, Vector2.down, 0.1f, groundLayer);
    }
}
