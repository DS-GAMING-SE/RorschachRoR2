using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class AvatarMaker
{
    [MenuItem("CustomTools/MakeAvatarMask")]
    private static void MakeAvatarMask()
    {
        GameObject activeGameObject = Selection.activeGameObject;

        if (activeGameObject != null)
        {
            AvatarMask avatarMask = new AvatarMask();

            avatarMask.AddTransformPath(activeGameObject.transform);

            var path = string.Format("Assets/{0}.mask", activeGameObject.name.Replace(':', '_'));
            AssetDatabase.CreateAsset(avatarMask, path);
        }
    }

    [MenuItem("CustomTools/MakeAvatar")]
    private static void MakeAvatar()
    {
        GameObject activeGameObject = Selection.activeGameObject;

        if (activeGameObject != null)
        {
            Avatar avatar = AvatarBuilder.BuildGenericAvatar(activeGameObject, "");
            avatar.name = activeGameObject.name;
            Debug.Log(avatar.isHuman ? "is human" : "is generic");

            var path = string.Format("Assets/{0}.ht", avatar.name.Replace(':', '_'));
            AssetDatabase.CreateAsset(avatar, path);
        }
    }
    [MenuItem("Assets/Clean Animation Clips", validate = true)]
    private static bool CleanAnimationClipsValidate()
    {
        for (int i = 0; i < Selection.objects.Length; i++)
        {
            if (!AssetDatabase.GetAssetPath(Selection.objects[i]).EndsWith(".anim"))
                return false;
        }
        return true;
    }
    [MenuItem("Assets/Clean Animation Clips")]
    private static void CleanAnimationClips()
    {
        EditorUtility.DisplayProgressBar("Renaming Bones", "", 0);
        for (int i = 0; i < Selection.objects.Length; i++)
        {
            EditorUtility.DisplayProgressBar("Renaming Bones", Selection.objects[i].name, (float)i / (Selection.objects.Length - 1));
            AnimationClip clip = (AnimationClip)Selection.objects[i];
            EditorCurveBinding[] objRef = AnimationUtility.GetObjectReferenceCurveBindings(clip);
            for (int j = 0; j < objRef.Length; j++)
            {
                objRef[j].propertyName = objRef[j].propertyName.Replace(".", "");
            }
            //AnimationUtility.SetObjectReferenceCurves()
        }
        EditorUtility.ClearProgressBar();
    }
}
