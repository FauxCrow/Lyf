using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Lean.Touch;

public class PopupSelect : MonoBehaviour
{
    // Current image target (set in SetTargetName script)
    public static GameObject mTarget;

    public GameObject mStickerPrefab;
    public GameObject mBackButton;

    // variables to store information from infoList for reference
    private GameObject mAppManager;
    private InfoList mInfoList;

    // Total number of popups in the scene
    public static int mTotalPopupNo = 0;

    // Reference to sticker number of sticker chosen
    private int mCurrentPopupNo;

    // Reference to the previous popup's name
    private string mLastPopupName;

    // Reference to target's SpriteRenderer and Animator, changed upon sticker spawned
    private SpriteRenderer mStickerSprite;
    private Animator mStickerAnim;

    // Reference to last sticker spawned
    private GameObject mLastPopup;

    // Start is called before the first frame update
    void Start()
    {
        SetBackActive();
        mAppManager = GameObject.Find("AppManager");
        mInfoList = mAppManager.GetComponent<InfoList>();
    }

    public void SpawnPopup()
    {
        //Disable Lean for any previous sticker (if applicable)
        if (mTotalPopupNo > 0)
        {
            mLastPopupName = "Popup" + (mTotalPopupNo - 1);
            mLastPopup = GameObject.Find(mLastPopupName);
            DisableLean(mLastPopup);
        }

        // switch statement identities popup-type and scans for corresponding popup number to setup popup
        switch (gameObject.tag)
        {
            case ("Sticker"):
                for (int i = 0; i < mInfoList.mStickers.Length; i++)
                {
                    if (mInfoList.mStickers[i].name == this.name)
                    {
                        mCurrentPopupNo = i;
                    }
                }

                SetupTarget(mStickerPrefab);
                break;

            case ("Effect"):
                for (int i = 0; i < mInfoList.mEffects.Length; i++)
                {
                    if (mInfoList.mEffects[i].name == this.name)
                    {
                        mCurrentPopupNo = i;
                    }
                }

                SetupTarget(mInfoList.mEffects[mCurrentPopupNo]);
                break;

            case ("Model"):
                for (int i = 0; i < mInfoList.mModels.Length; i++)
                {
                    if (mInfoList.mModels[i].name == this.name)
                    {
                        mCurrentPopupNo = i;
                    }
                }

                SetupTarget(mInfoList.mModels[mCurrentPopupNo]);
                break;
        }

        mTotalPopupNo++;

        // Sets BackButton active if this was first sticker added to mural
        SetBackActive();
    }

    private void SetupTarget(GameObject type)
    {
        // Spawn new type as child of current image target
        // Change position to worldspace 0,0,0
        // Rename to current sticker number (used to delete target if user chooses to undo)
        GameObject target = Instantiate(type);
        target.transform.parent = mTarget.transform;
        target.transform.localPosition = new Vector3(0f, 0f, 0f);
        target.name = "Popup" + mTotalPopupNo;

        if (type == mStickerPrefab)
        {
            // Use popup number to setup sticker
            SetupSticker(target);
        }
    }

    // Setup sticker sprite and animations (if applicable) to newly spawned popup (target)
    private void SetupSticker(GameObject target)
    {
        int staticStickers = 1; // used to show how many static stickers at the beginning of the array to skip part when calling animations for animated stickers

        mStickerSprite = target.GetComponent<SpriteRenderer>();
        mStickerSprite.sprite = mInfoList.mStickers[mCurrentPopupNo];

        if (mCurrentPopupNo > staticStickers - 1)
        {
            mStickerAnim = target.GetComponent<Animator>();
            mStickerAnim.runtimeAnimatorController = mInfoList.mAnimators[mCurrentPopupNo + staticStickers];
        }
    }

    // Finds the last sticker placed by name and destroys it
    public void BackButton()
    {
        mLastPopupName = "Popup" + (mTotalPopupNo - 1);
        mLastPopup = GameObject.Find(mLastPopupName);
        Destroy(mLastPopup);

        mTotalPopupNo--;

        // Sets new last popup and enables lean movement for it
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
        if (mTotalPopupNo > 0)
        {
            mBackButton.SetActive(true);
            return;
        }

        mBackButton.SetActive(false);
    }

    // Disables Lean components of previous objects (user can only move current sticker)
    public void DisableLean(GameObject previousObject)
    {
        previousObject.GetComponent<LeanPinchScale>().enabled = false;
        previousObject.GetComponent<LeanDragTranslate>().enabled = false;
        previousObject.GetComponent<LeanTwistRotate>().enabled = false;
    }

    // Enables Lean components of previous object after clicking back button
    public void EnableLean(GameObject previousObject)
    {
        previousObject.GetComponent<LeanPinchScale>().enabled = true;
        previousObject.GetComponent<LeanDragTranslate>().enabled = true;
        previousObject.GetComponent<LeanTwistRotate>().enabled = true;
    }
}