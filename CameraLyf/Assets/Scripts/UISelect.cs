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

    // Button click sound
    public AudioClip mBtnClick;

    // UI Text for information prompt
    private string[] mInformationPrompts = { "This is a mural about community living that reads: 'the more we get together, the happier we'll be'.",
        "This is a mural that about expressing creativity in a colourful way, with a sign that reads 'Innovators bloom here'.", 
        "This is a mural of interlocking chicken patterns, serving as a tribute to the chickens that used to roam this property.",
        "This is a mural that plays on the themes used across the property, such as portals and chickens." };
    private string[] mCurrentLocation = { "Rooftop level Unwind & Hang Out", "Level 5 Tower B Lift Area", "Level 1 Wash & Hang", "Level 5 Tower A Lift Area" };
    private string[] mMurals = { "Together", "Space", "Chicken", "Portal" };
    public Text mInfoPromptText;

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
        mInformationButton.SetActive(false);
        mInformationPrompt.SetActive(false);
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

        for (int i = 0; i < mMurals.Length; i++)
        {
            if (mMurals[i] == PopupSelect.mTarget.name)
            {
                mInfoPromptText.text = "Current Location: " +  mCurrentLocation[i] + "\n\n" + mInformationPrompts[i];
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
        PlaySound();

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
        PlaySound();

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

    public void PlaySound()
    {
        AudioSource audio = GetComponent<AudioSource>();

        audio.clip = mBtnClick;
        audio.Play();
    }
}