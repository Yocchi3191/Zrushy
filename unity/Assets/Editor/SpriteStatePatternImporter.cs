using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using UnityEditor;
using UnityEngine;
using Zrushy.Core.Domain.Interactions.ValueObject;
using Zrushy.Core.Presentation.Unity;

public class SpriteStatePatternImporter
{
    private const string ResourceRoot = "Heroin";

    [MenuItem("Tools/Import SpriteStatePattern")]
    public static void Import()
    {
        string jsonPath = EditorUtility.OpenFilePanel("JSONを選択", "", "json");
        if (string.IsNullOrEmpty(jsonPath)) return;

        var data = JsonConvert.DeserializeObject<SpriteStatePatternData>(File.ReadAllText(jsonPath));

        var pattern = ScriptableObject.CreateInstance<SpriteStatePattern>();
        pattern.layerID = data.layerID;
        pattern.initialState = LoadSprite(data.layerID, data.initialState);
        if (pattern.initialState == null) return;

        pattern.transitions = new List<StateTransition>();
        foreach (var t in data.transitions)
        {
            var from = LoadSprite(data.layerID, t.from);
            var to = LoadSprite(data.layerID, t.to);
            if (from == null || to == null) return;

            pattern.transitions.Add(new StateTransition
            {
                fromState = from,
                requiredType = Enum.Parse<InteractionType>(t.type),
                requiredDirection = Enum.Parse<CardinalDirection>(t.direction),
                toState = to
            });
        }

        string savePath = EditorUtility.SaveFilePanelInProject(
            "保存先を選択",
            $"{data.layerID}_SpriteStatePattern",
            "asset",
            ""
        );
        if (string.IsNullOrEmpty(savePath)) return;

        AssetDatabase.CreateAsset(pattern, savePath);
        AssetDatabase.SaveAssets();
        Debug.Log($"SpriteStatePattern を作成しました: {savePath}");
    }

    private static Sprite LoadSprite(string layerID, string stateName)
    {
        string path = $"Assets/Resources/{ResourceRoot}/{layerID}/{stateName}.png";
        var sprite = AssetDatabase.LoadAssetAtPath<Sprite>(path);
        if (sprite == null)
            Debug.LogError($"Sprite が見つかりません: {path}");
        return sprite;
    }

    private class SpriteStatePatternData
    {
        public string layerID;
        public string initialState;
        public List<TransitionData> transitions;
    }

    private class TransitionData
    {
        public string from;
        public string type;
        public string direction;
        public string to;
    }
}
