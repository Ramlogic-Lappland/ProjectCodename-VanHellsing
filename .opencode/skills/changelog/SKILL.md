---
name: changelog
description: Create or update CHANGELOG.md for the latest tagged release using only git history (tags, commit messages, diffs). Use when asked to "update the changelog", "write the changelog", "add a changelog entry", or "create CHANGELOG.md".
license: See LICENSE.md
---

# Update CHANGELOG.md

Write the changelog entry for the latest release of this project, using **git history only**.
Keep token usage low: read git output, not source code.

Resources next to this file:
- `CHANGELOG-TEMPLATE.md` - the structure every entry must follow.
- `CHANGELOG-EXAMPLE.md` - a correct, filled-in changelog.

## Permissions (strict)

- You MAY run read-only `git` commands and read files.
- You MAY create or edit **only** `CHANGELOG.md` in the repository root.
- You MUST NOT edit source code, tests, configuration, dependencies or other docs.
- You MUST NOT commit, push, merge, tag, delete, install packages or open pull requests.
- You MUST NOT invent features, fixes, versions, dates or ticket numbers.
- Ask the developer for confirmation before doing **anything** not listed above.

## Steps

### 1. Read the current changelog

If `CHANGELOG.md` exists, read it and note the newest version (the first `## [X.Y.Z]` line).
Copy its style (bullet character, trailing periods, capitalization) for the new entry.
Do not rewrite old entries. If they break the format rules below, list the problems in
your final report and ask the developer before fixing them.

### 2. Find the release (tags only)

```bash
git tag --sort=-v:refname
```

- **No tags:** STOP. Tell the developer: "There are no git tags. Create a release tag
  (for example `git tag v0.1.0`) and run me again."
- The version is the tag name without a leading `v` (`v0.1.1` -> `0.1.1`).
- **Newest tag is already in CHANGELOG.md:** STOP. Tell the developer that the changelog
  is up to date, and that commits after the tag need a new tag first.
- **CHANGELOG.md does not exist:** write ONE entry, for the newest tag only. Do not
  rebuild the project's whole history.
- **CHANGELOG.md exists:** write one entry for each tag newer than the newest version in
  the file, oldest first, so the newest ends up on top.

Get the date of the tag (always `YYYY-MM-DD`). Never use today's date:

```bash
git for-each-ref --format='%(creatordate:short)' refs/tags/<TAG>
```

(For tags made with `git tag -a` this is the day the tag was created; for plain
`git tag` it is the date of the tagged commit. Both are correct.)

### 3. Read the commits of the release

`<PREV>` is the tag just before `<TAG>`. If `<TAG>` is the first tag, use just `<TAG>`
instead of `<PREV>..<TAG>`.

```bash
git log --no-merges --format='%h %s%n%b' <PREV>..<TAG>
git diff --stat <PREV>..<TAG>
```

Only when a commit message does not explain what changed, look at that commit's files:

```bash
git show --stat --format='%h %s' <SHA>
```

Do not read full diffs or source files unless the developer asks you to.

### 4. Look for known issues (cheap)

```bash
git diff -U0 <PREV>..<TAG> | grep -E '^\+.*(TODO|FIXME|HACK|BUG)'
git log --format='%h %s %b' <PREV>..<TAG> | grep -iE 'known issue|todo|wip|not working|broken'
```

No output (exit code 1) just means nothing was found; it is not an error.
If nothing is found, ask the developer: "Are there any known issues in this version?"

### 5. Classify (only relevant, user-visible changes)

Skip refactors, formatting, comments, renames, build/CI files, merge commits and
"fix typo" commits, unless the player or user would notice the change.

Unfinished work (commit says "WIP", "not working yet", etc.) is NOT listed under `New` or
`Changes`. Only write what is missing or broken under `Known Issues`
(e.g. `- Pause menu does not work with gamepad yet.`).

| Type | Use it for |
|---|---|
| `New` | Something that did not exist before (level, enemy, menu, mechanic, sound). |
| `Changes` | Something that already existed and now works or looks different. |
| `Bug Fixes` | **Only** problems solved that have an issue tracker ticket. Format: `- <TICKET>-<NUMBER> <Ticket title>.` (e.g. `- PRJ-10 Mouse input does not work.`) |
| `Known Issues` | Problems or missing work the developer knows about: TODO/FIXME added in the code, or mentioned in a commit message, or told to you by the developer. |

Bug Fixes rules:
- Take the ticket ID and title from the commit message (pattern like `PRJ-10`).
- If a commit fixes a bug but has no ticket ID or title, ask the developer for them.
- If the developer says there is no ticket, or does not answer, put the item under
  `Changes` instead (e.g. `- Enemy no longer goes through walls.`).

### 6. Write CHANGELOG.md

Format rules:
- A. Dates are `YYYY-MM-DD`.
- B. The first line is exactly `# Changelog` (H1, this exact capitalization).
- C. No text between `# Changelog` and the first version entry.
- D. Versions go from newest (top) to oldest (bottom).
- E. Exactly one blank line between headings and lists. No double blank lines, no `---`,
  no text between version entries.
- F. Version titles are H2: `## [X.Y.Z] - YYYY-MM-DD`.
- G. Change types are H3 and only these, in this order: `### New`, `### Changes`,
  `### Bug Fixes`, `### Known Issues`. Leave out the ones that are empty.
- Each item is one short line starting with `- ` and ending with `.`.

New entries go right below `# Changelog`, above the previous newest version.

### 7. Report to the developer

1. Show the final diff:
   ```bash
   git diff -- CHANGELOG.md                      # if CHANGELOG.md was already in git
   git diff --no-index /dev/null CHANGELOG.md    # if CHANGELOG.md is new
   ```
   (`--no-index` exits with code 1 when there are differences; that is expected.)
2. For every changelog item, list the commits (short SHA + subject) or files that
   support it, for example:
   ```
   - Level 2.                       <- a1b2c3d Add level 2 scene
   - PRJ-10 Mouse input does not work. <- d4e5f6a Fix PRJ-10 mouse input
   ```
3. List the commits you skipped as not relevant, in one line each.
4. List every question you asked or still have for the developer, and what you did
   while it was unanswered. Always include each bug fix you put under `Changes` because
   it had no ticket, so the developer can give you the ticket ID and title.

Do not commit. The developer reviews and commits the change.
