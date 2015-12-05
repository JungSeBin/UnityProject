using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class EnemyMove : MonoBehaviour 
{
	public Transform target;

	NavMeshAgent agent;

    Animator animator;
    public AudioClip walkClip;
    public AudioClip growlClip;

    ENEMYSTATE enemyState = ENEMYSTATE.IDLE;
    delegate void StateFunc();
    Dictionary<ENEMYSTATE, Action> dicState = new Dictionary<ENEMYSTATE, Action>();

    static float idleStateTime = 2.2f;
    static float patrolStateTime = 3.0f;
    Vector3 myPosition;
    Vector3 startPosition;
    Vector3 patrolDestination;

    float detectDistance = 0.0f;
    float patrolDistance = 0.0f;
    float stopDistance = 0.5f;
    float detectRange = 10.0f;
    float patrolRange = 20.0f;
    float stateTime = 0.0f;

	void Start () 
	{
		agent = gameObject.GetComponent<NavMeshAgent>();
        animator = gameObject.GetComponent<Animator>();
        startPosition = this.transform.position;

        dicState[ENEMYSTATE.IDLE] = Idle;
        dicState[ENEMYSTATE.PATROL] = Patrol;
        dicState[ENEMYSTATE.CHASE] = Chase;

        StartCoroutine(Wander(3.0f));
        StartCoroutine(WalkSound(0.5f));
    }
	
	// Update is called once per frame
	void Update ()
	{
        Detect();
        dicState[enemyState]();
	}

    void Idle()
    {
        stateTime += Time.deltaTime;

        animator.SetTrigger("idle");

        if(stateTime > idleStateTime)
        {
            stateTime = 0.0f;
            enemyState = ENEMYSTATE.PATROL;
        }
    }

    void Patrol()
    {
        animator.SetTrigger("walk");
        patrolDistance = (patrolDestination - myPosition).magnitude;
        if (patrolDistance < stopDistance)
        {
            Debug.Log("Stop");
            enemyState = ENEMYSTATE.IDLE;
        }
    }

    IEnumerator Wander(float waitTime)
    {
        while (true)
        {
            if (enemyState == ENEMYSTATE.PATROL)
            {
                patrolDestination = startPosition + new Vector3(UnityEngine.Random.Range(-patrolRange, patrolRange),
                                                                   0,
                                                                   UnityEngine.Random.Range(-patrolRange, patrolRange));
                agent.SetDestination(patrolDestination);
                yield return new WaitForSeconds(waitTime);
            }
            yield return null;
        }
    }

    IEnumerator WalkSound(float waitTime)
    {
        while(true)
        {
            if(enemyState != ENEMYSTATE.IDLE)
            {
                GetComponent<AudioSource>().PlayOneShot(walkClip);
            }
            yield return new WaitForSeconds(waitTime);
        }
    }
    void Chase()
    {
        animator.SetTrigger("run");
        agent.SetDestination(target.position);
    }

    void Detect()
    {
        if (enemyState == ENEMYSTATE.CHASE)
            return;

        myPosition = this.transform.position;

        detectDistance = (target.position - myPosition).magnitude;
        if (detectDistance <= detectRange)
        {
            Debug.Log("Detect");
            enemyState = ENEMYSTATE.CHASE;
            agent.speed = 11.0f;
            GetComponent<AudioSource>().PlayOneShot(growlClip);
            return;
        }
    }
}
