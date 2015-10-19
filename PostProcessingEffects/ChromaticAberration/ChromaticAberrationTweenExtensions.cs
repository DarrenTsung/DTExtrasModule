#if DT_TWEENING

﻿using DT;
using DT.Tweening;
using System.Collections;
using UnityEngine;

namespace DT.Tweening {
	public static class ChromaticAberrationTweenExtensions {
		public static Tween<float> AberrationOffsetTweenTo(this ChromaticAberrationEffect e, float to, float duration = 0.3f) {
			return e.AberrationOffsetTween(from: e.AberrationOffset, to: to, duration: duration);
		}
			
		public static Tween<float> AberrationOffsetTween(this ChromaticAberrationEffect e, float from, float to, float duration = 0.3f) {
			var tweenTarget = new ChromaticAberrationFloatTarget(e, ChromaticAberrationType.ABERRATION_OFFSET);
			var tween = FloatTween.Create();
			tween.Initialize(tweenTarget, to, duration);
			tweenTarget.SetValue(from);
			
			return tween;
		}
	}

	public enum ChromaticAberrationType {
	  ABERRATION_OFFSET
	}
	
	public class ChromaticAberrationFloatTarget : AbstractTweenTarget<ChromaticAberrationEffect, float> {
		// PRAGMA MARK - Interface
	  public ChromaticAberrationFloatTarget(ChromaticAberrationEffect target, ChromaticAberrationType targetType) {
	    _target = target;
	    _targetType = targetType;
	  }
	  
	  public override float GetValue() {
			switch (_targetType) {
				case ChromaticAberrationType.ABERRATION_OFFSET:
				default:
			    return _target.AberrationOffset;
			}
	  }
	  
	  public override void SetValue(float value) {
			switch (_targetType) {
				case ChromaticAberrationType.ABERRATION_OFFSET:
				default:
			    _target.AberrationOffset = value;
					break;
			}
	  }
	  
		// PRAGMA MARK - Internal
	  protected ChromaticAberrationType _targetType;
	}
}

#endif