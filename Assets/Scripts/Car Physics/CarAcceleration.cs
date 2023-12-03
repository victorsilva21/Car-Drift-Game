using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UIElements;

public class CarAcceleration : MonoBehaviour
{
    [SerializeField] private Rigidbody _carRigidbody;
    [SerializeField] private float _breakPower = 70000;
    [SerializeField] private float _carSpeed;
    [SerializeField] private float _turnForce;
    [SerializeField] private float _gearPower;
    [SerializeField] private List<WheelCollider> Wheels;
    [SerializeField] private Drift_Score _driftScore;
    [SerializeField] private ScoreTotal _scoreTotal;
    [SerializeField] private ParticleSystem _sfx;
    [SerializeField] private GameObject _DeathScreen;
    private float drag;
    private bool _inReverse = false;
    private bool _inFloor = false;
    private bool _alive = true;



    // Start is called before the first frame update
    void Start()
    {
        drag = Wheels[0].forwardFriction.stiffness;


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
        if (_inFloor && _alive)
        {
            if (Input.GetAxis("Vertical") > 0)
            {
                _inReverse = false;
                _carRigidbody.AddForce(-transform.forward * Input.GetAxis("Vertical") * CarGear() * Time.deltaTime, ForceMode.Acceleration);
            }


            if (Input.GetAxis("Vertical") < 0 && _carSpeed > 2f)
                _carRigidbody.AddForce(-_carRigidbody.velocity * Input.GetAxis("Vertical") * Time.deltaTime * _breakPower * 0.25f, ForceMode.Force);
            else if (Input.GetAxis("Vertical") < 0)
            {
                _inReverse = true;
                _carRigidbody.AddForce(-transform.forward * Input.GetAxis("Vertical") * CarGear() * Time.deltaTime, ForceMode.Acceleration);
            }
            if (Input.GetKey(KeyCode.Space) && _carSpeed > 3.5f)
                HandBreak();
            else if (Input.GetKey(KeyCode.Space))
            {
                foreach (var item in Wheels)
                {
                    WheelFrictionCurve frictionCurve = item.forwardFriction;
                    frictionCurve.stiffness = drag;
                    item.forwardFriction = frictionCurve;
                }
                _carRigidbody.velocity = Vector3.zero;


            }


            else if (Input.GetKeyUp(KeyCode.Space))
            {
                foreach (var item in Wheels)
                {
                    WheelFrictionCurve frictionCurve = item.forwardFriction;
                    frictionCurve.stiffness = drag;
                    item.forwardFriction = frictionCurve;
                }
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
    void HandBreak()
    {
        foreach (var item in Wheels)
        {
            WheelFrictionCurve frictionCurve = item.forwardFriction;
            frictionCurve.stiffness = 0;
            item.forwardFriction = frictionCurve;
        }
        _carRigidbody.AddForce(_carRigidbody.velocity * Time.deltaTime * _breakPower * 0.75f, ForceMode.Force);

    }
    float CarTurn()
    {
        switch (_carSpeed)
        {
            default:
                _turnForce = 14000;
                break;
            case float when _carSpeed > 8.3f && _carSpeed < 20:
                _turnForce = 14000;
                break;
            case float when _carSpeed > 20 && _carSpeed < 27.7f:
                _turnForce = 14000;
                break;
            case float when _carSpeed > 27.7f && _carSpeed < 50:
                _turnForce = 13000;
                break;
            case float when _carSpeed > 50:
                _turnForce = 13000;
                break;
        }
        return _turnForce;
    }

    float CarGear()
    {

        switch (_carSpeed)
        {

            default:
                _gearPower = 1250;
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
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 7 && _driftScore.GetDriftStatus())
        {
            other.gameObject.SendMessage("Death");
            _scoreTotal.AddScore(250);


        }
        else if (other.gameObject.layer == 7)
        {
            other.gameObject.SendMessage("Killed");
            if (_alive)
                _sfx.Play();
            _alive = false;
            StartCoroutine(DeathSequence());

        }

    }
    private IEnumerator DeathSequence()
    {
        yield return new WaitForSeconds(3);
        _DeathScreen.SetActive(true);
    }
}
