using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

public class LoadTexture : MonoBehaviour
{
    enum m_Texture
    {
        mob = 0,
        yao
    }
    [SerializeField]
    m_Texture m_select = m_Texture.mob;
    GameObject MyBox;
    string selectMaterial = "mob";
    // Start is called before the first frame update
    void Start()
    {
        selectMaterial = m_select != m_Texture.mob ? "y" : "mob";
        Addressables.LoadAssetAsync<GameObject>("MobCube").Completed += onLoadDown;
    }
            
    void onLoadDown(AsyncOperationHandle<GameObject> obj)
    {
        if(obj.Status == AsyncOperationStatus.Succeeded)
        {
            print("Load Succeeded!");
            MyBox = obj.Result;
            //MyBox = Instantiate(obj.Result, new Vector3(0, 0, 0), Quaternion.identity);
            Addressables.LoadAssetAsync<Texture2D>(selectMaterial).Completed += onMaterialLoadDown;
        }
    }

    void onMaterialLoadDown(AsyncOperationHandle<Texture2D> obj)
    {
        if(obj.Status == AsyncOperationStatus.Succeeded)
        {
            print("Matrial Load Succeed!");
            MyBox.GetComponent<Renderer>().sharedMaterial.mainTexture = obj.Result;
            MyBox = Instantiate(MyBox, new Vector3(0, 0, 0), Quaternion.identity);
        }
    }

    // Update is called once per frame

}
