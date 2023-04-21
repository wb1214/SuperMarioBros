using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Camera cam;
    private Rigidbody2D rigid;
    private Collider2D col;

    private Vector2 velocity;
    private float inputAxis;

    [HideInInspector]
    public float moveSpeed = 8f;
    [HideInInspector]
    public float maxJumpHeight = 5f;
    [HideInInspector]
    public float maxJumpTime = 1f;
    public float jumpForce => (2f * maxJumpHeight) / (maxJumpTime / 2f);
    public float gravity => (-2f * maxJumpHeight) / Mathf.Pow((maxJumpTime / 2f), 2);

    public bool grounded { get; private set; }
    public bool jumping { get; private set; }
    public bool running => Mathf.Abs(velocity.x) > 0.25f || Mathf.Abs(inputAxis) > 0.25f;
    public bool sliding => (inputAxis > 0f && velocity.x < 0f) || (inputAxis < 0f && velocity.x > 0f);

    private void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        col = GetComponent<Collider2D>();
        cam = Camera.main;
    }
    private void OnEnable()
    {
        rigid.isKinematic = false;
        col.enabled = true;
        velocity = Vector2.zero;
        jumping = false;
    }

    private void OnDisable()
    {
        rigid.isKinematic = true;
        col.enabled = false;
        velocity = Vector2.zero;
        jumping = false;
    }

    private void Update()
    {
        PlayerMove();

        grounded = rigid.Raycast(Vector2.down);

        if (grounded)
        {
            GroundedMove();
        }

        ApplyGravity();
    }

    private void PlayerMove()
    {
        inputAxis = Input.GetAxis("Horizontal");
        velocity.x = Mathf.MoveTowards(velocity.x, inputAxis * moveSpeed, moveSpeed * Time.deltaTime);

        if(rigid.Raycast(Vector2.right * velocity.x))
        {
            velocity.x = 0f;
        }

        if (velocity.x > 0f)
        {
            transform.eulerAngles = Vector3.zero;
        }
        else if (velocity.x < 0f)
        {
            transform.eulerAngles = new Vector3(0f, 180f, 0f);
        }
    }

    private void GroundedMove()
    {
        velocity.y = Mathf.Max(velocity.y, 0f);
        jumping = velocity.y > 0f;

        if (Input.GetButtonDown("Jump"))
        {
            velocity.y = jumpForce;
            jumping = true;
        }
    }
    private void ApplyGravity()
    {
        bool falling = velocity.y < 0f || !Input.GetButton("Jump");
        float multiplier = falling ? 2f : 1f;

        velocity.y += gravity * multiplier * Time.deltaTime;
        velocity.y = Mathf.Max(velocity.y, gravity / 2f);
    }
    private void FixedUpdate()
    {
        Vector2 pos = rigid.position;
        pos += velocity * Time.fixedDeltaTime;

        Vector2 leftEdge = cam.ScreenToWorldPoint(Vector2.zero);
        Vector2 rightEdge = cam.ScreenToWorldPoint(new Vector2(Screen.width, Screen.height));
        pos.x = Mathf.Clamp(pos.x, leftEdge.x + 0.5f, rightEdge.x - 0.5f);

        rigid.MovePosition(pos);
    }

    private void OnCollisionEnter2D(Collision2D _col)
    {
        if(_col.gameObject.layer == LayerMask.NameToLayer("Enemy"))
        {
            if(transform.DotTest(_col.transform, Vector2.down))
            {
                velocity.y = jumpForce / 2f;
                jumping = true;
            }
        }
        else if(_col.gameObject.layer != LayerMask.NameToLayer("PowerUp"))
        {
            if(transform.DotTest(_col.transform, Vector2.up))
            {
                velocity.y = 0f;
            }
        }
    }




}
