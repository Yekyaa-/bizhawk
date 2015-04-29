﻿using System;
using System.Collections.Generic;
using System.Text;
using Eto;
using Eto.Forms;
using Eto.Drawing;
using BizHawk.Emulation.Common;
using BizHawk.Common;
using BizHawk.Client.Common;
using System.Linq;
using System.Collections;

namespace EtoHawk.Config
{
    public partial class ControllerConfig : Dialog
    {
        private const int MAXPLAYERS = 8;
        private static readonly Dictionary<string, Bitmap> ControllerImages = new Dictionary<string, Bitmap>();
        private readonly ControllerDefinition _theDefinition;

        static ControllerConfig()
        {
            ControllerImages.Add("NES Controller", MakeTempImage("NES Controller"));
            ControllerImages.Add("SNES Controller", MakeTempImage("SNES_Controller"));
            ControllerImages.Add("Nintento 64 Controller", MakeTempImage("N64"));
            ControllerImages.Add("Gameboy Controller", MakeTempImage("GBController"));
            ControllerImages.Add("GBA Controller", MakeTempImage("GBA_Controller"));
            ControllerImages.Add("Dual Gameboy Controller", MakeTempImage("GBController"));

            ControllerImages.Add("SMS Controller", MakeTempImage("SMSController"));
            ControllerImages.Add("Genesis 3-Button Controller", MakeTempImage("GENController"));
            ControllerImages.Add("GPGX Genesis Controller", MakeTempImage("GENController"));
            ControllerImages.Add("Saturn Controller", MakeTempImage("SaturnController"));

            ControllerImages.Add("Intellivision Controller", MakeTempImage("IntVController"));
            ControllerImages.Add("ColecoVision Basic Controller", MakeTempImage("colecovisioncontroller"));
            ControllerImages.Add("Atari 2600 Basic Controller", MakeTempImage("atari_controller"));
            ControllerImages.Add("Atari 7800 ProLine Joystick Controller", MakeTempImage("A78Joystick"));

            ControllerImages.Add("PC Engine Controller", MakeTempImage("PCEngineController"));
            ControllerImages.Add("Commodore 64 Controller", MakeTempImage("C64Joystick"));
            ControllerImages.Add("TI83 Controller", MakeTempImage("TI83_Controller"));

            ControllerImages.Add("WonderSwan Controller", MakeTempImage("WonderSwanColor"));
            ControllerImages.Add("Lynx Controller", MakeTempImage("Lynx"));
            ControllerImages.Add("PSX Gamepad Controller", MakeTempImage("PSX_Original_Controller"));
            ControllerImages.Add("PSX DualShock Controller", MakeTempImage("psx_dualshock"));

            /*ControllerImages.Add("NES Controller", Properties.Resources.NES_Controller);
            ControllerImages.Add("SNES Controller", Properties.Resources.SNES_Controller);
            ControllerImages.Add("Nintento 64 Controller", Properties.Resources.N64);
            ControllerImages.Add("Gameboy Controller", Properties.Resources.GBController);
            ControllerImages.Add("GBA Controller", Properties.Resources.GBA_Controller);
            ControllerImages.Add("Dual Gameboy Controller", Properties.Resources.GBController);

            ControllerImages.Add("SMS Controller", Properties.Resources.SMSController);
            ControllerImages.Add("Genesis 3-Button Controller", Properties.Resources.GENController);
            ControllerImages.Add("GPGX Genesis Controller", Properties.Resources.GENController);
            ControllerImages.Add("Saturn Controller", Properties.Resources.SaturnController);

            ControllerImages.Add("Intellivision Controller", Properties.Resources.IntVController);
            ControllerImages.Add("ColecoVision Basic Controller", Properties.Resources.colecovisioncontroller);
            ControllerImages.Add("Atari 2600 Basic Controller", Properties.Resources.atari_controller);
            ControllerImages.Add("Atari 7800 ProLine Joystick Controller", Properties.Resources.A78Joystick);

            ControllerImages.Add("PC Engine Controller", Properties.Resources.PCEngineController);
            ControllerImages.Add("Commodore 64 Controller", Properties.Resources.C64Joystick);
            ControllerImages.Add("TI83 Controller", Properties.Resources.TI83_Controller);

            ControllerImages.Add("WonderSwan Controller", Properties.Resources.WonderSwanColor);
            ControllerImages.Add("Lynx Controller", Properties.Resources.Lynx);
            ControllerImages.Add("PSX Gamepad Controller", Properties.Resources.PSX_Original_Controller);
            ControllerImages.Add("PSX DualShock Controller", Properties.Resources.psx_dualshock);*/
        }

