using System;
using System.IO;
using System.IO.Compression;
using Emergence.Core;
using libtcod;

namespace Emergence.Utilities {
	public static class RexPaintImageLoader {
		public static TCODConsole LoadImage(string fileName) {
			var transparentColor = new TCODColor(255, 0, 255);
			TCODConsole image = null;
			try {
				using(var file = File.Open(fileName, FileMode.Open)) {
					var decrompressedStream = new GZipStream(file, CompressionMode.Decompress);
					var reader = new BinaryReader(decrompressedStream);
					var fileVersion = reader.ReadInt32();
					var layerCount = reader.ReadInt32();

					// Reserving space for these in advance, so I'm not doing memory
					// allocations repeatedly in the loop.
					int layerWidth = 0;
					int layerHeight = 0;
					int x, y;
					int characterCode = 0;
					TCODColor foreground = new TCODColor();
					TCODColor background = new TCODColor();

					for(int layerIndex = 0; layerIndex < layerCount; ++layerIndex) {
						layerWidth = reader.ReadInt32();
						layerHeight = reader.ReadInt32();
						if(image == null) {
							image = new TCODConsole(layerWidth, layerHeight);
							image.setBackgroundColor(transparentColor);
							image.clear();
							image.setKeyColor(transparentColor);
						}

						for(var charIndex = 0; charIndex < layerWidth * layerHeight; ++charIndex) {
							x = charIndex / layerHeight;
							y = charIndex % layerHeight;
							characterCode = reader.ReadInt32();
							foreground.Red = reader.ReadByte();
							foreground.Green = reader.ReadByte();
							foreground.Blue = reader.ReadByte();
							background.Red = reader.ReadByte();
							background.Green = reader.ReadByte();
							background.Blue = reader.ReadByte();

							// Check if the current cell is transparent.
							if(background.NotEqual(transparentColor)) {
								image.putCharEx(x, y, characterCode, foreground, background);
							}
						}
					}
				}
			} catch (Exception e) {
				Logger.Error($"Error loading file '{fileName}': {e.Message}");
			}
			return image;
		}
	}
}