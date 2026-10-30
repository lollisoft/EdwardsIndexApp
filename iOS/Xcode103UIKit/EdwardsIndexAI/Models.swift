import Foundation

struct Theme {
    let id: UUID
    let name: String
    let category: String

    init(name: String, category: String) {
        self.id = UUID()
        self.name = name
        self.category = category
    }
}

struct BookCoordinate {
    let id: UUID
    let bookName: String
    let chapter: Int
    let verse: Int
    let page: Int
    let subject: String

    init(bookName: String, chapter: Int, verse: Int, page: Int, subject: String) {
        self.id = UUID()
        self.bookName = bookName
        self.chapter = chapter
        self.verse = verse
        self.page = page
        self.subject = subject
    }
}

struct IndexMarker {
    let id: UUID
    let alphanumericTag: String
    let dateCreated: Date
    let theme: Theme
    let coordinates: [BookCoordinate]

    init(alphanumericTag: String, dateCreated: Date, theme: Theme, coordinates: [BookCoordinate]) {
        self.id = UUID()
        self.alphanumericTag = alphanumericTag
        self.dateCreated = dateCreated
        self.theme = theme
        self.coordinates = coordinates
    }
}

struct Annotation {
    let id: UUID
    let text: String
    let dateCreated: Date
    let theme: Theme

    init(text: String, dateCreated: Date, theme: Theme) {
        self.id = UUID()
        self.text = text
        self.dateCreated = dateCreated
        self.theme = theme
    }
}
