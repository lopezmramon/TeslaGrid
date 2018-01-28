using System.Collections;
using UnityEngine;

public class MusicManager : MonoBehaviour 
{

    public AudioClip[] musicTracks;
    public AudioClip[] soundClips;
    int currentSongPlaying;
    public static MusicManager instance;
    public AudioSource musicSource, soundSource;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else Debug.LogWarning("You have more than one Music Manager");


    }
    private void Start()
    {
        PlaySong(Random.Range(0, musicTracks.Length));
    }
    public void NextSong()
    {
        PlaySong(currentSongPlaying++);
    }

    public void PlaySong(int index)
    {
        musicSource.Stop();
        if(index < musicTracks.Length)
        {
            musicSource.PlayOneShot(musicTracks[index]);
            currentSongPlaying = index;
        }
        else
        {
            index = Random.Range(0, musicTracks.Length);
            musicSource.PlayOneShot(musicTracks[index]);
            currentSongPlaying = index;
        }
    }
    
    public void PlaySound(int index)
    {
        if(index < soundClips.Length)
        {
            soundSource.PlayOneShot(soundClips[index]);

        }
    }
}