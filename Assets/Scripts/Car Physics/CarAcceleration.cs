using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CarAcceleration : MonoBehaviour
{
    [SerializeField] private Rigidbody _carRigidbody;
    [SerializeField] private float _breakPower = 70000;
    [SerializeField] private float _carSpeed;
    [SerializeField] private float _turnForce;
    [SerializeField] private float _gearPower;
    private bool _inReverse = false;
    private bool _inFloor = false;
    [SerializeField] private Vector3 _aaaaaa;


    // Start is called before the first frame update
    void Start()
    {

    }
    public void SetInFloor(bool inFloor = true)
    {
        _inFloor = inFloor;
    }
    public float CarSpeed()
    {
        float carSpeed = _carSpeed * 18 / 5;
        return carSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        //_carRigidbody.centerOfMass = _aaaaaa;
        _carSpeed = Vector3.Magnitude(_carRigidbody.velocity);
        if (_inFloor)
        {
            if (Input.GetAxis("Vertical") > 0)
            {
                _inReverse = false;
                _carRigidbody.AddForce(-transform.forward * Input.GetAxis("Vertical") * CarGear() * Time.deltaTime, ForceMode.Acceleration);
            }


            if (Input.GetAxis("Vertical") < 0 && _carSpeed > 3f)
                _carRigidbody.AddForce(-_carRigidbody.velocity * Input.GetAxis("Vertical") * Time.deltaTime * _breakPower, ForceMode.Force);
            else if (Input.GetAxis("Vertical") < 0)
            {
                _inReverse = true;
                _carRigidbody.AddForce(-transform.forward * Input.GetAxis("Vertical") * CarGear() * Time.deltaTime, ForceMode.Acceleration);
            }

            if (_inReverse)
            {
                _carRigidbody.AddTorque(new Vector3(0, 1, 0) * -1 * Input.GetAxis("Horizontal") * Time.deltaTime * CarTurn(), ForceMode.Force);
            }
            else
            {
                _carRigidbody.AddTorque(new Vector3(0, 1, 0) * Input.GetAxis("Horizontal") * Time.deltaTime * CarTurn(), ForceMode.Force);
            }


            /*     if (_carSpeed > 10 && Input.GetAxis("Vertical") == 0)
                {
                    _carRigidbody.AddForce(-1 * transform.forward * CarGear() / 2 * Time.deltaTime, ForceMode.Acceleration);
                } */

        }

    }
    float CarTurn()
    {
        switch (_carSpeed)
        {
            default:
                _turnForce = 10000;
                break;
            case float when _carSpeed > 8.3f && _carSpeed < 20:
                _turnForce = 10000;
                break;
            case float when _carSpeed > 20 && _carSpeed < 27.7f:
                _turnForce = 10150;
                break;
            case float when _carSpeed > 27.7f && _carSpeed < 50:
                _turnForce = 10150;
                break;
            case float when _carSpeed > 50:
                _turnForce = 10200;
                break;
        }
        return _turnForce;
    }

    float CarGear()
    {

        switch (_carSpeed)
        {

            default:
                _gearPower = 1000;
                break;
            case float when _carSpeed > 2f && _carSpeed < 15:
                _gearPower = 350;
                break;
            case float when _carSpeed > 15 && _carSpeed < 27.7f:
                _gearPower = 300;
                break;
            case float when _carSpeed > 27.7f && _carSpeed < 50:
                _gearPower = 250;
                break;
            case float when _carSpeed > 50:
                _gearPower = 200;
                break;
        }
        return _gearPower;
    }
}
