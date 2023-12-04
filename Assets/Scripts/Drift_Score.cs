using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Drift_Score : MonoBehaviour
{
    [SerializeField] private List<WheelCollider> Wheels;
    private bool drifting = false;
    private float drift_multiplyier_point = 0;
    private int drift_multiplyier = 0;
    private int score = 0;
    [SerializeField] private ScoreTotal _scoreTotal;
    [SerializeField] private TMP_Text _text;
    // Start is called before the first frame update
    void Start()
    {
        _text.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        Drift_Modifier();


        HUDUpdate();
    }
    private void FixedUpdate()
    {
        DriftMultiplyier();
        DriftPoint();
    }
    void DriftPoint()
    {
        if (drifting)
        {
            score += 5 * drift_multiplyier;
        }
    }
    void DriftMultiplyier()
    {

        if (drifting)
        {

            drift_multiplyier_point += Drift_Modifier() * Time.deltaTime;
            if (drift_multiplyier_point > 0.8f)
            {
                drift_multiplyier++;
                drift_multiplyier_point = 0;
            }
        }
        else
        {
            _scoreTotal.AddScore(score);
            drift_multiplyier_point = 0;
            score = 0;
            drift_multiplyier = 0;
        }
    }
    float Drift_Modifier()
    {

        foreach (var item in Wheels)
        {
            WheelHit hit;
            item.GetGroundHit(out hit);
            if (!drifting && item.isGrounded && (Mathf.Abs(hit.sidewaysSlip) > 0.6f || Mathf.Abs(hit.forwardSlip) > 0.88f))
            {

                drifting = true;

            }
            else if (drifting && (!item.isGrounded || (Mathf.Abs(hit.sidewaysSlip) < 0.4f && Mathf.Abs(hit.forwardSlip) < 0.88f)))
            {
                drifting = false;

            }

        }
        float driftFactor = 0;
        WheelHit hit2;
        Wheels[2].GetGroundHit(out hit2);
        driftFactor = Mathf.Abs(hit2.sidewaysSlip);
        return driftFactor;



    }
    void HUDUpdate()
    {
        if (drift_multiplyier > 0)
        {
            _text.gameObject.SetActive(true);
            _text.text = drift_multiplyier.ToString() + "X:  " + score.ToString();
        }
        else
        {
            _text.gameObject.SetActive(false);
        }
    }
    public bool GetDriftStatus()
    {
        return drifting;
    }
}
