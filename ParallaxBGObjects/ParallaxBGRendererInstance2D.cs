using DT;
using System;
using System.Collections;
﻿using UnityEditor;
﻿using UnityEngine;

namespace DT.ParallaxBackgrounds {
	[ExecuteInEditMode]
	public class ParallaxBGRendererInstance2D : RendererInstance2D {
		// PRAGMA MARK - INTERFACE
		public int Depth {
			get { return _depth; }
			set { 
				Undo.RecordObject(this, "ChangeDepth");
				_depth = value; 
				transform.position = transform.position.SetZ(_depth);
				this.RecomputeSortingOrder();
			}
		}
		
		public int DepthOffset {
			get { return _depthOffset; }
			set { 
				Undo.RecordObject(this, "ChangeDepthOffset");
				_depthOffset = value; 
				this.RecomputeSortingOrder();
			}
		}
		
		public int SortingOrder {
			get { return (-_depth * 100) + _depthOffset; }
		}
		
		public float RelativeDepth {
			get { return Mathf.Clamp((float)this.Depth / (float)this.MaxDepth, 0.0f, 1.0f); }
		}
		
		public int MaxDepth {
			get { return _maxDepth; }
			set {
				Undo.RecordObject(this, "ChangeMaxDepth");
				_maxDepth = value;
			}
		}
		
		public float ColorBlendScale {
			get { return _colorBlendScale; }
			set {
				Undo.RecordObject(this, "ChangeColorBlendScale");
				_colorBlendScale = value;
			}
		}
		
		public Color ColorToBlendTo {
			get { return _colorToBlendTo; }
			set {
				Undo.RecordObject(this, "ChangeColorToBlendTo");
				_colorToBlendTo = value;
			}
		}
		
		public float SizeReductionScale {
			get { return _sizeReductionScale; }
			set { 
				Undo.RecordObject(this, "ChangeSizeReductionScale");
				_sizeReductionScale = value; 
			}
		}
		
		// PRAGMA MARK - INTERNAL
		[SerializeField]
		protected int _depth = 0;
		[SerializeField]
		protected int _maxDepth = 100;
		
		[SerializeField]
		protected int _depthOffset = 0;
		
		[SerializeField]
		protected float _colorBlendScale = 1.0f;
		[SerializeField]
		protected Color _colorToBlendTo = Color.white;
		[SerializeField]
		protected float _sizeReductionScale = 0.5f;
		
		protected override string ShaderName() {
			return "Sprites/Default-BackgroundArt";
		}
		
		protected override void UpdateMaterial() {
		  this.MaterialInstance.SetFloat("_MaxDepth", _maxDepth);
			this.MaterialInstance.SetFloat("_ColorBlendScale", _colorBlendScale);
			this.MaterialInstance.SetColor("_ColorToBlendTo", _colorToBlendTo);
		}
		
		protected void LateUpdate() {
			Vector3 cameraPosition = Camera.main.transform.position;
			
			this.SetChildren(cameraPosition * this.RelativeDepth, (1.0f - _sizeReductionScale * this.RelativeDepth));
		}
		
		protected void OnDrawGizmos() {
			if (!Application.isPlaying) {
				this.LateUpdate();
			}
		}
		
		protected void SetChildren(Vector2 position, float size) {
			foreach (Transform child in transform) {
				child.transform.localPosition = position;
				child.transform.localScale = child.transform.localScale.SetXY(size, size);
			}
		}
		
		protected void RecomputeSortingOrder() {
			this.DoActionOnChildSpriteRenderers(renderer => {
					renderer.sortingOrder = this.SortingOrder;
				});
		}
		
		protected void DoActionOnChildSpriteRenderers(Action<SpriteRenderer> action) {
			SpriteRenderer[] renderers = this.GetComponentsInChildren<SpriteRenderer>();
			foreach (SpriteRenderer renderer in renderers) {
				action(renderer);
			}
		}
	}
}