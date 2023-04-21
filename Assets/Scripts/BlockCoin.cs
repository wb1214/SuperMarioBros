using System.Collections;
using UnityEngine;

public class BlockCoin : MonoBehaviour
{
    private void Start()
    {
        GameManager.instance.AddCoin();

        StartCoroutine(Animate());
    }

    private IEnumerator Animate()
    {

        Vector3 restingPos = transform.localPosition;
        Vector3 animatedPos = restingPos + Vector3.up * 2f;

        yield return Move(restingPos, animatedPos);
        yield return Move(animatedPos, restingPos);

        Destroy(gameObject);
    }

    private IEnumerator Move(Vector3 _from, Vector3 _to)
    {
        float elapsed = 0f;
        float duration = 0.25f;

        while (elapsed < duration)
        {
            float t = elapsed / duration;

            transform.localPosition = Vector3.Lerp(_from, _to, t);
            elapsed += Time.deltaTime;

            yield return null;
        }

        transform.localPosition = _to;
    }
}
