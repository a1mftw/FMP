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
    public Transform enemyBattleSpot1;
    public Transform enemyBattleSpot2;
    public Transform enemyBattleSpot3;

    public BattleHUD playerHUD;
    public PlayerCombat playerCombat;
    public TransitionController tranController;

    public GameObject enemyTarget;

    public BattleState state;

    public bool playerStartAction = true;

    GameObject newEnemy;
    List<GameObject> inactiveChars = new List<GameObject>();
    List<GameObject> availableChars = new List<GameObject>();
    List<GameObject> Enemies = new List<GameObject>();
    List<Transform> battleSpots = new List<Transform>();
    bool playerTurn;
    int enemiesSpawned = 0;
    int bigSpeed = 0;
    int nextAttacker = 0;
    int target = 0;



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


        enemiesSpawned = 3;//Random.Range(1, 3);

        battleSpots.Add(enemyBattleSpot1.transform);
        battleSpots.Add(enemyBattleSpot2.transform);
        battleSpots.Add(enemyBattleSpot3.transform);


        //Transport the player onto the arena
        playerPrefab.transform.position = playerBattleSpot.position;

        availableChars.Add(playerPrefab);
        

        for (int i = 0; i < enemiesSpawned; i++)
        {
            newEnemy = Instantiate(enemyPrefab);
            newEnemy.transform.position = battleSpots[i].position;
            newEnemy.transform.LookAt(playerPrefab.transform);
            availableChars.Add(newEnemy);
            Enemies.Add(newEnemy);
        }

        playerPrefab.transform.LookAt(newEnemy.transform);
        enemyTarget = Enemies[target];

        NextTurn();
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

    void enemyAction(GameObject enemy) 
    {
        enemy.GetComponent<EnemyCombat>().EnemyTurn();
        playerStartAction = true;
    }

    public void NextTarget() 
    {

        if ((target+1) >= enemiesSpawned)
        {
            target = 0;
        }
        else
        {
            target++;
        }

        enemyTarget = Enemies[target];
    
    }

    public void PreviousTarget() 
    {
        if ((target-1) < 0)
        {
            target = enemiesSpawned-1;
        }
        else
        {
            target--;
        }

        enemyTarget = Enemies[target];

    }

    public void NextTurn()
    {
        playerHUD.SetHUD();
        int tempSpeed;

        if (availableChars.Count == 0)
        {
            for (int i = 0; i < inactiveChars.Count; i++)
            {
                if (inactiveChars[i].tag == "Enemy"&&inactiveChars[i].GetComponent<EnemyStats>().enemy.baseStats.currentHealth<=0)
                {
                    inactiveChars.RemoveAt(i);
                }
                else
                {
                    availableChars.Add(inactiveChars[i]);
                }
            }
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
            enemyAction(availableChars[nextAttacker]);
            inactiveChars.Add(availableChars[nextAttacker]);
            availableChars.Remove(availableChars[nextAttacker]);
            
        }
    }

    public void EndBattle()
    {
        BattleUI.SetActive(false);
    }

}
