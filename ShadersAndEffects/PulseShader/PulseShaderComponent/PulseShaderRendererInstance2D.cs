using DT;
using System.Collections;
﻿using UnityEngine;

namespace DT {
	[ExecuteInEditMode]
	public class PulseShaderRendererInstance2D : RendererInstance2D {
		// PRAGMA MARK - Interface 
		public bool Pulsing {
			get { return _pulsing; }
			set {
				_pulsing = value;
				this.UpdateMaterial();
			}
		}
		
		// PRAGMA MARK - Internal
		[SerializeField]
		public bool _pulsing;
		[SerializeField]
		protected float _pulseSpeed;
		
		[SerializeField]
		protected Color _pulseColor;
		[SerializeField]
		protected float _pulseColorPercentLerp;
		
		protected override string ShaderName() {
			return "Sprites/Default-PulseShader";
		}
		
		protected override void UpdateMaterial() {
			base.UpdateMaterial();
			
			this.MaterialInstance.SetInt("_Pulsing", (_pulsing) ? 1 : 0); 
			this.MaterialInstance.SetFloat("_PulseSpeed", _pulseSpeed); 
			this.MaterialInstance.SetColor("_PulseColor", _pulseColor);
			this.MaterialInstance.SetFloat("_PulseColorPercentLerp", _pulseColorPercentLerp);
		}
	}
}