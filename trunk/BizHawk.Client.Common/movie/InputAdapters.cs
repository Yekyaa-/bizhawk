﻿using System;
using System.Collections.Generic;

using BizHawk.Common;
using BizHawk.Emulation.Common;

namespace BizHawk.Client.Common
{
	using System.Linq;

	/// <summary>
	/// will hold buttons for 1 frame and then release them. (Calling Click() from your button click is what you want to do)
	/// TODO - should the duration be controllable?
	/// </summary>
	public class ClickyVirtualPadController : IController
	{
		public ControllerDefinition Type { get; set; }
		
		public bool this[string button]
		{
			get { return IsPressed(button); }
		}

		public float GetFloat(string name)
		{
			return 0.0f;
		}

		// TODO
		public bool IsPressed(string button)
		{
			return _pressed.Contains(button);
		}
		
		/// <summary>
		/// call this once per frame to do the timekeeping for the hold and release
		/// </summary>
		public void FrameTick()
		{
			_pressed.Clear();
		}

		/// <summary>
		/// call this to hold the button down for one frame
		/// </summary>
		public void Click(string button)
		{
			_pressed.Add(button);
		}

		public void Unclick(string button)
		{
			_pressed.Remove(button);
		}

		public void Toggle(string button)
		{
			if (IsPressed(button))
			{
				_pressed.Remove(button);
			}
			else
			{
				_pressed.Add(button);
			}
		}

		private readonly HashSet<string> _pressed = new HashSet<string>();
	}

	/// <summary>
	/// Filters input for things called Up and Down while considering the client's AllowUD_LR option. 
	/// This is a bit gross but it is unclear how to do it more nicely
	/// </summary>
	public class UD_LR_ControllerAdapter : IController
	{
		public ControllerDefinition Type
		{
			get { return Source.Type; }
		}

		public bool this[string button]
		{
			get { return IsPressed(button); }
		}

		public IController Source { get; set; }

		// The float format implies no U+D and no L+R no matter what, so just passthru
		public float GetFloat(string name)
		{
			return Source.GetFloat(name);
		}

		public bool IsPressed(string button)
		{
			if (Global.Config.AllowUD_LR)
			{
				return Source.IsPressed(button);
			}

			string prefix;

			if (button.Contains("Down") && !button.Contains(" C "))
			{
				prefix = button.GetPrecedingString("Down");
				if (Source.IsPressed(prefix + "Up"))
				{
					return false;
				}
			}

			if (button.Contains("Right") && !button.Contains(" C "))
			{
				prefix = button.GetPrecedingString("Right");
				if (Source.IsPressed(prefix + "Left"))
				{
					return false;
				}
			}

			return Source.IsPressed(button);
		}
	}

	public class SimpleController : IController
	{
		public ControllerDefinition Type { get; set; }

		protected WorkingDictionary<string, bool> Buttons = new WorkingDictionary<string, bool>();
		protected WorkingDictionary<string, float> Floats = new WorkingDictionary<string, float>();

		public virtual void Clear()
		{
			Buttons = new WorkingDictionary<string, bool>();
			Floats = new WorkingDictionary<string, float>();
		}

		public virtual bool this[string button]
		{
			get { return Buttons[button]; } set { Buttons[button] = value; }
		}

		public virtual bool IsPressed(string button)
		{
			return this[button];
		}

		public float GetFloat(string name)
		{
			return Floats[name];
		}

		public IEnumerable<KeyValuePair<string, bool>> BoolButtons()
		{
			return Buttons;
		}

		public virtual void LatchFrom(IController source)
		{
			foreach (var button in source.Type.BoolButtons)
			{
				Buttons[button] = source[button];
			}
		}

		public void AcceptNewFloats(IEnumerable<Tuple<string, float>> newValues)
		{
			foreach (var sv in newValues)
			{
				Floats[sv.Item1] = sv.Item2;
			}
		}
	}

	public class ORAdapter : IController
	{
		public bool IsPressed(string button)
		{
			return this[button];
		}

		// pass floats solely from the original source
		// this works in the code because SourceOr is the autofire controller
		public float GetFloat(string name) { return Source.GetFloat(name); }

		public IController Source { get; set; }
		public IController SourceOr { get; set; }
		public ControllerDefinition Type { get { return Source.Type; } set { throw new InvalidOperationException(); } }

