using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;

    public float sfxMinimumDistance;
    public AudioSource[] sfx;
    public AudioSource[] bgm;

    public bool playBgm;
    private int bgmIndex;

    private bool canPLaySfX;
    private void Awake()
    {
        if (instance != null)
            Destroy(instance.gameObject);
        else
            instance = this;

        Invoke("AllowSFX", 1f);
    }

    private void Update()
    {
        if (!playBgm)
            StopAllBgm();
        else
        {
            if (!bgm[bgmIndex].isPlaying)
                PlayBGM(bgmIndex);
        }
    }

    public void PlaySFX(int _sfxIndex, Transform _source)
    {
        // if (sfx[_sfxIndex].isPlaying)
        //     return;
        if (canPLaySfX == false)
            return;

        if (_source != null && Vector2.Distance(PlayerManager.instance.player.transform.position, _source.position) > sfxMinimumDistance)
           return;

        if (_sfxIndex < sfx.Length)
        {
            sfx[_sfxIndex].pitch = Random.Range(0.85f, 1.1f);
            sfx[_sfxIndex].Play();
        }
    }
    public void StopSFX(int _Index) => sfx[_Index].Stop();

    public void PlayRandomBGm()
    {
        bgmIndex = Random.Range(0, bgm.Length);
        PlayBGM(bgmIndex);
    }

    public void PlayBGM(int _bgmIndex)
    {
        bgmIndex = _bgmIndex;
        StopAllBgm();

        bgm[bgmIndex].Play();
    }

    private void StopAllBgm()
    {
        for (int i = 0; i < bgm.Length; i++)
        {
            bgm[i].Stop();
        }
    }
    private void AllowSFX() => canPLaySfX = true;   
}
