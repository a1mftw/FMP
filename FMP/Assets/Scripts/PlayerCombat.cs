using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerCombat : MonoBehaviour
{

    public GameObject UIControls;
    public BattleSystem battleSystem;
    public PlayerAttacks playerAttacks;

    public enum Stances 
    {
        Physical,
        Magic,
        Alchemy,
        Overdrive

    }

    public Stances stance;
    public PlayerAttacks.Attacks attack;
    public PlayerAttacks.Target target;
    public Animator cameraController;

    private GameObject enemyTarget;

    private bool firstPress = true;
    private bool targeting = false;

    private Text FirstButton;
    private Text SecondButton;
    private Text ThirdButton;
    private Text FourthButton;

    public GameObject bodyPartUI;
    public Button partHighlight;

    private int bodyPartCount = 0;
    public bool BattleOver = false;

    private void Update()
    {
        if (BattleOver)
        {
                Flee();
        }
       

        if (targeting)
        {



            //Change between enemy
            if (Input.GetButtonDown("Right"))
            {
                //If theres more than 1 enemy change camera and target here
            }

            if (Input.GetButtonDown("Left"))
            {
                //If theres more than 1 enemy change camera and target here
            }


            //Change between body part
            if (Input.GetButtonDown("Up"))
            {
                if (--bodyPartCount < 0)
                {
                    bodyPartCount = 5;
                }
            }

            if (Input.GetButtonDown("Down"))
            {
                if (++bodyPartCount > 5)
                {
                    bodyPartCount = 0;

                }
            }

            target = (PlayerAttacks.Target)bodyPartCount;

            if (Input.GetButtonDown("Submit"))
            {
                switch (attack)
                {
                    case PlayerAttacks.Attacks.Bludgeoning:
                        playerAttacks.BludgeoningAttack(target,enemyTarget);
                        break;
                    case PlayerAttacks.Attacks.Piercing:
                        playerAttacks.PiercingAttack(target, enemyTarget);
                        break;
                    case PlayerAttacks.Attacks.Slashing:
                        playerAttacks.SlashingAttack(target, enemyTarget);
                        break;
                    default:
                        break;
                }

                bodyPartUI.SetActive(false);
                targeting = false;
                cameraController.Play("BattleCamera");
                bodyPartCount = 0;
                battleSystem.changeState(BattleState.EnemyTurn);
            }


            if (Input.GetButtonDown("Cancel")) 
            {
                targeting = false;
                bodyPartCount = 0;
                target = PlayerAttacks.Target.Head;
                bodyPartUI.SetActive(false);
                cameraController.Play("BattleCamera");
            }


        }
    }


    public void SetUI() 
    {

        UIControls.SetActive(true);

        FirstButton = GameObject.Find("FunctionText1").GetComponent<Text>();
        SecondButton = GameObject.Find("FunctionText2").GetComponent<Text>();
        ThirdButton = GameObject.Find("FunctionText3").GetComponent<Text>();
        FourthButton = GameObject.Find("FunctionText4").GetComponent<Text>();

    }

    public void FirstButtonAction() 
    {

        switch (stance)
        {
            case Stances.Physical:
                
                if (firstPress)
                {

                    FirstButton.text = "Bludgeoning";
                    SecondButton.text = "Piercing";
                    ThirdButton.text = "Slashing";
                    FourthButton.text = "Back";
                    firstPress = false;

                }
                else
                {
                    if (!targeting)
                    {
                        attack = PlayerAttacks.Attacks.Bludgeoning;
                        enemyTarget = battleSystem.enemyTarget;
                        partHighlight.Select();
                        targeting = true;
                        //Change camera to enemy
                        cameraController.Play("TargetingEnemy");


                        //Change color of parts if they are damaged
                        EnemyColorTargetSystem();


                        //Show body part select
                        bodyPartUI.SetActive(true);
                    }
                }

                break;
            case Stances.Magic:
                break;
            case Stances.Alchemy:
                break;
            case Stances.Overdrive:
                break;
            default:
                break;
        }

    }
    public void SecondButtonAction() 
    {
       
            switch (stance)
            {
                case Stances.Physical:
                    
                    if (firstPress)
                    {

                    }
                    else
                    {
                    if (!targeting)
                    {
                        attack = PlayerAttacks.Attacks.Piercing;
                        enemyTarget = battleSystem.enemyTarget;
                        partHighlight.Select();
                        targeting = true;


                        //Change camera to enemy
                        cameraController.Play("TargetingEnemy");


                        //Change color of parts if they are damaged
                        EnemyColorTargetSystem();


                        //Show body part select
                        bodyPartUI.SetActive(true);
                    }
                }

                break;
                case Stances.Magic:
                    break;
                case Stances.Alchemy:
                    break;
                case Stances.Overdrive:
                    break;
            }
        
       

        

    }
    public void ThirdButtonAction() 
    {
        if (firstPress)
        {
            
        }
        else
        {
            if (!targeting)
            {
                attack = PlayerAttacks.Attacks.Slashing;
                enemyTarget = battleSystem.enemyTarget;
                partHighlight.Select();
                targeting = true;
                //Change camera to enemy



                //Change color of parts if they are damaged
                EnemyColorTargetSystem();


                //Show body part select
                bodyPartUI.SetActive(true);
            }
        }

    }
    public void FourthButtonAction() 
    {
        if (firstPress)
        {
            Flee();
        }
        else
        {
            if (!targeting)
            {
                FirstButton.text = "Attack";
                SecondButton.text = "Stance Ability";
                ThirdButton.text = "Change Stance";
                FourthButton.text = "Flee";
                firstPress = true;
            }
        }
    }


    void Flee() 
    {
        battleSystem.tranController.FleeBattle();
        BattleOver = false;
    }

    void EnemyColorTargetSystem() 
    {
        EnemyStats enemyStats = enemyTarget.GetComponent<EnemyStats>();

        bodyPartUI.transform.GetChild(1).GetComponent<Slider>().maxValue = enemyStats.foxEnemy.maxHealth;
        bodyPartUI.transform.GetChild(1).GetComponent<Slider>().value = enemyStats.foxEnemy.currentHealth;

        if (enemyStats.foxEnemy.bodyPartHealth.headHealth <= 0)
        {
            bodyPartUI.transform.GetChild(2).GetChild(0).GetComponent<Image>().color = Color.gray;
        }
        else if(enemyStats.foxEnemy.bodyPartHealth.headHealth <= enemyStats.foxEnemy.bodyPartHealth.headMaxHealth / 4)
        {
            bodyPartUI.transform.GetChild(2).GetChild(0).GetComponent<Image>().color = Color.red;
        }
        else if(enemyStats.foxEnemy.bodyPartHealth.headHealth <= enemyStats.foxEnemy.bodyPartHealth.headMaxHealth / 2)
        {
            bodyPartUI.transform.GetChild(2).GetChild(0).GetComponent<Image>().color = Color.yellow;
        }
        else
        {
            bodyPartUI.transform.GetChild(2).GetChild(0).GetComponent<Image>().color = Color.green;
        }

        if (enemyStats.foxEnemy.bodyPartHealth.torsoHealth <= 0)
        {
            bodyPartUI.transform.GetChild(2).GetChild(1).GetComponent<Image>().color = Color.gray;
        }
        else if (enemyStats.foxEnemy.bodyPartHealth.torsoHealth <= enemyStats.foxEnemy.bodyPartHealth.torsoMaxHealth / 4)
        {
            bodyPartUI.transform.GetChild(2).GetChild(1).GetComponent<Image>().color = Color.red;
        }
        else if (enemyStats.foxEnemy.bodyPartHealth.torsoHealth <= enemyStats.foxEnemy.bodyPartHealth.torsoMaxHealth / 2)
        {
            bodyPartUI.transform.GetChild(2).GetChild(1).GetComponent<Image>().color = Color.yellow;
        }
        else
        {
            bodyPartUI.transform.GetChild(2).GetChild(1).GetComponent<Image>().color = Color.green;
        }


        if (enemyStats.foxEnemy.bodyPartHealth.armsHealth <= 0)
        {
            bodyPartUI.transform.GetChild(2).GetChild(2).GetComponent<Image>().color = Color.gray;
        }
        else if (enemyStats.foxEnemy.bodyPartHealth.armsHealth <= enemyStats.foxEnemy.bodyPartHealth.armsMaxHealth / 4)
        {
            bodyPartUI.transform.GetChild(2).GetChild(2).GetComponent<Image>().color = Color.red;
        }
        else if (enemyStats.foxEnemy.bodyPartHealth.armsHealth <= enemyStats.foxEnemy.bodyPartHealth.armsMaxHealth / 2)
        {
            bodyPartUI.transform.GetChild(2).GetChild(2).GetComponent<Image>().color = Color.yellow;
        }
        else
        {
            bodyPartUI.transform.GetChild(2).GetChild(2).GetComponent<Image>().color = Color.green;
        }

        if (enemyStats.foxEnemy.bodyPartHealth.handsHealth <= 0)
        {
            bodyPartUI.transform.GetChild(2).GetChild(3).GetComponent<Image>().color = Color.gray;
        }
        else if (enemyStats.foxEnemy.bodyPartHealth.handsHealth <= enemyStats.foxEnemy.bodyPartHealth.handsMaxHealth / 4)
        {
            bodyPartUI.transform.GetChild(2).GetChild(3).GetComponent<Image>().color = Color.red;
        }
        else if (enemyStats.foxEnemy.bodyPartHealth.handsHealth <= enemyStats.foxEnemy.bodyPartHealth.handsMaxHealth / 2)
        {
            bodyPartUI.transform.GetChild(2).GetChild(3).GetComponent<Image>().color = Color.yellow;
        }
        else
        {
            bodyPartUI.transform.GetChild(2).GetChild(3).GetComponent<Image>().color = Color.green;
        }

        if (enemyStats.foxEnemy.bodyPartHealth.legsHealth <= 0)
        {
            bodyPartUI.transform.GetChild(2).GetChild(4).GetComponent<Image>().color = Color.gray;
        }
        else if (enemyStats.foxEnemy.bodyPartHealth.legsHealth <= enemyStats.foxEnemy.bodyPartHealth.legsMaxHealth / 4)
        {
            bodyPartUI.transform.GetChild(2).GetChild(4).GetComponent<Image>().color = Color.red;
        }
        else if (enemyStats.foxEnemy.bodyPartHealth.legsHealth <= enemyStats.foxEnemy.bodyPartHealth.legsMaxHealth / 2)
        {
            bodyPartUI.transform.GetChild(2).GetChild(4).GetComponent<Image>().color = Color.yellow;
        }
        else
        {
            bodyPartUI.transform.GetChild(2).GetChild(4).GetComponent<Image>().color = Color.green;
        }

        if (enemyStats.foxEnemy.bodyPartHealth.feetHealth <= 0)
        {
            bodyPartUI.transform.GetChild(2).GetChild(5).GetComponent<Image>().color = Color.gray;
        }
        else if (enemyStats.foxEnemy.bodyPartHealth.feetHealth <= enemyStats.foxEnemy.bodyPartHealth.feetMaxHealth / 4)
        {
            bodyPartUI.transform.GetChild(2).GetChild(5).GetComponent<Image>().color = Color.red;
        }
        else if (enemyStats.foxEnemy.bodyPartHealth.feetHealth <= enemyStats.foxEnemy.bodyPartHealth.feetMaxHealth / 2)
        {
            bodyPartUI.transform.GetChild(2).GetChild(5).GetComponent<Image>().color = Color.yellow;
        }
        else
        {
            bodyPartUI.transform.GetChild(2).GetChild(5).GetComponent<Image>().color = Color.green;
        }

    }

}
