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

            if (Input.GetButtonDown("Cancel")) // "Cancel" ��ư�� ���� ��
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
            // �̹� �ɼ� �޴��� Ȱ��ȭ�Ǿ� ������ ��Ȱ��ȭ
            Option.Instance.gameObject.SetActive(false);
            Time.timeScale = 1;
        }
        else
        {
            // �ɼ� �޴��� ��Ȱ��ȭ�Ǿ� ������ Ȱ��ȭ
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
            // JSON ������ ���ڿ��� �о�ɴϴ�.
            string jsonString = File.ReadAllText(filePath);

            // JSON ���ڿ��� Vector2 �迭�� ��ȯ�մϴ�.
            JsonData jsonData = JsonUtility.FromJson<JsonData>(jsonString);
            Vector2[] posArray = jsonData.Pos;

            // Vector2 �迭�� ����� ���� ����մϴ�.
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
