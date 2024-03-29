using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [SerializeField] private AudioSource music;
    [SerializeField] private AudioSource narrator;
    [SerializeField] private AudioSource spell1;
    [SerializeField] private AudioSource spell2;
    [SerializeField] private AudioSource explosion;
    [SerializeField] private AudioSource playerDamage;
    [SerializeField] private AudioSource playerDeath;

    public void PlayNarratorAudioClip(AudioClip clip)
    {
        narrator.clip = clip;
        narrator.Play();
    }
    public void PlaySpellAudioClip(int spellNumber)
    {
        if (spellNumber == 1)
            PlaySpellAudioClip();
        else if (spellNumber == 2)
            PlaySpel2AudioClip();

    }
    private void PlaySpellAudioClip()
    { spell1.Play(); }
    private void PlaySpel2AudioClip()
    { spell2.Play(); }
    public void PlayExplosionAudioClip()
    { explosion.Play(); }
    public void PlayPlayerDamageAudioClip()
    { playerDamage.Play(); }
    public void PlayPlayerDeathAudioClip()
    { playerDeath.Play(); }

}
