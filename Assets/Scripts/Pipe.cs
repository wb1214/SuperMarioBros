using System.Collections;
using UnityEngine;

public class Pipe : MonoBehaviour
{
    public Transform connection;
    public KeyCode enterKeyCode = KeyCode.S;
    public Vector3 enterDir = Vector3.down;
    public Vector3 exitDir = Vector3.zero;

    private void OnTriggerStay2D(Collider2D _col)
    {
        if(connection != null && _col.CompareTag("Player"))
        {
            if(Input.GetKeyDown(enterKeyCode))
            {
                StartCoroutine(Enter(_col.transform));
            }
        }
    }

    private IEnumerator Enter(Transform _player)
    {
        _player.GetComponent<PlayerMovement>().enabled = false;

        Vector3 enteredPos = transform.position + enterDir;
        Vector3 enteredScale = Vector3.one * 0.5f;

        yield return Move(_player, enteredPos, enteredScale);
        yield return new WaitForSeconds(1f);

        bool underground = connection.position.y < 0f;
        Camera.main.GetComponent<SideScrolling>().SetUnderground(underground);

        if(exitDir != Vector3.zero)
        {
            _player.position = connection.position - exitDir;
            yield return Move(_player, connection.position + exitDir, Vector3.one);
        }
        else
        {
            _player.position = connection.position;
            _player.localScale = Vector3.one;
        }

        _player.GetComponent<PlayerMovement>().enabled = true;
    }

    private IEnumerator Move(Transform _player, Vector3 _endPos, Vector3 _endScale)
    {
        float elapsed = 0f;
        float duration = 1f;

        Vector3 startPos = _player.position;
        Vector3 startScale = _player.localScale;

        while(elapsed < duration)
        {
            float t = elapsed / duration;

            _player.position = Vector3.Lerp(startPos, _endPos, t);
            _player.localScale = Vector3.Lerp(startScale, _endScale, t);
            elapsed += Time.deltaTime;

            yield return null;
        }

        _player.position = _endPos;
        _player.localScale = _endScale;
    }
}
