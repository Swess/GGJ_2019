using System;
using UnityEngine;

namespace Core.Types {
    public static class TypesHelper {

        // ====================================
        // === Vectors Helpers
        // ====================================

        /// <summary>
        /// Used only on specific cases where we have a directional vector that we need
        /// to point in the positive direction
        /// </summary>
        /// <param name="vector"></param>
        /// <returns>Vector with all axis in absolute values</returns>
        public static Vector3 AbsVector(Vector3 vector) {
            return new Vector3(Mathf.Abs(vector.x),
                               Mathf.Abs(vector.y),
                               Mathf.Abs(vector.z));
        }

    }
}