using DT;
using System.Collections;
using System.Collections.Generic;
﻿using UnityEngine;

public class InteractionActorController : MonoBehaviour, IInteractionActor {
	// PRAGMA MARK - IInteractionActor
	void IInteractionActor.EnteredZone(int zoneType, InteractionZoneController zone) {
		this.HashSetForZoneType(zoneType).Add(zone);
	}

	void IInteractionActor.ExitedZone(int zoneType, InteractionZoneController zone) {
		this.HashSetForZoneType(zoneType).Remove(zone);
	}

	bool IInteractionActor.IsClosestZone(int zoneType, InteractionZoneController zone) {
		return this.ClosestInteractionZoneForType(zoneType) == zone;
	}

	bool IInteractionActor.InteractWithZoneType(int zoneType) {
		this.UpdateClosestInteractionZone();
		
		InteractionZoneController zone = this.ClosestInteractionZoneForType(zoneType);
		if (zone != null) {
			zone.Interact(this);
			return true;
		}
		
		return false;
	}
	
	
	// PRAGMA MARK - Internal
	protected Dictionary<int, HashSet<InteractionZoneController>> _interactionZonesTouchingMapping;
	protected Dictionary<int, InteractionZoneController> _closestInteractionZoneMapping;
	
	protected void Awake() {
		_interactionZonesTouchingMapping = new Dictionary<int, HashSet<InteractionZoneController>>();
		_closestInteractionZoneMapping = new Dictionary<int, InteractionZoneController>();
	}
	
	protected void Update() {
		this.UpdateClosestInteractionZone();
	}

	protected void UpdateClosestInteractionZone() {
		foreach (KeyValuePair<int, HashSet<InteractionZoneController>> pair in _interactionZonesTouchingMapping) {
			int zoneType = pair.Key;
			HashSet<InteractionZoneController> zonesTouching = pair.Value;
			
			float minimumDistance = float.MaxValue;
			InteractionZoneController closestZone = null;
			
			foreach (InteractionZoneController other in zonesTouching) {
				GameObject otherGameObject = other.gameObject;
				
				float distanceToOther = Vector2.Distance(gameObject.transform.position, otherGameObject.transform.position);
				if (distanceToOther < minimumDistance) {
					minimumDistance = distanceToOther;
					closestZone = other;
				}
			}

			_closestInteractionZoneMapping[zoneType] = closestZone;
		}
	}
	
	protected InteractionZoneController ClosestInteractionZoneForType(int zoneType) {
		return _closestInteractionZoneMapping.SafeGet(zoneType, null);
	}
	
	protected HashSet<InteractionZoneController> HashSetForZoneType(int zoneType) {
		if (!_interactionZonesTouchingMapping.ContainsKey(zoneType)) {
			_interactionZonesTouchingMapping[zoneType] = new HashSet<InteractionZoneController>();
		}
		return _interactionZonesTouchingMapping[zoneType];
	}
}
