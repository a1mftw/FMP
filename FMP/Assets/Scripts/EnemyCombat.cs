using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum ParryStance
{
    BludgeonStance,
    PierceStance,
    SlashStance,
}

public class EnemyCombat : MonoBehaviour
{

    private PlayerAttacks playerAttacks;
    private GameObject playerTarget;
    private EnemyStats enemyStats;
    public BattleSystem battleSystem;

    private bool attacking = false;
    private PlayerAttacks.Spells previousSpell = PlayerAttacks.Spells.None;
    private PlayerAttacks.Spells currentSpell = PlayerAttacks.Spells.None;

    void Start() 
    {
        enemyStats = GetComponent<EnemyStats>();
        playerAttacks = GameObject.Find("Player").GetComponent<PlayerAttacks>();

    }


    public void EnemyTurn() 
    {
        if (!attacking)
        {
            Debug.Log("Enemy Turn");

            if (enemyStats.foxEnemy.buffs.Burning || enemyStats.foxEnemy.buffs.Drowning || enemyStats.foxEnemy.buffs.Freezing || enemyStats.foxEnemy.buffs.Poisoned || enemyStats.foxEnemy.buffs.Electrified)
            {
                enemyStats.foxEnemy.currentHealth -= Mathf.RoundToInt(enemyStats.foxEnemy.maxHealth * 0.05f);
            }

            StartCoroutine("TailSwipe");
            
        }
    }

    public void EndTurn() 
    {
    
    }

    #region EnemyActions

    IEnumerator TailSwipe() 
    {
        attacking = true;
        playerTarget = battleSystem.playerPrefab;
        Debug.Log("Attacking");

        yield return new WaitForSeconds(1);
        var returnPos = gameObject.transform.position;
        float step = (6 / (gameObject.transform.position - playerTarget.transform.position).magnitude) * Time.fixedDeltaTime;
        float t = 0;

        //playerAnimations.SetBool("Sprinting", true);

        while (Vector3.Distance(gameObject.transform.position, playerTarget.transform.position) > 5)
        {
            t += step;
            gameObject.transform.position = Vector3.Lerp(gameObject.transform.position, playerTarget.transform.position, t);
            yield return new WaitForFixedUpdate();
        }

        //playerAnimations.SetBool("Sprinting", false);
        //battleCamera.Play("StrikeState");
        yield return new WaitForSeconds(0.5f);
        //playerAnimations.SetBool("Slashing", true);
        yield return new WaitForSeconds(1);
        playerTarget.GetComponent<PlayerAttacks>().TakeTailSwipeDamage(enemyStats.foxEnemy.baseDamage, PlayerAttacks.Target.Torso);
        //playerAnimations.SetBool("Slashing", false);
        gameObject.transform.position = returnPos;
        //battleCamera.Play("BattleCamera");

        battleSystem.ChangeState(BattleState.PlayerTurn);

        attacking = false;
    }
    IEnumerator ClawAttack() 
    {
        attacking = true;
        playerTarget = battleSystem.playerPrefab;
        Debug.Log("Attacking");
        yield return new WaitForSeconds(5f);
        playerTarget.GetComponent<PlayerAttacks>().TakeTailSwipeDamage(enemyStats.foxEnemy.baseDamage, PlayerAttacks.Target.Torso);
        battleSystem.ChangeState(BattleState.PlayerTurn);
        attacking = false;
    }
    #endregion

    #region ReceiveDamage-Physical


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

    #endregion

    #region RecieveDamage-Overdrive

    public void TakeMyriadDamage(int damage) 
    {
        enemyStats.foxEnemy.currentHealth -= damage;
    }

    #endregion

