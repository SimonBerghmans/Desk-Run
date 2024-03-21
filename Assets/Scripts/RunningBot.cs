using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class RunningBot : MonoBehaviour
{
    private AudioSource zombieSource;
    public List<AudioClip> zombieSounds;
    private NavMeshAgent botAgent;
    private Transform playerLocation;
    private float speed;
    private Animator zombieAnimator;
    void Start()
    {
        botAgent = GetComponent<NavMeshAgent>();
        playerLocation = GameObject.Find("Player").transform;
        zombieAnimator = GetComponentInChildren<Animator>();
        SetSpeed();
        botAgent.speed = speed;
        zombieSource = GetComponentInChildren<AudioSource>();
        zombieSource.time = 1.5f;
    }

    // Update is called once per frame
    void FixedUpdate()
    {

        Vector3 direction = Vector3.Normalize(transform.position - playerLocation.transform.position);
        RunAway(direction);
        StartCoroutine(PlaySound());
    }
    private void RunAway(Vector3 dir)
    {
        if (Vector3.Distance(transform.position, playerLocation.position) < 20f)
        {
            botAgent.SetDestination(transform.position + dir * 3);
        }
        else
        {
            botAgent.SetDestination(transform.position);
        }
    }

    
    private void SetSpeed()
    {
        speed = 10;
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
