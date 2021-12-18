using OnlineShop.Server.DB.Mappers;
using System.Collections.Generic;

namespace OnlineShop.Shared
{
    public record Statistics()
    {
        public Dictionary<int, Dictionary<int, double>> stats = new Dictionary<int, Dictionary<int, double>>();

        public void Add(ValueStats pair)
        {
            if (stats.ContainsKey(pair.SizeID))
            {
                if (stats[pair.SizeID].ContainsKey(pair.ColorPaletteID))
                {
                    stats[pair.SizeID][pair.ColorPaletteID]++;
                }
                else
                {
                    stats[pair.SizeID].Add(pair.ColorPaletteID, pair.count);
                }
            }
            else
            {
                stats.Add(pair.SizeID, new Dictionary<int, double>());
                stats[pair.SizeID].Add(pair.ColorPaletteID, pair.count);
            }
        }
        
        public void Add(UserImage image)
        {
            if (stats.ContainsKey(image.SizeID))
            {
                if (stats[image.SizeID].ContainsKey(image.ColorPaletteID))
                {
                    stats[image.SizeID][image.ColorPaletteID]++;
                }
                else
                {
                    stats[image.SizeID].Add(image.ColorPaletteID, 1);
                }
            }
            else
            {
                stats.Add(image.SizeID, new Dictionary<int, double>());
                stats[image.SizeID].Add(image.ColorPaletteID, 1);
            }
        }
        
        public void Add(GoodsStats good)
        {
            if (stats.ContainsKey(good.SizeID))
            {
                if (stats[good.SizeID].ContainsKey(good.ColorPaletteID))
                {
                    stats[good.SizeID][good.ColorPaletteID]++;
                }
                else
                {
                    stats[good.SizeID].Add(good.ColorPaletteID, 0);
                }
            }
            else
            {
                stats.Add(good.SizeID, new Dictionary<int, double>());
            }
        }

        public bool isSizeExists(int sizeID)
        {
            return stats.ContainsKey(sizeID);
        }
        
        public bool isColorPaletteExists(int sizeID, int colorPaletteID)
        {
            if (isSizeExists(sizeID))
            {
                if (stats[sizeID].ContainsKey(colorPaletteID))
                {
                    return true;
                }
            }

            return false;
        }

        public double GetAmount(int sizeID, int colorPaletteID)
        {
            if (isColorPaletteExists(sizeID, colorPaletteID))
            {
                return stats[sizeID][colorPaletteID];
            }
            else
            {
                return 0.1f;
            }
        }
    }
}