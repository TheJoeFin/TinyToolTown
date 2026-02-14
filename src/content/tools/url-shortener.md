---
name: "URL shortener"
tagline: "A simple HTTP server application for shortening URI addresses."
author: "Vítězslav Šimon"
author_github: "VitSimon"
github_url: "https://github.com/VitSimon/urlshort"
tags: ["web", "server", "url", "shortener"]
language: "Go"
license: "MIT"
date_added: "2026-02-13"
featured: false
ai_summary: "A no-frills URL shortener you can self-host in seconds — just spin up the container, throw URLs at it, and get short codes back without any tracking or analytics nonsense."
ai_features: ["🔗 Simple GET request creates short codes for any URL", "💾 SQLite storage keeps everything in a single portable database file", "🐳 Scratch-based container image means its tiny and dependency-free", "🔒 Zero traffic logging because your links are your business"]
---

Provides shortened codes for given URI addresses. Each URI address can be defined multiple times and will receive a new code each time. Data is stored in an SQLite database. The server does not record any traffic or code usage. The tool is designed for self-hosting in a local environment.