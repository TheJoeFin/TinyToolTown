namespace TinyToolSummarizer;

/// <summary>
/// Parses and manipulates YAML-style frontmatter in markdown files.
/// </summary>
public static class FrontmatterParser
{
    public record ParsedFile(Dictionary<string, string> Frontmatter, string Body, string RawFrontmatter);

    public static ParsedFile Parse(string content)
    {
        var frontmatter = new Dictionary<string, string>();
        var body = content;
        var rawFrontmatter = "";

        if (!content.StartsWith("---"))
            return new ParsedFile(frontmatter, body, rawFrontmatter);

        var endIndex = content.IndexOf("---", 3, StringComparison.Ordinal);
        if (endIndex < 0)
            return new ParsedFile(frontmatter, body, rawFrontmatter);

        rawFrontmatter = content[3..endIndex].Trim();
        body = content[(endIndex + 3)..].Trim();

        foreach (var line in rawFrontmatter.Split('\n'))
        {
            var trimmed = line.Trim();
            if (string.IsNullOrEmpty(trimmed) || !trimmed.Contains(':'))
                continue;

            var colonIndex = trimmed.IndexOf(':');
            var key = trimmed[..colonIndex].Trim();
            var value = trimmed[(colonIndex + 1)..].Trim();
            frontmatter[key] = value;
        }

        return new ParsedFile(frontmatter, body, rawFrontmatter);
    }

    public static string AddField(string content, string fieldName, string value)
    {
        if (!content.StartsWith("---"))
            return content;

        var endIndex = content.IndexOf("---", 3, StringComparison.Ordinal);
        if (endIndex < 0)
            return content;

        // Escape the value for YAML — wrap in quotes, escape inner quotes
        var escapedValue = value.Replace("\\", "\\\\").Replace("\"", "\\\"");
        var newField = $"{fieldName}: \"{escapedValue}\"";

        // Insert the new field just before the closing ---
        var before = content[..endIndex].TrimEnd();
        var after = content[endIndex..];

        return $"{before}\n{newField}\n{after}";
    }

    public static string AddArrayField(string content, string fieldName, string[] values)
    {
        if (!content.StartsWith("---"))
            return content;

        var endIndex = content.IndexOf("---", 3, StringComparison.Ordinal);
        if (endIndex < 0)
            return content;

        // Build YAML array: ai_features: ["item1", "item2"]
        var escaped = values.Select(v => $"\"{ v.Replace("\\", "\\\\").Replace("\"", "\\\"")}\"");
        var newField = $"{fieldName}: [{string.Join(", ", escaped)}]";

        var before = content[..endIndex].TrimEnd();
        var after = content[endIndex..];

        return $"{before}\n{newField}\n{after}";
    }

    public static string RemoveField(string content, string fieldName)
    {
        if (!content.StartsWith("---"))
            return content;

        var endIndex = content.IndexOf("---", 3, StringComparison.Ordinal);
        if (endIndex < 0)
            return content;

        var frontmatterBlock = content[3..endIndex];
        var lines = frontmatterBlock.Split('\n');
        var filteredLines = lines
            .Where(line =>
            {
                var trimmed = line.Trim();
                return !trimmed.StartsWith($"{fieldName}:");
            })
            .ToList();

        var newFrontmatter = string.Join('\n', filteredLines);
        var after = content[endIndex..];
        return $"---{newFrontmatter}{after}";
    }
}
