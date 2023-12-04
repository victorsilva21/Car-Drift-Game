using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StartGame : MonoBehaviour
{
    private Button _start;
    // Start is called before the first frame update
    void Start()
    {
        _start = GetComponent<Button>();
        _start.onClick.AddListener(PlayButton);
    }

    // Update is called once per frame
    void Update()
    {

    }
    void PlayButton()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

}
