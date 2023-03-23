using UnityEngine;
using UnityEditor;
using System.Reflection;

namespace AICommand
{

    public sealed class AICommandWindow : EditorWindow
    {
        #region Script generator

        void Reset()
        {
            _prompt = "請介紹Unity";
            _ans = "No msg.";
        }

        void RunChatGenerator()
        {
            var msg = OpenAIUtil.InvokeChat(_prompt);
            _ans = "AI: " + msg;
            Debug.Log("AI: " + msg);
        }

        #endregion

        #region Editor GUI

        string _prompt = "請介紹Unity";
        string _ans = "No msg.";

        //
        const string ApiKeyErrorText =
          "API Key hasn't been set. Please check the project settings " +
          "(Edit > Project Settings > AI Command > API Key).";

        bool IsApiKeyOk
          => !string.IsNullOrEmpty(AICommandSettings.instance.apiKey);

        [MenuItem("Window/cardooo/AI Command")]
        static void Init() => GetWindow<AICommandWindow>(true, "AI Command");

        void OnGUI()
        {
            if (IsApiKeyOk)
            {
                EditorGUILayout.LabelField("Prompt");
                _prompt = EditorGUILayout.TextArea(_prompt, GUILayout.ExpandHeight(true));
                EditorGUILayout.LabelField("AI Ans:");
                EditorGUILayout.TextArea(_ans, GUILayout.ExpandHeight(true));
                if (GUILayout.Button("Reset")) Reset();
                if (GUILayout.Button("Run Chat")) RunChatGenerator();
            }
            else
            {
                EditorGUILayout.HelpBox(ApiKeyErrorText, MessageType.Error);
            }
        }

        #endregion
    }

} // namespace AICommand
