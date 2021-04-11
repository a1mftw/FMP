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

    List<GameObject> inactiveChars;
    List<GameObject> availableChars;
    bool playerTurn;
    int enemiesSpawned = 0;
    int bigSpeed = 0;
    int nextAttacker = 0;



    private void Update()
    {
        //Update HUD
        //HUDUpdate();
        if (playerTurn)
        {
            PlayerAction();
        }
    }

    public void SetupBattle()
    {

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


        availableChars.Add(playerPrefab);
        

        for (int i = 0; i < enemiesSpawned; i++)
        {
            var newEnemy = Instantiate(enemyPrefab);
            availableChars.Add(newEnemy);
        }

        NextTurn();
    }

    void PlayerAction()
    {
        if (playerStartAction)
        {
            playerHUD.SetHUD();
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

    void enemyAction(GameObject enemy) 
    {
        enemy.GetComponent<EnemyCombat>().EnemyTurn();
        playerStartAction = true;
    } 


    public void NextTurn(BattleState stateToChange) 
    {
        state = stateToChange;
    }

    public void NextTurn()
    {
        int tempSpeed;

        if (availableChars.Count == 0)
        {
            availableChars = inactiveChars;
            inactiveChars.Clear();
            bigSpeed = 0;
        }

        for (int i = 0; i < availableChars.Count; i++)
        {
            if (availableChars[i].tag == "Player")
            {
                tempSpeed = availableChars[i].GetComponent<PlayerStats>().player.baseStats.baseSpeed;  
            }
            else
            {
                tempSpeed = availableChars[i].GetComponent<EnemyStats>().enemy.baseStats.baseSpeed;
            }

            if (tempSpeed >= bigSpeed)
            {
                bigSpeed = tempSpeed;
                nextAttacker = i;
            }
        }

        if (availableChars[nextAttacker].tag == "Player")
        {
            playerTurn = true;
            inactiveChars.Add(availableChars[nextAttacker]);
            availableChars.Remove(availableChars[nextAttacker]);
        }
        else
        {
            playerTurn = false;
            inactiveChars.Add(availableChars[nextAttacker]);
            availableChars.Remove(availableChars[nextAttacker]);
            enemyAction(availableChars[nextAttacker]);
        }
    }

    public void EndBattle()
    {
        BattleUI.SetActive(false);
    }

}
