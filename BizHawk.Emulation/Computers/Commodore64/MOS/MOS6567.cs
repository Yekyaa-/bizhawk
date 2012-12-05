﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BizHawk.Emulation.Computers.Commodore64.MOS
{
	// vic ntsc
	public class MOS6567 : Vic
	{
		static uint[][] pipeline = new uint[5][];

		public MOS6567()
			: base(65, 263, pipeline, 14318181 / 14)
		{
		}
	}
}
