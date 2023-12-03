using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class DeathScreen : MonoBehaviour
{
    private Button Retry;
    // Start is called before the first frame update
    void Start()
    {
        Retry = GetComponent<Button>();
        Retry.onClick.AddListener(TryAgain);
        Time.timeScale = 0;
    }

    // Update is called once per frame
    void Update()
    {

    }
    void TryAgain()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
