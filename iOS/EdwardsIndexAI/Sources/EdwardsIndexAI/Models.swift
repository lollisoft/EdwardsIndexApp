import Foundation

struct Theme: Identifiable, Hashable, Codable {
    let id: UUID
    let name: String
    let category: String

    init(id: UUID = UUID(), name: String, category: String) {
        self.id = id
        self.name = name
        self.category = category
    }
}

struct BookCoordinate: Identifiable, Hashable, Codable {
    let id: UUID
    let bookName: String
    let chapter: Int
    let verse: Int
    let page: Int
    let subject: String

    init(id: UUID = UUID(), bookName: String, chapter: Int, verse: Int, page: Int, subject: String) {
        self.id = id
        self.bookName = bookName
        self.chapter = chapter
        self.verse = verse
        self.page = page
        self.subject = subject
    }
}

struct IndexMarker: Identifiable, Hashable, Codable {
    let id: UUID
    let alphanumericTag: String
    let dateCreated: Date
    let theme: Theme
    let coordinates: [BookCoordinate]

    init(id: UUID = UUID(), alphanumericTag: String, dateCreated: Date, theme: Theme, coordinates: [BookCoordinate]) {
        self.id = id
        self.alphanumericTag = alphanumericTag
        self.dateCreated = dateCreated
        self.theme = theme
        self.coordinates = coordinates
    }
}

struct Annotation: Identifiable, Hashable, Codable {
    let id: UUID
    let text: String
    let dateCreated: Date
    let theme: Theme

    init(id: UUID = UUID(), text: String, dateCreated: Date, theme: Theme) {
        self.id = id
        self.text = text
        self.dateCreated = dateCreated
        self.theme = theme
    }
}

struct AIInsight: Identifiable, Codable {
    let id: UUID
    let title: String
    let summary: String
    let confidence: Int

    init(id: UUID = UUID(), title: String, summary: String, confidence: Int) {
        self.id = id
        self.title = title
        self.summary = summary
        self.confidence = confidence
    }
}

struct EdwardsIndexSnapshot: Codable {
    var themes: [Theme]
    var markers: [IndexMarker]
    var annotations: [Annotation]
}
