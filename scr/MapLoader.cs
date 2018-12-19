using SFML.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Xml;
using GameEngine.Tools;
using SFML.System;

namespace GameEngine
{
	public static class MapLoader
	{
		public static List<Province> LoadProvinces(string path)
		{
			var provinces = new List<Province>();

			var xDoc = new XmlDocument();
			xDoc.Load(path);
			var node = xDoc.DocumentElement["g"];

			foreach (XmlNode child in node.ChildNodes)
			{
				var id = child.Attributes["id"].Value;
				var shape = child.Attributes["d"].Value;
				var points = ConvertSvgToFloats(shape);
				var province = CreateProvince(id, points.ToList());

				provinces.Add(province);
			}

			return provinces;
		}

		private static IEnumerable<float> ConvertSvgToFloats(string svgString)
		{
			//TODO: добавить компиляцию регвыра для скорости
			//TODO: протестирова регвыр
			var regex = new Regex(@"-?\d+(\.\d*)?");
			var matches = regex.Matches(svgString);

			foreach (var match in matches)
				yield return float.Parse(match.ToString(), System.Globalization.CultureInfo.InvariantCulture);
		}

		private static Province CreateProvince(string name, List<float> points)
		{
			var shape = new ConvexShape((uint)(points.Count / 2));

			if (points.Count < 2)
				throw new ArgumentException("points count must be >= 2");
			if (points.Count % 2 != 0)
				throw new ArgumentException("points count must be % 2 == 0");

			shape.FillColor = GameRandom.GetRandomColor();

			for (var i = 0; i < points.Count; i += 2)
				shape.SetPoint((uint)(i / 2), new Vector2f(points[i], points[i + 1]));

			return new Province(name, shape);
		}
	}
}
