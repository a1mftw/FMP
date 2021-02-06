using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum BattleState
{
    Start,
    PlayerTurn,
    EnemyTurn,
    Won,
    Lost
}

public class BattleSystem : MonoBehaviour
{

    public GameObject BattleUI;
    public GameObject playerPrefab;
    public GameObject enemyPrefab;

    public Transform playerBattleSpot;
    public Transform enemyBattleSpot;

    public BattleState state;

    public void SetupBattle() 
    {

        state = BattleState.Start;
        BattleUI.enabled = true;
        playerPrefab.transform.position = playerBattleSpot.position;
        enemyPrefab.transform.position = enemyBattleSpot.position;
        playerPrefab.transform.LookAt(enemyPrefab.transform);
        enemyPrefab.transform.LookAt(playerPrefab.transform);

    }
  
}
