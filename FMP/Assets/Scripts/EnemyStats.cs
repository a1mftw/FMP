using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStats : MonoBehaviour
{
    public string EnemyName;

    public struct BodyPartHealth
    {
        public int headHealth;
        public int headMaxHealth;
        public int torsoHealth;
        public int torsoMaxHealth;
        public int armsHealth;
        public int armsMaxHealth;
        public int handsHealth;
        public int handsMaxHealth;
        public int legsHealth;
        public int legsMaxHealth;
        public int feetHealth;
        public int feetMaxHealth;

    }

    public struct BuffDebuff
    {
        public bool headDebuff;
        public bool torsoDebuff;
        public bool armsDebuff;
        public bool handsDebuff;
        public bool legsDebuff;
        public bool feetDebuff;
        public bool Paralysed;
        public bool Clouded;
        public bool Slowed;
        public bool Scared;
        public bool Unatunned;
        public bool Unbalanced;
        public bool Burning;
        public bool Freezing;
        public bool Drowning;
        public bool Poisoned;
        public bool Electrified;


    }

    public struct Fox
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
        Fox
    }

    public Enemy enemyType;

    public Fox foxEnemy;

    private void Start()
    {
        switch (enemyType)
        {
            case Enemy.Fox:
                SetFoxValues();
                break;
            default:
                break;
        }
    }




    void SetFoxValues() 
    {
        foxEnemy.enemyLevel = 1;
        foxEnemy.baseDamage = 10;
        foxEnemy.baseArmor = 5;
        foxEnemy.maxHealth = 100;
        foxEnemy.currentHealth = foxEnemy.maxHealth;
        foxEnemy.bodyPartHealth.headMaxHealth = 40;
        foxEnemy.bodyPartHealth.torsoMaxHealth = 40;
        foxEnemy.bodyPartHealth.armsMaxHealth = 40;
        foxEnemy.bodyPartHealth.handsMaxHealth = 40;
        foxEnemy.bodyPartHealth.legsMaxHealth = 40;
        foxEnemy.bodyPartHealth.feetMaxHealth = 40;
        foxEnemy.bodyPartHealth.headHealth = foxEnemy.bodyPartHealth.headMaxHealth;
        foxEnemy.bodyPartHealth.torsoHealth = foxEnemy.bodyPartHealth.torsoMaxHealth;
        foxEnemy.bodyPartHealth.armsHealth = foxEnemy.bodyPartHealth.armsMaxHealth;
        foxEnemy.bodyPartHealth.handsHealth = foxEnemy.bodyPartHealth.handsMaxHealth;
        foxEnemy.bodyPartHealth.legsHealth = foxEnemy.bodyPartHealth.legsMaxHealth;
        foxEnemy.bodyPartHealth.feetHealth = foxEnemy.bodyPartHealth.feetMaxHealth;
        foxEnemy.buffs.headDebuff = false;
        foxEnemy.buffs.torsoDebuff = false;
        foxEnemy.buffs.armsDebuff = false;
        foxEnemy.buffs.handsDebuff = false;
        foxEnemy.buffs.legsDebuff = false;
        foxEnemy.buffs.feetDebuff = false;
        foxEnemy.buffs.Paralysed = false;
        foxEnemy.buffs.Clouded = false;
        foxEnemy.buffs.Slowed = false;
        foxEnemy.buffs.Scared = false;
        foxEnemy.buffs.Unatunned = false;
        foxEnemy.buffs.Unbalanced = false;
        foxEnemy.buffs.Burning = false;
        foxEnemy.buffs.Freezing = false;
        foxEnemy.buffs.Drowning = false;
        foxEnemy.buffs.Poisoned = false;
        foxEnemy.buffs.Electrified = false;

    }

}
