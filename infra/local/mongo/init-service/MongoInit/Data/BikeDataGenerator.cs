namespace MongoInit.Data;

public static class BikeDataGenerator
{
    private static readonly Random _random = new(126);

    private static readonly string[] _bikeBrands =
    [
        "TrailMaster",
        "PeakRider",
        "VelocityCycles",
        "SummitBikes",
        "AdventureWorks",
        "MountainEdge",
        "UrbanCruiser",
        "GravelPro",
        "E-BikeXpress",
        "RideNation"
    ];

    private static readonly Dictionary<string, string[]> _bikeCategories = new()
    {
        { "Mountain Bike", new[] { "Cross Country", "Trail", "All-Mountain", "Enduro", "Downhill" } },
        { "Electric Bike", new[] { "E-Mountain", "E-Road", "E-Commuter", "E-Cargo", "E-Gravel" } },
        { "Gravel Bike", new[] { "Adventure", "Race", "Bikepacking", "All-Road" } }
    };

    public static List<Product> GenerateBikes(long timestamp)
    {
        var bikes = new List<Product>();

        var productNumber = 1000;

        foreach (var brand in _bikeBrands)
        {
            foreach (var category in _bikeCategories)
            {
                var subcategory = category.Value[_random.Next(category.Value.Length)];

                bikes.Add(CreateNewProduct(
                    productNumber,
                    brand,
                    category.Key,
                    subcategory,
                    timestamp));

                productNumber++;
            }
        }

        return bikes;
    }

    private static Product CreateNewProduct(
        int productNumber,
        string brand,
        string category,
        string subcategory,
        long timestampUnixTimeSeconds)
    {
        var (basePrice, currentPrice, isOnSale, salePercentage) = PriceDetailsGenerator.GeneratePriceDetails(
            fromPrice: 1200,
            toPrice: 8500,
            chanceOfSale: 0.3,
            minSalePercentage: 10,
            maxSalePercentage: 30,
            _random);

        var sku = $"BIK-{productNumber:D4}";

        return new Product
        {
            Id = ObjectIdGenerator.GenerateId(sku, timestampUnixTimeSeconds),
            ProductId = ProductIdGenerator.GenerateProductId(productNumber, brand),
            Sku = sku,
            Name = $"{brand} {subcategory} {GenerateModelName()}",
            Description = $"Premium {subcategory.ToLower()} bike from {brand}. Built for {category.ToLower()} enthusiasts seeking performance and reliability on any terrain.",
            BasePrice = basePrice,
            IsOnSale = isOnSale,
            SalePercentage = salePercentage,
            CurrentPrice = currentPrice,
            Brand = brand,
            Type = "Bike",
            Category = category,
            Subcategory = subcategory,
            UpdatedAtUnixTimeSeconds = timestampUnixTimeSeconds,
            PrimaryImageUrl = $"https://example.com/bikes/{productNumber:D4}/primary.jpg",
            ImageUrls = GenerateImageUrls("bikes", productNumber, _random.Next(4, 7)),
            Features = GenerateBikeFeatures(category),
            Specifications = GenerateBikeSpecifications(category),
            Variants = GenerateBikeVariants(productNumber, category)
        };
    }

    private static List<string> GenerateImageUrls(string productType, long productId, int count)
    {
        var urls = new List<string>();
        for (var i = 1; i <= count; i++)
        {
            urls.Add($"https://example.com/{productType}/{productId:D4}/{i}.jpg");
        }
        return urls;
    }

    private static List<string> GenerateBikeFeatures(string category)
    {
        var commonFeatures = new List<string>
            {
                "Lightweight aluminum or carbon frame",
                "Hydraulic disc brakes for reliable stopping power",
                "Tubeless-ready wheelset",
                "Ergonomic saddle and grips"
            };

        var categoryFeatures = category switch
        {
            "Mountain Bike" => new List<string>
                {
                    "Full suspension with adjustable travel",
                    "Wide knobby tires for aggressive traction",
                    "Dropper seatpost for technical descents",
                    "1x12 drivetrain for simplicity and range",
                    "Rock Shox or Fox suspension",
                    "Tapered head tube for precision steering"
                },
            "Electric Bike" => new List<string>
                {
                    "Mid-drive motor with 250W-750W power",
                    "Integrated battery with 400-700Wh capacity",
                    "LCD display with ride modes and battery level",
                    "Pedal-assist with multiple power levels",
                    "Range up to 60-100 miles per charge",
                    "Regenerative braking system"
                },
            "Gravel Bike" => new List<string>
                {
                    "Versatile tire clearance up to 45mm+",
                    "Multiple mounting points for racks and bags",
                    "Endurance geometry for all-day comfort",
                    "Wide-range gearing for varied terrain",
                    "Flared drop bars for control",
                    "Carbon fork for vibration damping"
                },
            _ => new List<string>()
        };

        var allFeatures = commonFeatures.Concat(categoryFeatures).ToList();
        return allFeatures.OrderBy(x => _random.Next()).Take(_random.Next(6, 9)).ToList();
    }

