using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Cinemachine;

public class PlayerCombat : MonoBehaviour
{

    public enum UIStates 
    {
        EnemyTurn,
        Action,
        PhysicalNormal,
        PhysicalAttacks,
        PhysicalAbilities,
        MagicNormal,
        MagicAttacks,
        MagicAbilities,
        AlchemyNormal,
        AlchemyAttacks,
        AlchemyAbilities,
        OverdriveNormal,
        OverdriveAttacks,
        OverdriveAbilities
    }
    public enum Stances
    {
        Physical,
        Magic,
        Alchemy,
        Overdrive

    }

    public UIStates uiState = UIStates.PhysicalNormal;


    public Material outlineMaterial;
    private List<Material> matArray;
    private Material[] originalMaterials;
    private SkinnedMeshRenderer targetRenderer;
    private SkinnedMeshRenderer currRenderer;


    public ListPositionCtrl spellListControl;

    public GameObject magicSpellList;
    public GameObject UIControls;
    public GameObject PlayerUI;
    public BattleSystem battleSystem;
    public PlayerAttacks playerAttacks;
    public GameObject spellPlaceHolder;
    public BattleHUD battleHUD;

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

    private bool targeting = false;
    private bool playerTargeting = false;
    private bool changeStanceActive = false;
    private bool spellChoiceActive = false;
    private bool MyriadStrikes = false;
    private bool alchemyTargeting = false;

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
    private bool cancel = false;

    public CinemachineVirtualCamera targetingCamera;
    public CinemachineVirtualCamera physicalCamera;
    

