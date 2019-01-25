using System;
using Rewired;
using UnityEngine;

namespace Core.Inputs {
    /// <summary>
    /// ReWired helper to streamline input queries management
    /// </summary>
    public class ActionsMapsHelper {

        public Player Player1Inputs { get; protected set; }
        public Player Player2Inputs { get; protected set; }


        /// <summary>
        /// Constructor
        /// </summary>
        public ActionsMapsHelper() {
            // Set ref once
            Player1Inputs = ReInput.players.GetPlayer(0);
            Player2Inputs = ReInput.players.GetPlayer(1);
        }


        /// <summary>
        /// Apply the rules of the contextual states for the inputs.
        /// </summary>
        /// <param name="context"></param>
        public void ApplyContext(ContextualInputsState context) {
            // Apply for all categories
            foreach ( var state in context.actionsCategories ) {
                if ( state.setStateTo ) {
                    EnableMap(state.category);
                } else {
                    DisableMap(state.category);
                }
            }

            // Apply for all specific actions
            foreach ( var state in context.specificActions ) {
                SetActionEnabled(state.action, state.setStateTo);
            }
        }


        /// <summary>
        /// Enable/disable a particular action of any type in any maps
        /// </summary>
        /// <param name="actionId"></param>
        /// <param name="state"></param>
        public void SetActionEnabled(int actionId, bool state) {
            // All individual Action Element Maps in all Controller Maps in the Player
            foreach ( ControllerMap map in Player1Inputs.controllers.maps.GetAllMaps() ) {
                // Action Element Maps of all types
                foreach ( ActionElementMap aem in map.AllMaps ) {
                    if ( actionId == aem.actionId ) {
                        aem.enabled = state;
                    }
                }
            }
        }


        /// <summary>
        /// Method Overload
        /// </summary>
        /// <param name="actionName"></param>
        /// <param name="state"></param>
        public void SetActionEnabled(string actionName, bool state) {
            InputAction action = ReInput.mapping.GetAction(actionName);
            SetActionEnabled(action.id, state);
        }


        /// <summary>
        /// Set all action inside a mapping to enabled
        /// </summary>
        /// <param name="categoryName"></param>
        public void ActivateAllActionsOfMap(string categoryName) {
            foreach ( ControllerMap map in Player1Inputs.controllers.maps.GetAllMaps() ) {
                InputMapCategory mapCategory = ReInput.mapping.GetMapCategory(map.categoryId);
                if ( mapCategory.name == categoryName ) {
                    foreach ( ActionElementMap aem in map.AllMaps ) {
                        aem.enabled = true;
                    }
                }
            }
        }


        /// <summary>
        /// Enable entire action category mapping
        /// </summary>
        /// <param name="categoryName"></param>
        /// <param name="reactivateAllActions">Defines if we ensure reactivation of all actions inside the map. (Default: true)</param>
        public void EnableMap(string categoryName, bool reactivateAllActions = true) {
            Player1Inputs.controllers.maps.SetMapsEnabled(true, categoryName);
            if ( reactivateAllActions ) {
                ActivateAllActionsOfMap(categoryName);
            }
        }


        /// <summary>
        /// Method overload
        /// </summary>
        public void EnableMap(int categoryId, bool reactivateAllActions = true) {
            InputMapCategory mapCategory = ReInput.mapping.GetMapCategory(categoryId);
            EnableMap(mapCategory.name, reactivateAllActions);
        }


        /// <summary>
        /// Disable entire action category mapping
        /// </summary>
        /// <param name="categoryName"></param>
        public void DisableMap(string categoryName) { Player1Inputs.controllers.maps.SetMapsEnabled(false, categoryName); }


        public void DisableMap(int categoryId) { Player1Inputs.controllers.maps.SetMapsEnabled(false, categoryId); }

    }
}