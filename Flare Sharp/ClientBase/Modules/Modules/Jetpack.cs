﻿using Flare_Sharp.ClientBase.Categories;
using Flare_Sharp.ClientBase.Keybinds;
using Flare_Sharp.Memory;
using Flare_Sharp.Memory.CraftSDK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Flare_Sharp.ClientBase.Modules.Modules
{
    public class Jetpack : Module
    {
        public List<float> directionalVector(float yaw, float pitch)
        {
            List<float> calculations = new List<float>();
            calculations.Add((float)Math.Cos(yaw) * (float)Math.Cos(pitch));
            calculations.Add((float)Math.Sin(pitch));
            calculations.Add((float)Math.Sin(yaw) * (float)Math.Cos(pitch));
            return calculations;
        }
        public Jetpack() : base("Jetpack", CategoryHandler.registry.categories[1], 'F', false)
        {
            KeybindHandler.clientKeyUpEvent += UpKeyHeld;
        }
        public void UpKeyHeld(object sender, clientKeyEvent e)
        {
            if (e.key == keybind)
            {
                enabled = false;
            }
        }
        public override void onTick()
        {
            base.onTick();
            List<float> directionalVec = directionalVector((SDK.instance.player.yaw + 89f) * (float)Math.PI / 178, SDK.instance.player.pitch * (float)Math.PI / 178);
            SDK.instance.player.velX = 1.2F * directionalVec[0];
            SDK.instance.player.velY = 1.2F * -directionalVec[1];
            SDK.instance.player.velZ = 1.2F * directionalVec[2];
        }
    }
}