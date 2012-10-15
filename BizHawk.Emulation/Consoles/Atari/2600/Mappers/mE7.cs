﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BizHawk.Emulation.Consoles.Atari._2600
{
	/*
	E7 (M-Network)
	-----

	M-network wanted something of their own too, so they came up with what they called 
	"Big Game" (this was printed on the prototype ASICs on the prototype carts).  It
	can handle up to 16K of ROM and 2K of RAM.

	1000-17FF is selectable
	1800-19FF is RAM
	1A00-1FFF is fixed to the last 1.5K of ROM

	Accessing 1FE0 through 1FE6 selects bank 0 through bank 6 of the ROM into 1000-17FF.
	Accessing 1FE7 enables 1K of the 2K RAM, instead.

	When the RAM is enabled, this 1K appears at 1000-17FF.  1000-13FF is the write port, 1400-17FF
	is the read port.  

	1800-19FF also holds RAM. 1800-18FF is the write port, 1900-19FF is the read port.  
	Only 256 bytes of RAM is accessable at time, but there are four different 256 byte
	banks making a total of 1K accessable here.

	Accessing 1FE8 through 1FEB select which 256 byte bank shows up.
	*/

	class mE7 : MapperBase
	{
		int toggle = 0;
		int rambank1_toggle = 0;
		ByteBuffer rambank0 = new ByteBuffer(1024);
		ByteBuffer rambank1 = new ByteBuffer(1024);
		bool EnableRam0 = false;

		public override byte ReadMemory(ushort addr)
		{
			Address(addr);
			if (addr < 0x1000) return base.ReadMemory(addr);
			else if (addr < 0x1400)
			{
				if (EnableRam0)
				{
					return rambank0[(addr & 1023)];
				}
				else
				{
					return core.rom[toggle * 2 * 1024 + (addr & 0x7FF)];
				}
			}
			else if (addr < 0x1800)
			{
				return core.rom[toggle * 2 * 1024 + (addr & 0x7FF)];
			}
			else if (addr < 0x1A00)
			{
				return rambank1[toggle * 256 + (addr & 0x255)];
			}
			else if (addr < 0x2000)
			{
				addr -= 0x1A00;
				addr &= 0x5FF;
				return core.rom[14848 + addr]; //Fixed to last 1.5K
			}
			else
			{
				return base.ReadMemory(addr);
			}
		}
		public override void WriteMemory(ushort addr, byte value)
		{
			Address(addr);
			if (addr < 0x1000)
			{
				base.WriteMemory(addr, value);
			}
			else if (addr < 0x1400)
			{
				rambank0[addr & 0x3FF] = value;
			}
			else if (addr >= 0x1800 && addr < 0x2000)
			{
				rambank0[(addr & 0xFF) + (rambank1_toggle * 0x100)] = value;
			}
		}

		public override void SyncState(Serializer ser)
		{
			base.SyncState(ser);
			ser.Sync("toggle", ref toggle);
			ser.Sync("rambank0", ref rambank0);
			ser.Sync("rambank1", ref rambank1);
			ser.Sync("EnableRam0", ref EnableRam0);
			ser.Sync("rambank1_toggle", ref rambank1_toggle);
		}

		void Address(ushort addr)
		{
			switch (addr)
			{
				case 0x1FE0:
					toggle = 0;
					break;
				case 0x1FE1:
					toggle = 1;
					break;
				case 0x1FE2:
					toggle = 2;
					break;
				case 0x1FE3:
					toggle = 3;
					break;
				case 0x1FE4:
					toggle = 4;
					break;
				case 0x1FE5:
					toggle = 5;
					break;
				case 0x1FE6:
					toggle = 6;
					break;
				case 0x1FE7:
					EnableRam0 = true;
					break;
				case 0x1FE8:
					rambank1_toggle = 0;
					break;
				case 0x1FE9:
					rambank1_toggle = 1;
					break;
				case 0x1FEA:
					rambank1_toggle = 2;
					break;
				case 0x1FEB:
					rambank1_toggle = 3;
					break;
			}
		}
	}
}
