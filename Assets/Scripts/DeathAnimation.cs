using UnityEngine;
using System.Collections;

public class DeathAnimation : MonoBehaviour
{
    public SpriteRenderer spr;
    public Sprite deadSprite;

    private void Reset()
    {
        spr = GetComponent<SpriteRenderer>();
    }

    private void OnEnable()
    {
        UpdateSprite();
        DisablePhysics();
        StartCoroutine(Animate());
    }

    private void UpdateSprite()
    {
        spr.enabled = true;
        spr.sortingOrder = 10;

        if(deadSprite != null)
        {
            spr.sprite = deadSprite;
        }
        
    }

    private void DisablePhysics()
    {
        Collider2D[] cols = GetComponents<Collider2D>();

        foreach(Collider2D col in cols)
        {
            col.enabled = false;
        }
        GetComponent<Rigidbody2D>().isKinematic = true;
        PlayerMovement playerMovement = GetComponent<PlayerMovement>();
        EntityMovement entityMovement = GetComponent<EntityMovement>();

        if(playerMovement != null)
        {
            playerMovement.enabled = false;
        }

        if (entityMovement != null)
        {
            entityMovement.enabled = false;
        }
    }

    private IEnumerator Animate()
    {
        float elapsed = 0f;
        float duration = 3f;

        float jumpVelocity = 10f;
        float gravity = -36f;

        Vector3 velocity = Vector3.up * jumpVelocity;

        while(elapsed < duration)
        {
            transform.position += velocity * Time.deltaTime;
            velocity.y += gravity * Time.deltaTime;
            elapsed += Time.deltaTime;
            yield return null;
        }    

        
    }

}
