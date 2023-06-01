using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AidPersonFollowTarget : MonoBehaviour
{
    NavMeshAgent agent;
    Animator anim;
    public Transform target;
    public bool liveperson = false;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
        //anim = GetComponentInChildren<Animator>();
    }

    void Update()
    {
        if (liveperson == true)
        {
            anim.SetBool("isStand", true); /* 탈출 지점에 도착하기까지 걷는다. */
            anim.SetBool("isWalk", true); /* 탈출 지점에 도착하기까지 걷는다. */
            agent.SetDestination(target.position); /* 탈출 지점으로 이동한다. */
            anim.SetBool("isIdle", agent.remainingDistance <= 0.1f); /* 탈출 지점에 도착하기까지 걷는다. */
        }
        // anim.SetBool("isStand", agent.remainingDistance <= 0.1f); /* 탈출 지점에 도착하면 걷기를 그만둔다. */
        // anim.SetBool("isWalk", agent.remainingDistance > 0.1f);
    }
}
