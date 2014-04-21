﻿using System;
using Caliburn.Micro;
using ElasticOps.Model;

namespace ElasticOps.ViewModels.ManagmentScreens
{
    public class NodesInfoViewModel : ClusterConnectedAutorefreashScreen
    {
        public IObservableCollection<NodeInfoViewModel> NodesInfo { get; set; }

        public NodesInfoViewModel(Settings settings, IEventAggregator eventAggregator)
            : base(settings.ClusterUri, eventAggregator)
        {
            NodesInfo = new BindableCollection<NodeInfoViewModel>();
        }

        public override void RefreshData()
        {
            var nodesInfo = new ClusterInfo().GetNodesInfo(new Uri("http://localhost:9200"));
            foreach (var node in nodesInfo)
            {
                NodesInfo.Add(new NodeInfoViewModel(node));
            }
        }
    }
}
