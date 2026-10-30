import Foundation
import SQLite3

final class SQLiteStore {
    static let databaseName = "EdwardsIndexAI.sqlite"

    private var db: OpaquePointer?
    private let databaseURL: URL

    init() {
        let docs = FileManager.default.urls(for: .documentDirectory, in: .userDomainMask).first!
        self.databaseURL = docs.appendingPathComponent(SQLiteStore.databaseName)
        self.db = nil
        setupDatabase()
    }

    private func setupDatabase() {
        if sqlite3_open(databaseURL.path, &db) != SQLITE_OK {
            print("Failed to open database")
            return
        }

        let createTheme = "CREATE TABLE IF NOT EXISTS Theme (id TEXT PRIMARY KEY, name TEXT, category TEXT);"
        let createIndexMarker = "CREATE TABLE IF NOT EXISTS IndexMarker (id TEXT PRIMARY KEY, alphanumericTag TEXT, dateCreated TEXT, themeId TEXT);"
        let createBookCoordinate = "CREATE TABLE IF NOT EXISTS BookCoordinate (id TEXT PRIMARY KEY, markerId TEXT, bookName TEXT, chapter INTEGER, verse INTEGER, page INTEGER, subject TEXT);"
        let createAnnotation = "CREATE TABLE IF NOT EXISTS Annotation (id TEXT PRIMARY KEY, text TEXT, dateCreated TEXT, themeId TEXT);"

        execute(createTheme)
        execute(createIndexMarker)
        execute(createBookCoordinate)
        execute(createAnnotation)

        if countRows("Theme") == 0 {
            seedData()
        }
    }

    private func execute(_ sql: String) {
        var statement: OpaquePointer?
        if sqlite3_prepare_v2(db, sql, -1, &statement, nil) == SQLITE_OK {
            sqlite3_step(statement)
            sqlite3_finalize(statement)
        }
    }

    private func countRows(_ table: String) -> Int {
        let sql = "SELECT COUNT(*) FROM \(table);"
        var statement: OpaquePointer?
        var count = 0

        if sqlite3_prepare_v2(db, sql, -1, &statement, nil) == SQLITE_OK {
            if sqlite3_step(statement) == SQLITE_ROW {
                count = Int(sqlite3_column_int(statement, 0))
            }
            sqlite3_finalize(statement)
        }
        return count
    }

    private func seedData() {
        let themes = [
            Theme(name: "Providence", category: "Doctrine"),
            Theme(name: "Prayer", category: "Devotion"),
            Theme(name: "Grace", category: "Theology"),
            Theme(name: "Humility", category: "Character")
        ]

        let coordinates = [
            BookCoordinate(bookName: "Romans", chapter: 8, verse: 28, page: 142, subject: "All things work together"),
            BookCoordinate(bookName: "Philippians", chapter: 4, verse: 6, page: 201, subject: "Prayer and peace"),
            BookCoordinate(bookName: "Ephesians", chapter: 2, verse: 8, page: 177, subject: "Grace through faith"),
            BookCoordinate(bookName: "James", chapter: 4, verse: 10, page: 88, subject: "Humility before God")
        ]

        var markerList: [IndexMarker] = []
        markerList.append(IndexMarker(alphanumericTag: "A001", dateCreated: Date(), theme: themes[0], coordinates: [coordinates[0]]))
        markerList.append(IndexMarker(alphanumericTag: "P204", dateCreated: Date(), theme: themes[1], coordinates: [coordinates[1]]))
        markerList.append(IndexMarker(alphanumericTag: "G118", dateCreated: Date(), theme: themes[2], coordinates: [coordinates[2]]))
        markerList.append(IndexMarker(alphanumericTag: "H009", dateCreated: Date(), theme: themes[3], coordinates: [coordinates[3]]))

        let annotations = [
            Annotation(text: "Providence connects the believer's endurance with divine purpose.", dateCreated: Date(), theme: themes[0]),
            Annotation(text: "Prayer is a posture of dependence and peace.", dateCreated: Date(), theme: themes[1]),
            Annotation(text: "Grace is the gift that transforms the person before merit is earned.", dateCreated: Date(), theme: themes[2]),
            Annotation(text: "Humility is the path to receiving guidance and restoration.", dateCreated: Date(), theme: themes[3])
        ]

        for theme in themes {
            insertTheme(theme)
        }

        for marker in markerList {
            insertMarker(marker)
            for coordinate in marker.coordinates {
                insertCoordinate(markerid: marker.id.uuidString, coordinate: coordinate)
            }
        }

        for annotation in annotations {
            insertAnnotation(annotation)
        }
    }

    func allThemes() -> [Theme] {
        var results: [Theme] = []
        let sql = "SELECT id, name, category FROM Theme;"
        var statement: OpaquePointer?

        if sqlite3_prepare_v2(db, sql, -1, &statement, nil) == SQLITE_OK {
            while sqlite3_step(statement) == SQLITE_ROW {
                let id = String(cString: sqlite3_column_text(statement, 0))
                let name = String(cString: sqlite3_column_text(statement, 1))
                let category = String(cString: sqlite3_column_text(statement, 2))
                let theme = Theme(name: name, category: category)
                _ = id
                results.append(theme)
            }
            sqlite3_finalize(statement)
        }
        return results
    }

