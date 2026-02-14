---
name: "DecentDB"
tagline: "ACID first. Everything else… eventually."
author: "Steven Hildreth"
author_github: "sphildreth"
github_url: "https://github.com/sphildreth/decentdb"
thumbnail: "/thumbnails/decentdb.png"
website_url: "https://decentdb.org"
tags: ["database", "embedded", "sqlite", "orm", "dapper"]
language: "Nim"
license: "Apache-2.0"
date_added: "2026-02-13"
featured: false
ai_summary: "A refreshingly honest embedded database that prioritizes getting the fundamentals right — rock-solid ACID transactions, crash-safe recovery, and PostgreSQL-like SQL without pretending to be something its not."
ai_features: ["🔒 ACID transactions with write-ahead logging and crash-safe recovery", "🐘 PostgreSQL-flavored SQL with JOINs, CTEs, and window functions", "👥 Snapshot isolation for concurrent readers with a single writer", "🌐 SDKs for C#, Go, Node.js, and Python so you can embed it anywhere"]
---

DecentDB is a embedded relational database engine focused on durable writes, fast reads, and predictable correctness. I created it thinking me and my AI friends could make something close to SQLite speed, turns out we did! Then I created native binding in a couple of languages and then made a DecentDB.MicroOrm and published to NuGet.org. Not terrible indeed.