using System;
using UnityEngine;

public class CharacterManager : MonoBehaviour
{
    private static CharacterManager _instance;
    public event Action<int> getCard;
    public static CharacterManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = new GameObject("CharacterManager").AddComponent<CharacterManager>();
            }
            return _instance;
        }
    }

    public Player _player;

    public Player Player
    {
        get { return _player; }
        set { _player = value; }
    }

    private void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else if (_instance != this)
        {
            if (_instance == this)
            {
                Destroy(gameObject);
            }
        }
    }

    public void GetCard(Keycard index)
    {
        Debug.Log(index.ToString());
        Player.key[(int)index] = true;
        getCard?.Invoke((int)index);
    }
}