		public bool this[string button]
		{
			get
			{
				return Source[button] | SourceOr[button];
			}
			set
			{
				throw new InvalidOperationException();
			}
		}

	}

	public class ForceOffAdaptor : IController
	{
		public bool IsPressed(string button) { return this[button]; }

		// what exactly would we want to do here with floats?
		// ForceOffAdaptor is only used by lua, and the code there looks like a big mess...
		public float GetFloat(string name) { return Source.GetFloat(name); }

		protected HashSet<string> StickySet = new HashSet<string>();
		public IController Source { get; set; }
		public IController SourceOr { get; set; }

		public ControllerDefinition Type
		{
			get { return Source.Type; } 
			set { throw new InvalidOperationException(); }
		}

		public bool this[string button]
		{
			get
			{
				return !StickySet.Contains(button) && Source[button];
			}

			set
			{
				throw new InvalidOperationException();
			}
		}

		public void SetSticky(string button, bool isSticky)
		{
			if (isSticky)
			{
				this.StickySet.Add(button);
			}
			else
			{
				this.StickySet.Remove(button);
			}
		}
	}

	public class StickyXorAdapter : IController
	{
		protected HashSet<string> stickySet = new HashSet<string>();
		
		public IController Source { get; set; }

		public ControllerDefinition Type
		{
			get { return Source.Type; } 
			set { throw new InvalidOperationException(); }
		}

		public bool Locked { get; set; } // Pretty much a hack, 

		public bool IsPressed(string button) { return this[button]; }

		// if SetFloat() is called (typically virtual pads), then that float will entirely override the Source input
		// otherwise, the source is passed thru.
		private readonly WorkingDictionary<string,float?> _floatSet = new WorkingDictionary<string,float?>();
		
		public void SetFloat(string name, float? value)
		{
			if (value.HasValue)
			{
				_floatSet[name] = value;
			}
			else
			{
				_floatSet.Remove(name);
			}
		}

		public float GetFloat(string name)
		{
			return _floatSet[name] ?? Source.GetFloat(name);
		}

		public void ClearStickyFloats()
		{
			_floatSet.Clear();
		}

		public bool this[string button]
		{ 
			get 
			{
				var source = Source[button];
				source ^= stickySet.Contains(button);
				return source;
			}

			set
			{
				throw new InvalidOperationException();
			}
		}

		public void SetSticky(string button, bool isSticky)
		{
			if (isSticky)
			{
				stickySet.Add(button);
			}
			else
			{
				stickySet.Remove(button);
			}
		}

		public bool IsSticky(string button)
		{
			return stickySet.Contains(button);
		}

		public HashSet<string> CurrentStickies
		{
			get
			{
				return stickySet;
			}
		}

		public void ClearStickies()
		{
			stickySet.Clear();
		}

		public void MassToggleStickyState(List<string> buttons)
		{
			foreach (var button in buttons.Where(button => !_justPressed.Contains(button)))
			{
				if (stickySet.Contains(button))
				{
					stickySet.Remove(button);
				}
				else
				{
					stickySet.Add(button);
				}
			}

			_justPressed = buttons;
		}

		private List<string> _justPressed = new List<string>();
	}

	public class AutoFireStickyXorAdapter : IController
	{
		public int On { get; set; }
		public int Off { get; set; }
		public WorkingDictionary<string, int> buttonStarts = new WorkingDictionary<string, int>();
		
		private readonly HashSet<string> _stickySet = new HashSet<string>();

		public IController Source { get; set; }

		public void SetOnOffPatternFromConfig()
		{
			On = Global.Config.AutofireOn < 1 ? 0 : Global.Config.AutofireOn;
			Off = Global.Config.AutofireOff < 1 ? 0 : Global.Config.AutofireOff;
		}

		public AutoFireStickyXorAdapter()
		{
			// On = Global.Config.AutofireOn < 1 ? 0 : Global.Config.AutofireOn;
			// Off = Global.Config.AutofireOff < 1 ? 0 : Global.Config.AutofireOff;
			On = 1;
			Off = 1;
		}

		public bool IsPressed(string button)
		{
			if (_stickySet.Contains(button))
			{
				var a = (Global.Emulator.Frame - buttonStarts[button]) % (On + Off);
				if (a < On)
				{
					return this[button];
				}
				else
				{
					return false;
				}
			}
			else
			{
				return Source[button];
			}
		}

