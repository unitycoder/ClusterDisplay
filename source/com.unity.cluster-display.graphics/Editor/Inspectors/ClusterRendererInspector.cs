﻿using System;
using UnityEngine;
using UnityEditor;

namespace Unity.ClusterDisplay.Graphics.Inspectors
{
    [CustomEditor(typeof(ClusterRenderer))]
    class ClusterRendererInspector : UnityEditor.Editor
    {
        public override void OnInspectorGUI()
        {
            using (var check = new EditorGUI.ChangeCheckScope())
            {
                var adapter = target as ClusterRenderer;
             
                var settings = adapter.Settings;
                settings.GridSize = EditorGUILayout.Vector2IntField(Labels.GetGUIContent(Labels.Field.GridSize), settings.GridSize);
                settings.PhysicalScreenSize = EditorGUILayout.Vector2Field(Labels.GetGUIContent(Labels.Field.PhysicalScreenSize), settings.PhysicalScreenSize);
                settings.Bezel = EditorGUILayout.Vector2Field(Labels.GetGUIContent(Labels.Field.Bezel), settings.Bezel);
                settings.OverscanInPixels = EditorGUILayout.IntSlider(Labels.GetGUIContent(Labels.Field.Overscan), settings.OverscanInPixels, 0, 256);

                adapter.Debug = EditorGUILayout.Toggle(Labels.GetGUIContent(Labels.Field.Debug), adapter.Debug);
                
                if (adapter.Debug)
                    EditDebugSettings(adapter.DebugSettings);
                
                if (check.changed)
                    EditorUtility.SetDirty(adapter);
            }
        }
     
        static void EditDebugSettings(ClusterRendererDebugSettings settings)
        {
            //settings.TileIndexOverride = EditorGUILayout.IntField("Tile Index Override", settings.TileIndexOverride);
            settings.TileIndexOverride = EditorGUILayout.IntField(Labels.GetGUIContent(Labels.Field.TileIndexOverride), settings.TileIndexOverride);
            settings.EnableKeyword = EditorGUILayout.Toggle(Labels.GetGUIContent(Labels.Field.Keyword), settings.EnableKeyword);
            settings.EnableStitcher = EditorGUILayout.Toggle(Labels.GetGUIContent(Labels.Field.Stitcher), settings.EnableStitcher);
            settings.UseDebugViewportSubsection = EditorGUILayout.Toggle(Labels.GetGUIContent(Labels.Field.DebugViewportSubsection), settings.UseDebugViewportSubsection);

            // Let user manipulate viewport directly instead of inferring it from tile index.
            if (settings.UseDebugViewportSubsection)
            {
                EditorGUILayout.LabelField("Viewport Section");

                var rect = settings.ViewportSubsection;
                float xMin = rect.xMin;
                float xMax = rect.xMax;
                float yMin = rect.yMin;
                float yMax = rect.yMax;

                xMin = EditorGUILayout.Slider("xMin", xMin, 0, 1);
                xMax = EditorGUILayout.Slider("xMax", xMax, 0, 1);
                yMin = EditorGUILayout.Slider("yMin", yMin, 0, 1);
                yMax = EditorGUILayout.Slider("yMax", yMax, 0, 1);
                settings.ViewportSubsection = Rect.MinMaxRect(xMin, yMin, xMax, yMax);
            }

            EditorGUILayout.LabelField(Labels.GetGUIContent(Labels.Field.ScaleBiasOffset));
            var offset = settings.ScaleBiasTexOffset;
            offset.x = EditorGUILayout.Slider("x", offset.x, -1, 1);
            offset.y = EditorGUILayout.Slider("y", offset.y, -1, 1);
            settings.ScaleBiasTexOffset = offset;
        }
    }
}