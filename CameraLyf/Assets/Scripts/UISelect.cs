using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
    public GameObject mInformationButton;
    public GameObject mInformationPrompt;
    public GameObject mReward;

    // Button click sound
    public AudioClip mBtnClick;

    // UI Text for information prompt
    public Text mInfoPromptText;

    //check if top menu is open
    private bool stickerState;
    private bool effectState;
    private bool modelState;

    //checks if it is the first time a mural is open
    private bool isFirstTogether = true;

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
        mInformationButton.SetActive(false);
        mInformationPrompt.SetActive(false);
        mReward.SetActive(false);
        mCameraPrompt.SetActive(true);

        stickerState = false;
        effectState = false;
        modelState = false;
    }

    public void OpenMenu()
    {
        mBottomMenu.SetActive(true);
        mCameraButton.SetActive(true);
        mInformationButton.SetActive(true);
        mCameraPrompt.SetActive(false);

        // sets back button activ if there are children under target
        if (PopupSelect.mTarget.transform.childCount > 0) 
        {
            mBackButton.SetActive(true);
        }
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
        mInformationButton.SetActive(false);
        mCameraPrompt.SetActive(true);
    }

    public void OpenInformation()
    {
        PlaySound();

        mInformationPrompt.SetActive(true);

        // Sets text in information prompt based on mural name
        foreach (InfoList.MuralInfo mMuralInfo in InfoList.mMuralInfo)
        {
            if (mMuralInfo.mMuralName == PopupSelect.mTarget.name)
            {
                mInfoPromptText.text = "Current Location: " + mMuralInfo.mCurrentLocation + "\n\n" + mMuralInfo.mInfoPrompt;
                return;
            }
        }
    }

    public void CloseInformation()
    {
        mInformationPrompt.SetActive(false);
    }

    public void StickerState()
    {
        PlaySound();

        //sets model/effect menu false in case they are open
        mEffectsMenu.SetActive(false);
        effectState = false;
        mModelsMenu.SetActive(false);
        modelState = false;

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
        PlaySound();

        //sets sticker/model menu false in case they are open
        mStickerMenu.SetActive(false);
        stickerState = false;
        mModelsMenu.SetActive(false);
        modelState = false;

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
        PlaySound();

        //sets sticker/effect menu false in case they are open
        mStickerMenu.SetActive(false);
        stickerState = false;
        mEffectsMenu.SetActive(false);
        effectState = false;

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

    public void PlaySound()
    {
        AudioSource audio = GetComponent<AudioSource>();

        audio.clip = mBtnClick;
        audio.Play();
    }
}