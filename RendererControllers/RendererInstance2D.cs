using DT;
using System.Collections;
﻿using UnityEngine;

namespace DT {
	public class RendererInstance2D : RendererInstance {
		// PRAGMA MARK - Internal
		[SerializeField]
		protected Texture2D _mainTexture;
		
		protected override void UpdateMaterial() {
			this.MaterialInstance.SetTexture("_MainTex", _mainTexture); 
		}
	}
}