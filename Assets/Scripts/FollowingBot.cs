using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class FollowingBot : MonoBehaviour
{
    private AudioSource zombieSource;
    public List <AudioClip> zombieSounds;
    private NavMeshAgent botAgent;
    private Transform playerLocation;
    private float speed;
    private Animator zombieAnimator;
    void Start()
    {
        botAgent = GetComponent<NavMeshAgent>();
        playerLocation = GameObject.Find("Player").transform;
        zombieAnimator = GetComponentInChildren<Animator>();
        SetRandomSpeed();
        botAgent.speed = speed;
        zombieSource = GetComponentInChildren<AudioSource>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Vector3 direction = playerLocation.transform.position;
        FollowPlayer(direction);
        StartCoroutine(PlaySound());
    }
    private void FollowPlayer(Vector3 dir)
    {
        botAgent.SetDestination(dir);
    }

    private void SetRandomSpeed()
    {
        speed = Random.Range(3, 10);
        zombieAnimator.SetFloat("RunSpeed", speed/3);
    }
    IEnumerator PlaySound()
    {
        int randomSeconds = Random.Range(5, 10);
        yield return new WaitForSeconds(randomSeconds*Time.deltaTime);
        int randomSound = Random.Range(0, zombieSounds.Count);
        if (!zombieSource.isPlaying)
        {
            zombieSource.clip = zombieSounds[randomSound];
            zombieSource.Play();
        }
    }
}