        private static Bitmap MakeTempImage(string name)
        {
            //This is really awful and I will replace it with the actual images as soon as possible
            Bitmap moo = new Bitmap(new Size(150, 150), PixelFormat.Format32bppRgba);
            Graphics g = new Graphics(moo);
            g.DrawText(new Font(SystemFont.Label, 12), Eto.Drawing.Colors.Black, 0, 0, name + " goes here");
            return moo;
        }

        /*protected override void OnActivated(EventArgs e)
        {
            base.OnActivated(e);
            Input.Instance.ControlInputFocus(this, Input.InputFocus.Mouse, true);
        }

        protected override void OnDeactivate(EventArgs e)
        {
            base.OnDeactivate(e);
            Input.Instance.ControlInputFocus(this, Input.InputFocus.Mouse, false);
        }*/

        private ControllerConfig()
        {
            InitializeComponent();
            /*Closing += (o, e) =>
            {
                buttonOK.Focus(); // A very dirty hack to avoid https://code.google.com/p/bizhawk/issues/detail?id=161
            };*/
        }

        private delegate Panel PanelCreator<T>(Dictionary<string, T> settings, List<string> buttons, Size size);

        private Panel CreateNormalPanel(Dictionary<string, string> settings, List<string> buttons, Size size)
        {
            var cp = new ControllerConfigPanel { /*Dock = DockStyle.Fill, AutoScroll = true*/ };
            //cp.Tooltip = toolTip1;
            cp.LoadSettings(settings, checkBoxAutoTab.Checked==true, buttons, size.Width, size.Height);
            return cp;
        }

        /*private static Control CreateAnalogPanel(Dictionary<string, Config.AnalogBind> settings, List<string> buttons, Size size)
        {
            return new AnalogBindPanel(settings, buttons) { Dock = DockStyle.Fill, AutoScroll = true };
        }*/

