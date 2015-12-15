using DT;
using System.Collections;
using System.Collections.Generic;
﻿using UnityEngine;

namespace DT {
	[ExecuteInEditMode]
	public class VertexSpringShaderRendererInstance2D : RendererInstance2D {
		// PRAGMA MARK - Public Interface 
		public Vector3 SpringValue {
			protected get; set;
		}
		
		public float SmallestVertexY {
			protected get; set;
		}
		
		public float Height {
			protected get; set;
		}
		
		// PRAGMA MARK - Internal
		[Header("Shader Properties")]
		[SerializeField]
		protected Color _color = Color.white;
		
		protected void Update() {
			this.UpdateMaterial();
		}
		
		protected override string ShaderName() {
			return "Custom/Surface-VertexSpringShader";
		}
		
		protected override void UpdateMaterial() {
			base.UpdateMaterial();
			
			this.MaterialInstance.SetColor("_Color", _color);
			this.MaterialInstance.SetFloat("_SmallestVertexY", this.SmallestVertexY);
			this.MaterialInstance.SetFloat("_Height", this.Height);
			this.MaterialInstance.SetFloat("_SpringValueX", this.SpringValue.x);
			this.MaterialInstance.SetFloat("_SpringValueZ", this.SpringValue.z);
		}
	}
}