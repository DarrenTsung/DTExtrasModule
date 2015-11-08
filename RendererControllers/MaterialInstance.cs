using DT;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
﻿using UnityEngine;

namespace DT {
	[ExecuteInEditMode]
	public class MaterialInstanceBase : MonoBehaviour {
		// PRAGMA MARK - Internal
		protected Material MaterialInstance {
			get {
				if (_material == null) {
					_material = new Material(Shader.Find(this.ShaderName()));
					_material.hideFlags = HideFlags.HideAndDontSave;
					this.OnCreatedMaterial(_material);
				}
				return _material;
			}
		}
		
		protected virtual void OnCreatedMaterial(Material mat) {
			// do nothing
		}
		
		[SerializeField]
		protected Material _material;
		
		protected virtual void Awake() {
			this.DestroyMaterial();
			this.RegisterNotifications();
		}
		
		protected virtual void Start() {
			this.UpdateMaterial();
		}
		
		protected virtual void OnDisable() {
			this.DestroyMaterial();
			this.RemoveNotifications();
		}
		
		protected virtual void OnValidate() {
			this.UpdateMaterial();
		}
		
		protected void DestroyMaterial() {
			if (_material != null) {
				GameObject.DestroyImmediate(_material);
			}
			_material = null;
		}
		
		protected virtual void RegisterNotifications() {
			// do nothing
		}
		
		protected virtual void RemoveNotifications() {
			// do nothing
		}
		
		protected virtual void UpdateMaterial() {
			// do nothing in base
		}
		
		protected virtual string ShaderName() {
			return "Sprites/Default";
		}
	}
}