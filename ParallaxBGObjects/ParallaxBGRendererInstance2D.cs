using DT;
using System;
using System.Collections;
﻿using UnityEngine;

namespace DT.Extras {
	[ExecuteInEditMode]
	public class ParallaxBGRendererInstance2D : RendererInstance2D {
		// PRAGMA MARK - Public Interface
		public int Depth {
			get { return _depth; }
			set {
				_depth = value;
				transform.position = transform.position.SetZ(_depth);
        this._computedRelativeDepth = null;
				this.RecomputeSortingOrder();
			}
		}

		public int DepthOffset {
			get { return _depthOffset; }
			set {
				_depthOffset = value;
				this.RecomputeSortingOrder();
			}
		}

		public int SortingOrder {
			get { return (-_depth * 100) + _depthOffset; }
		}

		public float RelativeDepth {
			get {
        if (this._computedRelativeDepth == null) {
          this._computedRelativeDepth = Mathf.Clamp((float)this.Depth / (float)this.MaxDepth, 0.0f, 1.0f);
        }
        return (float)this._computedRelativeDepth;
      }
		}

		public int MaxDepth {
			get { return _maxDepth; }
			set {
				_maxDepth = value;
			}
		}

		public float ColorBlendScale {
			get { return _colorBlendScale; }
			set {
				_colorBlendScale = value;
			}
		}

		public Color ColorToBlendTo {
			get { return _colorToBlendTo; }
			set {
				_colorToBlendTo = value;
			}
		}

		public float SizeReductionScale {
			get { return _sizeReductionScale; }
			set {
				_sizeReductionScale = value;
			}
		}

		// PRAGMA MARK - Internal
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

    protected float? _computedRelativeDepth;

		protected override string ShaderName() {
			return "Sprites/Default-BackgroundArt";
		}

		protected override void UpdateMaterial() {
			base.UpdateMaterial();

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