using System;
using System.Collections;
using UnityEngine;
using InControl;


namespace ComputerProfile
{
	// This custom profile is enabled by adding it to the Custom Profiles list
	// on the InControlManager component, or you can attach it yourself like so:
	// InputManager.AttachDevice( new UnityInputDevice( "KeyboardAndMouseProfile" ) );
	// 
	public class KeyboardAndMouseProfile : UnityInputDeviceProfile
	{
		public KeyboardAndMouseProfile()
		{
			Name = "Keyboard/Mouse";
			Meta = "A keyboard and mouse combination profile.";

			// This profile only works on desktops.
			SupportedPlatforms = new[]
			{
				"Windows",
				"Mac",
				"Linux"
			};

			Sensitivity = 1.0f;
			LowerDeadZone = 0.0f;
			UpperDeadZone = 1.0f;

			/*ButtonMappings = new[]
			{
				new InputControlMapping
				{
					Handle = "A Button",
					Target = InputControlType.Action1,
					Source = KeyCodeButton( KeyCode.DownArrow )
				}
			};*/

			AnalogMappings = new[]
			{
				new InputControlMapping
				{
					Handle = "Left Stick X",
					Target = InputControlType.LeftStickX,
					// KeyCodeAxis splits the two KeyCodes over an axis. The first is negative, the second positive.
					Source = KeyCodeAxis( KeyCode.LeftArrow, KeyCode.RightArrow ),
				}/*,
                new InputControlMapping
                {
                    Handle = "Left Stick Y",
                    Target = InputControlType.LeftStickY,
					// KeyCodeAxis splits the two KeyCodes over an axis. The first is negative, the second positive.
					Source = KeyCodeAxis( KeyCode.DownArrow, KeyCode.UpArrow ),
                }*/
            };
		}
	}
}

