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
    public BattleSystem battleSystemController;
    public float menuTransitionTime = 1f;
    public float battleTransitionTime = 2f;

   


    public void StartGame() 
    {
        StartCoroutine(StartMenuTransition());
    }

    public void StartBattle(GameObject enemy) 
    {
        battleSystemController.enemyTarget = enemy;
        StartCoroutine(StartBattleTransition());
    }

    public void FleeBattle() 
    {
        StartCoroutine(FleeBattleTransition());
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
        battleTransition.SetTrigger("End");
        yield return new WaitForSeconds(menuTransitionTime);
        battleSystemController.SetupBattle();
    }

    IEnumerator FleeBattleTransition() 
    {
        battleSystemController.EndBattle();
        menuTransition.SetTrigger("Start");
        yield return new WaitForSeconds(menuTransitionTime);
        playerMovement.ResumeTravel();
        playerCamera.enabled = true;
        battleCamera.enabled = false;
        menuTransition.SetTrigger("End");
       
    }




}
