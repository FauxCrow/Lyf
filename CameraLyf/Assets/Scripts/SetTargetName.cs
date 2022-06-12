using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SetTargetName : MonoBehaviour
{
    // UI GameObject for rewards
    public GameObject mReward;
    public Text mRewardText;

    // Calls audiosource from app manager + reward clip
    public AudioSource mAudioSource;
    public AudioClip mRewardSound;

    private bool isNew = true;

    public void SetTarget()
    {
        PopupSelect.mTarget = this.gameObject;

        if (isNew == true)
        {
            StartCoroutine(Reward());
            UISelect.mRewardCoins += 5;
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

        yield return new WaitForSeconds(4);

        // Disable after 4 seconds
        mReward.SetActive(false);
        mAudioSource.Stop();
    }
}