using UnityEngine;

public class SoundManager : MonoBehaviour
{
    [SerializeField] AudioSource[] Sources;
    [SerializeField] AudioClip[] Clips;


    public void UseSound(int whatSound)
    {
        int Index=0;
        if(whatSound > -1 && whatSound < 4)
        {
            Index = 0;
        }
        else if(whatSound > 3 && whatSound < 7)
        {
            Index = 1;
        }
        else if (whatSound > 6 && whatSound < 10)
        {
            Index = 2;
        }
        else
        {
            Index = 3;
        }
        Sources[Index].clip = Clips[whatSound];
        Sources[Index].Play();
    }
}
