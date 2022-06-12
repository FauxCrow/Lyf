using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Lean.Touch;

public class PopupSelect : MonoBehaviour
{
    // Current image target (set in SetTargetName script)
    public static GameObject mTarget;

    // Pre-existing array contraining effects prefabs
    public GameObject[] mEffects;

    // Pre-existing array contraining models prefabs
    public GameObject[] mModels;

    // Pre-existing arrays containing all sprites, Effects and animators, used to set spawned sticker's sprite and animator
    public Sprite[] mStickers;
    public RuntimeAnimatorController[] mAnimators;

    public GameObject mStickerPrefab;
    public GameObject mBackButton;

    // Reference to AR Camera
    public Camera mCamera;

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

        switch (gameObject.tag)
        {
            case ("Sticker"):
                for (int i = 0; i < mStickers.Length; i++)
                {
                    if (mStickers[i].name == this.name)
                    {
                        mCurrentPopupNo = i;
                    }
                }

                SetupTarget(mStickerPrefab);
                break;

            case ("Effect"):
                for (int i = 0; i < mEffects.Length; i++)
                {
                    if (mEffects[i].name == this.name)
                    {
                        mCurrentPopupNo = i;
                    }
                }

                SetupTarget(mEffects[mCurrentPopupNo]);
                break;

            case ("Model"):
                for (int i = 0; i < mModels.Length; i++)
                {
                    if (mModels[i].name == this.name)
                    {
                        mCurrentPopupNo = i;
                    }
                }

                SetupTarget(mModels[mCurrentPopupNo]);
                break;
        }

        mTotalPopupNo++;

        // Sets BackButton active if this was first sticker added to mural
        SetBackActive();
    }

    private void SetupTarget(GameObject type)
    {
        // Spawn new type as child of current image target
        // Change position and rotation to 0
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
        mStickerSprite = target.GetComponent<SpriteRenderer>();
        mStickerSprite.sprite = mStickers[mCurrentPopupNo];

        if (mCurrentPopupNo > 0)
        {
            mStickerAnim = target.GetComponent<Animator>();
            mStickerAnim.runtimeAnimatorController = mAnimators[mCurrentPopupNo - 1];
        }
    }

    // Finds the last sticker placed by name and destroys it
    public void BackButton()
    {
        mLastPopupName = "Popup" + (mTotalPopupNo - 1);
        mLastPopup = GameObject.Find(mLastPopupName);
        Destroy(mLastPopup);

        mTotalPopupNo--;

        // Sets new last popup and enables lean movement
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
    
    // Resets all sticker rotations to face AR camera
    public void FaceCamera(int totalPopupNo)
    {
        for (int i = totalPopupNo; i > -1; i--)
        {
            mLastPopupName = "Popup" + (i);
            mLastPopup = GameObject.Find(mLastPopupName);

            mLastPopup.transform.LookAt(mLastPopup.transform.position + mCamera.transform.rotation * Vector3.forward, mCamera.transform.rotation * Vector3.up);
        }
    }
}