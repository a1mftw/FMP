using System;
using System.Reflection;
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
    private GameObject playerTarget;
    private EnemyStats enemyStats;
    public BattleSystem battleSystem;
    public Animator foxAnimation;
    public DamageNumber damageNumber;

    private bool attacking = false;
    private PlayerAttacks.Spells previousSpell = PlayerAttacks.Spells.None;
    private PlayerAttacks.Spells currentSpell = PlayerAttacks.Spells.None;

    void Start() 
    {
        enemyStats = GetComponent<EnemyStats>();

    }


    public void EnemyTurn() 
    {
        if (!attacking)
        {
            Debug.Log("Enemy Turn");

            if (enemyStats.enemy.buffs.Burning || enemyStats.enemy.buffs.Drowning || enemyStats.enemy.buffs.Freezing || enemyStats.enemy.buffs.Poisoned || enemyStats.enemy.buffs.Electrified)
            {
                enemyStats.enemy.baseStats.currentHealth -= Mathf.RoundToInt(enemyStats.enemy.baseStats.maxHealth * 0.05f);
            }

            if (!enemyStats.enemy.buffs.Clouded)
            {
                StartCoroutine("TailSwipe");
                RemoveBuffs();
            }
            else
            {
                RemoveBuffs();
                battleSystem.NextTurn();
            }


            

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

        foxAnimation.SetBool("Running", true);

        while (Vector3.Distance(gameObject.transform.position, playerTarget.transform.position) > 5)
        {
            t += step;
            gameObject.transform.position = Vector3.Lerp(gameObject.transform.position, playerTarget.transform.position, t);
            yield return new WaitForFixedUpdate();
        }

        foxAnimation.SetBool("Running", false);
        foxAnimation.SetBool("TailSwipe", true);
        yield return new WaitForSeconds(1);
        if (enemyStats.enemy.buffs.Unbalanced)
        {
            playerTarget.GetComponent<PlayerAttacks>().TakeTailSwipeDamage(0, PlayerAttacks.Target.Torso);
        }
        else
        {
            playerTarget.GetComponent<PlayerAttacks>().TakeTailSwipeDamage(enemyStats.enemy.baseStats.baseDamage, PlayerAttacks.Target.Torso);
        } 
        foxAnimation.SetBool("TailSwipe", false);

        step = (12 / (gameObject.transform.position - returnPos).magnitude) * Time.fixedDeltaTime;
        t = 0;
        gameObject.transform.LookAt(returnPos);
        foxAnimation.SetBool("Running", true);

        while (Vector3.Distance(gameObject.transform.position, returnPos) > 2)
        {
            t += step;
            gameObject.transform.position = Vector3.Lerp(gameObject.transform.position, returnPos, t);
            yield return new WaitForFixedUpdate();
        }

        foxAnimation.SetBool("Running", false);
        gameObject.transform.LookAt(playerTarget.transform);
        gameObject.transform.position = returnPos;
        battleSystem.NextTurn();
        attacking = false;
    }
    IEnumerator ClawAttack() 
    {
        attacking = true;
        playerTarget = battleSystem.playerPrefab;
        Debug.Log("Attacking");

        yield return new WaitForSeconds(1);
        var returnPos = gameObject.transform.position;
        float step = (6 / (gameObject.transform.position - playerTarget.transform.position).magnitude) * Time.fixedDeltaTime;
        float t = 0;

        foxAnimation.SetBool("Running", true);

        while (Vector3.Distance(gameObject.transform.position, playerTarget.transform.position) > 5)
        {
            t += step;
            gameObject.transform.position = Vector3.Lerp(gameObject.transform.position, playerTarget.transform.position, t);
            yield return new WaitForFixedUpdate();
        }

        foxAnimation.SetBool("Running", false);
        foxAnimation.SetBool("Claw", true);
        yield return new WaitForSeconds(1);
        playerTarget.GetComponent<PlayerAttacks>().TakeTailSwipeDamage(enemyStats.enemy.baseStats.baseDamage, PlayerAttacks.Target.Torso);
        foxAnimation.SetBool("Claw", false);

        step = (12 / (gameObject.transform.position - returnPos).magnitude) * Time.fixedDeltaTime;
        t = 0;
        gameObject.transform.LookAt(returnPos);
        foxAnimation.SetBool("Running", true);

        while (Vector3.Distance(gameObject.transform.position, returnPos) > 2)
        {
            t += step;
            gameObject.transform.position = Vector3.Lerp(gameObject.transform.position, returnPos, t);
            yield return new WaitForFixedUpdate();
        }

        foxAnimation.SetBool("Running", false);
        gameObject.transform.LookAt(playerTarget.transform);
        gameObject.transform.position = returnPos;
        battleSystem.NextTurn();
        attacking = false;
    }
    #endregion

    #region ReceiveDamage-Physical


    public void TakeBludgeoningDamage(int damage,PlayerAttacks.Target target) 
    {
        int damageDealt = damage - enemyStats.enemy.baseStats.baseArmor;

        switch (target)
        {
            case PlayerAttacks.Target.Head:

                if (enemyStats.enemy.bodyPartHealth.headHealth - damageDealt<=0)
                {
                    enemyStats.enemy.bodyPartHealth.headHealth = 0;
                    ApplyBodyPartDebuff(target);
                }
                else
                {
                    enemyStats.enemy.bodyPartHealth.headHealth -= damageDealt;
                }
                
                break;
            case PlayerAttacks.Target.Torso:

                if (enemyStats.enemy.bodyPartHealth.torsoHealth - damageDealt <= 0)
                {
                    enemyStats.enemy.bodyPartHealth.torsoHealth = 0;
                    ApplyBodyPartDebuff(target);
                }
                else
                {
                    enemyStats.enemy.bodyPartHealth.torsoHealth -= damageDealt;
                }
                break;

            case PlayerAttacks.Target.Arms:

                if (enemyStats.enemy.bodyPartHealth.armsHealth - damageDealt <= 0)
                {
                    enemyStats.enemy.bodyPartHealth.armsHealth = 0;
                    ApplyBodyPartDebuff(target);
                }
                else
                {
                    enemyStats.enemy.bodyPartHealth.armsHealth -= damageDealt;
                }
                break;

            case PlayerAttacks.Target.Hands:

                if (enemyStats.enemy.bodyPartHealth.handsHealth - damageDealt <= 0)
                {
                    enemyStats.enemy.bodyPartHealth.handsHealth = 0;
                    ApplyBodyPartDebuff(target);
                }
                else
                {
                    enemyStats.enemy.bodyPartHealth.handsHealth -= damageDealt;
                }
                break;
            case PlayerAttacks.Target.Legs:

                if (enemyStats.enemy.bodyPartHealth.legsHealth - damageDealt <= 0)
                {
                    enemyStats.enemy.bodyPartHealth.legsHealth = 0;
                    ApplyBodyPartDebuff(target);
                }
                else
                {
                    enemyStats.enemy.bodyPartHealth.legsHealth -= damageDealt;
                }
                break;
            case PlayerAttacks.Target.Feet:

                if (enemyStats.enemy.bodyPartHealth.feetHealth - damageDealt <= 0)
                {
                    enemyStats.enemy.bodyPartHealth.feetHealth = 0;
                    ApplyBodyPartDebuff(target);
                }
                else
                {
                    enemyStats.enemy.bodyPartHealth.feetHealth -= damageDealt;
                }
                break;

        }

        enemyStats.enemy.baseStats.currentHealth -= damageDealt;

        damageNumber.Create(transform.position, damageDealt, false);
        Debug.Log("HeadHealth: " + enemyStats.enemy.bodyPartHealth.headHealth);
        Debug.Log("Dealt " + damageDealt + " damage\nRemains " + enemyStats.enemy.baseStats.currentHealth + " HP" );



        if (enemyStats.enemy.baseStats.currentHealth <= 0)
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
                enemyStats.enemy.bodyPartHealth.headHealth -= damageDealt;
                break;
            case PlayerAttacks.Target.Torso:
                enemyStats.enemy.bodyPartHealth.torsoHealth -= damageDealt;
                break;
            case PlayerAttacks.Target.Arms:
                enemyStats.enemy.bodyPartHealth.armsHealth -= damageDealt;
                break;
            case PlayerAttacks.Target.Hands:
                enemyStats.enemy.bodyPartHealth.handsHealth -= damageDealt;
                break;
            case PlayerAttacks.Target.Legs:
                enemyStats.enemy.bodyPartHealth.legsHealth -= damageDealt;
                break;
            case PlayerAttacks.Target.Feet:
                enemyStats.enemy.bodyPartHealth.feetHealth -= damageDealt;
                break;
        }

        enemyStats.enemy.baseStats.currentHealth -= damageDealt;
        damageNumber.Create(transform.position, damageDealt, false);
        Debug.Log("HeadHealth: " + enemyStats.enemy.bodyPartHealth.headHealth);
        Debug.Log("Dealt " + damageDealt + " damage\nRemains " + enemyStats.enemy.baseStats.currentHealth + " HP");
        if (enemyStats.enemy.baseStats.currentHealth <= 0)
        {
            Destroy(this.gameObject);
        }
    }
    public void TakeSlashingDamage(int damage, PlayerAttacks.Target target) 
    {
        int damageDealt = damage - enemyStats.enemy.baseStats.baseArmor;

        switch (target)
        {
            case PlayerAttacks.Target.Head:
                enemyStats.enemy.bodyPartHealth.headHealth -= damageDealt;
                break;
            case PlayerAttacks.Target.Torso:
                enemyStats.enemy.bodyPartHealth.torsoHealth -= damageDealt;
                break;
            case PlayerAttacks.Target.Arms:
                enemyStats.enemy.bodyPartHealth.armsHealth -= damageDealt;
                break;
            case PlayerAttacks.Target.Hands:
                enemyStats.enemy.bodyPartHealth.handsHealth -= damageDealt;
                break;
            case PlayerAttacks.Target.Legs:
                enemyStats.enemy.bodyPartHealth.legsHealth -= damageDealt;
                break;
            case PlayerAttacks.Target.Feet:
                enemyStats.enemy.bodyPartHealth.feetHealth -= damageDealt;
                break;
        }

        enemyStats.enemy.baseStats.currentHealth -= damageDealt;
        damageNumber.Create(transform.position, damageDealt, false);
        Debug.Log("HeadHealth: " + enemyStats.enemy.bodyPartHealth.headHealth);
        Debug.Log("Dealt " + damageDealt + " damage\nRemains " + enemyStats.enemy.baseStats.currentHealth + " HP");
        if (enemyStats.enemy.baseStats.currentHealth <= 0)
        {
            Destroy(this.gameObject);
        }
    }

    #endregion

    #region RecieveDamage-Overdrive

    public void TakeMyriadDamage(int damage) 
    {
        damageNumber.Create(transform.position, damage, false);
        enemyStats.enemy.baseStats.currentHealth -= damage;
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

        damageNumber.Create(transform.position, damage, false);
        enemyStats.enemy.baseStats.currentHealth -= damage;

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

        damageNumber.Create(transform.position, damage, false);
        enemyStats.enemy.baseStats.currentHealth -= damage;

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

        damageNumber.Create(transform.position, damage, false);
        enemyStats.enemy.baseStats.currentHealth -= damage;

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

        damageNumber.Create(transform.position, damage, false);
        enemyStats.enemy.baseStats.currentHealth -= damage;

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

        damageNumber.Create(transform.position, damage, false);
        enemyStats.enemy.baseStats.currentHealth -= damage;

    }
    #endregion

    #region Buff/Debuff/Ailments
    private void ApplyBodyPartDebuff(PlayerAttacks.Target target)
    {
        switch (target)
        {
            case PlayerAttacks.Target.Head:
                enemyStats.enemy.buffs.headDebuff = true;
                break;
            case PlayerAttacks.Target.Torso:
                enemyStats.enemy.buffs.torsoDebuff = true;
                break;
            case PlayerAttacks.Target.Arms:
                enemyStats.enemy.buffs.armsDebuff = true;
                break;
            case PlayerAttacks.Target.Hands:
                enemyStats.enemy.buffs.handsDebuff = true;
                break;
            case PlayerAttacks.Target.Legs:
                enemyStats.enemy.buffs.legsDebuff = true;
                break;
            case PlayerAttacks.Target.Feet:
                enemyStats.enemy.buffs.feetDebuff = true;
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
                        enemyStats.enemy.buffs.Burning = true;
                        previousSpell = PlayerAttacks.Spells.None;
                        break;

                    case PlayerAttacks.Spells.Water:
                        enemyStats.enemy.buffs.Clouded = true;
                        previousSpell = PlayerAttacks.Spells.None;
                        break;

                    case PlayerAttacks.Spells.Lightning:
                        enemyStats.enemy.buffs.Unatunned = true;
                        previousSpell = PlayerAttacks.Spells.None;
                        break;
                }
                break;
            case PlayerAttacks.Spells.Water:

                switch (previousSpell)
                {
                    case PlayerAttacks.Spells.Fire:
                        enemyStats.enemy.buffs.Clouded = true;
                        previousSpell = PlayerAttacks.Spells.None;
                        break;
                    case PlayerAttacks.Spells.Water:
                        enemyStats.enemy.buffs.Drowning = true;
                        previousSpell = PlayerAttacks.Spells.None;
                        break;
                    case PlayerAttacks.Spells.Earth:
                        enemyStats.enemy.buffs.Slowed = true;
                        previousSpell = PlayerAttacks.Spells.None;
                        break;
                    case PlayerAttacks.Spells.Lightning:
                        enemyStats.enemy.buffs.Paralysed = true;
                        previousSpell = PlayerAttacks.Spells.None;
                        break;
                }

                break;
            case PlayerAttacks.Spells.Air:
                switch (previousSpell)
                {
                    case PlayerAttacks.Spells.Air:
                        enemyStats.enemy.buffs.Freezing = true;
                        previousSpell = PlayerAttacks.Spells.None;
                        break;
                    case PlayerAttacks.Spells.Earth:
                        enemyStats.enemy.buffs.Unbalanced = true;
                        previousSpell = PlayerAttacks.Spells.None;
                        break;
                    case PlayerAttacks.Spells.Lightning:
                        enemyStats.enemy.buffs.Scared = true;
                        previousSpell = PlayerAttacks.Spells.None;
                        break;
                }
                break;
            case PlayerAttacks.Spells.Earth:
                switch (previousSpell)
                {

                    case PlayerAttacks.Spells.Water:
                        enemyStats.enemy.buffs.Slowed = true;
                        previousSpell = PlayerAttacks.Spells.None;
                        break;
                    case PlayerAttacks.Spells.Air:
                        enemyStats.enemy.buffs.Unbalanced = true;
                        previousSpell = PlayerAttacks.Spells.None;
                        break;
                    case PlayerAttacks.Spells.Earth:
                        enemyStats.enemy.buffs.Poisoned = true;
                        previousSpell = PlayerAttacks.Spells.None;
                        break;
                }
                break;
            case PlayerAttacks.Spells.Lightning:
                switch (previousSpell)
                {
                    case PlayerAttacks.Spells.Fire:
                        enemyStats.enemy.buffs.Unatunned = true;
                        previousSpell = PlayerAttacks.Spells.None;
                        break;
                    case PlayerAttacks.Spells.Water:
                        enemyStats.enemy.buffs.Paralysed = true;
                        previousSpell = PlayerAttacks.Spells.None;
                        break;
                    case PlayerAttacks.Spells.Air:
                        enemyStats.enemy.buffs.Scared = true;
                        previousSpell = PlayerAttacks.Spells.None;
                        break;
                    case PlayerAttacks.Spells.Lightning:
                        enemyStats.enemy.buffs.Electrified = true;
                        previousSpell = PlayerAttacks.Spells.None;
                        break;
                }
                break;
            default:
                break;
        }

    }

    private void RemoveBuffs() 
    {
        object a = enemyStats.enemy.buffs;
        PropertyInfo[] info = a.GetType().GetProperties();
        for (int i = 0; i < info.Length; i++)
        {
            info[i].SetValue(a, false, null);
        }
    }

    #endregion
}
