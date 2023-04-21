using System.Collections;
using UnityEngine;

public class FlagPole : MonoBehaviour
{
    public Transform flag;
    public Transform poleBottom;
    public Transform castle;
    
    public float speed = 6f;
    public int nextWorld = 1;
    public int nextStage = 1;


    private void OnTriggerEnter2D(Collider2D _col)
    {
        if(_col.CompareTag("Player"))
        {
            StartCoroutine(MoveTo(flag, poleBottom.position));
            StartCoroutine(LevelClear(_col.transform));
        }
    }

    private IEnumerator LevelClear(Transform _player)
    {
        _player.GetComponent<PlayerMovement>().enabled = false;

        yield return MoveTo(_player, poleBottom.position);
        yield return MoveTo(_player, _player.position + Vector3.right);
        yield return MoveTo(_player, _player.position + Vector3.right + Vector3.down);
        yield return MoveTo(_player, castle.position);

        _player.gameObject.SetActive(false);

        yield return new WaitForSeconds(2f);

        GameManager.instance.LoadLevel(nextWorld, nextStage);
    }

    private IEnumerator MoveTo(Transform _subject, Vector3 _destination)
    {
        while(Vector3.Distance(_subject.position, _destination) > 0.125f)
        {
            _subject.position = Vector3.MoveTowards(_subject.position, _destination, speed * Time.deltaTime);
            yield return null;
        }

        _subject.position = _destination;
    }
}
