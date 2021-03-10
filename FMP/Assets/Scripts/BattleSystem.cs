using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum BattleState
{
    Start,
    PlayerTurn,
    EnemyTurn,
    Won,
    Lost,
    None
}

public class BattleSystem : MonoBehaviour
{

    public GameObject BattleUI;
    public GameObject playerPrefab;
    public GameObject enemyPrefab;

    public Transform playerBattleSpot;
    public Transform enemyBattleSpot;

    public BattleHUD playerHUD;
    public PlayerCombat playerCombat;
    public TransitionController tranController;

    public GameObject enemyTarget;

    public BattleState state;

    public bool playerStartAction = true;






    private void Update()
    {

        //Update HUD
        //HUDUpdate();


        switch (state)
        {
            case BattleState.PlayerTurn:
                PlayerAction();
                break;
            case BattleState.EnemyTurn:
                enemyAction();
                break;
            case BattleState.Won:
                break;
            case BattleState.Lost:
                break;
            default:
                break;
        }
    }

    public void SetupBattle()
    {

        //Set the initial Battle State
        state = BattleState.Start;

        //Show Battle UI
        BattleUI.SetActive(true);

        //Set player hud
        playerHUD.SetHUD();

        //Transport the enemy and the player onto the arena
        playerPrefab.transform.position = playerBattleSpot.position;
        enemyPrefab.transform.position = enemyBattleSpot.position;

        //Make them look at each other
        playerPrefab.transform.LookAt(enemyPrefab.transform);
        enemyPrefab.transform.LookAt(playerPrefab.transform);

        state = BattleState.PlayerTurn;

    }

    void PlayerAction()
    {
        if (playerStartAction)
        {
            playerCombat.SetUI();
            playerStartAction = false;
        }
        
        if (Input.GetButtonDown("FirstBattleButton"))
        {
            playerCombat.FirstButtonAction();
        }

        if (Input.GetButtonDown("SecondBattleButton"))
        {
            playerCombat.SecondButtonAction();
        }

        if (Input.GetButtonDown("ThirdBattleButton"))
        {
            playerCombat.ThirdButtonAction();
        }

        if (Input.GetButtonDown("FourthBattleButton"))
        {
            playerCombat.FourthButtonAction();
        }

        if (!enemyPrefab)
        {
            playerCombat.BattleOver = true;
        }

    }

    void enemyAction() 
    {


        enemyPrefab.GetComponent<EnemyCombat>().EnemyTurn();
        playerStartAction = true;
    } 


    public void ChangeState(BattleState stateToChange) 
    {
        state = stateToChange;
    }

    public void EndBattle()
    {
        BattleUI.SetActive(false);
        state = BattleState.None;
    }

}
