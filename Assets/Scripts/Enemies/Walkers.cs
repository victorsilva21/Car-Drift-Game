using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Walkers : MonoBehaviour
{
    private NavMeshAgent _agent;
    private Animator _animation;
    private bool _alive = true;
    private bool _chasing = false;
    [SerializeField] private Transform _destiny;
    [SerializeField] private ParticleSystem _sfx;
    // Start is called before the first frame update
    void Start()
    {
        _agent = GetComponent<NavMeshAgent>();
        _animation = GetComponent<Animator>();
        _sfx = GetComponentInChildren<ParticleSystem>();
        _animation.SetBool("Chasing", false);
        _agent.speed = 12;
        StartCoroutine(_chaseStart());

    }

    // Update is called once per frame
    void Update()
    {
        if (_alive && _chasing)
            _agent.destination = _destiny.position;
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
        GetComponent<CapsuleCollider>().isTrigger = false;
        _animation.SetBool("Chasing", false);
        _agent.speed = 0;
        _animation.SetTrigger("Attack");
        _alive = false;

        _agent.destination = transform.position;
    }
    private IEnumerator _chaseStart()
    {
        yield return new WaitUntil(() => Vector3.Distance(_destiny.position, transform.position) < 15);
        _chasing = true;
        _animation.SetBool("Chasing", true);
    }
}
