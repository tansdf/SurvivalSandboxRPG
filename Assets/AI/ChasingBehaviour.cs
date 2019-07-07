using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChasingBehaviour : StateMachineBehaviour
{
    Transform playerPos;
    public float chaseDist, attackDist;
    public float speed;
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.SetBool("isChasing", true);
        playerPos = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if(Vector2.Distance(playerPos.position, animator.transform.position) < attackDist)
        {
            animator.SetTrigger("Attack");
        }
        else if(Vector2.Distance(playerPos.position, animator.transform.position) > chaseDist)
        {
            animator.SetBool("isChasing", false);
        }
        else
        {
            animator.transform.position = Vector2.MoveTowards(animator.transform.position, playerPos.position, speed * Time.deltaTime);
            animator.SetFloat("Horizontal", (playerPos.position-animator.transform.position).normalized.x);
            animator.SetFloat("Vertical", (playerPos.position-animator.transform.position).normalized.y);
        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
       
    }

    // OnStateMove is called right after Animator.OnAnimatorMove()
    //override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that processes and affects root motion
    //}

    // OnStateIK is called right after Animator.OnAnimatorIK()
    //override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that sets up animation IK (inverse kinematics)
    //}
}
