using DT;
using System.Collections;
using UnityEngine;

namespace DT {
	public interface IInteractionZoneDelegate {
		void Interact(InteractionActorController actor);
		void BecameActive();
		void LostActive();
	}
}
