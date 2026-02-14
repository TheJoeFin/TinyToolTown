---
name: "lastexecuterecord"
tagline: "It is a simple launcher,  run multiple tasks at the set intervals when it starts."
author: "Kazushi Kamegawa"
author_github: "kkamegawa"
github_url: "https://github.com/kkamegawa/lastexecrecord"
tags: ["cli", "windows"]
language: "C++"
license: "MIT"
date_added: "2026-02-13"
featured: false
ai_summary: "A lightweight Windows launcher that runs your registered commands on a smart schedule — it remembers when things last ran and politely skips them if the interval hasnt passed yet, perfect for automating those terminal-opens-do-the-things routines."
ai_features: ["⏱️ Tracks last run time with second precision and skips commands before their interval is up", "🌐 Network-aware execution can skip or wait based on connectivity and metered status", "🔒 Safe operation with config file locking and atomic updates", "🖥️ Integrates beautifully as a Windows Terminal startup profile"]
---

There are many ways to install applications, like winget, npm install, volta, or dotnet tool update. But it’s hard to know when to run them, and it’s annoying to run the commands every time. With this tool, I can run them regularly based on the last time they were executed. I can also choose not to run them when I’m on a metered connection.