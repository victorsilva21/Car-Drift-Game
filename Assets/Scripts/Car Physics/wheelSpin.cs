using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class wheelSpin : MonoBehaviour
{
    [SerializeField] private CarAcceleration _carAcc;
    [SerializeField] private float _speedFactor = 10;
    private WheelCollider wheelCollider;
    [SerializeField] private bool backWheel = false;
    [SerializeField] private bool frontWheel = false;
    // Start is called before the first frame update
    void Start()
    {
        wheelCollider = GetComponent<WheelCollider>();

    }

    // Update is called once per frame
    void Update()
    {
        /* transform.Rotate(_carAcc.CarSpeed() * Time.deltaTime * _speedFactor, 0, 0);
        */
        if (backWheel)
            _carAcc.SetInFloor(wheelCollider.isGrounded);
        /* if (frontWheel && Input.GetAxis("Horizontal") != 0)
        {
            if (Input.GetAxis("Horizontal") == 1 )
                transform.RotateAround(transform.position, new Vector3(0, 1, 0), Input.GetAxis("Horizontal"));
                
        } */
    }
    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.layer == 3)
        {

            Debug.Log("infloor");
        }
    }
    private void OnCollisionExit(Collision other)
    {
        if (other.gameObject.layer == 3)
        {
            _carAcc.SetInFloor(false);
            Debug.Log("outfloor");
        }
    }
}
