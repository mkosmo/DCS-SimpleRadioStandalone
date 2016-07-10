﻿using System;
using Microsoft.Win32;
using static Ciribob.DCS.SimpleRadio.Standalone.Client.InputDevice;

namespace Ciribob.DCS.SimpleRadio.Standalone.Client
{
    public class InputConfiguration
    {
        public static readonly string RegPath = "HKEY_CURRENT_USER\\SOFTWARE\\DCS-SimpleRadioStandalone";

        public InputDevice[] InputDevices = new InputDevice[4];


        public InputConfiguration()
        {
            //load from registry
            //    PTTCommon = ReadInputRegistry("common");

            InputDevices[0] = ReadInputRegistry(InputBinding.Ptt);
            InputDevices[1] = ReadInputRegistry(InputBinding.Switch1);
            InputDevices[2] = ReadInputRegistry(InputBinding.Switch2);
            InputDevices[3] = ReadInputRegistry(InputBinding.Switch3);
        }

        public InputDevice ReadInputRegistry(InputBinding bind)
        {
            var device = new InputDevice();
            try
            {
                var key = bind.ToString();


                var deviceName = (string) Registry.GetValue(RegPath,
                    key + "_name",
                    "");

                var button = (int) Registry.GetValue(RegPath,
                    key + "_button",
                    "");

                var guid = (string) Registry.GetValue(RegPath,
                    key + "_guid",
                    "");


                device.DeviceName = deviceName;
                device.Button = button;
                device.InstanceGuid = Guid.Parse(guid);
                device.InputBind = bind;

                return device;
            }
            catch (Exception ex)
            {
            }


            return null;
        }

        public void WriteInputRegistry(InputBinding bind, InputDevice device)
        {
            try
            {
                var key = bind.ToString();
                Registry.SetValue(RegPath,
                    key + "_name",
                    device.DeviceName.Replace("\0", ""));

                Registry.SetValue(RegPath,
                    key + "_button",
                    device.Button);

                Registry.SetValue(RegPath,
                    key + "_guid",
                    device.InstanceGuid.ToString());
            }
            catch (Exception ex)
            {
            }
        }

        public void ClearInputRegistry(InputBinding bind)
        {
            try
            {
                var key = bind.ToString();
                Registry.SetValue(RegPath,
                    key + "_name",
                    "");

                Registry.SetValue(RegPath,
                    key + "_button",
                    "");

                Registry.SetValue(RegPath,
                    key + "_guid",
                    "");
            }
            catch (Exception ex)
            {
            }
        }
    }
}