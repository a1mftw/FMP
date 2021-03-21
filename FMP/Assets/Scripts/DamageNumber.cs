using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DamageNumber : MonoBehaviour
{
    public GameObject pfDamageNumber;
    private const float DISAPPEAR_TIMER_MAX = 1f;
    private float disappearTimer = DISAPPEAR_TIMER_MAX;
    public TextMeshPro textMesh;
    private Color textColor;
    private Vector3 moveVector;
    private static int sortingOrder;
    public GameObject lookattarget;

    public void Create(Vector3 position, int damageAmount, bool isCrit)
    { 
        textMesh.SetText(damageAmount.ToString());
        Instantiate(pfDamageNumber, position + new Vector3(0,5f), Quaternion.identity);

        if (isCrit)
        {
            textMesh.fontSize = 45;
        }
        else
        {
            textMesh.fontSize = 36;
        }

        sortingOrder++;
        textMesh.sortingOrder = sortingOrder;

    }

     public void Update()
     {

        transform.LookAt(lookattarget.transform);
        moveVector = new Vector3(0f, 3f);
        transform.position += moveVector * Time.deltaTime;
        moveVector -= moveVector * Time.deltaTime;
        
        disappearTimer -= Time.deltaTime;
        textColor = GetComponent<TextMeshPro>().color;
        if (disappearTimer<0)
        {
            float disappearSpeed = 3f;
            textColor.a -= disappearSpeed * Time.deltaTime ;
            GetComponent<TextMeshPro>().color = textColor;

            if (textColor.a < 0)
            {
                Destroy(gameObject);
            }
        }

    }
}
