using UnityEngine;
using Player;

public class MonoHelper : MonoBehaviour
{
    private static PlayerController _player;
    public PlayerController player
    {
        get
        {
            if (_player == null)
                _player = FindObjectOfType<PlayerController>();
            return _player;
        }
    }

    private static CameeraShake _shaker;
    public CameeraShake shaker
    {
        get
        {
            if (_shaker == null)
                _shaker = FindObjectOfType<CameeraShake>();
            return _shaker;
        }
    }
}