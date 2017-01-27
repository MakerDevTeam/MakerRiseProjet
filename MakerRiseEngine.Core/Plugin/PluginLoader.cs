﻿using Maker.RiseEngine.Core.EngineDebug;
using Maker.RiseEngine.Core.Storage;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Maker.RiseEngine.Core.Plugin
{
    public class PluginLoader<PluginType> where PluginType : IPlugin
    {
        public Dictionary<string, PluginType> Plugins;
        List<string> LoadedPlugins;
        List<string> OnIntializationPlugin;

        public PluginLoader(string pluginPath) {
            // setup list.
            Plugins = new Dictionary<string, PluginType>();
            LoadedPlugins = new List<string>();
            OnIntializationPlugin = new List<string>();

            foreach (PluginType p in LoadPluginFrom(pluginPath)) {

                Plugins.Add(p.Name, p);

            }
        }
        

        public void Include(object Parent, string pluginName) {

            var plug = Plugins[pluginName];

            if (OnIntializationPlugin.Contains(pluginName))
            {

                DebugLogs.WriteLog($"A circular dependency has been detected! {Parent.GetType().Name} refers to {pluginName} which makes itself reference to {Parent.GetType().Name}.");

            }
            else {

                if (!LoadedPlugins.Contains(pluginName)) {
                    DebugLogs.WriteLog("Load pluging :" + plug.GetType().Name, LogType.Info, GetType().Name);
                    OnIntializationPlugin.Add(pluginName);
                    plug.Initialize(this);
                    OnIntializationPlugin.Remove(pluginName);

                    LoadedPlugins.Add(pluginName);
                }


            }


        }

        public void initializePlugin() {

            foreach (var plug in Plugins) {

                DebugLogs.WriteLog("Initializing " + plug.Key, EngineDebug.LogType.Info, "Engine");
                Include(this,plug.Key);

            }

        }

        private List<PluginType> LoadPluginFrom(string Path) {

            if (Directory.Exists(Path))
            {

                List<PluginType> PluginList = new List<PluginType>();

                foreach (var dir in Directory.GetDirectories(Path))
                {

                    string metaFilePath = dir + "\\plugin.risemeta";

                    if (File.Exists(metaFilePath))
                    {

                        // Load meta file.
                        DataSheet riseMetaData = new DataSheet(metaFilePath);
                        riseMetaData.Load();

                        // Getting metaData.
                        string plugin_NeedBuild = riseMetaData.GetData("NeedBuild");
                        string plugin_Path = riseMetaData.GetData("Path");


                        if (plugin_NeedBuild != "null" && plugin_Path != "null")
                        {

                            if (plugin_NeedBuild == "true" || plugin_NeedBuild == "false") {

                                string assemblie_path = dir + '\\' + plugin_Path;

                                if (File.Exists(assemblie_path))
                                {


                                    if (plugin_NeedBuild == "true") {

                                        DebugLogs.WriteLog("Building plugin..." + metaFilePath, LogType.Info, GetType().Name);
                                        BuildOutput buildout = Builder.Build(assemblie_path, dir + "\\plugin_build.dll");

                                        if (buildout.Sucess) {

                                            assemblie_path = dir + "\\plugin_build.dll";

                                        }

                                    }

                                    if (File.Exists(assemblie_path))
                                    {

                                        Assembly pluginAsm = Assembly.LoadFrom(Environment.CurrentDirectory + '\\' + assemblie_path);
                                        
                                        PluginList.AddRange(LoadAssembly(pluginAsm));
                                        

                                    }
                                    else {

                                        DebugLogs.WriteLog("Plugin assembly not found !", LogType.Warning, GetType().Name);

                                    }

                                }


                            } else {

                                DebugLogs.WriteLog("Invalide value for 'NeedBuild' in " + metaFilePath , LogType.Warning, GetType().Name);

                            }

                        }
                        else
                        {

                            DebugLogs.WriteLog("Invalide Plugin metadata : " + metaFilePath, LogType.Warning, GetType().Name);

                        }

                    }
                    else {

                        DebugLogs.WriteLog("No plugin metadata found : " + dir, LogType.Warning, GetType().Name);

                    }

                }

                return PluginList;

            }
            else {

                return new List<PluginType>();
            }
        }
        private List<PluginType> LoadAssembly(Assembly assembly)
        {
            DebugLogs.WriteLog("load \'" + assembly.FullName + "\'", LogType.Info, GetType().Name);

            Type pluginType = typeof(PluginType);
            List<Type> pluginTypes = new List<Type>();

            if (assembly != null)
            {
                Type[] types = assembly.GetTypes();

                foreach (Type type in types)
                {
                    if (type.IsInterface || type.IsAbstract)
                    {
                        continue;
                    }
                    else
                    {
                        if (type.GetInterface(pluginType.FullName) != null)
                        {
                            pluginTypes.Add(type);
                        }
                    }
                }
            }

            List<PluginType> plugins = new List<PluginType>(pluginTypes.Count);
            foreach (Type type in pluginTypes)
            {
                PluginType plugin = (PluginType)Activator.CreateInstance(type);
                plugins.Add(plugin);
            }

            return plugins;
        }

    }
}
