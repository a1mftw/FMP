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
        AlchemyCircle,
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
     
        for (int i = 0; i < particleEffects.Count; i++)
        {
            if (particleEffects[i].effect == effect)
            {
                magicGO = Instantiate(particleEffects[i].particle, position, Quaternion.identity);
            }
        }

        if (!magicCircleGO)
        {
            magicCircleGO = Instantiate(particleEffects[5].particle, transform.position, Quaternion.identity);
        }
        



    }

    public GameObject GetMagicParticle() 
    {
        Destroy(magicCircleGO);
        return magicGO;
    }

    public void RemoveSpells() 
    {
        if (magicGO)
        {
            Destroy(magicGO);
        }

        if (magicCircleGO)
        {
            Destroy(magicCircleGO);
        }
        
        
    }

    public void AlchemyMagic() 
    {
        magicCircleGO = Instantiate(particleEffects[6].particle, transform.position, Quaternion.identity);
    }



}
