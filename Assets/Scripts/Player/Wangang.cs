using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wangang : MonoBehaviour
{
    Animator anim;
    public bool interectwangang = false;

    private void Awake()
    {
        anim = GetComponentInChildren<Animator>();
    }

    void Update()
    {
        if (interectwangang == true)
        {
            anim.SetBool("isRotate", true);
        }
    }
}
