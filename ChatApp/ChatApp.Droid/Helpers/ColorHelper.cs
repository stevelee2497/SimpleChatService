﻿using System;
using System.Globalization;
using Android.Graphics;
using Android.Support.V4.Content;

namespace ChatApp.Droid.Helpers
{
	public static class ColorHelper
	{
		public static Color Default => ToColor("e4e4e4");
		public static Color Green => ToColor("2CB273");
		public static Color DarkGray => ToColor("666666");
		public static Color LightBlack => ToColor("888888");
		public static Color LightBlue => ToColor("3fb8ea");

		public static Color FromColorId(int colorId)
		{
			return new Color(ContextCompat.GetColor(Android.App.Application.Context, colorId));
		}

		public static Color ToColor(string hexString)
		{
			if (string.IsNullOrWhiteSpace(hexString))
			{
				return Color.White;
			}

			hexString = hexString.Replace("#", string.Empty);
			hexString = hexString.Replace("argb: ", string.Empty);

			if (hexString.Length != 6 && hexString.Length != 8)
			{
				throw new Exception("Invalid hex string");
			}

			var index = -2;
			int alpha;

			if (hexString.Length == 6)
			{
				alpha = 255;
			}
			else
			{
				index += 2;
				alpha = int.Parse(hexString.Substring(index, 2), NumberStyles.AllowHexSpecifier);
			}

			var red = int.Parse(hexString.Substring(index + 2, 2), NumberStyles.AllowHexSpecifier);
			var green = int.Parse(hexString.Substring(index + 4, 2), NumberStyles.AllowHexSpecifier);
			var blue = int.Parse(hexString.Substring(index + 6, 2), NumberStyles.AllowHexSpecifier);

			return Color.Argb(alpha, red, green, blue);
		}
	}
}