﻿using System;
using System.Text;
using System.Windows.Forms;

namespace BizHawk.MultiClient
{
	public partial class VirtualPadC64Keyboard : UserControl , IVirtualPad
	{
		public VirtualPadC64Keyboard()
		{
			InitializeComponent();
		}

		private void Buttons_CheckedChanged(object sender, EventArgs e)
		{
			if (GlobalWinF.Emulator.SystemId != "C64")
			{
				return;
			}
			else if (sender == KF1)
			{
				GlobalWinF.StickyXORAdapter.SetSticky("Key F1", KF1.Checked);
			}
			else if (sender == KF3)
			{
				GlobalWinF.StickyXORAdapter.SetSticky("Key F3", KF3.Checked);
			}
			else if (sender == KF5)
			{
				GlobalWinF.StickyXORAdapter.SetSticky("Key F5", KF5.Checked);
			}
			else if (sender == KF7)
			{
				GlobalWinF.StickyXORAdapter.SetSticky("Key F7", KF7.Checked);
			}
			else if (sender == KLeftArrow)
			{
				GlobalWinF.StickyXORAdapter.SetSticky("Key Left Arrow", KLeftArrow.Checked);
			}
			else if (sender == K1)
			{
				GlobalWinF.StickyXORAdapter.SetSticky("Key 1", K1.Checked);
			}
			else if (sender == K2)
			{
				GlobalWinF.StickyXORAdapter.SetSticky("Key 2", K2.Checked);
			}
			else if (sender == K3)
			{
				GlobalWinF.StickyXORAdapter.SetSticky("Key 3", K3.Checked);
			}
			else if (sender == K4)
			{
				GlobalWinF.StickyXORAdapter.SetSticky("Key 4", K4.Checked);
			}
			else if (sender == K5)
			{
				GlobalWinF.StickyXORAdapter.SetSticky("Key 5", K5.Checked);
			}
			else if (sender == K6)
			{
				GlobalWinF.StickyXORAdapter.SetSticky("Key 6", K6.Checked);
			}
			else if (sender == K7)
			{
				GlobalWinF.StickyXORAdapter.SetSticky("Key 7", K7.Checked);
			}
			else if (sender == K8)
			{
				GlobalWinF.StickyXORAdapter.SetSticky("Key 8", K8.Checked);
			}
			else if (sender == K9)
			{
				GlobalWinF.StickyXORAdapter.SetSticky("Key 9", K9.Checked);
			}
			else if (sender == K0)
			{
				GlobalWinF.StickyXORAdapter.SetSticky("Key 0", K0.Checked);
			}
			else if (sender == KPlus)
			{
				GlobalWinF.StickyXORAdapter.SetSticky("Key Plus", KPlus.Checked);
			}
			else if (sender == KMinus)
			{
				GlobalWinF.StickyXORAdapter.SetSticky("Key Minus", KMinus.Checked);
			}
			else if (sender == KPound)
			{
				GlobalWinF.StickyXORAdapter.SetSticky("Key Pound", KPound.Checked);
			}
			else if (sender == KClear)
			{
				GlobalWinF.StickyXORAdapter.SetSticky("Key Clear/Home", KClear.Checked);
			}
			else if (sender == KInsert)
			{
				GlobalWinF.StickyXORAdapter.SetSticky("Key Insert/Delete", KInsert.Checked);
			}
			else if (sender == KCtrl)
			{
				GlobalWinF.StickyXORAdapter.SetSticky("Key Control", KCtrl.Checked);
			}
			else if (sender == KQ)
			{
				GlobalWinF.StickyXORAdapter.SetSticky("Key Q", KQ.Checked);
			}
			else if (sender == KW)
			{
				GlobalWinF.StickyXORAdapter.SetSticky("Key W", KW.Checked);
			}
			else if (sender == KE)
			{
				GlobalWinF.StickyXORAdapter.SetSticky("Key E", KE.Checked);
			}
			else if (sender == KR)
			{
				GlobalWinF.StickyXORAdapter.SetSticky("Key R", KR.Checked);
			}
			else if (sender == KT)
			{
				GlobalWinF.StickyXORAdapter.SetSticky("Key T", KT.Checked);
			}
			else if (sender == KY)
			{
				GlobalWinF.StickyXORAdapter.SetSticky("Key Y", KY.Checked);
			}
			else if (sender == KU)
			{
				GlobalWinF.StickyXORAdapter.SetSticky("Key U", KU.Checked);
			}
			else if (sender == KI)
			{
				GlobalWinF.StickyXORAdapter.SetSticky("Key I", KI.Checked);
			}
			else if (sender == KO)
			{
				GlobalWinF.StickyXORAdapter.SetSticky("Key O", KO.Checked);
			}
			else if (sender == KP)
			{
				GlobalWinF.StickyXORAdapter.SetSticky("Key P", KP.Checked);
			}
			else if (sender == KAt)
			{
				GlobalWinF.StickyXORAdapter.SetSticky("Key At", KAt.Checked);
			}
			else if (sender == KAsterisk)
			{
				GlobalWinF.StickyXORAdapter.SetSticky("Key Asterisk", KAsterisk.Checked);
			}
			else if (sender == KUpArrow)
			{
				GlobalWinF.StickyXORAdapter.SetSticky("Key Up Arrow", KUpArrow.Checked);
			}
			else if (sender == KRST)
			{
				GlobalWinF.StickyXORAdapter.SetSticky("Key Restore", KRST.Checked);
			}
			else if (sender == KRun)
			{
				GlobalWinF.StickyXORAdapter.SetSticky("Key Run/Stop", KRun.Checked);
			}
			else if (sender == KLck)
			{
				GlobalWinF.StickyXORAdapter.SetSticky("Key Lck", KLck.Checked);
			}
			else if (sender == KA)
			{
				GlobalWinF.StickyXORAdapter.SetSticky("Key A", KA.Checked);
			}
			else if (sender == KS)
			{
				GlobalWinF.StickyXORAdapter.SetSticky("Key S", KS.Checked);
			}
			else if (sender == KD)
			{
				GlobalWinF.StickyXORAdapter.SetSticky("Key D", KD.Checked);
			}
			else if (sender == KF)
			{
				GlobalWinF.StickyXORAdapter.SetSticky("Key F", KF.Checked);
			}
			else if (sender == KG)
			{
				GlobalWinF.StickyXORAdapter.SetSticky("Key G", KG.Checked);
			}
			else if (sender == KH)
			{
				GlobalWinF.StickyXORAdapter.SetSticky("Key H", KH.Checked);
			}
			else if (sender == KJ)
			{
				GlobalWinF.StickyXORAdapter.SetSticky("Key J", KJ.Checked);
			}
			else if (sender == KK)
			{
				GlobalWinF.StickyXORAdapter.SetSticky("Key K", KK.Checked);
			}
			else if (sender == KL)
			{
				GlobalWinF.StickyXORAdapter.SetSticky("Key L", KL.Checked);
			}
			else if (sender == KColon)
			{
				GlobalWinF.StickyXORAdapter.SetSticky("Key Colon", KColon.Checked);
			}
			else if (sender == KSemicolon)
			{
				GlobalWinF.StickyXORAdapter.SetSticky("Key Semicolon", KSemicolon.Checked);
			}
			else if (sender == KEquals)
			{
				GlobalWinF.StickyXORAdapter.SetSticky("Key Equal", KEquals.Checked);
			}
			else if (sender == KReturn)
			{
				GlobalWinF.StickyXORAdapter.SetSticky("Key Return", KReturn.Checked);
			}
			else if (sender == KCommodore)
			{
				GlobalWinF.StickyXORAdapter.SetSticky("Key Commodore", KCommodore.Checked);
			}
			else if (sender == KLeftShift)
			{
				GlobalWinF.StickyXORAdapter.SetSticky("Key Left Shift", KLeftShift.Checked);
			}
			else if (sender == KZ)
			{
				GlobalWinF.StickyXORAdapter.SetSticky("Key Z", KZ.Checked);
			}
			else if (sender == KX)
			{
				GlobalWinF.StickyXORAdapter.SetSticky("Key X", KX.Checked);
			}
			else if (sender == KC)
			{
				GlobalWinF.StickyXORAdapter.SetSticky("Key C", KC.Checked);
			}
			else if (sender == KV)
			{
				GlobalWinF.StickyXORAdapter.SetSticky("Key V", KV.Checked);
			}
			else if (sender == KB)
			{
				GlobalWinF.StickyXORAdapter.SetSticky("Key B", KB.Checked);
			}
			else if (sender == KN)
			{
				GlobalWinF.StickyXORAdapter.SetSticky("Key N", KN.Checked);
			}
			else if (sender == KM)
			{
				GlobalWinF.StickyXORAdapter.SetSticky("Key M", KM.Checked);
			}
			else if (sender == KComma)
			{
				GlobalWinF.StickyXORAdapter.SetSticky("Key Comma", KComma.Checked);
			}
			else if (sender == KSemicolon)
			{
				GlobalWinF.StickyXORAdapter.SetSticky("Key Semicolon", KSemicolon.Checked);
			}
			else if (sender == KEquals)
			{
				GlobalWinF.StickyXORAdapter.SetSticky("Key Equal", KEquals.Checked);
			}
			else if (sender == KReturn)
			{
				GlobalWinF.StickyXORAdapter.SetSticky("Key Return", KReturn.Checked);
			}
			else if (sender == KCommodore)
			{
				GlobalWinF.StickyXORAdapter.SetSticky("Key Commodore", KCommodore.Checked);
			}
			else if (sender == KLeftShift)
			{
				GlobalWinF.StickyXORAdapter.SetSticky("Key Left Shift", KLeftShift.Checked);
			}
			else if (sender == KZ)
			{
				GlobalWinF.StickyXORAdapter.SetSticky("Key Z", KZ.Checked);
			}
			else if (sender == KX)
			{
				GlobalWinF.StickyXORAdapter.SetSticky("Key X", KX.Checked);
			}
			else if (sender == KC)
			{
				GlobalWinF.StickyXORAdapter.SetSticky("Key C", KC.Checked);
			}
			else if (sender == KV)
			{
				GlobalWinF.StickyXORAdapter.SetSticky("Key V", KV.Checked);
			}
			else if (sender == KB)
			{
				GlobalWinF.StickyXORAdapter.SetSticky("Key B", KB.Checked);
			}
			else if (sender == KN)
			{
				GlobalWinF.StickyXORAdapter.SetSticky("Key N", KN.Checked);
			}
			else if (sender == KM)
			{
				GlobalWinF.StickyXORAdapter.SetSticky("Key M", KM.Checked);
			}
			else if (sender == KComma)
			{
				GlobalWinF.StickyXORAdapter.SetSticky("Key Comma", KComma.Checked);
			}
			else if (sender == KPeriod)
			{
				GlobalWinF.StickyXORAdapter.SetSticky("Key Period", KPeriod.Checked);
			}
			else if (sender == KSlash)
			{
				GlobalWinF.StickyXORAdapter.SetSticky("Key Slash", KSlash.Checked);
			}
			else if (sender == KRightShift)
			{
				GlobalWinF.StickyXORAdapter.SetSticky("Key Right Shift", KRightShift.Checked);
			}
			else if (sender == KCursorUp)
			{
				GlobalWinF.StickyXORAdapter.SetSticky("Key Cursor Up/Down", KCursorUp.Checked);
			}
			else if (sender == KCursorLeft)
			{
				GlobalWinF.StickyXORAdapter.SetSticky("Key Cursor Left/Right", KCursorLeft.Checked);
			}
			else if (sender == KSpace)
			{
				GlobalWinF.StickyXORAdapter.SetSticky("Key Space", KSpace.Checked);
			}
		}

