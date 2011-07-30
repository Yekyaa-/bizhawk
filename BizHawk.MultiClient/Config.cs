﻿namespace BizHawk.MultiClient
{
	public class Config
	{
		public Config()
		{
			SMSController[0] = new SMSControllerTemplate(true);
			SMSController[1] = new SMSControllerTemplate(false);
			PCEController[0] = new PCEControllerTemplate(true);
			PCEController[1] = new PCEControllerTemplate(false);
			PCEController[2] = new PCEControllerTemplate(false);
			PCEController[3] = new PCEControllerTemplate(false);
			PCEController[4] = new PCEControllerTemplate(false);
			NESController[0] = new NESControllerTemplate(true);
			NESController[1] = new NESControllerTemplate(false);
			NESController[2] = new NESControllerTemplate(false);
			NESController[3] = new NESControllerTemplate(false);
			GameBoyController = new NESControllerTemplate(true);
			TI83Controller[0] = new TI83ControllerTemplate(true);
		}

		// Directories
		public bool UseRecentForROMs = false;
		public string LastRomPath = ".";
		public string BasePath = ".";

		public string BaseNES = ".\\NES";
		public string PathNESROMs = ".";
		public string PathNESSavestates = ".\\State";
		public string PathNESSaveRAM = ".\\SaveRAM";
		public string PathNESScreenshots = ".\\Screenshots";
		public string PathNESCheats = ".\\Cheats";
		public string PathNESPalette = ".\\Palettes";

		public string BaseSMS = ".\\SMS";
		public string PathSMSROMs = ".";
		public string PathSMSSavestates = ".\\State";
		public string PathSMSSaveRAM = ".\\SaveRAM";
		public string PathSMSScreenshots = ".\\Screenshots";
		public string PathSMSCheats = ".\\Cheats";

		public string BaseGG = ".\\GG";
		public string PathGGROMs = ".";
		public string PathGGSavestates = ".\\State";
		public string PathGGSaveRAM = ".\\SaveRAM";
		public string PathGGScreenshots = ".\\Screenshots";
		public string PathGGCheats = ".\\Cheats";

		public string BaseSG = ".\\GG";
		public string PathSGROMs = ".";
		public string PathSGSavestates = ".\\State";
		public string PathSGSaveRAM = ".\\SaveRAM";
		public string PathSGScreenshots = ".\\Screenshots";
		public string PathSGCheats = ".\\Cheats";

		public string BaseGenesis = ".\\Genesis";
		public string PathGenesisROMs = ".";
		public string PathGenesisSavestates = ".\\State";
		public string PathGenesisSaveRAM = ".\\SaveRAM";
		public string PathGenesisScreenshots = ".\\Screenshots";
		public string PathGenesisCheats = ".\\Cheats";

		public string BasePCE = ".\\PCE";
		public string PathPCEROMs = ".";
		public string PathPCESavestates = ".\\State";
		public string PathPCESaveRAM = ".\\SaveRAM";
		public string PathPCEScreenshots = ".\\Screenshots";
		public string PathPCECheats = ".\\Cheats";

		public string BaseGameboy = ".\\Gameboy";
		public string PathGBROMs = ".";
		public string PathGBSavestates = ".\\State";
		public string PathGBSaveRAM = ".\\SaveRAM";
		public string PathGBScreenshots = ".\\Screenshots";
		public string PathGBCheats = ".\\Cheats";

		public string BaseTI83 = ".\\TI83";
		public string PathTI83ROMs = ".";
		public string PathTI83Savestates = ".\\State";
		public string PathTI83SaveRAM = ".\\SaveRAM";
		public string PathTI83Screenshots = ".\\Screenshots";
		public string PathTI83Cheats = ".\\Cheats";

		public string MoviesPath = ".\\Movies";
		public string LuaPath = ".\\Lua";
		public string WatchPath = ".";
		public string AVIPath = ".";


		//BIOS Paths
		public string PathPCEBios = ".\\PCECDBios.pce"; //TODO: better default filename

		public string FFMpegPath = "ffmpeg.exe";

		// General Client Settings
		public int TargetZoomFactor = 2;
		public bool AutoLoadMostRecentRom = false;
		public RecentFiles RecentRoms = new RecentFiles(8);
		public bool PauseWhenMenuActivated = true;
		public bool SaveWindowPosition = true;
		public bool StartPaused = false;
		public int MainWndx = -1; //Negative numbers will be ignored
		public int MainWndy = -1;
		public bool RunInBackground = true;
		public bool AcceptBackgroundInput = false;
		public bool SingleInstanceMode = false;
		public bool AllowUD_LR = false;
		public bool ShowContextMenu = true;
		public bool EnableBackupMovies = true;
		public bool HotkeyConfigAutoTab = true;
		public bool InputConfigAutoTab = true;
	    public bool ShowLogWindow = false;

		// Run-Control settings
		public int FrameProgressDelayMs = 500; //how long until a frame advance hold turns into a frame progress?
		public int FrameSkip = 4;
		public int SpeedPercent = 100;
		public int SpeedPercentAlternate = 400;
		public bool LimitFramerate = true;
		public bool AutoMinimizeSkipping = true;
		public bool DisplayVSync = false;
		public bool RewindEnabled = true;

		// Display options
		public int MessagesColor = -1;
		public int AlertMessageColor = -65536;
		public int LastInputColor = -23296;
		public int MovieColor = -65536;
		public int MovieInput = -8355712;
		public bool DisplayFPS = false;
		public int DispFPSx = 0;
		public int DispFPSy = 0;
		public int DispFPSanchor = 0;	//0 = UL, 1 = UR, 2 = DL, 3 = DR
		public bool DisplayFrameCounter = false;
		public int DispFrameCx = 0;
		public int DispFrameCy = 12;
		public int DispFrameanchor = 0;
		public bool DisplayLagCounter = false;
		public int DispLagx = 0;
		public int DispLagy = 36;
		public int DispLaganchor = 0;
		public bool DisplayInput = false;
		public int DispInpx = 0;
		public int DispInpy = 24;
		public int DispInpanchor = 0;
		public bool DisplayRerecordCount = false;
		public int DispRecx = 0;
		public int DispRecy = 48;
		public int DispRecanchor = 0;
		public int DispMultix = 36;
		public int DispMultiy = 0;
		public int DispMultianchor = 0;
		public bool ForceGDI = false;
		public bool DisplayStatusBar = true;

		// Sound options
		public bool SoundEnabled = true;
		public bool MuteFrameAdvance = true;
		public int SoundVolume = 100; //Range 0-100

		// Lua Console
		public RecentFiles RecentLua = new RecentFiles(8);
		public bool AutoLoadLua = false;
		public bool AutoLoadLuaConsole = false;
		public bool LuaConsoleSaveWindowPosition = true;
		public int LuaConsoleWndx = -1;   //Negative numbers will be ignored even with save window position set
		public int LuaConsoleWndy = -1;
		public int LuaConsoleWidth = -1;
		public int LuaConsoleHeight = -1;

		// RamWatch Settings
		public bool AutoLoadRamWatch = false;
		public RecentFiles RecentWatches = new RecentFiles(8);
		public bool RamWatchSaveWindowPosition = true;
		public int RamWatchWndx = -1;   //Negative numbers will be ignored even with save window position set
		public int RamWatchWndy = -1;
		public int RamWatchWidth = -1;
		public int RamWatchHeight = -1;
		public bool RamWatchShowChangeColumn = true;
		public bool RamWatchShowPrevColumn = false;
		public bool RamWatchShowChangeFromPrev = true;
		public int RamWatchAddressWidth = -1;
		public int RamWatchValueWidth = -1;
		public int RamWatchPrevWidth = -1;
		public int RamWatchChangeWidth = -1;
		public int RamWatchNotesWidth = -1;
		public int RamWatchAddressIndex = 0;
		public int RamWatchValueIndex = 1;
		public int RamWatchPrevIndex = 2;
		public int RamWatchChangeIndex = 3;
		public int RamWatchNotesIndex = 4;

		// RamSearch Settings
		public bool AutoLoadRamSearch = false;
		public bool RamSearchSaveWindowPosition = true;
		public RecentFiles RecentSearches = new RecentFiles(8);
		public int RamSearchWndx = -1;   //Negative numbers will be ignored even with save window position set
		public int RamSearchWndy = -1;
		public int RamSearchWidth = -1;  //Negative numbers will be ignored
		public int RamSearchHeight = -1;
		public int RamSearchPreviousAs = 0;
		public bool RamSearchPreviewMode = true;
		public bool AlwaysExludeRamWatch = false;
		public int RamSearchAddressWidth = -1;
		public int RamSearchValueWidth = -1;
		public int RamSearchPrevWidth = -1;
		public int RamSearchChangesWidth = -1;
		public int RamSearchAddressIndex = 0;
		public int RamSearchValueIndex = 1;
		public int RamSearchPrevIndex = 2;
		public int RamSearchChangesIndex = 3;

		// HexEditor Settings
		public bool AutoLoadHexEditor = false;
		public bool HexEditorSaveWindowPosition = true;
		public int HexEditorWndx = -1;  //Negative numbers will be ignored even with save window position set
		public int HexEditorWndy = -1;
		public int HexEditorWidth = -1;
		public int HexEditorHeight = -1;

		// NESPPU Settings
		public bool AutoLoadNESPPU = false;
		public bool NESPPUSaveWindowPosition = true;
		public int NESPPUWndx = -1;
		public int NESPPUWndy = -1;

		// NESDebuger Settings
		public bool AutoLoadNESDebugger = false;
		public bool NESDebuggerSaveWindowPosition = true;
		public int NESDebuggerWndx = -1;
		public int NESDebuggerWndy = -1;
		public int NESDebuggerWidth = -1;
		public int NESDebuggerHeight = -1;

		// NESNameTableViewer Settings
		public bool AutoLoadNESNameTable = false;
		public bool NESNameTableSaveWindowPosition = true;
		public int NESNameTableWndx = -1;
		public int NESNameTableWndy = -1;

		// NES Graphics settings
		public bool NESAllowMoreThanEightSprites = false;
		public bool NESClipLeftAndRight = false;
		public bool NESAutoLoadPalette = true;
		public bool NESDispBackground = true;
		public bool NESDispSprites = true;
		public int NESBackgroundColor = 0;
		public string NESPaletteFile = "";

		//GB Debugger settings
		public bool AutoloadGBDebugger = false;
		public bool GBDebuggerSaveWindowPosition = true;

		// Cheats Dialog
		public bool AutoLoadCheats = false;
		public bool CheatsSaveWindowPosition = true;
		public bool DisableCheatsOnLoad = false;
		public bool LoadCheatFileByGame = true;
		public bool CheatsAutoSaveOnClose = true;
		public RecentFiles RecentCheats = new RecentFiles(8);
		public int CheatsWndx = -1;
		public int CheatsWndy = -1;
		public int CheatsWidth = -1;
		public int CheatsHeight = -1;
		public int CheatsNameWidth = -1;
		public int CheatsAddressWidth = -1;
		public int CheatsValueWidth = -1;
		public int CheatsDomainWidth = -1;
		public int CheatsOnWidth = -1;
		public int CheatsNameIndex = 0;
		public int CheatsAddressIndex = 1;
		public int CheatsValueIndex = 2;
		public int CheatsDomainIndex = 3;
		public int CheatsOnIndex = 4;


		// TAStudio Dialog
		public bool TAStudioSaveWindowPosition = true;
		public bool AutoloadTAStudio = false;
		public int TASWndx = -1;
		public int TASWndy = -1;
		public int TASWidth = -1;
		public int TASHeight = -1;
		public bool TASUpdatePads = true;

		// NES Game Genie Encoder/Decoder
		public bool NESGGAutoload = false;
		public bool NESGGSaveWindowPosition = true;
		public int NESGGWndx = -1;
		public int NESGGWndy = -1;

		//Movie Settings
		public RecentFiles RecentMovies = new RecentFiles(8);
		public bool AutoLoadMostRecentMovie = false;
		public bool BindSavestatesToMovies = true;
		public string DefaultAuthor = "default user";
		public bool UseDefaultAuthor = true;
		public bool DisplaySubtitles = true;

		//Play Movie Dialog
		public bool PlayMovie_IncludeSubdir = true;
		public bool PlayMovie_ShowStateFiles = false;

		//TI83
		public bool TI83autoloadKeyPad = true;
		public bool TI83KeypadSaveWindowPosition = true;
		public int TI83KeyPadWndx = -1;
		public int TI83KeyPadWndy = -1;
		public bool TI83ToolTips = true;

		// Client Hotkey Bindings
		public string HardResetBinding = "Ctrl+R";
		public string FastForwardBinding = "Tab, J1 B6";
		public string RewindBinding = "Shift+R, J1 B5";
		public string EmulatorPauseBinding = "Pause";
		public string FrameAdvanceBinding = "F";
		public string TurboBinding = "Shift+Tab";
		public string ScreenshotBinding = "F12";
		public string ToggleFullscreenBinding = "Alt+Return";
		public string QuickSave = "I";
		public string QuickLoad = "P";
		public string SelectSlot0 = "D0";
		public string SelectSlot1 = "D1";
		public string SelectSlot2 = "D2";
		public string SelectSlot3 = "D3";
		public string SelectSlot4 = "D4";
		public string SelectSlot5 = "D5";
		public string SelectSlot6 = "D6";
		public string SelectSlot7 = "D7";
		public string SelectSlot8 = "D8";
		public string SelectSlot9 = "D9";
		public string SaveSlot0 = "Shift+F10";
		public string SaveSlot1 = "Shift+F1";
		public string SaveSlot2 = "Shift+F2";
		public string SaveSlot3 = "Shift+F3";
		public string SaveSlot4 = "Shift+F4";
		public string SaveSlot5 = "Shift+F5";
		public string SaveSlot6 = "Shift+F6";
		public string SaveSlot7 = "Shift+F7";
		public string SaveSlot8 = "Shift+F8";
		public string SaveSlot9 = "Shift+F9";
		public string LoadSlot0 = "F10";
		public string LoadSlot1 = "F1";
		public string LoadSlot2 = "F2";
		public string LoadSlot3 = "F3";
		public string LoadSlot4 = "F4";
		public string LoadSlot5 = "F5";
		public string LoadSlot6 = "F6";
		public string LoadSlot7 = "F7";
		public string LoadSlot8 = "F8";
		public string LoadSlot9 = "F9";
		public string ToolBox = "T";
		public string SaveNamedState = "";
		public string LoadNamedState = "";
		public string PreviousSlot = "";
		public string NextSlot = "";
		public string RamWatch = "";
		public string RamSearch = "";
		public string RamPoke = "";
		public string HexEditor = "";
		public string LuaConsole = "";
		public string Cheats = "";
		public string OpenROM = "Ctrl+O";
		public string CloseROM = "Ctrl+W";
		public string FrameCounterBinding = "";
		public string FPSBinding = "";
		public string LagCounterBinding = "";
		public string InputDisplayBinding = "";
		public string ReadOnlyToggleBinding = "Q";
		public string PlayMovieBinding = "";
		public string RecordMovieBinding = "";
		public string StopMovieBinding = "";
		public string PlayBeginningBinding = "";
		public string VolUpBinding = "";
		public string VolDownBinding = "";
		public string SoftResetBinding = "";
		public string ToggleMultiTrack = "";
		public string MTRecordAll = "";
		public string MTRecordNone = "";
		public string MTIncrementPlayer = "";
		public string MTDecrementPlayer = "";
		public string AVIRecordBinding = "";
		public string AVIStopBinding = "";
		
		// SMS / GameGear Settings
		public bool SmsEnableFM = true;
		public bool SmsAllowOverlock = false;
		public bool SmsForceStereoSeparation = false;

		public string SmsReset = "C";
		public string SmsPause = "V, J1 B8";
		public SMSControllerTemplate[] SMSController = new SMSControllerTemplate[2];

		// PCEngine Settings
		public PCEControllerTemplate[] PCEController = new PCEControllerTemplate[5];

		// Genesis Settings
		public string GenP1Up = "UpArrow, J1 Up";
		public string GenP1Down = "DownArrow, J1 Down";
		public string GenP1Left = "LeftArrow, J1 Left";
		public string GenP1Right = "RightArrow, J1 Right";
		public string GenP1A = "Z, J1 B1";
		public string GenP1B = "X, J1 B3";
		public string GenP1C = "C, J1 B4";
		public string GenP1Start = "Return, J1 B8";

		//GameBoy Settings
		public NESControllerTemplate GameBoyController = new NESControllerTemplate(true);

		//NES settings
		public string NESReset = "Backspace";
		public NESControllerTemplate[] NESController = new NESControllerTemplate[4];

		//TI 83 settings
		public TI83ControllerTemplate[] TI83Controller = new TI83ControllerTemplate[1];

		//GB settings
		public GBControllerTemplate GBController = new GBControllerTemplate();
	}

	public class SMSControllerTemplate
	{
		public string Up;
		public string Down;
		public string Left;
		public string Right;
		public string B1;
		public string B2;
		public bool Enabled;
		public SMSControllerTemplate() { }
		public SMSControllerTemplate(bool defaults)
		{
			if (defaults)
			{
				Enabled = true;
				Up = "UpArrow, J1 Up";
				Down = "DownArrow, J1 Down";
				Left = "LeftArrow, J1 Left";
				Right = "RightArrow, J1 Right";
				B1 = "Z, J1 B1";
				B2 = "X, J1 B2";
			}
			else
			{
				Enabled = false;
				Up = "";
				Down = "";
				Right = "";
				Left = "";
				B1 = "";
				B2 = "";
			}
		}
	}

	public class PCEControllerTemplate
	{
		public string Up;
		public string Down;
		public string Left;
		public string Right;
		public string I;
		public string II;
		public string Run;
		public string Select;
		public bool Enabled;
		public PCEControllerTemplate() { }
		public PCEControllerTemplate(bool defaults)
		{
			if (defaults)
			{
				Enabled = true;
				Up = "UpArrow, J1 Up";
				Down = "DownArrow, J1 Down";
				Left = "LeftArrow, J1 Left";
				Right = "RightArrow, J1 Right";
				I = "Z, J1 B2";
				II = "X, J1 B1";
				Run = "C, J1 B8";
				Select = "V, J1 B7";
			}
			else
			{
				Enabled = false;
				Up = "";
				Down = "";
				Right = "";
				Left = "";
				I = "";
				II = "";
				Run = "";
				Select = "";
			}
		}
	}

	public class NESControllerTemplate
	{
		public string Up;
		public string Down;
		public string Left;
		public string Right;
		public string A;
		public string B;
		public string Start;
		public string Select;
		public bool Enabled;
		public NESControllerTemplate() { }
		public NESControllerTemplate(bool defaults)
		{
			if (defaults)
			{
				Enabled = true;
				Up = "UpArrow, J1 Up";
				Down = "DownArrow, J1 Down";
				Left = "LeftArrow, J1 Left";
				Right = "RightArrow, J1 Right";
				A = "X, J1 B2";
				B = "Z, J1 B1";
				Start = "Return, J1 B8";
				Select = "Space, J1 B7";
			}
			else
			{
				Enabled = false;
				Up = "";
				Down = "";
				Right = "";
				Left = "";
				A = "";
				B = "";
				Start = "";
				Select = "";
			}
		}
	}

	public class GBControllerTemplate
	{
		public string Up;
		public string Down;
		public string Left;
		public string Right;
		public string A;
		public string B;
		public string Start;
		public string Select;
		public bool Enabled;
		public GBControllerTemplate() { }
		public GBControllerTemplate(bool defaults)
		{
			if (defaults)
			{
				Enabled = true;
				Up = "UpArrow, J1 Up";
				Down = "DownArrow, J1 Down";
				Left = "LeftArrow, J1 Left";
				Right = "RightArrow, J1 Right";
				A = "X, J1 B2";
				B = "Z, J1 B1";
				Start = "Return, J1 B8";
				Select = "Space, J1 B7";
			}
			else
			{
				Enabled = false;
				Up = "";
				Down = "";
				Right = "";
				Left = "";
				A = "";
				B = "";
				Start = "";
				Select = "";
			}
		}
	}

	public class TI83ControllerTemplate
	{
		public string _0;
		public string _1;
		public string _2;
		public string _3;
		public string _4;
		public string _5;
		public string _6;
		public string _7;
		public string _8;
		public string _9;
		public string DOT;
		public string ON;
		public string ENTER;
		public string DOWN;
		public string UP;
		public string LEFT;
		public string RIGHT;
		public string PLUS;
		public string MINUS;
		public string MULTIPLY;
		public string DIVIDE;
		public string CLEAR;
		public string EXP;
		public string DASH;
		public string PARACLOSE;
		public string TAN;
		public string VARS;
		public string PARAOPEN;
		public string COS;
		public string PRGM;
		public string STAT;
		public string SIN;
		public string MATRIX;
		public string X;
		public string STO;
		public string LN;
		public string LOG;
		public string SQUARED;
		public string NEG1;
		public string MATH;
		public string ALPHA;
		public string GRAPH;
		public string TRACE;
		public string ZOOM;
		public string WINDOW;
		public string Y;
		public string SECOND;
		public string MODE;
		public string DEL;
		public TI83ControllerTemplate() { }
		public bool Enabled;
		public string COMMA;
		public TI83ControllerTemplate(bool defaults)
		{
			if (defaults)
			{
				Enabled = true;
				_0 = "NumberPad0";      //0
				_1 = "NumberPad1";      //1
				_2 = "NumberPad2";      //2
				_3 = "NumberPad3";      //3
				_4 = "NumberPad4";      //4
				_5 = "NumberPad5";      //5
				_6 = "NumberPad6";      //6
				_7 = "NumberPad7";      //7
				_8 = "NumberPad8";      //8
				_9 = "NumberPad9";      //9
				DOT = "NumberPadPeriod";//10
				ON = "Space";           //11
				ENTER = "Return";       //12
				UP = "UpArrow";         //13
				DOWN = "DownArrow";     //14
				LEFT = "LeftArrow";     //15
				RIGHT = "RightArrow";   //16
				PLUS = "NumberPadPlus"; //17
				MINUS = "NumberPadMinus";     //18
				MULTIPLY = "NumberPadStar";   //19
				DIVIDE = "NumberPadSlash";    //20
				CLEAR = "Escape";       //21
				EXP = "6";              //22
				DASH = "Minus";         //23
				PARACLOSE = "0";        //24
				PARAOPEN = "9";         //25
				TAN = "T";              //26
				VARS = "V";             //27
				COS = "C";              //28
				PRGM = "R";             //29
				STAT = "S";             //30
				MATRIX = "LeftBracket"; //31
				X = "X";                //32
				STO = "Insert";         //33
				LN = "L";               //34
				LOG = "O";              //35
				SQUARED = "2";          //36
				NEG1 = "1";             //37
				MATH = "M";             //38
				ALPHA = "A";            //39
				GRAPH = "G";            //40
				TRACE = "Home";         //41
				ZOOM = "Z";             //42
				WINDOW = "W";           //43
				Y = "Y";                //44
				SECOND = "Slash";       //45
				MODE = "BackSlash";     //46
				DEL = "Delete";         //47
				COMMA = "Comma";        //48
				SIN = "Period";         //49
			}
			else
			{
				Enabled = false;
				_0 = "";
				_1 = "";
				_2 = "";
				_3 = "";
				_4 = "";
				_5 = "";
				_6 = "";
				_7 = "";
				_8 = "";
				_9 = "";
				DOT = "";
				ON = "";
				ENTER = "";
				UP = "";
				DOWN = "";
				LEFT = "";
				RIGHT = "";
				PLUS = "";
				MINUS = "";
				MULTIPLY = "";
				DIVIDE = "";
				CLEAR = "";
				EXP = "";
				DASH = "";
				PARACLOSE = "";
				TAN = "";
				VARS = "";
				PARAOPEN = "";
				COS = "";
				PRGM = "";
				STAT = "";
				SIN = "";
				MATRIX = "";
				X = "";
				STO = "";
				LN = "";
				LOG = "";
				SQUARED = "";
				NEG1 = "";
				MATH = "";
				ALPHA = "";
				GRAPH = "";
				TRACE = "";
				ZOOM = "";
				WINDOW = "";
				Y = "";
				SECOND = "";
				MODE = "";
				DEL = "";
				COMMA = "";
			}
		}
	}
}