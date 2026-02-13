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
---

ccburn is a terminal tool for visualizing Claude Code token usage with live burn-up charts. I built it because I kept hitting my Claude Code usage limits without warning — now I can see my burn rate in real time and pace myself.

It shows session and weekly usage with pace indicators (🧊 behind, 🔥 on pace, 🚨 too hot), supports a compact single-line mode for tmux/status bars, and JSON output for scripting. Data is persisted in SQLite for historical trend analysis. Available via npm, pip, WinGet, or npx.