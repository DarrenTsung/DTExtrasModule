﻿using DT;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
﻿using UnityEngine;

namespace DT {
	[ExecuteInEditMode]
	public class RendererInstance : MaterialInstanceBase {
		// PRAGMA MARK - INTERNAL
		protected Renderer[] Renderers {
			get { 
				return this.GetComponentsInChildren<Renderer>(); 
			}
		}
		
		protected override void OnCreatedMaterial(Material mat) {
			base.OnCreatedMaterial(mat);
			foreach (Renderer r in this.Renderers) {
				r.sharedMaterial = this.MaterialInstance;
			}
		}
	}
}