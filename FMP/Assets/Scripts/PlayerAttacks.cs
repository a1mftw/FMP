using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttacks : MonoBehaviour
{

    public enum Attacks
    {
        Bludgeoning,
        Piercing,
        Slashing,
    }

    public enum Spells
    {
        None,
        Fire,
        Water,
        Air,
        Earth,
        Lightning,
    
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

    public PlayerStats playerStats;

    #region Physical Attacks
   
    public void BludgeoningAttack(Target target, GameObject enemy) 
    {
        enemy.GetComponent<EnemyCombat>().TakeBludgeoningDamage(playerStats.player.baseDamage,target);
    }

    public void PiercingAttack(Target target, GameObject enemy) 
    {
        enemy.GetComponent<EnemyCombat>().TakePiercingDamage(playerStats.player.baseDamage, target);
    }

    public void SlashingAttack(Target target, GameObject enemy) 
    {
        enemy.GetComponent<EnemyCombat>().TakeSlashingDamage(playerStats.player.baseDamage, target);
    }

    #endregion

    #region Physical Skills

    #endregion

    #region Magic Attacks

    public void FireDamage(GameObject enemy)
    {
        enemy.GetComponent<EnemyCombat>().TakeFireDamage(playerStats.player.baseDamage);
    }

    public void WaterDamage(GameObject enemy)
    {
        enemy.GetComponent<EnemyCombat>().TakeWaterDamage(playerStats.player.baseDamage);
    }

    public void AirDamage(GameObject enemy)
    {
        enemy.GetComponent<EnemyCombat>().TakeAirDamage(playerStats.player.baseDamage);
    }

    public void EarthDamage(GameObject enemy)
    {
        enemy.GetComponent<EnemyCombat>().TakeEarthDamage(playerStats.player.baseDamage);
    }

    public void LightningDamage(GameObject enemy)
    {
        enemy.GetComponent<EnemyCombat>().TakeLightningDamage(playerStats.player.baseDamage);
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
}
