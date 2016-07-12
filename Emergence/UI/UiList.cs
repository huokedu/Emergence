using System;
using System.Collections.Generic;
using System.IO;
using Emergence.Utilities;
using libtcod;

namespace Emergence.Ui {
	public class UiList<T> {
		private List<T> Options { get; set; }
		private Action<Point, T, bool> RenderMethod { get; set; }
		private int selectedIndex { get; set; }
		
		public int SelectedIndex { 
			get {
				return selectedIndex;
			}
			set {
				selectedIndex = value;
				if(selectedIndex < 0) { selectedIndex = Options.Count - 1; }
				else if(selectedIndex >= Options.Count) { selectedIndex = 0; }
			}
		}
		public T Selected { 
			get {
				return Options[SelectedIndex];
			}
			set {
				var index = Options.IndexOf(value);
				if(index >= 0) {
					SelectedIndex = index;
				}
			}
		}
		public int VerticalSpacing { get; set; }
		public int PageSize { get; set; }
		public int NumberOfPages {
			get {
				var pageCount = (Options.Count / PageSize) + 1;
				if(Options.Count % PageSize == 0) {
					pageCount -= 1;
				}
				return pageCount;
			}
		}
		public int CurrentPage {
			get {
				return SelectedIndex / PageSize;
			}
		}
		
		public UiList(List<T> options, Action<Point, T, bool> renderMethod) {
			Options = options;
			RenderMethod = renderMethod;
			SelectedIndex = 0;
			VerticalSpacing = 2;
			PageSize = 10;
		}
		
		public void Render(Point position) {
			var indexOffset = CurrentPage * PageSize;
			var renderOffset = new Point(position);
			for(int i = 0; i < PageSize; ++i) {
				if(i + indexOffset >= Options.Count) { break; }
				RenderMethod(renderOffset, Options[i + indexOffset], SelectedIndex == i + indexOffset);
				renderOffset.Y += VerticalSpacing;
			}
		}
		
		public void RenderScrollBar(Point position, int height) {
			var renderOffset = new Point(position);
			if(CurrentPage == 0) {
				// render top arrow, dull, at renderOffset
			} else {
				// render top arrow, bright, at renderOffset
			}
			if(CurrentPage == NumberOfPages - 1) {
				// render bottom arrow, dull, at renderOffset + height + 1
			} else {
				// render bottom arrow, bright, at renderOffset + height + 1
			}
			height -= 2;
			var barSize = height / NumberOfPages;
			var barOffset = barSize * CurrentPage;
			if(barSize < 1) { barSize = 1; }
			for(int i = 0; i < height; ++i) {
				// Check if we're in the bar range, or if this is the end of the bar and the last page (to handle odd bar sizes)
				if((i >= barOffset && i < barOffset + barSize) || (i == height - 1 && CurrentPage == NumberOfPages - 1)) {
                    TCODConsole.root.print(renderOffset.X, renderOffset.Y + i + 1, "#");
				} else {
                    TCODConsole.root.print(renderOffset.X, renderOffset.Y + i + 1, "|");
                }
			}
		}
	}
}