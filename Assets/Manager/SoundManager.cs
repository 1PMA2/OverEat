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
            // �� ���۵ɶ� �ν��Ͻ� �ʱ�ȭ, ���� �Ѿ���� �����Ǳ����� ó��
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            // instance��, GameManager�� �����Ѵٸ� GameObject ���� 
            Destroy(this.gameObject);
        }
    }

    // Public ������Ƽ�� �����ؼ� �ܺο��� private ��������� ���ٸ� �����ϰ� ����
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
