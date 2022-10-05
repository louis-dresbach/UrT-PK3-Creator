using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO.Packaging;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace UrT_pk3_creator
{
    public partial class Main : Form
    {
        List<Server> ServerList = new List<Server>();

        public Main()
        {
            InitializeComponent();

            /*ServerList.Add(new Server() { Name = "^1-V^7R^2z-^6BomB ^7ServeR", IP = "urt.vrzclan.com" });
            ServerList.Add(new Server() { Name = "^1-V^7R^2z-^6Zombie^7.", IP = "daray.dyndns.info:27961" });
            ServerList.Add(new Server() { Name = "^1-V^7R^2z-^6Snipe^7&^2BomB^7.", IP = "daray.dyndns.info" });*/
            load();
            foreach (Server s in ServerList)
            {
                listBox1.Items.Add(s.Name);
            }
            if (File.Exists(Properties.Settings.Default.CustomPK3Name+".pk3"))
            {
                DialogResult contin = MessageBox.Show("The program found a custom pk3 in this directory!\r\nDo you wish to overwrite it?", "Custom file found!", MessageBoxButtons.YesNo, MessageBoxIcon.Error);
                if (contin == DialogResult.Yes)
                {
                    using (FileStream fs = new FileStream(Properties.Settings.Default.CustomPK3Name + ".pk3", FileMode.OpenOrCreate, FileAccess.Write))
                    {
                        fs.Write(Properties.Resources.zpak000, 0, Properties.Resources.zpak000.Length);
                    }
                }
                else
                {
                    Application.Exit();
                }
            }
        }

        private void AddFileToZip(String FilePathInZip, String Content)
        {
            using (Package zip = System.IO.Packaging.Package.Open(Properties.Settings.Default.CustomPK3Name + ".pk3", FileMode.OpenOrCreate))
            {
                Uri uri = PackUriHelper.CreatePartUri(new Uri(".\\"+FilePathInZip, UriKind.Relative));
                if (zip.PartExists(uri))
                {
                    zip.DeletePart(uri);
                }
                PackagePart part = zip.CreatePart(uri, "", CompressionOption.Normal);
                using (Stream destination = part.GetStream())
                {
                    Byte[] Bytes = new System.Text.ASCIIEncoding().GetBytes(Content);
                    destination.Write(Bytes, 0, Bytes.Count());
                }
            }
        }
        private void AddFileToZip(String FilePath)
        {
            using (Package zipPackage = ZipPackage.Open(Properties.Settings.Default.CustomPK3Name + ".pk3", FileMode.OpenOrCreate))
            {
                string destFilename = ".\\" + Path.GetFileName(FilePath);

                Uri zipPartUri = PackUriHelper.CreatePartUri(new Uri(destFilename, UriKind.Relative));

                if (zipPackage.PartExists(zipPartUri))
                {
                    zipPackage.DeletePart(zipPartUri);
                }

                PackagePart zipPackagePart = zipPackage.CreatePart(zipPartUri, "", CompressionOption.Normal);

                using (FileStream fileStream = new FileStream(FilePath, FileMode.Open, FileAccess.Read))
                {
                    using (Stream dest = zipPackagePart.GetStream())
                    {
                        CopyStream(fileStream, dest);
                    }
                }
            }
        }

        private static void CopyStream(Stream source, Stream target)
        {
            const int bufSize = 0x1000;
            byte[] buf = new byte[bufSize];
            int bytesRead = 0;
            while ((bytesRead = source.Read(buf, 0, bufSize)) > 0)
                target.Write(buf, 0, bytesRead);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            AddFiles();
            MessageBox.Show("DONE.");
            save();
            Application.ExitThread();
        }

        private void AddFiles()
        {
            using (FileStream fs = new FileStream(Properties.Settings.Default.CustomPK3Name + ".pk3", FileMode.Create, FileAccess.Write))
            {
                fs.Write(Properties.Resources.zpak000, 0, Properties.Resources.zpak000.Length);
            }

            #region Some default files
            AddFileToZip("teaminfo.txt", Properties.Resources.teaminfo);
            AddFileToZip("gameinfo.txt", Properties.Resources.gameinfo);

            AddFileToZip("ui/betacredit.menu", Properties.Resources.betacredit);
            AddFileToZip("ui/betacredit_back.menu", Properties.Resources.betacredit_back);
            AddFileToZip("ui/connect.menu", Properties.Resources.connect);
            AddFileToZip("ui/controls.menu", Properties.Resources.controls);
            AddFileToZip("ui/createfavorite.menu", Properties.Resources.createfavorite);
            AddFileToZip("ui/createserver.menu", Properties.Resources.createserver);
            AddFileToZip("ui/default.menu", Properties.Resources._default);
            AddFileToZip("ui/demo.menu", Properties.Resources.demo);
            AddFileToZip("ui/error.menu", Properties.Resources.error);
            AddFileToZip("ui/findplayer.menu", Properties.Resources.findplayer);
            AddFileToZip("ui/hud.menu", Properties.Resources.hud_menu);
            AddFileToZip("ui/hud.txt", Properties.Resources.hud_txt);
            AddFileToZip("ui/ingame.menu", Properties.Resources.ingame_menu);
            AddFileToZip("ui/ingame.txt", Properties.Resources.ingame_txt);
            AddFileToZip("ui/ingame_about.menu", Properties.Resources.ingame_about);
            AddFileToZip("ui/ingame_addbot.menu", Properties.Resources.ingame_addbot);
            AddFileToZip("ui/ingame_callvote.menu", Properties.Resources.ingame_callvote);
            AddFileToZip("ui/ingame_controls.menu", Properties.Resources.ingame_controls);
            AddFileToZip("ui/ingame_leave.menu", Properties.Resources.ingame_leave);
            AddFileToZip("ui/ingame_options.menu", Properties.Resources.ingame_options);
            AddFileToZip("ui/ingame_player.menu", Properties.Resources.ingame_player);
            AddFileToZip("ui/ingame_rcon.menu", Properties.Resources.ingame_rcon);
            AddFileToZip("ui/ingame_select_gear.menu", Properties.Resources.ingame_select_gear);
            AddFileToZip("ui/ingame_select_gear_grenade.menu", Properties.Resources.ingame_select_gear_grenade);
            AddFileToZip("ui/ingame_select_gear_item.menu", Properties.Resources.ingame_select_gear_item);
            AddFileToZip("ui/ingame_select_gear_primary.menu", Properties.Resources.ingame_select_gear_primary);
            AddFileToZip("ui/ingame_select_gear_secondary.menu", Properties.Resources.ingame_select_gear_secondary);
            AddFileToZip("ui/ingame_select_gear_sidearm.menu", Properties.Resources.ingame_select_gear_sidearm);
            AddFileToZip("ui/ingame_select_player.menu", Properties.Resources.ingame_select_player);
            AddFileToZip("ui/ingame_select_team.menu", Properties.Resources.ingame_select_team);
            AddFileToZip("ui/ingame_stats.menu", Properties.Resources.ingame_stats);
            AddFileToZip("ui/ingame_system.menu", Properties.Resources.ingame_system);
            AddFileToZip("ui/ingame_ut_matchmode.menu", Properties.Resources.ingame_ut_matchmode);
            AddFileToZip("ui/ingame_vote.menu", Properties.Resources.ingame_vote);
            AddFileToZip("ui/joinserver.menu", Properties.Resources.joinserver);
            AddFileToZip("ui/menudef.h", Properties.Resources.menudef);
            AddFileToZip("ui/menus.txt", Properties.Resources.Menus);
            AddFileToZip("ui/options.menu", Properties.Resources.options);
            AddFileToZip("ui/password.menu", Properties.Resources.password);
            AddFileToZip("ui/psetup.menu", Properties.Resources.psetup);
            AddFileToZip("ui/quitcredit.menu", Properties.Resources.quitcredit);
            AddFileToZip("ui/quitcredit_back.menu", Properties.Resources.quitcredit_back);
            AddFileToZip("ui/score.menu", Properties.Resources.score);
            AddFileToZip("ui/serverinfo.menu", Properties.Resources.serverinfo);
            AddFileToZip("ui/setup.menu", Properties.Resources.setup);
            AddFileToZip("ui/specialcredit.menu", Properties.Resources.specialcredit);
            AddFileToZip("ui/system.menu", Properties.Resources.system);
            AddFileToZip("ui/teaminfo.menu", Properties.Resources.teaminfo);
            AddFileToZip("ui/teamscore.menu", Properties.Resources.teamscore);
            AddFileToZip("ui/vid_restart.menu", Properties.Resources.vid_restart);
            #endregion

            #region Informations text file for this applications use. (information.txt)
            String Servers = String.Empty;
            foreach (Server sv in ServerList)
            {
                Servers += sv.Name + "\r\n" + sv.IP + "\r\n";
            }
            AddFileToZip("information.txt", Servers.TrimEnd('\r', '\n'));
            #endregion

            #region Main menu (main.menu)
            String MenuItemsDef = "\t// This is for the extra Page //\r\n";
            String ExtraButtonVisible = (Properties.Settings.Default.ExtraPageEnabled) ? "1" : "0";
            MenuItemsDef += "\titemDef {\r\n\t\tname ExtraPageButton\r\n\t\tcvartest \"name\"\r\n\t\thideCvar { \"New_UrT_Player\", \"Unnamed Player\" }\r\n\t\ttype 1\r\n\t\tstyle 1\r\n\trect 501 31 120 40 \r\n\t\tborder 0\r\n\t\tbackcolor 0 0 0 0\r\n\t\tforecolor 1 1 1 1\r\n\t\tvisible " + ExtraButtonVisible + " \r\n\t\taction { play \"sound/misc/kcaction.wav\" ;\r\n\t\tplay \"sound/stomp.wav\" ;\r\n\t\tclose main ;\r\n\t\topen extra_menu ;}\r\n\t\tmouseEnter { setcolor backcolor 0 0 .55 1}\r\n\t\tmouseExit { setcolor backcolor 0 0 0 0 }\r\n\t}\r\n";
            MenuItemsDef += "\titemDef {\r\n\t\tname ExtraPageText\r\n\t\ttext \"" + Properties.Settings.Default.ExtraPageButton + "\"\r\n\t\ttextscale 0.3\r\n\t\ttextalign 0\r\n\t\ttextalignx 10\r\n\t\ttextaligny 21\r\n\t\tstyle 1\r\n\t\trect 501 31 120 40 \r\n\t\tborder 0\r\n\t\tbackcolor 0 0 0 0\r\n\t\tforecolor 1 1 1 1\r\n\t\tvisible " + ExtraButtonVisible + " \r\n\t\tdecoration\r\n\t}";
            MenuItemsDef += "\t// This is for the custom server buttons //\r\n";
            // Adds the Box
            int Height = 6 + (45 * ServerList.Count());
            MenuItemsDef += "\titemDef {\r\n\t\tname CustomServerBar\r\n\t\ttype 0\r\n\t\trect 17 156 256 " + Height + "\tstyle 1\r\n\t\tborder 1\r\n\t\tbordercolor .5 .5 .5 1\r\n\t\tbordersize 1\r\n\t\tbackcolor 0 0 0 .8\r\n\t\tvisible 1\r\n\t\tmouseEnter { show CustomAngle_on ; hide CustomAngle }\r\n\t\tmouseExit { show CustomAngle ; hide CustomAngle_on } \r\n\t\tdecoration\r\n\t}";
            // Adds the Custom Angle
            MenuItemsDef += "\r\n\titemDef {\r\n\t\tname CustomAngle\r\n\t\tcvartest \"name\"\r\n\t\thideCvar { \"New_UrT_Player\", \"Unnamed Player\" }\r\n\t\tstyle WINDOW_STYLE_SHADER\r\n\t\tbackground \"ui/assets/angle.tga\"\r\n\t\trect 257 157 16 16\r\n\t\tvisible 1\r\n\t\tdecoration \r\n\t}\r\n\titemDef {\r\n\t\tname CustomAngle_on\r\n\t\tcvartest \"name\"\r\n\t\thideCvar { \"New_UrT_Player\", \"Unnamed Player\" }\r\n\t\tstyle WINDOW_STYLE_SHADER\r\n\t\tbackground \"ui/assets/angle_on.tga\"\r\n\t\trect 257 157 16 16\r\n\t\tvisible 0\r\n\t\ttype 1\r\n\t\taction { play \"sound/misc/laugh.wav\" }\r\n\t\t//decoration \r\n\t}";
            int i = 0;
            foreach (Server s in ServerList)
            {
                int x = 20;
                int y = 180 + (34 * i);
                int width = 250;
                int height = 30;
                i++;
                MenuItemsDef += "\r\n\t// Custom Button " + i + ": " + s.Name + " //";
                MenuItemsDef += "\r\n\titemDef {\r\n\t\tname CustomServerButton" + i + "\r\n\t\ttype 1\r\n\t\tstyle 1\r\n\trect " + x + " " + y + " " + width + " " + height + "\r\n\t\tborder 0\r\n\t\tbackcolor 0 0 0 0\r\n\t\tforecolor 1 1 1 1\r\n\t\tvisible 1\r\n\t\taction { play \"sound/misc/kcaction.wav\" ; exec \"connect " + s.IP + "\" }\r\n\t\tmouseEnter { setcolor backcolor 0 0 .55 1 }\r\n\t\tmouseExit { setcolor backcolor 0 0 0 0 }\r\n\t}";
                MenuItemsDef += "\r\n\titemDef {\r\n\t\tname CustomServerText" + i + "\r\n\t\ttext \"" + s.Name + "\"\r\n\t\ttextscale 0.3\r\n\t\ttextalign 0\r\n\t\ttextalignx 10\r\n\t\ttextaligny 21\r\n\t\tstyle 1\r\n\trect " + x + " " + y + " " + width + " " + height + "\r\n\t\tborder 0\r\n\t\tbackcolor 0 0 0 0\r\n\t\tforecolor 1 1 1 1\r\n\t\tvisible 1 \r\n\t\tdecoration\r\n\t}";
            }
            MenuItemsDef += "\r\n\t// Until here //";
            AddFileToZip("ui/main.menu", Properties.Resources.Main_Menu.Replace("<CUSTOM DATA GOES HERE>", MenuItemsDef));
            #endregion

            #region Extra menu (extra.menu) 
            String extraMenu = Properties.Resources.Extra_Menu.Replace("<TITLE GOES HERE>", Properties.Settings.Default.ExtraPageTitle);
            extraMenu = extraMenu.Replace("<LEFT CONTENT GOES HERE>", "\"" + (Properties.Settings.Default.ExtraPageLeft + "\r\n").Replace("\r\n", "\\r\"\r\n\"").TrimEnd('"'));
            extraMenu = extraMenu.Replace("<TOP RIGHT CONTENT GOES HERE>", "\"" + (Properties.Settings.Default.ExtraPageTopRight + "\r\n").Replace("\r\n", "\\r\"\r\n\"").TrimEnd('"'));
            extraMenu = extraMenu.Replace("<BOTTOM RIGHT CONTENT GOES HERE>", "\"" + (Properties.Settings.Default.ExtraPageBottomRight + "\r\n").Replace("\r\n", "\\r\"\r\n\"").TrimEnd('"'));
            AddFileToZip("ui/extra.menu", extraMenu);
            #endregion

            #region Custom Background
            if (Properties.Settings.Default.CustomBackgroundEnabled)
            {
                if (!File.Exists(Properties.Settings.Default.CustomBackground))
                {
                    MessageBox.Show("Couln't find the custom background you set. Using default one instead.");

                }
                else
                {
                    AddFileToZip("ui/main.menu", File.ReadAllLines(Properties.Settings.Default.CustomBackground).ToString());
                }
            }
            else
            {

            }
            #endregion
        }

        private void save()
        {
            using (MemoryStream ms = new MemoryStream())
            {
                using (StreamReader sr = new StreamReader(ms))
                {
                    BinaryFormatter bf = new BinaryFormatter();
                    bf.Serialize(ms, ServerList);
                    ms.Position = 0;
                    byte[] buffer = new byte[(int)ms.Length];
                    ms.Read(buffer, 0, buffer.Length);
                    Properties.Settings.Default.ServerList = Convert.ToBase64String(buffer);
                    Properties.Settings.Default.Save();
                }
            }
        }

        private void load()
        {
            if (Properties.Settings.Default.ServerList == String.Empty) { ServerList = new List<Server>(); return; }
            using (MemoryStream ms = new MemoryStream(Convert.FromBase64String(Properties.Settings.Default.ServerList)))
            {
                BinaryFormatter bf = new BinaryFormatter();
                ServerList = (List<Server>)bf.Deserialize(ms);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            AddServer af = new AddServer();
            if (af.ShowDialog() == DialogResult.OK)
            {
                String Name = af.GetName().Trim();
                String IP = af.GetIp().Trim();
                if (Name == String.Empty) return;

                listBox1.Items.Add(Name);
                ServerList.Add(new Server() { Name = Name, IP = IP });
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            ServerList.RemoveAt(listBox1.SelectedIndex);
            listBox1.Items.RemoveAt(listBox1.SelectedIndex);
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            new AdvancedSettingsForm().ShowDialog();
        }
    }

    [Serializable()]
    public class Server
    {
        public String Name { get; set; }
        public String IP { get; set; }
    }
}
