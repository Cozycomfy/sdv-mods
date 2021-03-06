﻿using Microsoft.Xna.Framework;
using StardewValley;
using System;
using System.Collections.Generic;
using System.Linq;
using SObject = StardewValley.Object;

namespace DeluxeGrabberRedux
{
    class SlimeHutchGrabber : ObjectsMapGrabber
    {
        public SlimeHutchGrabber(ModEntry mod, GameLocation location) : base(mod, location)
        {
        }

        public override bool GrabObject(Vector2 tile, SObject obj)
        {
            if (Config.slimeHutch && obj.Name == "Slime Ball")
            {
                // impl @ StardewValley::Object::checkForAction::name.Equals("Slime Ball")
                List<SObject> items = new List<SObject>();
                var r = new Random((int)Game1.stats.DaysPlayed + (int)Game1.uniqueIDForThisGame + (int)tile.X * 77 + (int)tile.Y * 777 + 2);

                var amount = r.Next(10, 21);
                var slimes = new SObject(ItemIds.Slime, amount);
                items.Add(slimes);

                int extraSlimesAmount = 0;
                while (r.NextDouble() < 0.33) extraSlimesAmount++;
                var extraSlimes = new SObject(ItemIds.PetrifiedSlime, extraSlimesAmount);
                items.Add(extraSlimes);

                if (TryAddItems(items))
                {
                    Location.Objects.Remove(tile);
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }
    }
}
