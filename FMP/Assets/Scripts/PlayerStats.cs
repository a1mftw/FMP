using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : CharactersClass
{
    public struct Characters
    {
        public int playerLevel;
        public int baseDamage;
        public int baseArmor;
        public int maxHealth;
        public int currentHealth;
        public int maxMP;
        public int currentMp;
        public BodyPartHealth bodyPartHealth;
        public BuffDebuff buffs;
    }
    public enum Players
    {
        player,
    }

    public Players playableChars;

    public Characters player;

    private void Start()
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
        player.playerLevel = 1;
        player.baseDamage = 10;
        player.baseArmor = 5;
        player.maxMP = 200;
        player.currentMp = player.maxMP;
        player.maxHealth = 200;
        player.currentHealth = player.maxHealth;
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
