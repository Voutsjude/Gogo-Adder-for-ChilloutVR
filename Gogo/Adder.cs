#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEditor.UIElements;
using System.IO;
using System.Collections.Generic;
using ABI.CCK.Components;
using ABI.CCK.Scripts;
using System.Drawing.Printing;
using System.Threading;
using static UnityEditor.ShaderData;

public class Adder : EditorWindow
{

    [MenuItem("Judes/GogoAdder")]
    public static void ShowExample()
    {
        Adder wnd = GetWindow<Adder>();
        wnd.titleContent = new GUIContent("Adder");
    }

    public void CreateGUI()
    {
        // Each editor window contains a root VisualElement object
        VisualElement root = rootVisualElement;

        ObjectField avi = new ObjectField("Avi") { allowSceneObjects = true, objectType = typeof(GameObject) };

        Button enableAdvancedSettingsBtn = new Button() { text = "Enable Advanced Settings" };

        enableAdvancedSettingsBtn.clicked += () =>
        {
            GameObject aviTemp = (GameObject)avi.value;

            CVRAvatar cvrAvatar = aviTemp.GetComponent<CVRAvatar>();

            cvrAvatar.avatarUsesAdvancedSettings = true;
        };



        Button addBtn = new Button() { text = "Add Inputs" };

        addBtn.clicked += () =>
        {
            
            GameObject aviTemp = (GameObject)avi.value;

            CVRAvatar cvrAvatar = aviTemp.GetComponent<CVRAvatar>();


            cvrAvatar.avatarSettings.settings.Add(new CVRAdvancedSettingsEntry() { name = "Gogo Chair", machineName = "GogoChair", type = CVRAdvancedSettingsEntry.SettingsType.GameObjectDropdown });
            cvrAvatar.avatarSettings.settings.Add(new CVRAdvancedSettingsEntry() { name = "Gogo Sit", machineName = "GogoSit", type = CVRAdvancedSettingsEntry.SettingsType.GameObjectDropdown });
            cvrAvatar.avatarSettings.settings.Add(new CVRAdvancedSettingsEntry() { name = "Gogo Prone", machineName = "GogoProne", type = CVRAdvancedSettingsEntry.SettingsType.GameObjectDropdown });

            
            


            

        };


        Button addBtn2 = new Button() { text = "Add Rest" };

        addBtn2.clicked += () =>
        {
            
            string[] posesChairPaths = Directory.GetFiles(Application.dataPath + "/Gogo/Chair/", "*.anim");
            string[] posesSitPaths = Directory.GetFiles(Application.dataPath + "/Gogo/Sit/", "*.anim");
            string[] posesPronePaths = Directory.GetFiles(Application.dataPath + "/Gogo/Prone/", "*.anim");

            

            for (int i = 0; i < posesChairPaths.Length; i++)
            {
                string tempPath = posesChairPaths[i];
                if (tempPath.StartsWith(Application.dataPath))
                {
                    posesChairPaths[i] = "Assets" + tempPath.Substring(Application.dataPath.Length);
                }
            }
            for (int i = 0; i < posesSitPaths.Length; i++)
            {
                string tempPath = posesSitPaths[i];
                if (tempPath.StartsWith(Application.dataPath))
                {
                    posesSitPaths[i] = "Assets" + tempPath.Substring(Application.dataPath.Length);
                }
            }
            for (int i = 0; i < posesPronePaths.Length; i++)
            {
                string tempPath = posesPronePaths[i];
                if (tempPath.StartsWith(Application.dataPath))
                {
                    posesPronePaths[i] = "Assets" + tempPath.Substring(Application.dataPath.Length);
                }
            }

            List<AnimationClip> posesChair = new List<AnimationClip>();
            List<AnimationClip> posesSit = new List<AnimationClip>();
            List<AnimationClip> posesProne = new List<AnimationClip>();
            



            foreach (string fileName in posesChairPaths)
            {
                posesChair.Add(AssetDatabase.LoadAssetAtPath(fileName, typeof(AnimationClip)) as AnimationClip);
            }
            foreach (string fileName in posesSitPaths)
            {
                posesSit.Add(AssetDatabase.LoadAssetAtPath(fileName, typeof(AnimationClip)) as AnimationClip);
            }
            foreach (string fileName in posesPronePaths)
            {
                posesProne.Add(AssetDatabase.LoadAssetAtPath(fileName, typeof(AnimationClip)) as AnimationClip);
            }
            

            

            GameObject aviTemp = (GameObject)avi.value;

            CVRAvatar cvrAvatar = aviTemp.GetComponent<CVRAvatar>();

            var cvrAvatarASettings = cvrAvatar.avatarSettings;


            int currentSize = cvrAvatar.avatarSettings.settings.Count;
            Thread.Sleep(500);


            CVRAdvancesAvatarSettingGameObjectDropdown temporaryDropdown = (CVRAdvancesAvatarSettingGameObjectDropdown)cvrAvatar.avatarSettings.settings[currentSize - 3].setting;
            temporaryDropdown.options.Add(new CVRAdvancedSettingsDropDownEntry() { name = "Off" });
            temporaryDropdown = (CVRAdvancesAvatarSettingGameObjectDropdown)cvrAvatar.avatarSettings.settings[currentSize - 2].setting;
            temporaryDropdown.options.Add(new CVRAdvancedSettingsDropDownEntry() { name = "Off" });
            temporaryDropdown = (CVRAdvancesAvatarSettingGameObjectDropdown)cvrAvatar.avatarSettings.settings[currentSize - 1].setting;
            temporaryDropdown.options.Add(new CVRAdvancedSettingsDropDownEntry() { name = "Off" });


            
            foreach (AnimationClip tempAnim in posesChair)
            {
                CVRAdvancesAvatarSettingGameObjectDropdown ChairDropdown = (CVRAdvancesAvatarSettingGameObjectDropdown)cvrAvatar.avatarSettings.settings[currentSize - 3].setting;
                ChairDropdown.options.Add(new CVRAdvancedSettingsDropDownEntry() { name = tempAnim.name, useAnimationClip = true, animationClip = tempAnim });
            }
            
            foreach (AnimationClip tempAnim in posesSit)
            {
                CVRAdvancesAvatarSettingGameObjectDropdown tempDropdown = (CVRAdvancesAvatarSettingGameObjectDropdown)cvrAvatar.avatarSettings.settings[currentSize - 2].setting;
                tempDropdown.options.Add(new CVRAdvancedSettingsDropDownEntry() { name = tempAnim.name, useAnimationClip = true, animationClip = tempAnim });
            }
            foreach (AnimationClip tempAnim in posesProne)
            {
                CVRAdvancesAvatarSettingGameObjectDropdown tempDropdown = (CVRAdvancesAvatarSettingGameObjectDropdown)cvrAvatar.avatarSettings.settings[currentSize - 1].setting;
                tempDropdown.options.Add(new CVRAdvancedSettingsDropDownEntry() { name = tempAnim.name, useAnimationClip = true, animationClip = tempAnim });
            }
        };

        root.Add(avi);
        root.Add(enableAdvancedSettingsBtn);
        root.Add(addBtn);
        root.Add(addBtn2);

    }
}

#endif