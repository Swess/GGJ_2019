// <auto-generated>
// Rewired Constants
// This list was generated on 01/26/2019 08:36:08
// The list applies to only the Rewired Input Manager from which it was generated.
// If you use a different Rewired Input Manager, you will have to generate a new list.
// If you make changes to the exported items in the Rewired Input Manager, you will
// need to regenerate this list.
// </auto-generated>

namespace RewiredConsts {
    public static partial class Action {
        // Default
        // Gameplay
        [Rewired.Dev.ActionIdFieldInfo(categoryName = "Gameplay", friendlyName = "Horizontal")]
        public const int Horizontal = 5;
        [Rewired.Dev.ActionIdFieldInfo(categoryName = "Gameplay", friendlyName = "Vertical")]
        public const int Vertical = 6;
        [Rewired.Dev.ActionIdFieldInfo(categoryName = "Gameplay", friendlyName = "Use")]
        public const int Use = 8;
        [Rewired.Dev.ActionIdFieldInfo(categoryName = "Gameplay", friendlyName = "Drop item")]
        public const int Drop = 10;
        [Rewired.Dev.ActionIdFieldInfo(categoryName = "Gameplay", friendlyName = "Pause")]
        public const int Pause = 0;
        // Menus
        [Rewired.Dev.ActionIdFieldInfo(categoryName = "Menus", friendlyName = "Cancel")]
        public const int UICancel = 1;
        [Rewired.Dev.ActionIdFieldInfo(categoryName = "Menus", friendlyName = "Submit")]
        public const int UISubmit = 2;
        [Rewired.Dev.ActionIdFieldInfo(categoryName = "Menus", friendlyName = "Vertical")]
        public const int UIVertical = 3;
        [Rewired.Dev.ActionIdFieldInfo(categoryName = "Menus", friendlyName = "Horizontal")]
        public const int UIHorizontal = 4;
    }
    public static partial class Category {
        public const int Default = 0;
        public const int Gameplay = 1;
        public const int Menus = 2;
    }
    public static partial class Layout {
        public static partial class Joystick {
            public const int Default = 0;
        }
        public static partial class Keyboard {
            public const int Default = 0;
        }
        public static partial class Mouse {
            public const int Default = 0;
        }
        public static partial class CustomController {
            public const int Default = 0;
        }
    }
    public static partial class Player {
        [Rewired.Dev.PlayerIdFieldInfo(friendlyName = "System")]
        public const int System = 9999999;
        [Rewired.Dev.PlayerIdFieldInfo(friendlyName = "Player1")]
        public const int Player1 = 0;
    }
    public static partial class CustomController {
    }
}
