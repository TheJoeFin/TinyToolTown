using GitHub.Copilot.SDK;
using TinyToolSummarizer;

Console.ForegroundColor = ConsoleColor.Cyan;
Console.WriteLine("╔══════════════════════════════════════════════════════════════╗");
Console.WriteLine("║       Tiny Tool Town — AI Summary Generator 🏘️✨          ║");
Console.WriteLine("║       Powered by GitHub Copilot SDK                        ║");
Console.WriteLine("╚══════════════════════════════════════════════════════════════╝");
Console.ResetColor();
Console.WriteLine();

// Resolve the content/tools directory
var contentDir = args.Length > 0
    ? args[0]
    : Path.GetFullPath(Path.Combine(AppContext.BaseDirectory, "..", "..", "..", "..", "..", "src", "content", "tools"));

if (!Directory.Exists(contentDir))
{
    Console.ForegroundColor = ConsoleColor.Red;
    Console.WriteLine($"❌ Tools directory not found: {contentDir}");
    Console.WriteLine("   Usage: dotnet run [path-to-src/content/tools]");
    Console.ResetColor();
    return;
}

Console.WriteLine($"📂 Tools directory: {contentDir}");

var allMdFiles = Directory.GetFiles(contentDir, "*.md");
Console.WriteLine($"📋 Found {allMdFiles.Length} tool files\n");

// --- Run mode selection ---
var mode = 0; // 0 = NewOnly, 1 = All, 2 = Single
string? singleTarget = null;

Console.ForegroundColor = ConsoleColor.Yellow;
Console.WriteLine("🎯 How would you like to run?");
Console.ResetColor();
Console.WriteLine("   1. Only tools without AI summaries (default)");
Console.WriteLine("   2. Re-run on ALL tools (overwrite existing)");
Console.WriteLine("   3. Pick a specific tool");
Console.Write("\nEnter choice (1-3) [default: 1]: ");

var modeChoice = Console.ReadLine()?.Trim();
switch (modeChoice)
{
    case "2":
        mode = 1; // All
        Console.ForegroundColor = ConsoleColor.Magenta;
        Console.WriteLine("🔄 Will re-generate ALL summaries\n");
        Console.ResetColor();
        break;
    case "3":
        mode = 2; // Single
        // List tools for selection
        var toolNames = allMdFiles
            .Select((f, i) => (Index: i + 1, Name: Path.GetFileNameWithoutExtension(f)))
            .ToList();

        Console.WriteLine();
        foreach (var t in toolNames)
            Console.WriteLine($"   {t.Index,3}. {t.Name}");

        Console.Write($"\nEnter tool number (1-{toolNames.Count}): ");
        var toolChoice = Console.ReadLine()?.Trim();
        if (int.TryParse(toolChoice, out var toolIndex) && toolIndex >= 1 && toolIndex <= toolNames.Count)
        {
            singleTarget = allMdFiles[toolIndex - 1];
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.WriteLine($"🎯 Will process: {Path.GetFileName(singleTarget)}\n");
            Console.ResetColor();
        }
        else
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("❌ Invalid selection. Exiting.");
            Console.ResetColor();
            return;
        }
        break;
    default:
        Console.ForegroundColor = ConsoleColor.Magenta;
        Console.WriteLine("⏭️  Will skip tools that already have AI summaries\n");
        Console.ResetColor();
        break;
}

var mdFiles = mode == 2 && singleTarget != null
    ? new[] { singleTarget }
    : allMdFiles;

// Check prerequisites
Console.WriteLine("🔍 Checking Copilot prerequisites...");
CopilotClient? client = null;
CopilotSession? session = null;

