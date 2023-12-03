using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Finish : MonoBehaviour
{
    [SerializeField] private ScoreTotal _score;

    [SerializeField] private GameObject _baseHud, _finishHud;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.layer == 6)
        {
            if (PlayerPrefs.HasKey("HiScore") == false || PlayerPrefs.GetInt("HiScore") < _score.GetScore())
                PlayerPrefs.SetInt("HiScore", _score.GetScore());
            PlayerPrefs.Save();
            StartCoroutine(FinishSequence());

        }
    }
    private IEnumerator FinishSequence()
    {
        yield return new WaitForSeconds(3);
        _baseHud.SetActive(false);
        _finishHud.SetActive(true);
    }
}
