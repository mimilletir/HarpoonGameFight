using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance;
    
    [SerializeField] AudioSource[] Sources;
    [SerializeField] AudioClip[] Clips;

    private void Awake()
    {
        if (Instance != null) Destroy(gameObject);
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }


    public void UseSound(int whatSound)
    {
        int index=0;
        if(whatSound > -1 && whatSound < 4)
        {
            index = 0;
        }
        else if(whatSound > 3 && whatSound < 7)
        {
            index = 1;
        }
        else if (whatSound > 6 && whatSound < 10)
        {
            index = 2;
        }
        else
        {
            index = 3;
        }

        if (Sources[index] == null) return;
        Sources[index].clip = Clips[whatSound];
        Sources[index].Play();
    }
}
