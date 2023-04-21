using System.Collections;
using UnityEngine;

public class PowerUp : MonoBehaviour
{
    public enum Type
    {
        Coin,
        ExtraLife,
        MagicMushroom,
        Starpower
    }

    public Type type;


    private void OnTriggerEnter2D(Collider2D _col)
    {
        if(_col.CompareTag("Player"))
        {
            Collect(_col.gameObject);
        }
    }

    private void Collect(GameObject _player)
    {
        switch(type)
        {
            case Type.Coin:
                GameManager.instance.AddCoin();
                break;
            case Type.ExtraLife:
                GameManager.instance.AddLife();
                break;
            case Type.MagicMushroom:
                _player.GetComponent<Player>().Grow();
                break;
            case Type.Starpower:
                _player.GetComponent<Player>().StarPower();
                break;
        }

        Destroy(gameObject);
    }
}