    #region BasicSpellRecieveDamage
    public void TakeFireDamage(int damage)
    {

        currentSpell = PlayerAttacks.Spells.Fire;

        if (previousSpell != PlayerAttacks.Spells.None)
        {
            StatusEffects();
        }
        else
        {
            previousSpell = currentSpell;
        }


        enemyStats.foxEnemy.currentHealth -= damage;

    }
    public void TakeWaterDamage(int damage)
    {

        currentSpell = PlayerAttacks.Spells.Water;

        if (previousSpell != PlayerAttacks.Spells.None)
        {
            StatusEffects();
        }
        else
        {
            previousSpell = currentSpell;
        }


        enemyStats.foxEnemy.currentHealth -= damage;

    }
    public void TakeAirDamage(int damage)
    {

        currentSpell = PlayerAttacks.Spells.Air;

        if (previousSpell != PlayerAttacks.Spells.None)
        {
            StatusEffects();
        }
        else
        {
            previousSpell = currentSpell;
        }


        enemyStats.foxEnemy.currentHealth -= damage;

    }
    public void TakeEarthDamage(int damage)
    {

        currentSpell = PlayerAttacks.Spells.Earth;

        if (previousSpell != PlayerAttacks.Spells.None)
        {
            StatusEffects();
        }
        else
        {
            previousSpell = currentSpell;
        }


        enemyStats.foxEnemy.currentHealth -= damage;

    }
    public void TakeLightningDamage(int damage)
    {

        currentSpell = PlayerAttacks.Spells.Lightning;

        if (previousSpell != PlayerAttacks.Spells.None)
        {
            StatusEffects();
        }
        else
        {
            previousSpell = currentSpell;
        }


        enemyStats.foxEnemy.currentHealth -= damage;

    }
    #endregion

    #region Buff/Debuff/Ailments
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

    private void StatusEffects() 
    {
        switch (currentSpell)
        {
            case PlayerAttacks.Spells.Fire:

                switch (previousSpell)
                {
                    case PlayerAttacks.Spells.Fire:
                        enemyStats.foxEnemy.buffs.Burning = true;
                        break;

                    case PlayerAttacks.Spells.Water:
                        enemyStats.foxEnemy.buffs.Clouded = true;
                        break;

                    case PlayerAttacks.Spells.Lightning:
                        enemyStats.foxEnemy.buffs.Unatunned = true;
                        break;
                }
                break;
            case PlayerAttacks.Spells.Water:

                switch (previousSpell)
                {
                    case PlayerAttacks.Spells.Fire:
                        enemyStats.foxEnemy.buffs.Clouded = true;
                        break;
                    case PlayerAttacks.Spells.Water:
                        enemyStats.foxEnemy.buffs.Drowning = true;
                        break;
                    case PlayerAttacks.Spells.Earth:
                        enemyStats.foxEnemy.buffs.Slowed = true;
                        break;
                    case PlayerAttacks.Spells.Lightning:
                        enemyStats.foxEnemy.buffs.Paralysed = true;
                        break;
                }

                break;
            case PlayerAttacks.Spells.Air:
                switch (previousSpell)
                {
                    case PlayerAttacks.Spells.Air:
                        enemyStats.foxEnemy.buffs.Freezing = true;
                        break;
                    case PlayerAttacks.Spells.Earth:
                        enemyStats.foxEnemy.buffs.Unbalanced = true;
                        break;
                    case PlayerAttacks.Spells.Lightning:
                        enemyStats.foxEnemy.buffs.Scared = true;
                        break;
                }
                break;
            case PlayerAttacks.Spells.Earth:
                switch (previousSpell)
                {

                    case PlayerAttacks.Spells.Water:
                        enemyStats.foxEnemy.buffs.Slowed = true;
                        break;
                    case PlayerAttacks.Spells.Air:
                        enemyStats.foxEnemy.buffs.Unbalanced = true;
                        break;
                    case PlayerAttacks.Spells.Earth:
                        enemyStats.foxEnemy.buffs.Poisoned = true;
                        break;
                }
                break;
            case PlayerAttacks.Spells.Lightning:
                switch (previousSpell)
                {
                    case PlayerAttacks.Spells.Fire:
                        enemyStats.foxEnemy.buffs.Unatunned = true;
                        break;
                    case PlayerAttacks.Spells.Water:
                        enemyStats.foxEnemy.buffs.Paralysed = true;
                        break;
                    case PlayerAttacks.Spells.Air:
                        enemyStats.foxEnemy.buffs.Scared = true;
                        break;
                    case PlayerAttacks.Spells.Lightning:
                        enemyStats.foxEnemy.buffs.Electrified = true;
                        break;
                }
                break;
            default:
                break;
        }

    }

    #endregion
}