		public void Clear()
		{
			if (GlobalWinF.Emulator.SystemId != "C64")
			{
				return;
			}
			else
			{
				if (KF1.Checked) GlobalWinF.StickyXORAdapter.SetSticky("Key F1", false);
				if (KF3.Checked) GlobalWinF.StickyXORAdapter.SetSticky("Key F3", false);
				if (KF5.Checked) GlobalWinF.StickyXORAdapter.SetSticky("Key F5", false);
				if (KF7.Checked) GlobalWinF.StickyXORAdapter.SetSticky("Key F7", false);
				if (KLeftArrow.Checked) GlobalWinF.StickyXORAdapter.SetSticky("Key Left Arrow", false);
				if (K1.Checked) GlobalWinF.StickyXORAdapter.SetSticky("Key 1", false);
				if (K2.Checked) GlobalWinF.StickyXORAdapter.SetSticky("Key 2", false);
				if (K3.Checked) GlobalWinF.StickyXORAdapter.SetSticky("Key 3", false);
				if (K4.Checked) GlobalWinF.StickyXORAdapter.SetSticky("Key 4", false);
				if (K5.Checked) GlobalWinF.StickyXORAdapter.SetSticky("Key 5", false);
				if (K6.Checked) GlobalWinF.StickyXORAdapter.SetSticky("Key 6", false);
				if (K7.Checked) GlobalWinF.StickyXORAdapter.SetSticky("Key 7", false);
				if (K8.Checked) GlobalWinF.StickyXORAdapter.SetSticky("Key 8", false);
				if (K9.Checked) GlobalWinF.StickyXORAdapter.SetSticky("Key 9", false);
				if (K0.Checked) GlobalWinF.StickyXORAdapter.SetSticky("Key Plus", false);
				if (KPlus.Checked) GlobalWinF.StickyXORAdapter.SetSticky("Key Minus", false);
				if (KMinus.Checked) GlobalWinF.StickyXORAdapter.SetSticky("Key Pound", false);
				if (KPound.Checked) GlobalWinF.StickyXORAdapter.SetSticky("Key Clear/Home", false);
				if (KClear.Checked) GlobalWinF.StickyXORAdapter.SetSticky("Key Insert/Delete", false);
				if (KInsert.Checked) GlobalWinF.StickyXORAdapter.SetSticky("Key Control", false);
				if (KCtrl.Checked) GlobalWinF.StickyXORAdapter.SetSticky("Key Q", false);
				if (KQ.Checked) GlobalWinF.StickyXORAdapter.SetSticky("Key W", false);
				if (KW.Checked) GlobalWinF.StickyXORAdapter.SetSticky("Key E", false);
				if (KE.Checked) GlobalWinF.StickyXORAdapter.SetSticky("Key R", false);
				if (KR.Checked) GlobalWinF.StickyXORAdapter.SetSticky("Key T", false);
				if (KT.Checked) GlobalWinF.StickyXORAdapter.SetSticky("Key Y", false);
				if (KY.Checked) GlobalWinF.StickyXORAdapter.SetSticky("Key U", false);
				if (KU.Checked) GlobalWinF.StickyXORAdapter.SetSticky("Key I", false);
				if (KI.Checked) GlobalWinF.StickyXORAdapter.SetSticky("Key O", false);
				if (KO.Checked) GlobalWinF.StickyXORAdapter.SetSticky("Key P", false);
				if (KP.Checked) GlobalWinF.StickyXORAdapter.SetSticky("Key At", false);
				if (KAt.Checked) GlobalWinF.StickyXORAdapter.SetSticky("Key Asterisk", false);
				if (KAsterisk.Checked) GlobalWinF.StickyXORAdapter.SetSticky("Key Up Arrow", false);
				if (KUpArrow.Checked) GlobalWinF.StickyXORAdapter.SetSticky("Key Restore", false);
				if (KRST.Checked) GlobalWinF.StickyXORAdapter.SetSticky("Key Run/Stop", false);
				if (KRun.Checked) GlobalWinF.StickyXORAdapter.SetSticky("Key Lck", false);
				if (KLck.Checked) GlobalWinF.StickyXORAdapter.SetSticky("Key A", false);
				if (KA.Checked) GlobalWinF.StickyXORAdapter.SetSticky("Key S", false);
				if (KS.Checked) GlobalWinF.StickyXORAdapter.SetSticky("Key D", false);
				if (KD.Checked) GlobalWinF.StickyXORAdapter.SetSticky("Key F", false);
				if (KF.Checked) GlobalWinF.StickyXORAdapter.SetSticky("Key G", false);
				if (KG.Checked) GlobalWinF.StickyXORAdapter.SetSticky("Key H", false);
				if (KH.Checked) GlobalWinF.StickyXORAdapter.SetSticky("Key J", false);
				if (KJ.Checked) GlobalWinF.StickyXORAdapter.SetSticky("Key K", false);
				if (KK.Checked) GlobalWinF.StickyXORAdapter.SetSticky("Key L", false);
				if (KL.Checked) GlobalWinF.StickyXORAdapter.SetSticky("Key Colon", false);
				if (KColon.Checked) GlobalWinF.StickyXORAdapter.SetSticky("Key Semicolon", false);
				if (KSemicolon.Checked) GlobalWinF.StickyXORAdapter.SetSticky("Key Equal", false);
				if (KEquals.Checked) GlobalWinF.StickyXORAdapter.SetSticky("Key Return", false);
				if (KReturn.Checked) GlobalWinF.StickyXORAdapter.SetSticky("Key Commodore", false);
				if (KCommodore.Checked) GlobalWinF.StickyXORAdapter.SetSticky("Key Left Shift", false);
				if (KLeftShift.Checked) GlobalWinF.StickyXORAdapter.SetSticky("Key Z", false);
				if (KZ.Checked) GlobalWinF.StickyXORAdapter.SetSticky("Key X", false);
				if (KX.Checked) GlobalWinF.StickyXORAdapter.SetSticky("Key C", false);
				if (KC.Checked) GlobalWinF.StickyXORAdapter.SetSticky("Key V", false);
				if (KV.Checked) GlobalWinF.StickyXORAdapter.SetSticky("Key B", false);
				if (KB.Checked) GlobalWinF.StickyXORAdapter.SetSticky("Key N", false);
				if (KN.Checked) GlobalWinF.StickyXORAdapter.SetSticky("Key M", false);
				if (KM.Checked) GlobalWinF.StickyXORAdapter.SetSticky("Key Comma", false);
				if (KComma.Checked) GlobalWinF.StickyXORAdapter.SetSticky("Key Period", false);
				if (KPeriod.Checked) GlobalWinF.StickyXORAdapter.SetSticky("Key Slash", false);
				if (KSlash.Checked) GlobalWinF.StickyXORAdapter.SetSticky("Key Right Shift", false);
				if (KRightShift.Checked) GlobalWinF.StickyXORAdapter.SetSticky("Key Cursor Up/Down", false);
				if (KCursorUp.Checked) GlobalWinF.StickyXORAdapter.SetSticky("Key Cursor Left/Right", false);
				if (KCursorLeft.Checked) GlobalWinF.StickyXORAdapter.SetSticky("Key Space", false);

				KF1.Checked = false;
				KF3.Checked = false;
				KF5.Checked = false;
				KF7.Checked = false;
				KLeftArrow.Checked = false;
				K1.Checked = false;
				K2.Checked = false;
				K3.Checked = false;
				K4.Checked = false;
				K5.Checked = false;
				K6.Checked = false;
				K7.Checked = false;
				K8.Checked = false;
				K9.Checked = false;
				K0.Checked = false;
				KPlus.Checked = false;
				KMinus.Checked = false;
				KPound.Checked = false;
				KClear.Checked = false;
				KInsert.Checked = false;
				KCtrl.Checked = false;
				KQ.Checked = false;
				KW.Checked = false;
				KE.Checked = false;
				KR.Checked = false;
				KT.Checked = false;
				KY.Checked = false;
				KU.Checked = false;
				KI.Checked = false;
				KO.Checked = false;
				KP.Checked = false;
				KAt.Checked = false;
				KAsterisk.Checked = false;
				KUpArrow.Checked = false;
				KRST.Checked = false;
				KRun.Checked = false;
				KLck.Checked = false;
				KA.Checked = false;
				KS.Checked = false;
				KD.Checked = false;
				KF.Checked = false;
				KG.Checked = false;
				KH.Checked = false;
				KJ.Checked = false;
				KK.Checked = false;
				KL.Checked = false;
				KColon.Checked = false;
				KSemicolon.Checked = false;
				KEquals.Checked = false;
				KReturn.Checked = false;
				KCommodore.Checked = false;
				KLeftShift.Checked = false;
				KZ.Checked = false;
				KX.Checked = false;
				KC.Checked = false;
				KV.Checked = false;
				KB.Checked = false;
				KN.Checked = false;
				KM.Checked = false;
				KComma.Checked = false;
				KPeriod.Checked = false;
				KSlash.Checked = false;
				KRightShift.Checked = false;
				KCursorUp.Checked = false;
				KCursorLeft.Checked = false;
				KSpace.Checked = false;
			}
		}

