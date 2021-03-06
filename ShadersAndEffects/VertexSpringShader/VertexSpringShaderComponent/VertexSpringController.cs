using DT;
using System.Collections;
using System.Collections.Generic;
﻿using UnityEngine;

namespace DT {
	[ExecuteInEditMode]
	public class VertexSpringController : MonoBehaviour, IVertexSpring {
		// PRAGMA MARK - IVertexSpring
		public void SetSpringTarget(string name, Vector3 target) {
			_springTargets[name] = target;
		}
		
		public void RemoveSpringTarget(string name) {
			_springTargets.Remove(name);
		}
		
		
		// PRAGMA MARK - Internal
		[Header("Synced Shader Properties")]
		[SerializeField]
		protected float _smallestVertexY = 0.0f;
		[SerializeField]
		protected float _height = 1.0f;
		
		[Header("Spring Properties")]
		[SerializeField, Range(0.01f, 1.0f)]
		protected float _dampingRatio = 0.2f;
		[SerializeField]
		protected float _angularFrequencyPerPI = 4;
		[SerializeField]
		protected Vector3 _springValue;
		
		protected Dictionary<string, Vector3> _springTargets;
		protected Vector3 _springVelocity;
    
    private VertexSpringShaderRendererInstance2D[] _vertexSpringRenderers;
    protected VertexSpringShaderRendererInstance2D[] VertexSpringRenderers {
      get {
        if (_vertexSpringRenderers == null) {
          _vertexSpringRenderers = this.GetComponentsInChildren<VertexSpringShaderRendererInstance2D>();
        }
        return _vertexSpringRenderers;
      }
    }
		
		protected void Awake() {
			_springTargets = new Dictionary<string, Vector3>();
		}
		
		protected void Update() {
			if (Application.isPlaying) {
				Vector3 springTarget = this.ComputeSpringTarget();
				_springValue = Easers.StableSpring(_springValue, springTarget, ref _springVelocity, _dampingRatio, _angularFrequencyPerPI * Mathf.PI);
				this.UpdateChildren();
			}
		}
		
		protected void OnValidate() {
			this.UpdateChildren();
		}
		
		protected Vector3 ComputeSpringTarget() {
			if (_springTargets.Count > 0) {
				Vector3 springTarget = Vector3.zero;
				
				foreach (KeyValuePair<string, Vector3> pair in _springTargets) {
					Vector3 target = pair.Value;
					springTarget += target;
				}
				
				return springTarget / _springTargets.Count;
			} else {
				return Vector3.zero;
			}
		}
    
    protected void UpdateChildren() {
      foreach (VertexSpringShaderRendererInstance2D vertexSpring in this.VertexSpringRenderers) {
        vertexSpring.SpringValue = _springValue;
        vertexSpring.SmallestVertexY = _smallestVertexY;
        vertexSpring.Height = _height;
      }
    }
	}
}