        private static void LoadToPanel<T>(Panel dest, string controllerName, IList<string> controllerButtons, IDictionary<string, Dictionary<string, T>> settingsblock, T defaultvalue, PanelCreator<T> createpanel)
        {
            Dictionary<string, T> settings;
            if (!settingsblock.TryGetValue(controllerName, out settings))
            {
                settings = new Dictionary<string, T>();
                settingsblock[controllerName] = settings;
            }

            // check to make sure that the settings object has all of the appropriate boolbuttons
            foreach (var button in controllerButtons)
            {
                if (!settings.Keys.Contains(button))
                {
                    settings[button] = defaultvalue;
                }
            }

            if (controllerButtons.Count == 0)
            {
                return;
            }

            // split the list of all settings into buckets by player number
            var buckets = new List<string>[MAXPLAYERS + 1];
            for (var i = 0; i < buckets.Length; i++)
            {
                buckets[i] = new List<string>();
            }

            // by iterating through only the controller's active buttons, we're silently
            // discarding anything that's not on the controller right now.  due to the way
            // saving works, those entries will still be preserved in the config file, tho
            foreach (var button in controllerButtons)
            {
                int i;
                for (i = 1; i <= MAXPLAYERS; i++)
                {
                    if (button.StartsWith("P" + i))
                    {
                        break;
                    }
                }

                if (i > MAXPLAYERS) // couldn't find
                {
                    i = 0;
                }

                buckets[i].Add(button);
            }

            if (buckets[0].Count == controllerButtons.Count)
            {
                // everything went into bucket 0, so make no tabs at all
                dest.Content = createpanel(settings, controllerButtons.ToList(), dest.Size);
            }
            else
            {
                // create multiple player tabs
                var tt = new TabControl { /*Dock = DockStyle.Fill*/ };
                int pageidx = 0;
                for (int i = 1; i <= MAXPLAYERS; i++)
                {
                    if (buckets[i].Count > 0)
                    {
                        string tabname = Global.Emulator.SystemId == "WSWAN" ? i == 1 ? "Normal" : "Rotated" : "Player " + i; // hack
                        TabPage pg = new TabPage(createpanel(settings, buckets[i], tt.Size));
                        pg.Text = tabname;
                        tt.Pages.Add(pg);
                        pageidx++;
                    }
                }

                if (buckets[0].Count > 0)
                {
                    string tabname = Global.Emulator.SystemId == "C64" ? "Keyboard" : "Console"; // hack
                    TabPage pg = new TabPage(createpanel(settings, buckets[0], tt.Size));
                    pg.Text = tabname;
                    tt.Pages.Add(pg);
                }
                dest.Content = tt;
            }
        }

        public ControllerConfig(ControllerDefinition def) : this()
        {
            _theDefinition = def;
            SuspendLayout();
            LoadPanels(Global.Config);

            checkBoxUDLR.Checked = Global.Config.AllowUD_LR;
            checkBoxAutoTab.Checked = Global.Config.InputConfigAutoTab;

            SetControllerPicture(def.Name);

            var analog = tabControl1.TabPages[0];

            ResumeLayout();
        }

        private void LoadPanels(
            IDictionary<string, Dictionary<string, string>> normal,
            IDictionary<string, Dictionary<string, string>> autofire,
            IDictionary<string, Dictionary<string, BizHawk.Client.Common.Config.AnalogBind>> analog)
        {
            LoadToPanel(NormalControlsTab, _theDefinition.Name, _theDefinition.BoolButtons, normal, string.Empty, CreateNormalPanel);
            LoadToPanel(AutofireControlsTab, _theDefinition.Name, _theDefinition.BoolButtons, autofire, string.Empty, CreateNormalPanel);
            /*LoadToPanel(AnalogControlsTab, _theDefinition.Name, _theDefinition.FloatControls, analog, new BizHawk.Client.Common.Config.AnalogBind(string.Empty, 1.0f, 0.1f), CreateAnalogPanel);

            if (AnalogControlsTab.Controls.Count == 0)
            {
                tabControl1.TabPages.Remove(AnalogControlsTab);
            }*/
        }

        private void LoadPanels(ControlDefaults cd)
        {
            LoadPanels(cd.AllTrollers, cd.AllTrollersAutoFire, cd.AllTrollersAnalog);
        }

        private void LoadPanels(BizHawk.Client.Common.Config c)
        {
            LoadPanels(c.AllTrollers, c.AllTrollersAutoFire, c.AllTrollersAnalog);
        }

        private void SetControllerPicture(string controlName)
        {
            Bitmap bmp;
            if (!ControllerImages.TryGetValue(controlName, out bmp))
            {
                bmp = ControllerConfig.MakeTempImage("Question Mark"); //Properties.Resources.Help;
            }

            pictureBox1.Image = bmp;
            pictureBox1.Size = bmp.Size;
            //tableLayoutPanel1.ColumnStyles[1].Width = bmp.Width;

            // Uberhack
            /*if (controlName == "Commodore 64 Controller")
            {
                var pictureBox2 = new PictureBox
                {
                    Image = Properties.Resources.C64Keyboard,
                    Size = Properties.Resources.C64Keyboard.Size
                };
                tableLayoutPanel1.ColumnStyles[1].Width = Properties.Resources.C64Keyboard.Width;
                pictureBox1.Height /= 2;
                pictureBox1.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
                pictureBox1.Dock = DockStyle.Top;
                pictureBox2.Location = new Point(pictureBox1.Location.X, pictureBox1.Location.Y + pictureBox1.Size.Height + 10);
                tableLayoutPanel1.Controls.Add(pictureBox2, 1, 0);

                pictureBox2.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Bottom;
            }*/
        }

