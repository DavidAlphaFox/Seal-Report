﻿//
// Copyright (c) Seal Report, Eric Pfirsch (sealreport@gmail.com), http://www.sealreport.org.
// Licensed under the Apache License, Version 2.0 (the "License"); you may not use this file except in compliance with the License. http://www.apache.org/licenses/LICENSE-2.0..
//
using System;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;

namespace Seal.Model
{
    public class SecurityUserProfile
    {
        private string _culture;
        private string _view = "reports";
        private List<string> _dashboards = new List<string>();

        public string Culture { get => _culture; set => _culture = value; }
        public string View { get => _view; set => _view = value; }
        public List<string> Dashboards { get => _dashboards; set => _dashboards = value; }

        static public SecurityUserProfile LoadFromFile(string path)
        {
            SecurityUserProfile result = null;
            try
            {
                StreamReader sr = new StreamReader(path);
                XmlSerializer serializer = new XmlSerializer(typeof(SecurityUserProfile));
                result = (SecurityUserProfile)serializer.Deserialize(sr);
                result.Path = path;
                sr.Close();
            }
            catch (Exception ex)
            {
                throw new Exception(string.Format("Unable to read the file '{0}'.\r\n{1}\r\n", path, ex.Message, ex.StackTrace));
            }
            return result;
        }

        public void SaveToFile()
        {
            SaveToFile(Path);
        }

        public void SaveToFile(string path)
        {
            try
            {
                _dashboards.RemoveAll(i => string.IsNullOrEmpty(i));

                XmlSerializer serializer = new XmlSerializer(typeof(SecurityUserProfile));
                StreamWriter sw = new StreamWriter(path);
                serializer.Serialize(sw, this);
                sw.Close();
            }
            finally
            {
                Path = path;
            }
        }

        [XmlIgnore]
        public string Path;

    }
}
