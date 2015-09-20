using System.Collections.Generic;
using SmallWorld.Models.Units;
using SmallWorld.Models.Utils;
using SmallWorld.ViewModels.Utils;
using System.Collections.ObjectModel;

namespace SmallWorld.ViewModels
{
    public class UnitsContext : BaseViewModel
    {
        public ObservableCollection<Unit> Units { get; private set; }

        public Point Coordinates { get; private set; }

        public Color Color { get; private set; }

        public string Type
        {
            get
            {
                if(Units.Count > 0)
                {
                    return Units[0].GetType().Name;
                }
                return "BaseUnit";
            }
        }

        public int Count
        {
            get
            {
                return Units.Count;
            }
        }

        public UnitsContext(Point coordinates, List<Unit> units, Color color)
        {
            Units = new ObservableCollection<Unit>(units);
            Coordinates = coordinates;
            Color = color;
        }
    }
}