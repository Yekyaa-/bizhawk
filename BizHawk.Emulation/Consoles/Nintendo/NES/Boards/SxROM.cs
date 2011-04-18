using System;
using System.IO;
using System.Diagnostics;

namespace BizHawk.Emulation.Consoles.Nintendo
{
	//AKA MMC1
	//http://wiki.nesdev.com/w/index.php/SxROM

	//consult nestopia as well.
	//the initial conditions for MMC1 games are known to be different. this may have to do with which MMC1 rev it is.
	//but how do we know which revision a game is? i don't know which revision is on which board
	//check UNIF for more information.. it may specify board and MMC1 rev independently because boards may have any MMC1 rev
	//in that case, we need to capture MMC1 rev in the game database (maybe add a new `chip` parameter)

	//Final Fantasy
	//Mega Man 2
	//Blaster Master
	//Metroid
	//Kid Icarus
	//Zelda
	//Zelda 2
	//Castlevania 2

	class MMC1
	{
		public MMC1()
		{
			//collect data about whether this is required here:
			//kid icarus requires it
			//zelda doesnt; nor megaman2; nor blastermaster; nor metroid
			StandardReset();
			//well, lets leave it.
		}

		public void SyncState(Serializer ser)
		{
			ser.Sync("shift_count", ref shift_count);
			ser.Sync("shift_val", ref shift_val);
			ser.Sync("chr_mode", ref chr_mode);
			ser.Sync("prg_mode", ref prg_mode);
			ser.Sync("prg_slot", ref prg_slot);
			ser.Sync("chr_0", ref chr_0);
			ser.Sync("chr_1", ref chr_1);
			ser.Sync("wram_disable", ref wram_disable);
			ser.Sync("prg", ref prg);
			ser.SyncEnum("mirror", ref mirror);
		}

		public enum Rev
		{
			A, B1, B2, B3
		}

		//shift register
		int shift_count, shift_val;

		//register 0:
		public int chr_mode;
		public int prg_mode;
		public int prg_slot; //complicated
		public NES.NESBoardBase.EMirrorType mirror;
		static NES.NESBoardBase.EMirrorType[] _mirrorTypes = new NES.NESBoardBase.EMirrorType[] { NES.NESBoardBase.EMirrorType.OneScreenA, NES.NESBoardBase.EMirrorType.OneScreenB, NES.NESBoardBase.EMirrorType.Vertical, NES.NESBoardBase.EMirrorType.Horizontal };

		//register 1,2:
		int chr_0, chr_1;

		//register 3:
		int wram_disable;
		int prg;

		void StandardReset()
		{
			prg_mode = 1;
			prg_slot = 1;
		}

		public void Write(int addr, byte value)
		{
			int data = value & 1;
			int reset = (value >> 7) & 1;
			if (reset == 1)
			{
				shift_count = 0;
				shift_val = 0;
				StandardReset();
			}
			else
			{
				shift_val >>= 1;
				shift_val |= (data<<4);
				shift_count++;
				if (shift_count == 5)
				{
					WriteRegister(addr >> 13, shift_val);
					shift_count = 0;
					shift_val = 0;
				}
			}
		}

		void WriteRegister(int addr, int value)
		{
			switch (addr)
			{
				case 0: //8000-9FFF
					mirror = _mirrorTypes[value & 3];
					prg_slot = ((value >> 2) & 1);
					prg_mode = ((value >> 3) & 1);
					chr_mode = ((value >> 4) & 1);
					break;
				case 1: //A000-BFFF
					chr_0 = value & 0x1F;
					break;
				case 2: //C000-DFFF
					chr_1 = value & 0x1F;
					break;
				case 3: //E000-FFFF
					prg = value & 0xF;
					wram_disable = (value >> 4) & 1;
					break;
			}
			//Console.WriteLine("mapping.. chr_mode={0}, chr={1},{2}", chr_mode, chr_0, chr_1);
			//Console.WriteLine("mapping.. prg_mode={0}, prg_slot{1}, prg={2}", prg_mode, prg_slot, prg);
		}

		public int Get_PRGBank(int addr)
		{
			int PRG_A14 = (addr >> 14) & 1;
			if (prg_mode == 0)
				if (PRG_A14 == 0)
					return prg;
				else
				{
					//"not tested very well yet! had to guess!
					return (prg+1) & 0xF;
				}
			else if (prg_slot == 0)
				if (PRG_A14 == 0)
					return 0;
				else return prg;
			else
				if (PRG_A14 == 0)
					return prg;
				else return 0xF;
		}

		public int Get_CHRBank_4K(int addr)
		{
			int CHR_A12 = (addr >> 12) & 1;
			if (chr_mode == 0)
				return chr_0;
			else if (CHR_A12 == 0)
				return chr_0;
			else return chr_1;
		}
	}

	public class SxROM : NES.NESBoardBase
	{
		//configuration
		int prg_mask, chr_mask;
		int vram_mask, wram_mask;

		//state
		MMC1 mmc1;

		public override void WritePRG(int addr, byte value)
		{
			mmc1.Write(addr, value);
			SetMirrorType(mmc1.mirror); //often redundant, but gets the job done
		}

		public override byte ReadPRG(int addr)
		{
			int bank = mmc1.Get_PRGBank(addr) & prg_mask;
			addr = (bank << 14) | (addr & 0x3FFF);
			return ROM[addr];
		}

		int Gen_CHR_Address(int addr)
		{
			int bank = mmc1.Get_CHRBank_4K(addr);
			addr = ((bank & chr_mask) << 12) | (addr & 0x0FFF);
			return addr;
		}

