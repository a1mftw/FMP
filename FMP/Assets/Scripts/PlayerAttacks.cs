using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttacks : MonoBehaviour
{

    public enum Attacks
    {
        None,
        Bludgeoning,
        Piercing,
        Slashing,
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

}
