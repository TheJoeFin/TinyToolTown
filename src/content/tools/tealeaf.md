---
name: "TeaLeaf"
tagline: "TeaLeaf - Schema-aware data format CLI"
author: "Jagadish Krishnan"
author_github: "krishjag"
github_url: "https://github.com/krishjag/tealeaf"
thumbnail: "/thumbnails/tealeaf.png"
website_url: "https://krishjag.github.io/tealeaf/"
tags: ["serialization", "json", "binary", "data-format", "tea-leaf"]
language: "Rust, C#"
license: "MIT"
date_added: "2026-02-12"
featured: false
ai_summary: "A clever data format that slashes your JSON bloat by defining schemas inline — field names appear once instead of repeated on every record, cutting LLM input tokens by ~51% while staying human-readable."
ai_features: ["📝 Human-readable text format compiles to compact binary for storage and transmission", "🧠 51% fewer tokens than JSON makes LLM context windows go further", "🔄 JSON interoperability so you can easily convert back and forth", "📐 Inline schemas mean validation and compression without external files"]
---

TeaLeaf is a data format that combines:
- **Human-readable text** (`.tl`) for editing and version control
- **Compact binary** (`.tlbx`) for storage and transmission
- **Inline schemas** for validation and compression
- **JSON interoperability** for easy integration