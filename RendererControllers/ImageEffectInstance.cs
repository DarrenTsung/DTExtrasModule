using DT;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
﻿using UnityEngine;

namespace DT {
	[ExecuteInEditMode]
	public class ImageEffectInstance : MaterialInstanceBase {
		// PRAGMA MARK - Internal
		protected override void Awake() {
			base.Awake();
			if (!SystemInfo.supportsImageEffects) {
				// disable script if image effects aren't supported
				this.enabled = false;
				return;
			}
		}
		
		protected void OnRenderImage(RenderTexture sourceTexture, RenderTexture destTexture) {
			Graphics.Blit(sourceTexture, destTexture, this.MaterialInstance);
		}
	}
}