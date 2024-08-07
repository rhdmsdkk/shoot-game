using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public int enemyCount;
    
    [SerializeField] private GameObject _restartButton;

    private void Update()
    {
        if (enemyCount <= 0)
        {
            StartCoroutine(GameOver());
        }
    }

    private void Awake()
    {
        if (!instance)
        {
            instance = this;
        }

        Time.timeScale = 1f;
    }

    public IEnumerator GameOver()
    {
        yield return new WaitForSeconds(0.35f);
        _restartButton.SetActive(true);
        Time.timeScale = 0f;
    }
}
