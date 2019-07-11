using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WolfWalkingBehaviour : StateMachineBehaviour
{
    Vector2 prevPos;
    public float speed;
    Vector2 destination;
    int counter;
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        prevPos = animator.transform.position;
        destination = Random.insideUnitCircle * 5;
        counter = 0;
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        Debug.Log(Time.deltaTime);
        if(++counter == 5)
        {
            counter = 0;
            prevPos = animator.transform.position;
        }

        if(counter == 4)
        {
            Debug.Log(Vector2.Distance(prevPos, animator.transform.position));
        }

        if(Vector2.Distance(animator.transform.position, destination) < 0.1 || (Vector2.Distance(prevPos, animator.transform.position) < 0.003 * speed && counter == 4))
        {
            animator.SetBool("isWalking", false);
        }
        animator.transform.position = Vector2.MoveTowards(animator.transform.position, destination, speed * Time.deltaTime);
        animator.SetFloat("Horizontal", (destination-(Vector2)animator.transform.position).normalized.x);
        animator.SetFloat("Vertical", (destination-(Vector2)animator.transform.position).normalized.y);
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
       
    }
}
