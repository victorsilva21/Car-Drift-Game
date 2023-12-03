using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class HiScore : MonoBehaviour
{

    private TMP_Text _text;
    // Start is called before the first frame update
    void Start()
    {

        _text = GetComponent<TMP_Text>();
        if (PlayerPrefs.HasKey("HiScore"))
            _text.text = "Hi-Score: " + PlayerPrefs.GetInt("HiScore").ToString();
        else
        {
            _text.text = "Hi-Score: " + "0";
        }
    }
    public void UpdateHiScore()
    {
        if (PlayerPrefs.HasKey("HiScore"))
            _text.text = "Hi-Score: " + PlayerPrefs.GetInt("HiScore").ToString();
        else
        {
            _text.text = "Hi-Score: " + "0";
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
