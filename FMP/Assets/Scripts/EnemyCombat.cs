using System;
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

                if (enemyStats.foxEnemy.bodyPartHealth.headHealth - damageDealt<=0)
                {
                    enemyStats.foxEnemy.bodyPartHealth.headHealth = 0;
                    ApplyBodyPartDebuff(target);
                }
                else
                {
                    enemyStats.foxEnemy.bodyPartHealth.headHealth -= damageDealt;
                }
                
                break;
            case PlayerAttacks.Target.Torso:

                if (enemyStats.foxEnemy.bodyPartHealth.torsoHealth - damageDealt <= 0)
                {
                    enemyStats.foxEnemy.bodyPartHealth.torsoHealth = 0;
                    ApplyBodyPartDebuff(target);
                }
                else
                {
                    enemyStats.foxEnemy.bodyPartHealth.torsoHealth -= damageDealt;
                }
                break;

            case PlayerAttacks.Target.Arms:

                if (enemyStats.foxEnemy.bodyPartHealth.armsHealth - damageDealt <= 0)
                {
                    enemyStats.foxEnemy.bodyPartHealth.armsHealth = 0;
                    ApplyBodyPartDebuff(target);
                }
                else
                {
                    enemyStats.foxEnemy.bodyPartHealth.armsHealth -= damageDealt;
                }
                break;

            case PlayerAttacks.Target.Hands:

                if (enemyStats.foxEnemy.bodyPartHealth.handsHealth - damageDealt <= 0)
                {
                    enemyStats.foxEnemy.bodyPartHealth.handsHealth = 0;
                    ApplyBodyPartDebuff(target);
                }
                else
                {
                    enemyStats.foxEnemy.bodyPartHealth.handsHealth -= damageDealt;
                }
                break;
            case PlayerAttacks.Target.Legs:

                if (enemyStats.foxEnemy.bodyPartHealth.legsHealth - damageDealt <= 0)
                {
                    enemyStats.foxEnemy.bodyPartHealth.legsHealth = 0;
                    ApplyBodyPartDebuff(target);
                }
                else
                {
                    enemyStats.foxEnemy.bodyPartHealth.legsHealth -= damageDealt;
                }
                break;
            case PlayerAttacks.Target.Feet:

                if (enemyStats.foxEnemy.bodyPartHealth.feetHealth - damageDealt <= 0)
                {
                    enemyStats.foxEnemy.bodyPartHealth.feetHealth = 0;
                    ApplyBodyPartDebuff(target);
                }
                else
                {
                    enemyStats.foxEnemy.bodyPartHealth.feetHealth -= damageDealt;
                }
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

    public void TakeFireDamage(int damage) 
    {
        
    }
    private void ApplyBodyPartDebuff(PlayerAttacks.Target target)
    {
        switch (target)
        {
            case PlayerAttacks.Target.Head:
                enemyStats.foxEnemy.buffs.headDebuff = true;
                break;
            case PlayerAttacks.Target.Torso:
                enemyStats.foxEnemy.buffs.torsoDebuff = true;
                break;
            case PlayerAttacks.Target.Arms:
                enemyStats.foxEnemy.buffs.armsDebuff = true;
                break;
            case PlayerAttacks.Target.Hands:
                enemyStats.foxEnemy.buffs.handsDebuff = true;
                break;
            case PlayerAttacks.Target.Legs:
                enemyStats.foxEnemy.buffs.legsDebuff = true;
                break;
            case PlayerAttacks.Target.Feet:
                enemyStats.foxEnemy.buffs.feetDebuff = true;
                break;
            default:
                break;
        }
    }
}
