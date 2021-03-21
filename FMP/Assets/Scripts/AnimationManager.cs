using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationManager : MonoBehaviour
{

    public enum Effects
    {
        Fire,
        Water,
        Air,
        Earth,
        Electric,
        MagicCircle,
    }

    [System.Serializable]
    public struct ParticleEffects
    {
        public Effects effect;
        public GameObject particle;
    }

    public List<ParticleEffects> particleEffects = new List<ParticleEffects>(); 

    GameObject magicGO;
    GameObject magicCircleGO;

    public void MagicCastParticle(Effects effect,Vector3 position) 
    {

        Destroy(magicGO);
        Destroy(magicCircleGO);
        for (int i = 0; i < particleEffects.Count; i++)
        {
            if (particleEffects[i].effect == effect)
            {
                magicGO = Instantiate(particleEffects[i].particle, position, Quaternion.identity);
            }
        }

        magicCircleGO = Instantiate(particleEffects[5].particle, transform.position, Quaternion.identity);



    }

    public GameObject GetMagicParticle() 
    {
        Destroy(magicCircleGO);
        return magicGO;
    }

    public void RemoveSpells() 
    {
        Destroy(magicGO);
        Destroy(magicCircleGO);
    }



}
