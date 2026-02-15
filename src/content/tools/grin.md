---
name: "grin"
tagline: "Make INI files greppable — like gron, but for INI (single binary, ~2 MB)"
author: "Charles Yost"
author_github: "Yoshi325"
github_url: "https://github.com/Yoshi325/grin"
tags: ["cli", "ini", "grep", "unix", "config"]
language: "go"
license: "MIT"
date_added: "2026-02-15"
featured: false
ai_summary: "Turn your INI files into a grep-friendly playground and slice through config chaos like a pro with this nifty little binary! It’s like giving your INI a superpower to be searched, filtered, and transformed with ease."
ai_features: ["🔥 Converts INI files into discrete, greppable assignments", "⚡ Reconstructs filtered INI files with --ungrin", "🎯 Works seamlessly with Unix tools like grep, sed, and diff", "💾 Lightweight single binary around 2 MB"]
---

Grin can round trip .ini files into discrete, greppable assignments and back again. Inspired by gron (which does the same for JSON), grin lets you explore and filter .ini config files with standard *nix tools like grep, sed, and diff. It can also be useful in a PowerShell pipeline. I built it because INI files are everywhere but it was hard to search across sections while keeping context. It's delightful because it round-trips perfectly: pipe through grep, then grin --ungrin to get a valid .ini file back with just the parts you want.