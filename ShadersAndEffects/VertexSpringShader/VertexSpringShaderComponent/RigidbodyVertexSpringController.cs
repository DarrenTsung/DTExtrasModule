using DT;
﻿using DT.GameEngine;
using System.Collections;
using UnityEngine;

namespace DT {
  public class RigidbodyVertexSpringController : MonoBehaviour {
    // PRAGMA MARK - Internal 
  	[SerializeField]
  	protected float _velocitySpringTargetFactor = 0.3f;
  	
  	protected Rigidbody _rigidbody;
  	protected IVertexSpring _vertexSpring;
  	
  	protected void Awake() {
  		_rigidbody = this.GetComponent<Rigidbody>();
  		_vertexSpring = this.GetRequiredComponentInChildren<IVertexSpring>();
  	}
  	
  	protected void Update() {
  		_vertexSpring.SetSpringTarget("RigidbodyVertexSpring", -_rigidbody.velocity * _velocitySpringTargetFactor);
  	}
  }
}