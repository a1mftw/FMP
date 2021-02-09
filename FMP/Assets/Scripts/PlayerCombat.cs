using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerCombat : MonoBehaviour
{

    public GameObject UIControls;
    public BattleSystem battleSystem;

    public enum Stances 
    {
        Physical,
        Magic,
        Alchemy,
        Overdrive

    }

    public Stances stance;

    private bool firstPress = true;

    private Text FirstButton;
    private Text SecondButton;
    private Text ThirdButton;
    private Text FourthButton;

    public void SetUI() 
    {

        UIControls.SetActive(true);

        Debug.Log("Hello");

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
            
            FirstButton.text = "Attack";
            SecondButton.text = "Stance Ability";
            ThirdButton.text = "Change Stance";
            FourthButton.text = "Flee";
            firstPress = true;
        }
    }


    void Flee() 
    {
        battleSystem.tranController.FleeBattle();
    }


}
