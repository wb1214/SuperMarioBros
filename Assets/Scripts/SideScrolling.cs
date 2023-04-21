using UnityEngine;

public class SideScrolling : MonoBehaviour
{
    private Transform player;

    public float height = 6.5f;
    public float undergroundHeight = -9.5f;

    private void Awake()
    {
        player = GameObject.FindWithTag("Player").transform;
    }

    private void LateUpdate()
    {
        Vector3 camPos = transform.position;
        camPos.x = Mathf.Max(camPos.x, player.position.x);
        transform.position = camPos;
    }

    public void SetUnderground(bool _underground)
    {
        Vector3 camPos = transform.position;
        camPos.y = _underground ? undergroundHeight : height;
        transform.position = camPos;
    }
}
