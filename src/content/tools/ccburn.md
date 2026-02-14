---
name: "ccburn"
tagline: "Watch your tokens burn — TUI for Claude Code usage limits with burn-up charts, compact mode, and JSON output"
author: "Juan José Fuchs"
author_github: "JuanjoFuchs"
github_url: "https://github.com/JuanjoFuchs/ccburn"
thumbnail: "/thumbnails/ccburn.png"
tags: ["cli", "tui", "python", "terminal", "developer-tools", "ai"]
language: "Python"
license: "MIT"
date_added: "2026-02-13"
featured: false
ai_summary: "Keep tabs on your Claude Code token consumption with live burn-up charts that show you exactly how fast you're spending — complete with pace indicators that tell you if you're 🧊 cool or 🚨 too hot before you hit the wall."
ai_features: ["📊 Real-time burn-up charts with session and weekly usage visualization", "🌡️ Pace indicators show if youre cool, on track, or burning too fast", "🖥️ Compact single-line output perfect for tmux status bars", "📦 Install via npm, pip, or winget — whatever floats your boat"]
---

ccburn is a terminal tool for visualizing Claude Code token usage with live burn-up charts. I built it because I kept hitting my Claude Code usage limits without warning — now I can see my burn rate in real time and pace myself.

It shows session and weekly usage with pace indicators (🧊 behind, 🔥 on pace, 🚨 too hot), supports a compact single-line mode for tmux/status bars, and JSON output for scripting. Data is persisted in SQLite for historical trend analysis. Available via npm, pip, WinGet, or npx.