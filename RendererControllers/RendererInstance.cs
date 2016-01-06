using DT;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
﻿using UnityEngine;

namespace DT {
	[ExecuteInEditMode]
	public class RendererInstance : MaterialInstanceBase {
		// PRAGMA MARK - Internal
		[SerializeField]
		protected bool _affectChildren = false;
		
		protected Renderer[] Renderers {
			get { 
				if (_affectChildren) {
					return this.GetRenderersWithoutInstancesInChildren();
				} else {
					Renderer r = this.GetComponent<Renderer>();
					if (r != null) {
						return new Renderer[] { r };
					} else {
						return new Renderer[0];
					}
				}
			}
		}
		
		protected override void OnCreatedMaterial(Material mat) {
			base.OnCreatedMaterial(mat);
			
			foreach (Renderer r in this.Renderers) {
				r.sharedMaterial = mat;
			}
		}
		
		protected override void UpdateMaterial() {
			base.UpdateMaterial();
			
			foreach (Renderer r in this.Renderers) {
				r.sharedMaterial = this.MaterialInstance;
			}
		}
		
		private Renderer[] GetRenderersWithoutInstancesInChildren() {
			List<Renderer> renderers = new List<Renderer>();
			
			Queue<Transform> transformQueue = new Queue<Transform>();
			// enqueue children first
			foreach (Transform child in transform) {
				transformQueue.Enqueue(child);
			}
			
			while (transformQueue.Count > 0) {
				Transform t = transformQueue.Dequeue();
				GameObject g = t.gameObject;
				RendererInstance instance = g.GetComponent<RendererInstance>();
				if (instance == null) {
					Renderer r = g.GetComponent<Renderer>();
					if (r != null) {
						renderers.Add(r);
					}
					foreach (Transform child in t) {
						transformQueue.Enqueue(child);
					}
				}
			}
			
			return renderers.ToArray();
		}
	}
}