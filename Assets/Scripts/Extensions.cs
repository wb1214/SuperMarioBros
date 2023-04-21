using UnityEngine;

public static class Extensions
{
    private static LayerMask layerMask = LayerMask.GetMask("Default");
    public static bool Raycast(this Rigidbody2D _rigid, Vector2 _dir)
    {
        if(_rigid.isKinematic)
        {
            return false;
        }

        float radius = 0.25f;
        float distance = 0.375f;

        RaycastHit2D hit = Physics2D.CircleCast(_rigid.position, radius, _dir.normalized, distance, layerMask);
        return hit.collider != null && hit.rigidbody != _rigid;
    }

    public static bool DotTest(this Transform _transform, Transform _other, Vector2 _testDirection)
    {
        Vector2 dir = _other.position - _transform.position;
        return Vector2.Dot(dir.normalized, _testDirection) > 0.25f;
    }
}
