using System.Collections;
using UnityEngine;

public class BlockItem : MonoBehaviour
{
    private void Start()
    {
        StartCoroutine(Animate());
    }

    private IEnumerator Animate()
    {
        Rigidbody2D rigid = GetComponent<Rigidbody2D>();
        CircleCollider2D circleCol = GetComponent<CircleCollider2D>();
        BoxCollider2D boxCol = GetComponent<BoxCollider2D>();
        SpriteRenderer spr = GetComponent<SpriteRenderer>();

        rigid.isKinematic = true;
        circleCol.enabled = false;
        boxCol.enabled = false;
        spr.enabled = false;

        yield return new WaitForSeconds(0.25f);

        spr.enabled = true;

        float elapsed = 0f;
        float duration = 0.5f;

        Vector3 startPos = transform.localPosition;
        Vector3 endPos = transform.localPosition + Vector3.up;

        while(elapsed < duration)
        {
            float t = elapsed / duration;

            transform.localPosition = Vector3.Lerp(startPos, endPos, t);
            elapsed += Time.deltaTime;

            yield return null;
        }

        transform.localPosition = endPos;

        rigid.isKinematic = false;
        circleCol.enabled = true;
        boxCol.enabled = true;
    }
}
