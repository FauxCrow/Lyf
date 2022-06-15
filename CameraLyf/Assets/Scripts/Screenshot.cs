using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;

public class Screenshot : MonoBehaviour
{
    public GameObject mUI;
    public GameObject mBackBtn;
    public GameObject mInfoBtn;
    public Image mCameraSprite;

    public void TakeScreenshot()
    {
        // Clears UI before taking a screenshot
        mUI.SetActive(false);
        mBackBtn.SetActive(false);
        mInfoBtn.SetActive(false);
        mCameraSprite.color = Color.clear;

        StartCoroutine("TakingScreenshot");
    }

    private IEnumerator TakingScreenshot()
    {
        yield return new WaitForEndOfFrame();
        Texture2D texture = new Texture2D(Screen.width, Screen.height, TextureFormat.RGB24, false);

        texture.ReadPixels(new Rect(0, 0, Screen.width, Screen.height), 0, 0);
        texture.Apply();

        string name = "Screenshot_Lyf" + System.DateTime.Now.ToString("yyyy.MM.dd.HH.mm.ss") + ".png"; // Adds unique name to image so it will not share image names

        //PC
        //byte[] bytes = texture.EncodeToPNG();
        //File.WriteAllBytes(Application.dataPath + "/../" + name, bytes);

        //Mobile
        NativeGallery.SaveImageToGallery(texture, "LyfApp Pictures", name); // Refer to NativeGallery Plugin

        Destroy(texture);

        // Resets UI to previous state
        mUI.SetActive(true);
        mBackBtn.SetActive(true);
        mInfoBtn.SetActive(true);
        mCameraSprite.color = Color.white;
    }
}