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
using System.Diagnostics.Contracts;

namespace AurelienRibon.Ui.SyntaxHighlightBox {
	public class TextUtilities {
		/// <summary>
		/// Returns the raw number of the current line count.
		/// </summary>
		public static int GetLineCount(String text) {
			int lcnt = 1;
			for (int i = 0; i < text.Length; i++) {
				if (text[i] == '\n')
					lcnt += 1;
			}
			return lcnt;
		}

		/// <summary>
		/// Returns the index of the first character of the
		/// specified line. If the index is greater than the current
		/// line count, the method returns the index of the last
		/// character. The line index is zero-based.
		/// </summary>
		public static int GetFirstCharIndexFromLineIndex(string text, int lineIndex) {
			if (text == null)
				throw new ArgumentNullException("text");
			if (lineIndex <= 0)
				return 0;

			int currentLineIndex = 0;
			for (int i = 0; i < text.Length - 1; i++) {
				if (text[i] == '\n') {
					currentLineIndex += 1;
					if (currentLineIndex == lineIndex)
						return Math.Min(i + 1, text.Length - 1);
				}
			}

			return Math.Max(text.Length - 1, 0);
		}

		/// <summary>
		/// Returns the index of the last character of the
		/// specified line. If the index is greater than the current
		/// line count, the method returns the index of the last
		/// character. The line-index is zero-based.
		/// </summary>
		public static int GetLastCharIndexFromLineIndex(string text, int lineIndex) {
			if (text == null)
				throw new ArgumentNullException("text");
			if (lineIndex < 0)
				return 0;

			int currentLineIndex = 0;
			for (int i = 0; i < text.Length - 1; i++) {
				if (text[i] == '\n') {
					if (currentLineIndex == lineIndex)
						return i;
					currentLineIndex += 1;
				}
			}

			return Math.Max(text.Length - 1, 0);
		}
	}
}