		public bool this[string button]
		{
			get
			{
				var source = Source[button];

				if (_stickySet.Contains(button))
				{
					var a = (Global.Emulator.Frame - buttonStarts[button]) % (On + Off);
					if (a < On)
					{
						source ^= true;
					}
					else
					{
						source ^= false;
					}
				}
				
				return source;
			}

			set
			{
				throw new InvalidOperationException();
			}
		}

		public ControllerDefinition Type { get { return Source.Type; } set { throw new InvalidOperationException(); } }
		public bool Locked { get; set; } // Pretty much a hack, 

		// dumb passthrough for floats, because autofire doesn't care about them
		public float GetFloat(string name)
		{
			return Source.GetFloat(name);
		}

		public void SetSticky(string button, bool isSticky)
		{
			if (isSticky)
			{
				this._stickySet.Add(button);
			}
			else
			{
				this._stickySet.Remove(button);
			}
		}

		public bool IsSticky(string button)
		{
			return this._stickySet.Contains(button);
		}

		public HashSet<string> CurrentStickies
		{
			get
			{
				return this._stickySet;
			}
		}

		public void ClearStickies()
		{
			this._stickySet.Clear();
		}

		public void MassToggleStickyState(List<string> buttons)
		{
			foreach (var button in buttons.Where(button => !_justPressed.Contains(button)))
			{
				if (_stickySet.Contains(button))
				{
					_stickySet.Remove(button);
				}
				else
				{
					_stickySet.Add(button);
				}
			}

			_justPressed = buttons;
		}

		private List<string> _justPressed = new List<string>();
	}

	/// <summary>
	/// Just copies source to sink, or returns whatever a NullController would if it is disconnected. useful for immovable hardpoints.
	/// </summary>
	public class CopyControllerAdapter : IController
	{
		public IController Source { get; set; }
		
		private readonly NullController _null = new NullController();

		private IController Curr
		{
			get
			{
				if (Source == null)
				{
					return _null;
				}
				else
				{
					return Source;
				}
			}
		}

		public ControllerDefinition Type
		{
			get { return Curr.Type; }
		}

		public bool this[string button]
		{
			get { return Curr[button]; }
		}

		public bool IsPressed(string button)
		{
			return Curr.IsPressed(button);
		}

		public float GetFloat(string name)
		{
			return Curr.GetFloat(name);
		}
	}

	public class ButtonNameParser
	{
		public static ButtonNameParser Parse(string button)
		{
			// See if we're being asked for a button that we know how to rewire
			var parts = button.Split(' ');
			
			if (parts.Length < 2)
			{
				return null;
			}

			if (parts[0][0] != 'P')
			{
				return null;
			}

			int player;
			if (!int.TryParse(parts[0].Substring(1), out player))
			{
				return null;
			}
			else
			{
				return new ButtonNameParser { PlayerNum = player, ButtonPart = button.Substring(parts[0].Length + 1) };
			}
		}

		public int PlayerNum { get; set; }
		public string ButtonPart { get; private set; }

		public override string ToString()
		{
			return string.Format("P{0} {1}", PlayerNum, ButtonPart);
		}
	}

	/// <summary>
	/// rewires player1 controls to playerN
	/// </summary>
	public class MultitrackRewiringControllerAdapter : IController
	{
		public IController Source { get; set; }
		public int PlayerSource = 1;
		public int PlayerTargetMask = 0;

		public ControllerDefinition Type { get { return Source.Type; } }
		public bool this[string button] { get { return IsPressed(button); } }
		
		// floats can be player number remapped just like boolbuttons
		public float GetFloat(string name) { return Source.GetFloat(RemapButtonName(name)); }

		private string RemapButtonName(string button)
		{
			// Do we even have a source?
			if (PlayerSource == -1)
			{
				return button;
			}

			// See if we're being asked for a button that we know how to rewire
			var bnp = ButtonNameParser.Parse(button);
			
			if (bnp == null)
			{
				return button;
			}

			// Ok, this looks like a normal `P1 Button` type thing. we can handle it
			// Were we supposed to replace this one?
			int foundPlayerMask = (1 << bnp.PlayerNum);
			if ((PlayerTargetMask & foundPlayerMask) == 0)
			{
				return button;
			}

			// Ok, we were. swap out the source player and then grab his button
			bnp.PlayerNum = PlayerSource;
			return bnp.ToString();
		}

		public bool IsPressed(string button)
		{
			return Source.IsPressed(RemapButtonName(button));
		}
	}
}