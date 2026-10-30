# EdwardsIndexAI

This folder contains a SwiftUI prototype for an iOS app inspired by the UML model in the `EdwardsIndexApp.xmi` file.

## Included concepts

- `IndexMarker`: alphanumeric reference tags and creation dates
- `BookCoordinate`: book, chapter, verse, page and subject mapping
- `Annotation`: free-form note text and timestamps
- `Theme`: thematic grouping and category classification

## App purpose

The app presents an AI-driven study dashboard that helps a user explore annotated biblical references, surface thematic insights, and review content by topic.

## Build

Open the package in Xcode 15+ or run:

```bash
cd iOS/EdwardsIndexAI
swift build
```

The app is a lightweight SwiftUI showcase and can be expanded into a full production app with persistence, AI services, and real document storage.
