using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : CharactersClass
{
    public struct Characters
    {
        public Stats baseStats;
        public BodyPartHealth bodyPartHealth;
        public BuffDebuff buffs;
    }
    public enum Players
    {
        player,
    }

    public Players playableChars;

    public Characters player;

    private void Awake()
    {
        switch (playableChars)
        {
            case Players.player:
                SetPlayerValues();
                break;
            default:
                break;
        }
    }

    private void SetPlayerValues()
    {
        player.baseStats.level = 1;
        player.baseStats.baseDamage = 10;
        player.baseStats.baseArmor = 5;
        player.baseStats.baseSpeed = 10;
        player.baseStats.baseDodge = 10;
        player.baseStats.baseMagicDamage = 20;
        player.baseStats.magicResist = 5;
        player.baseStats.maxMana = 200;
        player.baseStats.currentMana = player.baseStats.maxMana;
        player.baseStats.maxHealth = 200;
        player.baseStats.currentHealth = player.baseStats.maxHealth;
        player.bodyPartHealth.headMaxHealth = 40;
        player.bodyPartHealth.torsoMaxHealth = 40;
        player.bodyPartHealth.armsMaxHealth = 40;
        player.bodyPartHealth.handsMaxHealth = 40;
        player.bodyPartHealth.legsMaxHealth = 40;
        player.bodyPartHealth.feetMaxHealth = 40;
        player.bodyPartHealth.headHealth = player.bodyPartHealth.headMaxHealth;
        player.bodyPartHealth.torsoHealth = player.bodyPartHealth.torsoMaxHealth;
        player.bodyPartHealth.armsHealth = player.bodyPartHealth.armsMaxHealth;
        player.bodyPartHealth.handsHealth = player.bodyPartHealth.handsMaxHealth;
        player.bodyPartHealth.legsHealth = player.bodyPartHealth.legsMaxHealth;
        player.bodyPartHealth.feetHealth = player.bodyPartHealth.feetMaxHealth;
    }
}
