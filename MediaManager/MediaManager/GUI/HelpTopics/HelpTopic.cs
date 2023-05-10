﻿using System.Collections.Generic;
using System.Drawing;

namespace MediaManager.GUI.HelpTopics
{
    public class HelpTopic
    {
        public string TreeCaption { get; set; }
        public List<HelpTopicPage> Pages { get; set; }
        public List<HelpTopic> Children { get; set; }
    }
    public class HelpTopicPage
    {
        public string PageCaption { get; set; }
        public Bitmap Image { get; set; }
        public string Content { get; set; }
    }
}