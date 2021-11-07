using System.Collections;
using UnityEngine.Audio;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    static AudioManager _instance;
    public static AudioManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<AudioManager>();
                if (_instance == null)
                {
                    _instance = new GameObject("Audio Manager", typeof(AudioManager)).GetComponent<AudioManager>();
                }
            }
            return _instance;
        }

        private set
        {
            _instance = value;
        }

    }


    AudioSource musicSource;
    AudioSource musicSource1;
    AudioSource sfxSource;

    float musicVolume;
    float sfxVolume;

    bool firstMusicSourceIsPlaying;

    [HideInInspector]
    public float MusicTimeLeft
    {
        get;
        private set;
    }
    [HideInInspector]
    public float MusicFade
    {
        get;
        set;
    }
    [HideInInspector] public bool MusicCanFade { get; private set; } = false;

    [SerializeField] private AudioClip[] sounds;
    [SerializeField] private AudioMixerGroup[] audioGroup;


    void Awake()
    {
        DontDestroyOnLoad(this.gameObject);

        musicSource = this.gameObject.AddComponent<AudioSource>();
        musicSource1 = this.gameObject.AddComponent<AudioSource>();
        sfxSource = this.gameObject.AddComponent<AudioSource>();

        musicSource.outputAudioMixerGroup = audioGroup[0];
        musicSource1.outputAudioMixerGroup = audioGroup[0];
        sfxSource.outputAudioMixerGroup = audioGroup[1];

        musicSource.loop = false;
        musicSource1.loop = false;

        musicVolume = 1f;
        sfxVolume = 1f;
    }
    private void Update()
    {
        MusicTimeLeft -= Time.deltaTime;
        if (MusicTimeLeft <= MusicFade)
            MusicCanFade = true;
        else
        {
            MusicCanFade = false;
        }

    }

    public AudioClip RandomMusic()
    {
        return sounds[Random.Range(0, sounds.Length)];
    }
    /// <summary>
    /// Joue une music Aléatoire
    /// </summary>
    public void PlayMusic()
    {
        //On determine si une source est active;
        AudioSource activeSource = ( firstMusicSourceIsPlaying ) ? musicSource : musicSource1;
        //On choisit une music au hasard
        musicSource.clip = RandomMusic();
        MusicTimeLeft = musicSource.clip.length;
        musicSource.volume = musicVolume;
        musicSource.Play();
    }
    public void PlayMusic(AudioClip musicClip)
    {
        //On determine si une source est active;
        AudioSource activeSource = ( firstMusicSourceIsPlaying ) ? musicSource : musicSource1;

        musicSource.clip = musicClip;
        MusicTimeLeft = musicClip.length;
        musicSource.volume = musicVolume;
        musicSource.Play();
    }

    public void StopMusic()
    {
        musicSource.Stop();
        musicSource1.Stop();
    }

    public void PlayMusicWithFade(AudioClip newClip, float transitionTime = 1f)
    {
        AudioSource activeSource = ( firstMusicSourceIsPlaying ) ? musicSource : musicSource1;

        StartCoroutine(UpdateMusicWithFade(activeSource, newClip, transitionTime));
    }
    private IEnumerator UpdateMusicWithFade(AudioSource activeSource, AudioClip newClip, float transitionTime)
    {
        //Etre sur que la source est inactive. Qui n'est pas en pause
        if (!activeSource.isPlaying)
            activeSource.Play();

        float t = 0.0f;
        //Fade Out
        for (t = 0; t < transitionTime; t += Time.deltaTime)
        {
            activeSource.volume = ( 1 - ( t / transitionTime ) );
            yield return null;
        }
        activeSource.Stop();
        activeSource.clip = newClip;
        activeSource.Play();
        MusicTimeLeft = newClip.length;
        //Fade in 
        for (t = 0; t < transitionTime; t += Time.deltaTime)
        {
            activeSource.volume = ( t / transitionTime );
            yield return null;
        }
    }

    public void PlayMusicWithCrossFade(AudioClip musicClip, float transitionTime = 1f)
    {
        AudioSource activeSource = ( firstMusicSourceIsPlaying ) ? musicSource : musicSource1;
        AudioSource newSource = ( firstMusicSourceIsPlaying ) ? musicSource1 : musicSource;

        //Swap the source 
        firstMusicSourceIsPlaying = !firstMusicSourceIsPlaying;

        //Set the field of the audio source, then start the coroutine to crossfade.
        newSource.clip = musicClip;
        newSource.Play();
        MusicTimeLeft = musicClip.length;
        StartCoroutine(UpdateMusicWithCrossFade(activeSource, newSource, transitionTime));
    }
    private IEnumerator UpdateMusicWithCrossFade(AudioSource originalSource, AudioSource newSource, float transitionTime)
    {
        //Etre sur que la source est active.
        if (!originalSource.isPlaying)
            originalSource.Play();

        float t = 0.0f;
        //Fade Out
        for (t = 0; t < transitionTime; t += Time.deltaTime)
        {
            originalSource.volume = ( 1 - ( t / transitionTime ) );
            newSource.volume = ( t / transitionTime );
            yield return null;
        }
        originalSource.Stop();
    }


    public void PlaySfx(AudioClip sfxClip)
    {
        sfxSource.PlayOneShot(sfxClip);
    }
    public void PlaySfx(AudioClip sfxClip, float volume)
    {
        sfxSource.PlayOneShot(sfxClip, volume);
    }

    public void SetMusicVolume(float volume)
    {
        musicVolume = volume;
    }
    public void SetSFXVolume(float volume)
    {
        sfxVolume = volume;
    }

    public bool MusicIsPlaying()
    {
        if (musicSource.isPlaying || musicSource1.isPlaying)
        {
            return true;
        }
        return false;
    }
}
