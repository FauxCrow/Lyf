using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InfoList : MonoBehaviour
{
    // total reward coins collected
    public static float mRewardCoins = 0;

    // Pre-existing array contraining popup prefabs
    public GameObject[] mEffects;
    public GameObject[] mModels;

    // Pre-existing array containing sticker sprites and animations
    public Sprite[] mStickers;
    public RuntimeAnimatorController[] mAnimators;

    // Class containing information needed for information prompt
    public class MuralInfo
    {
        public string mMuralName;
        public string mInfoPrompt;
        public string mCurrentLocation;

        public MuralInfo(string muralName, string infoPrompt, string currentLocation)
        {
            this.mMuralName = muralName;
            this.mInfoPrompt = infoPrompt;
            this.mCurrentLocation = currentLocation;
        }
    }

    // List containing all instances of mural information
    public static List<MuralInfo> mMuralInfo = new List<MuralInfo>();

    // Start is called before the first frame update
    void Start()
    {
        // Adds all mural information based on those currently in App
        mMuralInfo.Add(new MuralInfo("Together", "This is a mural about community living that reads: 'the more we get together, the happier we'll be'.", "Rooftop level Unwind & Hang Out"));
        mMuralInfo.Add(new MuralInfo("Space", "This is a mural that about expressing creativity in a colourful way, with a sign that reads 'Innovators bloom here'.", "Level 5 Tower B Lift Area"));
        mMuralInfo.Add(new MuralInfo("Chicken", "This is a mural of interlocking chicken patterns, serving as a tribute to the chickens that used to roam this property.", "Level 1 Wash & Hang"));
        mMuralInfo.Add(new MuralInfo("Portal", "This is a mural that plays on the themes used across the property, such as portals and chickens.", "Level 5 Tower A Lift Area"));
    }
}

