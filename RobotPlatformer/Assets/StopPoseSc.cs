using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StopPoseSc : MonoBehaviour {

    [SerializeField]
    private float cinSpeed;    
    private GameObject stopPose;
    private GameObject walkPose;
    public bool isRunning, isWalking;
    //public Transform stopPose;
    [SerializeField]
    private Animator cinAnim;


    private void Start()
    {
        stopPose = GameObject.FindGameObjectWithTag("CinStopPose");
        walkPose = GameObject.FindGameObjectWithTag("WalkStopPose");
    }
    private void Update()
    {
        if (isRunning)
        {
            transform.position = Vector2.MoveTowards(transform.position, stopPose.transform.position, cinSpeed * Time.deltaTime);
        }
        if (Vector2.Distance(transform.position,stopPose.transform.position)<=0.02f)
        {
            cinAnim.ResetTrigger("run");
            cinAnim.SetTrigger("falldown");
        }
        if (isWalking)
        {
            transform.position = Vector2.MoveTowards(transform.position, walkPose.transform.position, 5f * Time.deltaTime);
        }
  
           
       
  
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag=="CinStopPose")
        {
            cinAnim.SetTrigger("falldown");
        }
  
    }
    public void Running()
    {
        isRunning = true;
    }
    public void Walking()
    {
        isWalking = true;
    }
}
