using DT;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
﻿using UnityEngine;

namespace DT {
	[ExecuteInEditMode]
	public class RendererInstance : MonoBehaviour {
		// PRAGMA MARK - INTERNAL
		protected Renderer[] Renderers {
			get { 
				return this.GetComponentsInChildren<Renderer>(); 
			}
		}
		
		protected Material MaterialInstance {
			get {
				if (_material == null) {
					_material = new Material(Shader.Find(this.ShaderName()));
					_material.hideFlags = HideFlags.HideAndDontSave;
					foreach (Renderer r in this.Renderers) {
						r.sharedMaterial = this.MaterialInstance;
					}
				}
				return _material;
			}
		}
		
		[SerializeField]
		protected Material _material;
		
		protected const float _updateMaterialSpeed = 0.2f;
		
		protected virtual void OnDisable() {
			GameObject.DestroyImmediate(_material);
		}
		
		protected virtual void Start() {
			this.StartCoroutine(this.UpdateMaterialOnLoop());
		}
		
		protected virtual void Update() {
			if (!Application.isPlaying) {
				this.UpdateMaterial();
			}
		}
		
		protected IEnumerator UpdateMaterialOnLoop() {
			while (true) {
				this.UpdateMaterial();
				yield return new WaitForSeconds(_updateMaterialSpeed);
			}
		}
		
		protected virtual void UpdateMaterial() {
			// do nothing in base
		}
		
		protected virtual string ShaderName() {
			return "Sprites/Default";
		}
	}
}