using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private static GameManager instance = null;

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
    public static GameManager Instance
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

    void Start()
    {

        StartCoroutine(CoESCInput());

        Option.Instance.gameObject.SetActive(false);

        SoundManager.Instance.PlayBGM();
    }

    IEnumerator CoESCInput()
    {
        while(true)
        {

            if (Input.GetButtonDown("Cancel")) // "Cancel" 버튼을 누를 때
            {
                SettingButton();
            }

            yield return null;
        }
    }

    public void SettingButton()
    {
        if (Option.Instance.gameObject.activeSelf)
        {
            // 이미 옵션 메뉴가 활성화되어 있으면 비활성화
            Option.Instance.gameObject.SetActive(false);
            Time.timeScale = 1;
        }
        else
        {
            // 옵션 메뉴가 비활성화되어 있으면 활성화
            Option.Instance.gameObject.SetActive(true);
            Time.timeScale = 0;
        }
    }

    [Serializable]
    public class JsonData
    {
        public Vector2[] Pos;
    }

    void CreateBreadFormJSON(string str)
    {
        string jsonFileName = str;
        string filePath = Path.Combine(Application.streamingAssetsPath, jsonFileName);

        if (File.Exists(filePath))
        {
            // JSON 파일을 문자열로 읽어옵니다.
            string jsonString = File.ReadAllText(filePath);

            // JSON 문자열을 Vector2 배열로 변환합니다.
            JsonData jsonData = JsonUtility.FromJson<JsonData>(jsonString);
            Vector2[] posArray = jsonData.Pos;

            // Vector2 배열에 저장된 값을 출력합니다.
            if (posArray != null && posArray.Length > 0)
            {
                for (int i = 0; i < posArray.Length; i++)
                    ObjectManager.Instance.CreateBread(posArray[i].x, posArray[i].y, i);
            }
            else
                Debug.LogError("Failed to load JSON file: " + jsonFileName);
        }
        else
            Debug.LogError("Failed to load JSON file: " + jsonFileName);
    }

    public void Retry()
    {
        ObjectManager.Instance.RemoveHotdog();

        ObjectManager.Instance.CreateHotdog();

        ObjectManager.Instance.RemoveFirstBread();

        ObjectManager.Instance.CreateFirstBread();
        //ObjectManager.Instance.RemoveBread();

        //CreateBreadFormJSON("BreadPosition.json");
    }

    public void GoMain()
    {
       ObjectManager.Instance.RemoveHotdog();
       ObjectManager.Instance.RemoveBread();
       SceneManager.LoadScene("Main");
    }

    public void CreateBreadStage()
    {
        CreateBreadFormJSON("BreadPosition.json");
    }


    string strRecord = "";
    public void Record(float fTime)
    {
        int hours = Mathf.FloorToInt(fTime / 3600);
        int minutes = Mathf.FloorToInt((fTime / 60) % 60);
        int seconds = Mathf.FloorToInt(fTime % 60);
        int milliseconds = Mathf.FloorToInt((fTime * 1000) % 100);

        strRecord = string.Format(" {0:00}:{1:00}:{2:00}:{3:00}", hours, minutes, seconds, milliseconds);
    }

    public string RecTime() { return strRecord; }

}
