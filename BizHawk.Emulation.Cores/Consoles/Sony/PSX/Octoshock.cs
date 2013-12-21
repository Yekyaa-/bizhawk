﻿using System;
using System.Runtime.InteropServices;
using System.IO;
using System.Collections.Generic;

using BizHawk.Emulation.Common;

#pragma warning disable 649 //adelikat: Disable dumb warnings until this file is complete

namespace BizHawk.Emulation.Cores.Sony.PSX
{
	public unsafe class Octoshock : IEmulator, IVideoProvider, ISoundProvider
	{
		public string SystemId { get { return "NULL"; } }
		public static readonly ControllerDefinition NullController = new ControllerDefinition { Name = "Null Controller" };

		public string BoardName { get { return null; } }

		private int[] frameBuffer = new int[0];
		private Random rand = new Random();
		public CoreComm CoreComm { get; private set; }
		public IVideoProvider VideoProvider { get { return this; } }
		public ISoundProvider SoundProvider { get { return this; } }
		public ISyncSoundProvider SyncSoundProvider { get { return new FakeSyncSound(this, 735); } }
		public bool StartAsyncSound() { return true; }
		public void EndAsyncSound() { }

		public List<KeyValuePair<string, int>> GetCpuFlagsAndRegisters()
		{
			throw new NotImplementedException();
		}

		public static bool CheckIsPSX(DiscSystem.Disc disc)
		{
			bool ret = false;

			byte[] buf = new byte[59];
			disc.ReadLBA_2352_Flat(0x24D8, buf, 0, 59);
			string sig = System.Text.ASCIIEncoding.ASCII.GetString(buf);

			//this string is considered highly unlikely to exist anywhere besides a psx disc
			if (sig == "          Licensed  by          Sony Computer Entertainment")
				ret = true;

			return ret;
		}

		//we can only have one active core at a time, due to the lib being so static.
		//so we'll track the current one here and detach the previous one whenever a new one is booted up.
		static Octoshock CurrOctoshockCore;

		bool disposed = false;
		public void Dispose()
		{
			if (disposed) return;
			disposed = true;

			//BizHawk.Emulation.Consoles.Nintendo.SNES.LibsnesDll.snes_set_audio_sample(null);
		}

		public Octoshock(CoreComm comm)
		{
			var domains = new List<MemoryDomain>();
			CoreComm = comm;
			VirtualWidth = BufferWidth = 256;
			BufferHeight = 192;

			MemoryDomains = new MemoryDomainList(memoryDomains);
		}

		void Attach()
		{
			//attach this core as the current
			if (CurrOctoshockCore != null)
				CurrOctoshockCore.Dispose();
			CurrOctoshockCore = this;
		}

		//note to self: try to make mednafen have file IO callbacks into here: open, close, read, write.
		//we'll trick mednafen into using a virtual filesystem and track the fake files internally

		public void LoadCuePath(string path)
		{
			Attach();

			//note to self:
			//consider loading a fake cue, which is generated by our Disc class, and converting all reads to the fake bin to reads into the disc class.
			//thatd be pretty cool.... (may need to add an absolute byte range read method into the disc class, which can traverse the requisite LBAs)...
			//...but... are there other ideas?
			LibMednahawkDll.psx_LoadCue(path);
		}


		static Octoshock()
		{
			LibMednahawkDll.dll_Initialize();

			FopenCallback = new LibMednahawkDll.t_FopenCallback(FopenCallbackProc);
			FcloseCallback = new LibMednahawkDll.t_FcloseCallback(FcloseCallbackProc);
			FopCallback = new LibMednahawkDll.t_FopCallback(FopCallbackProc);
			LibMednahawkDll.dll_SetPropPtr(LibMednahawkDll.eProp.SetPtr_FopenCallback, Marshal.GetFunctionPointerForDelegate(FopenCallback));
			LibMednahawkDll.dll_SetPropPtr(LibMednahawkDll.eProp.SetPtr_FcloseCallback, Marshal.GetFunctionPointerForDelegate(FcloseCallback));
			LibMednahawkDll.dll_SetPropPtr(LibMednahawkDll.eProp.SetPtr_FopCallback, Marshal.GetFunctionPointerForDelegate(FopCallback));
		}

		static LibMednahawkDll.t_FopenCallback FopenCallback;
		static LibMednahawkDll.t_FcloseCallback FcloseCallback;
		static LibMednahawkDll.t_FopCallback FopCallback;

		class VirtualFile : IDisposable
		{
			public Stream stream;
			public int id;
			public void Dispose()
			{
				if(stream != null) stream.Dispose();
				stream = null;
			}
		}

		static Dictionary<int, VirtualFile> VirtualFiles = new Dictionary<int, VirtualFile>();