    func allMarkers() -> [IndexMarker] {
        var results: [IndexMarker] = []
        let themes = allThemes()
        let sql = "SELECT id, alphanumericTag, dateCreated, themeId FROM IndexMarker;"
        var statement: OpaquePointer?

        if sqlite3_prepare_v2(db, sql, -1, &statement, nil) == SQLITE_OK {
            while sqlite3_step(statement) == SQLITE_ROW {
                let id = String(cString: sqlite3_column_text(statement, 0))
                let tag = String(cString: sqlite3_column_text(statement, 1))
                let dateText = String(cString: sqlite3_column_text(statement, 2))
                let themeId = String(cString: sqlite3_column_text(statement, 3))
                let theme = themes.first { $0.name == themeId } ?? themes[0]
                let date = ISO8601DateFormatter().date(from: dateText) ?? Date()
                let coordinates = allCoordinates(for: id)
                results.append(IndexMarker(alphanumericTag: tag, dateCreated: date, theme: theme, coordinates: coordinates))
            }
            sqlite3_finalize(statement)
        }
        return results
    }

    func allCoordinates(for markerId: String) -> [BookCoordinate] {
        var results: [BookCoordinate] = []
        let sql = "SELECT bookName, chapter, verse, page, subject FROM BookCoordinate WHERE markerId = '\(markerId)';"
        var statement: OpaquePointer?

        if sqlite3_prepare_v2(db, sql, -1, &statement, nil) == SQLITE_OK {
            while sqlite3_step(statement) == SQLITE_ROW {
                let bookName = String(cString: sqlite3_column_text(statement, 0))
                let chapter = Int(sqlite3_column_int(statement, 1))
                let verse = Int(sqlite3_column_int(statement, 2))
                let page = Int(sqlite3_column_int(statement, 3))
                let subject = String(cString: sqlite3_column_text(statement, 4))
                results.append(BookCoordinate(bookName: bookName, chapter: chapter, verse: verse, page: page, subject: subject))
            }
            sqlite3_finalize(statement)
        }
        return results
    }

    func allAnnotations() -> [Annotation] {
        var results: [Annotation] = []
        let themes = allThemes()
        let sql = "SELECT text, dateCreated, themeId FROM Annotation;"
        var statement: OpaquePointer?

        if sqlite3_prepare_v2(db, sql, -1, &statement, nil) == SQLITE_OK {
            while sqlite3_step(statement) == SQLITE_ROW {
                let text = String(cString: sqlite3_column_text(statement, 0))
                let dateText = String(cString: sqlite3_column_text(statement, 1))
                let themeId = String(cString: sqlite3_column_text(statement, 2))
                let date = ISO8601DateFormatter().date(from: dateText) ?? Date()
                let theme = themes.first { $0.name == themeId } ?? themes[0]
                results.append(Annotation(text: text, dateCreated: date, theme: theme))
            }
            sqlite3_finalize(statement)
        }
        return results
    }

    private func insertTheme(_ theme: Theme) {
        let sql = "INSERT INTO Theme (id, name, category) VALUES (?, ?, ?);"
        var statement: OpaquePointer?
        if sqlite3_prepare_v2(db, sql, -1, &statement, nil) == SQLITE_OK {
            sqlite3_bind_text(statement, 1, theme.id.uuidString, -1, nil)
            sqlite3_bind_text(statement, 2, theme.name, -1, nil)
            sqlite3_bind_text(statement, 3, theme.category, -1, nil)
            sqlite3_step(statement)
            sqlite3_finalize(statement)
        }
    }

    private func insertMarker(_ marker: IndexMarker) {
        let sql = "INSERT INTO IndexMarker (id, alphanumericTag, dateCreated, themeId) VALUES (?, ?, ?, ?);"
        var statement: OpaquePointer?
        if sqlite3_prepare_v2(db, sql, -1, &statement, nil) == SQLITE_OK {
            sqlite3_bind_text(statement, 1, marker.id.uuidString, -1, nil)
            sqlite3_bind_text(statement, 2, marker.alphanumericTag, -1, nil)
            sqlite3_bind_text(statement, 3, ISO8601DateFormatter().string(from: marker.dateCreated), -1, nil)
            sqlite3_bind_text(statement, 4, marker.theme.name, -1, nil)
            sqlite3_step(statement)
            sqlite3_finalize(statement)
        }
    }

    private func insertCoordinate(markerid: String, coordinate: BookCoordinate) {
        let sql = "INSERT INTO BookCoordinate (id, markerId, bookName, chapter, verse, page, subject) VALUES (?, ?, ?, ?, ?, ?, ?);"
        var statement: OpaquePointer?
        if sqlite3_prepare_v2(db, sql, -1, &statement, nil) == SQLITE_OK {
            sqlite3_bind_text(statement, 1, coordinate.id.uuidString, -1, nil)
            sqlite3_bind_text(statement, 2, markerid, -1, nil)
            sqlite3_bind_text(statement, 3, coordinate.bookName, -1, nil)
            sqlite3_bind_int(statement, 4, Int32(coordinate.chapter))
            sqlite3_bind_int(statement, 5, Int32(coordinate.verse))
            sqlite3_bind_int(statement, 6, Int32(coordinate.page))
            sqlite3_bind_text(statement, 7, coordinate.subject, -1, nil)
            sqlite3_step(statement)
            sqlite3_finalize(statement)
        }
    }

    private func insertAnnotation(_ annotation: Annotation) {
        let sql = "INSERT INTO Annotation (id, text, dateCreated, themeId) VALUES (?, ?, ?, ?);"
        var statement: OpaquePointer?
        if sqlite3_prepare_v2(db, sql, -1, &statement, nil) == SQLITE_OK {
            sqlite3_bind_text(statement, 1, annotation.id.uuidString, -1, nil)
            sqlite3_bind_text(statement, 2, annotation.text, -1, nil)
            sqlite3_bind_text(statement, 3, ISO8601DateFormatter().string(from: annotation.dateCreated), -1, nil)
            sqlite3_bind_text(statement, 4, annotation.theme.name, -1, nil)
            sqlite3_step(statement)
            sqlite3_finalize(statement)
        }
    }
}
