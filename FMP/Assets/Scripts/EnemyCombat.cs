using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCombat : MonoBehaviour
{

    private PlayerAttacks playerAttacks;
    private EnemyStats enemyStats;

    void Start() 
    {
        enemyStats = GetComponent<EnemyStats>();
        playerAttacks = GameObject.Find("Player").GetComponent<PlayerAttacks>();

    }        


    public void TakeBludgeoningDamage(int damage,PlayerAttacks.Target target) 
    {
        int damageDealt = damage - enemyStats.foxEnemy.baseArmor;

        switch (target)
        {
            case PlayerAttacks.Target.Head:
                enemyStats.foxEnemy.bodyPartHealth.headHealth -= damageDealt;
                break;
            case PlayerAttacks.Target.Torso:
                enemyStats.foxEnemy.bodyPartHealth.torsoHealth -= damageDealt;
                break;
            case PlayerAttacks.Target.Arms:
                enemyStats.foxEnemy.bodyPartHealth.armsHealth -= damageDealt;
                break;
            case PlayerAttacks.Target.Hands:
                enemyStats.foxEnemy.bodyPartHealth.handsHealth -= damageDealt;
                break;
            case PlayerAttacks.Target.Legs:
                enemyStats.foxEnemy.bodyPartHealth.legsHealth -= damageDealt;
                break;
            case PlayerAttacks.Target.Feet:
                enemyStats.foxEnemy.bodyPartHealth.feetHealth -= damageDealt;
                break;
        }

        enemyStats.foxEnemy.currentHealth -= damageDealt;
        Debug.Log("HeadHealth: " + enemyStats.foxEnemy.bodyPartHealth.headHealth);
        Debug.Log("Dealt " + damageDealt + " damage\nRemains " + enemyStats.foxEnemy.currentHealth + " HP" );
        if (enemyStats.foxEnemy.currentHealth <= 0)
        {
            Destroy(this.gameObject);
        }

    }

    public void TakePiercingDamage(int damage, PlayerAttacks.Target target) 
    {
        int damageDealt = damage;
        switch (target)
        {
            case PlayerAttacks.Target.Head:
                enemyStats.foxEnemy.bodyPartHealth.headHealth -= damageDealt;
                break;
            case PlayerAttacks.Target.Torso:
                enemyStats.foxEnemy.bodyPartHealth.torsoHealth -= damageDealt;
                break;
            case PlayerAttacks.Target.Arms:
                enemyStats.foxEnemy.bodyPartHealth.armsHealth -= damageDealt;
                break;
            case PlayerAttacks.Target.Hands:
                enemyStats.foxEnemy.bodyPartHealth.handsHealth -= damageDealt;
                break;
            case PlayerAttacks.Target.Legs:
                enemyStats.foxEnemy.bodyPartHealth.legsHealth -= damageDealt;
                break;
            case PlayerAttacks.Target.Feet:
                enemyStats.foxEnemy.bodyPartHealth.feetHealth -= damageDealt;
                break;
        }

        enemyStats.foxEnemy.currentHealth -= damageDealt;
        Debug.Log("HeadHealth: " + enemyStats.foxEnemy.bodyPartHealth.headHealth);
        Debug.Log("Dealt " + damageDealt + " damage\nRemains " + enemyStats.foxEnemy.currentHealth + " HP");
        if (enemyStats.foxEnemy.currentHealth <= 0)
        {
            Destroy(this.gameObject);
        }
    }
    public void TakeSlashingDamage(int damage, PlayerAttacks.Target target) 
    {
        int damageDealt = damage - enemyStats.foxEnemy.baseArmor;

        switch (target)
        {
            case PlayerAttacks.Target.Head:
                enemyStats.foxEnemy.bodyPartHealth.headHealth -= damageDealt;
                break;
            case PlayerAttacks.Target.Torso:
                enemyStats.foxEnemy.bodyPartHealth.torsoHealth -= damageDealt;
                break;
            case PlayerAttacks.Target.Arms:
                enemyStats.foxEnemy.bodyPartHealth.armsHealth -= damageDealt;
                break;
            case PlayerAttacks.Target.Hands:
                enemyStats.foxEnemy.bodyPartHealth.handsHealth -= damageDealt;
                break;
            case PlayerAttacks.Target.Legs:
                enemyStats.foxEnemy.bodyPartHealth.legsHealth -= damageDealt;
                break;
            case PlayerAttacks.Target.Feet:
                enemyStats.foxEnemy.bodyPartHealth.feetHealth -= damageDealt;
                break;
        }

        enemyStats.foxEnemy.currentHealth -= damageDealt;
        Debug.Log("HeadHealth: " + enemyStats.foxEnemy.bodyPartHealth.headHealth);
        Debug.Log("Dealt " + damageDealt + " damage\nRemains " + enemyStats.foxEnemy.currentHealth + " HP");
        if (enemyStats.foxEnemy.currentHealth <= 0)
        {
            Destroy(this.gameObject);
        }
    }


}
