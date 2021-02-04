using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransitionController : MonoBehaviour
{

    public Animator transition;
    public float transitionTime = 1f;

    public void StartGame() 
    {

        StartCoroutine(StartGameTransition());
    
    }

    IEnumerator StartGameTransition() 
    {
        transition.SetTrigger("Start");

        yield return new WaitForSeconds(transitionTime);
    

    }




}
