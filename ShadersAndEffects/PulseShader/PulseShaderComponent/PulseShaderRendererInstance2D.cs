using DT;
using System.Collections;
﻿using UnityEngine;

namespace DT {
	[ExecuteInEditMode]
	public class PulseShaderRendererInstance2D : RendererInstance2D {
		// PRAGMA MARK - Public Interface 
		public bool Pulsing {
			get { return _pulsing; }
			set {
				// if pulsing value changes
				if (value != _pulsing) {
					_pulseTime = 0.0f;
				}
				_pulsing = value;
				this.UpdateMaterial();
			}
		}
		
		public float PulseSpeed {
			get { return _pulseSpeed; }
			set { 
				_pulseSpeed = value;
				this.UpdateMaterial();
			}
		}
		
		public Color PulseColor {
			get { return _pulseColor; }
			set { 
				_pulseColor = value;
				this.UpdateMaterial();
			}
		}
		
		public float PulseColorPercentLerp {
			get { return _pulseColorPercentLerp; }
			set { 
				_pulseColorPercentLerp = value;
				this.UpdateMaterial();
			}
		}
		
		// PRAGMA MARK - Internal
		[SerializeField]
		protected bool _pulsing;
		[SerializeField]
		protected float _pulseSpeed;
		
		[SerializeField]
		protected Color _pulseColor;
		[SerializeField]
		protected float _pulseColorPercentLerp;
		
		[SerializeField, ReadOnly]
		protected float _pulseTime = 0.0f;
		[SerializeField, ReadOnly]
		protected float _pulseValue;
		
		protected override string ShaderName() {
			return "Sprites/Default-PulseShader";
		}
		
		protected void Update() {
			if (_pulsing) {
				this.UpdatePulseValue();
			}
		}
		
		protected void UpdatePulseValue() {
			// pulse time will add 1.0 in _pulseSpeed seconds
			_pulseTime += Time.deltaTime / _pulseSpeed;
			// pulse value will complete a period (0 -- 1 -- 0) every 1 unity of pulse time
			_pulseValue = ((-Mathf.Cos(_pulseTime * 2.0f * Mathf.PI) / 2.0f) + 0.5f);
			
			this.UpdateMaterial();
		}
		
		protected override void UpdateMaterial() {
			base.UpdateMaterial();
			
			this.MaterialInstance.SetInt("_Pulsing", (_pulsing) ? 1 : 0); 
			this.MaterialInstance.SetFloat("_PulseValue", _pulseValue); 
			this.MaterialInstance.SetColor("_PulseColor", _pulseColor);
			this.MaterialInstance.SetFloat("_PulseColorPercentLerp", _pulseColorPercentLerp);
		}
	}
}