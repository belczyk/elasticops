﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using Caliburn.Micro;
using ElasticOps.Com;
using ElasticOps.Com.Infrastructure;
using ElasticOps.Com.Models;
using NLog;
using LogManager = NLog.LogManager;

namespace ElasticOps.ViewModels.ManagmentScreens
{
    public class IndicesInfoViewModel : ClusterConnectedAutorefreashScreen
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();

        private List<IndexInfoViewModel> AllIndicesInfo = new List<IndexInfoViewModel>();
        public IObservableCollection<IndexInfoViewModel> IndicesInfo { get; set; }

        public IndicesInfoViewModel(Infrastructure infrastructure)
            : base(infrastructure)
        {
            IndicesInfo = new BindableCollection<IndexInfoViewModel>();
        }

        public override void RefreshData()
        {
            try
            {
                var result = commandBus.Execute(new ClusterInfo.IndicesInfoCommand(connection));
                if (result.Failed) return;

                AllIndicesInfo.Clear();
                foreach (var indexInfo in result.Result)
                    AllIndicesInfo.Add(new IndexInfoViewModel(indexInfo));

                FilterIndices();
            }
            catch (Exception ex)
            {
                logger.Warn(ex);
            }
        }

        private void FilterIndices()
        {
            IndicesInfo.Clear();
            IndicesInfo.AddRange(AllIndicesInfo.Where(x=>ShowMarvelIndices || !x.Name.StartsWith(".marvel")));
        }

        private bool _ShowMarvelIndices;

        public bool ShowMarvelIndices
        {
            get { return _ShowMarvelIndices; }
            set
            {
                _ShowMarvelIndices = value;
                NotifyOfPropertyChange(() => ShowMarvelIndices);
                FilterIndices();
            }
        }
    }
}