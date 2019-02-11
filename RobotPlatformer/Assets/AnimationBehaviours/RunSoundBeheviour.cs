using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunSoundBeheviour : StateMachineBehaviour {

    private float waitForRunSound;
    [SerializeField]
    private float startWaitForRunSound=0.75f;
    [SerializeField]
    private GameObject runSound;
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        waitForRunSound = startWaitForRunSound;
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (waitForRunSound<=0 && Player.Instance.OnGround)
        {
            GameObject runSoundEffect = Instantiate(runSound);            
            waitForRunSound = startWaitForRunSound;
            Destroy(runSoundEffect,1f);
        }
        else
        {
            waitForRunSound -= Time.deltaTime;
        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    //override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
    //
    //}

    // OnStateMove is called right after Animator.OnAnimatorMove(). Code that processes and affects root motion should be implemented here
    //override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
    //
    //}

    // OnStateIK is called right after Animator.OnAnimatorIK(). Code that sets up animation IK (inverse kinematics) should be implemented here.
    //override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
    //
    //}
}
