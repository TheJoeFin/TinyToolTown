---
name: "Octostore"
tagline: "Free distributed locking & leader election as a service. One binary, no dependencies"
author: "David Aronchick"
author_github: "aronchick"
github_url: "https://github.com/octostore/octostore.io"
website_url: "https://octostore.io"
tags: ["cli", "distributed-systems", "locking", "api"]
language: "Rust"
license: "MIT"
date_added: "2026-02-13"
featured: false
ai_summary: "Distributed locking without the distributed headache — a single Rust binary that gives you HTTP-based locks with proper fencing tokens, no ZooKeeper/etcd/Consul cluster required."
ai_features: ["🔐 Simple HTTP API for acquire, release, and renew with automatic TTL expiration", "🎫 Monotonically increasing fencing tokens make your locks actually safe unlike Redlock", "🐙 GitHub OAuth authentication so you can start locking in seconds", "📦 Single binary with SQLite persistence and zero external dependencies"]
---

Lock any resource with a single HTTP call. Built for CI/CD pipelines, distributed workers, and anywhere you need "only one at a time." Single Rust binary, SQLite-backed, fencing tokens, TTL expiry, GitHub OAuth. Self-host or use the free hosted version.