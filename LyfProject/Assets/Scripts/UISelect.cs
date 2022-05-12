using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UISelect : MonoBehaviour
{
    // UI GameObjects for user selection
    public GameObject mStickerMenu;
    public GameObject mEffectsMenu;
    public GameObject mModelsMenu;
    public GameObject mBottomMenu;
    public GameObject mTopMenu;
    public GameObject mCameraButton;
    public GameObject mCameraPrompt;
    public GameObject mBackButton;
    public GameObject mMenuButton;

    //check if top menu is open
    private bool stickerState;
    private bool effectState;
    private bool modelState;

    // Start is called before the first frame update
    void Start()
    {
        // Set all UI inactive
        mBottomMenu.SetActive(false);
        mTopMenu.SetActive(false);
        mStickerMenu.SetActive(false);
        mEffectsMenu.SetActive(false);
        mModelsMenu.SetActive(false);
        mCameraButton.SetActive(false);
        mMenuButton.SetActive(false);
        mCameraPrompt.SetActive(true);

        stickerState = false;
        effectState = false;
        modelState = false;
    }

    public void OpenMenu()
    {
        mBottomMenu.SetActive(true);
        mCameraButton.SetActive(true);
        mMenuButton.SetActive(true);
        mCameraPrompt.SetActive(false);
    }

    public void CloseMenu()
    {
        mStickerMenu.SetActive(false);
        mEffectsMenu.SetActive(false);
        mModelsMenu.SetActive(false);
        mBottomMenu.SetActive(false);
        mTopMenu.SetActive(false);
        mCameraButton.SetActive(false);
        mBackButton.SetActive(false);
        mMenuButton.SetActive(false);
        mCameraPrompt.SetActive(true);
    }

    public void StickerState()
    {
        if (effectState == true || modelState == true)
        {
            mEffectsMenu.SetActive(false);
            effectState = false;
            mModelsMenu.SetActive(false);
            modelState = false;
        }

        if (stickerState == false)
        {
            mStickerMenu.SetActive(true);
            mTopMenu.SetActive(true);
            stickerState = true;
            return;
        }
        mStickerMenu.SetActive(false);
        mTopMenu.SetActive(false);
        stickerState = false;
    }

    public void EffectState()
    {
        if (stickerState == true || modelState == true)
        {
            mStickerMenu.SetActive(false);
            stickerState = false;
            mModelsMenu.SetActive(false);
            modelState = false;
        }

        if (effectState == false)
        {
            mEffectsMenu.SetActive(true);
            mTopMenu.SetActive(true);
            effectState = true;
            return;
        }

        mEffectsMenu.SetActive(false);
        mTopMenu.SetActive(false);
        effectState = false;
    }

    public void ModelState()
    {
        if (stickerState == true || effectState == true)
        {
            mStickerMenu.SetActive(false);
            stickerState = false;
            mEffectsMenu.SetActive(false);
            effectState = false;
        }

        if (modelState == false)
        {
            mModelsMenu.SetActive(true);
            mTopMenu.SetActive(true);
            modelState = true;
            return;
        }
        mModelsMenu.SetActive(false);
        mTopMenu.SetActive(false);
        modelState = false;
    }
}