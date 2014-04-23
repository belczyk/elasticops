using System;
using System.Collections.Generic;
using Humanizer;
using Humanizer.Bytes;

namespace ElasticOps.Model
{
    public class NodeInfo
    {
        public string Name { get; set; }
        public string Hostname { get; set; }
        public string HttpAddress { get; set; }
        public Dictionary<string, string> OS { get; set; }
        public Dictionary<string, string> CPU { get; set; }
        public Dictionary<string, string> Settings { get; set; }

        public NodeInfo(Nest.NodeInfo nodeInfo)
        {
            Name = nodeInfo.Name;
            Hostname = nodeInfo.Hostname;
            HttpAddress = nodeInfo.HttpAddress;
            if (nodeInfo.Settings != null)
                Settings = new Dictionary<string, string>
                {
                    {"Data path", ((string)nodeInfo.Settings["path"]["data"]).HumanizePath()},
                    {"Configs path", ((string)nodeInfo.Settings["path"]["conf"]).HumanizePath()},
                    {"Logs path", ((string)nodeInfo.Settings["path"]["logs"]).HumanizePath()},
                };
            if (nodeInfo.OS != null)
            {
                OS = new Dictionary<string, string>
                {
                    {"# Processors", Convert.ToString(nodeInfo.OS.AvailableProcessors)},
                    {"Refresh interval", Convert.ToString(nodeInfo.OS.RefreshInterval)},
                    {"Memory", new ByteSize(nodeInfo.OS.Mem.TotalInBytes).Humanize("#.##")},
                    {"Swap", new ByteSize(nodeInfo.OS.Swap.TotalInBytes).Humanize("#.##")},
                };

                CPU = new Dictionary<string, string>
                {
                    {"Vendor", nodeInfo.OS.Cpu.Vendor},
                    {"Model", nodeInfo.OS.Cpu.Model},
                    {"MHz", Convert.ToString(nodeInfo.OS.Cpu.Mhz)},
                    {"Total cores", Convert.ToString(nodeInfo.OS.Cpu.TotalCores)},
                    {"Total sockets", Convert.ToString(nodeInfo.OS.Cpu.TotalSockets)},
                    {"Cores per socket", Convert.ToString(nodeInfo.OS.Cpu.CoresPerSocket)},
                    {"Cache size", new ByteSize(nodeInfo.OS.Cpu.CacheSizeInBytes).Humanize("#.##")},
                };
            }
        }
    }
}
