namespace MongoInit.Data;

public static class KayakDataGenerator
{
    private static readonly string[] _kayakBrands =
    [
        "OceanPro", "RiverQuest", "AquaGlide", "WaveRider", "PaddleMaster", "SeaExplorer"
    ];

    private static readonly Dictionary<string, string[]> _kayakCategories = new()
    {
        { "Recreational", new[] { "Sit-In Kayak", "Sit-On-Top Kayak", "Inflatable Kayak" } },
        { "Touring", new[] { "Sea Kayak", "Folding Kayak", "Whitewater Kayak" } },
        { "Fishing", new[] { "Fishing Kayak", "Pedal Kayak", "Tandem Kayak" } }
    };

    private static readonly Random _random = new(42);

    public static List<Product> GenerateKayakData(long timestampUnixTimeSeconds)
    {
        var kayaks = new List<Product>();
        var productCounter = 1000;
        foreach (var brand in _kayakBrands)
        {
            foreach (var category in _kayakCategories)
            {
                var subcategory = category.Value[_random.Next(category.Value.Length)];
                kayaks.Add(CreateNewProduct(productCounter, brand, category.Key, subcategory, timestampUnixTimeSeconds));
                productCounter++;
            }
        }
        return kayaks;
    }

    private static Product CreateNewProduct(
        long productNumber,
        string brand,
        string category,
        string subcategory,
        long timestampUnixTimeSeconds)
    {
        var (basePrice, currentPrice, isOnSale, salePercentage) = PriceDetailsGenerator.GeneratePriceDetails(
            fromPrice: 400,
            toPrice: 3500,
            chanceOfSale: 0.3,
            minSalePercentage: 10,
            maxSalePercentage: 30,
            _random);

        var sku = $"KYK-{productNumber:D4}";

        return new Product
        {
            Id = ObjectIdGenerator.GenerateId(sku, timestampUnixTimeSeconds),
            ProductId = ProductIdGenerator.GenerateProductId(productNumber, brand),
            Sku = sku,
            Name = $"{brand} {subcategory} {GenerateModelName()}",
            Description = $"High-quality {subcategory.ToLower()} kayak from {brand}. Perfect for {category.ToLower()} adventures with excellent stability and performance.",
            BasePrice = basePrice,
            IsOnSale = isOnSale,
            SalePercentage = salePercentage,
            CurrentPrice = currentPrice,
            Brand = brand,
            Type = "Kayak",
            Category = category,
            Subcategory = subcategory,
            UpdatedAtUnixTimeSeconds = timestampUnixTimeSeconds,
            Features = GenerateKayakFeatures(category),
            Specifications = GenerateKayakSpecifications(category),
            Variants = GenerateKayakVariants(productNumber),
            PrimaryImageUrl = $"https://example.com/kayaks/{productNumber:D4}/primary.jpg",
            ImageUrls = GenerateImageUrls("kayaks", productNumber, _random.Next(3, 6))
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

    private static string GenerateModelName()
    {
        var prefixes = new[] { "Pro", "Elite", "Sport", "Adventure", "Explorer", "Expedition" };
        var suffixes = new[] { "XT", "LX", "GT", "XL", "Pro", "Max" };
        return $"{prefixes[_random.Next(prefixes.Length)]} {suffixes[_random.Next(suffixes.Length)]}";
    }

    private static List<string> GenerateKayakFeatures(string category)
    {
        var commonFeatures = new List<string>
        {
            "Adjustable padded seat with lumbar support",
            "Molded footrests for ergonomic positioning",
            "Bow and stern carry handles",
            "UV-resistant polyethylene construction"
        };

        var categoryFeatures = category switch
        {
            "Recreational" =>
                [
                    "Large cockpit for easy entry and exit",
                    "Storage hatch with bungee deck rigging",
                    "Built-in cup holder and paddle holder",
                    "Stable flat bottom design"
                ],
            "Touring" =>
                [
                    "Retractable skeg for tracking control",
                    "Sealed bulkheads with waterproof hatches",
                    "Compass mount and chart deck",
                    "Extended keel for straight-line tracking"
                ],
            "Fishing" =>
                [
                    "Multiple rod holders and gear tracks",
                    "Tackle storage compartments",
                    "Standing platform with non-slip surface",
                    "Anchor trolley system included"
                ],
            "Whitewater" =>
                [
                    "High-impact rocker design",
                    "Thigh braces for control",
                    "Reinforced stern and bow",
                    "Quick-drain scupper holes"
                ],
            "Racing" =>
                [
                    "Lightweight carbon fiber layup option",
                    "Narrow racing hull design",
                    "Adjustable foot brace system",
                    "Competition-grade rudder system"
                ],
            _ => new List<string>()
        };

        var allFeatures = commonFeatures.Concat(categoryFeatures).ToList();
        return allFeatures.OrderBy(x => _random.Next()).Take(_random.Next(5, 8)).ToList();
    }

    private static Dictionary<string, string> GenerateKayakSpecifications(string category)
    {
        var lengths = category switch
        {
            "Recreational" => ["9'6\"", "10'0\"", "10'6\"", "11'0\""],
            "Touring" => ["12'0\"", "13'6\"", "14'0\"", "15'6\"", "16'0\""],
            "Fishing" => ["10'6\"", "11'6\"", "12'0\"", "13'0\""],
            "Whitewater" => ["6'6\"", "7'0\"", "7'6\"", "8'0\"", "8'6\""],
            "Racing" => ["17'0\"", "18'0\"", "19'0\"", "21'0\""],
            _ => new[] { "10'0\"" }
        };

        var widths = category switch
        {
            "Recreational" => ["28\"", "29\"", "30\"", "31\"", "32\""],
            "Touring" => ["21\"", "22\"", "23\"", "24\""],
            "Fishing" => ["32\"", "33\"", "34\"", "36\""],
            "Whitewater" => ["24\"", "25\"", "26\"", "27\""],
            "Racing" => ["17\"", "18\"", "19\"", "20\""],
            _ => new[] { "28\"" }
        };

        var weights = category switch
        {
            "Recreational" => _random.Next(40, 60),
            "Touring" => _random.Next(45, 65),
            "Fishing" => _random.Next(60, 85),
            "Whitewater" => _random.Next(35, 50),
            "Racing" => _random.Next(25, 40),
            _ => _random.Next(40, 60)
        };

        var capacities = category switch
        {
            "Recreational" => _random.Next(250, 350),
            "Touring" => _random.Next(280, 380),
            "Fishing" => _random.Next(350, 500),
            "Whitewater" => _random.Next(200, 275),
            "Racing" => _random.Next(220, 280),
            _ => _random.Next(250, 350)
        };

        return new Dictionary<string, string>
        {
            { "Length", lengths[_random.Next(lengths.Length)] },
            { "Width", widths[_random.Next(widths.Length)] },
            { "Weight", $"{weights} lbs" },
            { "Capacity", $"{capacities} lbs" },
            { "Material", _random.Next(2) == 0 ? "High-Density Polyethylene" : "Thermoformed ABS" },
            { "CockpitSize", $"{_random.Next(34, 48)}\" x {_random.Next(18, 24)}\"" },
            { "HullType", _random.Next(2) == 0 ? "Flat Bottom" : "V-Shaped" }
        };
    }

    private static List<ProductVariant> GenerateKayakVariants(long productNumber)
    {
        var colorLookup = new Dictionary<string, string>
        {
            { "Ocean Blue", "#1E90FF" },
            { "Fire Red", "#FF4500" },
            { "Forest Green", "#228B22" },
            { "Sunset Orange", "#FF8C00" },
            { "Arctic White", "#F0F8FF" },
            { "Charcoal Gray", "#36454F" },
            { "Yellow", "#FFD700" },
            { "Lime Green", "#32CD32" }
        };
        var colorNames = colorLookup.Keys.ToArray();
        var packageNames = new[] { "Basic", "Standard", "Deluxe" };

        var variants = new List<ProductVariant>();
        var variantCounter = 1;

        foreach (var package in packageNames)
        {
            var selectedColors = colorNames.OrderBy(x => _random.Next()).Take(_random.Next(2, 4));

            foreach (var color in selectedColors)
            {
                var priceModifier = package switch
                {
                    "Basic" => 0m,
                    "Standard" => Math.Round((decimal)_random.Next(50, 150), 2),
                    "Deluxe" => Math.Round((decimal)_random.Next(200, 400), 2),
                    _ => 0m
                };

                var packageIncludes = package switch
                {
                    "Basic" => "Kayak only",
                    "Standard" => "Kayak + Paddle + Life Vest",
                    "Deluxe" => "Kayak + Premium Paddle + Life Vest + Dry Bag + Roof Rack Straps",
                    _ => "Kayak only"
                };

                variants.Add(new ProductVariant
                {
                    Sku = $"KYK-{productNumber:D4}-V{variantCounter:D2}",
                    Name = $"{color} - {package} Package",
                    PriceModifier = priceModifier,
                    ImageUrl = $"https://example.com/kayaks/{productNumber:D4}/variants/{color.Replace(" ", "-").ToLower()}-{package.ToLower()}.jpg",
                    Attributes = new Dictionary<string, string>
                        {
                            { "color", color },
                            { "colorHex", colorLookup[color] },
                            { "package", package },
                            { "includes", packageIncludes }
                        },
                    StockQuantity = _random.Next(0, 25)
                });

                variantCounter++;
            }
        }

        return variants;
    }
}
