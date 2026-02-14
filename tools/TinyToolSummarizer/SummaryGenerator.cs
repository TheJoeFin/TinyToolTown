using GitHub.Copilot.SDK;

namespace TinyToolSummarizer;

/// <summary>
/// Uses the Copilot SDK to generate fun AI summaries of tools.
/// </summary>
public static class SummaryGenerator
{
    public static async Task<string?> GenerateSummaryAsync(
        CopilotSession session, string toolName, string tagline, string readmeContent)
    {
        var prompt = $"""
            You are a witty, fun tech writer for "Tiny Tool Town" — a curated collection of small, 
            delightful open source tools. Write an AI summary for the following tool.
            
            Format your response EXACTLY like this (use | as separator between items):
            
            SUMMARY: A fun, enthusiastic 1-2 sentence overview of what the tool does and why its awesome.
            FEATURES: 🔥 Feature one | ⚡ Feature two | 🎯 Feature three
            
            Rules:
            - The SUMMARY should be conversational and playful, 1-2 sentences max
            - Pick 3-4 key features from the README, each prefixed with a fun relevant emoji
            - Separate features with " | " (space pipe space)
            - Do NOT use quotes, colons, or newlines in feature text
            - Do NOT include the tool name in the summary
            - Keep the whole thing concise but informative
            
            Tool: {toolName}
            Tagline: {tagline}
            
            README content:
            {readmeContent}
            """;

        var result = new System.Text.StringBuilder();
        var done = new TaskCompletionSource();

        var subscription = session.On(evt =>
        {
            switch (evt)
            {
                case AssistantMessageDeltaEvent delta:
                    result.Append(delta.Data.DeltaContent);
                    break;
                case AssistantMessageEvent msg:
                    if (result.Length == 0)
                        result.Append(msg.Data.Content);
                    break;
                case SessionIdleEvent:
                    done.TrySetResult();
                    break;
                case SessionErrorEvent err:
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.Write($" [AI error: {err.Data.Message}] ");
                    Console.ResetColor();
                    done.TrySetResult();
                    break;
            }
        });

        try
        {
            await session.SendAsync(new MessageOptions { Prompt = prompt });
            await done.Task;
        }
        finally
        {
            subscription.Dispose();
        }

        var summary = result.ToString().Trim().Trim('"');
        // Keep newlines intact so ParseResponse can split SUMMARY/FEATURES lines
        summary = summary.Replace("\r", "").Trim();
        return string.IsNullOrWhiteSpace(summary) ? null : summary;
    }

    /// <summary>
    /// Parses the structured AI response into summary and features.
    /// </summary>
    public static (string summary, string? features) ParseResponse(string raw)
    {
        string summary = raw;
        string? features = null;

        // Try to extract SUMMARY: and FEATURES: lines
        var lines = raw.Split('\n', StringSplitOptions.RemoveEmptyEntries);
        foreach (var line in lines)
        {
            var trimmed = line.Trim();
            if (trimmed.StartsWith("SUMMARY:", StringComparison.OrdinalIgnoreCase))
            {
                summary = trimmed["SUMMARY:".Length..].Trim();
            }
            else if (trimmed.StartsWith("FEATURES:", StringComparison.OrdinalIgnoreCase))
            {
                features = trimmed["FEATURES:".Length..].Trim();
            }
        }

        // Clean up YAML-unsafe characters
        summary = summary.Replace("\n", " ").Replace("\r", "").Trim();
        features = features?.Replace("\n", " ").Replace("\r", "").Trim();

        return (summary, features);
    }
}
