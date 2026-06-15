using UnityEngine;
using UnityEditor;

public class FBXExtractor
{
    private static string _progressTitle = "Extracting";
    private static string _sourceExtension = ".fbx";
    private static string _targetMeshExtension = ".asset";
    private static string _targetAnimExtension = ".anim";


    [MenuItem("Assets/Extract Assets", validate = true)]
    private static bool ExtractMeshesMenuItemValidate()
    {
        for (int i = 0; i < Selection.objects.Length; i++)
        {
            if (!AssetDatabase.GetAssetPath(Selection.objects[i]).EndsWith(_sourceExtension))
                return false;
        }
        return true;
    }

    [MenuItem("Assets/Extract Assets")]
    private static void ExtractMeshesMenuItem()
    {
        EditorUtility.DisplayProgressBar(_progressTitle, "", 0);
        for (int i = 0; i < Selection.objects.Length; i++)
        {
            EditorUtility.DisplayProgressBar(_progressTitle, Selection.objects[i].name, (float)i / (Selection.objects.Length - 1));
            Extract(Selection.objects[i]);
        }
        EditorUtility.ClearProgressBar();
    }

    private static void Extract(Object selectedObject)
    {
        //Create Folder Hierarchy
        string selectedObjectPath = AssetDatabase.GetAssetPath(selectedObject);
        string parentfolderPath = selectedObjectPath.Substring(0, selectedObjectPath.Length - (selectedObject.name.Length + 5));
        string objectFolderName = selectedObject.name;
        string objectFolderPath = parentfolderPath + "/" + objectFolderName;
        string meshFolderName = "Meshes";
        string meshFolderPath = objectFolderPath + "/" + meshFolderName;
        string animFolderName = "Animations";
        string animFolderPath = objectFolderPath + "/" + animFolderName;

        if (!AssetDatabase.IsValidFolder(objectFolderPath))
        {
            AssetDatabase.CreateFolder(parentfolderPath, objectFolderName);

            if (!AssetDatabase.IsValidFolder(meshFolderPath))
            {
                AssetDatabase.CreateFolder(objectFolderPath, meshFolderName);
            }

            if (!AssetDatabase.IsValidFolder(animFolderPath))
            {
                AssetDatabase.CreateFolder(objectFolderPath, animFolderName);
            }
        }

        //Create Meshes
        Object[] objects = AssetDatabase.LoadAllAssetsAtPath(selectedObjectPath);

        for (int i = 0; i < objects.Length; i++)
        {
            objects[i].name = objects[i].name.Replace("|", "").Replace("/", "").Replace("?", "").Replace("<", "").Replace(">", "").Replace(".", "").Replace(":", "");
            
            if (objects[i] is Mesh)
            {
                EditorUtility.DisplayProgressBar(_progressTitle, selectedObject.name + " : " + objects[i].name, (float)i / (objects.Length - 1));

                Mesh mesh = Object.Instantiate(objects[i]) as Mesh;

                AssetDatabase.CreateAsset(mesh, meshFolderPath + "/" + objects[i].name + _targetMeshExtension);
            }
            else if (objects[i] is AnimationClip)
            {
                EditorUtility.DisplayProgressBar(_progressTitle, selectedObject.name + " : " + objects[i].name, (float)i / (objects.Length - 1));

                AnimationClip animation = Object.Instantiate(objects[i]) as AnimationClip;

                AssetDatabase.CreateAsset(animation, animFolderPath + "/" + objects[i].name + _targetAnimExtension);
            }
        }

        //Cleanup
        AssetDatabase.MoveAsset(selectedObjectPath, objectFolderPath + "/" + selectedObject.name + _sourceExtension);
        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();

    }
}
 