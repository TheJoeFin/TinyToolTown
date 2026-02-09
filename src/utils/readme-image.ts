const imageCache = new Map<string, string | null>();

export async function getReadmeImage(githubUrl: string): Promise<string | null> {
  if (imageCache.has(githubUrl)) {
    return imageCache.get(githubUrl)!;
  }

  try {
    const match = githubUrl.match(/github\.com\/([^/]+)\/([^/]+)/);
    if (!match) return null;
    const [, owner, repo] = match;

    for (const branch of ['main', 'master']) {
      const rawUrl = `https://raw.githubusercontent.com/${owner}/${repo}/${branch}/README.md`;
      const res = await fetch(rawUrl);
      if (!res.ok) continue;

      const readme = await res.text();

      // Skip badges/shields — find the first "real" image
      const badgeHosts = ['img.shields.io', 'badge.fury.io', 'badgen.net', 'badges.', 'coveralls.io', 'codecov.io', 'travis-ci.', 'ci.appveyor.com', 'github.com/workflows'];
      const isBadge = (url: string) => badgeHosts.some(h => url.includes(h)) || url.includes('/badge');

      // Match all markdown images: ![alt](url)
      const mdMatches = [...readme.matchAll(/!\[[^\]]*\]\(([^)]+)\)/g)];
      const firstRealMd = mdMatches.find(m => !isBadge(m[1]));
      // Match all HTML images: <img src="url"
      const htmlMatches = [...readme.matchAll(/<img[^>]+src=["']([^"']+)["']/gi)];
      const firstRealHtml = htmlMatches.find(m => !isBadge(m[1]));

      const imgPath = firstRealMd?.[1] || firstRealHtml?.[1] || null;
      if (!imgPath) continue;

      // Resolve relative URLs to absolute GitHub raw URLs
      let absoluteUrl = imgPath;
      if (!imgPath.startsWith('http')) {
        const cleanPath = imgPath.replace(/^\.\//, '');
        absoluteUrl = `https://raw.githubusercontent.com/${owner}/${repo}/${branch}/${cleanPath}`;
      }

      imageCache.set(githubUrl, absoluteUrl);
      return absoluteUrl;
    }
  } catch (e) {
    console.warn(`Failed to fetch README image for ${githubUrl}:`, e);
  }

  imageCache.set(githubUrl, null);
  return null;
}
