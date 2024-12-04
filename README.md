# Overview

This application is an AR Camera Application which scans specific murals on the Lyf property. It has the following features:
- Screenshot (will be saved to phone gallery)
- Information button (shows current location + information on mural)
- Back button (undo added stickers)

## Adding new stickers

To add new stickers in the application, you will need to create your sticker sprite and create an animation clip if applicable. After they have been created, do the following to add it into the app:
1. Create a folder in (.../assets/textures) and add all sprites for the sticker. Create an animation clip if applicable and put it in the same folder.
2. In the hierarchy, click on Canvas -> UI -> StickerMenu -> Panel and duplicate one of the current sticker Game Objects. Change the name of this Game Object to the sprite name, and change the sprite to the new sticker sprite. Change the Transform of this new sticker to be after the previous sticker, and change the Panel under StickerMenu to fit in this new sprite.
3. In the hierarchy, click on AppManager and scroll to the InfoList script to add in your new sticker information.
- If new sticker has no animations, add new sticker to mStickers and reorder it to be behind the last stickers with no animations (currently: GoodLyf). Open the script 'PopupSelect', scroll to the function 'SetupSticker' and add 1 to int staticStickers. (for one new sticker)
- If new sticker has animations, add new sticker to mStickers and add its animation to mAnimators

## Adding new models / effects

To add new models or effects, you will need to create a full 3D Model with textures, or create effects using UnityParticleSystem, as well as a UI sprite for users to click on. For reference, you can see previous models and effects under (.../assets/prefabs). After they have been created, do the following to add it into the app:
1. Drag your model/effect to the folder (.../assets/prefabs) and put it in its respectively named folders. For models, any additional resources can be put under (.../assets/prefabs/models/resources).
2. In the hierarchy, click on Canvas -> UI -> EffectMenu OR ModelMenu -> Panel and duplicate one of the current effect/model Game Objects. Change the name of this Game Object to the effect/model name, and change the sprite to the new UI sprite. Change the Transform of this new sprite to be after the previous sprite, and change the Panel under EffectMenu/ModelMenu to fit in this new sprite.
3. In the hierarchy, click on AppManager and scroll to the InfoList script to add in your new models/effects information.
- If the new asset is a model, add new model to mModels.
- If the new asset is an effect, add new effect to mEffects.

## Adding new Image Targets

To add new image target, you will need to create a Vuforia Database and download it to your device. After downloaded, you will need to import it to the project. After the database has been imported, do the following to add an image target into the app:
1. In the hierarchy, click on Image Targets and duplicate one of the current image targets. Change the database to your new Vuforia Database and change the image target to whichever image you plan to add. Rename the game object to a name related to your image.
2. Open the script 'InfoList' and under the function 'Start()', add a new Mural Information in the order of mural name (should be the same as image target name from game object), information on mural and mural location.
