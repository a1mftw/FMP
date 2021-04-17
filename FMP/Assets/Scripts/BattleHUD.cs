using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BattleHUD : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject playerBodyParts;
    public PlayerStats playerStats;
    public Slider hpSlider;
    public Slider mpSlider;
    public Text hpText;
    public Text mpText;


    public void SetHUD()
    {
        
        hpSlider.maxValue = playerStats.player.baseStats.maxHealth;
        mpSlider.maxValue = playerStats.player.baseStats.maxHealth;
        SetHP();
        SetMP();
        BodyTargetHP();

    }
    private void BodyTargetHP()
    {

        //Head
        if (playerStats.player.bodyPartHealth.headHealth <= 0)
        {
            playerBodyParts.transform.GetChild(0).GetComponent<Image>().color = Color.gray;
        }
        else if (playerStats.player.bodyPartHealth.headHealth <= playerStats.player.bodyPartHealth.headMaxHealth / 4)
        {
            playerBodyParts.transform.GetChild(0).GetComponent<Image>().color = Color.red;
        }
        else if (playerStats.player.bodyPartHealth.headHealth <= playerStats.player.bodyPartHealth.headMaxHealth / 2)
        {
            playerBodyParts.transform.GetChild(0).GetComponent<Image>().color = Color.yellow;
        }
        else
        {
            playerBodyParts.transform.GetChild(0).GetComponent<Image>().color = Color.green;
        }


        //Torso
        if (playerStats.player.bodyPartHealth.torsoHealth <= 0)
        {
            playerBodyParts.transform.GetChild(1).GetComponent<Image>().color = Color.gray;
        }
        else if (playerStats.player.bodyPartHealth.torsoHealth <= playerStats.player.bodyPartHealth.torsoMaxHealth / 4)
        {
            playerBodyParts.transform.GetChild(1).GetComponent<Image>().color = Color.red;
        }
        else if (playerStats.player.bodyPartHealth.torsoHealth <= playerStats.player.bodyPartHealth.torsoMaxHealth / 2)
        {
            playerBodyParts.transform.GetChild(1).GetComponent<Image>().color = Color.yellow;
        }
        else
        {
            playerBodyParts.transform.GetChild(1).GetComponent<Image>().color = Color.green;
        }

        //Arms
        if (playerStats.player.bodyPartHealth.armsHealth <= 0)
        {
            playerBodyParts.transform.GetChild(2).GetComponent<Image>().color = Color.gray;
            playerBodyParts.transform.GetChild(2).GetChild(0).GetComponent<Image>().color = Color.gray;
        }
        else if (playerStats.player.bodyPartHealth.armsHealth <= playerStats.player.bodyPartHealth.armsMaxHealth / 4)
        {
            playerBodyParts.transform.GetChild(2).GetComponent<Image>().color = Color.red;
            playerBodyParts.transform.GetChild(2).GetChild(0).GetComponent<Image>().color = Color.red;
        }
        else if (playerStats.player.bodyPartHealth.armsHealth <= playerStats.player.bodyPartHealth.armsMaxHealth / 2)
        {
            playerBodyParts.transform.GetChild(2).GetComponent<Image>().color = Color.yellow;
            playerBodyParts.transform.GetChild(2).GetChild(0).GetComponent<Image>().color = Color.yellow;
        }
        else
        {
            playerBodyParts.transform.GetChild(2).GetComponent<Image>().color = Color.green;
            playerBodyParts.transform.GetChild(2).GetChild(0).GetComponent<Image>().color = Color.green;
        }

        //Hands
        if (playerStats.player.bodyPartHealth.handsHealth <= 0)
        {
            playerBodyParts.transform.GetChild(3).GetComponent<Image>().color = Color.gray;
            playerBodyParts.transform.GetChild(3).GetChild(0).GetComponent<Image>().color = Color.gray;
        }
        else if (playerStats.player.bodyPartHealth.handsHealth <= playerStats.player.bodyPartHealth.handsMaxHealth / 4)
        {
            playerBodyParts.transform.GetChild(3).GetComponent<Image>().color = Color.red;
            playerBodyParts.transform.GetChild(3).GetChild(0).GetComponent<Image>().color = Color.red;
        }
        else if (playerStats.player.bodyPartHealth.handsHealth <= playerStats.player.bodyPartHealth.handsMaxHealth / 2)
        {
            playerBodyParts.transform.GetChild(3).GetComponent<Image>().color = Color.yellow;
            playerBodyParts.transform.GetChild(3).GetChild(0).GetComponent<Image>().color = Color.yellow;
        }
        else
        {
            playerBodyParts.transform.GetChild(3).GetComponent<Image>().color = Color.green;
            playerBodyParts.transform.GetChild(3).GetChild(0).GetComponent<Image>().color = Color.green;
        }

        //Legs
        if (playerStats.player.bodyPartHealth.legsHealth <= 0)
        {
            playerBodyParts.transform.GetChild(4).GetComponent<Image>().color = Color.gray;
            playerBodyParts.transform.GetChild(4).GetChild(0).GetComponent<Image>().color = Color.gray;
        }
        else if (playerStats.player.bodyPartHealth.legsHealth <= playerStats.player.bodyPartHealth.legsMaxHealth / 4)
        {
            playerBodyParts.transform.GetChild(4).GetComponent<Image>().color = Color.red;
            playerBodyParts.transform.GetChild(4).GetChild(0).GetComponent<Image>().color = Color.red;
        }
        else if (playerStats.player.bodyPartHealth.legsHealth <= playerStats.player.bodyPartHealth.legsMaxHealth / 2)
        {
            playerBodyParts.transform.GetChild(4).GetComponent<Image>().color = Color.yellow;
            playerBodyParts.transform.GetChild(4).GetChild(0).GetComponent<Image>().color = Color.yellow;
        }
        else
        {
            playerBodyParts.transform.GetChild(4).GetComponent<Image>().color = Color.green;
            playerBodyParts.transform.GetChild(4).GetChild(0).GetComponent<Image>().color = Color.green;
        }

        //Feet
        if (playerStats.player.bodyPartHealth.feetHealth <= 0)
        {
            playerBodyParts.transform.GetChild(5).GetComponent<Image>().color = Color.gray;
            playerBodyParts.transform.GetChild(5).GetChild(0).GetComponent<Image>().color = Color.gray;
        }
        else if (playerStats.player.bodyPartHealth.feetHealth <= playerStats.player.bodyPartHealth.feetMaxHealth / 4)
        {
            playerBodyParts.transform.GetChild(5).GetComponent<Image>().color = Color.red;
            playerBodyParts.transform.GetChild(5).GetChild(0).GetComponent<Image>().color = Color.red;
        }
        else if (playerStats.player.bodyPartHealth.feetHealth <= playerStats.player.bodyPartHealth.feetMaxHealth / 2)
        {
            playerBodyParts.transform.GetChild(5).GetComponent<Image>().color = Color.yellow;
            playerBodyParts.transform.GetChild(5).GetChild(0).GetComponent<Image>().color = Color.yellow;
        }
        else
        {
            playerBodyParts.transform.GetChild(5).GetComponent<Image>().color = Color.green;
            playerBodyParts.transform.GetChild(5).GetChild(0).GetComponent<Image>().color = Color.green;
        }
    }
    private void SetHP()
    {
        hpSlider.value = playerStats.player.baseStats.currentHealth;
        hpText.text = playerStats.player.baseStats.currentHealth.ToString();
    }
    private void SetMP()
    {
        mpSlider.value = playerStats.player.baseStats.currentMana;
        mpText.text = playerStats.player.baseStats.currentMana.ToString();
    }

    public void BodyPartColor(int currentHealth, int maxHealth, int part, GameObject BodyUI)
    {
        if (currentHealth <= 0)
        {
            BodyUI.transform.GetChild(2).GetChild(part).GetComponent<Image>().color = Color.gray;
        }
        else if (currentHealth <= maxHealth / 4)
        {
            BodyUI.transform.GetChild(2).GetChild(part).GetComponent<Image>().color = Color.red;
        }
        else if (currentHealth <= maxHealth / 2)
        {
            BodyUI.transform.GetChild(2).GetChild(part).GetComponent<Image>().color = Color.yellow;
        }
        else
        {
            BodyUI.transform.GetChild(2).GetChild(part).GetComponent<Image>().color = Color.green;
        }
    }
    public void BuffDebuffColor(bool buff,GameObject BodyUI,int child,int status) 
    {
        var image = new Color();

        if (buff)
        {
            image = BodyUI.transform.GetChild(child).GetChild(status).GetChild(0).GetComponent<Image>().color;
            image.a = 1f;
            BodyUI.transform.GetChild(child).GetChild(status).GetChild(0).GetComponent<Image>().color = image;
        }
        else
        {
            image = BodyUI.transform.GetChild(child).GetChild(status).GetChild(0).GetComponent<Image>().color;
            image.a = 0.3f;
            BodyUI.transform.GetChild(child).GetChild(status).GetChild(0).GetComponent<Image>().color = image;
        }
        

    }


}
