using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicChangerSc : MonoBehaviour {

    private static MusicChangerSc instance;
    public static MusicChangerSc Instance
    {
        get
        {
            if (instance == null)
            {
                instance = GameObject.FindObjectOfType<MusicChangerSc>();
            }
            return instance;
        }
    }


    [SerializeField]
    private Animator animator;  
    public void FadeToLevel()
    {
       
        animator.SetTrigger("fade");
    }

}
