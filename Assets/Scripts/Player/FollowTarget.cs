using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class FollowTarget : MonoBehaviour
{
    NavMeshAgent agent;
    Animator anim;
    public Transform target;
    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        anim = GetComponentInChildren<Animator>(); // 변수 초기화
        anim.SetBool("isWalk", true);
    }

    void Update()
    {
        agent.SetDestination(target.position); /* 탈출 지점으로 이동한다. */
        anim.SetBool("isStand", agent.remainingDistance <= 0.1f); /* 탈출 지점에 도착하면 걷기를 그만둔다. */
        anim.SetBool("isWalk", agent.remainingDistance > 0.1f);
    }
}