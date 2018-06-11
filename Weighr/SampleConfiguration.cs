// Copyright (c) Microsoft. All rights reserved.


using System;
using System.Collections.Generic;
using Windows.UI.Xaml.Controls;
using Weighr;

namespace SDKTemplate
{
    public partial class MainPage : Page
    {
        public const string FEATURE_NAME = "WiFi";

        List<Scenario> scenarios = new List<Scenario>
        {
            new Scenario() { Title="WiFi Connect", ClassType=typeof(WifiConnect)}
        };
    }

    public class Scenario
    {
        public string Title { get; set; }
        public Type ClassType { get; set; }
    }
}
