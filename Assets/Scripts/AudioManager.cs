using Enums;
using UnityEngine;


[RequireComponent(typeof(AudioSource))]
public class AudioManager : MonoBehaviour
{
    public AudioClip[] clips;
    
    private AudioSource _source;

    private void Awake()
    {
        _source = GetComponent<AudioSource>();
    }

    public void PlayClip(AudioClipType clip, float volume = 1)
    {
        _source.PlayOneShot(clips[(int)clip], volume);
    }
}