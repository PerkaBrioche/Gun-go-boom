using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    [SerializeField] private Dictionary<string, AudioClip> allClips;
    private AudioSource _audioSource;
    
    // UTILISER CETTE FONCTION POUR JOUER UN SHAKE !!! //
    public void PlaySound(string KeyToFind)
    {
        string key = KeyToFind.ToLower();
        _audioSource.PlayOneShot(allClips[key]);
    }
}