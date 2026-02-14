---
name: "forkwatch"
tagline: "CLI that discovers meaningful patches hiding in GitHub forks"
author: "Ben Curtis"
author_github: "stympy"
github_url: "https://github.com/stympy/forkwatch"
tags: ["cli", "git", "github", "forks"]
language: "Go"
license: "MIT"
date_added: "2026-02-13"
featured: false
ai_summary: "A brilliant CLI for maintainers who suspect good fixes are hiding in their forks — it scans repos to surface patches that multiple people independently made but never bothered to PR, because convergence is the ultimate code smell."
ai_features: ["🔍 Discovers meaningful changes lurking in forks that were never submitted upstream", "🎯 Highlights convergence when multiple forks independently fix the same thing", "🩹 Outputs unified diffs ready to pipe directly into git apply", "📊 JSON output with recommended changes for automation and scripting"]
---

Forkwatch analyzes GitHub repository forks to find changes that haven't been submitted as pull requests. It filters out noise (bot commits, lock files, CI config), groups changes by file, and highlights convergence... when multiple independent forks touch the same code, that signals that something needs fixing upstream. Built to help maintainers discover valuable work happening in forks without requiring formal PRs.