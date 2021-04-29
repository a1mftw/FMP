using UnityEngine;

public class SFX_Manager_HR : MonoBehaviour
{
    public enum SoundEffectNames
    {
        SWING,
        HIT,
        FIRECAST,
        FIREFLING,
        FIREHIT,
        AIRCAST,
        AIRHIT,
        WATERCAST,
        WATERHIT,
        ZAPCAST,
        ZAPHIT,
        BIOCAST,
        BIOHIT,
        FOXDIE,
        HITONARMOR,
        UIPOSITIVE,
        UINEGATIVE,
        BATTLEPOSITIVE,
        

    }

    public static SFX_Manager_HR instance;
    public SoundEffect_HR[] soundEffects;
    Pooler_HR pooler;
    AudioSource soundSource;

    //Get the Pooler instance
    void Start()
    {
        instance = this;
        pooler = Pooler_HR.instance;
    }

    //Scripts will call this function and pass the name, the position and optionally the amount of time to play
    public void PlaySFX(SoundEffectNames clipName, Vector3 position, double seconds = 0)
    {
        //Calls the pooler script to dequeue a pool object 
        soundSource = pooler.SpawnFromPool(Pooler_HR.Tags.SFX, position, soundEffects[(int)clipName].clip).GetComponent<AudioSource>();

        //If time is given
        if (seconds != 0)
            soundSource.PlayScheduled(seconds);
        else
            soundSource.Play();

        StartCoroutine(pooler.ReturnToPool(Pooler_HR.Tags.SFX, soundSource.gameObject, soundSource.clip.length));
    }
    [System.Serializable]
    public class SoundEffect_HR
    {
        public SoundEffectNames name;
        public AudioClip clip;
    }

}