        // lazy methods, but they're not called often and actually
        // tracking all of the ControllerConfigPanels wouldn't be simpler
        private static void SetAutoTab(Control c, bool value)
        {
            if (c is ControllerConfigPanel)
            {
                (c as ControllerConfigPanel).SetAutoTab(value);
            }
            /*else if (c is AnalogBindPanel)
            {
                // TODO
            }*/
            else if (c is Panel && ((Panel)c).Controls.Count() > 0)
            {
                foreach (Control cc in ((Panel)c).Controls)
                {
                    SetAutoTab(cc, value);
                }
            }
        }

        private void Save()
        {
            ActOnControlCollection<ControllerConfigPanel>(NormalControlsTab, c => c.Save(Global.Config.AllTrollers[_theDefinition.Name]));
            ActOnControlCollection<ControllerConfigPanel>(AutofireControlsTab, c => c.Save(Global.Config.AllTrollersAutoFire[_theDefinition.Name]));
            //ActOnControlCollection<AnalogBindPanel>(AnalogControlsTab, c => c.Save(Global.Config.AllTrollersAnalog[_theDefinition.Name]));
        }

        private void SaveToDefaults(ControlDefaults cd)
        {
            ActOnControlCollection<ControllerConfigPanel>(NormalControlsTab, c => c.Save(cd.AllTrollers[_theDefinition.Name]));
            ActOnControlCollection<ControllerConfigPanel>(AutofireControlsTab, c => c.Save(cd.AllTrollersAutoFire[_theDefinition.Name]));
            //ActOnControlCollection<AnalogBindPanel>(AnalogControlsTab, c => c.Save(cd.AllTrollersAnalog[_theDefinition.Name]));
        }

        private static void ActOnControlCollection<T>(Control c, Action<T> proc)
            where T : Control
        {
            if (c is T)
            {
                proc(c as T);
            }
            else if (c is Panel && ((Panel)c).Controls.Count() > 0)
            {
                foreach (Control cc in ((Panel)c).Controls)
                {
                    ActOnControlCollection(cc, proc);
                }
            }
        }

        private void CheckBoxAutoTab_CheckedChanged(object sender, EventArgs e)
        {
            SetAutoTab(this, checkBoxAutoTab.Checked==true);
        }

        private void ButtonOk_Click(object sender, EventArgs e)
        {
            Global.Config.AllowUD_LR = checkBoxUDLR.Checked==true;
            Global.Config.InputConfigAutoTab = checkBoxAutoTab.Checked==true;

            Save();

            //GlobalWin.OSD.AddMessage("Controller settings saved");
            //DialogResult = DialogResult.OK;
            Close();
        }

        private void ButtonCancel_Click(object sender, EventArgs e)
        {
            //GlobalWin.OSD.AddMessage("Controller config aborted");
            Close();
        }

        private void NewControllerConfig_Load(object sender, EventArgs e)
        {
            Title = _theDefinition.Name + " Configuration";
        }

        private static TabControl GetTabControl(IEnumerable controls)
        {
            if (controls != null)
            {
                return controls
                    .OfType<TabControl>()
                    .Select(c => c)
                    .FirstOrDefault();
            }

            return null;
        }

