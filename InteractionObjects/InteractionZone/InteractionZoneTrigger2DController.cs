using DT;
using System.Collections;
﻿using UnityEngine;

namespace DT {
	public class InteractionZoneTrigger2DController : InteractionZoneController {
		// PRAGMA MARK - Internal
		protected void OnTriggerEnter2D(Collider2D other) {
			this.OnEnter(other.gameObject);
		}
		
		protected void OnTriggerStay2D(Collider2D other) {
			this.OnStay(other.gameObject);
		}

		protected void OnTriggerExit2D(Collider2D other) {
			this.OnExit(other.gameObject);
		}
	}
}
