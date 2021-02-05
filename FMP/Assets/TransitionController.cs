using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransitionController : MonoBehaviour
{
    public GameObject MainMenu;
    public Camera playerCamera;
    public Camera battleCamera;
    public Animator battleTransition;
    public Animator menuTransition;
    public ThirdPersonMovement playerMovement;
    public float menuTransitionTime = 1f;
    public float battleTransitionTime = 2f;

   


    public void StartGame() 
    {
        StartCoroutine(StartMenuTransition());
    }

    public void StartBattle() 
    {
        StartCoroutine(StartBattleTransition());
    }

    IEnumerator StartMenuTransition() 
    {
        menuTransition.SetTrigger("Start");
        yield return new WaitForSeconds(menuTransitionTime);
        MainMenu.SetActive(false);
        playerCamera.enabled = true;
        playerMovement.canControl = true;
        menuTransition.SetTrigger("End");

    }

    IEnumerator StartBattleTransition()
    {
        battleTransition.SetTrigger("Start");
        yield return new WaitForSeconds(battleTransitionTime);
        playerCamera.enabled = false;
        battleCamera.enabled = true;
        playerMovement.StartBattle();
        battleTransition.SetTrigger("End");


    }




}
