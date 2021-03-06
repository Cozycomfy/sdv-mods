﻿using StardewValley;
using SObject = StardewValley.Object;

namespace DeluxeGrabberRedux
{
    class InventoryEntry
    {
        public readonly Item Item;
        public readonly int Quality;
        public readonly int Id;

        public string QualityName
        {
            get
            {
                var qualityName = "Normal";
                switch (Quality)
                {
                    case SObject.medQuality:
                        qualityName = "Silver";
                        break;
                    case SObject.highQuality:
                        qualityName = "Gold";
                        break;
                    case SObject.bestQuality:
                        qualityName = "Iridium";
                        break;
                }
                return qualityName;
            }
        }

        public string Name
        {
            get
            {
                return Item.Name;
            }
        }

        public InventoryEntry(Item item)
        {
            Item = item;
            Quality = item is SObject obj ? obj.Quality : 0;
            Id = item.ParentSheetIndex;
        }

        public override bool Equals(object obj)
        {
            return obj is InventoryEntry item && item.Id == Id && item.Quality == Quality;
        }

        public override int GetHashCode()
        {
            return (Quality << 4) ^ Id;
        }
    }
}
