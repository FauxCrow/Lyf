using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Lean.Touch;

public class StickerSelect : MonoBehaviour
{
    // Current image target (set in SetTargetName script)
    public static GameObject mTarget;

    // Pre-existing array contraining effects prefabs
    public GameObject[] mEffects;

    // Pre-existing array contraining models prefabs
    public GameObject[] mModels;

    public GameObject mStickerPrefab;
    public GameObject mBackButton;

    // Pre-existing arrays containing all sprites, Effects and animators, used to set spawned sticker's sprite and animator
    public Sprite[] mStickers;
    public RuntimeAnimatorController[] mAnimators;

    // Total number of popups in the scene
    public static int mTotalPopupNo = 0;

    // Reference to sticker number of sticker chosen
    private int mCurrentPopupNo;

    // Reference to the previous popup's name
    private string mLastPopupName;

    // Reference to the type of popup chosen (sticker, 3d model, effects)
    private string mPopupType;

    // Reference to target's SpriteRenderer and Animator, changed upon sticker spawned
    private SpriteRenderer mStickerSprite;
    private Animator mStickerAnim;

    // Reference to last sticker spawned
    private GameObject mLastPopup;

    // Start is called before the first frame update
    void Start()
    {
        SetBackActive();
    }

    public void SpawnPopup()
    {
        SetupPopup();

        //Disable Lean for any previous sticker (if applicable)
        if (mTotalPopupNo > 0)
        {
            mLastPopupName = "Popup" + (mTotalPopupNo - 1);
            mLastPopup = GameObject.Find(mLastPopupName);
            DisableLean(mLastPopup);
        }

        if (mPopupType == "Sticker")
        {
            // Spawn new sticker as child of current image target
            // Change position and rotation to 0
            // Rename to current sticker number (used to delete target if user chooses to undo)
            GameObject target = Instantiate(mStickerPrefab);
            target.transform.parent = mTarget.transform;
            target.transform.localPosition = new Vector3(0f, 0f, 0f);
            target.transform.rotation = Quaternion.identity;
            target.name = "Popup" + mTotalPopupNo;

            // Use popup number to setup sticker
            SetupSticker(target);
        }
        else if (mPopupType == "Effects")
        {
            // Spawn new effects as child of current image target
            // Change position and rotation to 0
            // Rename to current sticker number (used to delete target if user chooses to undo)
            GameObject target = Instantiate(mEffects[mCurrentPopupNo]);
            target.transform.parent = mTarget.transform;
            target.transform.localPosition = new Vector3(0f, 0f, 0f);
            target.transform.rotation = Quaternion.identity;
            target.name = "Popup" + mTotalPopupNo;
        }
        else if (mPopupType == "Models")
        {
            GameObject target = Instantiate(mModels[mCurrentPopupNo]);
            target.transform.parent = mTarget.transform;
            target.transform.localPosition = new Vector3(0f, 0f, 0f);
            target.name = "Popup" + mTotalPopupNo;
        }

        mTotalPopupNo++;

        // Sets BackButton active if this was first sticker added to mural
        SetBackActive();
    }

    private void SetupPopup()
    {
        // checks if button calls for sticker sprites in pre-existing array, if so set sticker number as sprite key in array
        for (int i = 0; i < mStickers.Length; i++)
        {
            if (mStickers[i].name == this.name)
            {
                mPopupType = "Sticker";
                mCurrentPopupNo = i;
                return;
            }
        }

        // checks if button calls for effects particle systems in pre-exiting array, if so set effects number as effects key in array
        for (int i = 0; i < mEffects.Length; i++)
        {
            if (mEffects[i].name == this.name)
            {
                mPopupType = "Effects";
                mCurrentPopupNo = i;
                return;
            }
        }

        // checks if button calls for models prefab in pre-exiting array, if so set model number as effects key in array
        for (int i = 0; i < mModels.Length; i++)
        {
            if (mModels[i].name == this.name)
            {
                mPopupType = "Models";
                mCurrentPopupNo = i;
            }
        }
    }

    // Finds the last sticker placed by name and destroys it
    public void BackButton()
    {
        mLastPopupName = "Popup" + (mTotalPopupNo - 1);
        mLastPopup = GameObject.Find(mLastPopupName);
        Destroy(mLastPopup);

        mTotalPopupNo--;

        if (mTotalPopupNo > 0)
        {
            mLastPopupName = "Popup" + (mTotalPopupNo - 1);
            mLastPopup = GameObject.Find(mLastPopupName);
            EnableLean(mLastPopup);
        }

        // Checks if there are more stickers after deleting one
        SetBackActive();
    }

    // Hides the undo button if there are no stickers (nothing to undo)
    public void SetBackActive()
    {
        if (mTotalPopupNo == 0)
        {
            mBackButton.SetActive(false);
            return;
        }

        mBackButton.SetActive(true);
    }

    // Setup sticker sprite and animations (if applicable) to newly spawned popup (target)
    private void SetupSticker(GameObject target)
    {
        mStickerSprite = target.GetComponent<SpriteRenderer>();
        mStickerSprite.sprite = mStickers[mCurrentPopupNo];

        if (mCurrentPopupNo != 3)
        {
            mStickerAnim = target.GetComponent<Animator>();
            mStickerAnim.runtimeAnimatorController = mAnimators[mCurrentPopupNo];
        }
    }

    // Disables Lean components of previous objects (user can only move current sticker)
    public void DisableLean(GameObject previousObject)
    {
        previousObject.GetComponent<LeanPinchScale>().enabled = false;
        previousObject.GetComponent<LeanDragTranslate>().enabled = false;
    }

    // Enables Lean components of previous object after clicking back button
    public void EnableLean(GameObject previousObject)
    {
        previousObject.GetComponent<LeanPinchScale>().enabled = true;
        previousObject.GetComponent<LeanDragTranslate>().enabled = true;
    }
}
