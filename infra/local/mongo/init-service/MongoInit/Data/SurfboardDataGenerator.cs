namespace MongoInit.Data;

public static class SurfboardDataGenerator
{
    private static readonly string[] _brands = new[]
    {
        "WaveRider", "SurfMaster", "OceanGlide", "BeachPro", "TideChaser", "SeaSurfer"
    };

    private static readonly Dictionary<string, string[]> _categories = new()
    {
        { "Shortboard", new[] { "Performance Shortboard", "Fish Shortboard", "Hybrid Shortboard" } },
        { "Longboard", new[] { "Classic Longboard", "Funboard", "Mini Mal" } },
        { "Funboard", new[] { "Egg Shape Funboard", "Foam Funboard", "Soft Top Funboard" } }
    };

    private static readonly Random _random = new(42);

    public static List<Product> GenerateSurfboardData(long timestampUnixTimeSeconds)
    {
        var surfboards = new List<Product>();
        var productCounter = 1;
        foreach (var brand in _brands)
        {
            foreach (var category in _categories)
            {
                var subcategory = category.Value[_random.Next(category.Value.Length)];
                surfboards.Add(CreateNewProduct(productCounter, brand, category.Key, subcategory, timestampUnixTimeSeconds));
                productCounter++;
            }
        }
        return surfboards;
    }

