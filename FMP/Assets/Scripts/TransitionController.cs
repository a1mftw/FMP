using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
    public GameObject tutorial1;
    public GameObject tutorial2;
    public GameObject tutorial3;
    public Button btnTutorial1;
    public Button btnTutorial2;
    public Button btnTutorial3;


    public void NextTutorial() 
    {
        if (tutorial1.activeSelf)
        {
            SFX_Manager_HR.instance.PlaySFX(SFX_Manager_HR.SoundEffectNames.UIPOSITIVE, transform.position);
            btnTutorial2.Select();
            tutorial1.SetActive(false);
            tutorial2.SetActive(true);
        }
        else if(tutorial2.activeSelf)
        {
            SFX_Manager_HR.instance.PlaySFX(SFX_Manager_HR.SoundEffectNames.UIPOSITIVE, transform.position);
            btnTutorial3.Select();
            tutorial2.SetActive(false);
            tutorial3.SetActive(true);
        }
        else
        {
            SFX_Manager_HR.instance.PlaySFX(SFX_Manager_HR.SoundEffectNames.UIPOSITIVE, transform.position);
            tutorial3.SetActive(false);
            playerMovement.canControl = true;
            Cursor.lockState = CursorLockMode.Locked;
        }
    }
    public void StartGame() 
    {
        SFX_Manager_HR.instance.PlaySFX(SFX_Manager_HR.SoundEffectNames.UIPOSITIVE, transform.position);
        StartCoroutine(StartMenuTransition());
    }

    public void StartBattle() 
    {
        StartCoroutine(StartBattleTransition());
    }

    public void FleeBattle() 
    {
        StartCoroutine(FleeBattleTransition());
    }

    IEnumerator StartMenuTransition() 
    {
        btnTutorial1.Select();
        menuTransition.SetTrigger("Start");
        yield return new WaitForSeconds(menuTransitionTime);
        MainMenu.SetActive(false);
        playerCamera.enabled = true;
        menuTransition.SetTrigger("End");
        tutorial1.SetActive(true);

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