		public override byte ReadPPU(int addr)
		{
			if (addr < 0x2000)
			{
				if (Cart.vram_size != 0)
					return VRAM[addr & vram_mask];
				else return VROM[Gen_CHR_Address(addr)];
			}
			else return base.ReadPPU(addr);
		}

		public override void WritePPU(int addr, byte value)
		{
			if (addr < 0x2000)
			{
				if (Cart.vram_size != 0)
					VRAM[addr & vram_mask] = value;
			}
			else base.WritePPU(addr, value);
		}

		public override byte ReadWRAM(int addr)
		{
			if (Cart.wram_size != 0)
				return WRAM[addr & wram_mask];
			else return 0xFF;
		}

		public override void WriteWRAM(int addr, byte value)
		{
			if (Cart.wram_size != 0)
				WRAM[addr & wram_mask] = value;
		}

		public override byte[] SaveRam
		{
			get
			{
				if (!Cart.wram_battery) return null;
				return WRAM;
				//some boards have a pram that is backed-up or not backed-up. need to handle that somehow
				//(nestopia splits it into NVWRAM and WRAM but i didnt like that at first.. but it may player better with this architecture)
			}
		}

		public override void SyncState(Serializer ser)
		{
			base.SyncState(ser);
			mmc1.SyncState(ser);

		}
	
		public override bool Configure(NES.EDetectionOrigin origin)
		{
			//analyze board type
			switch (Cart.board_type)
			{
				case "NES-SAROM": //dragon warrior
					AssertPrg(64); AssertChr(16, 32, 64); AssertVram(0); AssertWram(8); 
					break;
				case "NES-SBROM": //dance aerobics
					AssertPrg(64); AssertChr(16, 32, 64);  AssertVram(0); AssertWram(0);
					break;
				case "NES-SCROM": //mechanized attack
				case "NES-SC1ROM": //knight rider
					AssertPrg(64); AssertChr(128); AssertVram(0); AssertWram(0);
				    break;
				case "NES-SEROM": //lolo
				case "HVC-SEROM": //dr. mario
					AssertPrg(32); AssertChr(32); AssertVram(0); AssertWram(0);
				    break;
				case "NES-SFROM": //bubble bobble
					AssertPrg(128,256); AssertChr(16,32,64); AssertVram(0); AssertWram(0);
				    break;
				case "NES-SGROM": //bionic commando
					AssertPrg(128, 256); AssertChr(0); AssertVram(8); AssertWram(0);
				    break;
				case "NES-SHROM": //family feud
				case "NES-SH1ROM": //airwolf
					AssertPrg(32); AssertChr(128); AssertVram(0); AssertWram(0);
				    break;
				case "HVC-SIROM": //Igo: Kyuu Roban Taikyoku  
					AssertPrg(32); AssertChr(16); AssertVram(0); AssertWram(8);
				    break;
				case "NES-SJROM": //air fortress
					AssertPrg(128,256); AssertChr(16,32,64); AssertVram(0); AssertWram(8);
				    break;
				case "NES-SKROM": //zelda 2
					AssertPrg(128, 256); AssertChr(128); AssertVram(0); AssertWram(8);
				    break;
				case "NES-SLROM": //castlevania 2
					AssertPrg(128, 256); AssertChr(128); AssertVram(0); AssertWram(0);
				    break;
				case "NES-SL1ROM": //hoops
					AssertPrg(64, 128, 256); AssertChr(128); AssertVram(0); AssertWram(0);
				    break;
				case "NES-SL2ROM": //blaster master
					AssertPrg(128); AssertChr(128); AssertVram(0); AssertWram(0);
				    break;
				case "NES-SL3ROM": //goal!
					AssertPrg(256); AssertChr(128); AssertVram(0); AssertWram(0);
				    break;
				case "NES-SLRROM": //tecmo bowl
					AssertPrg(128); AssertChr(128); AssertVram(0); AssertWram(0);
				    break;
				case "HVC-SMROM": //Hokkaidou Rensa Satsujin: Okhotsu ni Shoyu  
					AssertPrg(256); AssertChr(0); AssertVram(8); AssertWram(0);
				    break;
				case "NES-SNROM": //dragon warrior 2
				case "HVC-SNROM": 
					//prg=16 is unexpected but blargg's tests use it
					AssertPrg(16, 128, 256); AssertChr(0); AssertVram(8); AssertWram(8);
					//TODO - consider making a unique board type for homebrew, as i discover how more of them are working
					break;
				case "NES-SOROM": //Nobunaga's Ambition
					AssertPrg(128, 256); AssertChr(0); AssertVram(8); AssertWram(16);
				    break;
				case "NES-SUROM": //dragon warrior 4
				case "HVC-SUROM":
					AssertPrg(512); AssertChr(0); AssertVram(8); AssertWram(8);
				    break;
				case "HVC-SXROM": //final fantasy 1& 2
					AssertPrg(128,256,512); AssertChr(0); AssertVram(8); AssertWram(32);
				    break;
				default:
					return false;
			}

			mmc1 = new MMC1();
			prg_mask = (Cart.prg_size / 16) - 1;
			vram_mask = (Cart.vram_size*1024) - 1;
			wram_mask = (Cart.wram_size*1024) - 1;
			chr_mask = (Cart.chr_size / 8 * 2) - 1;
			SetMirrorType(mmc1.mirror);

			return true;
		}

	}
}
