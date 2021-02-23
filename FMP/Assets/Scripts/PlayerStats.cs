using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{

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

    public struct Player
    {
        public int playerLevel;
        public int baseDamage;
        public int baseArmor;
        public int maxHealth;
        public int currentHealth;
        public BodyPartHealth bodyPartHealth;
    }

    public enum Players
    {
        player,
    }

    public Players playableChar;

    public Player player;

    private void Start()
    {
        switch (playableChar)
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
        player.maxHealth = 100;
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
