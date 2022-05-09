using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.UI;

public class Screenshot : MonoBehaviour
{
    public GameObject mUI;
    public Image mCameraSprite;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void TakeScreenshot()
    {
        mUI.SetActive(false);
        mCameraSprite.color = Color.clear;
        StartCoroutine("TakingScreenshot");
    }

    private IEnumerator TakingScreenshot()
    {
        yield return new WaitForEndOfFrame();
        Texture2D texture = new Texture2D(Screen.width, Screen.height, TextureFormat.RGB24, false);

        texture.ReadPixels(new Rect(0, 0, Screen.width, Screen.height), 0, 0);
        texture.Apply();

        string name = "Screenshot_Lyf" + System.DateTime.Now.ToString("yyyy.MM.dd.HH.mm.ss") + ".png";

        //PC
        //byte[] bytes = texture.EncodeToPNG();
        //File.WriteAllBytes(Application.dataPath + "/../" + name, bytes);

        //Mobile
        NativeGallery.SaveImageToGallery(texture, "LyfApp Pictures", name);

        Destroy(texture);
        mUI.SetActive(true);
        mCameraSprite.color = Color.white;
    }
}
