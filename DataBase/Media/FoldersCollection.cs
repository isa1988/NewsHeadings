﻿using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataBase.Media
{
    [ConfigurationCollection(typeof(FolderElement))]

    class FoldersCollection : ConfigurationElementCollection
    {
        protected override ConfigurationElement CreateNewElement()
        {
            return new FolderElement();
        }

        protected override object GetElementKey(ConfigurationElement element)
        {
            return ((FolderElement) (element)).name;
        }

        public FolderElement this[int idx]
        {
            get { return (FolderElement) BaseGet(idx); }
        }
    }
}
