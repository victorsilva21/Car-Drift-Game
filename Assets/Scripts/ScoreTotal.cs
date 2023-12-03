using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreTotal : MonoBehaviour
{
    private int Score = 0;
    private TMP_Text _text;
    // Start is called before the first frame update
    void Start()
    {
        _text = GetComponent<TMP_Text>();
        _text.text = "Score: " + Score.ToString();
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void AddScore(int number)
    {
        Score += number;
        _text.text = "Score: " + Score.ToString();
    }
    public int GetScore()
    {
        return Score;
    }
}