    private static Dictionary<string, string> GenerateBikeSpecifications(string category)
    {
        var frameSizes = new[] { "XS", "S", "M", "L", "XL" };
        var wheelSizes = category switch
        {
            "Mountain Bike" => ["27.5\"", "29\""],
            "Electric Bike" => ["27.5\"", "29\"", "700c"],
            "Gravel Bike" => ["700c"],
            _ => new[] { "700c" }
        };

        var frameMaterials = new[] { "Aluminum", "Carbon Fiber", "Aluminum/Carbon" };
        var suspensionTravel = category == "Mountain Bike"
            ? ["100mm", "120mm", "140mm", "150mm", "160mm", "180mm"] :
            new[] { "N/A" };

        var specs = new Dictionary<string, string>
            {
                { "frameSize", frameSizes[_random.Next(frameSizes.Length)] },
                { "wheelSize", wheelSizes[_random.Next(wheelSizes.Length)] },
                { "frameMaterial", frameMaterials[_random.Next(frameMaterials.Length)] },
                { "weight", $"{_random.Next(22, 35)} lbs" },
                { "gearing", _random.Next(2) == 0 ? "1x12 speed" : "2x11 speed" },
                { "brakeType", "Hydraulic Disc" },
                { "maxRiderWeight", $"{_random.Next(250, 320)} lbs" }
            };

        if (category == "Mountain Bike")
        {
            specs.Add("suspensionTravel", suspensionTravel[_random.Next(suspensionTravel.Length)]);
            specs.Add("suspensionType", _random.Next(2) == 0 ? "Full Suspension" : "Hardtail");
        }

        if (category == "Electric Bike")
        {
            specs.Add("motorPower", $"{_random.Next(250, 750)}W");
            specs.Add("batteryCapacity", $"{_random.Next(400, 700)}Wh");
            specs.Add("maxRange", $"{_random.Next(40, 100)} miles");
            specs.Add("maxSpeed", _random.Next(2) == 0 ? "20 mph (Class 1)" : "28 mph (Class 3)");
        }

        if (category == "Gravel Bike")
        {
            specs.Add("tireClearance", _random.Next(2) == 0 ? "45mm" : "50mm");
            specs.Add("bottleCageMounts", "3");
        }

        return specs;
    }

    private static List<ProductVariant> GenerateBikeVariants(int productId, string category)
    {
        var colorLookup = new Dictionary<string, string>
        {
            { "Matte Black", "#1C1C1C" },
            { "Gloss Red", "#FF0000" },
            { "Electric Blue", "#007BFF" },
            { "Forest Green", "#228B22" },
            { "Titanium Gray", "#8B8B8B" },
            { "Orange", "#FFA500" },
            { "Purple", "#800080" },
            { "White", "#FFFFFF" }
        };
        var frameSizes = new[] { "XS", "S", "M", "L", "XL" };

        var variants = new List<ProductVariant>();
        var variantCounter = 1;

        foreach (var size in frameSizes)
        {
            var selectedColors = colorLookup.Keys.OrderBy(x => _random.Next()).Take(_random.Next(2, 4));

            foreach (var color in selectedColors)
            {
                var priceModifier = size switch
                {
                    "XS" => 0m,
                    "S" => 0m,
                    "M" => 0m,
                    "L" => 50m,
                    "XL" => 100m,
                    _ => 0m
                };

                var buildKit = _random.Next(3) switch
                {
                    0 => "Base",
                    1 => "Sport",
                    2 => "Elite",
                    _ => "Base"
                };

                priceModifier += buildKit switch
                {
                    "Base" => 0m,
                    "Sport" => 500m,
                    "Elite" => 1200m,
                    _ => 0m
                };

                var components = buildKit switch
                {
                    "Base" => category == "Electric Bike" ? "Shimano Deore, Basic Display" : "Shimano Deore",
                    "Sport" => category == "Electric Bike" ? "Shimano XT, Color Display" : "Shimano XT",
                    "Elite" => category == "Electric Bike" ? "Shimano XTR, Premium Display" : "Shimano XTR",
                    _ => "Shimano Deore"
                };

                variants.Add(new ProductVariant
                {
                    Sku = $"BIK-{productId:D4}-V{variantCounter:D2}",
                    Name = $"{size} - {color} - {buildKit}",
                    PriceModifier = priceModifier,
                    ImageUrl = $"https://example.com/bikes/{productId:D4}/variants/{size.ToLower()}-{color.Replace(" ", "-").ToLower()}-{buildKit.ToLower()}.jpg",
                    Attributes = new Dictionary<string, string>
                    {
                        { "frameSize", size },
                        { "color", color },
                        { "colorHex", colorLookup[color] },
                        { "buildKit", buildKit },
                        { "components", components }
                    },
                    StockQuantity = _random.Next(0, 12)
                });

                variantCounter++;
            }
        }

        return variants;
    }

    private static string GenerateModelName()
    {
        var names = new[] { "Apex", "Ranger", "Summit", "Trailblazer", "Nomad", "Fusion", "Velocity", "Ridge", "Phoenix", "Vortex" };
        var versions = new[] { "SL", "Comp", "Expert", "Pro", "Elite", "X" };
        return $"{names[_random.Next(names.Length)]} {versions[_random.Next(versions.Length)]}";
    }
}
