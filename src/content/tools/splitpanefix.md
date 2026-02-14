---
name: "SplitPaneFix"
tagline: "One-click fix for Windows Terminal split panes"
author: "Scott Hanselman"
author_github: "shanselman"
github_url: "https://github.com/shanselman/splitpanefix"
tags: ["cli", "windows", "terminal"]
language: "PowerShell"
license: "MIT"
date_added: "2026-02-09"
ai_summary: "Ever split a terminal pane only to land back in your home directory like some kind of lost puppy? This clever script makes Windows Terminal actually remember where you are, so splits and duplicates stay right where the action is."
ai_features: ["🔧 One-click fix for split panes and duplicate tabs", "🎯 Works with Oh My Posh or standalone", "⌨️ Configures Alt+Shift and Ctrl+Shift shortcuts automatically", "🤖 Optional GitHub Copilot CLI integration with spc command"]
---

When you split a pane in Windows Terminal, the new pane starts in your home directory instead of where you were. This fixes that. One script, one fix, preserves your current directory using Oh My Posh and OSC 99. The kind of fix that should be built in.
