using UnityEngine;

public class EntityMovement : MonoBehaviour
{
    public float speed = 1f;
    public Vector2 dir = Vector2.left;

    private Rigidbody2D rigid;
    private Vector2 velocity;

    private void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        enabled = false; 
    }

    private void OnBecameVisible()
    {
        enabled = true;
    }

    private void OnBecameInvisible()
    {
        enabled = false;
    }

    private void OnEnable()
    {
        rigid.WakeUp();
    }

    private void OnDisable()
    {
        rigid.velocity = Vector2.zero;
        rigid.Sleep();
    }

    private void FixedUpdate()
    {
        velocity.x = dir.x * speed;
        velocity.y += Physics2D.gravity.y * Time.fixedDeltaTime;

        rigid.MovePosition(rigid.position + velocity * Time.fixedDeltaTime);

        if(rigid.Raycast(dir))
        {
            dir = -dir;
        }

        if(rigid.Raycast(Vector2.down))
        {
            velocity.y = Mathf.Max(velocity.y, 0f);
        }
    }
}
