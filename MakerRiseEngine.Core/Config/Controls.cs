﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace Maker.RiseEngine.Core.Config
{
    public static class Controls
    {
        

        public static Keys MoveUp = Keys.Z;
        public static Keys MoveDown = Keys.S;
        public static Keys MoveLeft = Keys.Q;
        public static Keys MoveRight = Keys.D;

        public static Keys MoveRun = Keys.LeftShift;

        public static Keys Interact = Keys.E;
        public static Keys Attack = Keys.A;

        public static Keys ShowInventory = Keys.I;
        public static Keys ShowMenu = Keys.Escape;
        public static Keys ShowChat = Keys.T;

        public static Storage.DataSheet DS = new Storage.DataSheet("Data\\Engine\\Config\\Input.rise");
        public static void Load() {
            Core.Debug.DebugLogs.WriteInLogs("[Config.Controls] Load config...", Core.Debug.LogType.Info);
            DS.Load();

            MoveUp = (Keys)int.Parse(DS.GetData("MoveUP", ((int)Keys.Z).ToString()));
            MoveDown = (Keys)int.Parse(DS.GetData("MoveDown", ((int)Keys.S).ToString()));
            MoveLeft = (Keys)int.Parse(DS.GetData("MoveLeft", ((int)Keys.Q).ToString()));
            MoveRight = (Keys)int.Parse(DS.GetData("MoveRight", ((int)Keys.D).ToString()));

            MoveRun = (Keys)int.Parse(DS.GetData("MoveRun", ((int)Keys.LeftShift).ToString()));

            Interact = (Keys)int.Parse(DS.GetData("Interact", ((int)Keys.E).ToString()));
            Attack = (Keys)int.Parse(DS.GetData("Attack", ((int)Keys.A).ToString()));

            ShowInventory = (Keys)int.Parse(DS.GetData("ShowInventory", ((int)Keys.E).ToString()));
            ShowMenu = (Keys)int.Parse(DS.GetData("ShowMenu", ((int)Keys.Escape).ToString()));
            ShowChat = (Keys)int.Parse(DS.GetData("ShowChat", ((int)Keys.T).ToString()));

            DS.Save();
        }

    }
}
