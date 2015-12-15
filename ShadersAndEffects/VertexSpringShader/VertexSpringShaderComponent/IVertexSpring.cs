using DT;
using UnityEngine;

namespace DT {
	public interface IVertexSpring {
		void SetSpringTarget(string name, Vector3 target);
		void RemoveSpringTarget(string name);
	}
}