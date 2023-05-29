using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class main : MonoBehaviour
{
    public GameObject PC_Player;
    public GameObject VR_Player;
    
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            SceneManager.LoadScene("Office building");
            
            PC_Player.SetActive(false);
            VR_Player.SetActive(true);
        }

        else if(Input.GetMouseButtonDown(1))
        {
            SceneManager.LoadScene("Office building");

            PC_Player.SetActive(true);
            VR_Player.SetActive(false);
        }
    }
    
    public void SceneChange()
    {
        SceneManager.LoadScene("Office building");
    }
}
