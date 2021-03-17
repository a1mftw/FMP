using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttacks : MonoBehaviour
{
    Spells previousSpell = Spells.None;
    Spells currentSpell = Spells.None;
    public enum Attacks
    {
        Bludgeoning,
        Piercing,
        Slashing,
    }

    public enum Spells
    {
        
        Fire,
        Water,
        Air,
        Earth,
        Lightning,
        None,

    }


    public enum Target
    {
        Head,
        Torso,
        Arms,
        Hands,
        Legs,
        Feet
    }

    public Animator playerAnimations;
    public Animator battleCamera;
    public PlayerStats playerStats;

    #region Physical Attacks
   
    public void Blunt(Target target, GameObject enemy) 
    {
        StartCoroutine(BludgeoningAttack(target, enemy));
    }
    public void Pierce(Target target, GameObject enemy) 
    {
        StartCoroutine(PiercingAttack(target, enemy));
    }
    public void Slash(Target target, GameObject enemy) 
    {
        StartCoroutine(SlashingAttack(target,enemy));
    }

    #endregion

    #region Physical Skills

    #endregion

    #region Magic Attacks

    public void FireDamage(GameObject particleEffect, GameObject enemy)
    {
        StartCoroutine(SpellBall(particleEffect, enemy,Spells.Fire));
    }

    public void WaterDamage(GameObject particleEffect,GameObject enemy)
    {
        StartCoroutine(SpellBall(particleEffect, enemy, Spells.Water));
    }

    public void AirDamage(GameObject particleEffect, GameObject enemy)
    {
        StartCoroutine(SpellBall(particleEffect, enemy, Spells.Air));
    }

    public void EarthDamage(GameObject particleEffect, GameObject enemy)
    {
        StartCoroutine(SpellBall(particleEffect, enemy, Spells.Earth));
    }

    public void LightningDamage(GameObject particleEffect, GameObject enemy)
    {
        StartCoroutine(SpellBall(particleEffect, enemy, Spells.Lightning));
    }

    #endregion

    #region Magic Skills

    #endregion

    #region Alchemy Abilities

    #endregion

    #region Alchemy Skills

    #endregion

    #region Overdrives
    public void MyriadStrikes(GameObject enemy, int hits) 
    {
        if (hits>=30)
        {
            enemy.GetComponent<EnemyCombat>().TakeMyriadDamage(hits);
        }
        
    }

    #endregion


    public void TakeTailSwipeDamage(int damage, Target target) 
    {
        int damageDealt = damage - playerStats.player.baseArmor;

        switch (target)
        {
            case Target.Head:

                if (playerStats.player.bodyPartHealth.headHealth - damageDealt <= 0)
                {
                    playerStats.player.bodyPartHealth.headHealth = 0;
                    ApplyBodyPartDebuff(target);
                }
                else
                {
                    playerStats.player.bodyPartHealth.headHealth -= damageDealt;
                }

                break;
            case Target.Torso:

                if (playerStats.player.bodyPartHealth.torsoHealth - damageDealt <= 0)
                {
                    playerStats.player.bodyPartHealth.torsoHealth = 0;
                    ApplyBodyPartDebuff(target);
                }
                else
                {
                    playerStats.player.bodyPartHealth.torsoHealth -= damageDealt;
                }
                break;

            case Target.Arms:

                if (playerStats.player.bodyPartHealth.armsHealth - damageDealt <= 0)
                {
                    playerStats.player.bodyPartHealth.armsHealth = 0;
                    ApplyBodyPartDebuff(target);
                }
                else
                {
                    playerStats.player.bodyPartHealth.armsHealth -= damageDealt;
                }
                break;

            case Target.Hands:

                if (playerStats.player.bodyPartHealth.handsHealth - damageDealt <= 0)
                {
                    playerStats.player.bodyPartHealth.handsHealth = 0;
                    ApplyBodyPartDebuff(target);
                }
                else
                {
                    playerStats.player.bodyPartHealth.handsHealth -= damageDealt;
                }
                break;
            case Target.Legs:

                if (playerStats.player.bodyPartHealth.legsHealth - damageDealt <= 0)
                {
                    playerStats.player.bodyPartHealth.legsHealth = 0;
                    ApplyBodyPartDebuff(target);
                }
                else
                {
                    playerStats.player.bodyPartHealth.legsHealth -= damageDealt;
                }
                break;
            case Target.Feet:

                if (playerStats.player.bodyPartHealth.feetHealth - damageDealt <= 0)
                {
                    playerStats.player.bodyPartHealth.feetHealth = 0;
                    ApplyBodyPartDebuff(target);
                }
                else
                {
                    playerStats.player.bodyPartHealth.feetHealth -= damageDealt;
                }
                break;

        }

        playerStats.player.currentHealth -= damageDealt;


        Debug.Log("Torso Health: " + playerStats.player.bodyPartHealth.torsoHealth);
        Debug.Log("Dealt " + damageDealt + " damage\nRemains " + playerStats.player.currentHealth + " HP");



        if (playerStats.player.currentHealth <= 0)
        {
            Destroy(this.gameObject);
        }
    }

    #region Buff/Debuff/Ailments
    private void ApplyBodyPartDebuff(Target target)
    {
        switch (target)
        {
            case Target.Head:
                playerStats.player.buffs.headDebuff = true;
                break;
            case Target.Torso:
                playerStats.player.buffs.torsoDebuff = true;
                break;
            case Target.Arms:
                playerStats.player.buffs.armsDebuff = true;
                break;
            case Target.Hands:
                playerStats.player.buffs.handsDebuff = true;
                break;
            case Target.Legs:
                playerStats.player.buffs.legsDebuff = true;
                break;
            case Target.Feet:
                playerStats.player.buffs.feetDebuff = true;
                break;
            default:
                break;
        }
    }

    private void StatusEffects()
    {
        switch (currentSpell)
        {
            case Spells.Fire:

                switch (previousSpell)
                {
                    case Spells.Fire:
                        playerStats.player.buffs.Burning = true;
                        break;

                    case Spells.Water:
                        playerStats.player.buffs.Clouded = true;
                        break;

                    case Spells.Lightning:
                        playerStats.player.buffs.Unatunned = true;
                        break;
                }
                break;
            case Spells.Water:

                switch (previousSpell)
                {
                    case Spells.Fire:
                        playerStats.player.buffs.Clouded = true;
                        break;
                    case Spells.Water:
                        playerStats.player.buffs.Drowning = true;
                        break;
                    case Spells.Earth:
                        playerStats.player.buffs.Slowed = true;
                        break;
                    case Spells.Lightning:
                        playerStats.player.buffs.Paralysed = true;
                        break;
                }

                break;
            case Spells.Air:
                switch (previousSpell)
                {
                    case Spells.Air:
                        playerStats.player.buffs.Freezing = true;
                        break;
                    case Spells.Earth:
                        playerStats.player.buffs.Unbalanced = true;
                        break;
                    case Spells.Lightning:
                        playerStats.player.buffs.Scared = true;
                        break;
                }
                break;
            case Spells.Earth:
                switch (previousSpell)
                {

                    case Spells.Water:
                        playerStats.player.buffs.Slowed = true;
                        break;
                    case Spells.Air:
                        playerStats.player.buffs.Unbalanced = true;
                        break;
                    case Spells.Earth:
                        playerStats.player.buffs.Poisoned = true;
                        break;
                }
                break;
            case Spells.Lightning:
                switch (previousSpell)
                {
                    case Spells.Fire:
                        playerStats.player.buffs.Unatunned = true;
                        break;
                    case Spells.Water:
                        playerStats.player.buffs.Paralysed = true;
                        break;
                    case Spells.Air:
                        playerStats.player.buffs.Scared = true;
                        break;
                    case Spells.Lightning:
                        playerStats.player.buffs.Electrified = true;
                        break;
                }
                break;
            default:
                break;
        }

    }

    #endregion


    IEnumerator SlashingAttack(Target target, GameObject enemy) 
    {
        yield return new WaitForSeconds(1);
        var returnPos = gameObject.transform.position;
        float step = (6 / (gameObject.transform.position - enemy.transform.position).magnitude) * Time.fixedDeltaTime;
        float t = 0;

        playerAnimations.SetBool("Sprinting", true);

        while (Vector3.Distance(gameObject.transform.position,enemy.transform.position)>5)
        {
            t += step;
            gameObject.transform.position = Vector3.Lerp(gameObject.transform.position, enemy.transform.position, t);
            yield return new WaitForFixedUpdate();
        }

        playerAnimations.SetBool("Sprinting", false);
        battleCamera.Play("StrikeState");
        yield return new WaitForSeconds(0.5f);
        playerAnimations.SetBool("Slashing", true);
        yield return new WaitForSeconds(1);
        enemy.GetComponent<EnemyCombat>().TakeSlashingDamage(playerStats.player.baseDamage, target);
        playerAnimations.SetBool("Slashing", false);
        gameObject.transform.position = returnPos;
        battleCamera.Play("BattleCamera");

    }
    IEnumerator PiercingAttack(Target target, GameObject enemy)
    {
        yield return new WaitForSeconds(1);
        var returnPos = gameObject.transform.position;
        float step = (6 / (gameObject.transform.position - enemy.transform.position).magnitude) * Time.fixedDeltaTime;
        float t = 0;

        playerAnimations.SetBool("Sprinting", true);

        while (Vector3.Distance(gameObject.transform.position, enemy.transform.position) > 5)
        {
            t += step;
            gameObject.transform.position = Vector3.Lerp(gameObject.transform.position, enemy.transform.position, t);
            yield return new WaitForFixedUpdate();
        }
        playerAnimations.SetBool("Sprinting", false);
        battleCamera.Play("StrikeState");
        yield return new WaitForSeconds(0.5f);
        playerAnimations.SetBool("Piercing", true);
        yield return new WaitForSeconds(1);
        enemy.GetComponent<EnemyCombat>().TakePiercingDamage(playerStats.player.baseDamage, target);
        playerAnimations.SetBool("Piercing", false);
        gameObject.transform.position = returnPos;
        battleCamera.Play("BattleCamera");

    }
    IEnumerator BludgeoningAttack(Target target, GameObject enemy)
    {
        yield return new WaitForSeconds(1);
        var returnPos = gameObject.transform.position;
        float step = (6 / (gameObject.transform.position - enemy.transform.position).magnitude) * Time.fixedDeltaTime;
        float t = 0;

        playerAnimations.SetBool("Sprinting", true);

        while (Vector3.Distance(gameObject.transform.position, enemy.transform.position) > 5)
        {
            t += step;
            gameObject.transform.position = Vector3.Lerp(gameObject.transform.position, enemy.transform.position, t);
            yield return new WaitForFixedUpdate();
        }
        playerAnimations.SetBool("Sprinting", false);
        battleCamera.Play("StrikeState");
        yield return new WaitForSeconds(0.5f);
        playerAnimations.SetBool("Blunt", true);
        yield return new WaitForSeconds(1);
        enemy.GetComponent<EnemyCombat>().TakeSlashingDamage(playerStats.player.baseDamage, target);
        playerAnimations.SetBool("Blunt", false);
        gameObject.transform.position = returnPos;
        battleCamera.Play("BattleCamera");

    }
    IEnumerator SpellBall(GameObject particle, GameObject enemy, Spells spellType)
    {
        yield return new WaitForSeconds(1);

        float step = (6 / (particle.transform.position - enemy.transform.position).magnitude) * Time.fixedDeltaTime;
        float t = 0;

        while (Vector3.Distance(particle.transform.position, enemy.transform.position) > 1)
        {
            t += step;
            particle.transform.position = Vector3.Lerp(particle.transform.position, enemy.transform.position, t);
            yield return new WaitForFixedUpdate();
        }

        switch (spellType)
        {
            case Spells.Fire:
                enemy.GetComponent<EnemyCombat>().TakeFireDamage(playerStats.player.baseDamage);
                break;
            case Spells.Water:
                enemy.GetComponent<EnemyCombat>().TakeWaterDamage(playerStats.player.baseDamage);
                break;
            case Spells.Air:
                enemy.GetComponent<EnemyCombat>().TakeAirDamage(playerStats.player.baseDamage);
                break;
            case Spells.Earth:
                enemy.GetComponent<EnemyCombat>().TakeEarthDamage(playerStats.player.baseDamage);
                break;
            case Spells.Lightning:
                enemy.GetComponent<EnemyCombat>().TakeLightningDamage(playerStats.player.baseDamage);
                break;
        }
        
        Destroy(particle);
        battleCamera.Play("BattleCamera");

    }




}


