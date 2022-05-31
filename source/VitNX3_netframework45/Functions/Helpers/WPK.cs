﻿using Microsoft.Win32;

using System;
using System.Collections;
using System.Runtime.InteropServices;
using System.Text;

using VitNX3.Functions.Win32;

namespace VitNX3.Functions.Helpers.WPK
{
    public class GWK
    {
        public enum DigitalProductIdVersion
        {
            UpToWindows7,
            Windows8AndUp
        }

        public static string GetWindowsProductKeyFromDigitalProductId(byte[] digitalProductId,
       DigitalProductIdVersion digitalProductIdVersion)
        {
            var productKey = digitalProductIdVersion == DigitalProductIdVersion.Windows8AndUp ?
                DecodeProductKeyWin8AndUp(digitalProductId) :
                DecodeProductKey(digitalProductId);
            return productKey;
        }

        public static bool CheckMSDM(out byte[] buffer)
        {
            var firmwareTableProviderSignature = Enums.FIRMWARE_TABLE_TYPE.Acpi;
            uint bufferSize = Import.EnumSystemFirmwareTables(firmwareTableProviderSignature,
                IntPtr.Zero, 0);
            IntPtr pFirmwareTableBuffer = Marshal.AllocHGlobal((int)bufferSize);
            buffer = new byte[bufferSize];
            Import.EnumSystemFirmwareTables(firmwareTableProviderSignature,
                pFirmwareTableBuffer,
                bufferSize);
            Marshal.Copy(pFirmwareTableBuffer,
                buffer, 0,
                buffer.Length);
            Marshal.FreeHGlobal(pFirmwareTableBuffer);
            if (Encoding.ASCII.GetString(buffer).Contains("MSDM"))
            {
                uint firmwareTableID = 0x4d44534d;
                bufferSize = Import.GetSystemFirmwareTable(firmwareTableProviderSignature,
                    firmwareTableID,
                    IntPtr.Zero, 0);
                buffer = new byte[bufferSize];
                pFirmwareTableBuffer = Marshal.AllocHGlobal((int)bufferSize);
                Import.GetSystemFirmwareTable(firmwareTableProviderSignature,
                    firmwareTableID,
                    pFirmwareTableBuffer,
                    bufferSize);
                Marshal.Copy(pFirmwareTableBuffer,
                    buffer, 0,
                    buffer.Length);
                Marshal.FreeHGlobal(pFirmwareTableBuffer); return true;
            }
            return false;
        }

        public static string GetWindowsProductKeyFromRegistry()
        {
            var localKey = RegistryKey.OpenBaseKey(RegistryHive.LocalMachine,
                Environment.Is64BitOperatingSystem ? RegistryView.Registry64 : RegistryView.Registry32);
            var registryKeyValue = localKey.OpenSubKey(@"SOFTWARE\Microsoft\Windows NT\CurrentVersion")?.GetValue("DigitalProductId");
            if (registryKeyValue == null)
                return "Failed to get DigitalProductId from registry";
            var digitalProductId = (byte[])registryKeyValue;
            localKey.Close();
            var isWin8OrUp = Environment.OSVersion.Version.Major == 6 &&
                Environment.OSVersion.Version.Minor >= 2 ||
                Environment.OSVersion.Version.Major > 6;
            return GetWindowsProductKeyFromDigitalProductId(digitalProductId, isWin8OrUp ? DigitalProductIdVersion.Windows8AndUp : DigitalProductIdVersion.UpToWindows7);
        }

        static string DecodeProductKey(byte[] digitalProductId)
        {
            const int keyStartIndex = 52;
            const int keyEndIndex = keyStartIndex + 15;
            var digits = new[] { 'B', 'C', 'D', 'F', 'G', 'H', 'J', 'K', 'M', 'P', 'Q', 'R', 'T', 'V', 'W', 'X', 'Y', '2', '3', '4', '6', '7', '8', '9', };
            const int decodeLength = 29;
            const int decodeStringLength = 15;
            var decodedChars = new char[decodeLength];
            var hexPid = new ArrayList();
            for (var i = keyStartIndex; i <= keyEndIndex; i++)
                hexPid.Add(digitalProductId[i]);
            for (var i = decodeLength - 1; i >= 0; i--)
            {
                if ((i + 1) % 6 == 0)
                    decodedChars[i] = '-';
                else
                {
                    var digitMapIndex = 0;
                    for (var j = decodeStringLength - 1; j >= 0; j--)
                    {
                        var byteValue = (digitMapIndex << 8) | (byte)hexPid[j];
                        hexPid[j] = (byte)(byteValue / 24);
                        digitMapIndex = byteValue % 24;
                        decodedChars[i] = digits[digitMapIndex];
                    }
                }
            }
            return new string(decodedChars);
        }

        public static string DecodeProductKeyWin8AndUp(byte[] digitalProductId)
        {
            var key = string.Empty;
            const int keyOffset = 52;
            var isWin8 = (byte)((digitalProductId[66] / 6) & 1);
            digitalProductId[66] = (byte)((digitalProductId[66] & 0xf7) | (isWin8 & 2) * 4);
            const string digits = "BCDFGHJKMPQRTVWXY2346789";
            var last = 0; for (var i = 24; i >= 0; i--)
            {
                var current = 0;
                for (var j = 14; j >= 0; j--)
                {
                    current = current * 256;
                    current = digitalProductId[j + keyOffset] + current; digitalProductId[j + keyOffset] = (byte)(current / 24); current = current % 24; last = current;
                }
                key = digits[current] + key;
            }
            var keypart1 = key.Substring(1, last);
            var keypart2 = key.Substring(last + 1, key.Length - (last + 1));
            key = keypart1 + "N" + keypart2;
            for (var i = 5; i < key.Length; i += 6)
                key = key.Insert(i, "-");
            return key;
        }
    }
}