using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cylinder : MonoBehaviour
{
    Animator anim;
    public bool interectcylinder = false;

    private void Awake()
    {
        anim = GetComponentInChildren<Animator>();
    }

    void Update()
    {
        if (interectcylinder == true)
        {
            anim.SetBool("isRotate", true);
        }
    }
}
