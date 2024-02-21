using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VideoOption : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        InitUI();
    }

    List<Resolution> resolutions = new List<Resolution>();

    public Dropdown resolutionDropdown;

    public int resolutionNum = 0;

    void InitUI()
    {
        foreach(var resolution in Screen.resolutions)
        {
            if (resolution.width > 1300)
                resolutions.Add(resolution);
        }

        resolutionDropdown.options.Clear();

        int OptionNum = 0;
        foreach (Resolution item in resolutions)
        {
            Dropdown.OptionData option = new Dropdown.OptionData();
            option.text = item.width + "x" + item.height + "(" + item.refreshRate + ")";
            resolutionDropdown.options.Add(option);

            if(item.width == Screen.width && item.height == Screen.height)
            {
                resolutionDropdown.value = OptionNum;
            }
            OptionNum++;
        }

        resolutionDropdown.RefreshShownValue();

        fullBtn.isOn = Screen.fullScreenMode.Equals(FullScreenMode.FullScreenWindow) ? true : false;
    }

    public void DropboxOptionChange(int x)
    {
        resolutionNum = x;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void QuitButton()
    {
        Application.Quit();
    }

    FullScreenMode ScreenMode;

    public Toggle fullBtn;

    public void FullScreenBtn(bool isFull)
    {
        ScreenMode = isFull ? FullScreenMode.FullScreenWindow : FullScreenMode.Windowed;
    }

    public void ApplyButton()
    {
        Screen.SetResolution(resolutions[resolutionNum].width,
            resolutions[resolutionNum].height,
            ScreenMode);

        Application.targetFrameRate = resolutions[resolutionNum].refreshRate;

        Option.Instance.gameObject.SetActive(false);
    }

    public void Middle()
    {
        QualitySettings.SetQualityLevel(3);
    }

    public void Low()
    {
        QualitySettings.SetQualityLevel(1);
    }

    public void High()
    {
        QualitySettings.SetQualityLevel(5);
    }
    public void ChangeVolume(float volume)
    {
        SoundManager.Instance.SetVolume(volume);
        // 사운드의 볼륨을 슬라이더 값으로 설정
    }
}
