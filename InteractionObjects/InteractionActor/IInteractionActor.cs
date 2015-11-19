using DT;
using System.Collections;
using UnityEngine;

namespace DT {
	public interface IInteractionActor {
		void EnteredZone(int zoneType, InteractionZoneController zone);
		void ExitedZone(int zoneType, InteractionZoneController zone);
		bool IsClosestZone(int zoneType, InteractionZoneController zone);
		bool InteractWithZoneType(int zoneType);
	}
}