		public string GetMnemonic()
		{
			StringBuilder input = new StringBuilder("");

			input.Append(KF1.Checked ? "1" : ".");
			input.Append(KF3.Checked ? "3" : ".");
			input.Append(KF5.Checked ? "5" : ".");
			input.Append(KF7.Checked ? "7" : ".");
			input.Append(KLeftArrow.Checked ? "l" : ".");
			input.Append(K1.Checked ? "1" : ".");
			input.Append(K2.Checked ? "2" : ".");
			input.Append(K3.Checked ? "3" : ".");
			input.Append(K4.Checked ? "4" : ".");
			input.Append(K5.Checked ? "5" : ".");
			input.Append(K6.Checked ? "6" : ".");
			input.Append(K7.Checked ? "7" : ".");
			input.Append(K8.Checked ? "8" : ".");
			input.Append(K9.Checked ? "9" : ".");
			input.Append(K0.Checked ? "0" : ".");
			input.Append(KPlus.Checked ? "+" : ".");
			input.Append(KMinus.Checked ? "-" : ".");
			input.Append(KPound.Checked ? "l" : ".");
			input.Append(KClear.Checked ? "c" : ".");
			input.Append(KInsert.Checked ? "i" : ".");
			input.Append(KCtrl.Checked ? "c" : ".");
			input.Append(KQ.Checked ? "Q" : ".");
			input.Append(KW.Checked ? "W" : ".");
			input.Append(KE.Checked ? "E" : ".");
			input.Append(KR.Checked ? "R" : ".");
			input.Append(KT.Checked ? "T" : ".");
			input.Append(KY.Checked ? "Y" : ".");
			input.Append(KU.Checked ? "U" : ".");
			input.Append(KI.Checked ? "I" : ".");
			input.Append(KO.Checked ? "O" : ".");
			input.Append(KP.Checked ? "P" : ".");
			input.Append(KAt.Checked ? "@" : ".");
			input.Append(KAsterisk.Checked ? "*" : ".");
			input.Append(KUpArrow.Checked ? "u" : ".");
			input.Append(KRST.Checked ? "r" : ".");
			input.Append(KRun.Checked ? "s" : ".");
			input.Append(KLck.Checked ? "k" : ".");
			input.Append(KA.Checked ? "A" : ".");
			input.Append(KS.Checked ? "S" : ".");
			input.Append(KD.Checked ? "D" : ".");
			input.Append(KF.Checked ? "F" : ".");
			input.Append(KG.Checked ? "G" : ".");
			input.Append(KH.Checked ? "H" : ".");
			input.Append(KJ.Checked ? "J" : ".");
			input.Append(KK.Checked ? "K" : ".");
			input.Append(KL.Checked ? "L" : ".");
			input.Append(KColon.Checked ? ":" : ".");
			input.Append(KSemicolon.Checked ? ";" : ".");
			input.Append(KEquals.Checked ? "=" : ".");
			input.Append(KReturn.Checked ? "e" : ".");
			input.Append(KCommodore.Checked ? "o" : ".");
			input.Append(KLeftShift.Checked ? "s" : ".");
			input.Append(KZ.Checked ? "Z" : ".");
			input.Append(KX.Checked ? "X" : ".");
			input.Append(KC.Checked ? "C" : ".");
			input.Append(KV.Checked ? "V" : ".");
			input.Append(KB.Checked ? "B" : ".");
			input.Append(KN.Checked ? "N" : ".");
			input.Append(KM.Checked ? "M" : ".");
			input.Append(KComma.Checked ? "," : ".");
			input.Append(KPeriod.Checked ? ">" : ".");
			input.Append(KSlash.Checked ? "/" : ".");
			input.Append(KRightShift.Checked ? "s" : ".");
			input.Append(KCursorUp.Checked ? "u" : ".");
			input.Append(KCursorLeft.Checked ? "l" : ".");
			input.Append(KSpace.Checked ? "_" : ".");

			input.Append("|");
			return input.ToString();
		}

