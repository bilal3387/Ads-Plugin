using System.IO;

using UnityEditor;
using UnityEngine;
using GoogleMobileAds.Api;

//namespace AdsManagerPlugin.Editor
//{



[InitializeOnLoad]
    [CustomEditor(typeof(AdsManagerSettings))]
    public class AdsManagerSettingsEditor : UnityEditor.Editor
    {
    private const string AdsManagerSettingsDir = "Assets/AdsManager";

    private const string AdsManagerSettingsResDir = "Assets/AdsManager/Resources";

    private const string AdsManagerSettingsFile =
        "Assets/AdsManager/Resources/AdsManagerSettings.asset";

    [MenuItem("Assets/Ads Manager/Settings...")]
        public static void OpenInspector()
        {
        AdsManagerSettings instance = AdsManagerSettings.Instance;

                if (instance == null)
                {
                    if (!AssetDatabase.IsValidFolder(AdsManagerSettingsResDir))
                    {
                        AssetDatabase.CreateFolder(AdsManagerSettingsDir, "Resources");
                    }

            instance = AdsManagerSettings.Instance;

                    if (instance == null)
                    {
                        instance = ScriptableObject.CreateInstance<AdsManagerSettings>();
                        AssetDatabase.CreateAsset(instance, AdsManagerSettingsFile);
                    }
                }

       

        Selection.activeObject = AdsManagerSettings.Instance;
        }

        public override void OnInspectorGUI()
        {
            EditorGUILayout.LabelField("Test Mode", EditorStyles.boldLabel);
            AdsManagerSettings.Instance.IsTestModeEnabled =
                    EditorGUILayout.Toggle(new GUIContent("Enabled"),
                            AdsManagerSettings.Instance.IsTestModeEnabled);
            if (AdsManagerSettings.Instance.IsTestModeEnabled)
            {
                EditorGUILayout.HelpBox(
                        "When enabled, AdsManager will show test ads. Disable it before publishing to the store.",
                        MessageType.Info);
            }

            EditorGUILayout.Separator();
            EditorGUILayout.Separator();

            EditorGUILayout.LabelField("Unity Ads", EditorStyles.boldLabel);
            AdsManagerSettings.Instance.IsUnityAdsEnabled =
                    EditorGUILayout.Toggle(new GUIContent("Enabled"),
                            AdsManagerSettings.Instance.IsUnityAdsEnabled);

            EditorGUILayout.Separator();
            EditorGUI.BeginDisabledGroup(!AdsManagerSettings.Instance.IsUnityAdsEnabled);

            //EditorGUILayout.LabelField("UnityAds");

            AdsManagerSettings.Instance.UnityAdsId =
                    EditorGUILayout.TextField("UnityAds ID", AdsManagerSettings.Instance.UnityAdsId);

            EditorGUI.EndDisabledGroup();

            EditorGUILayout.Separator();
            EditorGUILayout.Separator();

            EditorGUILayout.LabelField("Google AdMob", EditorStyles.boldLabel);
            AdsManagerSettings.Instance.IsAdMobEnabled =
                    EditorGUILayout.Toggle(new GUIContent("Enabled"),
                            AdsManagerSettings.Instance.IsAdMobEnabled);

            EditorGUILayout.Separator();

            EditorGUI.BeginDisabledGroup(!AdsManagerSettings.Instance.IsAdMobEnabled);

            EditorGUILayout.LabelField("AdMob App ID");

            AdsManagerSettings.Instance.AdMobAndroidAppId =
                    EditorGUILayout.TextField("Android",
                            AdsManagerSettings.Instance.AdMobAndroidAppId);

            AdsManagerSettings.Instance.AdMobIOSAppId =
                    EditorGUILayout.TextField("iOS", AdsManagerSettings.Instance.AdMobIOSAppId);

            EditorGUILayout.Separator();

            AdsManagerSettings.Instance.IsLoadingInterstitialEnabled =
                    EditorGUILayout.Toggle(new GUIContent("Loading Interstitial Enabled"),
                            AdsManagerSettings.Instance.IsLoadingInterstitialEnabled);
            EditorGUI.BeginDisabledGroup(!AdsManagerSettings.Instance.IsLoadingInterstitialEnabled);

            AdsManagerSettings.Instance.LoadingInterstitalId =
                    EditorGUILayout.TextField("Loading Interstial Id",
                            AdsManagerSettings.Instance.LoadingInterstitalId);
            EditorGUI.EndDisabledGroup();

            EditorGUILayout.Separator();

            AdsManagerSettings.Instance.InterstitalId =
                    EditorGUILayout.TextField("Other Interstial Id", AdsManagerSettings.Instance.InterstitalId);

        EditorGUILayout.Separator();
        EditorGUILayout.Separator();


        AdsManagerSettings.Instance.IsBannerEnabled =
                    EditorGUILayout.Toggle(new GUIContent("Banner Enabled"),
                            AdsManagerSettings.Instance.IsBannerEnabled);
            EditorGUI.BeginDisabledGroup(!AdsManagerSettings.Instance.IsBannerEnabled);

            AdsManagerSettings.Instance.BannerId = EditorGUILayout.TextField("Banner Id", AdsManagerSettings.Instance.BannerId);
        AdsManagerSettings.Instance.BannerPosition = (AdPosition)EditorGUILayout.EnumPopup("Banner Position", AdsManagerSettings.Instance.BannerPosition);
        AdsManagerSettings.Instance.BannerSize = (BannerSize)EditorGUILayout.EnumPopup("Banner Size", AdsManagerSettings.Instance.BannerSize);
        EditorGUI.EndDisabledGroup();

            EditorGUILayout.Separator();


            AdsManagerSettings.Instance.DelayAppMeasurementInit =
                    EditorGUILayout.Toggle(new GUIContent("Delay app measurement"),
                    AdsManagerSettings.Instance.DelayAppMeasurementInit);
            if (AdsManagerSettings.Instance.DelayAppMeasurementInit) {
                    EditorGUILayout.HelpBox(
                            "Delays app measurement until you explicitly initialize the Mobile Ads SDK or load an ad.",
                            MessageType.Info);
            }
            EditorGUI.EndDisabledGroup();

            if (GUI.changed)
            {
                OnSettingsChanged();
            }
        }

    private void OnSettingsChanged()
    {
        EditorUtility.SetDirty((AdsManagerSettings)target);
        AssetDatabase.SaveAssets();
    }
    }
//}
