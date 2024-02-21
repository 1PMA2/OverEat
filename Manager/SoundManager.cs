using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class SoundManager : MonoBehaviour
{

    private static SoundManager instance = null;

    private void Awake()
    {
        if (null == instance)
        {
            // 씬 시작될때 인스턴스 초기화, 씬을 넘어갈때도 유지되기위한 처리
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            // instance가, GameManager가 존재한다면 GameObject 제거 
            Destroy(this.gameObject);
        }
    }

    // Public 프로퍼티로 선언해서 외부에서 private 멤버변수에 접근만 가능하게 구현
    public static SoundManager Instance
    {
        get
        {
            if (null == instance)
            {
                return null;
            }
            return instance;
        }
    }


    public float masterVolumeBGM = 1f;

    [SerializeField] private AudioSource bgm;

    public void PlayBGM()
    {
        bgm.loop = true;
        bgm.volume = masterVolumeBGM;
        bgm.Play();
    }

    public void SetVolume(float vol)
    {
        masterVolumeBGM = vol;
        bgm.volume = masterVolumeBGM;
    }


}
