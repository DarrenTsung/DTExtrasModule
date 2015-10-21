using DT;
using System.Collections;
﻿using UnityEngine;

namespace DT {
	[ExecuteInEditMode]
	public class BurnShaderRendererInstance2D : RendererInstance2D {
		// PRAGMA MARK - Internal
		[SerializeField]
		protected Texture2D _dissolveMap;
		[SerializeField]
		protected float _dissolveAmount;
		[SerializeField]
		protected Texture2D _burnRampTexture;
		[SerializeField]
		protected float _burnRampScale;
		
		protected override string ShaderName() {
			return "Sprites/Default-BurnShader";
		}
		
		protected override void UpdateMaterial() {
			base.UpdateMaterial();
			
			this.MaterialInstance.SetTexture("_DissolveMap", _dissolveMap); 
			this.MaterialInstance.SetFloat("_DissolveAmount", _dissolveAmount); 
			this.MaterialInstance.SetTexture("_BurnRampTex", _burnRampTexture); 
			this.MaterialInstance.SetFloat("_BurnRampScale", _burnRampScale); 
		}
	}
}