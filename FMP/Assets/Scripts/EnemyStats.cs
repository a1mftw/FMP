using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStats : CharactersClass
{
    public string EnemyName;

    public struct EnemyAtributes
    {
       public int enemyLevel;
       public int baseDamage;
       public int baseArmor;
       public int maxHealth;
       public int currentHealth;
       public BodyPartHealth bodyPartHealth;
       public BuffDebuff buffs;
    }

    public enum Enemy
    {
        Fox,
        Random
    }

    public Enemy enemyType;

    public EnemyAtributes enemy;

    private void Start()
    {
        switch (enemyType)
        {
            case Enemy.Fox:
                SetFoxValues();
                break;

            case Enemy.Random:
                SetFoxValues();
                break;
            default:
                break;
        }
    }



    void SetFoxValues() 
    {
        enemy.enemyLevel = 1;
        enemy.baseDamage = 10;
        enemy.baseArmor = 5;
        enemy.maxHealth = 100;
        enemy.currentHealth = enemy.maxHealth;
        enemy.bodyPartHealth.headMaxHealth = 40;
        enemy.bodyPartHealth.torsoMaxHealth = 40;
        enemy.bodyPartHealth.armsMaxHealth = 40;
        enemy.bodyPartHealth.handsMaxHealth = 40;
        enemy.bodyPartHealth.legsMaxHealth = 40;
        enemy.bodyPartHealth.feetMaxHealth = 40;
        enemy.bodyPartHealth.headHealth = enemy.bodyPartHealth.headMaxHealth;
        enemy.bodyPartHealth.torsoHealth = enemy.bodyPartHealth.torsoMaxHealth;
        enemy.bodyPartHealth.armsHealth = enemy.bodyPartHealth.armsMaxHealth;
        enemy.bodyPartHealth.handsHealth = enemy.bodyPartHealth.handsMaxHealth;
        enemy.bodyPartHealth.legsHealth = enemy.bodyPartHealth.legsMaxHealth;
        enemy.bodyPartHealth.feetHealth = enemy.bodyPartHealth.feetMaxHealth;
        //foxEnemy.buffs.headDebuff = false;
        //foxEnemy.buffs.torsoDebuff = false;
        //foxEnemy.buffs.armsDebuff = false;
        //foxEnemy.buffs.handsDebuff = false;
        //foxEnemy.buffs.legsDebuff = false;
        //foxEnemy.buffs.feetDebuff = false;
        //foxEnemy.buffs.Paralysed = false;
        //foxEnemy.buffs.Clouded = false;
        //foxEnemy.buffs.Slowed = false;
        //foxEnemy.buffs.Scared = false;
        //foxEnemy.buffs.Unatunned = false;
        //foxEnemy.buffs.Unbalanced = false;
        //foxEnemy.buffs.Burning = false;
        //foxEnemy.buffs.Freezing = false;
        //foxEnemy.buffs.Drowning = false;
        //foxEnemy.buffs.Poisoned = false;
        //foxEnemy.buffs.Electrified = false;

    }

}
