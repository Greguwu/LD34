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

			ButtonMappings = new[]
			{
				new InputControlMapping
				{
					Handle = "A Button",
					Target = InputControlType.Action1,
					Source = KeyCodeButton( KeyCode.DownArrow )
				},
				new InputControlMapping
				{
					Handle = "B Button",
					Target = InputControlType.Action2,
					Source = KeyCodeButton( KeyCode.RightArrow )
				},
				new InputControlMapping
				{
					Handle = "X Button",
					Target = InputControlType.Action3,
					Source = KeyCodeButton( KeyCode.LeftArrow )
				},
				new InputControlMapping
				{
					Handle = "Y Button",
					Target = InputControlType.Action4,
					Source = KeyCodeButton( KeyCode.UpArrow )
				},
				new InputControlMapping
				{
					Handle = "LB Button",
					Target = InputControlType.LeftBumper,
					Source = KeyCodeButton( KeyCode.A )
				},
				new InputControlMapping
				{
					Handle = "RB Button",
					Target = InputControlType.RightBumper,
					Source = KeyCodeButton( KeyCode.E )
				},
				new InputControlMapping
				{
					Handle = "DPad Up Button",
					Target = InputControlType.DPadUp,
					Source = KeyCodeButton( KeyCode.Alpha1 )
				},
				new InputControlMapping
				{
					Handle = "DPad Down Button",
					Target = InputControlType.DPadDown,
					Source = KeyCodeButton( KeyCode.Alpha2 )
				},
				new InputControlMapping
				{
					Handle = "DPad Right Button",
					Target = InputControlType.DPadRight,
					Source = KeyCodeButton( KeyCode.Alpha3 )
				},
				new InputControlMapping
				{
					Handle = "DPad Left Button",
					Target = InputControlType.DPadLeft,
					Source = KeyCodeButton( KeyCode.Alpha4 )
				},
				new InputControlMapping
				{
					Handle = "Start Button",
					Target = InputControlType.Menu,
					Source = KeyCodeButton( KeyCode.Return )
				}
			};

			AnalogMappings = new[]
			{
				new InputControlMapping
				{
					Handle = "Left Stick",
					Target = InputControlType.LeftStickX,
					// KeyCodeAxis splits the two KeyCodes over an axis. The first is negative, the second positive.
					Source = KeyCodeAxis( KeyCode.Q, KeyCode.D )
				},
				new InputControlMapping
				{
					Handle = "Right Trigger",
					Target = InputControlType.RightTrigger,
					// Notes that up is positive in Unity, therefore the order of KeyCodes is down, up.
					Source = KeyCodeAxis( KeyCode.S, KeyCode.Z )
				},
				new InputControlMapping
				{
					Handle = "Left Trigger",
					Target = InputControlType.LeftTrigger,
					// Notes that up is positive in Unity, therefore the order of KeyCodes is down, up.
					Source = KeyCodeAxis( KeyCode.Space, KeyCode.LeftShift )
				}
			};
		}
	}
}

