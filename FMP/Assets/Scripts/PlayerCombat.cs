using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerCombat : MonoBehaviour
{

    public ListPositionCtrl spellListControl;

    public GameObject magicSpellList;
    public GameObject UIControls;
    public GameObject PlayerUI;
    public BattleSystem battleSystem;
    public PlayerAttacks playerAttacks;
    public GameObject spellPlaceHolder;

    public enum Stances 
    {
        Physical,
        Magic,
        Alchemy,
        Overdrive

    }

    public Sprite physicalImage;
    public Sprite magicImage;
    public Sprite alchemyImage;
    public Sprite overdriveImage;

    public Stances stance = Stances.Physical;
    public PlayerAttacks.Attacks attack;
    public PlayerAttacks.Spells spell;
    public PlayerAttacks.Target target;
    public Animator cameraController;
    public AnimationManager animationManager;

    private GameObject enemyTarget;

    private bool firstPress = true;
    private bool targeting = false;
    private bool playerTargeting = false;
    private bool spellTargeting = false;
    private bool changeStanceActive = false;
    private bool spellChoiceActive = false;
    private bool overdriveTargeting = false;
    private bool MyriadStrikes = false;

    private Text FirstButton;
    private Text SecondButton;
    private Text ThirdButton;
    private Text FourthButton;

    public GameObject OverdriveText;
    public GameObject bodyPartUI;
    public GameObject ChangeStanceUI;
    public Button partHighlight;
    public Button spellHighlight;


    private int hits = 0;
    private int bodyPartCount = 0;
    public bool BattleOver = false;
    

    private void Update()
    {
        if (BattleOver)
        {
                Flee();
        }

        if (changeStanceActive)
        {
            if (Input.GetButtonDown("Up"))
            {
                ChangeStance(Stances.Physical);
                ChangeStanceUI.SetActive(false);
            }

            if (Input.GetButtonDown("Left"))
            {
                ChangeStance(Stances.Magic);
                ChangeStanceUI.SetActive(false);
            }

            if (Input.GetButtonDown("Right"))
            {
                ChangeStance(Stances.Alchemy);
                ChangeStanceUI.SetActive(false);
            }

            if (Input.GetButtonDown("Down"))
            {
                ChangeStance(Stances.Overdrive);
                ChangeStanceUI.SetActive(false);

            }

            
            
        }

        if (spellTargeting)
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

            if (Input.GetButtonDown("Submit"))
            {

                switch (spell)
                {
                    case PlayerAttacks.Spells.Fire:
                        playerAttacks.FireDamage(animationManager.GetMagicParticle(),enemyTarget);
                        Debug.Log("Fire damage");
                        break;
                    case PlayerAttacks.Spells.Water:
                        playerAttacks.WaterDamage(animationManager.GetMagicParticle(), enemyTarget);
                        Debug.Log("Water damage");
                        break;
                    case PlayerAttacks.Spells.Air:
                        playerAttacks.AirDamage(animationManager.GetMagicParticle(), enemyTarget);
                        Debug.Log("Air damage");
                        break;
                    case PlayerAttacks.Spells.Earth:
                        playerAttacks.EarthDamage(animationManager.GetMagicParticle(), enemyTarget);
                        Debug.Log("Earth damage");
                        break;
                    case PlayerAttacks.Spells.Lightning:
                        playerAttacks.LightningDamage(animationManager.GetMagicParticle(), enemyTarget);
                        Debug.Log("Lightning damage");
                        break;
                }

                spellTargeting = false;
                firstPress = true;
                cameraController.Play("BattleCamera");
                battleSystem.ChangeState(BattleState.EnemyTurn);

            }
        }

        if (spellChoiceActive)
        {
            if (Input.GetButtonDown("Up"))
            {
                
                spellListControl.MoveOneUnitUp();
                if (spellListControl.GetCenteredContentID()+1>4)
                {
                    animationManager.MagicCastParticle(AnimationManager.Effects.Fire, spellPlaceHolder.transform.position);
                }
                else
                {
                    animationManager.MagicCastParticle((AnimationManager.Effects)spellListControl.GetCenteredContentID()+1, spellPlaceHolder.transform.position);
                }
                
            }

            if (Input.GetButtonDown("Down"))
            {
                
                spellListControl.MoveOneUnitDown();
                if (spellListControl.GetCenteredContentID() - 1 < 0)
                {
                    animationManager.MagicCastParticle(AnimationManager.Effects.Electric, spellPlaceHolder.transform.position);
                }
                else
                {
                    animationManager.MagicCastParticle((AnimationManager.Effects)spellListControl.GetCenteredContentID() - 1, spellPlaceHolder.transform.position);
                }
            }

            if (Input.GetButtonDown("Submit"))
            {
                ChooseSpellTarget(spellListControl.GetCenteredContentID());
            }
        }

        if (overdriveTargeting) 
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

            if (Input.GetButtonDown("Submit"))
            {
                hits = 0;
                overdriveTargeting = false;
                OverdriveText.SetActive(true);
                StartCoroutine("MyriadOverdrive");
            }

            if (Input.GetButtonDown("Cancel"))
            {
                overdriveTargeting = false;
                cameraController.Play("BattleCamera");
            }

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
                switch (stance)
                {
                    case Stances.Physical:
                        switch (attack)
                        {
                            case PlayerAttacks.Attacks.Bludgeoning:
                                playerAttacks.Blunt(target, enemyTarget);
                                break;
                            case PlayerAttacks.Attacks.Piercing:
                                playerAttacks.Pierce(target, enemyTarget);
                                break;
                            case PlayerAttacks.Attacks.Slashing:
                                playerAttacks.Slash(target, enemyTarget);
                                break;
                            default:
                                break;
                        }
                        break;
                    case Stances.Magic:
                        break;
                    case Stances.Alchemy:

                        switch (target)
                        {
                            case PlayerAttacks.Target.Head:
                                break;
                            case PlayerAttacks.Target.Torso:
                                break;
                            case PlayerAttacks.Target.Arms:
                                break;
                            case PlayerAttacks.Target.Hands:
                                break;
                            case PlayerAttacks.Target.Legs:
                                break;
                            case PlayerAttacks.Target.Feet:
                                break;
                            default:
                                break;
                        }
                        break;

                    case Stances.Overdrive:
                        break;
                    default:
                        break;
                }
                

                bodyPartUI.SetActive(false);
                targeting = false;
                firstPress = true;
                cameraController.Play("BattleCamera");
                bodyPartCount = 0;
                battleSystem.ChangeState(BattleState.EnemyTurn);
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

        if (MyriadStrikes)
        {
            if (Input.GetButtonDown("Jump"))
            {
                ++hits;
                OverdriveText.GetComponent<Text>().text = hits.ToString() + "/30 Hits";

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

        FirstButton.text = "Attack";
        SecondButton.text = "Abilities";
        ThirdButton.text = "Change Stance";
        FourthButton.text = "Flee";
        firstPress = true;

        ChangeStance(stance);
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
                    if (!targeting && !changeStanceActive)
                    {
                        attack = PlayerAttacks.Attacks.Bludgeoning;
                        enemyTarget = battleSystem.enemyTarget;
                        partHighlight.Select();
                        targeting = true;
                        //Change camera to enemy
                        cameraController.Play("TargetingEnemy");


                        //Change color of parts if they are damaged
                        EnemyColorTargetSystem();
                        BuffDebuffs();


                        //Show body part select
                        bodyPartUI.SetActive(true);
                    }
                }

                break;
            case Stances.Magic:
                if (firstPress)
                {
                    cameraController.Play("MagicState");
                    magicSpellList.SetActive(true);
                    spellHighlight.Select();
                    spellChoiceActive = true;
                    animationManager.MagicCastParticle((AnimationManager.Effects)spellListControl.GetCenteredContentID(),spellPlaceHolder.transform.position);
                    firstPress = false;
                }
                break;
            case Stances.Alchemy:
                if (firstPress)
                {
                    cameraController.Play("AlchemyState");
                    partHighlight.Select();
                    targeting = true;

                    //Change color of parts if they are damaged
                    PlayerColorTargetSystem();
                    BuffDebuffs(true);

                    //Show body part select
                    bodyPartUI.SetActive(true);
                }
                break;
            case Stances.Overdrive:
                if (firstPress)
                {
                    FirstButton.text = "Myriad Strikes";
                    SecondButton.text = "";
                    ThirdButton.text = "";
                    FourthButton.text = "Back";
                    firstPress = false;
                }
                else
                {
                    if (!targeting && !changeStanceActive)
                    {
                        enemyTarget = battleSystem.enemyTarget;
                        overdriveTargeting = true;

                        //Change camera to enemy
                        cameraController.Play("TargetingEnemy");
                    }
                }
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
                        FirstButton.text = "War Cry";
                        SecondButton.text = "Piercing Gaze";
                        ThirdButton.text = "Meditate";
                        FourthButton.text = "Back";
                        firstPress = false;
                }
                    else
                    {
                    if (!targeting && !changeStanceActive)
                    {
                        attack = PlayerAttacks.Attacks.Piercing;
                        enemyTarget = battleSystem.enemyTarget;
                        partHighlight.Select();
                        targeting = true;


                        //Change camera to enemy
                        cameraController.Play("TargetingEnemy");


                        //Change color of parts if they are damaged
                        EnemyColorTargetSystem();
                        //Turn the buff debuff UI on
                        BuffDebuffs();


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
            changeStanceActive = true;
            ChangeStanceUI.SetActive(true);
            firstPress = false;
        }
        else
        {

            switch (stance)
            {
                case Stances.Physical:
                    if (!targeting && !changeStanceActive)
                    {
                        attack = PlayerAttacks.Attacks.Slashing;
                        enemyTarget = battleSystem.enemyTarget;
                        partHighlight.Select();
                        targeting = true;
                        cameraController.Play("TargetingEnemy");


                        //Change color of parts if they are damaged
                        EnemyColorTargetSystem();
                        BuffDebuffs();


                        //Show body part select
                        bodyPartUI.SetActive(true);
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

    }
    public void FourthButtonAction() 
    {
        if (firstPress && !changeStanceActive)
        {
            Flee();
        }
        else
        {
            if (!targeting && !changeStanceActive && !overdriveTargeting && !MyriadStrikes)
            {
                FirstButton.text = "Attack";
                SecondButton.text = "Abilities";
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
    void PlayerColorTargetSystem() 
    {
        PlayerStats playerStats = GameObject.Find("Player").GetComponent<PlayerStats>();

        bodyPartUI.transform.GetChild(1).GetComponent<Slider>().maxValue = playerStats.player.maxHealth;
        bodyPartUI.transform.GetChild(1).GetComponent<Slider>().value = playerStats.player.currentHealth;

        if (playerStats.player.bodyPartHealth.headHealth <= 0)
        {
            bodyPartUI.transform.GetChild(2).GetChild(0).GetComponent<Image>().color = Color.gray;
        }
        else if (playerStats.player.bodyPartHealth.headHealth <= playerStats.player.bodyPartHealth.headMaxHealth / 4)
        {
            bodyPartUI.transform.GetChild(2).GetChild(0).GetComponent<Image>().color = Color.red;
        }
        else if (playerStats.player.bodyPartHealth.headHealth <= playerStats.player.bodyPartHealth.headMaxHealth / 2)
        {
            bodyPartUI.transform.GetChild(2).GetChild(0).GetComponent<Image>().color = Color.yellow;
        }
        else
        {
            bodyPartUI.transform.GetChild(2).GetChild(0).GetComponent<Image>().color = Color.green;
        }

        if (playerStats.player.bodyPartHealth.torsoHealth <= 0)
        {
            bodyPartUI.transform.GetChild(2).GetChild(1).GetComponent<Image>().color = Color.gray;
        }
        else if (playerStats.player.bodyPartHealth.torsoHealth <= playerStats.player.bodyPartHealth.torsoMaxHealth / 4)
        {
            bodyPartUI.transform.GetChild(2).GetChild(1).GetComponent<Image>().color = Color.red;
        }
        else if (playerStats.player.bodyPartHealth.torsoHealth <= playerStats.player.bodyPartHealth.torsoMaxHealth / 2)
        {
            bodyPartUI.transform.GetChild(2).GetChild(1).GetComponent<Image>().color = Color.yellow;
        }
        else
        {
            bodyPartUI.transform.GetChild(2).GetChild(1).GetComponent<Image>().color = Color.green;
        }


        if (playerStats.player.bodyPartHealth.armsHealth <= 0)
        {
            bodyPartUI.transform.GetChild(2).GetChild(2).GetComponent<Image>().color = Color.gray;
        }
        else if (playerStats.player.bodyPartHealth.armsHealth <= playerStats.player.bodyPartHealth.armsMaxHealth / 4)
        {
            bodyPartUI.transform.GetChild(2).GetChild(2).GetComponent<Image>().color = Color.red;
        }
        else if (playerStats.player.bodyPartHealth.armsHealth <= playerStats.player.bodyPartHealth.armsMaxHealth / 2)
        {
            bodyPartUI.transform.GetChild(2).GetChild(2).GetComponent<Image>().color = Color.yellow;
        }
        else
        {
            bodyPartUI.transform.GetChild(2).GetChild(2).GetComponent<Image>().color = Color.green;
        }

        if (playerStats.player.bodyPartHealth.handsHealth <= 0)
        {
            bodyPartUI.transform.GetChild(2).GetChild(3).GetComponent<Image>().color = Color.gray;
        }
        else if (playerStats.player.bodyPartHealth.handsHealth <= playerStats.player.bodyPartHealth.handsMaxHealth / 4)
        {
            bodyPartUI.transform.GetChild(2).GetChild(3).GetComponent<Image>().color = Color.red;
        }
        else if (playerStats.player.bodyPartHealth.handsHealth <= playerStats.player.bodyPartHealth.handsMaxHealth / 2)
        {
            bodyPartUI.transform.GetChild(2).GetChild(3).GetComponent<Image>().color = Color.yellow;
        }
        else
        {
            bodyPartUI.transform.GetChild(2).GetChild(3).GetComponent<Image>().color = Color.green;
        }

        if (playerStats.player.bodyPartHealth.legsHealth <= 0)
        {
            bodyPartUI.transform.GetChild(2).GetChild(4).GetComponent<Image>().color = Color.gray;
        }
        else if (playerStats.player.bodyPartHealth.legsHealth <= playerStats.player.bodyPartHealth.legsMaxHealth / 4)
        {
            bodyPartUI.transform.GetChild(2).GetChild(4).GetComponent<Image>().color = Color.red;
        }
        else if (playerStats.player.bodyPartHealth.legsHealth <= playerStats.player.bodyPartHealth.legsMaxHealth / 2)
        {
            bodyPartUI.transform.GetChild(2).GetChild(4).GetComponent<Image>().color = Color.yellow;
        }
        else
        {
            bodyPartUI.transform.GetChild(2).GetChild(4).GetComponent<Image>().color = Color.green;
        }

        if (playerStats.player.bodyPartHealth.feetHealth <= 0)
        {
            bodyPartUI.transform.GetChild(2).GetChild(5).GetComponent<Image>().color = Color.gray;
        }
        else if (playerStats.player.bodyPartHealth.feetHealth <= playerStats.player.bodyPartHealth.feetMaxHealth / 4)
        {
            bodyPartUI.transform.GetChild(2).GetChild(5).GetComponent<Image>().color = Color.red;
        }
        else if (playerStats.player.bodyPartHealth.feetHealth <= playerStats.player.bodyPartHealth.feetMaxHealth / 2)
        {
            bodyPartUI.transform.GetChild(2).GetChild(5).GetComponent<Image>().color = Color.yellow;
        }
        else
        {
            bodyPartUI.transform.GetChild(2).GetChild(5).GetComponent<Image>().color = Color.green;
        }
    }

    public void ChooseSpellTarget(int spellChosen) 
    {
        spell = (PlayerAttacks.Spells)spellChosen;
        enemyTarget = battleSystem.enemyTarget;
        magicSpellList.SetActive(false);
        spellChoiceActive = false;
        spellTargeting = true;
        cameraController.Play("TargetingEnemy");

    }

    void ChangeStance(Stances newStance) 
    {
        stance = newStance;

        switch (stance)
        {
            case Stances.Physical:
                PlayerUI.transform.GetChild(2).GetChild(1).GetComponent<Image>().sprite = physicalImage;
                break;
            case Stances.Magic:
                PlayerUI.transform.GetChild(2).GetChild(1).GetComponent<Image>().sprite = magicImage;
                break;
            case Stances.Alchemy:
                PlayerUI.transform.GetChild(2).GetChild(1).GetComponent<Image>().sprite = alchemyImage;
                break;
            case Stances.Overdrive:
                PlayerUI.transform.GetChild(2).GetChild(1).GetComponent<Image>().sprite = overdriveImage;
                break;
        }

        changeStanceActive = false;
        firstPress = true;
    }

    private void BuffDebuffs(bool player = false)
    {
        var image = new Color();
        if (player)
        {
            PlayerStats playerStats = GetComponent<PlayerStats>();
            if (playerStats.player.buffs.headDebuff)
            {
                image = bodyPartUI.transform.GetChild(3).GetChild(0).GetChild(0).GetComponent<Image>().color;
                image.a = 1f;
                bodyPartUI.transform.GetChild(3).GetChild(0).GetChild(0).GetComponent<Image>().color = image;
            }
            else
            {
                image = bodyPartUI.transform.GetChild(3).GetChild(0).GetChild(0).GetComponent<Image>().color;
                image.a = 0.3f;
                bodyPartUI.transform.GetChild(3).GetChild(0).GetChild(0).GetComponent<Image>().color = image;
            }


            if (playerStats.player.buffs.torsoDebuff)
            {
                image = bodyPartUI.transform.GetChild(3).GetChild(1).GetChild(0).GetComponent<Image>().color;
                image.a = 1f;
                bodyPartUI.transform.GetChild(3).GetChild(1).GetChild(0).GetComponent<Image>().color = image;
            }
            else
            {
                image = bodyPartUI.transform.GetChild(3).GetChild(1).GetChild(0).GetComponent<Image>().color;
                image.a = 0.3f;
                bodyPartUI.transform.GetChild(3).GetChild(1).GetChild(0).GetComponent<Image>().color = image;
            }

            if (playerStats.player.buffs.armsDebuff)
            {
                image = bodyPartUI.transform.GetChild(3).GetChild(2).GetChild(0).GetComponent<Image>().color;
                image.a = 1f;
                bodyPartUI.transform.GetChild(3).GetChild(2).GetChild(0).GetComponent<Image>().color = image;
            }
            else
            {
                image = bodyPartUI.transform.GetChild(3).GetChild(2).GetChild(0).GetComponent<Image>().color;
                image.a = 0.3f;
                bodyPartUI.transform.GetChild(3).GetChild(2).GetChild(0).GetComponent<Image>().color = image;
            }

            if (playerStats.player.buffs.handsDebuff)
            {
                image = bodyPartUI.transform.GetChild(3).GetChild(3).GetChild(0).GetComponent<Image>().color;
                image.a = 1f;
                bodyPartUI.transform.GetChild(3).GetChild(3).GetChild(0).GetComponent<Image>().color = image;
            }
            else
            {
                image = bodyPartUI.transform.GetChild(3).GetChild(3).GetChild(0).GetComponent<Image>().color;
                image.a = 0.3f;
                bodyPartUI.transform.GetChild(3).GetChild(3).GetChild(0).GetComponent<Image>().color = image;
            }

            if (playerStats.player.buffs.legsDebuff)
            {
                image = bodyPartUI.transform.GetChild(3).GetChild(4).GetChild(0).GetComponent<Image>().color;
                image.a = 1f;
                bodyPartUI.transform.GetChild(3).GetChild(4).GetChild(0).GetComponent<Image>().color = image;
            }
            else
            {
                image = bodyPartUI.transform.GetChild(3).GetChild(4).GetChild(0).GetComponent<Image>().color;
                image.a = 0.3f;
                bodyPartUI.transform.GetChild(3).GetChild(4).GetChild(0).GetComponent<Image>().color = image;
            }

            if (playerStats.player.buffs.feetDebuff)
            {
                image = bodyPartUI.transform.GetChild(3).GetChild(5).GetChild(0).GetComponent<Image>().color;
                image.a = 1f;
                bodyPartUI.transform.GetChild(3).GetChild(5).GetChild(0).GetComponent<Image>().color = image;
            }
            else
            {
                image = bodyPartUI.transform.GetChild(3).GetChild(5).GetChild(0).GetComponent<Image>().color;
                image.a = 0.3f;
                bodyPartUI.transform.GetChild(3).GetChild(5).GetChild(0).GetComponent<Image>().color = image;
            }

            if (playerStats.player.buffs.Paralysed)
            {
                image = bodyPartUI.transform.GetChild(4).GetChild(0).GetChild(0).GetComponent<Image>().color;
                image.a = 1f;
                bodyPartUI.transform.GetChild(4).GetChild(0).GetChild(0).GetComponent<Image>().color = image;
            }
            else
            {
                image = bodyPartUI.transform.GetChild(4).GetChild(0).GetChild(0).GetComponent<Image>().color;
                image.a = 0.3f;
                bodyPartUI.transform.GetChild(4).GetChild(0).GetChild(0).GetComponent<Image>().color = image;
            }

            if (playerStats.player.buffs.Clouded)
            {
                image = bodyPartUI.transform.GetChild(4).GetChild(1).GetChild(0).GetComponent<Image>().color;
                image.a = 1f;
                bodyPartUI.transform.GetChild(4).GetChild(1).GetChild(0).GetComponent<Image>().color = image;
            }
            else
            {
                image = bodyPartUI.transform.GetChild(4).GetChild(1).GetChild(0).GetComponent<Image>().color;
                image.a = 0.3f;
                bodyPartUI.transform.GetChild(4).GetChild(1).GetChild(0).GetComponent<Image>().color = image;
            }

            if (playerStats.player.buffs.Slowed)
            {
                image = bodyPartUI.transform.GetChild(4).GetChild(2).GetChild(0).GetComponent<Image>().color;
                image.a = 1f;
                bodyPartUI.transform.GetChild(4).GetChild(2).GetChild(0).GetComponent<Image>().color = image;
            }
            else
            {
                image = bodyPartUI.transform.GetChild(4).GetChild(2).GetChild(0).GetComponent<Image>().color;
                image.a = 0.3f;
                bodyPartUI.transform.GetChild(4).GetChild(2).GetChild(0).GetComponent<Image>().color = image;
            }

            if (playerStats.player.buffs.Scared)
            {
                image = bodyPartUI.transform.GetChild(4).GetChild(3).GetChild(0).GetComponent<Image>().color;
                image.a = 1f;
                bodyPartUI.transform.GetChild(4).GetChild(3).GetChild(0).GetComponent<Image>().color = image;
            }
            else
            {
                image = bodyPartUI.transform.GetChild(4).GetChild(3).GetChild(0).GetComponent<Image>().color;
                image.a = 0.3f;
                bodyPartUI.transform.GetChild(4).GetChild(3).GetChild(0).GetComponent<Image>().color = image;
            }

            if (playerStats.player.buffs.Unatunned)
            {
                image = bodyPartUI.transform.GetChild(4).GetChild(4).GetChild(0).GetComponent<Image>().color;
                image.a = 1f;
                bodyPartUI.transform.GetChild(4).GetChild(4).GetChild(0).GetComponent<Image>().color = image;
            }
            else
            {
                image = bodyPartUI.transform.GetChild(4).GetChild(4).GetChild(0).GetComponent<Image>().color;
                image.a = 0.3f;
                bodyPartUI.transform.GetChild(4).GetChild(4).GetChild(0).GetComponent<Image>().color = image;
            }

            if (playerStats.player.buffs.Unbalanced)
            {
                image = bodyPartUI.transform.GetChild(4).GetChild(5).GetChild(0).GetComponent<Image>().color;
                image.a = 1f;
                bodyPartUI.transform.GetChild(4).GetChild(5).GetChild(0).GetComponent<Image>().color = image;
            }
            else
            {
                image = bodyPartUI.transform.GetChild(4).GetChild(5).GetChild(0).GetComponent<Image>().color;
                image.a = 0.3f;
                bodyPartUI.transform.GetChild(4).GetChild(5).GetChild(0).GetComponent<Image>().color = image;
            }
        }
        else
        {
            EnemyStats enemyStats = enemyTarget.GetComponent<EnemyStats>();

            if (enemyStats.foxEnemy.buffs.headDebuff)
            {
                image = bodyPartUI.transform.GetChild(3).GetChild(0).GetChild(0).GetComponent<Image>().color;
                image.a = 1f;
                bodyPartUI.transform.GetChild(3).GetChild(0).GetChild(0).GetComponent<Image>().color = image;
            }
            else
            {
                image = bodyPartUI.transform.GetChild(3).GetChild(0).GetChild(0).GetComponent<Image>().color;
                image.a = 0.3f;
                bodyPartUI.transform.GetChild(3).GetChild(0).GetChild(0).GetComponent<Image>().color = image;
            }


            if (enemyStats.foxEnemy.buffs.torsoDebuff)
            {
                image = bodyPartUI.transform.GetChild(3).GetChild(1).GetChild(0).GetComponent<Image>().color;
                image.a = 1f;
                bodyPartUI.transform.GetChild(3).GetChild(1).GetChild(0).GetComponent<Image>().color = image;
            }
            else
            {
                image = bodyPartUI.transform.GetChild(3).GetChild(1).GetChild(0).GetComponent<Image>().color;
                image.a = 0.3f;
                bodyPartUI.transform.GetChild(3).GetChild(1).GetChild(0).GetComponent<Image>().color = image;
            }

            if (enemyStats.foxEnemy.buffs.armsDebuff)
            {
                image = bodyPartUI.transform.GetChild(3).GetChild(2).GetChild(0).GetComponent<Image>().color;
                image.a = 1f;
                bodyPartUI.transform.GetChild(3).GetChild(2).GetChild(0).GetComponent<Image>().color = image;
            }
            else
            {
                image = bodyPartUI.transform.GetChild(3).GetChild(2).GetChild(0).GetComponent<Image>().color;
                image.a = 0.3f;
                bodyPartUI.transform.GetChild(3).GetChild(2).GetChild(0).GetComponent<Image>().color = image;
            }

            if (enemyStats.foxEnemy.buffs.handsDebuff)
            {
                image = bodyPartUI.transform.GetChild(3).GetChild(3).GetChild(0).GetComponent<Image>().color;
                image.a = 1f;
                bodyPartUI.transform.GetChild(3).GetChild(3).GetChild(0).GetComponent<Image>().color = image;
            }
            else
            {
                image = bodyPartUI.transform.GetChild(3).GetChild(3).GetChild(0).GetComponent<Image>().color;
                image.a = 0.3f;
                bodyPartUI.transform.GetChild(3).GetChild(3).GetChild(0).GetComponent<Image>().color = image;
            }

            if (enemyStats.foxEnemy.buffs.legsDebuff)
            {
                image = bodyPartUI.transform.GetChild(3).GetChild(4).GetChild(0).GetComponent<Image>().color;
                image.a = 1f;
                bodyPartUI.transform.GetChild(3).GetChild(4).GetChild(0).GetComponent<Image>().color = image;
            }
            else
            {
                image = bodyPartUI.transform.GetChild(3).GetChild(4).GetChild(0).GetComponent<Image>().color;
                image.a = 0.3f;
                bodyPartUI.transform.GetChild(3).GetChild(4).GetChild(0).GetComponent<Image>().color = image;
            }

            if (enemyStats.foxEnemy.buffs.feetDebuff)
            {
                image = bodyPartUI.transform.GetChild(3).GetChild(5).GetChild(0).GetComponent<Image>().color;
                image.a = 1f;
                bodyPartUI.transform.GetChild(3).GetChild(5).GetChild(0).GetComponent<Image>().color = image;
            }
            else
            {
                image = bodyPartUI.transform.GetChild(3).GetChild(5).GetChild(0).GetComponent<Image>().color;
                image.a = 0.3f;
                bodyPartUI.transform.GetChild(3).GetChild(5).GetChild(0).GetComponent<Image>().color = image;
            }

            if (enemyStats.foxEnemy.buffs.Paralysed)
            {
                image = bodyPartUI.transform.GetChild(4).GetChild(0).GetChild(0).GetComponent<Image>().color;
                image.a = 1f;
                bodyPartUI.transform.GetChild(4).GetChild(0).GetChild(0).GetComponent<Image>().color = image;
            }
            else
            {
                image = bodyPartUI.transform.GetChild(4).GetChild(0).GetChild(0).GetComponent<Image>().color;
                image.a = 0.3f;
                bodyPartUI.transform.GetChild(4).GetChild(0).GetChild(0).GetComponent<Image>().color = image;
            }

            if (enemyStats.foxEnemy.buffs.Clouded)
            {
                image = bodyPartUI.transform.GetChild(4).GetChild(1).GetChild(0).GetComponent<Image>().color;
                image.a = 1f;
                bodyPartUI.transform.GetChild(4).GetChild(1).GetChild(0).GetComponent<Image>().color = image;
            }
            else
            {
                image = bodyPartUI.transform.GetChild(4).GetChild(1).GetChild(0).GetComponent<Image>().color;
                image.a = 0.3f;
                bodyPartUI.transform.GetChild(4).GetChild(1).GetChild(0).GetComponent<Image>().color = image;
            }

            if (enemyStats.foxEnemy.buffs.Slowed)
            {
                image = bodyPartUI.transform.GetChild(4).GetChild(2).GetChild(0).GetComponent<Image>().color;
                image.a = 1f;
                bodyPartUI.transform.GetChild(4).GetChild(2).GetChild(0).GetComponent<Image>().color = image;
            } else
            {
                image = bodyPartUI.transform.GetChild(4).GetChild(2).GetChild(0).GetComponent<Image>().color;
                image.a = 0.3f;
                bodyPartUI.transform.GetChild(4).GetChild(2).GetChild(0).GetComponent<Image>().color = image;
            }

            if (enemyStats.foxEnemy.buffs.Scared)
            {
                image = bodyPartUI.transform.GetChild(4).GetChild(3).GetChild(0).GetComponent<Image>().color;
                image.a = 1f;
                bodyPartUI.transform.GetChild(4).GetChild(3).GetChild(0).GetComponent<Image>().color = image;
            } else
            {
                image = bodyPartUI.transform.GetChild(4).GetChild(3).GetChild(0).GetComponent<Image>().color;
                image.a = 0.3f;
                bodyPartUI.transform.GetChild(4).GetChild(3).GetChild(0).GetComponent<Image>().color = image;
            }

            if (enemyStats.foxEnemy.buffs.Unatunned)
            {
                image = bodyPartUI.transform.GetChild(4).GetChild(4).GetChild(0).GetComponent<Image>().color;
                image.a = 1f;
                bodyPartUI.transform.GetChild(4).GetChild(4).GetChild(0).GetComponent<Image>().color = image;
            } else
            {
                image = bodyPartUI.transform.GetChild(4).GetChild(4).GetChild(0).GetComponent<Image>().color;
                image.a = 0.3f;
                bodyPartUI.transform.GetChild(4).GetChild(4).GetChild(0).GetComponent<Image>().color = image;
            }

            if (enemyStats.foxEnemy.buffs.Unbalanced)
            {
                image = bodyPartUI.transform.GetChild(4).GetChild(5).GetChild(0).GetComponent<Image>().color;
                image.a = 1f;
                bodyPartUI.transform.GetChild(4).GetChild(5).GetChild(0).GetComponent<Image>().color = image;
            } else
            {
                image = bodyPartUI.transform.GetChild(4).GetChild(5).GetChild(0).GetComponent<Image>().color;
                image.a = 0.3f;
                bodyPartUI.transform.GetChild(4).GetChild(5).GetChild(0).GetComponent<Image>().color = image;
            }
        }
    }

    IEnumerator MyriadOverdrive() 
    {
        MyriadStrikes = true;
        yield return new WaitForSeconds(5);
        OverdriveText.SetActive(false);
        MyriadStrikes = false;
        firstPress = false;
        playerAttacks.MyriadStrikes(enemyTarget,hits);
        cameraController.Play("BattleCamera");
        battleSystem.ChangeState(BattleState.EnemyTurn);
    }

}
