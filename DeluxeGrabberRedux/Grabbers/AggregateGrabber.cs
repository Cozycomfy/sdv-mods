﻿using Microsoft.Xna.Framework;
using StardewValley;
using StardewValley.TerrainFeatures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeluxeGrabberRedux.Grabbers
{
    class AggregateObjectsGrabber : ObjectsMapGrabber
    {
        private readonly List<ObjectsMapGrabber> grabbers;

        public AggregateObjectsGrabber(ModEntry mod, GameLocation location) : base(mod, location)
        {
            grabbers = new List<ObjectsMapGrabber>()
            {
                new SlimeHutchGrabber(mod, location),
                new FarmCaveMushroomGrabber(mod, location),
                new IndoorPotGrabber(mod, location),
                new ArtifactSpotsGrabber(mod, location),
                new GenericObjectGrabber(mod, location)
            };
        }

        public override bool GrabObject(Vector2 tile, StardewValley.Object obj)
        {
            return grabbers.Select(grabber => grabber.GrabObject(tile, obj)).Any(x => x);
        }
    }

    class AggregateFeaturesGrabber : TerrainFeaturesMapGrabber
    {
        private readonly List<TerrainFeaturesMapGrabber> grabbers;

        public AggregateFeaturesGrabber(ModEntry mod, GameLocation location) : base(mod, location)
        {
            grabbers = new List<TerrainFeaturesMapGrabber>()
            {
                new ForageHoeDirtGrabber(mod, location),
                new HarvestableCropHoeDirtGrabber(mod, location),
                new FruitTreeGrabber(mod, location),
                new SeedTreeGrabber(mod, location),
                new BerryBushGrabber(mod, location)
            };
        }

        public override bool GrabFeature(Vector2 tile, TerrainFeature feature)
        {
            return grabbers.Select(grabber => grabber.GrabFeature(tile, feature)).Any(x => x);
        }
    }

    class AggregateDailyGrabber : MapGrabber
    {
        private readonly List<MapGrabber> grabbers;

        public AggregateDailyGrabber(ModEntry mod, GameLocation location) : base(mod, location)
        {
            grabbers = new List<MapGrabber>()
            {
                new AggregateObjectsGrabber(mod, location),
                new AggregateFeaturesGrabber(mod, location),
                new WoodsHardwoodGrabber(mod, location),
                new TownGarbageCanGrabber(mod, location)
            };
        }

        public override bool GrabItems()
        {
            return grabbers.Aggregate(false, (grabbed, grabber) => (grabber.CanGrab() && grabber.GrabItems()) || grabbed);
        }
    }
}
