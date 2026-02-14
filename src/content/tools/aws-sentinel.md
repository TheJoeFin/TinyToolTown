---
name: "aws-sentinel"
tagline: "AWS Sentinel is a powerful command-line security scanner for AWS resources."
author: "Rishab Kumar"
author_github: "rishabkumar7"
github_url: "https://github.com/rishabkumar7/aws-sentinel"
website_url: "https://pypi.org/project/aws-sentinel/"
tags: ["cli", "cloud", "security", "aws"]
language: "Python"
license: "MIT"
date_added: "2026-02-13"
featured: false
ai_summary: "A no-nonsense CLI that scans your AWS account for the security slip-ups that keep you up at night — public buckets, open ports, unencrypted volumes, and users without MFA all surfaced in one clean report."
ai_features: ["🔍 Scans S3, EC2 security groups, EBS volumes, and IAM for common misconfigurations", "🤖 Natural language queries powered by Amazon Bedrock let you ask security questions in plain English", "📊 Output as table, JSON, or CSV for easy integration", "⚡ One command scan across your entire AWS environment"]
---

AWS Sentinel is a powerful command-line security scanner for AWS resources. It helps identify common security issues and misconfigurations in your AWS environment. Now featuring natural language queries powered by Amazon Bedrock!

AWS Sentinel currently checks for the following security issues:

- S3 Buckets: Identifies publicly accessible buckets
- EC2 Security Groups: Finds security groups with port 22 (SSH) open to the public
- EBS Volumes: Detects unencrypted volumes
- IAM Users: Identifies users without Multi-Factor Authentication (MFA)