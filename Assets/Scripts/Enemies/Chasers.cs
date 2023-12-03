using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Chasers : MonoBehaviour
{
    private NavMeshAgent _agent;
    private Animator _animation;
    private bool _alive = true;
    [SerializeField] private Transform _destiny;
    [SerializeField] private ParticleSystem _sfx;
    // Start is called before the first frame update
    void Start()
    {
        _agent = GetComponent<NavMeshAgent>();
        _animation = GetComponent<Animator>();
        _sfx = GetComponentInChildren<ParticleSystem>();
        _animation.SetBool("Chasing", true);
        _agent.speed = 12;
    }

    // Update is called once per frame
    void Update()
    {
        if (_alive)
            _agent.destination = _destiny.position;
        else
        {
            _animation.SetBool("Chasing", false);
            _agent.speed = 0;
        }
    }
    void Death()
    {
        _sfx.Play();

        GetComponent<CapsuleCollider>().isTrigger = true;
        _agent.speed = 0;
        _alive = false;
        _agent.enabled = false;
        _animation.SetBool("Death", true);
    }
    void Killed()
    {
        _animation.SetBool("Chasing", false);
        _agent.speed = 0;
        _animation.SetTrigger("Attack");
        _alive = false;

        _agent.destination = transform.position;
    }

}
