using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    GameManager() { }

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    [SerializeField] int score;
    [SerializeField] float gameTimeInMin = 2.0f;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(StartCountDown());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void LoadScene(string name) {
        SceneManager.LoadScene(name);
    }

    void GameEnd() {
        LoadScene("End");
    }

    public void GameStart()
    {
        score = 0;
        LoadScene("Gameplay");
        StartCoroutine(StartCountDown());
    }

    IEnumerator MainTimer() {
        yield return new WaitForSeconds(gameTimeInMin * 60);
        GameEnd();
        Debug.Log("Game Over");
    }

    IEnumerator StartCountDown() {
        for (int i = 3; i >= 0; i--) {
            // display
            Debug.Log(i);
            yield return new WaitForSeconds(1);
        }
        StartCoroutine(MainTimer());
    }
}
