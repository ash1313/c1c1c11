using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorAnimatorParameter1 : MonoBehaviour
{
    public Playerinteraction playerinteraction;
    public Animator animator;

    void Awake()
    {
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (playerinteraction.interectDoor == true && playerinteraction.doorOpen == true)
            animator.SetBool("isOpen", true);
        else if (playerinteraction.interectDoor == true && playerinteraction.doorOpen == false)
            animator.SetBool("isOpen", false);
    }
}