    private static Product CreateNewProduct(long productNumber, string brand, string category, string subcategory, long timestampUnixTimeSeconds)
    {
        var (basePrice, currentPrice, isOnSale, salePercentage) = PriceDetailsGenerator.GeneratePriceDetails(
            fromPrice: 300,
            toPrice: 2500,
            chanceOfSale: 0.3,
            minSalePercentage: 10,
            maxSalePercentage: 40,
            _random);

        var sku = $"SRF-{productNumber:D4}";

        return new Product
        {
            Id = ObjectIdGenerator.GenerateId(sku, timestampUnixTimeSeconds),
            ProductId = ProductIdGenerator.GenerateProductId(productNumber, brand),
            Sku = sku,
            Name = $"{brand} {subcategory} {GenerateModelName()}",
            Description = $"High-performance {subcategory.ToLower()} surfboard from {brand}. Ideal for {category.ToLower()} surfing with excellent maneuverability and speed.",
            BasePrice = basePrice,
            IsOnSale = isOnSale,
            SalePercentage = salePercentage,
            CurrentPrice = currentPrice,
            Brand = brand,
            Type = "Surfboard",
            Category = category,
            Subcategory = subcategory,
            UpdatedAtUnixTimeSeconds = DateTimeOffset.UtcNow.ToUnixTimeSeconds(),
            Features = GenerateSurfboardFeatures(category),
            Specifications = GenerateSurfboardSpecifications(category),
            Variants = GenerateSurfboardVariants(productNumber, category),
            PrimaryImageUrl = $"https://example.com/surfboards/{productNumber:D4}/primary.jpg",
            ImageUrls = GenerateImageUrls("surfboards", productNumber, _random.Next(3, 6))
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
        var names = new[] { "Rocket", "Driver", "Voodoo", "Ghost", "Shadow", "Thunder", "Lightning", "Wave", "Tide", "Storm" };
        var versions = new[] { "V1", "V2", "V3", "Pro", "Elite", "XL" };
        return $"{names[_random.Next(names.Length)]} {versions[_random.Next(versions.Length)]}";
    }

    private static List<string> GenerateSurfboardFeatures(string category)
    {
        var commonFeatures = new List<string>
            {
                "Hand-shaped by professional shapers",
                "High-quality resin and fiberglass construction",
                "Leash plug installed",
                "Professional-grade finish"
            };

        var categoryFeatures = category switch
        {
            "Shortboard" => new List<string>
                {
                    "Aggressive rocker for steep waves",
                    "Narrow tail for tight turns",
                    "Thruster or quad fin setup",
                    "Low entry rocker for paddle speed",
                    "Thin rails for responsiveness"
                },
            "Longboard" => new List<string>
                {
                    "Wide nose for noseriding",
                    "Single fin box with stabilizers",
                    "Thick rails for stability",
                    "Classic outline shape",
                    "Volan glass option available"
                },
            "Fish" => new List<string>
                {
                    "Wide swallow tail design",
                    "Twin or quad fin configuration",
                    "Low rocker for speed",
                    "Wide point forward outline",
                    "Retro-inspired aesthetics"
                },
            "Mid-Length" => new List<string>
                {
                    "Versatile wave range performance",
                    "Easy paddling with good glide",
                    "2+1 or thruster fin setup",
                    "Forgiving rocker profile",
                    "Beginner to intermediate friendly"
                },
            "Alternative" => new List<string>
                {
                    "Soft foam top deck",
                    "Durable construction for beginners",
                    "Safe for learning",
                    "High buoyancy for easy paddling",
                    "Multiple fin options"
                },
            _ => []
        };

        var allFeatures = commonFeatures.Concat(categoryFeatures).ToList();
        return allFeatures.OrderBy(x => _random.Next()).Take(_random.Next(5, 8)).ToList();
    }

    private static Dictionary<string, string> GenerateSurfboardSpecifications(string category)
    {
        var dimensions = category switch
        {
            "Shortboard" => (
                lengths: ["5'6\"", "5'8\"", "5'10\"", "6'0\"", "6'2\""],
                widths: ["18.5\"", "18.75\"", "19\"", "19.25\"", "19.5\""],
                thicknesses: ["2.25\"", "2.3\"", "2.35\"", "2.4\"", "2.45\""],
                volumes: ["24L", "26L", "28L", "30L", "32L"]
            ),
            "Longboard" => (
                lengths: ["8'6\"", "9'0\"", "9'2\"", "9'6\"", "10'0\""],
                widths: ["22\"", "22.5\"", "23\"", "23.5\"", "24\""],
                thicknesses: ["2.75\"", "2.85\"", "3\"", "3.1\"", "3.25\""],
                volumes: ["65L", "70L", "75L", "80L", "85L"]
            ),
            "Fish" => (
                lengths: ["5'4\"", "5'6\"", "5'8\"", "5'10\"", "6'0\""],
                widths: ["20\"", "20.5\"", "21\"", "21.5\"", "22\""],
                thicknesses: ["2.35\"", "2.4\"", "2.45\"", "2.5\"", "2.6\""],
                volumes: ["30L", "32L", "34L", "36L", "38L"]
            ),
            "Mid-Length" => (
                lengths: ["6'6\"", "7'0\"", "7'2\"", "7'6\"", "8'0\""],
                widths: ["20.5\"", "21\"", "21.5\"", "22\"", "22.5\""],
                thicknesses: ["2.5\"", "2.6\"", "2.7\"", "2.75\"", "2.85\""],
                volumes: ["40L", "45L", "50L", "55L", "60L"]
            ),
            "Alternative" => (
                lengths: ["5'6\"", "6'0\"", "7'0\"", "8'0\"", "9'0\""],
                widths: ["20\"", "21\"", "22\"", "23\"", "24\""],
                thicknesses: ["2.5\"", "2.75\"", "3\"", "3.25\"", "3.5\""],
                volumes: ["35L", "45L", "55L", "65L", "75L"]
            ),
            _ => (
                lengths: new[] { "6'0\"" },
                widths: new[] { "19\"" },
                thicknesses: new[] { "2.4\"" },
                volumes: new[] { "30L" }
            )
        };

        var finSystems = new[] { "FCS II", "Futures", "US Box", "Glass-On" };
        var constructions = new[] { "PU/Polyester", "EPS/Epoxy", "Carbon Wrap", "Soft Top" };

        return new Dictionary<string, string>
            {
                { "length", dimensions.lengths[_random.Next(dimensions.lengths.Length)] },
                { "width", dimensions.widths[_random.Next(dimensions.widths.Length)] },
                { "thickness", dimensions.thicknesses[_random.Next(dimensions.thicknesses.Length)] },
                { "volume", dimensions.volumes[_random.Next(dimensions.volumes.Length)] },
                { "finSystem", finSystems[_random.Next(finSystems.Length)] },
                { "construction", constructions[_random.Next(constructions.Length)] },
                { "tailShape", GetTailShape(category) },
                { "riderWeight", $"{_random.Next(120, 220) * 0.45} kg" }
            };
    }

    private static string GetTailShape(string category)
    {
        var shapes = category switch
        {
            "Shortboard" => ["Squash", "Round", "Swallow", "Square"],
            "Longboard" => ["Square", "Round Pin", "Rounded Square"],
            "Fish" => ["Swallow", "Bat Tail", "W Tail"],
            "Mid-Length" => ["Squash", "Round", "Pin"],
            "Alternative" => ["Squash", "Round", "Square"],
            _ => new[] { "Squash" }
        };
        return shapes[_random.Next(shapes.Length)];
    }

    private static List<ProductVariant> GenerateSurfboardVariants(long productNumber, string category)
    {
        var colorLookup = new Dictionary<string, string>
        {
            { "Clear", "#FFFFFF" },
            { "White Tint", "#F0F0F0" },
            { "Blue Tint", "#ADD8E6" },
            { "Green Tint", "#90EE90" },
            { "Pink Tint", "#FFC0CB" },
            { "Solar Orange", "#FFA500" },
            { "Resin Tint", "#D2B48C" }
        };
        var colors = colorLookup.Keys.ToList();

        var sizeSuffix = category switch
        {
            "Longboard" => ["8'6\"", "9'0\"", "9'6\""],
            "Shortboard" => ["5'8\"", "5'10\"", "6'0\"", "6'2\""],
            "Fish" => ["5'6\"", "5'8\"", "5'10\""],
            "Mid-Length" => ["7'0\"", "7'6\"", "8'0\""],
            _ => new[] { "6'0\"", "7'0\"", "8'0\"" }
        };

        var variants = new List<ProductVariant>();
        var variantCounter = 1;

        foreach (var size in sizeSuffix)
        {
            var selectedColors = colors.OrderBy(x => _random.Next()).Take(_random.Next(2, 4));

            foreach (var color in selectedColors)
            {
                var finSetup = category switch
                {
                    "Shortboard" => _random.Next(2) == 0 ? "Thruster (3-fin)" : "Quad (4-fin)",
                    "Longboard" => "Single + Side Bites",
                    "Fish" => _random.Next(2) == 0 ? "Twin Fin" : "Quad",
                    "Mid-Length" => "2+1 Setup",
                    "Alternative" => "Thruster (3-fin)",
                    _ => "Thruster (3-fin)"
                };

                var priceModifier = size switch
                {
                    var s when s.Contains("5'") => -50m,
                    var s when s.Contains("6'") => 0m,
                    var s when s.Contains("7'") => 50m,
                    var s when s.Contains("8'") => 100m,
                    var s when s.Contains("9'") => 150m,
                    var s when s.Contains("10'") => 200m,
                    _ => 0m
                };

                if (color != "Clear") priceModifier += 30m;

                variants.Add(new ProductVariant
                {
                    Sku = $"SRF-{productNumber:D4}-V{variantCounter:D2}",
                    Name = $"{size} - {color}",
                    PriceModifier = priceModifier,
                    Attributes = new Dictionary<string, string>
                        {
                            { "size", size },
                            { "color", color },
                            { "colorHex", colorLookup[color] },
                            { "finSetup", finSetup }
                        },
                    ImageUrl = $"https://example.com/surfboards/{productNumber:D4}/variants/{variantCounter:D2}.jpg",
                    StockQuantity = _random.Next(0, 15)
                });

                variantCounter++;
            }
        }

        return variants;
    }
}
