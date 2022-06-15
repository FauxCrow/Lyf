using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// This script will be placed on all Image Targets
public class SetTargetName : MonoBehaviour
{
    // UI GameObject for rewards
    public GameObject mReward;
    public Text mRewardText;

    // Calls audiosource from app manager + reward clip
    public AudioSource mAudioSource;
    public AudioClip mRewardSound;

    private bool isNew = true;

    // Sets target name in PopupSelect and checks if this is the user's first time scanning target mural
    public void SetTarget()
    {
        PopupSelect.mTarget = this.gameObject;

        if (isNew == true)
        {
            StartCoroutine(Reward());
            InfoList.mRewardCoins += 5;
            isNew = false;
        }

    }

    // Plays reward sound and shows pop-up revealing how they got reward (first time opening)
    IEnumerator Reward()
    {
        mReward.SetActive(true);

        // play reward sound
        mAudioSource.clip = mRewardSound;
        mAudioSource.Play();

        // set text
        mRewardText.text = "Congratulations! You have found the " + this.name + " mural!";

        yield return new WaitForSeconds(2);

        // Disable after 2 seconds
        mReward.SetActive(false);
        mAudioSource.Stop();
    }
}