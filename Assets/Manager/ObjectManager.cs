using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;
using Unity.VisualScripting;
using UnityEngine;

public class ObjectManager : MonoBehaviour
{

    private static ObjectManager instance = null;

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
    public static ObjectManager Instance
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


    [SerializeField]
    GameObject breadPrefeb;

    GameObject breadInstance;

    List<GameObject> breadPrefebList = new List<GameObject>();

    [SerializeField]
    GameObject hotdog;

    GameObject hotdogInstance;

    Vector3 vStrartPos = new Vector3(0, 0, 0);

    private Dictionary<int, GameObject> objects = new Dictionary<int, GameObject>();



    private void Start()
    {

    }

    static int Convert_ToHash(string strTemp)
    {
        byte[] inputBytes = Encoding.Unicode.GetBytes(strTemp);
        SHA256 sha256 = SHA256.Create();
        byte[] hashBytes = sha256.ComputeHash(inputBytes);

        return BitConverter.ToInt32(hashBytes);
    }

    public void AddObject(GameObject gameObject)
    {

        int hash = Convert_ToHash(gameObject.name);
        objects[hash] = gameObject;
        //objects.Add(hash, obj);
    }

    public GameObject GetObject(string id)
    {
        int hash = Convert_ToHash(id);
        if (objects.ContainsKey(hash))
        {
            return objects[hash];
        }
        return null;
    }

    public GameObject GetObject(GameObject gameObject)
    {
        int hash = Convert_ToHash(gameObject.name);
        if (objects.ContainsKey(hash))
        {
            return objects[hash];
        }
        return null;
    }


    public bool Equals(GameObject gameObject, string strName)
    {
        int hash1 = Convert_ToHash(gameObject.name);
        int hash2 = Convert_ToHash(strName);

        return hash1 == hash2;
    }

    public GameObject GetSausage()
    {
        if (hotdogInstance)
            return hotdogInstance;

        return null;
    }

    public Vector2 GetPos()
    {
        if (hotdogInstance)
        {
            Vector2 vPos = hotdogInstance.transform.position;

            return vPos;
        }

        return Vector2.zero;
    }

    public Quaternion GetRotation()
    {
        if (hotdogInstance)
        {
            Quaternion qTrans = hotdogInstance.transform.rotation;

            return qTrans;
        }

        return Quaternion.identity;
    }

    public void CreateHotdog(int x = 0, int y = 0)
    {
        hotdogInstance = Instantiate(hotdog);
        AddObject(hotdogInstance);
        hotdogInstance.transform.localPosition = new Vector2(x, y);
    }

    public void Test()
    {
        hotdogInstance = Instantiate(hotdog);
        AddObject(hotdogInstance);
        hotdogInstance.transform.localPosition = new Vector3(50, 233, 0);
    }

    public void CreateBread(float fX, float fY, int iNum)
    {
        GameObject newObject = Instantiate(breadPrefeb);
        newObject.name = "Bread(Clone)" + iNum;

        breadPrefebList.Add( newObject );

        AddObject(breadPrefebList[iNum]);

        newObject.transform.localPosition = new Vector3(fX, fY, -0.5f);
    }

    public void RemoveObject(GameObject gameObject)
    {
        int hash = Convert_ToHash(gameObject.name);
        if (objects.ContainsKey(hash))
        {
            objects.Remove(hash);
            Destroy(gameObject);
        }

    
    }

    public void RemoveObject(string id)
    {
        int hash = Convert_ToHash(id);
        if (objects.ContainsKey(hash))
        {
            GameObject gameObject = objects[hash];
            objects.Remove(hash);
            Destroy(gameObject);
        }
    }

    public void RemoveBread()
    {
        for(int i =0; i < breadPrefebList.Count; i++)
        {
            RemoveObject(breadPrefebList[i]);
        }

        breadPrefebList.Clear();
    }

    public void RemoveHotdog()
    {
        RemoveObject(hotdogInstance);
        hotdogInstance = null;
    }

    public void CreateFirstBread()
    {
        breadInstance = Instantiate(breadPrefeb);

        AddObject(breadInstance);

        breadInstance.transform.localPosition = new Vector3(11.27f, -2.05f, -0.5f);
    }

    public void RemoveFirstBread()
    {
        RemoveObject(breadInstance);
        breadInstance = null;
    }

}
