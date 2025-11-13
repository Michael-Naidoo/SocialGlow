using UnityEngine;

public class ChoiceAudio : MonoBehaviour
{
    private AudioSource audio => GetComponentInParent<AudioSource>();
    [SerializeField] private AudioClip audioClip;

    public void PlaySound()
    {
        audio.resource = audioClip;
        audio.Play();
    }
}
