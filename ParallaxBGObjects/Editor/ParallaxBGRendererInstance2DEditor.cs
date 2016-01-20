using DT;
using System.Collections;
using System.Collections.Generic;
﻿using UnityEditor;
using UnityEditor.AnimatedValues;
﻿using UnityEngine;

namespace DT.Extras {
	[CustomEditor(typeof(ParallaxBGRendererInstance2D))]
	public class ParallaxBGRendererInstance2DEditor : DTEditor<ParallaxBGRendererInstance2D> {
		// PRAGMA MARK - Internal
		protected AnimBool _showDefaultInspector;

		protected override void OnEnable() {
			base.OnEnable();

			_showDefaultInspector = new AnimBool(false);
			_showDefaultInspector.valueChanged.AddListener(Repaint);
		}

		public override void OnInspectorGUI() {
			_showDefaultInspector.target = EditorGUILayout.ToggleLeft("Show default inspector", _showDefaultInspector.target);

			//Extra block that can be toggled on and off.
			if (EditorGUILayout.BeginFadeGroup(_showDefaultInspector.faded)) {
				EditorGUI.indentLevel++;
				this.DrawDefaultInspector();
				EditorGUI.indentLevel--;
			}
			EditorGUILayout.EndFadeGroup();

			// DEPTH
			_object.Depth = EditorGUILayout.IntSlider("Depth", _object.Depth, 0, _object.MaxDepth);
			_object.MaxDepth = EditorGUILayout.IntField("Max Depth", _object.MaxDepth);

			float relativeDepth = _object.RelativeDepth;
			EditorGUILayout.LabelField("Relative Depth (Used for calculations): " + relativeDepth);

			// SORTING
			EditorGUILayoutExtensions.Header("Sorting");
			_object.DepthOffset = EditorGUILayout.IntField("Depth Offset", _object.DepthOffset);
			EditorGUILayout.LabelField("Computed Sorting Order: " + _object.SortingOrder);

			// PROPERTIES
			EditorGUILayoutExtensions.Header("Properties");

			_object.ColorToBlendTo = EditorGUILayout.ColorField("Color To Blend To", _object.ColorToBlendTo);

			GUIContent colorBlendContent = new GUIContent("Color Blend Scale", "Lerp(SpriteColor, BackgroundColor, RelativeDepth * ColorBlendScale)");
			_object.ColorBlendScale = EditorGUILayout.Slider(colorBlendContent, _object.ColorBlendScale, 0.0f, 1.0f);

			float backgroundPercentage = (relativeDepth * _object.ColorBlendScale);
			float originalSpritePercentage = 1.0f - backgroundPercentage;
			EditorGUILayout.LabelField("Color = " + originalSpritePercentage.ToPercentageString() + " Original Sprite  |  " + backgroundPercentage.ToPercentageString() + " Color To Blend To");
			EditorGUILayout.Space();

			GUIContent sizeReductionContent = new GUIContent("Size Reduction Scale", "Scale = 1.0f - (SizeReductionScale * RelativeDepth)");
			_object.SizeReductionScale = EditorGUILayout.Slider(sizeReductionContent, _object.SizeReductionScale, 0.0f, 1.0f);

			float computedSize = 1.0f - (relativeDepth * _object.SizeReductionScale);
			EditorGUILayout.LabelField("Size = " + computedSize.ToPercentageString());

			if (GUILayout.Button("Recreate Material")) {
				_object.RecreateMaterial();
			}

			base.OnInspectorGUI();
		}
	}
}