		public void SetButtons(string buttons)
		{
			if (buttons.Length < 66)
			{
				return;
			}

			if (buttons[0] == '.') KF1.Checked = false; else KF1.Checked = true;
			if (buttons[1] == '.') KF3.Checked = false; else KF3.Checked = true;
			if (buttons[2] == '.') KF5.Checked = false; else KF5.Checked = true;
			if (buttons[3] == '.') KF7.Checked = false; else KF7.Checked = true;
			if (buttons[4] == '.') KLeftArrow.Checked = false; else KLeftArrow.Checked = true;
			if (buttons[5] == '.') K1.Checked = false; else K1.Checked = true;
			if (buttons[5] == '.') K2.Checked = false; else K2.Checked = true;
			if (buttons[6] == '.') K3.Checked = false; else K3.Checked = true;
			if (buttons[7] == '.') K4.Checked = false; else K4.Checked = true;
			if (buttons[8] == '.') K5.Checked = false; else K5.Checked = true;
			if (buttons[9] == '.') K6.Checked = false; else K6.Checked = true;
			if (buttons[10] == '.') K7.Checked = false; else K7.Checked = true;
			if (buttons[11] == '.') K8.Checked = false; else K8.Checked = true;
			if (buttons[12] == '.') K9.Checked = false; else K9.Checked = true;
			if (buttons[13] == '.') K0.Checked = false; else K0.Checked = true;
			if (buttons[14] == '.') KPlus.Checked = false; else KPlus.Checked = true;
			if (buttons[15] == '.') KMinus.Checked = false; else KMinus.Checked = true;
			if (buttons[16] == '.') KPound.Checked = false; else KPound.Checked = true;
			if (buttons[17] == '.') KClear.Checked = false; else KClear.Checked = true;
			if (buttons[18] == '.') KInsert.Checked = false; else KInsert.Checked = true;
			if (buttons[19] == '.') KCtrl.Checked = false; else KCtrl.Checked = true;
			if (buttons[20] == '.') KQ.Checked = false; else KQ.Checked = true;
			if (buttons[21] == '.') KW.Checked = false; else KW.Checked = true;
			if (buttons[22] == '.') KE.Checked = false; else KE.Checked = true;
			if (buttons[23] == '.') KR.Checked = false; else KR.Checked = true;
			if (buttons[24] == '.') KT.Checked = false; else KT.Checked = true;
			if (buttons[25] == '.') KY.Checked = false; else KY.Checked = true;
			if (buttons[26] == '.') KU.Checked = false; else KU.Checked = true;
			if (buttons[27] == '.') KI.Checked = false; else KI.Checked = true;
			if (buttons[28] == '.') KO.Checked = false; else KO.Checked = true;
			if (buttons[29] == '.') KP.Checked = false; else KP.Checked = true;
			if (buttons[30] == '.') KAt.Checked = false; else KAt.Checked = true;
			if (buttons[31] == '.') KAsterisk.Checked = false; else KAsterisk.Checked = true;
			if (buttons[32] == '.') KUpArrow.Checked = false; else KUpArrow.Checked = true;
			if (buttons[33] == '.') KRST.Checked = false; else KRST.Checked = true;
			if (buttons[34] == '.') KRun.Checked = false; else KRun.Checked = true;
			if (buttons[35] == '.') KLck.Checked = false; else KLck.Checked = true;
			if (buttons[36] == '.') KA.Checked = false; else KA.Checked = true;
			if (buttons[37] == '.') KS.Checked = false; else KS.Checked = true;
			if (buttons[38] == '.') KD.Checked = false; else KD.Checked = true;
			if (buttons[39] == '.') KF.Checked = false; else KF.Checked = true;
			if (buttons[40] == '.') KG.Checked = false; else KG.Checked = true;
			if (buttons[41] == '.') KH.Checked = false; else KH.Checked = true;
			if (buttons[42] == '.') KJ.Checked = false; else KJ.Checked = true;
			if (buttons[43] == '.') KK.Checked = false; else KK.Checked = true;
			if (buttons[44] == '.') KL.Checked = false; else KL.Checked = true;
			if (buttons[45] == '.') KColon.Checked = false; else KColon.Checked = true;
			if (buttons[46] == '.') KSemicolon.Checked = false; else KSemicolon.Checked = true;
			if (buttons[47] == '.') KEquals.Checked = false; else KEquals.Checked = true;
			if (buttons[48] == '.') KReturn.Checked = false; else KReturn.Checked = true;
			if (buttons[49] == '.') KCommodore.Checked = false; else KCommodore.Checked = true;
			if (buttons[50] == '.') KLeftShift.Checked = false; else KLeftShift.Checked = true;
			if (buttons[51] == '.') KZ.Checked = false; else KZ.Checked = true;
			if (buttons[52] == '.') KX.Checked = false; else KX.Checked = true;
			if (buttons[53] == '.') KC.Checked = false; else KC.Checked = true;
			if (buttons[54] == '.') KV.Checked = false; else KV.Checked = true;
			if (buttons[55] == '.') KB.Checked = false; else KB.Checked = true;
			if (buttons[56] == '.') KN.Checked = false; else KN.Checked = true;
			if (buttons[57] == '.') KM.Checked = false; else KM.Checked = true;
			if (buttons[58] == '.') KComma.Checked = false; else KComma.Checked = true;
			if (buttons[59] == '.') KPeriod.Checked = false; else KPeriod.Checked = true;
			if (buttons[60] == '.') KSlash.Checked = false; else KSlash.Checked = true;
			if (buttons[61] == '.') KRightShift.Checked = false; else KRightShift.Checked = true;
			if (buttons[62] == '.') KCursorUp.Checked = false; else KCursorUp.Checked = true;
			if (buttons[63] == '.') KCursorLeft.Checked = false; else KCursorLeft.Checked = true;
			if (buttons[64] == '.') KSpace.Checked = false; else KSpace.Checked = true;
		}
	}
}