		static IntPtr FopenCallbackProc(string fname, string mode)
		{
			throw new NotImplementedException("Antiquated CoreComm.PSX_FirmwaresPath must be replaced by CoreFileProvider");

			// TODO - this should be using the CoreComm.CoreFileProvider interfaces

			//TODO - probably this should never really fail. but for now, mednafen tries to create a bunch of junk, so just return failure for files which cant be opened
			/*
			if (fname.StartsWith("$psx"))
			{
				string[] parts = fname.Split('/');
				if (parts[0] != "$psx") throw new InvalidOperationException("Octoshock using some weird path we dont handle yet");
				if (parts[1] == "firmware")
				{
					//fname = Path.Combine(CurrOctoshockCore.CoreComm.PSX_FirmwaresPath, parts[2]);
					if (!File.Exists(fname))
					{
						System.Windows.Forms.MessageBox.Show("the Octoshock core is referencing a firmware file which could not be found. Please make sure it's in your configured PSX firmwares folder. The referenced filename is: " + parts[1]);
					}
				}
			}

			Stream stream = null;
			if (mode == "rb") { if (File.Exists(fname)) stream = new FileStream(fname, FileMode.Open, FileAccess.Read, FileShare.Read); }
			else if (mode == "wb") stream = new FileStream(fname, FileMode.Create, FileAccess.Write, FileShare.Read);
			else throw new InvalidOperationException("unexpected virtual file mode from libmednahawk");

			if (stream == null) return IntPtr.Zero;

			//find a free id. dont use 0 because it looks like an error
			int id = 1;
			for (; ; )
			{
			RETRY:
				foreach (var vfid in VirtualFiles.Keys)
					if (vfid == id)
					{
						id++;
						goto RETRY;
					}
				break;
			}

			var ret = new VirtualFile();
			ret.id = id;
			ret.stream = stream;

			VirtualFiles[ret.id] = ret;
			return new IntPtr(ret.id);
			*/
		}
		static int FcloseCallbackProc(IntPtr fp)
		{
			int id = fp.ToInt32();
			VirtualFiles[id].stream.Dispose();
			VirtualFiles.Remove(id);
			return 0;
		}
		static byte[] fiobuf = new byte[10*1024];
		static long FopCallbackProc(int op, IntPtr ptr, long a, long b, IntPtr fp)
		{
			var vf = VirtualFiles[fp.ToInt32()];
			int amt = (int)(a*b);
			switch ((LibMednahawkDll.FOP)op)
			{
				case LibMednahawkDll.FOP.FOP_clearerr: return 0;
				case LibMednahawkDll.FOP.FOP_ferror: return 0;
				case LibMednahawkDll.FOP.FOP_fflush: vf.stream.Flush(); return 0;
				case LibMednahawkDll.FOP.FOP_fread:
					{
						if(fiobuf.Length < amt)
							fiobuf = new byte[amt];
						int read = vf.stream.Read(fiobuf, 0, amt);
						Marshal.Copy(fiobuf, 0, ptr, amt);
						return read / a;
					}
				case LibMednahawkDll.FOP.FOP_fseeko:
					vf.stream.Seek(a, (SeekOrigin)b);
					return vf.stream.Position;
				case LibMednahawkDll.FOP.FOP_ftello:
					return vf.stream.Position;
				case LibMednahawkDll.FOP.FOP_fwrite:
					{
						if (fiobuf.Length < amt)
							fiobuf = new byte[amt];
						Marshal.Copy(fiobuf, 0, ptr, amt);
						vf.stream.Write(fiobuf, 0, amt);
						return (int)b;
					}
				case LibMednahawkDll.FOP.FOP_size: return vf.stream.Length;
				default:
					throw new InvalidOperationException("INESTIMABLE GOPHER");
			}
		}


		public void ResetCounters()
		{
			// FIXME when all this stuff is implemented
			Frame = 0;
			LagCount = 0;
			//IsLagFrame = false;
		}

		public void FrameAdvance(bool render, bool rendersound)
		{
			LibMednahawkDll.psx_FrameAdvance();
			
			if (render == false) return;

			int w = LibMednahawkDll.dll_GetPropPtr(LibMednahawkDll.eProp.GetPtr_FramebufferWidth).ToInt32();
			int h = LibMednahawkDll.dll_GetPropPtr(LibMednahawkDll.eProp.GetPtr_FramebufferHeight).ToInt32();
			int p = LibMednahawkDll.dll_GetPropPtr(LibMednahawkDll.eProp.GetPtr_FramebufferPitchPixels).ToInt32();
			IntPtr iptr = LibMednahawkDll.dll_GetPropPtr(LibMednahawkDll.eProp.GetPtr_FramebufferPointer);
			void* ptr = iptr.ToPointer();


			VirtualWidth = BufferWidth = w;
			BufferHeight = h;

			int len = w*h;
			if (frameBuffer.Length != len)
				frameBuffer = new int[len];

			//todo - we could do the reformatting in the PSX core
			//better yet, we could send a buffer into the psx core before frame advance to use for outputting video to

			for (int y = 0, i = 0; y < h; y++)
				for (int x = 0; x < w; x++, i++)
				{
					frameBuffer[i] = (int)unchecked(((int*)ptr)[y * p + x] | (int)0xFF000000);
				}
		}
		public ControllerDefinition ControllerDefinition { get { return NullController; } }
		public IController Controller { get; set; }

		public int Frame { get; set; }
		public int LagCount { get { return 0; } set { return; } }
		public bool IsLagFrame { get { return false; } }

		public byte[] ReadSaveRam() { return null; }
		public void StoreSaveRam(byte[] data) { }
		public void ClearSaveRam() { }
		public bool DeterministicEmulation { get { return true; } }
		public bool SaveRamModified { get; set; }
		public void SaveStateText(TextWriter writer) { }
		public void LoadStateText(TextReader reader) { }
		public void SaveStateBinary(BinaryWriter writer) { }
		public void LoadStateBinary(BinaryReader reader) { }
		public byte[] SaveStateBinary() { return new byte[1]; }
		public bool BinarySaveStatesPreferred { get { return false; } }
		public int[] GetVideoBuffer() { return frameBuffer; }
		public int VirtualWidth { get; private set; }
		public int BufferWidth { get; private set; }
		public int BufferHeight { get; private set; }
		public int BackgroundColor { get { return 0; } }
		public void GetSamples(short[] samples) { }
		public void DiscardSamples() { }
		public int MaxVolume { get; set; }
		private List<MemoryDomain> memoryDomains = new List<MemoryDomain>();
		public MemoryDomainList MemoryDomains { get; private set; }
	}

}
