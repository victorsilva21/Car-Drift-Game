using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
[RequireComponent(typeof(TMP_Text))]
public class SpeedMeter : MonoBehaviour
{
    [SerializeField] private CarAcceleration _carAcc;
    private TMP_Text _text;
    // Start is called before the first frame update
    void Start()
    {
        _text = GetComponent<TMP_Text>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        _text.text = _carAcc.CarSpeed().ToString("0") + " Km/h";
    }
}
