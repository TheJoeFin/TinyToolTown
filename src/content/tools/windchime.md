---
name: "Windchime"
tagline: "Play an audio tone in your browser based on the network traffic"
author: "Bill Johnson"
author_github: "dubrie"
github_url: "https://github.com/dubrie/Windchime"
tags: ["chrome extension", "fun", "network traffic", "data", "audio"]
language: "JavaScript"
license: "Apache-2.0"
date_added: "2026-02-15"
featured: false
ai_summary: "Turn your browsing into a secret symphony by hearing network requests as playful audio tones that change with file size—it’s like giving your tabs a soundtrack! Perfect for anyone curious about the behind-the-scenes chatter of the web."
ai_features: ["🎵 Audio feedback based on network request size", "🔊 Dynamic pitch mapping from tiny high tones to deep low notes", "⏱️ Tone duration scales with file size for immersive audio cues", "🎛️ Easy toggle control and test buttons in a popup interface"]
---

I made Windchime to scratch an itch I've had for a long time to easily tell how much data websites are loading as I browse. Turning the network traffic into audio tones means I can passively monitor the site for network traffic without having to take up screen real estate. Audio tones are assigned as follows: 

- Small files: High pitch, short duration
- Medium files: Medium pitch, medium duration
- Large files: Low pitch, long duration

I've been surprised and delighted to see which websites minimize network traffic and data and which ones abuse them!