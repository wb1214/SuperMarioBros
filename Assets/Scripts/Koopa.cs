using UnityEngine;

public class Koopa : MonoBehaviour
{
    public Sprite shellSprite;
    public float shellSpeed = 12f;

    private bool shelled;
    private bool pushed;

    private void OnCollisionEnter2D(Collision2D _col)
    {
        if (!shelled && _col.gameObject.CompareTag("Player"))
        {
            Player player = _col.gameObject.GetComponent<Player>();
            if(player.starPower)
            {
                Hit();
            }
            else if (_col.transform.DotTest(transform, Vector2.down))
            {
                EnterShell();
            }
            else
            {
                player.Hit();
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D _col)
    {
        if (shelled && _col.CompareTag("Player"))
        {
            if (!pushed)
            {
                Vector2 dir = new Vector2(transform.position.x - _col.transform.position.x, 0f);
                PushShell(dir);
            }
            else
            {
                Player player = _col.GetComponent<Player>();

                if(player.starPower)
                {
                    Hit();
                }
                else
                {
                    player.Hit();
                }
                
            }
        }
        else if (!shelled && _col.gameObject.layer == LayerMask.NameToLayer("Shell"))
        {
            Hit();
        }
    }

    private void EnterShell()
    {
        shelled = true;

        GetComponent<EntityMovement>().enabled = false;
        GetComponent<AnimatedSprite>().enabled = false;
        GetComponent<SpriteRenderer>().sprite = shellSprite;
        
    }

    private void PushShell(Vector2 _dir)
    {
        pushed = true;

        GetComponent<Rigidbody2D>().isKinematic = false;
        
        EntityMovement movement = GetComponent<EntityMovement>();
        movement.dir = _dir.normalized;
        movement.speed = shellSpeed;
        movement.enabled = true;

        gameObject.layer = LayerMask.NameToLayer("Shell");
    }

    private void Hit()
    {
        GetComponent<AnimatedSprite>().enabled = false;
        GetComponent<DeathAnimation>().enabled = true;
        Destroy(gameObject, 3f);
    }

    private void OnBecameInvisible()
    {
        if(pushed)
        {
            Destroy(gameObject);
        }
    }
}