        private void ButtonLoadDefaults_Click(object sender, EventArgs e)
        {
            tabControl1.SuspendLayout();

            TabPage wasTabbedMain = tabControl1.SelectedPage;
            var tb1 = GetTabControl(NormalControlsTab.Controls);
            var tb2 = GetTabControl(AutofireControlsTab.Controls);
            var tb3 = GetTabControl(AnalogControlsTab.Controls);
            int? wasTabbedPage1 = null;
            int? wasTabbedPage2 = null;
            int? wasTabbedPage3 = null;

            if (tb1 != null && tb1.SelectedPage != null) { wasTabbedPage1 = tb1.SelectedIndex; }
            if (tb2 != null && tb2.SelectedPage != null) { wasTabbedPage2 = tb2.SelectedIndex; }
            if (tb3 != null && tb3.SelectedPage != null) { wasTabbedPage3 = tb3.SelectedIndex; }

            NormalControlsTab.Content = null; //NormalControlsTab.Controls.Clear();
            AutofireControlsTab.Content = null; //AutofireControlsTab.Controls.Clear();
            AnalogControlsTab.Content = null; //AnalogControlsTab.Controls.Clear();

            // load panels directly from the default config.
            // this means that the changes are NOT committed.  so "Cancel" works right and you
            // still have to hit OK at the end.
            var cd = ConfigService.Load<ControlDefaults>(BizHawk.Client.Common.Config.ControlDefaultPath);
            LoadPanels(cd);

            tabControl1.SelectedPage = wasTabbedMain;

            if (wasTabbedPage1.HasValue)
            {
                var newTb1 = GetTabControl(NormalControlsTab.Controls);
                if (newTb1 != null)
                {
                    newTb1.SelectedIndex = wasTabbedPage1.Value;
                }
            }

            if (wasTabbedPage2.HasValue)
            {
                var newTb2 = GetTabControl(AutofireControlsTab.Controls);
                if (newTb2 != null)
                {
                    newTb2.SelectedIndex = wasTabbedPage2.Value;
                }
            }

            if (wasTabbedPage3.HasValue)
            {
                var newTb3 = GetTabControl(AnalogControlsTab.Controls);
                if (newTb3 != null)
                {
                    newTb3.SelectedIndex = wasTabbedPage3.Value;
                }
            }

            tabControl1.ResumeLayout();
        }

        private void ButtonSaveDefaults_Click(object sender, EventArgs e)
        {
            // this doesn't work anymore, as it stomps out any defaults for buttons that aren't currently active on the console
            // there are various ways to fix it, each with its own semantic problems
            var result = MessageBox.Show(this, "OK to overwrite defaults for current control scheme?", "Save Defaults", MessageBoxButtons.YesNo);
            if (result == DialogResult.Yes)
            {
                var cd = ConfigService.Load<ControlDefaults>(BizHawk.Client.Common.Config.ControlDefaultPath);
                cd.AllTrollers[_theDefinition.Name] = new Dictionary<string, string>();
                cd.AllTrollersAutoFire[_theDefinition.Name] = new Dictionary<string, string>();
                cd.AllTrollersAnalog[_theDefinition.Name] = new Dictionary<string, BizHawk.Client.Common.Config.AnalogBind>();

                SaveToDefaults(cd);

                ConfigService.Save(BizHawk.Client.Common.Config.ControlDefaultPath, cd);
            }
        }

        private void ClearWidgetAndChildren(Control c)
        {
            if (c is InputCompositeWidget)
            {
                (c as InputCompositeWidget).Clear();
            }

            if (c is InputWidget)
            {
                (c as InputWidget).ClearAll();
            }

            /*if (c is AnalogBindControl)
            {
                (c as AnalogBindControl).Unbind_Click(null, null);
            }*/

            if (c is Panel && ((Panel)c).Controls.Any())
            {
                foreach (Control child in ((Panel)c).Controls)
                {
                    ClearWidgetAndChildren(child);
                }
            }
        }

        private void ClearBtn_Click(object sender, EventArgs e)
        {
            foreach (var c in this.Controls)
            {
                ClearWidgetAndChildren(c);
            }
        }
    }
}