    private void Update()
    {

        cancel = false;

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

        if (targeting)
        {
            TargetShader();

            //Change between enemy
            if (Input.GetButtonDown("Right"))
            {
                battleSystem.NextTarget();
                targetingCamera.LookAt = battleSystem.enemyTarget.transform;
            }

            if (Input.GetButtonDown("Left"))
            {

                battleSystem.PreviousTarget();
                targetingCamera.LookAt = battleSystem.enemyTarget.transform;
            }

            if (stance == Stances.Physical)
            {
                target = (PlayerAttacks.Target)bodyPartCount;
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
            }
            
            if (Input.GetButtonDown("Submit"))
            {
                switch (stance)
                {
                    case Stances.Physical:
                        switch (attack)
                        {
                            case PlayerAttacks.Attacks.Bludgeoning:
                                playerAttacks.Blunt(target, battleSystem.enemyTarget);
                                break;
                            case PlayerAttacks.Attacks.Piercing:
                                playerAttacks.Pierce(target, battleSystem.enemyTarget);
                                break;
                            case PlayerAttacks.Attacks.Slashing:
                                playerAttacks.Slash(target, battleSystem.enemyTarget);
                                break;
                            default:
                                break;
                        }
                        bodyPartUI.SetActive(false);
                        break;

                    case Stances.Magic:
                        switch (spell)
                        {
                            case PlayerAttacks.Spells.Fire:
                                playerAttacks.FireDamage(animationManager.GetMagicParticle(), battleSystem.enemyTarget);
                                Debug.Log("Fire damage");
                                break;
                            case PlayerAttacks.Spells.Water:
                                playerAttacks.WaterDamage(animationManager.GetMagicParticle(), battleSystem.enemyTarget);
                                Debug.Log("Water damage");
                                break;
                            case PlayerAttacks.Spells.Air:
                                playerAttacks.AirDamage(animationManager.GetMagicParticle(), battleSystem.enemyTarget);
                                Debug.Log("Air damage");
                                break;
                            case PlayerAttacks.Spells.Earth:
                                playerAttacks.EarthDamage(animationManager.GetMagicParticle(), battleSystem.enemyTarget);
                                Debug.Log("Earth damage");
                                break;
                            case PlayerAttacks.Spells.Lightning:
                                playerAttacks.LightningDamage(animationManager.GetMagicParticle(), battleSystem.enemyTarget);
                                Debug.Log("Lightning damage");
                                break;
                        }

                        cameraController.Play("BattleCamera");

                        break;
                    case Stances.Alchemy:
                        playerAttacks.Alchemy(target);
                        bodyPartUI.SetActive(false);
                        break;

                    case Stances.Overdrive:
                        hits = 0;
                        OverdriveText.SetActive(true);
                        StartCoroutine("MyriadOverdrive");
                        break;
                    default:
                        break;
                }
                
                targeting = false;
                bodyPartCount = 0;
            }

            if (Input.GetButtonDown("Cancel")) 
            {
                TargetShader();
                targeting = false;
                bodyPartCount = 0;
                target = PlayerAttacks.Target.Head;

                switch (stance)
                {
                    case Stances.Physical:
                        bodyPartUI.SetActive(false);
                        uiState = UIStates.PhysicalAttacks;
                        cameraController.Play("BattleCamera");
                        break;
                    case Stances.Magic:
                        cancel = true;
                        spellChoiceActive = true;
                        cameraController.Play("MagicState");
                        magicSpellList.SetActive(true);
                        spellHighlight.Select();
                        animationManager.MagicCastParticle((AnimationManager.Effects)spellListControl.GetCenteredContentID(), spellPlaceHolder.transform.position);
                        break;
                    case Stances.Alchemy:
                        cameraController.Play("BattleCamera");
                        break;
                    case Stances.Overdrive:
                        cameraController.Play("BattleCamera");
                        break;
                    default:
                        break;
                }
             
            }


        }

        if (alchemyTargeting)
        {
            target = (PlayerAttacks.Target)bodyPartCount;
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

            if (Input.GetButtonDown("Submit"))
            {
               
                playerAttacks.Alchemy(target);
                bodyPartUI.SetActive(false);
                alchemyTargeting = false;
                bodyPartCount = 0;
                cameraController.Play("BattleCamera");
                animationManager.RemoveSpells();
            }

              

            if (Input.GetButtonDown("Cancel"))
            {
                TargetShader();
                alchemyTargeting = false;
                bodyPartCount = 0;
                target = PlayerAttacks.Target.Head;
                cameraController.Play("BattleCamera");
                animationManager.RemoveSpells();


            }

        }

        if (spellChoiceActive)
        {
            if (Input.GetButtonDown("Up"))
            {

                spellListControl.MoveOneUnitUp();
                if (spellListControl.GetCenteredContentID() + 1 > 4)
                {
                    animationManager.MagicCastParticle(AnimationManager.Effects.Fire, spellPlaceHolder.transform.position);
                }
                else
                {
                    animationManager.MagicCastParticle((AnimationManager.Effects)spellListControl.GetCenteredContentID() + 1, spellPlaceHolder.transform.position);
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

            if (!cancel)
            {
                if (Input.GetButtonDown("Cancel"))
                {
                    spellChoiceActive = false;
                    animationManager.RemoveSpells();

                    magicSpellList.SetActive(false);
                    cameraController.Play("BattleCamera");
                }
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
        

        ChangeStance(stance);
    }
    public void FirstButtonAction() 
    {
        switch (uiState)
        {
            case UIStates.PhysicalNormal:
                FirstButton.text = "Bludgeoning";
                SecondButton.text = "Piercing";
                ThirdButton.text = "Slashing";
                FourthButton.text = "Back";
                uiState = UIStates.PhysicalAttacks;
                break;

            case UIStates.PhysicalAttacks:
                    uiState = UIStates.Action;
                    attack = PlayerAttacks.Attacks.Bludgeoning;
                    partHighlight.Select();
                    targeting = true;
                    //Change camera to enemy
                    targetingCamera.LookAt = battleSystem.enemyTarget.transform;
                    cameraController.Play("TargetingEnemy");
                    //Change color of parts if they are damaged
                    EnemyColorTargetSystem();
                    //Show buffs/debuffs of selected enemy
                    BuffDebuffs();
                    //Show body part select
                    bodyPartUI.SetActive(true);
                break;

            case UIStates.PhysicalAbilities:
                break;

            case UIStates.MagicNormal:
                uiState = UIStates.MagicAttacks;
                cameraController.Play("MagicState");
                magicSpellList.SetActive(true);
                spellHighlight.Select();
                spellChoiceActive = true;
                animationManager.MagicCastParticle((AnimationManager.Effects)spellListControl.GetCenteredContentID(), spellPlaceHolder.transform.position);
                break;

            case UIStates.MagicAttacks:
                break;

            case UIStates.MagicAbilities:
                break;

            case UIStates.AlchemyNormal:
                cameraController.Play("AlchemyState");
                partHighlight.Select();
                alchemyTargeting = true;
                //Change color of parts if they are damaged
                PlayerColorTargetSystem();
                //Show player buffs/Debuffs
                BuffDebuffs(true);
                animationManager.AlchemyMagic();
                //Show body part select
                bodyPartUI.SetActive(true);
                break;

            case UIStates.AlchemyAttacks:
                break;
            case UIStates.AlchemyAbilities:
                break;
            case UIStates.OverdriveNormal:
                FirstButton.text = "Myriad Strikes";
                SecondButton.text = "";
                ThirdButton.text = "";
                FourthButton.text = "Back";
                uiState = UIStates.OverdriveAttacks;
                break;

            case UIStates.OverdriveAttacks:
                    uiState = UIStates.Action;
                    targeting = true;
                    //Change camera to enemy
                    targetingCamera.LookAt = battleSystem.enemyTarget.transform;
                    cameraController.Play("TargetingEnemy");
                break;

            case UIStates.OverdriveAbilities:
                break;

            default:
                break;
        }

    }
    public void SecondButtonAction() 
    {
        switch (uiState)
        {
            case UIStates.PhysicalNormal:
                FirstButton.text = "War Cry";
                SecondButton.text = "Piercing Gaze";
                ThirdButton.text = "Meditate";
                FourthButton.text = "Back";
                uiState = UIStates.PhysicalAbilities;
                break;

            case UIStates.PhysicalAttacks:
                uiState = UIStates.Action;
                attack = PlayerAttacks.Attacks.Piercing;
                partHighlight.Select();
                targeting = true;
                //Change camera to enemy
                targetingCamera.LookAt = battleSystem.enemyTarget.transform;
                cameraController.Play("TargetingEnemy");
                //Change color of parts if they are damaged
                EnemyColorTargetSystem();
                //Turn the buff debuff UI on
                BuffDebuffs();
                //Show body part select
                bodyPartUI.SetActive(true);
                break;

            case UIStates.PhysicalAbilities:
                break;

            case UIStates.MagicNormal:
                break;

            case UIStates.MagicAttacks:
                break;

            case UIStates.MagicAbilities:
                break;

            case UIStates.AlchemyNormal:
                break;

            case UIStates.AlchemyAttacks:
                break;

            case UIStates.AlchemyAbilities:
                break;

            case UIStates.OverdriveNormal:
                break;

            case UIStates.OverdriveAttacks:
                break;

            case UIStates.OverdriveAbilities:
                break;

            default:
                break;
        }
    }
    public void ThirdButtonAction() 
    {

        switch (uiState)
        {
            case UIStates.PhysicalNormal:
                changeStanceActive = true;
                ChangeStanceUI.SetActive(true);
                break;

            case UIStates.PhysicalAttacks:
                uiState = UIStates.Action;
                attack = PlayerAttacks.Attacks.Slashing;
                partHighlight.Select();
                targeting = true;
                targetingCamera.LookAt = battleSystem.enemyTarget.transform;
                cameraController.Play("TargetingEnemy");
                //Change color of parts if they are damaged
                EnemyColorTargetSystem();
                BuffDebuffs();
                //Show body part select
                bodyPartUI.SetActive(true);
                break;

            case UIStates.PhysicalAbilities:
                break;

            case UIStates.MagicNormal:
                changeStanceActive = true;
                ChangeStanceUI.SetActive(true);
                break;

            case UIStates.MagicAttacks:
                break;

            case UIStates.MagicAbilities:
                break;

            case UIStates.AlchemyNormal:
                changeStanceActive = true;
                ChangeStanceUI.SetActive(true);
                break;

            case UIStates.AlchemyAttacks:
                break;

            case UIStates.AlchemyAbilities:
                break;

            case UIStates.OverdriveNormal:
                changeStanceActive = true;
                ChangeStanceUI.SetActive(true);
                break;

            case UIStates.OverdriveAttacks:
                break;

            case UIStates.OverdriveAbilities:
                break;

            default:
                break;
        }

    }
    public void FourthButtonAction() 
    {
        switch (uiState)
        {
            case UIStates.PhysicalNormal:
                Flee();
                break;

            case UIStates.PhysicalAttacks:
                uiState = UIStates.PhysicalNormal;
                FirstButton.text = "Attack";
                SecondButton.text = "Abilities";
                ThirdButton.text = "Change Stance";
                FourthButton.text = "Flee";
                break;

            case UIStates.PhysicalAbilities:
                uiState = UIStates.PhysicalNormal;
                FirstButton.text = "Attack";
                SecondButton.text = "Abilities";
                ThirdButton.text = "Change Stance";
                FourthButton.text = "Flee";
                break;

            case UIStates.MagicNormal:
                Flee();
                break;

            case UIStates.MagicAttacks:
                uiState = UIStates.PhysicalNormal;
                FirstButton.text = "Attack";
                SecondButton.text = "Abilities";
                ThirdButton.text = "Change Stance";
                FourthButton.text = "Flee";
                break;

            case UIStates.MagicAbilities:
                uiState = UIStates.PhysicalNormal;
                FirstButton.text = "Attack";
                SecondButton.text = "Abilities";
                ThirdButton.text = "Change Stance";
                FourthButton.text = "Flee";
                break;

            case UIStates.AlchemyNormal:
                Flee();
                break;

            case UIStates.AlchemyAttacks:
                uiState = UIStates.PhysicalNormal;
                FirstButton.text = "Attack";
                SecondButton.text = "Abilities";
                ThirdButton.text = "Change Stance";
                FourthButton.text = "Flee";
                break;

            case UIStates.AlchemyAbilities:
                uiState = UIStates.PhysicalNormal;
                FirstButton.text = "Attack";
                SecondButton.text = "Abilities";
                ThirdButton.text = "Change Stance";
                FourthButton.text = "Flee";
                break;

            case UIStates.OverdriveNormal:
                Flee();
                break;

            case UIStates.OverdriveAttacks:
                uiState = UIStates.PhysicalNormal;
                FirstButton.text = "Attack";
                SecondButton.text = "Abilities";
                ThirdButton.text = "Change Stance";
                FourthButton.text = "Flee";
                break;

            case UIStates.OverdriveAbilities:
                uiState = UIStates.PhysicalNormal;
                FirstButton.text = "Attack";
                SecondButton.text = "Abilities";
                ThirdButton.text = "Change Stance";
                FourthButton.text = "Flee";
                break;

            default:
                break;
        } 
    }
    void Flee() 
    {
        battleSystem.tranController.FleeBattle();
        BattleOver = false;
    }
    void EnemyColorTargetSystem() 
    {
        EnemyStats enemyStats = battleSystem.enemyTarget.GetComponent<EnemyStats>();

        bodyPartUI.transform.GetChild(1).GetComponent<Slider>().maxValue = enemyStats.enemy.baseStats.maxHealth;
        bodyPartUI.transform.GetChild(1).GetComponent<Slider>().value = enemyStats.enemy.baseStats.currentHealth;

        battleHUD.BodyPartColor(enemyStats.enemy.bodyPartHealth.headHealth, enemyStats.enemy.bodyPartHealth.headMaxHealth, 0, bodyPartUI);
        battleHUD.BodyPartColor(enemyStats.enemy.bodyPartHealth.torsoHealth, enemyStats.enemy.bodyPartHealth.torsoMaxHealth, 1, bodyPartUI);
        battleHUD.BodyPartColor(enemyStats.enemy.bodyPartHealth.armsHealth, enemyStats.enemy.bodyPartHealth.armsMaxHealth, 2, bodyPartUI);
        battleHUD.BodyPartColor(enemyStats.enemy.bodyPartHealth.handsHealth, enemyStats.enemy.bodyPartHealth.handsMaxHealth, 3, bodyPartUI);
        battleHUD.BodyPartColor(enemyStats.enemy.bodyPartHealth.legsHealth, enemyStats.enemy.bodyPartHealth.legsMaxHealth, 4, bodyPartUI);
        battleHUD.BodyPartColor(enemyStats.enemy.bodyPartHealth.feetHealth, enemyStats.enemy.bodyPartHealth.feetMaxHealth, 5, bodyPartUI);

    }
    void PlayerColorTargetSystem() 
    {
        PlayerStats playerStats = GameObject.Find("Player").GetComponent<PlayerStats>();

        bodyPartUI.transform.GetChild(1).GetComponent<Slider>().maxValue = playerStats.player.baseStats.maxHealth;
        bodyPartUI.transform.GetChild(1).GetComponent<Slider>().value = playerStats.player.baseStats.currentHealth;

        battleHUD.BodyPartColor(playerStats.player.bodyPartHealth.headHealth, playerStats.player.bodyPartHealth.headMaxHealth, 0, bodyPartUI);
        battleHUD.BodyPartColor(playerStats.player.bodyPartHealth.torsoHealth, playerStats.player.bodyPartHealth.torsoMaxHealth, 1, bodyPartUI);
        battleHUD.BodyPartColor(playerStats.player.bodyPartHealth.armsHealth, playerStats.player.bodyPartHealth.armsMaxHealth, 2, bodyPartUI);
        battleHUD.BodyPartColor(playerStats.player.bodyPartHealth.handsHealth, playerStats.player.bodyPartHealth.handsMaxHealth, 3, bodyPartUI);
        battleHUD.BodyPartColor(playerStats.player.bodyPartHealth.legsHealth, playerStats.player.bodyPartHealth.legsMaxHealth, 4, bodyPartUI);
        battleHUD.BodyPartColor(playerStats.player.bodyPartHealth.feetHealth, playerStats.player.bodyPartHealth.feetMaxHealth, 5, bodyPartUI);
    }
    public void ChooseSpellTarget(int spellChosen) 
    {
        spell = (PlayerAttacks.Spells)spellChosen;
        magicSpellList.SetActive(false);
        spellChoiceActive = false;
        targeting = true;
        targetingCamera.LookAt = battleSystem.enemyTarget.transform;
        cameraController.Play("TargetingEnemy");

    }
    private void BuffDebuffs(bool player = false)
    {
        PlayerStats playerStats = GetComponent<PlayerStats>();
        EnemyStats enemyStats = battleSystem.enemyTarget.GetComponent<EnemyStats>();

        battleHUD.BuffDebuffColor(playerStats.player.buffs.headDebuff, bodyPartUI, 3, 0);
        battleHUD.BuffDebuffColor(playerStats.player.buffs.torsoDebuff, bodyPartUI, 3, 1);
        battleHUD.BuffDebuffColor(playerStats.player.buffs.armsDebuff, bodyPartUI, 3, 2);
        battleHUD.BuffDebuffColor(playerStats.player.buffs.handsDebuff, bodyPartUI, 3, 3);
        battleHUD.BuffDebuffColor(playerStats.player.buffs.legsDebuff, bodyPartUI, 3, 4);
        battleHUD.BuffDebuffColor(playerStats.player.buffs.feetDebuff, bodyPartUI, 3, 5);
        battleHUD.BuffDebuffColor(enemyStats.enemy.buffs.headDebuff, bodyPartUI, 3, 0);
        battleHUD.BuffDebuffColor(enemyStats.enemy.buffs.headDebuff, bodyPartUI, 3, 1);
        battleHUD.BuffDebuffColor(enemyStats.enemy.buffs.headDebuff, bodyPartUI, 3, 2);
        battleHUD.BuffDebuffColor(enemyStats.enemy.buffs.headDebuff, bodyPartUI, 3, 3);
        battleHUD.BuffDebuffColor(enemyStats.enemy.buffs.headDebuff, bodyPartUI, 3, 4);
        battleHUD.BuffDebuffColor(enemyStats.enemy.buffs.headDebuff, bodyPartUI, 3, 5);

        battleHUD.BuffDebuffColor(playerStats.player.buffs.Paralysed, bodyPartUI, 4, 0);
        battleHUD.BuffDebuffColor(playerStats.player.buffs.Clouded, bodyPartUI, 4, 1);
        battleHUD.BuffDebuffColor(playerStats.player.buffs.Slowed, bodyPartUI, 4, 2);
        battleHUD.BuffDebuffColor(playerStats.player.buffs.Scared, bodyPartUI, 4, 3);
        battleHUD.BuffDebuffColor(playerStats.player.buffs.Unatunned, bodyPartUI, 4, 4);
        battleHUD.BuffDebuffColor(playerStats.player.buffs.Unbalanced, bodyPartUI, 4, 5);
        battleHUD.BuffDebuffColor(enemyStats.enemy.buffs.Paralysed, bodyPartUI, 4, 0);
        battleHUD.BuffDebuffColor(enemyStats.enemy.buffs.Clouded, bodyPartUI, 4, 1);
        battleHUD.BuffDebuffColor(enemyStats.enemy.buffs.Slowed, bodyPartUI, 4, 2);
        battleHUD.BuffDebuffColor(enemyStats.enemy.buffs.Scared, bodyPartUI, 4, 3);
        battleHUD.BuffDebuffColor(enemyStats.enemy.buffs.Unatunned, bodyPartUI, 4, 4);
        battleHUD.BuffDebuffColor(enemyStats.enemy.buffs.Unbalanced, bodyPartUI, 4, 5);
        
    }
    IEnumerator MyriadOverdrive() 
    {
        MyriadStrikes = true;
        yield return new WaitForSeconds(5);
        OverdriveText.SetActive(false);
        MyriadStrikes = false;
        playerAttacks.MyriadStrikes(battleSystem.enemyTarget, hits);
        cameraController.Play("BattleCamera");
        battleSystem.NextTurn();
    }
    void TargetShader() 
    {
        if (!currRenderer )
        {
            currRenderer = battleSystem.enemyTarget.transform.GetChild(0).GetComponent<SkinnedMeshRenderer>();
            originalMaterials = currRenderer.materials;

        }

        if (currRenderer != battleSystem.enemyTarget.transform.GetChild(0).GetComponent<SkinnedMeshRenderer>())
        {
            currRenderer.materials = originalMaterials;
            currRenderer = battleSystem.enemyTarget.transform.GetChild(0).GetComponent<SkinnedMeshRenderer>();
            originalMaterials = currRenderer.materials;
        }
       else
        {
            matArray = new List<Material>(currRenderer.materials);
            matArray.Add(outlineMaterial);

            if (currRenderer.materials != matArray.ToArray() )
            {
                currRenderer.materials = matArray.ToArray();
                targetRenderer = currRenderer;
            }
        }
    
    }
    void ChangeStance(Stances newStance)
    {
        stance = newStance;

        switch (stance)
        {
            case Stances.Physical:
                PlayerUI.transform.GetChild(2).GetChild(1).GetComponent<Image>().sprite = physicalImage;
                uiState = UIStates.PhysicalNormal;
                break;
            case Stances.Magic:
                PlayerUI.transform.GetChild(2).GetChild(1).GetComponent<Image>().sprite = magicImage;
                uiState = UIStates.MagicNormal;
                break;
            case Stances.Alchemy:
                PlayerUI.transform.GetChild(2).GetChild(1).GetComponent<Image>().sprite = alchemyImage;
                uiState = UIStates.AlchemyNormal;
                break;
            case Stances.Overdrive:
                PlayerUI.transform.GetChild(2).GetChild(1).GetComponent<Image>().sprite = overdriveImage;
                uiState = UIStates.OverdriveNormal;
                break;
        }

        changeStanceActive = false;
        
    }

}
