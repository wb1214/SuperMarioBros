using UnityEngine;

public class DeathBarrier : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D _col)
    {
        if(_col.CompareTag("Player"))
        {
            _col.gameObject.SetActive(false);
            GameManager.instance.ResetLevel(3f);
        }
        else
        {
            Destroy(_col.gameObject);
        }
    }
}
