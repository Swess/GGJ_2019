using System;
using System.Collections;
using System.Collections.Generic;
using Rewired;
using UnityEngine;

namespace Core.Inputs {
    /// <summary>
    /// A list of input settings that defines a game context.
    /// Defines which ControllerMap to activate/deactivate and specific ActionMap to enabled/disabled when applied.
    /// Helps regroups inputs settings in different game context or event.
    /// </summary>
    [CreateAssetMenu(menuName = "GameSettings/Inputs/Contextual Inputs State", order = 149)]
    public class ContextualInputsState : ScriptableObject {

        public ActionCategoryState[] actionsCategories;
        public ActionState[]         specificActions;

        [Serializable]
        public struct ActionState {

            [ActionIdProperty(typeof(RewiredConsts.Action))]
            public int action;

            public bool setStateTo;

        }

        [Serializable]
        public struct ActionCategoryState {

            [ActionIdProperty(typeof(RewiredConsts.Category))]
            public int category;

            public bool setStateTo;

        }

    }
}