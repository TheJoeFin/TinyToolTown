---
name: "into-md"
tagline: "A CLI to fetch clean urls as clean markdown for LLMs"
author: "Nico Baier"
author_github: "nbbaier"
github_url: "https://github.com/nbbaier/into-md"
website_url: "https://into-md.nicobaier.com"
tags: ["cli", "markdown", "developer-tools", "ai"]
date_added: "2026-02-14"
featured: false
ai_summary: "Fetch any webpage and get beautifully clean markdown out the other end — perfect for feeding web content to LLMs without all the HTML cruft and JavaScript noise getting in the way."
ai_features: ["🔍 Auto-detects SPAs and falls back to headless browser rendering when needed", "📄 Readability-powered content extraction pulls the actual article, not the navbar", "🍪 Cookie authentication support for fetching content behind logins", "📋 Clean YAML frontmatter with title and metadata included automatically"]
---

into-md turns any webpage into clean, LLM-friendly markdown—no fuss, no mess. Smart  auto-detect skips unnecessary browser renders, caching keeps things speedy, and it handles cookies, tables-to-JSON, and all the web's chaos with grace. One command does it all: `into-md <url>`