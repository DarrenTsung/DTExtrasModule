using DT;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DT {
	public class InteractionZoneController : MonoBehaviour {
		// PRAGMA MARK - Public Interface
		public void Interact(InteractionActorController actor) {
			_delegate.Interact(actor);
		}
		
		// PRAGMA MARK - Internal
		[SerializeField]
		protected int _zoneType = 0;
		
		protected IInteractionZoneDelegate _delegate;
		protected Dictionary<GameObject, IInteractionActor> _cachedActorMap;
		protected bool _active;

		protected void Awake() {
			_cachedActorMap = new Dictionary<GameObject, IInteractionActor>();
			
			_delegate = GetComponentInParent<IInteractionZoneDelegate>();
			if (_delegate == null) {
				Debug.LogError("InteractionZoneController with no delegate in parent!");
			}
		}

		protected IInteractionActor GetActor(GameObject other) {
			if (!_cachedActorMap.ContainsKey(other)) {
				// cache miss - grab from other object using GetComponent
				IInteractionActor actor = other.GetComponentInParent<IInteractionActor>();

				if (actor == null) {
					Debug.LogError("InteractionZone entered by GameObject without InteractionZoneActor script");
					return null;
				}
				
				_cachedActorMap[other] = actor;
			}
			
			return _cachedActorMap[other];
		}

		protected void OnEnter(GameObject other) {
			IInteractionActor actor = GetActor(other);
			actor.EnteredZone(_zoneType, this);

			if (actor.IsClosestZone(_zoneType, this)) {
				_active = true;
				_delegate.BecameActive();
			}
		}

		protected void OnStay(GameObject other) {
			IInteractionActor actor = GetActor(other);

			bool previouslyActive = _active;
			_active = actor.IsClosestZone(_zoneType, this);

			if (!previouslyActive && _active) {
				_delegate.BecameActive();
			} else if (previouslyActive && !_active) {
				_delegate.LostActive();
			}
		}

		protected void OnExit(GameObject other) {
			IInteractionActor actor = GetActor(other);
			actor.ExitedZone(_zoneType, this);
			
			_cachedActorMap.Remove(other);

			if (_active) {
				_active = false;
				_delegate.LostActive();
			}
		}
	}
}