﻿using System;
using System.Collections.Generic;
using System.Text;

namespace GenericSettingsFile
{
    public static class SettingHelpers
    {
        public static string GetSingleStringValue(SettingsCollection coll, string key)
        {
            var setting = coll.GetValue(key);
            if (setting.ParameterCount > 0)
            {
                throw new Exception(string.Format("Setting '{0}' has multiple parameters", key));
            }
            if (!setting.HasSingleValue)
            {
                return null;
            }
            return setting.SingleValue;
        }
        public static int GetSingleIntValue(SettingsCollection coll, string key)
        {
            string text = GetSingleStringValue(coll, key);
            int value;
            if (!int.TryParse(text, out value))
            {
                throw new Exception(string.Format("Setting '{0}' failed to parse as int: '{1}'", key, value));
            }
            return value;
        }
        public static DateTime GetSingleDateTimeValue(SettingsCollection coll, string key)
        {
            string text = GetSingleStringValue(coll, key);
            DateTime value;
            if (!DateTime.TryParse(text, out value))
            {
                throw new Exception(string.Format("Setting '{0}' failed to parse as DateTime: '{1}'", key, value));
            }
            return value;
        }
    }
}
