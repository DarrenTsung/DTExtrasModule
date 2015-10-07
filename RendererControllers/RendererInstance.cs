using DT;
using System.Collections;
﻿using UnityEngine;

namespace DT {
	public class RendererInstance : MonoBehaviour {
		// PRAGMA MARK - INTERNAL
		protected Renderer[] Renderers {
			get { 
				if (_renderers == null || _renderers.Length == 0) {
					_renderers = this.GetComponentsInChildren<Renderer>(); 
				}
				return _renderers;
			}
		}
		
		protected Material MaterialInstance {
			get {
				if (_material == null) {
					_material = new Material(Shader.Find(this.ShaderName()));
					_material.hideFlags = HideFlags.HideAndDontSave;
					foreach (Renderer r in this.Renderers) {
						r.sharedMaterial = _material;
					}
				}
				return _material;
			}
		}
		
		protected virtual void OnDisable() {
			GameObject.DestroyImmediate(_material);
		}
		
		protected virtual string ShaderName() {
			return "Sprites/Default";
		}
		
		[SerializeField]
		protected Material _material;
		[SerializeField]
		protected Renderer[] _renderers;
	}
}