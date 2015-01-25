//---------------------------------------------------------------------------------------------------------------------------------
// Copyright 2010 AurelienRibon
// http://syntaxhighlightbox.codeplex.com/

// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at

//    http://www.apache.org/licenses/LICENSE-2.0

// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.
//---------------------------------------------------------------------------------------------------------------------------------
// Changes made to original source code as a part of ElasticOps project are distributed under the same license
// This file has not been modifed
//---------------------------------------------------------------------------------------------------------------------------------


using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Media;

namespace AurelienRibon.Ui.SyntaxHighlightBox {
	public class DrawingControl : FrameworkElement {
		private VisualCollection visuals;
		private DrawingVisual visual;

		public DrawingControl() {
			visual = new DrawingVisual();
			visuals = new VisualCollection(this);
			visuals.Add(visual);
		}

		public DrawingContext GetContext() {
			return visual.RenderOpen();
		}

		protected override int VisualChildrenCount {
			get { return visuals.Count; }
		}

		protected override Visual GetVisualChild(int index) {
			if (index < 0 || index >= visuals.Count)
				throw new ArgumentOutOfRangeException();
			return visuals[index];
		}
	}
}
