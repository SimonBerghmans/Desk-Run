using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class WanderingBot : MonoBehaviour
{
    private AudioSource zombieSource;
    public List<AudioClip> zombieSounds;
    private NavMeshAgent botAgent;
    private float speed;
    private float wanderTime = 5f;
    private float timeTillWander;
    private Animator zombieAnimator;
    void Start()
    {
        botAgent = GetComponent<NavMeshAgent>();
        zombieAnimator = GetComponentInChildren<Animator>();
        SetRandomSpeed();
        botAgent.speed = speed;
        zombieSource = GetComponentInChildren<AudioSource>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Wander();
        StartCoroutine(PlaySound());
    }
    private void Wander()
    {
        timeTillWander += Time.deltaTime;

        if
            (timeTillWander >= wanderTime)
        {
            Vector3 randomDirection = Random.insideUnitSphere * 100;
            randomDirection += transform.position;

            NavMeshHit navHit;

            NavMesh.SamplePosition(randomDirection, out navHit, 100, NavMesh.AllAreas);
            botAgent.SetDestination(navHit.position);
            timeTillWander = 0;
        }
    }

    private void SetRandomSpeed()
    {
        speed = Random.Range(3, 10);
        zombieAnimator.SetFloat("RunSpeed", speed / 3);
    }
    IEnumerator PlaySound()
    {
        int randomSeconds = Random.Range(5, 10);
        yield return new WaitForSeconds(randomSeconds * Time.deltaTime);
        int randomSound = Random.Range(0, zombieSounds.Count);
        if (!zombieSource.isPlaying)
        {
            zombieSource.clip = zombieSounds[randomSound];
            zombieSource.Play();
        }
    }
    
}
