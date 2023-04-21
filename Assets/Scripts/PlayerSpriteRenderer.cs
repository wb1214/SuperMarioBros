using UnityEngine;

public class PlayerSpriteRenderer : MonoBehaviour
{
    public SpriteRenderer spr { get; private set; }
    private PlayerMovement movement;

    public Sprite idle;
    public Sprite jump;
    public Sprite slide;
    public AnimatedSprite run;

    private void Awake()
    {
        spr = GetComponent<SpriteRenderer>();
        movement = GetComponentInParent<PlayerMovement>();
    }

    private void OnEnable()
    {
        spr.enabled = true;
    }

    private void OnDisable()
    {
        spr.enabled = false;
        run.enabled = false;
    }
    private void LateUpdate()
    {
        run.enabled = movement.running;
        if(movement.jumping)
        {
            spr.sprite = jump;
        }
        else if(movement.sliding)
        {
            spr.sprite = slide;
        }
        else if(!movement.running)
        {
            spr.sprite = idle;
        }
    }
}
