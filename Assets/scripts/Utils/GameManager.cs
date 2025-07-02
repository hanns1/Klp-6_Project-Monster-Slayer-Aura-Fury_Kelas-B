using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public float gameTime;
    public bool gameActive;

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
    }

    void Start()
    {
        gameActive = true;
    }

    void Update()
    {
        if (gameActive)
        {
            gameTime += Time.deltaTime;
            UIController.Instance.UpdateTimer(gameTime);
        }
    }
}