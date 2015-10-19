using DT;
using System.Collections;
using UnityEngine;

namespace DT {
	[ExecuteInEditMode]
	public class ChromaticAberrationEffect : ImageEffectInstance, ICustomEditor {
		// Example Tweening 
		// this.AberrationOffsetTween(from: 1.0f, to: 0.0f, duration: 0.3f)
		// 	.SetEaseType(EaseType.ElasticOut)
		// 	.Start();
		
		// PRAGMA MARK - INTERFACE
		public float AberrationOffset {
			get { return _aberrationOffset; }
			set { 
				_aberrationOffset = value; 
				this.UpdateMaterial();
			}
		}
		
		// PRAGMA MARK - INTERNAL
		[SerializeField, Range(0.0f, 3.0f)]
		protected float _aberrationOffset = 1.0f;
		[SerializeField, LocalVectorInspectable]
		protected Vector2 _aberrationDirection = new Vector2(1.0f, 1.0f);
		[SerializeField]
		protected Color _forwardChannelColor = Color.blue;
		[SerializeField]
		protected Color _backChannelColor = Color.red;
		
		protected override string ShaderName() {
			return "ImageEffects/ChromaticAberration";
		}
		
		protected override void UpdateMaterial() {
			this.MaterialInstance.SetFloat("_AberrationOffset", _aberrationOffset);
			
			Vector2 computedAberrationDirection = _aberrationDirection.normalized;
			this.MaterialInstance.SetFloat("_OffsetX", computedAberrationDirection.x);
			this.MaterialInstance.SetFloat("_OffsetY", computedAberrationDirection.y);
			this.MaterialInstance.SetColor("_ForwardChannelColor", _forwardChannelColor);
			this.MaterialInstance.SetColor("_BackChannelColor", _backChannelColor);
		}
	}
}