try
{
    client = new CopilotClient();
    await client.StartAsync();
    Console.ForegroundColor = ConsoleColor.Green;
    Console.WriteLine("✅ Copilot client started\n");
    Console.ResetColor();

    session = await client.CreateSessionAsync(new SessionConfig
    {
        Model = "gpt-4o",
        Streaming = true
    });
    Console.ForegroundColor = ConsoleColor.Green;
    Console.WriteLine($"✅ Session created (ID: {session.SessionId})\n");
    Console.ResetColor();

    var httpClient = new HttpClient();
    httpClient.DefaultRequestHeaders.UserAgent.ParseAdd("TinyToolSummarizer/1.0");

    var processed = 0;
    var skipped = 0;
    var failed = 0;

    foreach (var mdFile in mdFiles)
    {
        var fileName = Path.GetFileName(mdFile);
        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.Write($"[{processed + skipped + failed + 1}/{mdFiles.Length}] ");
        Console.ResetColor();
        Console.Write($"{fileName} ");

        try
        {
            var content = await File.ReadAllTextAsync(mdFile);
            var parsed = FrontmatterParser.Parse(content);

            // Skip if already has an AI summary (unless re-running all or single)
            if (mode == 0 && parsed.Frontmatter.ContainsKey("ai_summary"))
            {
                Console.ForegroundColor = ConsoleColor.DarkGray;
                Console.WriteLine("⏭️  already has ai_summary");
                Console.ResetColor();
                skipped++;
                continue;
            }

            // If re-running, strip existing ai_summary and ai_features first
            if (mode != 0 && parsed.Frontmatter.ContainsKey("ai_summary"))
            {
                content = FrontmatterParser.RemoveField(content, "ai_summary");
                content = FrontmatterParser.RemoveField(content, "ai_features");
            }

            if (!parsed.Frontmatter.TryGetValue("github_url", out var githubUrl))
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("⚠️  no github_url");
                Console.ResetColor();
                failed++;
                continue;
            }

            var toolName = parsed.Frontmatter.GetValueOrDefault("name", fileName);
            var tagline = parsed.Frontmatter.GetValueOrDefault("tagline", "");

            // Fetch the README from GitHub
            var readme = await ReadmeFetcher.FetchReadmeAsync(httpClient, githubUrl.Trim('"'));

            if (string.IsNullOrWhiteSpace(readme))
            {
                Console.ForegroundColor = ConsoleColor.DarkGray;
                Console.WriteLine("⚠️  no README found, using existing content");
                Console.ResetColor();
                // Fall back to the body content if available
                readme = parsed.Body;
            }

            if (string.IsNullOrWhiteSpace(readme) && string.IsNullOrWhiteSpace(tagline))
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("⚠️  no content to summarize");
                Console.ResetColor();
                failed++;
                continue;
            }

            // Generate AI summary
            var rawResponse = await SummaryGenerator.GenerateSummaryAsync(
                session, toolName.Trim('"'), tagline.Trim('"'), readme ?? "");

            if (string.IsNullOrWhiteSpace(rawResponse))
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("❌ empty summary");
                Console.ResetColor();
                failed++;
                continue;
            }

            // Parse the structured response into summary + features
            var (summary, features) = SummaryGenerator.ParseResponse(rawResponse);

            // Write the ai_summary and ai_features back into the frontmatter
            var updatedContent = FrontmatterParser.AddField(content, "ai_summary", summary);
            if (!string.IsNullOrWhiteSpace(features))
            {
                var featureList = features.Split(" | ", StringSplitOptions.RemoveEmptyEntries)
                    .Select(f => f.Trim())
                    .Where(f => !string.IsNullOrEmpty(f))
                    .ToArray();
                updatedContent = FrontmatterParser.AddArrayField(updatedContent, "ai_features", featureList);
            }
            await File.WriteAllTextAsync(mdFile, updatedContent);

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"✅ \"{summary[..Math.Min(60, summary.Length)]}...\"");
            if (!string.IsNullOrWhiteSpace(features))
            {
                Console.ForegroundColor = ConsoleColor.DarkGray;
                Console.WriteLine($"   Features: {features}");
            }
            Console.ResetColor();
            processed++;
        }
        catch (Exception ex)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"❌ {ex.Message}");
            Console.ResetColor();
            failed++;
        }
    }

    Console.WriteLine();
    Console.ForegroundColor = ConsoleColor.Cyan;
    Console.WriteLine("═══════════════════════════════════════════════");
    Console.WriteLine($"  ✅ Processed: {processed}");
    Console.WriteLine($"  ⏭️  Skipped:   {skipped}");
    Console.WriteLine($"  ❌ Failed:    {failed}");
    Console.WriteLine("═══════════════════════════════════════════════");
    Console.ResetColor();
}
catch (Exception ex)
{
    Console.ForegroundColor = ConsoleColor.Red;
    Console.WriteLine($"\n❌ Fatal error: {ex.Message}");
    if (ex.InnerException != null)
        Console.WriteLine($"   Inner: {ex.InnerException.Message}");
    Console.ResetColor();
}
finally
{
    if (session != null) await session.DisposeAsync();
    if (client != null) await client.DisposeAsync();
    Console.WriteLine("\n🛑 Done.");
}
